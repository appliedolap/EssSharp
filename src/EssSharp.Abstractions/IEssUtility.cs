using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssUtility : IEssObject
    {
        #region Properties

        /// <summary>
        /// Returns the server that contains this job.
        /// </summary>
        /// <returns>The server that contains this job.</returns>
        public IEssServer Server { get; }

        /// <summary>
        /// Returns whether the utility can be downloaded.
        /// </summary>
        /// <returns>A <see cref="bool"/> that represents whether the utility can be downloaded.</returns>
        public bool IsDownloadable { get; }

        /// <summary>
        /// Returns a url from which the utility can be downloaded.
        /// </summary>
        /// <returns>A <see cref="Uri"/> that represents the location of the utility.</returns>
        public Uri Url { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Downloads the utility.
        /// </summary>
        /// <returns>A <see cref="Stream"/> containing the utility content.</returns>
        public Stream Download();

        /// <summary>
        /// Asynchronously downloads the utility.
        /// </summary>
        /// <returns>A <see cref="Stream"/> containing the utility content.</returns>
        public Task<Stream> DownloadAsync( CancellationToken cancellationToken = default );

        #endregion
    }
}
