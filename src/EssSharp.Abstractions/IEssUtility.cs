using System.Threading.Tasks;
using System.Threading;
using System.IO;

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
        /// Returns true if the resource is a downloadable file, false if not.
        /// </summary>
        /// <returns>true if the resource is a downloadable file, false if not.</returns>
        public bool IsDownloadable { get; }

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
