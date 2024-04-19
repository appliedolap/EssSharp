using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssServer : EssObject, IEssServer
    {
        #region Private Data

        private const string _defaultRestApiPath = "/rest/v1";
        private const int    _maxApplications    = 100;
        private const long   _maxJobs            = 100;

        private readonly string _server;

        private bool _disposed;

        #endregion

        #region Constructors

        /// <summary />
        internal EssServer( Configuration configuration, ApiClient client ) : base(configuration, client) 
        {
            if ( !Uri.TryCreate(configuration?.BasePath, UriKind.Absolute, out _) )
                throw new ArgumentException("A fully qualified server REST endpoint must be set on the configuration.", nameof(configuration));

            int defaultRestApiPathIndex = configuration.BasePath.ToLowerInvariant().LastIndexOf(_defaultRestApiPath);

            if ( defaultRestApiPathIndex >= 0 )
                _server = configuration.BasePath.Substring(0, defaultRestApiPathIndex);
        }

        /// <summary />
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public EssServer( string server, string username, string password )
        {
            _server = server?.TrimEnd('/') ?? string.Empty;

            var basePath = _server;

            // If necessary, append the default REST API path if necessary.
            if ( !server.EndsWith(_defaultRestApiPath, StringComparison.OrdinalIgnoreCase) )
                basePath = $@"{_server}{_defaultRestApiPath}";

            if ( !Uri.TryCreate(basePath, UriKind.Absolute, out _) )
                throw new ArgumentException("A fully qualified server URL is required.", nameof(server));

            if ( string.IsNullOrEmpty(username) )
                throw new ArgumentException("A username is required.", nameof(username));

            Client        = new ApiClient(basePath);
            Configuration = new Configuration()
            {
                BasePath  = basePath,
                Username  = username,
                Password  = password,
                Timeout   = int.MaxValue,
                UserAgent = $"{nameof(EssSharp)}/{typeof(EssServer).Assembly.GetName().Version}",
                //Proxy = new WebProxy("localhost", 8070)
            };
        }

        #endregion

        #region Private Properties

        /// <summary />
        private EssGridPreferences DefaultGridPreferences { get; set; } = null;

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _server;

        /// <inheritdoc />
        public override EssType Type => EssType.Server;

        #endregion

        #region IEssServer Members

        /// <inheritdoc />
        public ILogger Logger
        {
            get => Configuration?.Logger;
            set
            {
                if ( Configuration is { } )
                    Configuration.Logger = value;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public IEssApplication CreateApplication( string applicationName, string cubeName, EssDatabaseCreationOptions options = null ) => CreateApplicationAsync(applicationName, cubeName, options).GetAwaiter().GetResult();
        
        /// <inheritdoc />
        /// <returns>An<see cref="IEssApplication"/> object.</returns>
        public async Task<IEssApplication> CreateApplicationAsync(string applicationName, string cubeName, EssDatabaseCreationOptions options = null, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(applicationName) )
                throw new ArgumentException($"An application name is required to create an {nameof(EssApplication)}.", nameof(applicationName));

            if ( string.IsNullOrWhiteSpace(cubeName) )
                throw new ArgumentException($"An cube name is required to create an {nameof(EssCube)}.", nameof(cubeName));

            try
            {
                options ??= new EssDatabaseCreationOptions();
                var createApp = new CreateApplication(applicationName: applicationName, databaseName: cubeName, allowDuplicates: options.AllowDuplicates, enableScenario: options.EnableScenarios, databaseType: options.DatabaseType.ToString());
                
                var api = GetApi<ApplicationsApi>();
                await api.ApplicationsCreateApplicationsAsync(body: createApp, cancellationToken: cancellationToken).ConfigureAwait(false);

                // Return the newly created application.
                return await GetApplicationAsync(applicationName).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create the application ""{applicationName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public IEssApplication CreateApplicationFromLcm( string applicationName, string cubeName, EssJobImportLcmOptions options ) =>
            CreateApplicationFromLcmAsync(applicationName, cubeName, options, CancellationToken.None).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public Task<IEssApplication> CreateApplicationFromLcmAsync( string applicationName, string cubeName, EssJobImportLcmOptions options, CancellationToken cancellationToken = default ) =>
            CreateApplicationFromLcmAsync(applicationName, cubeName, options, null, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns> 
        public IEssApplication CreateApplicationFromLcm( string applicationName, string cubeName, string localLcmPath, EssJobImportLcmOptions options = null ) =>
            CreateApplicationFromLcmAsync(applicationName, cubeName, localLcmPath, options).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public async Task<IEssApplication> CreateApplicationFromLcmAsync( string applicationName, string cubeName, string localLcmPath, EssJobImportLcmOptions options = null, CancellationToken cancellationToken = default )
        {
            try
            {
                if ( !File.Exists(localLcmPath) )
                    throw new FileNotFoundException("Unable to find the workbook file at the given local path.", localLcmPath);

                using var stream = new FileStream(localLcmPath, FileMode.Open, FileAccess.Read);
                return await CreateApplicationFromLcmAsync(applicationName, cubeName, options, stream).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create the application ""{applicationName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public IEssApplication CreateApplicationFromLcm( string applicationName, string cubeName, Stream stream, EssJobImportLcmOptions options = null ) =>
            CreateApplicationFromLcmAsync(applicationName, cubeName, options, stream).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public Task<IEssApplication> CreateApplicationFromLcmAsync( string applicationName, string cubeName, Stream stream, EssJobImportLcmOptions options = null, CancellationToken cancellationToken = default ) =>
            CreateApplicationFromLcmAsync(applicationName, cubeName, options, stream, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        internal IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, EssJobImportLcmOptions options = null, Stream stream = null ) =>
            CreateApplicationFromLcmAsync(applicationName, cubeName, options, stream).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        internal async Task<IEssApplication> CreateApplicationFromLcmAsync( string applicationName, string cubeName, EssJobImportLcmOptions options = null, Stream stream = null, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(applicationName) )
                throw new ArgumentException($"An application name is required to create an an {nameof(EssApplication)} from a workbook.", nameof(applicationName));

            if ( string.IsNullOrWhiteSpace(cubeName) )
                throw new ArgumentException($"A cube name is required to create an an {nameof(EssApplication)} from a workbook.", nameof(cubeName));

            if ( stream is null && string.IsNullOrEmpty(options?.ZipFileName) )
                throw new ArgumentException($"A local path, stream, or server file is required to create an {nameof(EssApplication)} from a workbook.");

            try
            {
                // Construct new options if none were given.
                options ??= new EssJobImportLcmOptions();

                if ( options.ZipFileName is null )
                {
                    var catalogLcmFolder = await GetFolderAsync("/users/admin/", cancellationToken).ConfigureAwait(false);

                    var importExcelFile = string.IsNullOrEmpty(options.ZipFileName) ?
                    await catalogLcmFolder.UploadFileAsync(stream, $@"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.zip", true, cancellationToken).ConfigureAwait(false) :
                    await catalogLcmFolder.GetFileAsync(options.ZipFileName).ConfigureAwait(false);

                    options.ZipFileName = importExcelFile.Name;
                    options.TargetApplicationName ??= applicationName;
                }

                // Set the application and cube names for the job.
                options.ApplicationName = applicationName;
                options.CubeName = cubeName;

                // Execute the import job and throw an exception if the job failed.
                (await CreateJob(options).ExecuteAsync(cancellationToken).ConfigureAwait(false)).ThrowIfFailed();

                // Return the corresponding application.
                return await GetApplicationAsync(applicationName: applicationName, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create the application ""{applicationName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, EssJobImportExcelOptions options ) => 
            CreateApplicationFromWorkbookAsync(applicationName, cubeName, options, CancellationToken.None).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public Task<IEssApplication> CreateApplicationFromWorkbookAsync( string applicationName, string cubeName, EssJobImportExcelOptions options, CancellationToken cancellationToken = default ) =>
            CreateApplicationFromWorkbookAsync(applicationName, cubeName, options, null, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null ) => 
            CreateApplicationFromWorkbookAsync(applicationName, cubeName, localWorkbookPath, options).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public async Task<IEssApplication> CreateApplicationFromWorkbookAsync( string applicationName, string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default )
        {
            try
            {
                if ( !File.Exists(localWorkbookPath) )
                    throw new FileNotFoundException("Unable to find the workbook file at the given local path.", localWorkbookPath);

                using var stream = new FileStream(localWorkbookPath, FileMode.Open, FileAccess.Read);
                return await CreateApplicationFromWorkbookAsync(applicationName, cubeName, options, stream).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create the application ""{applicationName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, Stream stream, EssJobImportExcelOptions options = null ) =>
            CreateApplicationFromWorkbookAsync(applicationName, cubeName, options, stream).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public Task<IEssApplication> CreateApplicationFromWorkbookAsync( string applicationName, string cubeName, Stream stream, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default ) =>
            CreateApplicationFromWorkbookAsync(applicationName, cubeName, options, stream, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        internal IEssApplication CreateApplicationFromWorkbook( string applicationName, string cubeName, EssJobImportExcelOptions options = null, Stream stream = null ) =>
            CreateApplicationFromWorkbookAsync(applicationName, cubeName, options, stream).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        internal async Task<IEssApplication> CreateApplicationFromWorkbookAsync( string applicationName, string cubeName, EssJobImportExcelOptions options = null, Stream stream = null, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(applicationName) )
                throw new ArgumentException($"An application name is required to create an an {nameof(EssApplication)} from a workbook.", nameof(applicationName));

            if ( string.IsNullOrWhiteSpace(cubeName) )
                throw new ArgumentException($"A cube name is required to create an an {nameof(EssApplication)} from a workbook.", nameof(cubeName));

            if ( stream is null && (string.IsNullOrEmpty(options?.CatalogExcelPath) || string.IsNullOrEmpty(options?.ImportExcelFilename)) )
                throw new ArgumentException($"A local path, stream, or server file is required to create an {nameof(EssApplication)} from a workbook.");

            try
            {
                // Construct new options if none were given.
                options ??= new EssJobImportExcelOptions();

                var catalogExcelFolder = string.IsNullOrEmpty(options.CatalogExcelPath) ?
                    await GetUserHomeFolderAsync(cancellationToken).ConfigureAwait(false) :
                    await GetFolderAsync(options.CatalogExcelPath, cancellationToken).ConfigureAwait(false);

                var importExcelFile = string.IsNullOrEmpty(options.ImportExcelFilename) ?
                    await catalogExcelFolder.UploadFileAsync(stream, $@"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.xlsx", true, cancellationToken).ConfigureAwait(false) :
                    await catalogExcelFolder.GetFileAsync(options.ImportExcelFilename).ConfigureAwait(false);

                // Set the CatalogExcelPath and ImportExcelFilename.
                options.CatalogExcelPath    = catalogExcelFolder.FullPath;
                options.ImportExcelFilename = importExcelFile.Name;

                // Set the BuildOption if it's not already set.
                if ( options.BuildOption is null )
                {
                    options.BuildOption = EssBuildOption.NONE;

                    try
                    {
                        if ( await GetApplicationAsync(applicationName, cancellationToken).ConfigureAwait(false) is { } )
                            options.BuildOption = EssBuildOption.RETAINALLDATA;
                    }
                    catch ( OperationCanceledException ) { throw; }
                    catch { /* The application does not exist. */ }
                }

                // Set the application and cube names for the job.
                options.ApplicationName = applicationName;
                options.CubeName        = cubeName;

                // Execute the import job and throw an exception if the job failed.
                (await CreateJob(options).ExecuteAsync(cancellationToken).ConfigureAwait(false)).ThrowIfFailed();

                // If the options are configured to do so, attempt to delete the imported Excel file.
                // The server doesn't respect the "deleteExcelOnSuccess" parameter.
                if ( options.DeleteExcelOnSuccess is true )
                    try { await importExcelFile.DeleteAsync(cancellationToken).ConfigureAwait(false); } catch { }

                // Return the corresponding application.
                return await GetApplicationAsync(applicationName: applicationName, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create the application ""{applicationName}"". {e.Message}", e);
            }
        }
        
        /// <inheritdoc />
        /// <returns>An <see cref="IEssGroup"/> object.</returns>
        public IEssGroup CreateGroup( string groupName, EssServerRole role = EssServerRole.User, string description = null ) => CreateGroupAsync(groupName, role, description).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGroup"/> object.</returns>
        public async Task<IEssGroup> CreateGroupAsync( string groupName, EssServerRole role = EssServerRole.User, string description = null, CancellationToken cancellationToken = default )
        {
            try
            {
                if ( string.IsNullOrEmpty(groupName) )
                    throw new ArgumentException("Name is required to create a new group.");

                var api = GetApi<GroupsApi>();

                var body = new GroupBean()
                {
                    Name = groupName,
                    Role = role.EssServerRoleToString(),
                    Description = description
                };

                if ( await api.GroupsAddAsync(body: body, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } group )
                    throw new Exception("cannot create new group.");

                return new EssGroup(group, this);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create the group ""{groupName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns> An <see cref="IEssJob"/> object.</returns>
        public IEssJob CreateJob( IEssJobOptions options ) => new EssJob(options, this);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssUser"/></returns>
        public IEssUser CreateUser( EssUserCreationOptions options ) => CreateUserAsync( options ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssUser"/></returns>
        public async Task<IEssUser> CreateUserAsync( EssUserCreationOptions options, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<UsersApi>();

                if ( await api.UsersAddAsync(body: options.ToUserBean(), cancellationToken: cancellationToken).ConfigureAwait(false) is not { } user )
                    throw new Exception($@"Cannot create new user.");

                return new EssUser(user, this);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create user {options.ID} on server ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns> An <see cref="IEssServerVariable"/> object.</returns>
        public IEssServerVariable CreateVariable( string name, string value ) => CreateVariableAsync( name, value ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns> An <see cref="IEssServerVariable"/> object.</returns>
        public async Task<IEssServerVariable> CreateVariableAsync( string name, string value, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(name) )
                throw new ArgumentException($"A variable name is required to create an {nameof(EssApplication)}.", nameof(name));

            if ( string.IsNullOrWhiteSpace(value) )
                throw new ArgumentException($"A value is required to create an {nameof(EssCube)}.", nameof(value));

            try
            {
                var variableInfo = new Variable(name: name, value: value);

                var api = GetApi<ServerVariablesApi>();
                if ( await api.VariablesCreateServerVariableAsync(body: variableInfo, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } variable )
                    throw new Exception("Cannot create new server variable.");

                return new EssServerVariable(variable, this);
            }
            catch ( OperationCanceledException ) { throw; }
            catch (Exception e)
            {
                throw new Exception($@"Unable to create the server variable ""{name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssAbout"/> object.</returns>
        public IEssAbout GetAbout() => GetAboutAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssAbout"/> object.</returns>
        public async Task<IEssAbout> GetAboutAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<AboutEssbaseApi>();
                var about = await api.AboutGetAboutAsync(0, cancellationToken).ConfigureAwait(false) ?? new About();

                return new EssAbout(about);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssAboutInstance"/> object.</returns>
        public IEssAboutInstance GetAboutInstance() => GetAboutInstanceAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssAboutInstance"/> object.</returns>
        public async Task<IEssAboutInstance> GetAboutInstanceAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<AboutEssbaseApi>();
                var aboutInstance = await api.AboutGetInstanceDetailsAsync(0, cancellationToken).ConfigureAwait(false) ?? new AboutInstance();

                return new EssAboutInstance(aboutInstance);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public IEssApplication GetApplication( string applicationName ) => GetApplicationAsync(applicationName)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public async Task<IEssApplication> GetApplicationAsync( string applicationName, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(applicationName) )
                throw new ArgumentException($"An application name is required to get an {nameof(EssApplication)}.", nameof(applicationName));

            try
            {
                var api = GetApi<ApplicationsApi>();

                if ( await api.ApplicationsGetApplicationAsync(applicationName, null, 0, cancellationToken).ConfigureAwait(false) is { } application )
                    return new EssApplication(application, this);

                throw new Exception("Received an empty or invalid response.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the application ""{applicationName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <see cref="_maxApplications"/>.</remarks>
        public List<IEssApplication> GetApplications() => GetApplicationsAsync(_maxApplications)?.GetAwaiter().GetResult() ?? new List<IEssApplication>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <paramref name="applicationsLimit"/>.</remarks>
        public List<IEssApplication> GetApplications( int applicationsLimit ) => GetApplicationsAsync(applicationsLimit)?.GetAwaiter().GetResult() ?? new List<IEssApplication>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <see cref="_maxApplications"/>.</remarks>
        public Task<List<IEssApplication>> GetApplicationsAsync( CancellationToken cancellationToken = default ) => GetApplicationsAsync(_maxApplications, cancellationToken);

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <paramref name="applicationsLimit"/>.</remarks>
        public async Task<List<IEssApplication>> GetApplicationsAsync( int applicationsLimit, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                var applications = await api.ApplicationsGetApplicationsAsync(null, null, applicationsLimit, null, null, null, 0, cancellationToken).ConfigureAwait(false);

                return applications?.ToEssSharpList(this) ?? new List<IEssApplication>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssDatasourceConnection"/> object.</returns>
        public IEssDatasourceConnection GetConnection( string connectionName ) => GetConnectionAsync(connectionName).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssDatasourceConnection"/> object.</returns>
        public async Task<IEssDatasourceConnection> GetConnectionAsync( string connectionName, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GlobalConnectionsApi>();
                if ( await api.GlobalConnectionsGetConnectionDetailsAsync(connectionName: connectionName, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } connection12 )
                    throw new Exception($@"Connection cannot be found.");

                return new EssDatasourceConnection(connection12, this);
            }
            catch ( OperationCanceledException ) { throw; }
            catch (Exception e)
            {
                throw new Exception($@"Unable to find connection with name: ""{this}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssDatasourceConnection"/> objects.</returns>
        public List<IEssDatasourceConnection> GetConnections() => GetConnectionsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssDatasourceConnection"/> objects.</returns>
        public async Task<List<IEssDatasourceConnection>> GetConnectionsAsync( CancellationToken cancellationToken = default )
        {

            try
            {
                var api = GetApi<GlobalConnectionsApi>();
                if ( await api.GlobalConnectionsGetConnectionsAsync(cancellationToken: cancellationToken).ConfigureAwait(false) is not { } connections )
                    throw new Exception("Connections cannot be found.");

                var connectionsList = new List<IEssDatasourceConnection>();

                foreach (var connection in connections.Items )
                {
                    connectionsList.Add(GetConnection(connection.Name));
                }

                return connectionsList;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to find connections list on ""{this}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssDatasource"/> object.</returns>
        public IEssDatasource GetDatasource( string datasourceName ) => GetDatasourceAsync( datasourceName ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssDatasource"/> object.</returns>
        public async Task<IEssDatasource> GetDatasourceAsync (string datasourceName , CancellationToken cancellationToken = default)
        {
            try
            {
                var api = GetApi<GlobalDatasourcesApi>();

                if ( await api.GlobalDatasourcesGetDatasourceDetailsAsync(datasourceName, 0, cancellationToken).ConfigureAwait(false) is { } datasource )
                    return new EssDatasource(datasource, this);

                throw new Exception("Received an empty or invalid response.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the datasource ""{datasourceName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssDatasource"/> objects.</returns>
        public List<IEssDatasource> GetDatasources() => GetDatasourcesAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssDatasource"/> objects.</returns>
        public async Task<List<IEssDatasource>> GetDatasourcesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GlobalDatasourcesApi>();
                var dataSource = await api.GlobalDatasourcesGetDatasourcesAsync(null, null, 0, cancellationToken).ConfigureAwait(false);

                return dataSource?.ToEssSharpList(this) ?? new List<IEssDatasource>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch 
            {
                throw;
            }
        }

        /// <inheritdoc />
        public EssGridPreferences GetDefaultGridPreferences() => GetDefaultGridPreferencesAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<EssGridPreferences> GetDefaultGridPreferencesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                // Return the default grid preferences if they've already been captured.
                if ( DefaultGridPreferences is not null )
                    return DefaultGridPreferences.Clone() as EssGridPreferences;

                // Otherwise, build a configuration that will use basic authentication but retain the new session.
                var config = new Configuration()
                {
                    ApplyCookies           = false,
                    RetainCookies          = true,
                    // Apply the base configuration settings.
                    BasePath               = Configuration.BasePath,
                    MaxDegreeOfParallelism = Configuration.MaxDegreeOfParallelism,
                    Username               = Configuration.Username,
                    Password               = Configuration.Password,
                    Timeout                = Configuration.Timeout,
                    UserAgent              = Configuration.UserAgent,
                };

                var api = ApiFactory.GetApiAndClient<GridPreferencesApi>(configuration: config, client: Client).Api;

                if ( await api.GridPreferencesGetAsync(cancellationToken: cancellationToken).ConfigureAwait(false) is { } preferences )
                    return (DefaultGridPreferences = preferences.ToEssGridPreferences()).Clone() as EssGridPreferences;

                throw new Exception("Received an empty or invalid response.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the default grid preferences for the server. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssFile"/> object.</returns>
        public IEssFile GetFile(string path) => GetFileAsync(path)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssFile"/> object.</returns>
        public async Task<IEssFile> GetFileAsync(string path, CancellationToken cancellationToken = default)
        {
            try
            {
                // Trim leading and trailing slashes from the given folder path.
                if ( string.IsNullOrEmpty(path = path?.Trim('/')) )
                    throw new ArgumentException("A file path is required.", nameof(path));

                var api = GetApi<FilesApi>();
                var files = default(FileCollectionResponse);

                // Split the given folder path into components and capture the last component as the folder name to filter on.
                var pathComponents = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var fileName = pathComponents?.LastOrDefault();

                // If we have one or fewer path components, we're looking for a root file.
                if (pathComponents?.Length <= 1)
                {
                    files = await api.FilesListFilesAsync(path: @"/", filter: fileName, recursive: false, cancellationToken: cancellationToken).ConfigureAwait(false);
                }
                // Otherwise, we're looking for a nested file.
                else
                {
                    // Build the search path from all but the last path component.
                    var searchPath = string.Join(@"/", pathComponents.Take(pathComponents.Length - 1));
                    files = await api.FilesListFilesAsync(path: searchPath, filter: fileName, recursive: false, cancellationToken: cancellationToken).ConfigureAwait(false);
                }

                // If the given file path was found, return it.
                foreach ( var file in files?.ToEssSharpList<IEssFile>(this) ?? new List<IEssFile>() )
                    if ( string.Equals(fileName, file.Name, StringComparison.OrdinalIgnoreCase) )
                        return file;

                throw new Exception("File not found.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                if ( !string.IsNullOrEmpty(path) )
                    throw new Exception($@"Unable to get the file ""/{path}"". {e.Message}", e);
                else
                    throw new Exception($@"Unable to get the file. {e.Message}", e); 
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssFolder"/> object.</returns>
        public IEssFolder GetFolder( string path ) => GetFolderAsync(path)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssFolder"/> object.</returns>
        public async Task<IEssFolder> GetFolderAsync( string path, CancellationToken cancellationToken = default )
        {
            // Trim leading and trailing slashes from the given folder path.
            if ( string.IsNullOrEmpty(path = path?.Trim('/')) )
                throw new ArgumentException("A folder path is required.", nameof(path));

            try
            {
                var api = GetApi<FilesApi>();
                var files = default(FileCollectionResponse);

                // Split the given folder path into components and capture the last component as the folder name to filter on.
                var pathComponents = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var folderName = pathComponents?.LastOrDefault();

                // If we have one or fewer path components, we're looking for a root folder.
                if ( pathComponents?.Length <= 1 )
                {
                    files = await api.FilesListRootFoldersAsync(folderName, false, 0, cancellationToken).ConfigureAwait(false);
                }
                // Otherwise, we're looking for a nested folder.
                else
                {
                    // Build the search path from all but the last path component.
                    var searchPath = string.Join(@"/", pathComponents.Take(pathComponents.Length - 1));
                    files = await api.FilesListFilesAsync(searchPath, null, null, "folder", null, null, null, folderName, false, 0, cancellationToken).ConfigureAwait(false);
                }

                // If the given folder path was found, return it.
                foreach ( var folder in files?.ToEssSharpList<IEssFolder>(this) ?? new List<IEssFolder>() )
                    if ( string.Equals(folderName, folder.Name, StringComparison.OrdinalIgnoreCase) )
                        return folder;

                throw new Exception("Folder not found.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch (Exception e)
            {
                throw new Exception($@"Unable to get the folder ""/{path}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A List of <see cref="IEssFolder"/> objects.</returns>
        public List<IEssFolder> GetFolders() => GetFoldersAsync()?.GetAwaiter().GetResult() ?? new List<IEssFolder>();

        /// <inheritdoc />
        /// <returns>A List of <see cref="IEssFolder"/> objects.</returns>
        public async Task<List<IEssFolder>> GetFoldersAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<FilesApi>();
                var files = await api.FilesListRootFoldersAsync(null, false).ConfigureAwait(false);

                return files?.ToEssSharpList<IEssFolder>(this) ?? new List<IEssFolder>();

            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGroup"/> object.</returns>
        public IEssGroup GetGroup( string name ) => GetGroupAsync(name).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGroup"/> object.</returns>
        public async Task<IEssGroup> GetGroupAsync( string name, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();
                foreach ( var group in (await api.GroupsSearchAsync(filter: name, cancellationToken: cancellationToken).ConfigureAwait(false)).Items )
                {
                    return new EssGroup(group, this);
                }

                throw new Exception("Groups not found.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the groups on ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssGroup"/> objects.</returns>
        public List<IEssGroup> GetGroups() => GetGroupsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssGroup"/> objects.</returns>
        public async Task<List<IEssGroup>> GetGroupsAsync(CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();
                if ( await api.GroupsSearchAsync(cancellationToken: cancellationToken).ConfigureAwait(false) is not { } groups ) 
                    throw new Exception("Groups not found.");

                return groups?.ToEssSharpList(this) ?? new List<IEssGroup>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the groups on ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssJob"/> objects.</returns>
        /// <remarks>The number of returned jobs is limited to the value of <see cref="_maxJobs"/>.</remarks>
        public List<IEssJob> GetJobs() => GetJobsAsync(_maxJobs)?.GetAwaiter().GetResult() ?? new List<IEssJob>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssJob"/> objects.</returns>
        /// <remarks>The number of returned jobs is limited to the value of <paramref name="jobsLimit"/>.</remarks>
        public List<IEssJob> GetJobs( long jobsLimit ) => GetJobsAsync(jobsLimit)?.GetAwaiter().GetResult() ?? new List<IEssJob>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssJob"/> objects.</returns>
        /// <remarks>The number of returned jobs is limited to the value of <see cref="_maxJobs"/>.</remarks>
        public Task<List<IEssJob>> GetJobsAsync( CancellationToken cancellationToken = default ) => GetJobsAsync(_maxJobs, cancellationToken);

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssJob"/> objects.</returns>
        /// <remarks>The number of returned jobs is limited to the value of <paramref name="jobsLimit"/>.</remarks>
        public async Task<List<IEssJob>> GetJobsAsync( long jobsLimit, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<JobsApi>();
                var jobs = await api.JobsGetAllJobRecordsAsync(null, null, "job_ID:desc", null, jobsLimit, null, 0, cancellationToken).ConfigureAwait(false);

                return jobs?.ToEssSharpList(this) ?? new List<IEssJob>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssSession"/> objects.</returns>
        public List<IEssSession> GetSessions() => GetSessionsAsync()?.GetAwaiter().GetResult() ?? new List<IEssSession>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssSession"/> objects.</returns>
        public async Task<List<IEssSession>> GetSessionsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<SessionsApi>();
                var sessions = await api.SessionsGetAllActiveSessionsAsync(null, null, null, 0, cancellationToken).ConfigureAwait(false);

                return sessions?.ToEssSharpList(this) ?? new List<IEssSession>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUrl" /> objects.</returns>
        public List<IEssUrl> GetURLs() => GetURLsAsync()?.GetAwaiter().GetResult() ?? new List<IEssUrl>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUrl" /> objects.</returns>
        public async Task<List<IEssUrl>> GetURLsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<URLsApi>();
                var urls = await api.URLsGetAsync(0, cancellationToken).ConfigureAwait(false);

                return urls?.ToEssSharpList(this) ?? new List<IEssUrl>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssUser"/> object</returns>
        public IEssUser GetUser( string id ) => GetUserAsync( id ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssUser"/> object</returns>
        public async Task<IEssUser> GetUserAsync( string id, CancellationToken cancellationToken = default )
        {
            try
            {
                foreach (var user in await GetUsersAsync( cancellationToken ).ConfigureAwait(false))
                {
                    if ( string.Equals(id, user.Name, StringComparison.OrdinalIgnoreCase) ) 
                    { 
                        return user; 
                    }
                }
                throw new Exception($@"Cannot get user {id}.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the users on ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssFolder"/> object.</returns>
        public IEssFolder GetUserHomeFolder() => GetUserHomeFolderAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssFolder"/> object.</returns>
        public async Task<IEssFolder> GetUserHomeFolderAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<FilesApi>();

                // Get the home path for the current user.
                if ( await api.FilesGetUserHomePathAsync(cancellationToken: cancellationToken).ConfigureAwait(false) is not { Length: > 0 } homePath )
                    throw new Exception("Could not resolve the user home path.");

                // Get the folder that corresponds to the obtained home path.
                return await GetFolderAsync(homePath).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                if ( !string.IsNullOrWhiteSpace(Configuration?.Username) )
                    throw new Exception($@"Unable to get the home folder for user ""{Configuration.Username}"". {e.Message}", e);
                else
                    throw new Exception($"Unable to get the user home folder. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssUserSession"/> object.</returns>
        public IEssUserSession GetUserSession( bool includeToken = true, bool includeGroups = true ) => GetUserSessionAsync(includeToken, includeGroups)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssUserSession"/> object.</returns>
        public async Task<IEssUserSession> GetUserSessionAsync( bool includeToken = true, bool includeGroups = true, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<UserSessionApi>();
                if ( await api.UserSessionGetSessionAsync(includeToken, includeGroups, 0, cancellationToken).ConfigureAwait(false) is { } userSession )
                    return new EssUserSession(userSession);

                throw new Exception("Received an empty or invalid response.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                if ( !string.IsNullOrWhiteSpace(Configuration?.Username) )
                    throw new Exception($@"Unable to create a new session for user ""{Configuration.Username}"". {e.Message}", e);
                else
                    throw new Exception($"Unable to create a new session. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssFolder"/> object.</returns>
        public IEssFolder GetUserSharedFolder() => GetUserSharedFolderAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssFolder"/> object.</returns>
        public async Task<IEssFolder> GetUserSharedFolderAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<FilesApi>();

                // Get the shared path for the current user.
                if ( await api.FilesGetSharedPathAsync(cancellationToken: cancellationToken).ConfigureAwait(false) is not { Length: > 0 } homePath )
                    throw new Exception("Could not resolve the user shared path.");

                // Return the folder that corresponds to the obtained shared path.
                return await GetFolderAsync(homePath).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                if ( !string.IsNullOrWhiteSpace(Configuration?.Username) )
                    throw new Exception($@"Unable to get the shared folder for user ""{Configuration.Username}"". {e.Message}", e);
                else
                    throw new Exception($"Unable to get the user shared folder. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUser"/> objects.</returns>
        public List<IEssUser> GetUsers() => GetUsersAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUser"/> objects.</returns>
        public async Task<List<IEssUser>> GetUsersAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<UsersApi>();
                if ( await api.UsersSearchAsync(cancellationToken: cancellationToken).ConfigureAwait(false) is not { } userList )
                    throw new Exception("No users were found.");

                return userList?.ToEssSharpList(this) ?? new List<IEssUser>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the users on ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUtility"/> objects.</returns>
        public List<IEssUtility> GetUtilities() => GetUtilitiesAsync()?.GetAwaiter().GetResult() ?? new List<IEssUtility>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUtility"/> objects.</returns>
        public async Task<List<IEssUtility>> GetUtilitiesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<TemplatesAndUtilitiesApi>();
                var utilities = await api.ResourcesGetUtilitiesAsync(0, cancellationToken).ConfigureAwait(false);

                return utilities?.ToEssSharpList(this) ?? new List<IEssUtility>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssServerVariable" /> objects.</returns>
        public List<IEssServerVariable> GetVariables() => GetVariablesAsync()?.GetAwaiter().GetResult() ?? new List<IEssServerVariable>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssServerVariable" /> objects.</returns>
        public async Task<List<IEssServerVariable>> GetVariablesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                // Setting getAllVariables to true would mean we get everything, including app and cube variables (qualified with app and cube names).
                // TODO: Enhance API to support getting all variables via the server.
                bool getAllVariables = false;

                var api = GetApi<ServerVariablesApi>();
                var variables = await api.VariablesListServerVariablesAsync(getAllVariables.ToString().ToLowerInvariant(), 0, cancellationToken).ConfigureAwait(false);

                return variables?.ToEssSharpList<IEssServerVariable>(this) ?? new List<IEssServerVariable>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <summary>
        /// Kills all active sessions for the given <paramref name="username"/> (including sessions with active requests).
        /// </summary>
        /// <param name="username">The user whose sessions to kill.</param>
        /// <remarks>Administrative privilege is required in order to perform this operation.</remarks>
        public void KillSessionsForUser( string username ) => KillSessionsForUserAsync(username).GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously kills all active sessions for the given <paramref name="username"/> (including sessions with active requests).
        /// </summary>
        /// <param name="username">The user whose sessions to kill.</param>
        /// <remarks>Administrative privilege is required in order to perform this operation.</remarks>
        public async Task KillSessionsForUserAsync( string username )
        {
            try
            {
                if ( string.IsNullOrEmpty(username) )
                    throw new ArgumentNullException(nameof(username), $"A {nameof(username)} is required to kill active sessions.");

                var api = GetApi<SessionsApi>();
                await api.SessionsDeleteAllActiveSessionsAsync(userId: username, disconnect: true).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                if ( !string.IsNullOrWhiteSpace(username) )
                    throw new Exception($@"Unable to kill all active sessions for user ""{username}"". {e.Message}", e);
                else
                    throw new Exception($@"Unable to kill all active sessions. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssUserSession"/> object, optionally containing a session token and group membership information.</returns>
        public IEssUserSession SignIn( bool includeToken = false, bool includeGroups = false ) => SignInAsync(includeToken, includeGroups).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssUserSession"/> object, optionally containing a session token and group membership information.</returns>
        public Task<IEssUserSession> SignInAsync( bool includeToken = false, bool includeGroups = false, CancellationToken cancellationToken = default ) => GetUserSessionAsync(includeToken, includeGroups, cancellationToken);

        /// <inheritdoc />
        public void SignOut( bool allSessions = false ) => SignOutAsync(allSessions).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task SignOutAsync( bool allSessions = false, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<UserSessionApi>();

                if ( allSessions )
                {
                    while ( !Client.SessionCookies.IsEmpty )
                        await api.UserSessionSignoffAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
                }

                await api.UserSessionSignoffAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                if ( !string.IsNullOrWhiteSpace(Configuration?.Username) )
                    throw new Exception($@"Unable to sign out of the current session for user ""{Configuration.Username}"". {e.Message}", e);
                else
                    throw new Exception(@"Unable to sign out of the current session.");
            }
        }

        #endregion

        #region IDisposable/IDisposableAsync Members

        /// <summary />
        /// <param name="disposing" />
        protected virtual void Dispose( bool disposing ) => DisposeAsync(disposing).GetAwaiter().GetResult();

        /// <summary />
        /// <param name="disposing" />
        /// <param name="cancellationToken" />
        protected virtual async Task DisposeAsync( bool disposing, CancellationToken cancellationToken = default )
        {
            if ( !_disposed )
            {
                if ( disposing )
                {
                    // Attempt to sign out of the server.
                    try { await SignOutAsync(allSessions: false, cancellationToken).ConfigureAwait(false); } catch { }
                }

                _disposed = true;
            }
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(disposing: true).ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
