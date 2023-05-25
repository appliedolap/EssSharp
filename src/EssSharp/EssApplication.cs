using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssApplication : EssObject, IEssApplication
    {
        #region Private Data

        private readonly EssServer   _server;
        private readonly Application _application;

        #endregion

        #region Constructors

        /// <summary />
        internal EssApplication( Application application, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _application = application ?? 
                throw new ArgumentNullException(nameof(application), $"An API model {nameof(application)} is required to create an {nameof(EssApplication)}.");

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssApplication)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string  Name => _application.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Application;

        #endregion

        #region IEssApplication Properties

        /// <inheritdoc />
        public DateTime CreatedDate => DateTimeOffset.FromUnixTimeMilliseconds(_application.CreationTime).DateTime;

        /// <inheritdoc />
        public DateTime ModifiedDate => DateTimeOffset.FromUnixTimeMilliseconds(_application.ModifiedTime).DateTime;

        /// <inheritdoc />
        public IEssServer Server => _server;

        /// <inheritdoc />
        public EssApplicationStatus Status => Enum.TryParse(_application.Status, true, out EssApplicationStatus status) ? status : EssApplicationStatus.Unknown;

        #endregion

        #region IEssApplication Methods

        /// <inheritdoc />
        /// <returns>An <see cref="IEssCube"/> object.</returns>
        public IEssCube CreateCube(string cubeName, EssDatabaseCreationOptions options = null ) => CreateCubeAsync(cubeName, options).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssCube"/> object.</returns>
        public async Task<IEssCube> CreateCubeAsync( string cubeName, EssDatabaseCreationOptions options = null, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(cubeName) )
                throw new ArgumentException($"A cube name is required to create an {nameof(EssCube)}.", nameof(cubeName));

            try
            {
                options ??= new EssDatabaseCreationOptions();
                var createApp = new CreateApplication(applicationName: Name, databaseName: cubeName, allowDuplicates: options.AllowDuplicates, enableScenario: options.EnableScenarios, databaseType: options.DatabaseType.ToString());

                var api = GetApi<ApplicationsApi>();
                await api.ApplicationsCreateApplicationsAsync(body: createApp, cancellationToken: cancellationToken).ConfigureAwait(false);

                // Return the newly created application.
                return await GetCubeAsync(cubeName, cancellationToken).ConfigureAwait(false);
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create the cube ""{cubeName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssCube"/> object.</returns>
        public IEssCube CreateCubeFromWorkbook( string cubeName, EssJobImportExcelOptions options ) =>
            CreateCubeFromWorkbookAsync(cubeName, options, CancellationToken.None).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssCube"/> object.</returns>
        public Task<IEssCube> CreateCubeFromWorkbookAsync( string cubeName, EssJobImportExcelOptions options, CancellationToken cancellationToken = default ) =>
            CreateCubeFromWorkbookAsync(cubeName, options, null, cancellationToken);

        /// <inheritdoc />
        /// <returns> An <see cref="IEssCube"/> object. </returns>
        public IEssCube CreateCubeFromWorkbook( string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null ) => 
            CreateCubeFromWorkbookAsync( cubeName, localWorkbookPath, options ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssCube"/> object.</returns>
        public async Task<IEssCube> CreateCubeFromWorkbookAsync( string cubeName, string localWorkbookPath, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default )
        {
            try
            {
                if ( !File.Exists(localWorkbookPath) )
                    throw new FileNotFoundException("Unable to find the workbook file at the given local path.", localWorkbookPath);

                using var stream = new FileStream(localWorkbookPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return await CreateCubeFromWorkbookAsync(cubeName, options, stream, cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create the cube ""{cubeName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssCube"/> object.</returns>
        public IEssCube CreateCubeFromWorkbook( string cubeName, Stream stream, EssJobImportExcelOptions options = null ) =>
            CreateCubeFromWorkbookAsync(cubeName, options, stream).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssCube"/> object.</returns>
        public Task<IEssCube> CreateCubeFromWorkbookAsync( string cubeName, Stream stream, EssJobImportExcelOptions options = null, CancellationToken cancellationToken = default ) =>
            CreateCubeFromWorkbookAsync(cubeName, options, stream, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="IEssCube"/> object.</returns>
        internal IEssCube CreateCubeFromWorkbook( string cubeName, EssJobImportExcelOptions options = null, Stream stream = null ) =>
            CreateCubeFromWorkbookAsync(cubeName, options, stream).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssCube"/> object.</returns>
        internal async Task<IEssCube> CreateCubeFromWorkbookAsync( string cubeName, EssJobImportExcelOptions options = null, Stream stream = null, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(cubeName) )
                throw new ArgumentException($"A cube name is required to create an an {nameof(EssCube)} from a workbook.", nameof(cubeName));

            if ( stream is null && (string.IsNullOrEmpty(options?.CatalogExcelPath) || string.IsNullOrEmpty(options?.ImportExcelFilename)) )
                throw new ArgumentException($"A local path, stream, or server file is required to create an {nameof(EssCube)} from a workbook.");

            try
            {
                // Create the cube via the server's create application from workbook method.
                await Server.CreateApplicationFromWorkbookAsync(Name, cubeName, stream, options, cancellationToken).ConfigureAwait(false);
                // Return the cube.
                return await GetCubeAsync(cubeName, cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create the cube ""{cubeName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>an <see cref="IEssApplicationVariable"/></returns>
        public IEssApplicationVariable CreateApplicationVariable( string varName, string value ) => CreateApplicationVariableAsync( varName, value ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>an <see cref="IEssApplicationVariable"/></returns>
        public async Task<IEssApplicationVariable> CreateApplicationVariableAsync( string name, string value, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(name) )
                throw new ArgumentException($"A variable name is required to create an {nameof(EssApplication)}.", nameof(name));

            if ( string.IsNullOrWhiteSpace(value) )
                throw new ArgumentException($"A value is required to create an {nameof(EssCube)}.", nameof(value));
            try
            {
                var variableInfo = new Variable(name: name, value: value);
                var api = GetApi<VariablesApi>();
                if ( await api.VariablesCreateAppVariableAsync(applicationName: Name, body: variableInfo, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } variable )
                    throw new Exception("Could not create application variable.");

                return new EssApplicationVariable(variable, this);
            }
            catch (Exception e )
            {
                throw new Exception($@"Unable to create application variable ""{name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public void Copy( string copyName ) => CopyAsync(copyName)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task CopyAsync( string copyName, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                var copy = new CopyRenameBean(_application.Name, copyName);
                await api.ApplicationsCopyApplicationAsync(copy, 0, cancellationToken).ConfigureAwait(false); ;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public void Delete() => DeleteAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                await api.ApplicationsDeleteApplicationAsync(_application.Name, 0, cancellationToken).ConfigureAwait(false); ;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public Stream DownloadLatestLogFile() => DownloadLatestLogFileAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<Stream> DownloadLatestLogFileAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationLogsApi>();
                var fileStream = await api.ApplicationLogsDownloadLatestLogFileAsync(_application.Name, 0, cancellationToken).ConfigureAwait(false);

                return fileStream;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public string DownloadLatestLogFileString() => DownloadLatestLogFileStringAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<string> DownloadLatestLogFileStringAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationLogsApi>();
                var fileContent = await api.ApplicationLogsDownloadLatestLogFileContentAsync(_application.Name, 0, cancellationToken).ConfigureAwait(false);

                return fileContent;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns></returns>
        public IEssApplicationDatasourceConnection GetConnection( string appConnectionName ) => GetConnectionAsync(appConnectionName).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<IEssApplicationDatasourceConnection> GetConnectionAsync( string appConnectionName, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationConnectionsApi>();
                if ( await api.ApplicationConnectionsGetConnectionDetailsAsync(applicationName: Name, connectionName: appConnectionName, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } applicationConnection )
                    throw new Exception($@"Application connection cannot be found.");

                return new EssApplicationDatasourceConnection(applicationConnection, this);
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to find application connection with name: ""{this}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns></returns>
        public List<IEssApplicationDatasourceConnection> GetConnections() => GetConnectionsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<List<IEssApplicationDatasourceConnection>> GetConnectionsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationConnectionsApi>();
                if ( await api.ApplicationConnectionsGetConnectionsAsync(applicationName: Name, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } connections )
                    throw new Exception("Application connections cannot be found.");

                var connectionsList = new List<IEssApplicationDatasourceConnection>();

                foreach ( var connection in connections.Items )
                {
                    connectionsList.Add(GetConnection(connection.Name));
                }

                return connectionsList;
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to find application connections list on ""{this}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssCube"/> object.</returns>
        public IEssCube GetCube( string cubeName ) => GetCubeAsync(cubeName)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssCube"/> object.</returns>
        public async Task<IEssCube> GetCubeAsync( string cubeName, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(cubeName) )
                throw new ArgumentException($"A cube name is required to get an {nameof(EssCube)}.", nameof(cubeName));

            try
            {
                var api = GetApi<ApplicationsApi>();

                if ( await api.ApplicationsGetCubeAsync(_application.Name, cubeName, 0, cancellationToken).ConfigureAwait(false) is not Cube cube )
                    throw new Exception("Received an empty or invalid response.");

                return new EssCube(cube, this);
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the cube ""{cubeName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssCube" /> objects under this application.</returns>
        public List<IEssCube> GetCubes() => GetCubesAsync()?.GetAwaiter().GetResult() ?? new List<IEssCube>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssCube" /> objects under this application.</returns>
        public async Task<List<IEssCube>> GetCubesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                var cubes = await api.ApplicationsGetCubesAsync(_application.Name, null, null, 0, cancellationToken).ConfigureAwait(false);

                return cubes?.ToEssSharpList(this) ?? new List<IEssCube>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssApplicationConfiguration" /> objects under this application.</returns>
        public List<IEssApplicationConfiguration> GetConfigurations() => GetConfigurationsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssApplicationConfiguration" /> objects under this application.</returns>
        public async Task<List<IEssApplicationConfiguration>> GetConfigurationsAsync(CancellationToken cancellationToken = default)
        {
            var api = GetApi<ApplicationConfigurationApi>();
            var configurationList = await api.ApplicationConfigurationGetConfigurationsAsync(Name).ConfigureAwait(false);

            return configurationList?.ToEssSharpList(this) ?? new List<IEssApplicationConfiguration>();
        }

        /// <inheritdoc />
        public List<IEssApplicationVariable> GetVariables() => GetVariablesAsync()?.GetAwaiter().GetResult() ?? new List<IEssApplicationVariable>();

        /// <inheritdoc />
        public async Task<List<IEssApplicationVariable>> GetVariablesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<VariablesApi>();
                var variables = await api.VariablesListAppVariablesAsync(_application.Name, 0, cancellationToken).ConfigureAwait(false);

                return variables?.ToEssSharpList<IEssApplicationVariable>(this) ?? new List<IEssApplicationVariable>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public void Start() => StartAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public Task StartAsync( CancellationToken cancellationToken = default ) => PerformOperationAsync(ApplicationAction.Start, cancellationToken);

        /// <inheritdoc />
        public void Stop() => StopAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public Task StopAsync( CancellationToken cancellationToken = default ) => PerformOperationAsync(ApplicationAction.Stop, cancellationToken);

        #endregion

        #region Private Methods

        /// <summary />
        private enum ApplicationAction { Start, Stop }

        /// <summary />
        /// <param name="action" />
        /// <param name="cancellationToken" />
        private async Task PerformOperationAsync( ApplicationAction action, CancellationToken cancellationToken )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                await api.ApplicationsPerformOperationAsync(_application.Name, action.ToString(), 0, cancellationToken).ConfigureAwait(false);
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to perform the requested operation ""{action}"". {e.Message}", e);
            }
        }

        #endregion
    }
}
