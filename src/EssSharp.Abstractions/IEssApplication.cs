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
        /// Create a new cube on an Application
        /// </summary>
        /// <param name="cubeName"></param>
        /// <param name="options"></param>
        public IEssCube CreateCube( string cubeName, EssDatabaseCreationOptions options = null );

        /// <summary>
        /// Create a new cube on an Application
        /// </summary>
        /// <param name="cubeName"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssCube> CreateCubeAsync( string cubeName, EssDatabaseCreationOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new cube (and application) from an existing server file.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="options" />
        public IEssCube CreateCubeFromWorkbook( string cubeName, EssJobImportExcelOptions options );

        /// <summary>
        /// Asynchronously creates a new cube (and application) from a server file.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public Task<IEssCube> CreateCubeFromWorkbookAsync( string cubeName, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new cube (and application) from a local workbook file path.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="localWorkbookPath" />
        /// <param name="options" />
        public IEssCube CreateCubeFromWorkbook( string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null );

        /// <summary>
        /// Asynchronously creates a new cube (and application) from a local workbook file path.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="localWorkbookPath" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public Task<IEssCube> CreateCubeFromWorkbookAsync( string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new cube (and application) from a workbook stream.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="stream" />
        /// <param name="options" />
        public IEssCube CreateCubeFromWorkbook( string cubeName, Stream stream, EssJobImportExcelOptions options = null );

        /// <summary>
        /// Asynchronously creates a new cube (and application) from a workbook stream.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="stream" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public Task<IEssCube> CreateCubeFromWorkbookAsync( string cubeName, Stream stream, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Create an application variable
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="varName"></param>
        /// <param name="value"></param>
        public IEssApplicationVariable CreateApplicationVariable( string varName, string value );

        /// <summary>
        /// Create an application variable
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="varName"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssApplicationVariable> CreateApplicationVariableAsync( string varName, string value, CancellationToken cancellationToken = default );

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
        /// Exports a cube to an excel workbook
        /// </summary>
        /// <param name="cubeName"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public Stream ExportCubeToWorkbook( string cubeName, EssJobExportExcelOptions options = null );

        /// <summary>
        /// Asynchronously Exports a cube to an excel workbook
        /// </summary>
        /// <param name="cubeName"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Stream> ExportCubeToWorkbookAsync( string cubeName, EssJobExportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appConnectionName"></param>
        /// <returns></returns>
        public IEssApplicationDatasourceConnection GetConnection( string appConnectionName );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appConnectionName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEssApplicationDatasourceConnection> GetConnectionAsync( string appConnectionName, CancellationToken cancellationToken = default );

        /// <inheritdoc />
        /// <returns></returns>
        public List<IEssApplicationDatasourceConnection> GetConnections();

        /// <inheritdoc />
        /// <returns></returns>
        public Task<List<IEssApplicationDatasourceConnection>> GetConnectionsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the cube with the given name.
        /// </summary>
        /// <param name="cubeName" />
        public IEssCube GetCube( string cubeName );

        /// <summary>
        /// Asynchronously gets the cube with the given name.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="cancellationToken" />
        public Task<IEssCube> GetCubeAsync( string cubeName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of cubes for this application available to the currently connected user.
        /// </summary>
        public List<IEssCube> GetCubes();

        /// <summary>
        /// Asynchronously gets the list of cubes for this application available to the currently connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssCube>> GetCubesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of configurations for this application
        /// </summary>
        public List<IEssApplicationConfiguration> GetConfigurations();

        /// <summary>
        /// returns a list of configurations for this application
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssApplicationConfiguration>> GetConfigurationsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the list of application-scoped variables available to the connected user.
        /// </summary>
        /// <returns>A list of <see cref="IEssServerVariable"/> objects.</returns>
        public List<IEssApplicationVariable> GetVariables();

        /// <summary>
        /// Asynchronously gets the list of application-scoped variables available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssServerVariable"/> objects.</returns>
        public Task<List<IEssApplicationVariable>> GetVariablesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Copy The Application.
        /// </summary>
        public void Copy( string copyName );

        /// <summary>
        /// Copy the application.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <param name="copyName" />
        public Task CopyAsync( string copyName, CancellationToken cancellationToken = default );


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

        #region Properties

        /// <summary>
        /// Returns the date and time the application was created.
        /// </summary>
        public DateTime CreatedDate { get; }

        /// <summary>
        /// Returns the date and time the application was last modified.
        /// </summary>
        public DateTime ModifiedDate { get; }

        /// <summary>
        /// Returns the status of the application.
        /// </summary>
        public EssApplicationStatus Status { get; }

        #endregion
    }

    /// <summary>
    /// Fluent extensions for <see cref="IEssApplication"/>.
    /// </summary>
    public static class IEssApplicationExtensions
    {
        /// <summary>
        /// Asynchronously gets the cube with the given name.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="cancellationToken" />
        public static async Task<IEssCube> GetCubeAsync( this Task<IEssApplication> applicationTask, string cubeName, CancellationToken cancellationToken = default ) =>
            await (await applicationTask.ConfigureAwait(false)).GetCubeAsync(cubeName, cancellationToken).ConfigureAwait(false);
    }
}
