using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Docker.DotNet;
using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Testcontainers.MsSql;
using Xunit.Sdk;

namespace EssSharp.Integration.Setup
{
    public static class IntegrationTestFactory
    {
        #region Private Data

        private static DockerContainer _databaseTestContainer;
        private static string          _databaseContainerId;

        private static DockerContainer _essbaseTestContainer;
        private static string          _essbaseContainerId;

        private static EssbaseContainerProfile _essbaseProfile;

        #endregion

        #region Public Properties

        /// <summary />
        public static string DatabaseContainerId => _databaseContainerId ?? throw new Exception("A database container must be initialized or running.");

        /// <summary />
        public static string EssbaseContainerId => _essbaseContainerId ?? throw new Exception("An essbase container must be initialized or running.");

        /// <summary>
        /// The profile resolved for the default (first configured) Essbase image.
        /// </summary>
        public static EssbaseContainerProfile EssbaseProfile => _essbaseProfile ??= EssbaseContainerProfile.Resolve(Images?.FirstOrDefault(), ImageSettings);

        #endregion

        #region Public Methods

        /// <summary />
        public static async Task InitializeDatabaseContainerAsync( IMessageSink sink = null, CancellationToken cancellationToken = default )
        {
            // Get the repository database image (and an appropriate container name) from the resolved profile.
            var profile       = EssbaseProfile;
            var image         = profile.DatabaseImage;                                    // "mcr.microsoft.com/mssql/server:2022-latest"
            var containerName = profile.DatabaseContainerName;                            // "essbase-21-7-database"

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
            _databaseTestContainer = new MsSqlBuilder(image)      // "mcr.microsoft.com/mssql/server:2022-latest"
                .WithName(containerName)                          // "essbase-21-7-database"
                .WithNetwork(profile.Network)
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SA_PASSWORD", profile.DatabaseAdminPassword)
                .WithPassword(profile.DatabaseAdminPassword)
                .WithResourceMapping(new DirectoryInfo(msSqlScriptPath), @"/opt/scripts", fileMode: UnixFileModes.UserRead | UnixFileModes.UserWrite | UnixFileModes.UserExecute | UnixFileModes.GroupRead | UnixFileModes.GroupExecute | UnixFileModes.OtherRead | UnixFileModes.OtherExecute)
                .WithCommand(@"/opt/scripts/start-db.sh")
                .WithCreateParameterModifier(pm => pm.HostConfig.DNS = new[] { "8.8.8.8", "8.8.4.4" })
                .WithCreateParameterModifier(pm => pm.Healthcheck = new Docker.DotNet.Models.HealthcheckConfig()
                {
                    Test = new[] { "CMD", "/opt/scripts/healthcheck.sh" },
                    Interval = TimeSpan.FromSeconds(30),
                    Timeout = TimeSpan.FromSeconds(5),
                    StartPeriod = TimeSpan.FromSeconds(30),
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
            // Get the default (first configured) essbase image and its resolved profile.
            var profile       = EssbaseProfile;
            var image         = profile.Image;
            var containerName = profile.ContainerName;

            // Fail early and clearly when no image has been configured.
            if ( image is not { Length: > 0 } )
                throw new Exception("An essbase image must be configured in the 'Images' section of the test settings.");

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
            var hostPort = Uri.TryCreate(connection.Server, UriKind.Absolute, out var serverUri) ? serverUri.Port.ToString() : profile.ManagedServerPort.ToString();

            var builder = new ContainerBuilder(image)                                             // "appliedolap/essbase:21.7-latest"
                .WithName(containerName)                                                          // "essbase-21-7"
                .WithNetwork(profile.Network)
                .WithPortBinding(hostPort, profile.ManagedServerPort.ToString())                  // "9000"
                .WithEnvironment("ADMIN_USERNAME", connection.Username)                               // "admin"
                .WithEnvironment("ADMIN_PASSWORD", connection.Password)                               // "welcome1"
                .WithEnvironment("DATABASE_TYPE", profile.DatabaseType)                               // "sqlserver"
                .WithEnvironment("DATABASE_CONNECT_STRING", profile.DatabaseConnectString)            // "essbase-21-7-database:1433:CertDB"
                .WithEnvironment("DATABASE_ADMIN_USERNAME", profile.DatabaseAdminUsername)            // "sa"
                .WithEnvironment("DATABASE_ADMIN_PASSWORD", profile.DatabaseAdminPassword)
                .WithEnvironment("DATABASE_WAIT_TIMEOUT", profile.DatabaseWaitTimeout)                // "240"
                .WithCommand(profile.StartupCommand)                                                  // "/u01/container-scripts/createAndStartDomain.sh"
                .WithCreateParameterModifier(pm => pm.HostConfig.DNS = new[] { "8.8.8.8", "8.8.4.4" });

            // Apply the profile environment last, so that a profile may override any variable set above.
            foreach ( var variable in profile.Environment )
                builder = builder.WithEnvironment(variable.Key, variable.Value);

            _essbaseTestContainer = builder.Build() as DockerContainer;

            // Start the essbase test container
            await (_essbaseTestContainer?.StartAsync(cancellationToken) ?? throw new Exception("Unable to build and start the configured Essbase container."));

            // If the essbase test container is running, update the retained ID and run any configured setup commands.
            if ( _essbaseTestContainer?.State is TestcontainersStates.Running )
            {
                _essbaseContainerId = _essbaseTestContainer.Id;

                await RunEssbaseSetupCommandsAsync(sink, cancellationToken);
            }
        }

        /// <summary>
        /// Executes the setup commands declared by the resolved profile against the running Essbase container.
        /// </summary>
        /// <param name="sink" />
        /// <param name="cancellationToken" />
        /// <remarks>
        /// This is called automatically for a container the harness starts. It is public so that a test - or a
        /// developer working against a container that was already running - may configure a container on demand.
        /// </remarks>
        public static async Task RunEssbaseSetupCommandsAsync( IMessageSink sink = null, CancellationToken cancellationToken = default )
        {
            foreach ( var command in EssbaseProfile.SetupCommands )
            {
                if ( command is not { Length: > 0 } )
                    continue;

                sink?.OnMessage(new Xunit.Sdk.DiagnosticMessage($@"Essbase container setup: {command}"));

                var (details, stdout) = await ExecAsync(EssbaseContainerId, new[] { "/bin/bash", "-lc", command }, cancellationToken);

                // Surface a failing setup command rather than letting the tests fail later for an unclear reason.
                if ( details?.ExitCode is not 0 )
                    throw new Exception($@"The Essbase container setup command '{command}' exited with code {details?.ExitCode}.{System.Environment.NewLine}{stdout}");
            }
        }

        /// <summary>
        /// Executes a command inside the given container and returns the exec details along with its output.
        /// </summary>
        /// <param name="id">The ID of the container to execute the command in.</param>
        /// <param name="command">The command and its arguments.</param>
        /// <param name="cancellationToken" />
        public static async Task<(ContainerExecInspectResponse details, string stdout)> ExecAsync( string id, string[] command, CancellationToken cancellationToken = default )
        {
            var execParams = new ContainerExecCreateParameters()
            {
                AttachStderr = true,
                AttachStdout = true,
                Cmd          = command,
            };

            using var client = GetDockerClient();

            // Create the exec instance, it is not started yet.
            var exec = await client.Exec.CreateContainerExecAsync(id, execParams, cancellationToken);
            // Start the exec instance and capture the output stream.
            using var stream = await client.Exec.StartContainerExecAsync(exec.ID, new ContainerExecStartParameters { Detach = false }, cancellationToken);
            var (stdout, stderr) = await stream.ReadOutputToEndAsync(cancellationToken);

            var details = await client.Exec.InspectContainerExecAsync(exec.ID, cancellationToken);

            return (details, stdout);
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
        internal static IntegrationTestAiQuerySettings AiQuery { get; set; }

        /// <summary />
        internal static IntegrationTestSettingsConnection[] Connections { get; set; }

        /// <summary />
        internal static string[] Images { get; set; }

        /// <summary>
        /// Per-image overrides describing how a given image differs from the Oracle Essbase 21.x container contract.
        /// </summary>
        internal static IntegrationTestSettingsImage[] ImageSettings { get; set; }

        #endregion

        #region Internal Methods

        /// <summary />
        internal static string GetContainerNameForImage( string image )
        {
            if ( image?.Split('/').LastOrDefault() is not { Length: > 0 } repoAndTag )
                return "essbase";

            if ( repoAndTag.EndsWith("-latest", StringComparison.OrdinalIgnoreCase) )
                repoAndTag = repoAndTag.Substring(0, repoAndTag.Length - "-latest".Length);

            if ( repoAndTag.Count(c => c == '.') == 2 )
                repoAndTag = repoAndTag.Substring(0, repoAndTag.LastIndexOf('.'));

            return new string(repoAndTag.Select(c => char.IsLetterOrDigit(c) ? c : '-').ToArray());
        }

        /// <summary />
        /// <param name="role" />
        internal static IntegrationTestSettingsConnection GetEssConnection( EssServerRole role = EssServerRole.ServiceAdministrator )
        {
            if ( Connections?.FirstOrDefault(conn => conn?.Role == role) is not IntegrationTestSettingsConnection connection )
                throw new Exception($@"A connection with the {role} role is not available");

            return connection.Clone() as IntegrationTestSettingsConnection;
        }

        /// <summary />
        /// <param name="role" />
        /// <param name="factory" />
        internal static IEssServer GetEssServer( EssServerRole role = EssServerRole.ServiceAdministrator, EssServerFactory factory = null ) =>
            GetEssServer(GetEssConnection(role), factory);

        /// <summary />
        /// <param name="connection" />
        /// <param name="factory" />
        internal static IEssServer GetEssServer( IntegrationTestSettingsConnection connection, EssServerFactory factory = null )
        {
            connection ??= GetEssConnection();
            factory    ??= new EssServerFactory();

            return (connection is { AccessToken.Length: > 0 }) switch
            {
                true => factory.CreateEssServer(connection.Server, oauthToken: connection.AccessToken, connect: false),
                false => factory.CreateEssServer(connection.Server, username: connection.Username, password: connection.Password, connect: false)
            };
        }

        /// <summary />
        internal static DockerClient GetDockerClient() => TestcontainersSettings.OS?.DockerEndpointAuthConfig?.GetDockerClientBuilder(ResourceReaper.DefaultSessionId)?.Build();

        #endregion
    }
}
