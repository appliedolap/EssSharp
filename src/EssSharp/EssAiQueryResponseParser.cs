using System;
using System.Collections.Generic;
using System.Linq;

using EssSharp.Client;

using Newtonsoft.Json.Linq;

namespace EssSharp
{
    /// <summary>
    /// Converts the unmodeled AI endpoint responses into stable EssSharp results.
    /// </summary>
    /// <remarks>
    /// Oracle's OpenAPI document does not currently describe the AI response bodies,
    /// so the generated client exposes them as <see cref="object"/> values.
    /// </remarks>
    internal static class EssAiQueryResponseParser
    {
        private static readonly string[] _mdxPropertyNames =
        {
            "mdx",
            "generatedMdx",
            "mdxQuery",
            "generatedMdxQuery",
            "generatedQuery",
            "query"
        };

        private static readonly string[] _explanationPropertyNames =
        {
            "explanation",
            "message",
            "response",
            "answer"
        };

        private static readonly string[] _sampleQueryPropertyNames =
        {
            "naturalLanguageQuery",
            "sampleQuery",
            "nlq",
            "question",
            "prompt",
            "query"
        };

        /// <summary>
        /// Converts an AI MDX generator response to an <see cref="EssAiQueryResult"/>.
        /// </summary>
        internal static EssAiQueryResult ToQueryResult( ApiResponse<object> response, string naturalLanguageQuery, string profileName )
        {
            var token = GetResponseToken(response);
            var mdx   = FindNamedString(token, _mdxPropertyNames);

            if ( !LooksLikeMdx(mdx) )
                mdx = GetStringValues(token).FirstOrDefault(LooksLikeMdx);

            if ( string.IsNullOrWhiteSpace(mdx) && LooksLikeMdx(response?.RawContent) )
                mdx = response.RawContent;

            if ( string.IsNullOrWhiteSpace(mdx) )
                throw new InvalidOperationException($"Received an empty or invalid MDX generator response. {response?.RawContent}".TrimEnd());

            mdx = RemoveMarkdownCodeFence(mdx);

            var explanation = FindNamedString(token, _explanationPropertyNames);
            if ( string.Equals(explanation?.Trim(), mdx, StringComparison.Ordinal) )
                explanation = null;

            return new EssAiQueryResult
            {
                NaturalLanguageQuery = naturalLanguageQuery,
                Mdx                  = mdx,
                Explanation          = explanation,
                ProfileName          = profileName,
                RawResponse          = response?.RawContent
            };
        }

        /// <summary>
        /// Converts an AI sample query response to a list of natural-language queries.
        /// </summary>
        internal static List<string> ToSampleQueries( ApiResponse<object> response )
        {
            var token = GetResponseToken(response);
            if ( token is null )
                return new List<string>();

            var queries = new List<string>();

            if ( token is JArray array )
            {
                foreach ( var item in array )
                {
                    if ( item.Type is JTokenType.String )
                        AddQuery(queries, item.Value<string>());
                    else
                        AddNamedStrings(queries, item, _sampleQueryPropertyNames);
                }
            }
            else
            {
                AddNamedStrings(queries, token, _sampleQueryPropertyNames);
            }

            if ( queries.Count is 0 )
                foreach ( var value in GetStringValues(token).Where(value => !LooksLikeMdx(value)) )
                    AddQuery(queries, value);

            return queries;
        }

        /// <summary>
        /// Returns a token for the parsed or raw response content.
        /// </summary>
        private static JToken GetResponseToken( ApiResponse<object> response )
        {
            if ( response?.Data is JToken token )
                return token;

            if ( response?.Data is not null )
                return JToken.FromObject(response.Data);

            if ( string.IsNullOrWhiteSpace(response?.RawContent) )
                return null;

            try
            {
                return JToken.Parse(response.RawContent);
            }
            catch
            {
                return new JValue(response.RawContent);
            }
        }

        /// <summary>
        /// Finds the first non-empty string held by one of the given property names.
        /// </summary>
        private static string FindNamedString( JToken token, IEnumerable<string> propertyNames )
        {
            if ( token is null )
                return null;

            var properties = GetSelfAndDescendants(token)
                .OfType<JProperty>()
                .ToList();

            foreach ( var propertyName in propertyNames )
            {
                var normalizedName = NormalizePropertyName(propertyName);
                var value = properties
                    .Where(property => string.Equals(NormalizePropertyName(property.Name), normalizedName, StringComparison.Ordinal))
                    .SelectMany(property => GetStringValues(property.Value))
                    .FirstOrDefault(candidate => !string.IsNullOrWhiteSpace(candidate));

                if ( !string.IsNullOrWhiteSpace(value) )
                    return value;
            }

            return null;
        }

        /// <summary>
        /// Adds values held by the given property names to a query collection.
        /// </summary>
        private static void AddNamedStrings( ICollection<string> queries, JToken token, IEnumerable<string> propertyNames )
        {
            if ( token is null )
                return;

            var normalizedNames = new HashSet<string>(propertyNames.Select(NormalizePropertyName), StringComparer.Ordinal);
            foreach ( var property in GetSelfAndDescendants(token).OfType<JProperty>() )
                if ( normalizedNames.Contains(NormalizePropertyName(property.Name)) )
                    foreach ( var value in GetStringValues(property.Value) )
                        if ( !LooksLikeMdx(value) )
                            AddQuery(queries, value);
        }

        /// <summary>
        /// Adds a unique, non-empty query to a collection.
        /// </summary>
        private static void AddQuery( ICollection<string> queries, string query )
        {
            query = query?.Trim();
            if ( string.IsNullOrEmpty(query) || queries.Any(item => string.Equals(item, query, StringComparison.OrdinalIgnoreCase)) )
                return;

            queries.Add(query);
        }

        /// <summary>
        /// Enumerates string values contained in a token.
        /// </summary>
        private static IEnumerable<string> GetStringValues( JToken token )
        {
            if ( token is null )
                yield break;

            if ( token.Type is JTokenType.String )
            {
                yield return token.Value<string>();
                yield break;
            }

            foreach ( var value in GetSelfAndDescendants(token).Where(item => item.Type is JTokenType.String) )
                yield return value.Value<string>();
        }

        /// <summary>
        /// Enumerates a token and all of its descendants.
        /// </summary>
        private static IEnumerable<JToken> GetSelfAndDescendants( JToken token )
        {
            if ( token is null )
                yield break;

            yield return token;

            if ( token is JContainer container )
                foreach ( var descendant in container.Descendants() )
                    yield return descendant;
        }

        /// <summary>
        /// Returns whether a value resembles an MDX query.
        /// </summary>
        private static bool LooksLikeMdx( string value )
        {
            if ( string.IsNullOrWhiteSpace(value) )
                return false;

            value = RemoveMarkdownCodeFence(value);
            return value.IndexOf("SELECT", StringComparison.OrdinalIgnoreCase) >= 0 &&
                   value.IndexOf("FROM", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Removes an optional Markdown code fence from generated MDX.
        /// </summary>
        private static string RemoveMarkdownCodeFence( string value )
        {
            value = value?.Trim();
            if ( string.IsNullOrEmpty(value) || !value.StartsWith("```", StringComparison.Ordinal) )
                return value;

            var firstLineEnd = value.IndexOf('\n');
            var closingFence = value.LastIndexOf("```", StringComparison.Ordinal);

            if ( firstLineEnd < 0 || closingFence <= firstLineEnd )
                return value;

            return value.Substring(firstLineEnd + 1, closingFence - firstLineEnd - 1).Trim();
        }

        /// <summary>
        /// Normalizes a JSON property name for tolerant matching.
        /// </summary>
        private static string NormalizePropertyName( string value ) =>
            new string((value ?? string.Empty).Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());
    }
}
