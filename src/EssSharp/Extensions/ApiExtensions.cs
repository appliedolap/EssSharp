using System;
using System.IO;
using System.Net;
using System.Text;
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
        /// Downloads and returns a <see cref="Stream"/> containing the latest application log file's text content.
        /// </summary>
        /// <param name="api" />
        /// <param name="applicationName" />
        /// <param name="operationIndex" />
        /// <param name="cancellationToken" />
        internal static async Task<Stream> ApplicationLogsDownloadLatestLogFileStreamAsync( this ApplicationLogsApi api, string applicationName, int operationIndex = 0, CancellationToken cancellationToken = default )
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(await ApplicationLogsDownloadLatestLogFileContentAsync(api, applicationName, operationIndex, cancellationToken).ConfigureAwait(false)));
        }

        /// <summary>
        /// A extension overload that returns a boolean to indicate whether an exception can 
        /// reasonably thrown for an <see cref="RestResponse"/>.
        /// </summary>
        /// <param name="response" />
        internal static bool IsSuccessful( this RestResponse response ) 
            => response.ErrorException switch
            {
                OperationCanceledException   oce => throw oce,
                XmlException or ApiException     => throw new WebException($@"The request failed with status code {(int)response.StatusCode} - {response.StatusCode}.", response.ErrorException),
                WebException                 we  => response.StatusCode > 0
                                                        ? throw new WebException($@"The request failed with status code {(int)response.StatusCode} - {response.StatusCode}. {(!string.IsNullOrEmpty(we.Message?.Trim()) ? we.Message.TrimEnd('.').Trim() + ". " : null)}{(!string.IsNullOrEmpty(we.InnerException?.Message?.Trim()) ? we.InnerException.Message.TrimEnd('.').Trim() + "." : null)}".TrimEnd())
                                                        : throw new WebException($@"The request failed. {(!string.IsNullOrEmpty(we.Message?.Trim()) ? we.Message.TrimEnd('.').Trim() + ". " : null)}{(!string.IsNullOrEmpty(we.InnerException?.Message?.Trim()) ? we.InnerException.Message.TrimEnd('.').Trim() + "." : null)}".TrimEnd()),
                                             _   => response.StatusCode.IsSuccessful()
            };
        

        /// <summary />
        /// <param name="statusCode" />
        private static bool IsSuccessful( this HttpStatusCode statusCode ) => (int)statusCode >= 200 && (int)statusCode <= 399;
    }
}
