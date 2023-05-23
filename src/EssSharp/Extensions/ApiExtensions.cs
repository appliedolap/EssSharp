using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

using EssSharp.Client;
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
                    _                           => throw new WebException($@"The request failed with status code {(int)response.StatusCode} ({response.StatusCode}). {(!string.IsNullOrEmpty(hre.Message?.Trim()) ? hre.Message.TrimEnd('.').Trim() + ". " : null)}{(!string.IsNullOrEmpty(hre.InnerException?.Message?.Trim()) ? hre.InnerException.Message.TrimEnd('.').Trim() + "." : null)}".TrimEnd(), hre.InnerException, WebExceptionStatus.UnknownError, new WebExceptionRestResponse(response))
                },
                _                               => response.StatusCode.IsSuccessful()
            };

        /// <summary />
        /// <param name="statusCode" />
        private static bool IsSuccessful( this HttpStatusCode statusCode ) => (int)statusCode >= 200 && (int)statusCode <= 399;
    }

    /// <summary />
    internal class WebExceptionRestResponse : WebResponse
    {
        private readonly RestResponse _response;

        /// <summary />
        /// <param name="response" />
        public WebExceptionRestResponse( RestResponse response ) { _response = response; }

        /// <summary />
        public HttpStatusCode? StatusCode => _response?.StatusCode;
    }
}
