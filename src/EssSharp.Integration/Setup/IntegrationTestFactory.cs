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
using Xunit.Sdk;

namespace EssSharp.Integration.Setup
{
    public static class IntegrationTestFactory
    {
        private static DockerContainer _databaseTestContainer;
        private static string          _databaseContainerId;

        private static DockerContainer _essbaseTestContainer;
        private static string          _essbaseContainerId;

        /// <summary />
        public static DockerClient GetDockerClient() => 
            TestcontainersSettings.OS?.DockerEndpointAuthConfig?.GetDockerClientConfiguration()?.CreateClient();

        /// <summary />
        /// <param name="cancellationToken" />
        public static async Task InitializeDatabaseContainerAsync( IMessageSink sink = null, CancellationToken cancellationToken = default )
        {
            // If the database test container is running, update the retained ID and return.
            if ( _databaseTestContainer?.State is TestcontainersStates.Running )
            {
                _databaseContainerId = _databaseTestContainer.Id;
                return;
            }

            // Look for a running database local container.
            var databaseLocalContainer = (await GetDockerClient()
                .Containers
                .ListContainersAsync(new Docker.DotNet.Models.ContainersListParameters() { All = true }, cancellationToken))
                .FirstOrDefault(container => string.Equals(container?.State, "Running", StringComparison.OrdinalIgnoreCase) && container?.Names?.Any(name => string.Equals(@"essbase-21-4-database", name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);

            // If a database local container is running, update the retained ID and return.
            if ( !string.IsNullOrEmpty(databaseLocalContainer?.ID) )
            {
                _databaseContainerId = databaseLocalContainer.ID;
                return;
            }

            // Otherwise, build and start a new database test container.
            var binPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var msSqlScriptPath = Path.Combine(binPath, "Scripts", "MsSql");

            sink?.OnMessage(new DiagnosticMessage($@"""{msSqlScriptPath}"" {(Path.Exists(msSqlScriptPath) ? "exists." : "does not exist.")}"));

            _databaseTestContainer = new MsSqlBuilder()
                .WithImage(@"mcr.microsoft.com/mssql/server:2022-latest")
                .WithName("essbase-21-4-database")
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
        public static string DatabaseContainerId => _databaseContainerId ?? throw new Exception("A database container must be initialized or running.");

        /// <summary />
        /// <param name="cancellationToken" />
        public static async Task InitializeEssbaseContainerAsync( IMessageSink sink = null, CancellationToken cancellationToken = default )
        {
            // If the essbase test container is running, update the retained ID and return.
            if ( _essbaseTestContainer?.State is TestcontainersStates.Running )
            {
                _essbaseContainerId = _essbaseTestContainer.Id;
                return;
            }

            // Look for a running essbase local container.
            var essbaseLocalContainer = (await GetDockerClient()
                .Containers
                .ListContainersAsync(new Docker.DotNet.Models.ContainersListParameters() { All = true }, cancellationToken))
                .FirstOrDefault(container => string.Equals(container?.State, "Running", StringComparison.OrdinalIgnoreCase) && container?.Names?.Any(name => string.Equals(@"essbase-21-4", name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);

            // If a essbase local container is running, update the retained ID and return.
            if ( !string.IsNullOrEmpty(essbaseLocalContainer?.ID) )
            {
                _essbaseContainerId = essbaseLocalContainer.ID;
                return;
            }

            // Otherwise, build and start a new essbase test container.
            _essbaseTestContainer = new ContainerBuilder()
                .WithImage(@"appliedolap/essbase:21.4-latest")
                .WithName("essbase-21-4")
                .WithNetwork("standalone")
                .WithPortBinding("9000", "9000")
                .WithEnvironment("ADMIN_PASSWORD", "password1")
                .WithEnvironment("DATABASE_TYPE", "sqlserver")
                .WithEnvironment("DATABASE_CONNECT_STRING", "essbase-21-4-database:1433:CertDB")
                .WithEnvironment("DATABASE_ADMIN_USERNAME", "sa")
                .WithEnvironment("DATABASE_ADMIN_PASSWORD", "StrongPassw0rd")
                .WithEnvironment("DATABASE_WAIT_TIMEOUT", "240")
                //.WithBindMount(msSqlScriptPath, @"/opt/scripts", AccessMode.ReadWrite)
                .WithCommand(@"/u01/container-scripts/createAndStartDomain.sh")
                .WithCreateParameterModifier(pm => pm.HostConfig.DNS = new[] { "8.8.8.8", "8.8.4.4" })
                .Build() as DockerContainer;

            // Start the essbase test container.
            await _essbaseTestContainer.StartAsync(cancellationToken);

            // If the essbase test container is running, update the retained ID and return.
            if ( _essbaseTestContainer?.State is TestcontainersStates.Running )
                _essbaseContainerId = _essbaseTestContainer.Id;
        }

        /// <summary />
        public static string EssbaseContainerId => _essbaseContainerId ?? throw new Exception("An essbase container must be initialized or running.");

        public static async Task DisposeAsync()
        {
            // Capture any test container disposal tasks.
            var disposeDatabase = _databaseTestContainer?.DisposeAsync().AsTask() ?? Task.CompletedTask;
            var disposeEssbase  = _essbaseTestContainer?.DisposeAsync().AsTask() ?? Task.CompletedTask;

            // Await the disposal of any created test containers.
            await Task.WhenAll(disposeDatabase, disposeEssbase);
        }
    }
}
