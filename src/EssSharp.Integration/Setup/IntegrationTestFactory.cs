using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Docker.DotNet;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Testcontainers.MsSql;
using Xunit.Abstractions;

namespace EssSharp.Integration.Setup
{
    public static class IntegrationTestFactory
    {
        #region Private Data

        private static DockerContainer _databaseTestContainer;
        private static string          _databaseContainerId;

        private static DockerContainer _essbaseTestContainer;
        private static string          _essbaseContainerId;

        #endregion

        #region Public Properties

        /// <summary />
        public static string DatabaseContainerId => _databaseContainerId ?? throw new Exception("A database container must be initialized or running.");

        /// <summary />
        public static string EssbaseContainerId => _essbaseContainerId ?? throw new Exception("An essbase container must be initialized or running.");

        #endregion

        #region Public Methods

        /// <summary />
        public static async Task InitializeDatabaseContainerAsync( IMessageSink sink = null, CancellationToken cancellationToken = default )
        {
            // Get the sql server image (and an appropriate container name).
            var image         =  @"mcr.microsoft.com/mssql/server:2022-latest";
            var containerName = $@"{GetContainerNameForImage(Images.FirstOrDefault())}-database";

            // If a database test container is already running, update the retained ID and return.
            if ( _databaseTestContainer?.State is TestcontainersStates.Running )
            {
                _databaseContainerId = _databaseTestContainer.Id;
                return;
            }

            // Try to find an already running local database container.
            var databaseLocalContainer = (await GetDockerClient()
                .Containers
                .ListContainersAsync(new Docker.DotNet.Models.ContainersListParameters() { All = true }, cancellationToken))
                .FirstOrDefault(container => string.Equals(container?.State, "Running", StringComparison.OrdinalIgnoreCase) && container?.Names?.Any(name => string.Equals(containerName, name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);

            // If a local database container is already running, update the retained ID and return.
            if ( !string.IsNullOrEmpty(databaseLocalContainer?.ID) )
            {
                _databaseContainerId = databaseLocalContainer.ID;
                return;
            }

            // Otherwise, build and start a new database test container.

            var binPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var msSqlScriptPath = Path.Combine(binPath, "Scripts", "MsSql");

            // Build the database container.
            _databaseTestContainer = new MsSqlBuilder()
                .WithImage(image)                                 // "mcr.microsoft.com/mssql/server:2022-latest"
                .WithName(containerName)                          // "essbase-21-5-database"
                .WithNetwork("standalone")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SA_PASSWORD", "StrongPassw0rd")
                .WithPassword("StrongPassw0rd")
                .WithResourceMapping(new DirectoryInfo(msSqlScriptPath), @"/opt/scripts", UnixFileModes.UserRead | UnixFileModes.UserWrite | UnixFileModes.UserExecute | UnixFileModes.GroupRead | UnixFileModes.GroupExecute | UnixFileModes.OtherRead | UnixFileModes.OtherExecute)
                .WithCommand(@"/opt/scripts/start-db.sh")
                .WithCreateParameterModifier(pm => pm.HostConfig.DNS = new[] { "8.8.8.8", "8.8.4.4" })
                .WithCreateParameterModifier(pm => pm.Healthcheck = new Docker.DotNet.Models.HealthConfig()
                {
                    Test = new[] { "CMD", "/opt/scripts/healthcheck.sh" },
                    Interval = TimeSpan.FromSeconds(30),
                    Timeout = TimeSpan.FromSeconds(5),
                    StartPeriod = TimeSpan.FromSeconds(30).Ticks,
                    Retries = 10
                })
                .Build();

            // Start the database test container.
            await _databaseTestContainer.StartAsync(cancellationToken);

            // If the database test container is running, update the retained ID and return.
            if ( _databaseTestContainer?.State is TestcontainersStates.Running )
                _databaseContainerId = _databaseTestContainer.Id;
        }

        /// <summary />
        public static async Task InitializeEssbaseContainerAsync( IMessageSink sink = null, CancellationToken cancellationToken = default )
        {
            // Get the first configured essbase image (and an appropriate container name).
            var image         = Images.FirstOrDefault();
            var containerName = GetContainerNameForImage(image);

            // If an essbase test container is already running, update the retained ID and return.
            if ( _essbaseTestContainer?.State is TestcontainersStates.Running )
            {
                _essbaseContainerId = _essbaseTestContainer.Id;
                return;
            }

            // Try to find an already running local essbase container.
            var essbaseLocalContainer = (await GetDockerClient()
                .Containers
                .ListContainersAsync(new Docker.DotNet.Models.ContainersListParameters() { All = true }, cancellationToken))
                .FirstOrDefault(container => string.Equals(container?.State, "Running", StringComparison.OrdinalIgnoreCase) && container?.Names?.Any(name => string.Equals(containerName, name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);

            // If a local essbase container is already running, update the retained ID and return.
            if ( !string.IsNullOrEmpty(essbaseLocalContainer?.ID) )
            {
                _essbaseContainerId = essbaseLocalContainer.ID;
                return;
            }

            // Otherwise, build and start a new essbase test container.

            // Start by getting the admin password and host port from configuration.
            var connection  = GetEssConnection(EssServerRole.ServiceAdministrator);
            var hostPort = Uri.TryCreate(connection.Server, UriKind.Absolute, out var serverUri) ? serverUri.Port.ToString() : "9000";

            _essbaseTestContainer = new ContainerBuilder()
                .WithImage(image)                                                                     // "appliedolap/essbase:21.5-latest"
                .WithName(containerName)                                                              // "essbase-21-5"
                .WithNetwork("standalone")
                .WithPortBinding(hostPort, "9000")                                                    // "9000"
                .WithEnvironment("ADMIN_PASSWORD", connection.Password)                               // "welcome1"
                .WithEnvironment("DATABASE_TYPE", "sqlserver")
                .WithEnvironment("DATABASE_CONNECT_STRING", $@"{containerName}-database:1433:CertDB") // "essbase-21-5-database:1433:CertDB"
                .WithEnvironment("DATABASE_ADMIN_USERNAME", "sa")
                .WithEnvironment("DATABASE_ADMIN_PASSWORD", "StrongPassw0rd")
                .WithEnvironment("DATABASE_WAIT_TIMEOUT", "240")
                .WithCommand(@"/u01/container-scripts/createAndStartDomain.sh")
                .WithCreateParameterModifier(pm => pm.HostConfig.DNS = new[] { "8.8.8.8", "8.8.4.4" })
                .Build() as DockerContainer;

            // Start the essbase test container
            await (_essbaseTestContainer?.StartAsync(cancellationToken) ?? throw new Exception("Unable to build and start the configured Essbase container."));

            // If the essbase test container is running, update the retained ID and return.
            if ( _essbaseTestContainer?.State is TestcontainersStates.Running )
                _essbaseContainerId = _essbaseTestContainer.Id;
        }


        /// <summary />
        public static async Task DisposeAsync()
        {
            // Capture any test container disposal tasks.
            var disposeDatabase = _databaseTestContainer?.DisposeAsync().AsTask() ?? Task.CompletedTask;
            var disposeEssbase  = _essbaseTestContainer?.DisposeAsync().AsTask() ?? Task.CompletedTask;

            // Await the disposal of any created test containers.
            await Task.WhenAll(disposeDatabase, disposeEssbase);
        }

        #endregion

        #region Internal Properties

        /// <summary />
        internal static IntegrationTestSettingsConnection[] Connections { get; set; }

        /// <summary />
        internal static string[] Images { get; set; }

        #endregion

        #region Internal Methods

        /// <summary />
        internal static string GetContainerNameForImage( string image )
        {
            if ( image?.Split('/').LastOrDefault() is not { Length: > 0 } repoAndTag )
                return "essbase";

            if ( repoAndTag.EndsWith("-latest", StringComparison.OrdinalIgnoreCase) )
                repoAndTag = repoAndTag.Substring(0, repoAndTag.Length - "-latest".Length);

            return new string(repoAndTag.Select(c => char.IsLetterOrDigit(c) ? c : '-').ToArray());
        }

        /// <summary />
        internal static IntegrationTestSettingsConnection GetEssConnection( EssServerRole role = EssServerRole.ServiceAdministrator )
        {
            if ( Connections?.FirstOrDefault(conn => conn?.Role == role) is not IntegrationTestSettingsConnection connection )
                throw new Exception($@"A connection with the {role} role is not available");

            return connection;
        }

        /// <summary />
        internal static IEssServer GetEssServer( EssServerRole role = EssServerRole.ServiceAdministrator, EssServerFactory factory = null )
        {
            var connection = GetEssConnection(role);

            factory ??= new EssServerFactory();

            return factory.CreateEssServer(connection.Server, connection.Username, connection.Password, connect: false);
        }

        /// <summary />
        internal static DockerClient GetDockerClient() => TestcontainersSettings.OS?.DockerEndpointAuthConfig?.GetDockerClientConfiguration()?.CreateClient();

        #endregion
    }
}
