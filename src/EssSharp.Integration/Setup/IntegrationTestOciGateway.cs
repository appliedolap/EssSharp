using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
    /// The OCI gateway: the one container that stands in for the Oracle Cloud services a test Essbase would otherwise
    /// need, wired the way the scoop <c>essbase</c> stack wires it.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Its identity role is an Identity Cloud tenant (<c>Scripts/Oci/gateway/identity.py</c>): an Essbase container
    /// created in Identity Cloud mode takes it for its identity provider, so Essbase REST accepts the bearer tokens it
    /// issues (OAuth) and looks its users up there (<c>users.json</c>). Its AI role answers the model calls the Oracle
    /// AI Database makes for Essbase AI, with canned answers (<c>answers.json</c>), and configures Essbase AI whenever
    /// that database is up. Both host names, the GenAI inference host and the identity tenant, are aliases of this
    /// container on the test network; it presents a certificate for them from an authority of its own, kept in a volume
    /// the AI database and Essbase read.
    /// </para>
    /// <para>
    /// The gateway starts before the Essbase container, from the assembly fixture, because an Essbase domain created in
    /// Identity Cloud mode trusts the authority at creation. A running gateway named after the Essbase container (the
    /// name the scoop stack gives it) is adopted instead, and is never removed; only a gateway this run started is.
    /// </para>
    /// </remarks>
    public static class IntegrationTestOciGateway
    {
        #region Private Data

        /// <summary>
        /// What the gateway logs once it serves.
        /// </summary>
        private const string ListeningMessage = @"OCI gateway listening";

        private const string GatewayDirectory  = @"/opt/oci-gateway";
        private const string CertificateFolder = @"/opt/oci-certs";
        private const string IdentityHost      = @"identity.local";
        private const string IdentityClientId  = @"essbase";

        /// <summary>
        /// The secret of the <c>essbase</c> OAuth client, which the Identity Cloud integrator in WebLogic uses to talk
        /// to the identity service. It matches the client in <c>users.json</c>.
        /// </summary>
        private const string IdentityClientSecret = @"EssbaseIdentityClient1";

        /// <summary>
        /// The port the identity service answers plain HTTP on inside the container, published on a random host port
        /// for the tests (the token endpoint).
        /// </summary>
        private const ushort IdentityHttpPort = 8080;

        // Everything the harness creates carries this label, so that a leftover of its own can be told from a container
        // or a volume that somebody else started.
        private const string LabelKey   = @"com.appliedolap.essharp.integration-tests";
        private const string LabelValue = @"oci-gateway";

        // The Unix permissions of the files copied into the container: rw-r--r-- and rwxr-xr-x.
        private const UnixFileModes ReadableMode   = UnixFileModes.UserRead | UnixFileModes.UserWrite | UnixFileModes.GroupRead | UnixFileModes.OtherRead;
        private const UnixFileModes ExecutableMode = ReadableMode | UnixFileModes.UserExecute | UnixFileModes.GroupExecute | UnixFileModes.OtherExecute;

        /// <summary>
        /// The gateway's files, copied into its container from <c>Scripts/Oci/gateway</c>.
        /// </summary>
        private static readonly string[] GatewayFiles =
        {
            "gateway.py", "identity.py", "drill.py", "essbase_hints.py", "configure-essbase-ai.py", "provision-essbase-users.py",
            "generate-certs.sh", "start.sh", "answers.json", "users.json"
        };

        private static readonly SemaphoreSlim _gate = new(1, 1);

        private static bool            _ensured;
        private static bool            _keep;
        private static string          _containerName;
        private static string          _containerId;
        private static string          _volumeName;
        private static IVolume         _createdVolume;
        private static DockerContainer _createdContainer;
        private static string          _identityProvider;
        private static bool            _cannedAnswers;
        private static int             _identityPort;
        private static IMessageSink    _sink;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of the gateway container: <c>&lt;essbase container&gt;-oci-gateway</c>.
        /// </summary>
        public static string ContainerName => _containerName;

        /// <summary>
        /// The ID of the gateway container, once it runs.
        /// </summary>
        public static string ContainerId => _containerId ?? throw new InvalidOperationException("The OCI gateway has not been started.");

        /// <summary>
        /// The name of the volume that holds the gateway's certificate authority, certificate and token signing key:
        /// <c>&lt;essbase container&gt;-oci-certs</c>.
        /// </summary>
        public static string CertificateVolumeName => _volumeName;

        /// <summary>
        /// The path the certificate volume is mounted at, in the gateway, the AI database and Essbase.
        /// </summary>
        public static string CertificateMountPath => CertificateFolder;

        /// <summary>
        /// Whether a gateway is running for this run, started by it or adopted.
        /// </summary>
        public static bool IsRunning => _containerId is not null;

        /// <summary>
        /// Whether the running gateway serves an identity provider for Essbase (it was started with
        /// <c>IDENTITY_PROVIDER=IDCS</c>), as opposed to the AI role alone.
        /// </summary>
        public static bool ServesIdentity => string.Equals(_identityProvider, "IDCS", StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Whether the running gateway answers the AI database with the canned answers in <c>answers.json</c>.
        /// </summary>
        public static bool ServesCannedAnswers => _cannedAnswers;

        /// <summary>
        /// The identity service's token endpoint as the tests reach it, over plain HTTP on the host, or
        /// <see langword="null" /> when no gateway runs.
        /// </summary>
        public static string TokenUrl => _identityPort > 0 ? $@"http://localhost:{_identityPort}/oauth2/v1/token" : null;

        /// <summary>
        /// The Identity Cloud tenant name the gateway serves for the given Essbase container: <c>idcs-&lt;container&gt;</c>.
        /// </summary>
        /// <param name="essbaseContainerName" />
        public static string TenantFor( string essbaseContainerName ) => $@"idcs-{essbaseContainerName}";

        /// <summary>
        /// The environment Essbase is created with to take the gateway for its Identity Cloud tenant.
        /// </summary>
        /// <param name="essbaseContainerName" />
        public static IEnumerable<KeyValuePair<string, string>> EssbaseIdentityEnvironment( string essbaseContainerName ) => new[]
        {
            new KeyValuePair<string, string>("IDENTITY_PROVIDER",  "IDCS"),
            new KeyValuePair<string, string>("IDCS_HOST",          IdentityHost),
            new KeyValuePair<string, string>("IDCS_PORT",          "443"),
            new KeyValuePair<string, string>("IDCS_TENANT",        TenantFor(essbaseContainerName)),
            new KeyValuePair<string, string>("IDCS_CLIENT_ID",     IdentityClientId),
            new KeyValuePair<string, string>("IDCS_CLIENT_SECRET", IdentityClientSecret)
        };

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts the gateway for the Essbase container the profile describes, or adopts the one already running under
        /// that name, unless this has been done already.
        /// </summary>
        /// <param name="profile">The Essbase container profile.</param>
        /// <param name="identity">Whether to serve as Essbase's identity provider (the OAuth tests' Managed mode).</param>
        /// <param name="aiQuery">The AI query settings, whose model names and application the AI role is configured with.</param>
        /// <param name="sink" />
        /// <param name="cancellationToken" />
        public static async Task EnsureAsync( EssbaseContainerProfile profile, bool identity, IntegrationTestAiQuerySettings aiQuery, IMessageSink sink = null, CancellationToken cancellationToken = default)
        {
            await _gate.WaitAsync(cancellationToken);

            try
            {
                if (_ensured)
                    return;

                _sink          = sink;
                _keep          = aiQuery?.KeepEnvironment is true;
                _containerName = $@"{profile.ContainerName}-oci-gateway";
                _volumeName    = $@"{profile.ContainerName}-oci-certs";

                using var client = IntegrationTestFactory.GetDockerClient();

                var all     = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true }, cancellationToken);
                var running = all.FirstOrDefault(container => container?.Names?.Any(name => string.Equals(_containerName, name?.Trim('/'), StringComparison.OrdinalIgnoreCase)) is true);

                if (string.Equals(running?.State, "running", StringComparison.OrdinalIgnoreCase))
                {
                    await AdoptAsync(client, running.ID, cancellationToken);
                    Log($@"Using the running OCI gateway '{_containerName}' (identity provider {_identityProvider}, {(_cannedAnswers ? "canned answers" : "the gateway's models")}, token endpoint {TokenUrl ?? "not published"}).");
                    _ensured = true;
                    return;
                }

                // A stopped leftover is only the harness's to remove if the harness made it.
                if (running is not null)
                {
                    if (running.Labels is not { } labels || !labels.TryGetValue(LabelKey, out var value) || !string.Equals(value, LabelValue, StringComparison.Ordinal))
                        throw new InvalidOperationException($@"The OCI gateway container '{_containerName}' exists but is not running, and the test harness did not create it, so it will not remove or start it. Start it, or remove it so that the harness starts its own.");

                    await client.Containers.RemoveContainerAsync(running.ID, new ContainerRemoveParameters() { Force = true }, cancellationToken);
                }

                await StartAsync(profile, identity, aiQuery ?? new IntegrationTestAiQuerySettings(), client, cancellationToken);

                _ensured = true;
            }
            finally
            {
                _gate.Release();
            }
        }

        /// <summary>
        /// Waits until the gateway has logged the given message, polling its log.
        /// </summary>
        /// <param name="message">The message to wait for.</param>
        /// <param name="timeout">How long to wait.</param>
        /// <param name="cancellationToken" />
        /// <returns><see langword="true" /> if the message appeared within the timeout.</returns>
        public static async Task<bool> WaitForLogAsync( string message, TimeSpan timeout, CancellationToken cancellationToken = default)
        {
            var deadline = DateTime.UtcNow.Add(timeout);

            while (DateTime.UtcNow < deadline)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if ((await ReadLogAsync(cancellationToken)).Contains(message, StringComparison.Ordinal))
                    return true;

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }

            return false;
        }

        /// <summary>
        /// Returns the gateway's log so far.
        /// </summary>
        /// <param name="cancellationToken" />
        public static async Task<string> ReadLogAsync( CancellationToken cancellationToken = default )
        {
            using var client = IntegrationTestFactory.GetDockerClient();

            var lines = new LogCollector();

            await client.Containers.GetContainerLogsAsync(ContainerId, new ContainerLogsParameters() { ShowStdout = true, ShowStderr = true }, lines, cancellationToken);

            return lines.ToString();
        }

        /// <summary>
        /// Collects the lines the Docker client reports, synchronously, so that they are all there when the call returns.
        /// </summary>
        private sealed class LogCollector : IProgress<string>
        {
            private readonly StringBuilder _text = new();

            /// <inheritdoc />
            public void Report( string line )
            {
                lock (_text)
                    _text.AppendLine(line);
            }

            /// <inheritdoc />
            public override string ToString()
            {
                lock (_text)
                    return _text.ToString();
            }
        }

        /// <summary>
        /// Removes the gateway and its volume if this run created them, unless the environment is to be kept. Never
        /// throws; it runs after the containers that mount the volume have been removed.
        /// </summary>
        public static async Task DisposeAsync()
        {
            try
            {
                if (_keep)
                    return;

                using var cancellation = new CancellationTokenSource(TimeSpan.FromMinutes(2));

                if (_createdContainer is not null)
                    await _createdContainer.DisposeAsync();

                if (_createdVolume is not null)
                    await _createdVolume.DeleteAsync(cancellation.Token);
            }
            catch (Exception error)
            {
                Log($@"Unable to remove the OCI gateway: {error.Message}");
            }
            finally
            {
                _createdContainer = null;
                _createdVolume    = null;
                _containerId      = null;
                _identityPort     = 0;
                _ensured          = false;
            }
        }

        /// <summary>
        /// Reads one of the scripts copied to the output directory, with Unix line endings. A script checked out on
        /// Windows can carry Windows line endings, which a shell in a Linux container would reject.
        /// </summary>
        /// <param name="area">The folder under <c>Scripts</c>, such as <c>Oci</c>, <c>Ai</c> or <c>Essbase</c>.</param>
        /// <param name="folder">The folder under the area, or <see langword="null" /> for the area itself.</param>
        /// <param name="name">The file name.</param>
        public static byte[] ReadScript( string area, string folder, string name)
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path      = folder is { Length: > 0 } ? Path.Combine(directory, "Scripts", area, folder, name) : Path.Combine(directory, "Scripts", area, name);
            var text      = File.ReadAllText(path);

            return new UTF8Encoding(encoderShouldEmitUTF8Identifier: false).GetBytes(text.Replace("\r\n", "\n"));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Takes a running gateway as this run's: its identity provider mode and AI mode from its environment, and its
        /// token endpoint from the host port its identity port is published on.
        /// </summary>
        private static async Task AdoptAsync( Docker.DotNet.DockerClient client, string id, CancellationToken cancellationToken)
        {
            var inspected = await client.Containers.InspectContainerAsync(id, cancellationToken);
            var env       = inspected.Config?.Env ?? new List<string>();

            _containerId      = id;
            _identityProvider = env.FirstOrDefault(variable => variable.StartsWith("IDENTITY_PROVIDER=", StringComparison.Ordinal))?.Substring("IDENTITY_PROVIDER=".Length) ?? "EMBEDDED";
            _cannedAnswers    = env.Contains("AI_GATEWAY_MODE=canned");

            if (inspected.NetworkSettings?.Ports is { } ports && ports.TryGetValue($@"{IdentityHttpPort}/tcp", out var bindings) && bindings?.FirstOrDefault()?.HostPort is { Length: > 0 } hostPort && int.TryParse(hostPort, out var port))
                _identityPort = port;
        }

        /// <summary>
        /// Starts the gateway: a fresh certificate volume, the container with both host names as aliases, its files and
        /// settings, and the identity port published for the tests.
        /// </summary>
        private static async Task StartAsync( EssbaseContainerProfile profile, bool identity, IntegrationTestAiQuerySettings aiQuery, Docker.DotNet.DockerClient client, CancellationToken cancellationToken)
        {
            var connection = IntegrationTestFactory.GetEssConnection();

            // A fresh volume for a fresh gateway: the AI database and Essbase trust only the authority they were started with.
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
                .WithLabel(LabelKey, LabelValue)
                .WithCleanUp(!_keep)
                .Build();

            await _createdVolume.CreateAsync(cancellationToken);

            var inferenceHost = $@"inference.generativeai.{aiQuery.Region}.oci.oraclecloud.com";
            var identityAlias = $@"{TenantFor(profile.ContainerName)}.{IdentityHost}";

            Log($@"Starting the OCI gateway '{_containerName}' from '{aiQuery.GatewayImage}' as {inferenceHost} and {identityAlias} (identity provider {(identity ? "IDCS" : "EMBEDDED")}).");

            var builder = new ContainerBuilder(aiQuery.GatewayImage)
                .WithName(_containerName)
                .WithLabel(LabelKey, LabelValue)
                .WithNetwork(profile.Network)
                .WithNetworkAliases(inferenceHost, identityAlias)
                .WithPortBinding(IdentityHttpPort, assignRandomHostPort: true)
                .WithCleanUp(!_keep)
                .WithVolumeMount(_createdVolume, CertificateFolder)
                // the identity tenant, the same values Essbase is created with
                .WithEnvironment("IDENTITY_PROVIDER", identity ? "IDCS" : "EMBEDDED")
                .WithEnvironment("IDCS_HOST", IdentityHost)
                .WithEnvironment("IDCS_PORT", "443")
                .WithEnvironment("IDCS_TENANT", TenantFor(profile.ContainerName))
                .WithEnvironment("IDCS_CLIENT_ID", IdentityClientId)
                .WithEnvironment("IDCS_CLIENT_SECRET", IdentityClientSecret)
                .WithEnvironment("OCI_IDENTITY_HTTP_PORT", IdentityHttpPort.ToString())
                // the AI role, answering with canned answers so that the AI query tests can assert on them
                .WithEnvironment("AI_GATEWAY_MODE", "canned")
                .WithEnvironment("AI_GATEWAY_CERTIFICATE", $@"{CertificateFolder}/server.crt")
                .WithEnvironment("AI_GATEWAY_PRIVATE_KEY", $@"{CertificateFolder}/server.key")
                .WithEnvironment("OCI_REGION", aiQuery.Region)
                .WithEnvironment("ESSBASE_URL", $@"http://{profile.ContainerName}:{profile.ManagedServerPort}")
                .WithEnvironment("ESSBASE_USER", connection.Username)
                .WithEnvironment("ESSBASE_PASSWORD", connection.Password)
                .WithEnvironment("AI_DATABASE_HOST", IntegrationTestAiEnvironment.DatabaseContainerNameFor(profile))
                .WithEnvironment("APP_USER", IntegrationTestAiEnvironment.DatabaseUser)
                .WithEnvironment("APP_USER_PASSWORD", IntegrationTestAiEnvironment.DatabaseUserPassword)
                .WithEnvironment("AI_CHAT_MODEL", aiQuery.ChatModel)
                .WithEnvironment("AI_EMBED_MODEL", aiQuery.EmbedModel)
                .WithEnvironment("AI_APPLICATIONS", $@"{aiQuery.ApplicationName}.{aiQuery.CubeName}")
                .WithEnvironment("AI_SETUP_MINUTES", $@"{IntegrationTestAiEnvironment.EnvironmentTimeout.TotalMinutes:0}")
                .WithCommand("sh", $@"{GatewayDirectory}/start.sh")
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(ListeningMessage));

            foreach (var file in GatewayFiles)
                builder = builder.WithResourceMapping(ReadScript("Oci", "gateway", file), $@"{GatewayDirectory}/{file}", fileMode: file.EndsWith(".sh", StringComparison.Ordinal) ? ExecutableMode : ReadableMode);

            _createdContainer = builder.Build() as DockerContainer;

            await _createdContainer.StartAsync(cancellationToken);

            _containerId      = _createdContainer.Id;
            _identityProvider = identity ? "IDCS" : "EMBEDDED";
            _cannedAnswers    = true;
            _identityPort     = _createdContainer.GetMappedPublicPort(IdentityHttpPort);

            Log($@"The OCI gateway is up; its token endpoint is {TokenUrl}.");
        }

        /// <summary>
        /// Writes a diagnostic message to the test run.
        /// </summary>
        private static void Log( string message ) => _sink?.OnMessage(new DiagnosticMessage(message));

        #endregion
    }
}
