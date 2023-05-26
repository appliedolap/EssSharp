using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.IO;

namespace EssSharp
{
    /// <summary />
    public interface IEssServer : IEssObject
    {
        /// <summary>
        /// Creates a new Application and database with the given name and options
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="databaseName"></param>
        /// <param name="appType"></param>
        /// <param name="dbType"></param>
        /// <param name="enableScenarios"></param>
        /// <param name="allowDuplicates"></param>
        public IEssApplication CreateApplication( string applicationName, string cubeName, EssDatabaseCreationOptions options = null );

        /// <summary>
        /// Creates a new Application and database with the given name and options
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="databaseName"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEssApplication> CreateApplicationAsync( string applicationName, string cubeName, EssDatabaseCreationOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new application (and cube) from an existing server file.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="options" />
        public IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, EssJobImportExcelOptions options );

        /// <summary>
        /// Asynchronously creates a new application (and cube) from an existing server file.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public Task<IEssApplication> CreateApplicationFromWorkbookAsync( string applicationName, string cubeName, EssJobImportExcelOptions options, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new application (and cube) from a local workbook file path.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="localWorkbookPath" />
        /// <param name="options" />
        public IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null );

        /// <summary>
        /// Asynchronously creates a new application (and cube) from a local workbook file path.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="localWorkbookPath" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public Task<IEssApplication> CreateApplicationFromWorkbookAsync( string applicationName, string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new application (and cube) from a workbook stream.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="stream" />
        /// <param name="options" />
        public IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, Stream stream, EssJobImportExcelOptions options = null );

        /// <summary>
        /// Asynchronously creates a new application (and cube) from a workbook stream.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="stream" />
        /// <param name="options" />
        /// <param name="cancellationToken" />
        public Task<IEssApplication> CreateApplicationFromWorkbookAsync( string applicationName, string cubeName, Stream stream, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new (unstarted) job on the server.
        /// </summary>
        /// <param name="options" />
        public IEssJob CreateJob( IEssJobOptions options );

        /// <summary>
        /// Creates a new server variable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public IEssServerVariable CreateVariable( string name, string value );

        /// <summary>
        /// Create a new server variable
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssServerVariable> CreateVariableAsync( string name, string value, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public IEssDatasourceConnection GetConnection( string connectionName );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEssDatasourceConnection> GetConnectionAsync( string connectionName, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IEssDatasourceConnection> GetConnections();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<IEssDatasourceConnection>> GetConnectionsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Return a specified data source as an IEssDatasource object
        /// </summary>
        /// <param name="datasourceName"></param>
        public IEssDatasource GetDatasource(string dataSourceName);

        /// <summary>
        /// Returns a specified data source as an IEssDatasource object
        /// </summary>
        /// <param name="datasourceName"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssDatasource> GetDatasourceAsync(string datasourceName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of IEssDatasource objects
        /// </summary>
        public List<IEssDatasource> GetDatasources();

        /// <summary>
        /// Returns a list of IEssDatasource Ojbects
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssDatasource>> GetDatasourcesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEssFile GetFile(string path);

        /// <summary />
        /// <param name="path" />
        /// <param name="cancellationToken" />
        public Task<IEssFile> GetFileAsync(string path, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IEssFolder GetFolder( string path );

        /// <summary />
        /// <param name="path" />
        /// <param name="cancellationToken" />
        public Task<IEssFolder> GetFolderAsync( string path, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of files for this server available to the connected user.
        /// </summary>
        public List<IEssFolder> GetFolders();

        /// <summary>
        /// Asynchronously gets the list of files for this server available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssFolder>> GetFoldersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a specified group
        /// </summary>
        /// <param name="name"></param>
        public IEssGroup GetGroup( string name );

        /// <summary>
        /// Returns a speicified
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEssGroup> GetGroupAsync( string name, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a litst of <see cref="IEssGroup"/> objects
        /// </summary>
        public List<IEssGroup> GetGroups();

        /// <summary>
        /// Returns a litst of <see cref="IEssGroup"/> objects
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssGroup>> GetGroupsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the about information of this server
        /// </summary>
        public Task<IEssAbout> GetAboutAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the about information of this server
        /// </summary>
        public Task<IEssAboutInstance> GetAboutInstanceAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the application with the given name.
        /// </summary>
        /// <param name="applicationName" />
        public IEssApplication GetApplication( string applicationName );

        /// <summary>
        /// Asynchronously gets the application with the given name.
        /// </summary>
        /// <param name="applicationName" />
        public Task<IEssApplication> GetApplicationAsync( string applicationName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of applications for this server available to the connected user.
        /// </summary>
        public List<IEssApplication> GetApplications();

        /// <summary>
        /// Gets the list of applications for this server available to the connected user.
        /// </summary>
        /// <param name="applicationsLimit">The maximum number of applications to return.</param>
        public List<IEssApplication> GetApplications( int applicationsLimit );

        /// <summary>
        /// Asynchronously gets the list of applications for this server available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssApplication>> GetApplicationsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously gets the list of applications for this server available to the connected user.
        /// </summary>
        /// <param name="applicationsLimit">The maximum number of applications to return.</param>
        /// <param name="cancellationToken" />
        public Task<List<IEssApplication>> GetApplicationsAsync( int applicationsLimit, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of jobs for this server available to the connected user.
        /// </summary>
        public List<IEssJob> GetJobs();

        /// <summary>
        /// Gets the list of jobs for this server available to the connected user.
        /// </summary>
        /// <param name="jobsLimit">The maximum number of jobs to return.</param>
        public List<IEssJob> GetJobs( long jobsLimit );

        /// <summary>
        /// Asynchronously gets the list of jobs for this server available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssJob>> GetJobsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously gets the list of jobs for this server available to the connected user.
        /// </summary>
        /// <param name="jobsLimit">The maximum number of jobs to return.</param>
        /// <param name="cancellationToken" />
        public Task<List<IEssJob>> GetJobsAsync( long jobsLimit, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the user with the given ID.
        /// </summary>
        /// <param name="userId" />
        /// <param name="cancellationToken" />
        public IEssUser GetUser( string userId );

        /// <summary>
        /// Asynchronously gets the user with the given ID.
        /// </summary>
        /// <param name="userId" />
        /// <param name="cancellationToken" />
        public Task<IEssUser> GetUserAsync( string userId, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the home folder for the current user.
        /// </summary>
        public IEssFolder GetUserHomeFolder();

        /// <summary>
        /// Asynchronously gets the home folder for the current user.
        /// </summary>
        public Task<IEssFolder> GetUserHomeFolderAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a user session from the server for the configured user.
        /// </summary>
        /// <param name="includeToken">Whether to capture a session token.</param>
        /// <param name="includeGroups">Whether to capture the user's groups.</param>
        /// <param name="cancellationToken" />
        public IEssUserSession GetUserSession( bool includeToken = true, bool includeGroups = true );

        /// <summary>
        /// Asynchronously gets a user session from the server for the configured user.
        /// </summary>
        /// <param name="includeToken">Whether to capture a session token.</param>
        /// <param name="includeGroups">Whether to capture the user's groups.</param>
        /// <param name="cancellationToken" />
        public Task<IEssUserSession> GetUserSessionAsync( bool includeToken = true, bool includeGroups = true, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the shared folder for the current user.
        /// </summary>
        public IEssFolder GetUserSharedFolder();

        /// <summary>
        /// Asynchronously gets the shared folder for the current user.
        /// </summary>
        public Task<IEssFolder> GetUserSharedFolderAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of all users for this server.
        /// </summary>
        /// <returns></returns>
        public List<IEssUser> GetUsers();

        /// <summary>
        /// Gets the list of all users for this server.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssUser>> GetUsersAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of server-scoped variables available to the connected user.
        /// </summary>
        public List<IEssServerVariable> GetVariables();

        /// <summary>
        /// Asynchronously gets the list of server-scoped variables available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssServerVariable>> GetVariablesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of server sessions available to the connected user.
        /// </summary>
        public List<IEssSession> GetSessions();

        /// <summary>
        /// Asynchronously gets the list of server sessions available to the connected user.
        /// </summary>
        public Task<List<IEssSession>> GetSessionsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the list of server URLs available to the connected user.
        /// </summary>
        public List<IEssUrl> GetURLs();

        /// <summary>
        /// Asynchronously gets the list of server URLs available to the connected user.
        /// </summary>
        public Task<List<IEssUrl>> GetURLsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of server utilities available to the connected user.
        /// </summary>
        public List<IEssUtility> GetUtilities();

        /// <summary>
        /// Asynchronously gets the list of server utilities available to the connected user.
        /// </summary>
        public Task<List<IEssUtility>> GetUtilitiesAsync(CancellationToken cancellationToken = default);
    }
}
