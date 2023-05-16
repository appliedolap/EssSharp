﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

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

        /// <summary>
        /// 
        /// </summary>
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

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _server;

        /// <inheritdoc />
        public override EssType Type => EssType.Server;

        #endregion

        #region IEssServer Members

        /// <inheritdoc />
        /// <returns>An <see cref="EssAbout"/> object.</returns>
        public IEssAbout GetAbout() => GetAboutAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssAbout"/> object.</returns>
        public async Task<IEssAbout> GetAboutAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<AboutEssbaseApi>();
                var about = await api.AboutGetAboutAsync(0, cancellationToken).ConfigureAwait(false) ?? new About();

                return new EssAbout(about);
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssAboutInstance"/> object.</returns>
        public IEssAboutInstance GetAboutInstance() => GetAboutInstanceAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssAboutInstance"/> object.</returns>
        public async Task<IEssAboutInstance> GetAboutInstanceAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<AboutEssbaseApi>();
                var aboutInstance = await api.AboutGetInstanceDetailsAsync(0, cancellationToken).ConfigureAwait(false) ?? new AboutInstance();

                return new EssAboutInstance(aboutInstance);
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssApplication"/> object.</returns>
        public IEssApplication GetApplication( string applicationName ) => GetApplicationAsync(applicationName)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssApplication"/> object.</returns>
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
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the application ""{applicationName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <see cref="_maxApplications"/>.</remarks>
        public List<IEssApplication> GetApplications() => GetApplicationsAsync(_maxApplications)?.GetAwaiter().GetResult() ?? new List<IEssApplication>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <paramref name="applicationsLimit"/>.</remarks>
        public List<IEssApplication> GetApplications( int applicationsLimit ) => GetApplicationsAsync(applicationsLimit)?.GetAwaiter().GetResult() ?? new List<IEssApplication>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <see cref="_maxApplications"/>.</remarks>
        public Task<List<IEssApplication>> GetApplicationsAsync( CancellationToken cancellationToken = default ) => GetApplicationsAsync(_maxApplications, cancellationToken);

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <paramref name="applicationsLimit"/>.</remarks>
        public async Task<List<IEssApplication>> GetApplicationsAsync( int applicationsLimit, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                var applications = await api.ApplicationsGetApplicationsAsync(null, null, applicationsLimit, null, null, null, 0, cancellationToken).ConfigureAwait(false);

                return applications?.ToEssSharpList(this) ?? new List<IEssApplication>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns></returns>
        public IEssDatasourceConnection GetConnection( string connectionName ) => GetConnectionAsync(connectionName).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<IEssDatasourceConnection> GetConnectionAsync( string connectionName, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GlobalConnectionsApi>();
                if ( await api.GlobalConnectionsGetConnectionDetailsAsync(connectionName: connectionName, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } connection12 )
                    throw new Exception($@"Connection cannot be found.");

                return new EssDatasourceConnection(connection12, this);
            }
            catch (Exception e)
            {
                throw new Exception($@"Unable to find connection with name: ""{this}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns></returns>
        public List<IEssDatasourceConnection> GetConnections() => GetConnectionsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
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
            catch ( Exception e )
            {
                throw new Exception($@"Unable to find connections list on ""{this}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns></returns>
        public IEssDatasource GetDatasource( string datasourceName ) => GetDatasourceAsync( datasourceName ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<IEssDatasource> GetDatasourceAsync (string datasourceName , CancellationToken cancellationToken = default)
        {
            try
            {
                var api = GetApi<GlobalDatasourcesApi>();

                if ( await api.GlobalDatasourcesGetDatasourceDetailsAsync(datasourceName, 0, cancellationToken).ConfigureAwait(false) is { } datasource )
                    return new EssDatasource(datasource, this);

                throw new Exception("Received an empty or invalid response.");
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the datasource ""{datasourceName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns> A list of <see cref="IEssDatasource"/> objects </returns>
        public List<IEssDatasource> GetDatasources() => GetDatasourcesAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns> A list of <see cref="IEssDatasource"/> objects </returns>
        public async Task<List<IEssDatasource>> GetDatasourcesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GlobalDatasourcesApi>();
                var dataSource = await api.GlobalDatasourcesGetDatasourcesAsync(null, null, 0, cancellationToken).ConfigureAwait(false);

                return dataSource?.ToEssSharpList(this) ?? new List<IEssDatasource>();
            }
            catch 
            {
                throw;
            }
        }



        /// <inheritdoc/>
        /// <returns>An <see cref="IEssFile"/> object.</returns>
        public IEssFile GetFile(string path) => GetFileAsync(path)?.GetAwaiter().GetResult();

        /// <inheritdoc/>
        /// <returns>An <see cref="IEssFile"/> object.</returns>
        public async Task<IEssFile> GetFileAsync(string path, CancellationToken cancellationToken = default)
        {
            // Trim leading and trailing slashes from the given folder path.
            if ( string.IsNullOrEmpty(path = path?.Trim('/')) )
                throw new ArgumentException("A file path is required.", nameof(path));

            try
            {
                var api = GetApi<FilesApi>();
                var files = default(FileCollectionResponse);

                // Split the given folder path into components and capture the last component as the folder name to filter on.
                var pathComponents = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                var fileName = pathComponents?.LastOrDefault();

                // If we have one or fewer path components, we're looking for a root folder.
                if (pathComponents?.Length <= 1)
                {
                    files = await api.FilesListRootFoldersAsync(fileName, false, 0, cancellationToken).ConfigureAwait(false);
                }
                // Otherwise, we're looking for a nested folder.
                else
                {
                    // Build the search path from all but the last path component.
                    var searchPath = string.Join(@"/", pathComponents.Take(pathComponents.Length - 1));
                    files = await api.FilesListFilesAsync(searchPath, null, null, null, null, null, null, fileName, false, 0, cancellationToken).ConfigureAwait(false);
                }

                // If the given folder path was found, return it.
                foreach (var file in files?.ToEssSharpList<IEssFile>(this) ?? new List<IEssFile>())
                    if (string.Equals(fileName, file.Name, StringComparison.OrdinalIgnoreCase))
                        return file;

                throw new Exception("File not found.");
            }
            catch (Exception e)
            {
                throw new Exception($@"Unable to get the file ""/{path}"". {e.Message}", e);
            }
        }

        /// <inheritdoc/>
        /// <returns>An <see cref="IEssFolder"/> object.</returns>
        public IEssFolder GetFolder( string path ) => GetFolderAsync(path)?.GetAwaiter().GetResult();

        /// <inheritdoc/>
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
            catch (Exception e)
            {
                throw new Exception($@"Unable to get the folder ""/{path}"". {e.Message}", e);
            }
        }

        /// <inheritdoc/>
        /// <returns>A List of <see cref="IEssFolder"/> objects.</returns>
        public List<IEssFolder> GetFolders() => GetFoldersAsync()?.GetAwaiter().GetResult() ?? new List<IEssFolder>();

        /// <inheritdoc/>
        /// <returns>A List of <see cref="IEssFolder"/> objects.</returns>
        public async Task<List<IEssFolder>> GetFoldersAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<FilesApi>();
                var files = await api.FilesListRootFoldersAsync(null, false).ConfigureAwait(false);

                return files?.ToEssSharpList<IEssFolder>(this) ?? new List<IEssFolder>();

            }
            catch ( Exception )
            {
                throw;
            }
        }

        public IEssGroup GetGroup( string name ) => GetGroupAsync(name).GetAwaiter().GetResult();

        public async Task<IEssGroup> GetGroupAsync( string name, CancellationToken cancellationToken = default )
        {
            {
                try
                {
                    var api = GetApi<GroupsApi>();
                    foreach ( var group in (await api.GroupsSearchAsync(filter: name, cancellationToken: cancellationToken).ConfigureAwait(false)).Items )
                    {
                        return new EssGroup(group, this);
                    }

                    throw new Exception("Groups not found.")
                }
                catch ( Exception e )
                {
                    throw new Exception($@"Unable to get the groups on ""{Name}"". {e.Message}", e);
                }
            }
        }

        public List<IEssGroup> GetGroups() => GetGroupsAsync().GetAwaiter().GetResult();

        public async Task<List<IEssGroup>> GetGroupsAsync(CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();
                if ( await api.GroupsSearchAsync(cancellationToken: cancellationToken).ConfigureAwait(false) is not { } groups ) 
                    throw new Exception("Groups not found.");

                return groups?.ToEssSharpList(this) ?? new List<IEssGroup>();
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the groups on ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssJob"/> objects.</returns>
        /// <remarks>The number of returned jobs is limited to the value of <see cref="_maxJobs"/>.</remarks>
        public List<IEssJob> GetJobs() => GetJobsAsync(_maxJobs)?.GetAwaiter().GetResult() ?? new List<IEssJob>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssJob"/> objects.</returns>
        /// <remarks>The number of returned jobs is limited to the value of <paramref name="jobsLimit"/>.</remarks>
        public List<IEssJob> GetJobs( long jobsLimit ) => GetJobsAsync(jobsLimit)?.GetAwaiter().GetResult() ?? new List<IEssJob>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssJob"/> objects.</returns>
        /// <remarks>The number of returned jobs is limited to the value of <see cref="_maxJobs"/>.</remarks>
        public Task<List<IEssJob>> GetJobsAsync( CancellationToken cancellationToken = default ) => GetJobsAsync(_maxJobs, cancellationToken);

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssJob"/> objects.</returns>
        /// <remarks>The number of returned jobs is limited to the value of <paramref name="jobsLimit"/>.</remarks>
        public async Task<List<IEssJob>> GetJobsAsync( long jobsLimit, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<JobsApi>();
                var jobs = await api.JobsGetAllJobRecordsAsync(null, null, "job_ID:desc", null, jobsLimit, null, 0, cancellationToken).ConfigureAwait(false);

                return jobs?.ToEssSharpList(this) ?? new List<IEssJob>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssSession"/> objects.</returns>
        public List<IEssSession> GetSessions() => GetSessionsAsync()?.GetAwaiter().GetResult() ?? new List<IEssSession>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssSession"/> objects.</returns>
        public async Task<List<IEssSession>> GetSessionsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<SessionsApi>();
                var sessions = await api.SessionsGetAllActiveSessionsAsync(null, null, null, 0, cancellationToken).ConfigureAwait(false);

                return sessions?.ToEssSharpList(this) ?? new List<IEssSession>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssUrl" /> objects.</returns>
        public List<IEssUrl> GetURLs() => GetURLsAsync()?.GetAwaiter().GetResult() ?? new List<IEssUrl>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssUrl" /> objects.</returns>
        public async Task<List<IEssUrl>> GetURLsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<URLsApi>();
                var urls = await api.URLsGetAsync(0, cancellationToken).ConfigureAwait(false);

                return urls?.ToEssSharpList(this) ?? new List<IEssUrl>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public IEssUserSession GetUserSession( bool includeToken = true, bool includeGroups = true ) => GetUserSessionAsync(includeToken, includeGroups)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<IEssUserSession> GetUserSessionAsync( bool includeToken = true, bool includeGroups = true, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<UserSessionApi>();
                if ( await api.UserSessionGetSessionAsync(includeToken, includeGroups, 0, cancellationToken).ConfigureAwait(false) is { } userSession )
                    return new EssUserSession(userSession);

                throw new Exception("Received an empty or invalid response.");
            }
            catch ( Exception e )
            {
                if ( !string.IsNullOrWhiteSpace(Configuration?.Username) )
                    throw new Exception($@"Unable to create a new session for user ""{Configuration.Username}"". {e.Message}", e);
                else
                    throw new Exception($"Unable to create a new session. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssUtility"/> objects.</returns>
        public List<IEssUtility> GetUtilities() => GetUtilitiesAsync()?.GetAwaiter().GetResult() ?? new List<IEssUtility>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssUtility"/> objects.</returns>
        public async Task<List<IEssUtility>> GetUtilitiesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<TemplatesAndUtilitiesApi>();
                var utilities = await api.ResourcesGetUtilitiesAsync(0, cancellationToken).ConfigureAwait(false);

                return utilities?.ToEssSharpList(this) ?? new List<IEssUtility>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssServerVariable" /> objects.</returns>
        public List<IEssServerVariable> GetVariables() => GetVariablesAsync()?.GetAwaiter().GetResult() ?? new List<IEssServerVariable>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssServerVariable" /> objects.</returns>
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
            catch ( Exception )
            {
                throw;
            }
        }

        #endregion
    }
}
