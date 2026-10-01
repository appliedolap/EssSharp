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
    /// The containers share the Essbase container's lifetime. When the run starts the Essbase container, it removes
    /// the AI containers with it. When the Essbase container was already running, the AI containers are left running
    /// too (and are kept away from the test framework's own cleanup), because the Essbase server keeps its AI
    /// connection - and a pool of connections to the database - between runs, and a database that was rebuilt under it
    /// would not be found again. Every step checks before it acts, so a later run finds the pair and reuses it.
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

        // The OCIDs only have to match the patterns Essbase validates; nothing ever looks them up.
        private const string UserOcid        = @"ocid1.user.oc1..aaaaaaaaessharpintegrationtests";
        private const string TenancyOcid     = @"ocid1.tenancy.oc1..aaaaaaaaessharpintegrationtests";
        private const string CompartmentOcid = @"ocid1.compartment.oc1..aaaaaaaaessharpintegrationtests";

        // The Unix permissions of the files copied into the containers: rw-r--r-- and rwxr-xr-x.
        private const UnixFileModes ReadableMode   = UnixFileModes.UserRead | UnixFileModes.UserWrite | UnixFileModes.GroupRead | UnixFileModes.OtherRead;
        private const UnixFileModes ExecutableMode = ReadableMode | UnixFileModes.UserExecute | UnixFileModes.GroupExecute | UnixFileModes.OtherExecute;

        private static readonly SemaphoreSlim _gate = new(1, 1);

        private static bool            _ready;
        private static bool            _persist;
        private static string          _databaseContainerId;
        private static DockerContainer _databaseContainer;
        private static DockerContainer _fakeServiceContainer;

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

            await _gate.WaitAsync(cancellationToken);

            try
            {
                if ( _ready )
                    return;

                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                var created = await StartContainersAsync(settings, profile, sink, cancellationToken);

                await ConfigureDatabaseAsync(sink, cancellationToken);
                await ConfigureEssbaseAsync(settings, profile, created, sink, cancellationToken);

                Log(sink, $@"The AI query environment is ready ({stopwatch.Elapsed.TotalSeconds:0} s).");

                _ready = true;
            }
            finally
            {
                _gate.Release();
            }
        }

        /// <summary>
        /// Removes the containers this run started, unless the Essbase container was already running when the run
        /// started: the containers then stay with it.
        /// </summary>
        public static async Task DisposeAsync()
        {
            if ( !_persist )
            {
                var disposeFakeService = _fakeServiceContainer?.DisposeAsync().AsTask() ?? Task.CompletedTask;
                var disposeDatabase    = _databaseContainer?.DisposeAsync().AsTask() ?? Task.CompletedTask;

                try
                {
                    await Task.WhenAll(disposeFakeService, disposeDatabase);
                }
                catch ( Exception )
                {
                    // Removing these containers is best effort, and must not stop the Essbase containers being removed
                    // after them; the test framework's cleanup removes whatever is left when the process exits.
                }
            }

            _fakeServiceContainer = null;
            _databaseContainer    = null;
            _databaseContainerId  = null;
            _ready                = false;
        }

        #endregion

        #region Containers

        /// <summary>
        /// Starts the fake OCI service and the database, unless a previous run left both of them running.
        /// </summary>
        /// <returns><see langword="true" /> if the containers were started, <see langword="false" /> if they were already running.</returns>
        private static async Task<bool> StartContainersAsync( IntegrationTestAiQuerySettings settings, EssbaseContainerProfile profile, IMessageSink sink, CancellationToken cancellationToken )
        {
            var fakeServiceName = $@"{profile.ContainerName}-ai-service";
            var databaseName    = $@"{profile.ContainerName}-ai-database";
            var host            = GetInferenceHost(settings);

            using var client = IntegrationTestFactory.GetDockerClient();

            var all         = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true }, cancellationToken);
            var fakeService = all.FirstOrDefault(container => container?.Names?.Any(name => string.Equals(fakeServiceName, name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);
            var database    = all.FirstOrDefault(container => container?.Names?.Any(name => string.Equals(databaseName, name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);

            // The two containers are a pair: the database trusts the certificate authority whose certificate the fake
            // service presents. If both are still running, a previous run made that pair, and it can be used as it is.
            if ( IsRunning(fakeService) && IsRunning(database) )
            {
                _databaseContainerId = database.ID;
                Log(sink, $@"Using the running AI query containers '{fakeServiceName}' and '{databaseName}'.");
                return false;
            }

            // Otherwise start again. A container left behind without its partner has no matching certificates.
            foreach ( var stray in new[] { fakeService, database }.Where(container => container is not null) )
                await client.Containers.RemoveContainerAsync(stray.ID, new ContainerRemoveParameters() { Force = true }, cancellationToken);

            // Containers for an Essbase container that was already running must outlive this run, so the test framework's
            // cleanup must not reap them when the test process exits.
            _persist = !IntegrationTestFactory.EssbaseContainerStarted;

            using var authority = TestCertificateAuthority.Create("EssSharp integration test CA");

            var (certificate, privateKey) = authority.IssueServerCertificate(host);

            _fakeServiceContainer = new ContainerBuilder(settings.FakeServiceImage)
                .WithName(fakeServiceName)
                .WithNetwork(profile.Network)
                .WithNetworkAliases(host)
                .WithCleanUp(!_persist)
                .WithResourceMapping(ReadScript("fake-oci-genai.py"), $@"{FakeServiceDirectory}/fake-oci-genai.py", fileMode: ReadableMode)
                .WithResourceMapping(Encoding.UTF8.GetBytes(certificate), $@"{FakeServiceDirectory}/server.crt", fileMode: ReadableMode)
                .WithResourceMapping(Encoding.UTF8.GetBytes(privateKey), $@"{FakeServiceDirectory}/server.key", fileMode: ReadableMode)
                .WithCommand("python", $@"{FakeServiceDirectory}/fake-oci-genai.py")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("fake OCI GenAI listening"))
                .Build() as DockerContainer;

            _databaseContainer = new ContainerBuilder(settings.DatabaseImage)
                .WithName(databaseName)
                .WithNetwork(profile.Network)
                .WithCleanUp(!_persist)
                .WithEnvironment("ORACLE_PASSWORD", DatabaseAdminPassword)
                .WithEnvironment("APP_USER", DatabaseUser)
                .WithEnvironment("APP_USER_PASSWORD", DatabaseUserPassword)
                .WithResourceMapping(ReadScript("setup-ai-database.sh"), $@"{ScriptDirectory}/setup-ai-database.sh", fileMode: ExecutableMode)
                .WithResourceMapping(Encoding.UTF8.GetBytes(authority.CertificatePem), $@"{ScriptDirectory}/ca.crt", fileMode: ReadableMode)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("DATABASE IS READY TO USE!"))
                .Build() as DockerContainer;

            Log(sink, $@"Starting the AI query containers '{fakeServiceName}' (as {host}) and '{databaseName}' from '{settings.DatabaseImage}'.");

            await Task.WhenAll(_fakeServiceContainer.StartAsync(cancellationToken), _databaseContainer.StartAsync(cancellationToken));

            _databaseContainerId = _databaseContainer.Id;

            return true;
        }

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
            var connection  = IntegrationTestFactory.GetEssConnection();

            using var http = new HttpClient() { BaseAddress = new Uri($@"{connection.Server.TrimEnd('/')}/rest/v1/"), Timeout = TimeSpan.FromMinutes(5) };

            http.DefaultRequestHeaders.Authorization = connection is { AccessToken.Length: > 0 }
                ? new AuthenticationHeaderValue("Bearer", connection.AccessToken)
                : new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($@"{connection.Username}:{connection.Password}")));

            // An application that already has an AI connection was set up by an earlier run.
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
                        $@"The AI query containers stay with an Essbase container that was already running, so this only happens if they were removed.");

                Log(sink, $@"'{application}' already uses the AI connection '{associatedName}'.");
                return;
            }

            // The AI connection is associated with, and vectorizes, an existing application.
            Require((await SendAsync(http, HttpMethod.Get, $@"applications/{Uri.EscapeDataString(application)}", body: null, cancellationToken)),
                $@"Find the '{application}' application. The AI query tests expect the tests that create the sample applications to have run first");

            // Essbase connects to the database by its container name, over the shared docker network.
            var databaseHost = $@"{profile.ContainerName}-ai-database";

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
