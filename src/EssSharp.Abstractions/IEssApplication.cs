using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssApplication : IEssObject
    {
        #region Properties

        /// <summary>
        /// Returns the server that contains this application.
        /// </summary>
        /// <returns>The server that contains this application.</returns>
        IEssServer Server { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Downloads the latest log file for this application.
        /// </summary>
        /// <returns>A <see cref="Stream"/> containing the log file content.</returns>
        public Stream DownloadLatestLogFile();

        /// <summary>
        /// Asynchronously downloads the latest log file for this application.
        /// </summary>
        /// <returns>A <see cref="Stream"/> containing the log file content.</returns>
        public Task<Stream> DownloadLatestLogFileAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Downloads the latest log file for this application.
        /// </summary>
        /// <returns>A <see cref="string"/> containing the log file content.</returns>
        public string DownloadLatestLogFileString();

        /// <summary>
        /// Asynchronously downloads the latest log file for this application.
        /// </summary>
        /// <returns>A <see cref="string"/> containing the log file content.</returns>
        public Task<string> DownloadLatestLogFileStringAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of cubes for this application available to the currently connected user.
        /// </summary>
        /// <returns>A list of <see cref="IEssCube" /> objects under this application.</returns>
        public List<IEssCube> GetCubes();

        /// <summary>
        /// Asynchronously gets the list of cubes for this application available to the currently connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssCube" /> objects under this application.</returns>
        public Task<List<IEssCube>> GetCubesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously gets the list of application-scoped variables available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssVariable"/> objects.</returns>
        public Task<List<IEssApplicationVariable>> GetVariablesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Copy The Application.
        /// </summary>
        public void Copy( String copyName );
        /// <summary>
        /// Copy the application.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <param name="copyName" />
        public Task CopyAsync( String copyName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Deletes The Application.
        /// </summary>
        public void Delete();
        /// <summary>
        /// Delete the application.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task DeleteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Start The Application.
        /// </summary>
        public void Start();
        /// <summary>
        /// Asynchronously starts the application.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task StartAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Stop The Application.
        /// </summary>
        public void Stop();

        /// <summary>
        /// Asynchronously stops the application.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task StopAsync( CancellationToken cancellationToken = default );
        #endregion
    }
    
}
