using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Volumes;
using Xunit.Sdk;
using Xunit.v3;

namespace EssSharp.Integration.Setup
{
    /// <summary>
    /// Builds the environment the AI query tests need: an Essbase AI connection, without an OCI tenancy.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Essbase does not call a model itself. It asks an Oracle AI Database to, through <c>DBMS_CLOUD_AI</c> (chat) and
    /// <c>DBMS_VECTOR_CHAIN</c> (embeddings), and the database calls the model service's inference endpoint for the
    /// region. In <see cref="IntegrationTestAiQueryMode.Managed" /> mode the harness therefore starts two containers
    /// beside the Essbase one: an Oracle AI Database, and the AI gateway, which answers those calls (see
    /// <c>Scripts/Ai/gateway/gateway.py</c>). It answers in canned mode: fixed answers from <c>answers.json</c> and
    /// deterministic embeddings, so that the tests can assert what Essbase makes of them. To give a new test a
    /// deterministic answer, add an entry to <c>answers.json</c>.
    /// </para>
    /// <para>
    /// The gateway runs as a sidecar of the database: it shares the database container's network, and the database maps
    /// the inference host name to 127.0.0.1, so the database's calls reach the gateway and no other container sees the
    /// override. The gateway issues a certificate for that host name from a certificate authority of its own, into a
    /// volume the database reads; the database's startup hook (<c>Scripts/Ai/database/startdb</c>) trusts the authority
    /// and prepares the database. The gateway then sets Essbase up as a user would - a connection to the database, chat
    /// and vector credentials, a GenAI connection that uses them, an association between that connection and the
    /// application, and the job that vectorizes the application's outline - and logs <c>Essbase AI configured</c>. The
    /// application has to exist for the last two steps, so this runs when the AI tests start rather than with the
    /// assembly fixture - by then the tests that create the sample applications have run.
    /// </para>
    /// <para>
    /// The pair may already be running: a previous run that kept its environment, or a tool that starts it as persistent
    /// containers (such as the scoop <c>essbase</c> stack, whose containers have the same names), may have left it. A
    /// running pair is adopted as it is, the way the Essbase container is.
    /// </para>
    /// <para>
    /// When the run ends, it removes what it created and nothing else, whoever else is using the containers. The AI
    /// connection is taken out of Essbase if the run configured it there (unless the run started the Essbase container,
    /// which goes with it), and then the containers and the volume the run created are removed. The order matters: an
    /// Essbase server that outlived its database would be left with an AI connection, and a pool of connections,
    /// pointing at nothing. Containers, a volume and an AI connection that were already there are left alone. A container
    /// the run did not create is never removed or started, even when it is in the way.
    /// <see cref="IntegrationTestAiQuerySettings.KeepEnvironment" /> leaves it all in place instead, which makes the next
    /// run faster; every step checks before it acts, so a later run finds the pair and reuses it.
    /// </para>
    /// </remarks>
    public static class IntegrationTestAiEnvironment
    {
        #region Private Data

        /// <summary>
        /// The first Essbase release that has AI query. A server older than this has nothing to configure; the tests
        /// assert that it refuses the AI endpoints.
        /// </summary>
        private static readonly System.Version FirstSupportedVersion = new(major: 21, minor: 8);

        /// <summary>
        /// The longest the whole environment may take to build. Pulling the images, starting the database and
        /// vectorizing the outline take minutes on a cold start; a build that takes this long is stuck, and the tests
        /// should fail with a message instead of waiting for the limit of the job that runs them.
        /// </summary>
        private static readonly TimeSpan EnvironmentTimeout = TimeSpan.FromMinutes(30);

        /// <summary>
        /// What the gateway logs once Essbase is ready to answer questions.
        /// </summary>
        private const string ConfiguredMessage = @"Essbase AI configured";

        private const string GatewayDirectory  = @"/opt/ai-gateway";
        private const string ScriptDirectory   = @"/opt/ai-scripts";
        private const string StartDbDirectory  = @"/container-entrypoint-startdb.d";
        private const string CertificateFolder = @"/opt/ai-certs";

        private const string DatabaseAdminPassword = @"AiSys_Passw0rd1";
        private const string DatabaseUser          = @"ESSAI";
        private const string DatabaseUserPassword  = @"AiUser_Passw0rd1";

        // The names the gateway gives what it creates in Essbase (see configure-essbase-ai.py).
        private const string DatabaseConnectionName = @"ess_ai_database";
        private const string AiConnectionName       = @"ess_ai_genai";

        // Everything the harness creates carries this label, so that a leftover of its own can be told from a container
        // or a volume that somebody else started.
        private const string EnvironmentLabelKey   = @"com.appliedolap.essharp.integration-tests";
        private const string EnvironmentLabelValue = @"ai-environment";

        // The Unix permissions of the files copied into the containers: rw-r--r-- and rwxr-xr-x.
        private const UnixFileModes ReadableMode   = UnixFileModes.UserRead | UnixFileModes.UserWrite | UnixFileModes.GroupRead | UnixFileModes.OtherRead;
        private const UnixFileModes ExecutableMode = ReadableMode | UnixFileModes.UserExecute | UnixFileModes.GroupExecute | UnixFileModes.OtherExecute;

        // The gateway's files, copied into the gateway container.
        private static readonly string[] GatewayFiles = { "gateway.py", "essbase_hints.py", "configure-essbase-ai.py", "generate-certs.sh", "start.sh", "answers.json" };

        private static readonly SemaphoreSlim _gate = new(1, 1);

        // The containers this run created, by ID. Only these are removed when the run ends.
        private static readonly List<string> _createdContainerIds = new();

        private static bool                           _ready;
        private static bool                           _cannedAnswers;
        private static bool                           _keep;
        private static bool                           _ownsEssbaseConfiguration;
        private static bool                           _createdDatabaseConnection;
        private static bool                           _createdAiConnection;
        private static string                         _gatewayName;
        private static string                         _databaseName;
        private static string                         _volumeName;
        private static IVolume                        _createdVolume;
        private static IntegrationTestAiQuerySettings _settings;
        private static IMessageSink                   _sink;

        #endregion

        #region Public Properties

        /// <summary>
        /// Whether the AI gateway answers with the canned answers in <c>answers.json</c>, so that a test can assert what
        /// Essbase makes of them: always when the harness started the gateway, and when it adopted a running one that was
        /// started in canned mode.
        /// </summary>
        public static bool ServesCannedAnswers => _cannedAnswers;

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds the AI query environment if the settings ask the harness to, and it has not been built already.
        /// </summary>
        /// <param name="sink" />
        /// <param name="cancellationToken" />
        public static async Task EnsureAsync( IMessageSink sink = null, CancellationToken cancellationToken = default )
        {
            var settings = IntegrationTestFactory.AiQuery ?? new IntegrationTestAiQuerySettings();
            var profile  = IntegrationTestFactory.EssbaseProfile;

            if ( settings.Mode != IntegrationTestAiQueryMode.Managed )
                return;

            if ( profile.Version is { } version && version < FirstSupportedVersion )
                return;

            // AI query needs registry settings that were forced when the Essbase container started. If that failed, say so
            // here, where it explains why these tests cannot run, rather than letting each of them fail on its own.
            if ( IntegrationTestFactory.RegistrySettingsFailure is { } registryFailure )
                throw new InvalidOperationException($@"The Essbase registry settings that AI query needs could not be forced: {registryFailure.Message}", registryFailure);

            await _gate.WaitAsync(cancellationToken);

            try
            {
                if ( _ready )
                    return;

                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                // What disposal has to remove is recorded first, so that it can be removed even if building fails half way.
                _settings     = settings;
                _sink         = sink;
                _keep         = settings.KeepEnvironment;
                _gatewayName  = $@"{profile.ContainerName}-ai-gateway";
                _databaseName = $@"{profile.ContainerName}-ai-database";
                _volumeName   = $@"{profile.ContainerName}-ai-certs";

                // Every wait below - the image pulls, the container wait strategies, the gateway's configuration of
                // Essbase - takes this token, so one timeout bounds them all.
                using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                timeout.CancelAfter(EnvironmentTimeout);

                try
                {
                    var associated = await InspectEssbaseAsync(settings, timeout.Token);
                    var created    = await StartContainersAsync(settings, profile, sink, timeout.Token);

                    if (associated is { } associatedName)
                    {
                        // That earlier run's database, with its credentials and the vectors of the outline, is only there if
                        // its containers still are. A database built just now is empty, and Essbase cannot be pointed back
                        // at it from outside: its connection pool still holds connections to the database that was removed.
                        if (created)
                            throw new InvalidOperationException(
                                $@"'{settings.ApplicationName}' still uses the AI connection '{associatedName}', which an earlier run set up against AI query containers that no longer exist, so it points at a database that has been rebuilt empty. " +
                                $@"Remove the AI connection (dissociate it from '{settings.ApplicationName}' and delete it; restart Essbase first if it reports 'UCP-29: Failed to get a connection') or use a new Essbase container, then run the tests again. " +
                                $@"A run removes the AI connection it created itself when it ends, so this only happens if an earlier run was stopped before it could.");

                        // An association somebody else made - an earlier run, or a persistent environment - is not this run's to undo.
                        Log(sink, $@"'{settings.ApplicationName}' already uses the AI connection '{associatedName}'.");
                    }
                    else if (!created)
                    {
                        // A running gateway configures Essbase when it starts: every cube, or only those in its AI_APPLICATIONS.
                        throw new InvalidOperationException(
                            $@"The AI query containers '{_gatewayName}' and '{_databaseName}' are running, but '{settings.ApplicationName}' has no AI connection. " +
                            $@"Restart '{_gatewayName}' so that it configures it (with an empty AI_APPLICATIONS, or one that includes '{settings.ApplicationName}.{settings.CubeName}'), or remove both containers so that the harness starts its own.");
                    }
                }
                catch ( OperationCanceledException ) when ( timeout.IsCancellationRequested && !cancellationToken.IsCancellationRequested )
                {
                    await DisposeAsync();

                    throw new TimeoutException($@"The AI query environment was not ready within {EnvironmentTimeout.TotalMinutes:0} minutes.");
                }
                catch
                {
                    // A half-built environment is no use to any test, and would otherwise keep its database container
                    // (and the memory it takes) until the run ends. Whatever was kept on purpose is left alone.
                    await DisposeAsync();

                    throw;
                }

                Log(sink, $@"The AI query environment is ready ({stopwatch.Elapsed.TotalSeconds:0} s).");

                _ready = true;
            }
            finally
            {
                _gate.Release();
            }
        }

        /// <summary>
        /// Removes what the run created of the AI query environment, unless the settings ask for it to be kept: the AI
        /// connection first, then the containers and the volume.
        /// </summary>
        /// <remarks>
        /// Containers, a volume and an AI connection that were already there are left alone. This never throws. It runs
        /// before the Essbase containers are removed, and a failure here must not stop that; the test framework's
        /// cleanup removes whatever is left when the process exits.
        /// </remarks>
        public static async Task DisposeAsync()
        {
            try
            {
                if (!_keep && _gatewayName is not null)
                {
                    // An Essbase container the run started goes away with its AI connection. One that was already running
                    // is left as it was found: only a connection the run itself created is taken out of it.
                    try
                    {
                        if ( _ownsEssbaseConfiguration && !IntegrationTestFactory.EssbaseContainerStarted )
                            await TearDownEssbaseAsync();
                    }
                    catch ( Exception error )
                    {
                        // The containers the run created are removed whether or not Essbase could be cleaned up.
                        Log(_sink, $@"Unable to remove the AI connection from Essbase: {error.Message}");
                    }

                    await RemoveContainersAsync();
                }
            }
            catch ( Exception error )
            {
                Log(_sink, $@"Unable to remove the AI query environment: {error.Message}");
            }
            finally
            {
                _createdContainerIds.Clear();
                _createdVolume             = null;
                _ownsEssbaseConfiguration  = false;
                _createdDatabaseConnection = false;
                _createdAiConnection       = false;
                _gatewayName               = null;
                _databaseName              = null;
                _volumeName                = null;
                _ready                     = false;
                _cannedAnswers             = false;
            }
        }
        #endregion

        #region Containers

        /// <summary>
        /// Starts the database and the gateway beside it, unless both of them are already running.
        /// </summary>
        /// <remarks>
        /// Only a container of the harness's own (one that carries <see cref="EnvironmentLabelKey" />) is ever removed to
        /// make room for a new one. If a container the harness did not create is in the way, because the pair is not
        /// running, this throws before anything is touched.
        /// </remarks>
        /// <returns><see langword="true" /> if the containers were started, <see langword="false" /> if they were already running.</returns>
        /// <exception cref="InvalidOperationException">A container that the harness did not create exists, and the pair is not running.</exception>
        private static async Task<bool> StartContainersAsync( IntegrationTestAiQuerySettings settings, EssbaseContainerProfile profile, IMessageSink sink, CancellationToken cancellationToken )
        {
            using var client = IntegrationTestFactory.GetDockerClient();

            var all      = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true }, cancellationToken);
            var gateway  = all.FirstOrDefault(container => container?.Names?.Any(name => string.Equals(_gatewayName, name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);
            var database = all.FirstOrDefault(container => container?.Names?.Any(name => string.Equals(_databaseName, name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);

            // The two containers are a pair: the gateway shares the database's network, and the database trusts the
            // certificate authority whose certificate the gateway presents. If both are running, somebody made that pair,
            // and it can be used as it is. It is not this run's to remove, whoever started it: only the containers this
            // run creates are removed when it ends.
            if (IsRunning(gateway) && IsRunning(database))
            {
                var inspected = await client.Containers.InspectContainerAsync(gateway.ID, cancellationToken);

                _cannedAnswers = inspected.Config?.Env?.Contains("AI_GATEWAY_MODE=canned") is true;

                Log(sink, $@"Using the running AI query containers '{_gatewayName}' and '{_databaseName}' ({(_cannedAnswers ? "canned answers" : "the gateway's models")}).");
                return false;
            }

            // Otherwise start again. A container left behind without its partner is no use, but it is only the harness's
            // to remove if the harness made it. Check them all before removing any.
            var strays  = new[] { gateway, database }.Where(container => container is not null).ToList();
            var foreign = strays.Where(container => !IsHarnessContainer(container)).ToList();

            if ( foreign.Count > 0 )
            {
                // Say what each of the two containers is, including one that does not exist, so that a lone container does
                // not read as a contradiction.
                var states = string.Join(", ", new[] { (Name: _gatewayName, Container: gateway), (Name: _databaseName, Container: database) }.Select(item => $@"'{item.Name}' ({item.Container?.State ?? "missing"})"));
                var names  = string.Join(" and ", foreign.Select(container => $@"'{container.Names?.FirstOrDefault()?.Trim('/')}'"));
                var verb   = foreign.Count == 1 ? "was" : "were";

                throw new InvalidOperationException(
                    $@"The AI query containers are not both running: {states}. " +
                    $@"{names} {verb} not created by the test harness, and the harness will not remove or start containers it did not create. " +
                    $@"Start them yourself so that both are running, or remove them, or set AiQuery:Mode to External so that the harness leaves the AI environment to you.");
            }

            foreach ( var stray in strays )
                await client.Containers.RemoveContainerAsync(stray.ID, new ContainerRemoveParameters() { Force = true }, cancellationToken);

            // The certificates the gateway issues, shared with the database. A fresh volume for a fresh pair: a database
            // trusts only the authority it was started with.
            try
            {
                await client.Volumes.RemoveAsync(_volumeName, force: true, cancellationToken);
            }
            catch (Docker.DotNet.DockerApiException)
            {
                // There was no such volume, or it is in use by a container that is not the harness's; creating it says which.
            }

            _createdVolume = new VolumeBuilder()
                .WithName(_volumeName)
                .WithLabel(EnvironmentLabelKey, EnvironmentLabelValue)
                .WithCleanUp(!_keep)
                .Build();

            await _createdVolume.CreateAsync(cancellationToken);

            var host       = GetInferenceHost(settings);
            var connection = IntegrationTestFactory.GetEssConnection();

            // Unless the environment is to be kept, the test framework's cleanup removes the containers when the test
            // process exits, even if the run never reached its own cleanup. The database is started first, because the
            // gateway joins its network; its startup hook waits for the gateway's certificate authority, so its readiness
            // is awaited after the gateway has started.
            var databaseContainer = new ContainerBuilder(settings.DatabaseImage)
                .WithName(_databaseName)
                .WithLabel(EnvironmentLabelKey, EnvironmentLabelValue)
                .WithNetwork(profile.Network)
                .WithExtraHost(host, "127.0.0.1")
                .WithCleanUp(!_keep)
                .WithEnvironment("ORACLE_PASSWORD", DatabaseAdminPassword)
                .WithEnvironment("APP_USER", DatabaseUser)
                .WithEnvironment("APP_USER_PASSWORD", DatabaseUserPassword)
                .WithVolumeMount(_createdVolume, CertificateFolder, AccessMode.ReadOnly)
                .WithResourceMapping(ReadScript("database", "setup-ai-database.sh"), $@"{ScriptDirectory}/setup-ai-database.sh", fileMode: ExecutableMode)
                .WithResourceMapping(ReadScript(Path.Combine("database", "startdb"), "01-essbase-ai.sh"), $@"{StartDbDirectory}/01-essbase-ai.sh", fileMode: ExecutableMode)
                .Build() as DockerContainer;

            Log(sink, $@"Starting the AI query containers '{_databaseName}' from '{settings.DatabaseImage}' and '{_gatewayName}' beside it (as {host}).");

            try
            {
                await databaseContainer.StartAsync(cancellationToken);
            }
            finally
            {
                RecordCreated(databaseContainer);
            }

            var gatewayBuilder = new ContainerBuilder(settings.GatewayImage)
                .WithName(_gatewayName)
                .WithLabel(EnvironmentLabelKey, EnvironmentLabelValue)
                .WithCleanUp(!_keep)
                // The gateway shares the database's network: it answers on 127.0.0.1, where the database sends the
                // inference host name, and reaches Essbase through the database's network.
                .WithCreateParameterModifier(parameters => parameters.HostConfig.NetworkMode = $@"container:{databaseContainer.Id}")
                .WithVolumeMount(_createdVolume, CertificateFolder)
                .WithEnvironment("AI_GATEWAY_MODE", "canned")
                .WithEnvironment("AI_GATEWAY_CERTIFICATE", $@"{CertificateFolder}/server.crt")
                .WithEnvironment("AI_GATEWAY_PRIVATE_KEY", $@"{CertificateFolder}/server.key")
                .WithEnvironment("OCI_REGION", settings.Region)
                .WithEnvironment("ESSBASE_URL", $@"http://{profile.ContainerName}:{profile.ManagedServerPort}")
                .WithEnvironment("ESSBASE_USER", connection.Username)
                .WithEnvironment("ESSBASE_PASSWORD", connection.Password)
                .WithEnvironment("AI_DATABASE_HOST", _databaseName)
                .WithEnvironment("APP_USER", DatabaseUser)
                .WithEnvironment("APP_USER_PASSWORD", DatabaseUserPassword)
                .WithEnvironment("AI_CHAT_MODEL", settings.ChatModel)
                .WithEnvironment("AI_EMBED_MODEL", settings.EmbedModel)
                .WithEnvironment("AI_APPLICATIONS", $@"{settings.ApplicationName}.{settings.CubeName}")
                .WithEnvironment("AI_SETUP_MINUTES", $@"{EnvironmentTimeout.TotalMinutes:0}")
                .WithCommand("sh", $@"{GatewayDirectory}/start.sh")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(ConfiguredMessage));

            foreach (var file in GatewayFiles)
                gatewayBuilder = gatewayBuilder.WithResourceMapping(ReadScript("gateway", file), $@"{GatewayDirectory}/{file}", fileMode: file.EndsWith(".sh", StringComparison.Ordinal) ? ExecutableMode : ReadableMode);

            var gatewayContainer = gatewayBuilder.Build() as DockerContainer;

            try
            {
                // The gateway logs that Essbase is configured only once the database is ready and the outline vectorized.
                await gatewayContainer.StartAsync(cancellationToken);
            }
            finally
            {
                RecordCreated(gatewayContainer);
            }

            Log(sink, $@"The AI gateway configured '{settings.ApplicationName}.{settings.CubeName}'.");

            _cannedAnswers = true;

            return true;
        }

        /// <summary>
        /// Removes the containers this run created, and then the volume if this run created it.
        /// </summary>
        /// <remarks>
        /// The containers are removed by ID, never by name: a container of the same name that somebody else started, or
        /// that was already there when the run began, is not this run's to remove. The gateway goes first, because it
        /// lives in the database's network.
        /// </remarks>
        private static async Task RemoveContainersAsync()
        {
            if (_createdContainerIds.Count == 0 && _createdVolume is null)
                return;

            using var cancellation = new CancellationTokenSource(TimeSpan.FromMinutes(2));
            using var client       = IntegrationTestFactory.GetDockerClient();

            foreach (var id in Enumerable.Reverse(_createdContainerIds))
            {
                try
                {
                    await client.Containers.RemoveContainerAsync(id, new ContainerRemoveParameters() { Force = true }, cancellation.Token);
                }
                catch ( Docker.DotNet.DockerContainerNotFoundException )
                {
                    // A container that is already gone, removed by the test framework's cleanup, needs no removing.
                }
                catch ( Exception error )
                {
                    // The other containers and the volume are still this run's to remove.
                    Log(_sink, $@"Unable to remove the AI query container '{id}': {error.Message}");
                }
            }

            if (_createdVolume is null)
                return;

            try
            {
                await _createdVolume.DeleteAsync(cancellation.Token);
            }
            catch ( Exception )
            {
                // A volume that is already gone needs no removing.
            }
        }

        /// <summary>
        /// Remembers that this run created the container, so that it is removed when the run ends.
        /// </summary>
        /// <param name="container" />
        private static void RecordCreated( IContainer container )
        {
            try
            {
                _createdContainerIds.Add(container.Id);
            }
            catch ( InvalidOperationException )
            {
                // The container was never created, so there is nothing to remove.
            }
        }

        /// <summary>
        /// Returns whether the container carries the harness's label, which means that an earlier run of the harness
        /// created it.
        /// </summary>
        /// <param name="container" />
        private static bool IsHarnessContainer( ContainerListResponse container ) =>
            container?.Labels is { } labels && labels.TryGetValue(EnvironmentLabelKey, out var value) && string.Equals(value, EnvironmentLabelValue, StringComparison.Ordinal);

        /// <summary>
        /// Returns whether the container exists and is running.
        /// </summary>
        /// <param name="container" />
        private static bool IsRunning( ContainerListResponse container ) =>
            string.Equals(container?.State, "running", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Returns the host name of the model service's inference endpoint for the configured region, which the database
        /// sends to the gateway.
        /// </summary>
        /// <param name="settings" />
        private static string GetInferenceHost( IntegrationTestAiQuerySettings settings ) =>
            $@"inference.generativeai.{settings.Region}.oci.oraclecloud.com";

        /// <summary>
        /// Reads one of the scripts copied to the output directory, with Unix line endings. A script checked out on
        /// Windows can carry Windows line endings, which a shell in a Linux container would reject.
        /// </summary>
        /// <param name="folder">The folder under <c>Scripts/Ai</c>.</param>
        /// <param name="name" />
        private static byte[] ReadScript( string folder, string name )
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var text      = File.ReadAllText(Path.Combine(directory, "Scripts", "Ai", folder, name));

            return new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes(text.Replace("\r\n", "\n"));
        }

        #endregion

        #region Essbase

        /// <summary>
        /// Looks at Essbase before anything is started: the application must exist, and what is already there of the AI
        /// connection decides what this run will own.
        /// </summary>
        /// <returns>The name of the AI connection the application already uses, or <see langword="null" /> if it has none.</returns>
        private static async Task<string> InspectEssbaseAsync( IntegrationTestAiQuerySettings settings, CancellationToken cancellationToken )
        {
            var application = settings.ApplicationName;

            using var http = CreateEssbaseClient();

            // An application that already has an AI connection was set up by somebody else, either an earlier run or a
            // persistent environment.
            var associated = await SendAsync(http, HttpMethod.Get, $@"ai/connection?application={Uri.EscapeDataString(application)}", body: null, cancellationToken);

            if ( associated.Status == HttpStatusCode.OK && associated.Body.Trim() is { Length: > 0 } associatedName )
                return associatedName;

            // The gateway associates the AI connection with, and vectorizes, an existing application.
            Require((await SendAsync(http, HttpMethod.Get, $@"applications/{Uri.EscapeDataString(application)}", body: null, cancellationToken)),
                $@"Find the '{application}' application. The AI query tests expect the tests that create the sample applications to have run first");

            // From here on, what the gateway creates is this run's to remove, even if building stops half way. Connections
            // that are already there are somebody else's.
            _ownsEssbaseConfiguration  = true;
            _createdDatabaseConnection = (await SendAsync(http, HttpMethod.Get, $@"connections/{DatabaseConnectionName}", body: null, cancellationToken)).Status != HttpStatusCode.OK;
            _createdAiConnection       = (await SendAsync(http, HttpMethod.Get, $@"connections/{AiConnectionName}", body: null, cancellationToken)).Status != HttpStatusCode.OK;

            return null;
        }

        /// <summary>
        /// Creates a client for the Essbase REST API, signed in as the configured service administrator.
        /// </summary>
        internal static HttpClient CreateEssbaseClient()
        {
            var connection = IntegrationTestFactory.GetEssConnection();
            var http       = new HttpClient() { BaseAddress = new Uri($@"{connection.Server.TrimEnd('/')}/rest/v1/"), Timeout = TimeSpan.FromMinutes(5) };

            http.DefaultRequestHeaders.Authorization = connection is { AccessToken.Length: > 0 }
                ? new AuthenticationHeaderValue("Bearer", connection.AccessToken)
                : new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($@"{connection.Username}:{connection.Password}")));

            return http;
        }

        /// <summary>
        /// Takes the AI connection out of Essbase: dissociates it from the application, then deletes the AI connection and
        /// the database connection it uses, each only if this run created it.
        /// </summary>
        /// <remarks>
        /// Best effort. Dissociating starts a job in Essbase that deletes the outline's vectors from the database, so
        /// the database has to be left in place until that has finished.
        /// </remarks>
        private static async Task TearDownEssbaseAsync()
        {
            using var cancellation = new CancellationTokenSource(TimeSpan.FromMinutes(3));
            using var http         = CreateEssbaseClient();

            var application = Uri.EscapeDataString(_settings.ApplicationName);

            await SendAsync(http, HttpMethod.Delete, $@"ai/applications/{application}/connection", body: null, cancellation.Token);

            // The association disappears once the job that deletes the vectors has finished.
            var deadline = DateTime.UtcNow.AddMinutes(2);

            while ( DateTime.UtcNow < deadline )
            {
                var associated = await SendAsync(http, HttpMethod.Get, $@"ai/connection?application={application}", body: null, cancellation.Token);

                if ( associated.Status != HttpStatusCode.OK || string.IsNullOrWhiteSpace(associated.Body) )
                    break;

                await Task.Delay(TimeSpan.FromSeconds(2), cancellation.Token);
            }

            if ( _createdAiConnection )
                await SendAsync(http, HttpMethod.Delete, $@"connections/{AiConnectionName}", body: null, cancellation.Token);

            if ( _createdDatabaseConnection )
                await SendAsync(http, HttpMethod.Delete, $@"connections/{DatabaseConnectionName}", body: null, cancellation.Token);

            Log(_sink, "The AI connection has been removed from Essbase.");
        }

        /// <summary>
        /// Sends a request to the Essbase REST API and returns the status and body of the response.
        /// </summary>
        private static async Task<(HttpStatusCode Status, string Body)> SendAsync( HttpClient http, HttpMethod method, string path, object body, CancellationToken cancellationToken )
        {
            using var request = new HttpRequestMessage(method, path);

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if ( body is not null )
                request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            using var response = await http.SendAsync(request, cancellationToken);

            return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Throws if the response is not a success.
        /// </summary>
        private static void Require( (HttpStatusCode Status, string Body) response, string step )
        {
            if ( (int)response.Status is < 200 or >= 300 )
                throw new Exception($@"{step} failed: HTTP {(int)response.Status}. {response.Body}");
        }

        #endregion

        #region Logging

        /// <summary>
        /// Writes a diagnostic message to the test run.
        /// </summary>
        private static void Log( IMessageSink sink, string message ) => sink?.OnMessage(new DiagnosticMessage(message));

        #endregion
    }
}
