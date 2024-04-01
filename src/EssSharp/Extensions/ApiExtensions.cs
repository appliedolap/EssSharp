using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

using EssSharp.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EssSharp.Api
{
    internal static class ApiExtensions
    {
        /// <summary>
        /// Downloads and returns a <see cref="string"/> containing the latest application log file's text content.
        /// </summary>
        /// <param name="api" />
        /// <param name="applicationName" />
        /// <param name="operationIndex" />
        /// <param name="cancellationToken" />
        internal static async Task<string> ApplicationLogsDownloadLatestLogFileContentAsync( this ApplicationLogsApi api, string applicationName, int operationIndex = 0, CancellationToken cancellationToken = default )
        {
            var rawContent = (await api.ApplicationLogsDownloadLatestLogFileWithHttpInfoAsync(applicationName, operationIndex, cancellationToken).ConfigureAwait(false))?.RawContent;

            if (string.IsNullOrEmpty(rawContent))
                return null;

            return rawContent;
        }

        /// <summary>
        /// A extension overload that returns a boolean to indicate whether an exception can 
        /// reasonably thrown for an <see cref="RestResponse"/>.
        /// </summary>
        /// <param name="response" />
        internal static bool IsSuccessful( this RestResponse response ) 
            => response.ErrorException switch
            {
                OperationCanceledException  oce => throw oce,
                XmlException                    => throw new WebException($@"The request failed with status code {(int)response.StatusCode} ({response.StatusCode}).", response.ErrorException),
                ApiException                 ae => ae.ErrorCode is 500 && response.StatusCode.IsSuccessful() ? true : 
                                                   throw new WebException($@"The request failed with status code {(int)response.StatusCode} ({response.StatusCode}).", response.ErrorException, WebExceptionStatus.ProtocolError, new WebExceptionRestResponse(response)),
                HttpRequestException        hre => response.StatusCode switch
                {
                    0                           => throw new WebException($@"The request failed. {(!string.IsNullOrEmpty(hre.Message?.Trim()) ? hre.Message.TrimEnd('.').Trim() + ". " : null)}{(!string.IsNullOrEmpty(hre.InnerException?.Message?.Trim()) ? hre.InnerException.Message.TrimEnd('.').Trim() + "." : null)}".TrimEnd(), hre.InnerException, WebExceptionStatus.UnknownError, new WebExceptionRestResponse(response)),
                    HttpStatusCode.Unauthorized => throw new WebException($@"The request failed with status code {(int)response.StatusCode} ({response.StatusCode}). Verify that the credentials are valid and the user is authorized to access this resource.", hre.InnerException, WebExceptionStatus.UnknownError, new WebExceptionRestResponse(response)),
                    HttpStatusCode.BadRequest   => throw new WebException($@"The request failed with status code {(int)response.StatusCode} ({response.StatusCode}). {ParseJsonErrorMessage(response.Content) ?? (!string.IsNullOrEmpty(hre.Message?.Trim()) ? hre.Message.TrimEnd('.').Trim() + ". " : null)}{(!string.IsNullOrEmpty(hre.InnerException?.Message?.Trim()) ? hre.InnerException.Message.TrimEnd('.').Trim() + "." : null)}".TrimEnd(), hre.InnerException, WebExceptionStatus.UnknownError, new WebExceptionRestResponse(response)),
                    _                           => throw new WebException($@"The request failed with status code {(int)response.StatusCode} ({response.StatusCode}). {(!string.IsNullOrEmpty(hre.Message?.Trim()) ? hre.Message.TrimEnd('.').Trim() + ". " : null)}{(!string.IsNullOrEmpty(hre.InnerException?.Message?.Trim()) ? hre.InnerException.Message.TrimEnd('.').Trim() + "." : null)}".TrimEnd(), hre.InnerException, WebExceptionStatus.UnknownError, new WebExceptionRestResponse(response))
                },
                _                               => response.StatusCode.IsSuccessful()
            };

        /// <summary />
        /// <param name="statusCode" />
        private static bool IsSuccessful( this HttpStatusCode statusCode ) => (int)statusCode >= 200 && (int)statusCode <= 399;

        #region Embarrasing Extensions

        private static string ParseJsonErrorMessage( string json )
        {
            try
            {
                if ( JObject.Parse(json).TryGetValue("errorMessage", StringComparison.OrdinalIgnoreCase, out var token) && token.Value<string>() is { Length: > 0 } message )
                    return message.AppendPunctuation('.', abortIfTrailingNewline: false);
            }
            catch
            {
                // swallow any exception here.
            }

            return null;
        }

        /// <summary>
        /// The array of whitespace characters trimmed by <see cref="string.Trim()"/>.
        /// </summary>
        private static readonly char[] WhiteSpaceCharacters = { '\x0009', '\x000a', '\x000b', '\x000c', '\x000d', '\x0020', '\x0085', '\x00a0', '\x1680', '\x2000', '\x2001', '\x2002', '\x2003', '\x2004', '\x2005', '\x2006', '\x2007', '\x2008', '\x2009', '\x200a', '\x2028', '\x2029', '\x202f', '\x205f', '\x3000' };

        /// <summary>
        /// Appends the given character to the end of the given string (if necessary).
        /// </summary>
        /// <param name="input"></param>
        /// <param name="character"></param>
        /// <param name="trim"></param>
        /// <param name="abortIfTrailingNewline"></param>
        private static string AppendPunctuation( this string input, char character, bool trim = true, bool abortIfTrailingNewline = true )
        {
            if ( string.IsNullOrWhiteSpace(input) )
                return input;

            if ( abortIfTrailingNewline && trim && input.TrimEnd(WhiteSpaceCharacters.Where(c => c != '\n').ToArray()).EndsWith('\n'.ToString()) )
                return input.Trim();

            if ( trim )
                input = input.Trim();

            input = input.Trim(character);

            if ( trim )
                input = input.Trim();

            return input + character;
        }

        private static string GetFormattedRestRequest( this RequestOptions request )
        {
            if ( request is null )
                return string.Empty;

            // Initialize the headers dictionary.
            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if ( request.PathParameters?.Any() is true )
                foreach ( var kvp in request.PathParameters )
                    if ( !string.IsNullOrEmpty(kvp.Key) )
                        headers[kvp.Key] = string.Join("; ", kvp.Value);

            // Add each header exposed by the standard headers collection to the headers dictionary.
            if ( request.QueryParameters?.Any() is true )
                foreach ( var kvp in request.QueryParameters )
                    if ( !string.IsNullOrEmpty(kvp.Key) )
                        headers[kvp.Key] = string.Join("; ", kvp.Value);

            if ( request.HeaderParameters?.Any() is true )
                foreach ( var kvp in request.HeaderParameters )
                    if ( !string.IsNullOrEmpty(kvp.Key) )
                        headers[kvp.Key] = string.Join("; ", kvp.Value);

            if ( request.FormParameters?.Any() is true )
                foreach ( var kvp in request.FormParameters )
                    if ( !string.IsNullOrEmpty(kvp.Key) )
                        headers[kvp.Key] = string.Join("; ", kvp.Value);

            if ( request.FileParameters?.Any() is true )
                foreach ( var kvp in request.FileParameters )
                    if ( !string.IsNullOrEmpty(kvp.Key) )
                        headers[kvp.Key] = string.Join("; ", kvp.Value);

            return string.Join(" ; ", headers.Keys
                    .OrderBy(key => key)
                    .Select(key => $@"{$@"{key}:".PadRight(key.Length)} {headers[key]}"));

            //return string.Empty;
        }

        private static string GetFormattedRestResponse( this IApiResponse response )
        {
            if ( response is null )
                return string.Empty;

            // Initialize the headers dictionary.
            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Add each header exposed by the standard headers collection to the headers dictionary.
            if ( response.Headers?.Any() is true )
                foreach ( var kvp in response.Headers )
                    if ( !string.IsNullOrEmpty(kvp.Key) )
                        headers[kvp.Key] = string.Join("; ", kvp.Value);

            return string.Join(" ; ", headers.Keys
                    .OrderBy(key => key)
                    .Select(key => $@"{$@"{key}:".PadRight(key.Length)} {headers[key]}"));

            //return string.Empty;
        }

        #endregion
    }

    /// <summary />
    public class WebExceptionRestResponse : WebResponse
    {
        private readonly RestResponse _response;

        /// <summary />
        /// <param name="response" />
        public WebExceptionRestResponse( RestResponse response ) { _response = response; }

        /// <summary />
        public HttpStatusCode? StatusCode => _response?.StatusCode;
    }
    
}
