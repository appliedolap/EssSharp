using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Model;

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
    }
}
