using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using EssSharp.Client;

using Newtonsoft.Json;
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

        private static readonly string[] _errorPropertyNames =
        {
            "errorMessage",
            "error"
        };

        // An MDX query starts with SELECT or WITH, and either lays out its axes with ON or names its cube after FROM.
        // Prose that merely contains the words "select" and "from" - an explanation, an error message, a sample
        // question - is not MDX.
        private static readonly Regex _mdxStart = new Regex(@"^\s*(WITH|SELECT)\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        private static readonly Regex _mdxAxis  = new Regex(@"\bON\s+(COLUMNS|ROWS|PAGES|CHAPTERS|SECTIONS|AXIS\s*\(\s*\d+\s*\)|\d+)\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        private static readonly Regex _mdxCube  = new Regex(@"\bFROM\s+(\[|\w+\s*\.\s*\w)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        private static readonly string[] _explanationPropertyNames =
        {
            "explanation",
            "info",
            "message",
            "response",
            "answer"
        };

        // Properties that may hold the sample queries as a list of plain strings, for a response that wraps the list.
        private static readonly HashSet<string> _sampleListPropertyNames = new HashSet<string>(StringComparer.Ordinal)
        {
            "samplequeries",
            "samples",
            "queries",
            "naturallanguagequeries",
            "nlqs",
            "questions",
            "items"
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
            {
                var error    = FindNamedString(token, _errorPropertyNames);
                var reported = string.IsNullOrWhiteSpace(error) ? string.Empty : $" The server reported: {error.Trim()}";

                throw new InvalidOperationException($"Received an empty or invalid MDX generator response.{reported} {response?.RawContent}".TrimEnd());
            }

            mdx = RemoveMarkdownCodeFence(mdx);

            var explanation = FindNamedString(token, _explanationPropertyNames);
            if ( string.Equals(RemoveMarkdownCodeFence(explanation), mdx, StringComparison.Ordinal) )
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

            var queries    = new List<string>();
            var recognized = token is JArray;

            if ( token is JArray array )
            {
                // The shape Essbase 26ai returns: a list of natural-language queries.
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

                // The queries may also be a list of plain strings held by a property of the response.
                foreach ( var property in GetSelfAndDescendants(token).OfType<JProperty>() )
                {
                    if ( property.Value is not JArray list || !_sampleListPropertyNames.Contains(NormalizePropertyName(property.Name)) )
                        continue;

                    recognized = true;

                    foreach ( var item in list.Where(item => item.Type is JTokenType.String) )
                        AddQuery(queries, item.Value<string>());
                }
            }

            // An empty list is a valid answer. Content that is not a list of queries is not: returning every string in
            // it (a status, an error message) would present those as sample queries.
            if ( queries.Count is 0 && !recognized && GetStringValues(token).Any(value => !string.IsNullOrWhiteSpace(value)) )
                throw new InvalidOperationException($"Received an unrecognized AI sample queries response. {response?.RawContent}".TrimEnd());

            return queries;
        }

        /// <summary>
        /// Returns a token for the parsed or raw response content.
        /// </summary>
        private static JToken GetResponseToken( ApiResponse<object> response )
        {
            var data = response?.Data;

            if ( data is JToken token )
                return token;

            // When the content type is not one the client reads as JSON, the data is the raw bytes (or the raw text)
            // and not the response. Describing it as a token would turn a JSON envelope into a string of base64, so
            // read the text instead.
            if ( data is not null and not byte[] and not string )
                return JToken.FromObject(data);

            var text = response?.RawContent;

            if ( string.IsNullOrWhiteSpace(text) )
                text = data switch
                {
                    byte[] bytes => Encoding.UTF8.GetString(bytes),
                    string value => value,
                    _            => null
                };

            if ( string.IsNullOrWhiteSpace(text) )
                return null;

            try
            {
                return JToken.Parse(text);
            }
            catch ( JsonReaderException )
            {
                return new JValue(text);
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
            return _mdxStart.IsMatch(value) && (_mdxAxis.IsMatch(value) || _mdxCube.IsMatch(value));
        }

        /// <summary>
        /// Removes an optional Markdown code fence from generated MDX: one on its own lines, with or without a language
        /// after the opening fence, one that is never closed, and one opened and closed on a single line.
        /// </summary>
        private static string RemoveMarkdownCodeFence( string value )
        {
            value = value?.Trim();
            if ( string.IsNullOrEmpty(value) || !value.StartsWith("```", StringComparison.Ordinal) )
                return value;

            var closingFence = value.LastIndexOf("```", StringComparison.Ordinal);
            var body         = value.Substring(3, (closingFence < 3 ? value.Length : closingFence) - 3);

            // The opening fence line may name a language, such as mdx, and nothing else.
            var firstLineEnd = body.IndexOf('\n');
            if ( firstLineEnd >= 0 && Regex.IsMatch(body.Substring(0, firstLineEnd).Trim(), @"^[\w+-]*$") )
                return body.Substring(firstLineEnd + 1).Trim();

            // Otherwise the code starts on the line of the fence, perhaps after a language.
            return Regex.Replace(body.Trim(), @"^(?:mdx|sql)\s+(?=\S)", string.Empty, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }

        /// <summary>
        /// Normalizes a JSON property name for tolerant matching.
        /// </summary>
        private static string NormalizePropertyName( string value ) =>
            new string((value ?? string.Empty).Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());
    }
}
