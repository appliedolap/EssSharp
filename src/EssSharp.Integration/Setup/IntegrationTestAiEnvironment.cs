using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Xunit.Sdk;
using Xunit.v3;

namespace EssSharp.Integration.Setup
{
    /// <summary>
    /// Builds the environment the AI query tests need: an Essbase AI connection, without an OCI tenancy.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Essbase does not call Oracle's cloud itself. It asks an Oracle AI Database to, through <c>DBMS_CLOUD_AI</c>
    /// (chat) and <c>DBMS_VECTOR_CHAIN</c> (embeddings), and the database calls the OCI Generative AI endpoint for the
    /// region. In <see cref="IntegrationTestAiQueryMode.Managed" /> mode the harness therefore starts two containers
    /// beside the Essbase one: an Oracle AI Database, and a fake OCI Generative AI service that answers as the
    /// region's endpoint does (see <c>Scripts/AiQuery/fake-oci-genai.py</c>). The database trusts the fake service's
    /// certificate, because the harness issues it from a throwaway authority that the database is told to trust.
    /// </para>
    /// <para>
    /// Essbase is then set up as a user would set it up: a connection to the database, chat and vector credentials,
    /// a GenAI connection that uses them, an association between that connection and the application, and a job that
    /// vectorizes the application's outline. The application has to exist for the last two steps, so this runs when the
    /// AI tests start rather than with the assembly fixture - by then the tests that create the sample applications have
    /// run.
    /// </para>
    /// <para>
    /// The fake service and the database share a docker network of their own, in which the fake service answers to the
    /// OCI host name. The alias is scoped to that network, so two AI environments on the same network - for two Essbase
    /// versions, say - cannot answer each other's requests. The database is also on the Essbase container's network, so
    /// that Essbase can reach it.
    /// </para>
    /// <para>
    /// The pair may already be running: a previous run that kept its environment, or a tool that starts it as persistent
    /// containers, may have left it. A running pair is adopted as it is, the way the Essbase container is.
    /// </para>
    /// <para>
    /// When the run ends, it removes what it created and nothing else, whoever else is using the containers. The AI
    /// connection is taken out of Essbase if the run configured it there (unless the run started the Essbase container,
    /// which goes with it), and then the containers and the network the run created are removed. The order matters: an
    /// Essbase server that outlived its database would be left with an AI connection, and a pool of connections,
    /// pointing at nothing. Containers and a network that were already there, and an AI connection that was already
    /// associated with the application, are left alone. A container the run did not create is never removed or
    /// started, even when it is in the way. <see cref="IntegrationTestAiQuerySettings.KeepEnvironment" /> leaves it all
    /// in place instead, which makes the next run faster; every step checks before it acts, so a later run finds the
    /// pair and reuses it.
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
        /// The longest the whole environment may take to build. Pulling the images and starting the database take
        /// minutes on a cold start; a build that takes this long is stuck, and the tests should fail with a message
        /// instead of waiting for the limit of the job that runs them.
        /// </summary>
        private static readonly TimeSpan EnvironmentTimeout = TimeSpan.FromMinutes(30);

        /// <summary>
        /// How long to wait for the vectorize job to finish.
        /// </summary>
        private static readonly TimeSpan VectorizeTimeout = TimeSpan.FromMinutes(15);

        private const string ScriptDirectory      = @"/opt/ai-scripts";
        private const string FakeServiceDirectory = @"/opt/fake-oci";

        private const string DatabaseAdminPassword = @"AiSys_Passw0rd1";
        private const string DatabaseUser          = @"ESSAI";
        private const string DatabaseUserPassword  = @"AiUser_Passw0rd1";
        private const string DatabaseService       = @"FREEPDB1";
        private const ushort DatabasePort          = 1521;

        private const string DatabaseConnectionName = @"ess_ai_database";
        private const string ChatCredentialName     = @"ess_ai_chat";
        private const string VectorCredentialName   = @"ess_ai_vector";
        private const string AiConnectionName       = @"ess_ai_genai";

        // Everything the harness creates carries this label, so that a leftover of its own can be told from a container
        // or a network that somebody else started.
        private const string EnvironmentLabelKey   = @"com.appliedolap.essharp.integration-tests";
        private const string EnvironmentLabelValue = @"ai-environment";

        // The OCIDs only have to match the patterns Essbase validates; nothing ever looks them up.
        private const string UserOcid        = @"ocid1.user.oc1..aaaaaaaaessharpintegrationtests";
        private const string TenancyOcid     = @"ocid1.tenancy.oc1..aaaaaaaaessharpintegrationtests";
        private const string CompartmentOcid = @"ocid1.compartment.oc1..aaaaaaaaessharpintegrationtests";

        // The Unix permissions of the files copied into the containers: rw-r--r-- and rwxr-xr-x.
        private const UnixFileModes ReadableMode   = UnixFileModes.UserRead | UnixFileModes.UserWrite | UnixFileModes.GroupRead | UnixFileModes.OtherRead;
        private const UnixFileModes ExecutableMode = ReadableMode | UnixFileModes.UserExecute | UnixFileModes.GroupExecute | UnixFileModes.OtherExecute;

        private static readonly SemaphoreSlim _gate = new(1, 1);

        // The containers and the network this run created, by ID. Only these are removed when the run ends.
        private static readonly List<string> _createdContainerIds = new();

        private static bool            _ready;
        private static bool            _keep;
        private static bool            _ownsEssbaseConfiguration;
        private static bool            _createdDatabaseConnection;
        private static bool            _createdAiConnection;
        private static string          _fakeServiceName;
        private static string          _databaseName;
        private static string          _networkName;
        private static string          _databaseContainerId;
        private static string          _createdNetworkId;
        private static IntegrationTestAiQuerySettings _settings;
        private static IMessageSink    _sink;

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
                _settings        = settings;
                _sink            = sink;
                _keep            = settings.KeepEnvironment;
                _fakeServiceName = $@"{profile.ContainerName}-ai-service";
                _databaseName    = $@"{profile.ContainerName}-ai-database";
                _networkName     = $@"{profile.ContainerName}-ai";

                // Every wait below - the image pulls, the container wait strategies, the database setup script, the
                // Essbase configuration - takes this token, so one timeout bounds them all.
                using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                timeout.CancelAfter(EnvironmentTimeout);

                try
                {
                    var created = await StartContainersAsync(settings, profile, sink, timeout.Token);

                    await ConfigureDatabaseAsync(sink, timeout.Token);
                    await ConfigureEssbaseAsync(settings, profile, created, sink, timeout.Token);
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
        /// connection first, then the containers and the network.
        /// </summary>
        /// <remarks>
        /// Containers, a network and an AI connection that were already there are left alone. This never throws. It
        /// runs before the Essbase containers are removed, and a failure here must not stop that; the test framework's
        /// cleanup removes whatever is left when the process exits.
        /// </remarks>
        public static async Task DisposeAsync()
        {
            try
            {
                if ( !_keep && _fakeServiceName is not null )
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
                _databaseContainerId       = null;
                _createdContainerIds.Clear();
                _createdNetworkId          = null;
                _ownsEssbaseConfiguration  = false;
                _createdDatabaseConnection = false;
                _createdAiConnection       = false;
                _fakeServiceName           = null;
                _databaseName              = null;
                _networkName               = null;
                _ready                     = false;
            }
        }
        #endregion

        #region Containers

        /// <summary>
        /// Starts the fake OCI service and the database, unless both of them are already running.
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
            var host = GetInferenceHost(settings);

            using var client = IntegrationTestFactory.GetDockerClient();

            var all         = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true }, cancellationToken);
            var fakeService = all.FirstOrDefault(container => container?.Names?.Any(name => string.Equals(_fakeServiceName, name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);
            var database    = all.FirstOrDefault(container => container?.Names?.Any(name => string.Equals(_databaseName, name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);

            // The two containers are a pair: the database trusts the certificate authority whose certificate the fake
            // service presents. If both are running, somebody made that pair, and it can be used as it is. It is not this
            // run's to remove, whoever started it: only the containers this run creates are removed when it ends.
            if ( IsRunning(fakeService) && IsRunning(database) )
            {
                _databaseContainerId = database.ID;
                Log(sink, $@"Using the running AI query containers '{_fakeServiceName}' and '{_databaseName}'.");
                return false;
            }

            // Otherwise start again. A container left behind without its partner has no matching certificates, but it is
            // only the harness's to remove if the harness made it. Check them all before removing any.
            var strays  = new[] { fakeService, database }.Where(container => container is not null).ToList();
            var foreign = strays.Where(container => !IsHarnessContainer(container)).ToList();

            if ( foreign.Count > 0 )
            {
                // Say what each of the two containers is, including one that does not exist, so that a lone container does
                // not read as a contradiction.
                var states = string.Join(", ", new[] { (Name: _fakeServiceName, Container: fakeService), (Name: _databaseName, Container: database) }.Select(item => $@"'{item.Name}' ({item.Container?.State ?? "missing"})"));
                var names  = string.Join(" and ", foreign.Select(container => $@"'{container.Names?.FirstOrDefault()?.Trim('/')}'"));
                var verb   = foreign.Count == 1 ? "was" : "were";

                throw new InvalidOperationException(
                    $@"The AI query containers are not both running: {states}. " +
                    $@"{names} {verb} not created by the test harness, and the harness will not remove or start containers it did not create. " +
                    $@"Start them yourself so that both are running, or remove them, or set AiQuery:Mode to External so that the harness leaves the AI environment to you.");
            }

            foreach ( var stray in strays )
                await client.Containers.RemoveContainerAsync(stray.ID, new ContainerRemoveParameters() { Force = true }, cancellationToken);

            // The fake service answers to the OCI host name on a network of its own, so that it cannot be confused with
            // the fake service of another AI environment on the Essbase container's network.
            _createdNetworkId ??= await EnsureNetworkAsync(client, _networkName, cancellationToken);

            using var authority = TestCertificateAuthority.Create("EssSharp integration test CA");

            var (certificate, privateKey) = authority.IssueServerCertificate(host);

            // Unless the environment is to be kept, the test framework's cleanup removes the containers when the test
            // process exits, even if the run never reached its own cleanup.
            var fakeServiceContainer = new ContainerBuilder(settings.FakeServiceImage)
                .WithName(_fakeServiceName)
                .WithLabel(EnvironmentLabelKey, EnvironmentLabelValue)
                .WithNetwork(_networkName)
                .WithNetworkAliases(host)
                .WithCleanUp(!_keep)
                .WithResourceMapping(ReadScript("fake-oci-genai.py"), $@"{FakeServiceDirectory}/fake-oci-genai.py", fileMode: ReadableMode)
                .WithResourceMapping(Encoding.UTF8.GetBytes(certificate), $@"{FakeServiceDirectory}/server.crt", fileMode: ReadableMode)
                .WithResourceMapping(Encoding.UTF8.GetBytes(privateKey), $@"{FakeServiceDirectory}/server.key", fileMode: ReadableMode)
                .WithCommand("python", $@"{FakeServiceDirectory}/fake-oci-genai.py")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("fake OCI GenAI listening"))
                .Build() as DockerContainer;

            var databaseContainer = new ContainerBuilder(settings.DatabaseImage)
                .WithName(_databaseName)
                .WithLabel(EnvironmentLabelKey, EnvironmentLabelValue)
                .WithNetwork(profile.Network)
                .WithNetwork(_networkName)
                .WithCleanUp(!_keep)
                .WithEnvironment("ORACLE_PASSWORD", DatabaseAdminPassword)
                .WithEnvironment("APP_USER", DatabaseUser)
                .WithEnvironment("APP_USER_PASSWORD", DatabaseUserPassword)
                .WithResourceMapping(ReadScript("setup-ai-database.sh"), $@"{ScriptDirectory}/setup-ai-database.sh", fileMode: ExecutableMode)
                .WithResourceMapping(Encoding.UTF8.GetBytes(authority.CertificatePem), $@"{ScriptDirectory}/ca.crt", fileMode: ReadableMode)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("DATABASE IS READY TO USE!"))
                .Build() as DockerContainer;

            Log(sink, $@"Starting the AI query containers '{_fakeServiceName}' (as {host}) and '{_databaseName}' from '{settings.DatabaseImage}'.");

            try
            {
                await Task.WhenAll(fakeServiceContainer.StartAsync(cancellationToken), databaseContainer.StartAsync(cancellationToken));
            }
            finally
            {
                // These are this run's to remove, even if one of them did not become ready.
                RecordCreated(fakeServiceContainer);
                RecordCreated(databaseContainer);
            }

            _databaseContainerId = databaseContainer.Id;

            return true;
        }

        /// <summary>
        /// Creates the docker network the fake service and the database share, unless it already exists.
        /// </summary>
        /// <remarks>
        /// The network carries the harness's label. Unless the environment is to be kept, it also carries the labels the
        /// test framework's cleanup looks for, so that it is removed with the containers if the run is stopped before it
        /// can remove them itself.
        /// </remarks>
        /// <returns>The ID of the network if it was created, <see langword="null" /> if it was already there and is not this run's to remove.</returns>
        private static async Task<string> EnsureNetworkAsync( Docker.DotNet.DockerClient client, string name, CancellationToken cancellationToken )
        {
            var networks = await client.Networks.ListNetworksAsync(new NetworksListParameters() { Filters = new Dictionary<string, IDictionary<string, bool>>() { ["name"] = new Dictionary<string, bool>() { [name] = true } } }, cancellationToken);

            if ( networks.Any(network => string.Equals(network.Name, name, StringComparison.OrdinalIgnoreCase)) )
                return null;

            var labels = new Dictionary<string, string>() { [EnvironmentLabelKey] = EnvironmentLabelValue };

            if ( !_keep )
            {
                labels["org.testcontainers"]            = "true";
                labels["org.testcontainers.session-id"] = ResourceReaper.DefaultSessionId.ToString("D");
            }

            var created = await client.Networks.CreateNetworkAsync(new NetworksCreateParameters()
            {
                Name   = name,
                Driver = "bridge",
                Labels = labels
            }, cancellationToken);

            return created.ID;
        }

        /// <summary>
        /// Removes the containers this run created, and then the network if this run created it.
        /// </summary>
        /// <remarks>
        /// They are removed by ID, never by name: a container or network of the same name that somebody else started, or
        /// that was already there when the run began, is not this run's to remove.
        /// </remarks>
        private static async Task RemoveContainersAsync()
        {
            if ( _createdContainerIds.Count == 0 && _createdNetworkId is null )
                return;

            using var cancellation = new CancellationTokenSource(TimeSpan.FromMinutes(2));
            using var client       = IntegrationTestFactory.GetDockerClient();

            foreach ( var id in _createdContainerIds )
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
                    // The other containers and the network are still this run's to remove.
                    Log(_sink, $@"Unable to remove the AI query container '{id}': {error.Message}");
                }
            }

            if ( _createdNetworkId is null )
                return;

            try
            {
                await client.Networks.DeleteNetworkAsync(_createdNetworkId, cancellation.Token);
            }
            catch ( Exception )
            {
                // A network that is already gone needs no removing.
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
        /// Returns the host name of the OCI Generative AI inference endpoint for the configured region.
        /// </summary>
        /// <param name="settings" />
        private static string GetInferenceHost( IntegrationTestAiQuerySettings settings ) =>
            $@"inference.generativeai.{settings.Region}.oci.oraclecloud.com";

        /// <summary>
        /// Reads one of the scripts copied to the output directory, with Unix line endings. A script checked out on
        /// Windows can carry Windows line endings, which a shell in a Linux container would reject.
        /// </summary>
        /// <param name="name" />
        private static byte[] ReadScript( string name )
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var text      = File.ReadAllText(Path.Combine(directory, "Scripts", "AiQuery", name));

            return new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes(text.Replace("\r\n", "\n"));
        }

        #endregion

        #region Database

        /// <summary>
        /// Makes the database trust the test certificate authority and runs the setup script in it.
        /// </summary>
        private static async Task ConfigureDatabaseAsync( IMessageSink sink, CancellationToken cancellationToken )
        {
            // The vector functions trust certificates from the operating system's store.
            var (trustDetails, trustOutput) = await IntegrationTestFactory.ExecAsync(_databaseContainerId,
                new[] { "/bin/sh", "-c", $@"cp {ScriptDirectory}/ca.crt /etc/pki/ca-trust/source/anchors/essharp-test-ca.crt && update-ca-trust extract 2>&1" },
                user: "root", cancellationToken);

            if ( trustDetails?.ExitCode is not 0 )
                throw new Exception($@"Unable to add the test certificate authority to the AI database's trust store (exit code {trustDetails?.ExitCode}).{Environment.NewLine}{trustOutput}");

            var (details, output) = await IntegrationTestFactory.ExecAsync(_databaseContainerId, new[] { "/bin/bash", $@"{ScriptDirectory}/setup-ai-database.sh" }, cancellationToken);

            if ( details?.ExitCode is not 0 )
                throw new Exception($@"The AI database setup script failed (exit code {details?.ExitCode}).{Environment.NewLine}{output}");

            Log(sink, "The AI database is configured.");
        }

        #endregion

        #region Essbase

        /// <summary>
        /// Gives Essbase an AI connection that uses the database and associates it with the application.
        /// </summary>
        private static async Task ConfigureEssbaseAsync( IntegrationTestAiQuerySettings settings, EssbaseContainerProfile profile, bool containersCreated, IMessageSink sink, CancellationToken cancellationToken )
        {
            var application = settings.ApplicationName;
            var cube        = settings.CubeName;

            using var http = CreateEssbaseClient();

            // An application that already has an AI connection was set up by somebody else, either an earlier run or a
            // persistent environment.
            var associated = await SendAsync(http, HttpMethod.Get, $@"ai/connection?application={Uri.EscapeDataString(application)}", body: null, cancellationToken);

            if ( associated.Status == HttpStatusCode.OK && associated.Body.Trim() is { Length: > 0 } associatedName )
            {
                // That earlier run's database, with its credentials and the vectors of the outline, is only there if its
                // containers still are. A database built just now is empty, and Essbase cannot be pointed back at it
                // from outside: its connection pool still holds connections to the database that was removed.
                if ( containersCreated )
                    throw new InvalidOperationException(
                        $@"'{application}' still uses the AI connection '{associatedName}', which an earlier run set up against AI query containers that no longer exist, so it points at a database that has been rebuilt empty. " +
                        $@"Remove the AI connection (dissociate it from '{application}' and delete it; restart Essbase first if it reports 'UCP-29: Failed to get a connection') or use a new Essbase container, then run the tests again. " +
                        $@"A run removes the AI connection it created itself when it ends, so this only happens if an earlier run was stopped before it could.");

                // An association somebody else made - an earlier run, or a persistent environment - is not this run's to undo.
                Log(sink, $@"'{application}' already uses the AI connection '{associatedName}'.");
                return;
            }

            // The AI connection is associated with, and vectorizes, an existing application.
            Require((await SendAsync(http, HttpMethod.Get, $@"applications/{Uri.EscapeDataString(application)}", body: null, cancellationToken)),
                $@"Find the '{application}' application. The AI query tests expect the tests that create the sample applications to have run first");

            // From here on, what is created is this run's to remove, even if building stops half way.
            _ownsEssbaseConfiguration = true;

            // Essbase connects to the database by its container name, over the shared docker network.
            var databaseHost = _databaseName;

            if ( (await SendAsync(http, HttpMethod.Get, $@"connections/{DatabaseConnectionName}", body: null, cancellationToken)).Status != HttpStatusCode.OK )
            {
                Require((await SendAsync(http, HttpMethod.Post, "connections", new Dictionary<string, object>()
                {
                    ["name"]        = DatabaseConnectionName,
                    ["description"] = "The Oracle AI Database the AI query tests use",
                    ["type"]        = "DB",
                    ["subtype"]     = "ORACLE",
                    ["host"]        = databaseHost,
                    ["port"]        = (int)DatabasePort,
                    ["service"]     = DatabaseService,
                    ["user"]        = DatabaseUser,
                    ["password"]    = DatabaseUserPassword,
                }, cancellationToken)), "Create the AI database connection");

                _createdDatabaseConnection = true;
            }

            Require((await SendAsync(http, HttpMethod.Post, $@"connections/{DatabaseConnectionName}/actions/test", body: null, cancellationToken)), "Connect to the AI database");

            // The credentials hold a key that signs each request to OCI. The fake service never checks the signature, so
            // a key made for the occasion will do, but it has to be a real key: the database parses it.
            using var rsa = RSA.Create(2048);

            var privateKey  = string.Concat(rsa.ExportPkcs8PrivateKeyPem().Split('\n').Where(line => !line.StartsWith("-----")).Select(line => line.Trim()));
            var fingerprint = string.Join(":", MD5.HashData(rsa.ExportSubjectPublicKeyInfo()).Select(value => value.ToString("x2")));
            var embedUrl    = $@"https://{GetInferenceHost(settings)}/20231130/actions/embedText";

            RequireOrExists((await SendAsync(http, HttpMethod.Post, $@"ai/dbconnection/{DatabaseConnectionName}/chat/credential/signingkey/{ChatCredentialName}", new Dictionary<string, object>()
            {
                ["user_ocid"]        = UserOcid,
                ["tenancy_ocid"]     = TenancyOcid,
                ["compartment_ocid"] = CompartmentOcid,
                ["private_key"]      = privateKey,
                ["fingerprint"]      = fingerprint,
                ["chat_model"]       = settings.ChatModel,
            }, cancellationToken)), "Create the chat credential");

            RequireOrExists((await SendAsync(http, HttpMethod.Post, $@"ai/dbconnection/{DatabaseConnectionName}/vector/credential/{VectorCredentialName}", new Dictionary<string, object>()
            {
                ["user_ocid"]        = UserOcid,
                ["tenancy_ocid"]     = TenancyOcid,
                ["compartment_ocid"] = CompartmentOcid,
                ["private_key"]      = privateKey,
                ["fingerprint"]      = fingerprint,
                ["url"]              = embedUrl,
                ["embed_model"]      = settings.EmbedModel,
            }, cancellationToken)), "Create the vector credential");

            if ( (await SendAsync(http, HttpMethod.Get, $@"connections/{AiConnectionName}", body: null, cancellationToken)).Status != HttpStatusCode.OK )
            {
                Require((await SendAsync(http, HttpMethod.Post, "connections", new Dictionary<string, object>()
                {
                    ["name"]               = AiConnectionName,
                    ["description"]        = "The AI connection the AI query tests use",
                    ["type"]               = "AI",
                    ["subtype"]            = "AI",
                    ["ai_connection"]      = DatabaseConnectionName,
                    ["chat_credential"]    = ChatCredentialName,
                    ["vector_credential"]  = VectorCredentialName,
                    ["chat_model"]         = settings.ChatModel,
                    ["oci_compartment_id"] = CompartmentOcid,
                    ["region"]             = settings.Region,
                    ["url"]                = embedUrl,
                    ["embed_model"]        = settings.EmbedModel,
                    ["transfer_timeout"]   = "3000",
                    ["oci_apiformat"]      = "GENERIC",
                }, cancellationToken)), "Create the AI connection");

                _createdAiConnection = true;
            }

            try
            {
                Require((await SendAsync(http, HttpMethod.Post, $@"ai/applications/{Uri.EscapeDataString(application)}/connection/{AiConnectionName}", body: null, cancellationToken)), $@"Associate the AI connection with '{application}'");

                await VectorizeOutlineAsync(http, application, cube, sink, cancellationToken);
            }
            catch ( Exception )
            {
                // An application that is associated but not vectorized would be taken for a finished setup by the next run.
                // Undo the association, on a fresh token because this may be a cancellation, and let the failure through.
                using var cleanup = new CancellationTokenSource(TimeSpan.FromMinutes(1));

                try
                {
                    await SendAsync(http, HttpMethod.Delete, $@"ai/applications/{Uri.EscapeDataString(application)}/connection", body: null, cleanup.Token);
                }
                catch ( Exception )
                {
                    // Nothing more can be done; the original failure is the one to report.
                }

                throw;
            }

            Log(sink, $@"Essbase is configured: '{application}' uses the AI connection '{AiConnectionName}'.");
        }

        /// <summary>
        /// Creates a client for the Essbase REST API, signed in as the configured service administrator.
        /// </summary>
        private static HttpClient CreateEssbaseClient()
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
        /// Vectorizes the outline of the application's cube, which AI query needs, and waits for the job to finish.
        /// </summary>
        private static async Task VectorizeOutlineAsync( HttpClient http, string application, string cube, IMessageSink sink, CancellationToken cancellationToken )
        {
            var started = await SendAsync(http, HttpMethod.Post, $@"ai/applications/{Uri.EscapeDataString(application)}/job/vectorize/databases/{Uri.EscapeDataString(cube)}", body: null, cancellationToken);

            Require(started, "Start the vectorize outline job");

            using var document = JsonDocument.Parse(started.Body);

            var jobId    = document.RootElement.GetProperty("job_ID").ToString();
            var deadline = DateTime.UtcNow.Add(VectorizeTimeout);

            while ( DateTime.UtcNow < deadline )
            {
                var job = await SendAsync(http, HttpMethod.Get, $@"jobs/{jobId}", body: null, cancellationToken);

                Require(job, $@"Read vectorize outline job {jobId}");

                using var status = JsonDocument.Parse(job.Body);

                // 100 is "in progress", 200 is "completed", 300 is "completed with warnings"; anything else is a failure.
                var statusCode = status.RootElement.GetProperty("statusCode").GetInt32();

                if ( statusCode is 200 or 300 )
                {
                    Log(sink, $@"Vectorize outline job {jobId} completed{(statusCode == 300 ? " with warnings" : string.Empty)}.");
                    return;
                }

                if ( statusCode != 100 )
                    throw new Exception($@"Vectorize outline job {jobId} failed: {job.Body}");

                await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
            }

            throw new TimeoutException($@"Vectorize outline job {jobId} did not finish within {VectorizeTimeout.TotalMinutes:0} minutes.");
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

        /// <summary>
        /// Throws if the response is not a success, unless Essbase is reporting that the object already exists.
        /// </summary>
        private static void RequireOrExists( (HttpStatusCode Status, string Body) response, string step )
        {
            if ( response.Body?.Contains("already exist", StringComparison.OrdinalIgnoreCase) is true )
                return;

            Require(response, step);
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
