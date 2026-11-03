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
    /// <c>Scripts/Oci/gateway/gateway.py</c>). It answers in canned mode: fixed answers from <c>answers.json</c> and
    /// deterministic embeddings, so that the tests can assert what Essbase makes of them. To give a new test a
    /// deterministic answer, add an entry to <c>answers.json</c>.
    /// </para>
    /// <para>
    /// The gateway is the harness's OCI gateway (<see cref="IntegrationTestOciGateway" />), which the assembly fixture
    /// starts before Essbase: the inference host name is an alias of its container on the test network, and it issues a
    /// certificate for that name from a certificate authority of its own, into a volume the database reads; the
    /// database's startup hook (<c>Scripts/Ai/database/startdb</c>) trusts the authority and prepares the database.
    /// Once the database listens, the gateway sets Essbase up as a user would - a connection to the database, chat and
    /// vector credentials, a GenAI connection that uses them, an association between that connection and the
    /// application, and the job that vectorizes the application's outline - and logs <c>Essbase AI configured</c>. The
    /// application has to exist for the last two steps, so the database is started when the AI tests start rather
    /// than with the assembly fixture - by then the tests that create the sample applications have run.
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
        internal static readonly TimeSpan EnvironmentTimeout = TimeSpan.FromMinutes(30);

        /// <summary>
        /// What the gateway logs once Essbase is ready to answer questions.
        /// </summary>
        private const string ConfiguredMessage = @"Essbase AI configured";

        private const string ScriptDirectory   = @"/opt/ai-scripts";
        private const string StartDbDirectory  = @"/container-entrypoint-startdb.d";

        private const string DatabaseAdminPassword = @"AiSys_Passw0rd1";
        internal const string DatabaseUser         = @"ESSAI";
        internal const string DatabaseUserPassword = @"AiUser_Passw0rd1";

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

        private static readonly SemaphoreSlim _gate = new(1, 1);

        // The containers this run created, by ID. Only these are removed when the run ends.
        private static readonly List<string> _createdContainerIds = new();

        private static bool                           _ready;
        private static bool                           _keep;
        private static bool                           _ownsEssbaseConfiguration;
        private static bool                           _createdDatabaseConnection;
        private static bool                           _createdAiConnection;
        private static string                         _databaseName;
        private static IntegrationTestAiQuerySettings _settings;
        private static IMessageSink                   _sink;

        #endregion

        #region Public Properties

        /// <summary>
        /// Whether the AI gateway answers with the canned answers in <c>answers.json</c>, so that a test can assert what
        /// Essbase makes of them: always when the harness started the gateway, and when it adopted a running one that was
        /// started in canned mode.
        /// </summary>
        public static bool ServesCannedAnswers => IntegrationTestOciGateway.ServesCannedAnswers;

        /// <summary>
        /// The name of the AI database container for the given Essbase container profile: <c>&lt;essbase container&gt;-ai-database</c>.
        /// </summary>
        /// <param name="profile" />
        public static string DatabaseContainerNameFor( EssbaseContainerProfile profile ) => $@"{profile.ContainerName}-ai-database";

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
                _databaseName = DatabaseContainerNameFor(profile);

                if (!IntegrationTestOciGateway.IsRunning)
                    throw new InvalidOperationException("The AI query environment needs the OCI gateway, which the assembly fixture starts in Managed mode.");

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
                        // The gateway configures Essbase whenever the database comes up: every cube, or only those in its AI_APPLICATIONS.
                        throw new InvalidOperationException(
                            $@"The AI database '{_databaseName}' and the OCI gateway '{IntegrationTestOciGateway.ContainerName}' are running, but '{settings.ApplicationName}' has no AI connection. " +
                            $@"Restart '{IntegrationTestOciGateway.ContainerName}' so that it configures it (with an empty AI_APPLICATIONS, or one that includes '{settings.ApplicationName}.{settings.CubeName}'), or remove the database so that the harness starts its own.");
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
                if (!_keep && _databaseName is not null)
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
                _ownsEssbaseConfiguration  = false;
                _createdDatabaseConnection = false;
                _createdAiConnection       = false;
                _databaseName              = null;
                _ready                     = false;
            }
        }
        #endregion

        #region Containers

        /// <summary>
        /// Starts the AI database, unless it is already running. The gateway it calls is the shared OCI gateway.
        /// </summary>
        /// <remarks>
        /// Only a container of the harness's own (one that carries <see cref="EnvironmentLabelKey" />) is ever removed to
        /// make room for a new one. If a stopped container the harness did not create is in the way, this throws before
        /// anything is touched.
        /// </remarks>
        /// <returns><see langword="true" /> if the database was started, <see langword="false" /> if it was already running.</returns>
        /// <exception cref="InvalidOperationException">A container that the harness did not create exists and is not running.</exception>
        private static async Task<bool> StartContainersAsync( IntegrationTestAiQuerySettings settings, EssbaseContainerProfile profile, IMessageSink sink, CancellationToken cancellationToken )
        {
            using var client = IntegrationTestFactory.GetDockerClient();

            var all      = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true }, cancellationToken);
            var database = all.FirstOrDefault(container => container?.Names?.Any(name => string.Equals(_databaseName, name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);

            // A running database was made by somebody, with the gateway it trusts; it can be used as it is and is not this
            // run's to remove, whoever started it: only the containers this run creates are removed when it ends.
            if (IsRunning(database))
            {
                Log(sink, $@"Using the running AI database '{_databaseName}' with the OCI gateway '{IntegrationTestOciGateway.ContainerName}' ({(IntegrationTestOciGateway.ServesCannedAnswers ? "canned answers" : "the gateway's models")}).");
                return false;
            }

            // A stopped database is only the harness's to remove if the harness made it.
            if (database is not null)
            {
                if (!IsHarnessContainer(database))
                    throw new InvalidOperationException(
                        $@"The AI database '{_databaseName}' exists but is not running ({database.State}), and it was not created by the test harness, which will not remove or start containers it did not create. " +
                        $@"Start it yourself, or remove it, or set AiQuery:Mode to External so that the harness leaves the AI environment to you.");

                await client.Containers.RemoveContainerAsync(database.ID, new ContainerRemoveParameters() { Force = true }, cancellationToken);
            }

            // Unless the environment is to be kept, the test framework's cleanup removes the container when the test
            // process exits, even if the run never reached its own cleanup. The database resolves the inference host name
            // to the OCI gateway through the gateway's network alias, and trusts the authority in the gateway's volume.
            var databaseContainer = new ContainerBuilder(settings.DatabaseImage)
                .WithName(_databaseName)
                .WithLabel(EnvironmentLabelKey, EnvironmentLabelValue)
                .WithNetwork(profile.Network)
                .WithCleanUp(!_keep)
                .WithEnvironment("ORACLE_PASSWORD", DatabaseAdminPassword)
                .WithEnvironment("APP_USER", DatabaseUser)
                .WithEnvironment("APP_USER_PASSWORD", DatabaseUserPassword)
                .WithVolumeMount(IntegrationTestOciGateway.CertificateVolumeName, IntegrationTestOciGateway.CertificateMountPath, AccessMode.ReadOnly)
                .WithResourceMapping(ReadScript("database", "setup-ai-database.sh"), $@"{ScriptDirectory}/setup-ai-database.sh", fileMode: ExecutableMode)
                .WithResourceMapping(ReadScript(Path.Combine("database", "startdb"), "01-essbase-ai.sh"), $@"{StartDbDirectory}/01-essbase-ai.sh", fileMode: ExecutableMode)
                .Build() as DockerContainer;

            Log(sink, $@"Starting the AI database '{_databaseName}' from '{settings.DatabaseImage}'.");

            try
            {
                await databaseContainer.StartAsync(cancellationToken);
            }
            finally
            {
                RecordCreated(databaseContainer);
            }

            // The gateway configures Essbase AI once the database listens, and logs that it is configured only once the
            // database is ready and the outline vectorized.
            if (!await IntegrationTestOciGateway.WaitForLogAsync(ConfiguredMessage, EnvironmentTimeout, cancellationToken))
                throw new TimeoutException($@"The OCI gateway did not report '{ConfiguredMessage}' within {EnvironmentTimeout.TotalMinutes:0} minutes.");

            Log(sink, $@"The OCI gateway configured '{settings.ApplicationName}.{settings.CubeName}'.");

            return true;
        }

        /// <summary>
        /// Removes the containers this run created.
        /// </summary>
        /// <remarks>
        /// The containers are removed by ID, never by name: a container of the same name that somebody else started, or
        /// that was already there when the run began, is not this run's to remove.
        /// </remarks>
        private static async Task RemoveContainersAsync()
        {
            if (_createdContainerIds.Count == 0)
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
                    // The other containers are still this run's to remove.
                    Log(_sink, $@"Unable to remove the AI query container '{id}': {error.Message}");
                }
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
        /// Reads one of the scripts copied to the output directory, with Unix line endings. A script checked out on
        /// Windows can carry Windows line endings, which a shell in a Linux container would reject.
        /// </summary>
        /// <param name="folder">The folder under <c>Scripts/Ai</c>.</param>
        /// <param name="name" />
        private static byte[] ReadScript( string folder, string name ) => IntegrationTestOciGateway.ReadScript("Ai", folder, name);

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
