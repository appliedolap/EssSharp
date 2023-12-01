using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.IO;

namespace EssSharp
{
    /// <summary />
    public interface IEssServer : IEssObject, IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Creates a new Application and database with the given name and options
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="options">Options for creating a Database.</param>
        public IEssApplication CreateApplication( string applicationName, string cubeName, EssDatabaseCreationOptions options = null );

        /// <summary>
        /// Asynchronously creates a new Application and database with the given name and options
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="options">Options for creating a Database.</param>
        /// <param name="cancellationToken" />
        public Task<IEssApplication> CreateApplicationAsync( string applicationName, string cubeName, EssDatabaseCreationOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new application (and cube) from an existing server file.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="options">Job options for creating an application from a workbook.</param>
        public IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, EssJobImportExcelOptions options );

        /// <summary>
        /// Asynchronously creates a new application (and cube) from an existing server file.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="options">Job options for creating an application from a workbook.</param>
        /// <param name="cancellationToken" />
        public Task<IEssApplication> CreateApplicationFromWorkbookAsync( string applicationName, string cubeName, EssJobImportExcelOptions options, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new application (and cube) from a local workbook file path.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="localWorkbookPath" />
        /// <param name="options">Job options for creating an application from a workbook.</param>
        public IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null );

        /// <summary>
        /// Asynchronously creates a new application (and cube) from a local workbook file path.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="localWorkbookPath" />
        /// <param name="options">Job options for creating an application from a workbook.</param>
        /// <param name="cancellationToken" />
        public Task<IEssApplication> CreateApplicationFromWorkbookAsync( string applicationName, string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new application (and cube) from a workbook stream.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="stream" />
        /// <param name="options">Job options for creating an application from a workbook.</param>
        public IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, Stream stream, EssJobImportExcelOptions options = null );

        /// <summary>
        /// Asynchronously creates a new application (and cube) from a workbook stream.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        /// <param name="stream">Workbook stream</param>
        /// <param name="options">Job options for creating an application from a workbook.</param>
        /// <param name="cancellationToken" />
        public Task<IEssApplication> CreateApplicationFromWorkbookAsync( string applicationName, string cubeName, Stream stream, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Create a new group.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public IEssGroup CreateGroup( string name, EssServerRole role = EssServerRole.User, string description = null );

        /// <summary>
        /// Asynchronously create a new group.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGroup> CreateGroupAsync( string name, EssServerRole role = EssServerRole.User, string description = null, CancellationToken cancellationToken = default );
        
        /// <summary>
        /// Creates a new (unstarted) job on the server.
        /// </summary>
        /// <param name="options">Job options.</param>
        public IEssJob CreateJob( IEssJobOptions options );

        /// <summary>
        /// Creates a new user on the server.
        /// </summary>
        /// <param name="options">Options for creating a new user.</param>
        public IEssUser CreateUser( EssUserCreationOptions options );

        /// <summary>
        /// Asynchronously creates a new user on the server.
        /// </summary>
        /// <param name="options">Options for creating a new user.</param>
        /// <param name="cancellationToken" />
        /// <returns></returns>
        public Task<IEssUser> CreateUserAsync( EssUserCreationOptions options, CancellationToken cancellationToken = default );

        /// <summary>
        /// Creates a new server variable.
        /// </summary>
        /// <param name="name" />
        /// <param name="value" />
        public IEssServerVariable CreateVariable( string name, string value );

        /// <summary>
        /// Asynchronously create a new server variable.
        /// </summary>
        /// <param name="name" />
        /// <param name="value" />
        /// <param name="cancellationToken"></param>
        public Task<IEssServerVariable> CreateVariableAsync( string name, string value, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a specified datasource connection as an <see cref="IEssDatasourceConnection"/>.
        /// </summary>
        /// <param name="connectionName" />
        public IEssDatasourceConnection GetConnection( string connectionName );

        /// <summary>
        /// Asynchronously returns a specified datasource connection as an <see cref="IEssDatasourceConnection"/>.
        /// </summary>
        /// <param name="connectionName" />
        /// <param name="cancellationToken" />
        public Task<IEssDatasourceConnection> GetConnectionAsync( string connectionName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of datasource connections as <see cref="IEssDatasourceConnection"/> objects.
        /// </summary>
        public List<IEssDatasourceConnection> GetConnections();

        /// <summary>
        /// Asynchronously returns a list of datasource connections as <see cref="IEssDatasourceConnection"/> objects
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssDatasourceConnection>> GetConnectionsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Return a specified data source as an <see cref="IEssDatasource"/> object.
        /// </summary>
        /// <param name="datasourceName" />
        public IEssDatasource GetDatasource( string datasourceName );

        /// <summary>
        /// Asynchronously returns a specified data source as an <see cref="IEssDatasource"/> object.
        /// </summary>
        /// <param name="datasourceName" />
        /// <param name="cancellationToken" />
        public Task<IEssDatasource> GetDatasourceAsync( string datasourceName, CancellationToken cancellationToken = default );

        /// <summary>
        /// returns a list of <see cref="IEssDatasource"/> objects.
        /// </summary>
        public List<IEssDatasource> GetDatasources();

        /// <summary>
        /// Asynchronously returns a list of <see cref="IEssDatasource"/> objects.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssDatasource>> GetDatasourcesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a specified server file as an <see cref="IEssFile"/> object.
        /// </summary>
        /// <param name="path"></param>
        public IEssFile GetFile(string path);

        /// <summary>
        /// Asynchronously returns a specified server file as an <see cref="IEssFile"/> object.
        /// </summary>
        /// <param name="path" />
        /// <param name="cancellationToken" />
        public Task<IEssFile> GetFileAsync(string path, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a server folder as an <see cref="IEssFolder"/> object.
        /// </summary>
        /// <param name="path" />
        public IEssFolder GetFolder( string path );

        /// <summary>
        /// Asynchronously returns a server folder as an <see cref="IEssFolder"/> object.
        /// </summary>
        /// <param name="path" />
        /// <param name="cancellationToken" />
        public Task<IEssFolder> GetFolderAsync( string path, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of <see cref="IEssFile"/> objects for this server available to the connected user.
        /// </summary>
        public List<IEssFolder> GetFolders();

        /// <summary>
        /// Asynchronously gets the list of <see cref="IEssFile"/> objects for this server available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssFolder>> GetFoldersAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a specified group as an <see cref="IEssGroup"/>.
        /// </summary>
        /// <param name="name" />
        public IEssGroup GetGroup( string name );

        /// <summary>
        /// Asynchronously Returns a specified group as an <see cref="IEssGroup"/>.
        /// </summary>
        /// <param name="name" />
        /// <param name="cancellationToken" />
        public Task<IEssGroup> GetGroupAsync( string name, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of <see cref="IEssGroup"/> objects.
        /// </summary>
        public List<IEssGroup> GetGroups();

        /// <summary>
        /// Asynchronously returns a list of <see cref="IEssGroup"/> objects.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssGroup>> GetGroupsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a about information of this server as an <see cref="IEssAbout"/>.
        /// </summary>
        public IEssAbout GetAbout();

        /// <summary>
        /// Asynchronously returns the about information of this server as an <see cref="IEssAbout"/>.
        /// </summary>
        public Task<IEssAbout> GetAboutAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a about information of this server as an <see cref="IEssAbout"/>.
        /// </summary>
        public IEssAboutInstance GetAboutInstance();

        /// <summary>
        /// Asynchronously returns a about information of this server as an <see cref="IEssAbout"/>.
        /// </summary>
        public Task<IEssAboutInstance> GetAboutInstanceAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns an application with the given name as an <see cref="IEssApplication"/>.
        /// </summary>
        /// <param name="applicationName" />
        public IEssApplication GetApplication( string applicationName );

        /// <summary>
        /// Asynchronously Returns an application with the given name as an <see cref="IEssApplication"/>.
        /// </summary>
        /// <param name="applicationName" />
        /// <param name="cancellationToken" />
        public Task<IEssApplication> GetApplicationAsync( string applicationName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of <see cref="IEssApplication"/> objects for this server available to the connected user.
        /// </summary>
        public List<IEssApplication> GetApplications();

        /// <summary>
        /// Returns a list of <see cref="IEssApplication"/> objects for this server available to the connected user.
        /// </summary>
        /// <param name="applicationsLimit">The maximum number of applications to return.</param>
        public List<IEssApplication> GetApplications( int applicationsLimit );

        /// <summary>
        /// Asynchronously returns a list of <see cref="IEssApplication"/> objects for this server available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssApplication>> GetApplicationsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously Returns a list of <see cref="IEssApplication"/> objects for this server available to the connected user.
        /// </summary>
        /// <param name="applicationsLimit">The maximum number of applications to return.</param>
        /// <param name="cancellationToken" />
        public Task<List<IEssApplication>> GetApplicationsAsync( int applicationsLimit, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of jobs for this server available to the connected user.
        /// </summary>
        public List<IEssJob> GetJobs();

        /// <summary>
        /// Returns a list of <see cref="IEssJob"/> objects for this server available to the connected user.
        /// </summary>
        /// <param name="jobsLimit">The maximum number of jobs to return.</param>
        public List<IEssJob> GetJobs( long jobsLimit );

        /// <summary>
        /// Asynchronously returns a list of <see cref="IEssJob"/> objects for this server available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssJob>> GetJobsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Asynchronously returns a list of <see cref="IEssJob"/> objects for this server available to the connected user.
        /// </summary>
        /// <param name="jobsLimit">The maximum number of jobs to return.</param>
        /// <param name="cancellationToken" />
        public Task<List<IEssJob>> GetJobsAsync( long jobsLimit, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the user with the given ID as an <see cref="IEssUser"/>.
        /// </summary>
        /// <param name="id" />
        public IEssUser GetUser( string id );

        /// <summary>
        /// Asynchronously returns the user with the given ID as an <see cref="IEssUser"/>.
        /// </summary>
        /// <param name="id" />
        /// <param name="cancellationToken" />
        public Task<IEssUser> GetUserAsync( string id, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the home folder for the current user as a list of <see cref="IEssFolder"/> objects.
        /// </summary>
        public IEssFolder GetUserHomeFolder();

        /// <summary>
        /// Asynchronously returns the home folder for the current user as a list of <see cref="IEssFolder"/> objects..
        /// </summary>
        public Task<IEssFolder> GetUserHomeFolderAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a user session from the server for the configured user as an <see cref="IEssUserSession"/>.
        /// </summary>
        /// <param name="includeToken">Whether to capture a session token.</param>
        /// <param name="includeGroups">Whether to capture the user's groups.</param>
        public IEssUserSession GetUserSession( bool includeToken = true, bool includeGroups = true );

        /// <summary>
        /// Asynchronously gets a user session from the server for the configured user as an <see cref="IEssUserSession"/>.
        /// </summary>
        /// <param name="includeToken">Whether to capture a session token.</param>
        /// <param name="includeGroups">Whether to capture the user's groups.</param>
        /// <param name="cancellationToken" />
        public Task<IEssUserSession> GetUserSessionAsync( bool includeToken = true, bool includeGroups = true, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the shared folder for the current user as an <see cref="IEssFolder"/>.
        /// </summary>
        public IEssFolder GetUserSharedFolder();

        /// <summary>
        /// Asynchronously returns the shared folder for the current user as an <see cref="IEssFolder"/>.
        /// </summary>
        public Task<IEssFolder> GetUserSharedFolderAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of all users for this server as <see cref="IEssUser"/> objects.
        /// </summary>
        public List<IEssUser> GetUsers();

        /// <summary>
        /// Asynchronously returns a list of all users for this server as <see cref="IEssUser"/> objects.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssUser>> GetUsersAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of server-scoped variables available to the connected user as <see cref="IEssServerVariable"/> objects.
        /// </summary>
        public List<IEssServerVariable> GetVariables();

        /// <summary>
        /// Asynchronously gets the list of server-scoped variables available to the connected user as <see cref="IEssServerVariable"/> objects..
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssServerVariable>> GetVariablesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of server sessions available to the connected user as <see cref="IEssSession"/> objects.
        /// </summary>
        public List<IEssSession> GetSessions();

        /// <summary>
        /// Asynchronously returns the list of server sessions available to the connected user as <see cref="IEssSession"/> objects.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<List<IEssSession>> GetSessionsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a list of server URLs available to the connected user as <see cref="IEssUrl"/>.
        /// </summary>
        public List<IEssUrl> GetURLs();

        /// <summary>
        /// Asynchronously returns a list of server URLs available to the connected user as <see cref="IEssUrl"/> objects.
        /// </summary>
        public Task<List<IEssUrl>> GetURLsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns a list of server utilities available to the connected user as <see cref="IEssUtility"/> objects.
        /// </summary>
        public List<IEssUtility> GetUtilities();

        /// <summary>
        /// Asynchronously returns a list of server utilities available to the connected user as <see cref="IEssUtility"/> objects..
        /// </summary>
        public Task<List<IEssUtility>> GetUtilitiesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Signs into the server and returns a user session for the connected user as an <see cref="IEssUserSession"/> object.
        /// </summary>
        /// <param name="includeGroups" />
        /// <param name="includeToken" />
        public IEssUserSession SignIn( bool includeToken = false, bool includeGroups = false );

        /// <summary>
        /// Asynchronously signs into the server and returns a user session for the connected user as an <see cref="IEssUserSession"/> object.
        /// </summary>
        /// <param name="includeGroups" />
         /// <param name="includeToken" />
        /// <param name="cancellationToken" />
        public Task<IEssUserSession> SignInAsync( bool includeToken = false, bool includeGroups = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Signs out of the server.
        /// </summary>
        /// <param name="allSessions">Whether to sign out all sessions associated with the consuming process.</param>
        public void SignOut( bool allSessions = false );

        /// <summary>
        /// Asynchronously signs out of the server.
        /// </summary>
        /// <param name="allSessions">Whether to sign out all sessions associated with the consuming process.</param>
        /// <param name="cancellationToken" />
        public Task SignOutAsync( bool allSessions = false, CancellationToken cancellationToken = default );
    }
}
