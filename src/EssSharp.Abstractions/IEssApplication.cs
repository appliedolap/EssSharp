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
        /// <param name="cubeName" />
        /// <param name="options">Options for creating a cube.</param>
        public IEssCube CreateCube( string cubeName, EssDatabaseCreationOptions options = null );

        /// <summary>
        /// Create a new cube on an Application
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="options">Options for creating a cube</param>
        /// <param name="cancellationToken" />
        public Task<IEssCube> CreateCubeAsync( string cubeName, EssDatabaseCreationOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new cube (and application) from an existing server file.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="options">Options for creating a cube from a workbook.</param>
        public IEssCube CreateCubeFromWorkbook( string cubeName, EssJobImportExcelOptions options );

        /// <summary>
        /// Asynchronously creates a new cube (and application) from a server file.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="options">Options for creating a cube from a workbook.</param>
        /// <param name="cancellationToken" />
        public Task<IEssCube> CreateCubeFromWorkbookAsync( string cubeName, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new cube (and application) from a local workbook file path.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="localWorkbookPath">Local path to workbook.</param>
        /// <param name="options">Options for creating a cube from a workbook.</param>
        public IEssCube CreateCubeFromWorkbook( string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null );

        /// <summary>
        /// Asynchronously creates a new cube (and application) from a local workbook file path.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="localWorkbookPath">Local path to workbook.</param>
        /// <param name="options">Options for creating a cube from a workbook.</param>
        /// <param name="cancellationToken" />
        public Task<IEssCube> CreateCubeFromWorkbookAsync( string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new cube (and application) from a workbook stream.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="stream">Workbook stream.</param>
        /// <param name="options">Options for creating a cube from a workbook.</param>
        public IEssCube CreateCubeFromWorkbook( string cubeName, Stream stream, EssJobImportExcelOptions options = null );

        /// <summary>
        /// Asynchronously creates a new cube (and application) from a workbook stream.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="stream">Workbook stream.</param>
        /// <param name="options">Options for creating a cube from a workbook.</param>
        /// <param name="cancellationToken" />
        public Task<IEssCube> CreateCubeFromWorkbookAsync( string cubeName, Stream stream, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Create an application variable.
        /// </summary>
        /// <param name="varName" />
        /// <param name="value" />
        public IEssApplicationVariable CreateApplicationVariable( string varName, string value );

        /// <summary>
        /// Asynchronously create an application variable.
        /// </summary>
        /// <param name="varName" />
        /// <param name="value" />
        /// <param name="cancellationToken" />
        public Task<IEssApplicationVariable> CreateApplicationVariableAsync( string varName, string value, CancellationToken cancellationToken = default );

        /// <summary>
        /// Add user permissions to an application.
        /// </summary>
        /// <param name="id"> Group or User ID.</param>
        /// <param name="applicationRole" />
        /// <returns></returns>
        public IEssApplicationPermission CreatePermissions( string id, EssApplicationRole applicationRole );

        /// <summary>
        /// Asynchronously add user permissions to an application. 
        /// </summary>
        /// <param name="id">Group or User ID.</param>
        /// <param name="applicationRole">User permission level.</param>
        /// <param name="isGroup">If creating a group permission, true.</param>
        /// <param name="cancellationToken"></param>
        public Task<IEssApplicationPermission> CreatePermissionsAsync( string id, EssApplicationRole applicationRole, bool isGroup = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Downloads the latest log file for this application.
        /// </summary>
        public Stream DownloadLatestLogFile();

        /// <summary>
        /// Asynchronously downloads the latest log file for this application.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<Stream> DownloadLatestLogFileAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Downloads the latest log file for this application.
        /// </summary>
        public string DownloadLatestLogFileString();

        /// <summary>
        /// Asynchronously downloads the latest log file for this application.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<string> DownloadLatestLogFileStringAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Exports a cube to an excel workbook.
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="options">Options for exporting excel to workbook.</param>
        public Stream ExportCubeToWorkbook( string cubeName, EssJobExportExcelOptions options = null );

        /// <summary>
        /// Asynchronously Exports a cube to an excel workbook
        /// </summary>
        /// <param name="cubeName" />
        /// <param name="options">Options for exporting excel to workbook.</param>
        /// <param name="cancellationToken" />
        public Task<Stream> ExportCubeToWorkbookAsync( string cubeName, EssJobExportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the specified Application datasource connection.
        /// </summary>
        /// <param name="appConnectionName" />
        public IEssApplicationDatasourceConnection GetConnection( string appConnectionName );

        /// <summary>
        /// Asynchronously gets the specified Application datasource connection.
        /// </summary>
        /// <param name="appConnectionName" />
        /// <param name="cancellationToken" />
        public Task<IEssApplicationDatasourceConnection> GetConnectionAsync( string appConnectionName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a list of application datasource connections.
        /// </summary>
        public List<IEssApplicationDatasourceConnection> GetConnections();

        /// <summary>
        /// Asynchronously gets a list of application datasource connections.
        /// </summary>
        /// <param name="cancellationToken" />
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
        /// Asynchronously returns a list of configurations for this application
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssApplicationConfiguration>> GetConfigurationsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get list of user permissions on an application.
        /// </summary>
        /// <param name="filter">Filter by user or group ID.</param>
        /// <param name="roles">Filter by role.</param>
        /// <param name="includeInheritence" />
        public List<IEssApplicationPermission> GetPermissions( EssPermissionType filter = EssPermissionType.All, EssApplicationRole[] roles = null, bool includeInheritence = true );

        /// <summary>
        /// Asynchronously get list of user permissions on application.
        /// </summary>
        /// <param name="filter">Filter by user or group ID.</param>
        /// <param name="roles">Filter by role.</param>
        /// <param name="includeInheritence" />
        /// <param name="cancellationToken" />
        public Task<List<IEssApplicationPermission>> GetPermissionsAsync( EssPermissionType filter = EssPermissionType.All, EssApplicationRole[] roles = null, bool includeInheritence = true, CancellationToken cancellationToken = default );

        /// <summary>
        /// Get permissions for a specified user.
        /// </summary>
        /// <param name="id" />
        /// <param name="isGroup">If creating a group permission, true.</param>
        public IEssApplicationPermission GetPermission( string id, bool isGroup = false );

        /// <summary>
        /// Asynchronously get Permissions for a specified user.
        /// </summary>
        /// <param name="id" />
        /// <param name="isGroup">If creating a group permission, true.</param>
        /// <param name="cancellationToken" />
        public Task<IEssApplicationPermission> GetPermissionAsync( string id, bool group = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Updates a specified users permissions on an application.
        /// </summary>
        /// <param name="userId" />
        /// <param name="newApplicationRole" />
        /// <param name="isGroup">If creating a group permission, true.</param>
        public IEssApplicationPermission UpdatePermissions( string userId, EssApplicationRole newApplicationRole, bool isGroup = false );

        /// <summary>
        /// Asynchronously updates a specified users permissions on an application.
        /// </summary>
        /// <param name="userId" />
        /// <param name="newApplicationRole" />
        /// <param name="isGroup">If creating a group permission, true.</param>
        /// <param name="cancellationToken" />
        public Task<IEssApplicationPermission> UpdatePermissionsAsync( string userId, EssApplicationRole newApplicationRole, bool group = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Updates a specified users permissions on an application.
        /// </summary>
        /// <param name="essPermission" />
        /// <param name="newApplicationRole" />
        /// <param name="isGroup">If creating a group permission, true.</param>
        public IEssApplicationPermission UpdatePermissions( IEssApplicationPermission essPermission, EssApplicationRole newApplicationRole, bool isGroup = false );

        /// <summary>
        /// Asynchronously updates a specified users permissions on an application.
        /// </summary>
        /// <param name="essPermission" />
        /// <param name="newApplicationRole" />
        /// <param name="isGroup">If creating a group permission, true.</param>
        /// <param name="cancellationToken" />
        public Task<IEssApplicationPermission> UpdatePermissionsAsync( IEssApplicationPermission essPermission, EssApplicationRole newApplicationRole, bool isGroup = false, CancellationToken cancellationToken = default );
        
        /// <summary>
        /// Gets the list of application-scoped variables available to the connected user.
        /// </summary>
        public List<IEssApplicationVariable> GetVariables();

        /// <summary>
        /// Asynchronously gets the list of application-scoped variables available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
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
