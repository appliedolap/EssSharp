using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using Xunit.Sdk;
using Xunit.v3;

namespace EssSharp.Integration.Setup
{
    /// <summary>
    /// The identity service the OAuth tests take their tokens from, resolved from the <c>OAuth</c> settings: the
    /// harness's own OCI gateway in <see cref="IntegrationTestOAuthMode.Managed" /> mode, or a token endpoint the
    /// settings name in <see cref="IntegrationTestOAuthMode.External" /> mode (for instance the scoop
    /// <c>essbase</c> stack's, <c>http://localhost:9005/oauth2/v1/token</c>).
    /// </summary>
    /// <remarks>
    /// The tokens are taken with the password grant, which a test can use unattended. An interactive client signs a
    /// user in with the authorization code or device code grant instead, but what Essbase gets is the same bearer
    /// token, and that is what these tests exercise.
    /// </remarks>
    public static class IntegrationTestIdentity
    {
        #region Private Data

        private static readonly HttpClient _http = new() { Timeout = TimeSpan.FromMinutes(1) };

        private static bool   _resolved;
        private static bool   _available;
        private static string _reason;
        private static string _tokenUrl;

        #endregion

        #region Public Properties

        /// <summary>
        /// Whether the Essbase server under test accepts the identity service's bearer tokens: the settings turn the
        /// OAuth tests on, a token endpoint could be resolved, and it answers. When false, <see cref="Reason" /> says why.
        /// </summary>
        public static bool Available => _resolved ? _available : throw new InvalidOperationException("The identity service has not been resolved; the OAuth collection fixture does that.");

        /// <summary>
        /// Why the OAuth tests cannot run, when <see cref="Available" /> is false.
        /// </summary>
        public static string Reason => _reason;

        /// <summary>
        /// The token endpoint, once resolved.
        /// </summary>
        public static string TokenUrl => _tokenUrl;

        #endregion

        #region Public Methods

        /// <summary>
        /// Resolves the identity service once, from the settings and the running containers, and records whether the
        /// OAuth tests can run.
        /// </summary>
        /// <param name="sink" />
        /// <param name="cancellationToken" />
        public static async Task ResolveAsync( IMessageSink sink = null, CancellationToken cancellationToken = default)
        {
            if (_resolved)
                return;

            _resolved = true;

            var settings = IntegrationTestFactory.OAuth ?? new IntegrationTestOAuthSettings();

            switch (settings.Mode)
            {
                case IntegrationTestOAuthMode.Disabled:
                    _reason = "The OAuth tests are disabled by the test settings (OAuth:Mode).";
                    return;

                case IntegrationTestOAuthMode.External:
                    if (settings.TokenUrl is not { Length: > 0 })
                    {
                        _reason = "OAuth:Mode is External, but OAuth:TokenUrl names no token endpoint.";
                        return;
                    }
                    _tokenUrl = settings.TokenUrl;
                    break;

                case IntegrationTestOAuthMode.Managed:
                    // The gateway was started, or adopted, by the assembly fixture. Essbase accepts its tokens only if
                    // the Essbase container was created in Identity Cloud mode against it, which an adopted container
                    // may not have been.
                    if (!IntegrationTestOciGateway.IsRunning || IntegrationTestOciGateway.TokenUrl is not { Length: > 0 } gatewayTokenUrl)
                    {
                        _reason = "OAuth:Mode is Managed, but the OCI gateway is not running or does not publish its identity port.";
                        return;
                    }
                    if (!await IsEssbaseInIdentityCloudModeAsync(cancellationToken))
                    {
                        _reason = $@"The Essbase container '{IntegrationTestFactory.EssbaseProfile.ContainerName}' was not created in Identity Cloud mode (IDENTITY_PROVIDER=IDCS), so it does not accept the identity service's tokens. Remove it so that the harness creates one that is, or set OAuth:Mode to Disabled.";
                        return;
                    }
                    _tokenUrl = gatewayTokenUrl;
                    break;
            }

            // The identity service must answer, and the server must accept a token it issues.
            try
            {
                var discovery = await _http.GetAsync(DiscoveryUrl(_tokenUrl), cancellationToken);

                if (!discovery.IsSuccessStatusCode)
                {
                    _reason = $@"The identity service at {_tokenUrl} does not answer its discovery document (HTTP {(int)discovery.StatusCode}).";
                    return;
                }

                var administrator = settings.Users?.FirstOrDefault(user => user.Role == EssServerRole.ServiceAdministrator)
                    ?? throw new InvalidOperationException("OAuth:Users names no user with the ServiceAdministrator role.");

                var token  = await GetTokenAsync(administrator.Username, administrator.Password, cancellationToken: cancellationToken);
                var server = IntegrationTestFactory.GetEssConnection().Server.TrimEnd('/');

                using var request = new HttpRequestMessage(HttpMethod.Get, $@"{server}/rest/v1/session");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using var response = await _http.SendAsync(request, cancellationToken);

                if (response.StatusCode is HttpStatusCode.Unauthorized)
                {
                    _reason = $@"The Essbase server at {server} does not accept the tokens of the identity service at {_tokenUrl} (HTTP 401): it was not created with that service as its identity provider.";

                    if (settings.Mode is IntegrationTestOAuthMode.Managed)
                        await LogManagedDiagnosisAsync(sink, cancellationToken);

                    return;
                }

                // A 403 means the token was accepted and the user merely holds no role yet; the fixture provisions roles.
                _available = true;

                sink?.OnMessage(new DiagnosticMessage($@"OAuth tests: the identity service at {_tokenUrl} issues tokens the Essbase server accepts."));
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception error)
            {
                _reason = $@"The identity service at {_tokenUrl} could not be used: {error.Message}";
            }
        }

        /// <summary>
        /// Gets an access token for a user from the identity service, with the password grant.
        /// </summary>
        /// <param name="username" />
        /// <param name="password" />
        /// <param name="expiresIn">A lifetime in seconds to ask for instead of the service's default; negative for a token that has already expired (the harness's own identity service honours this, for tests).</param>
        /// <param name="scope">The scope to ask for; by default the Essbase scope of the tenant, which the harness's identity service does not check.</param>
        /// <param name="cancellationToken" />
        public static async Task<string> GetTokenAsync( string username, string password, int? expiresIn = null, string scope = null, CancellationToken cancellationToken = default)
        {
            var response = await GetTokenResponseAsync(username, password, expiresIn, scope, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new InvalidOperationException($@"The identity service refused a token for '{username}' (HTTP {(int)response.StatusCode}): {response.Body}");

            using var document = JsonDocument.Parse(response.Body);

            return document.RootElement.GetProperty("access_token").GetString();
        }

        /// <summary>
        /// Asks the identity service for a token and returns the raw answer, for tests that expect a refusal.
        /// </summary>
        public static async Task<(HttpStatusCode StatusCode, string Body)> GetTokenResponseAsync( string username, string password, int? expiresIn = null, string scope = null, CancellationToken cancellationToken = default)
        {
            var settings = IntegrationTestFactory.OAuth ?? new IntegrationTestOAuthSettings();
            var form     = new Dictionary<string, string>()
            {
                ["grant_type"] = "password",
                ["username"]   = username,
                ["password"]   = password,
                ["scope"]      = scope ?? settings.Scope ?? "openid"
            };

            if (expiresIn is { } seconds)
                form["expires_in"] = seconds.ToString();

            using var request = new HttpRequestMessage(HttpMethod.Post, _tokenUrl ?? settings.TokenUrl) { Content = new FormUrlEncodedContent(form) };

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($@"{settings.ClientId}:{settings.ClientSecret}")));

            using var response = await _http.SendAsync(request, cancellationToken);

            return (response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Returns the configured identity user with the given role.
        /// </summary>
        /// <param name="role" />
        public static IntegrationTestOAuthUser GetUser( EssServerRole role ) =>
            (IntegrationTestFactory.OAuth?.Users ?? Array.Empty<IntegrationTestOAuthUser>()).FirstOrDefault(user => user.Role == role)
            ?? throw new InvalidOperationException($@"OAuth:Users names no user with the {role} role.");

        /// <summary>
        /// Gives each configured identity user its Essbase role, through the REST API as the service administrator, so
        /// that the tests do not wait for the identity service's own provisioning.
        /// </summary>
        /// <param name="cancellationToken" />
        public static async Task ProvisionUsersAsync( CancellationToken cancellationToken = default)
        {
            var users = IntegrationTestFactory.OAuth?.Users ?? Array.Empty<IntegrationTestOAuthUser>();

            using var http = IntegrationTestAiEnvironment.CreateEssbaseClient();

            foreach (var user in users.Where(user => user.Role is not EssServerRole.Unkown))
            {
                var role = user.Role switch
                {
                    EssServerRole.ServiceAdministrator => "service_administrator",
                    EssServerRole.PowerUser            => "power_user",
                    _                                  => "user"
                };

                using var request = new HttpRequestMessage(HttpMethod.Put, $@"permissions/{Uri.EscapeDataString(user.Username)}")
                {
                    Content = new StringContent(JsonSerializer.Serialize(new { id = user.Username, role, group = false }), Encoding.UTF8, "application/json")
                };

                using var response = await http.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException($@"Unable to give '{user.Username}' the {role} role (HTTP {(int)response.StatusCode}): {await response.Content.ReadAsStringAsync(cancellationToken)}");

                foreach (var (application, applicationRole) in user.Applications ?? new Dictionary<string, string>())
                {
                    using var applicationRequest = new HttpRequestMessage(HttpMethod.Put, $@"applications/{Uri.EscapeDataString(application)}/permissions/{Uri.EscapeDataString(user.Username)}")
                    {
                        Content = new StringContent(JsonSerializer.Serialize(new { id = user.Username, role = applicationRole, group = false }), Encoding.UTF8, "application/json")
                    };

                    using var applicationResponse = await http.SendAsync(applicationRequest, cancellationToken);

                    if (!applicationResponse.IsSuccessStatusCode)
                        throw new InvalidOperationException($@"Unable to give '{user.Username}' the {applicationRole} role on '{application}' (HTTP {(int)applicationResponse.StatusCode}): {await applicationResponse.Content.ReadAsStringAsync(cancellationToken)}");
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Writes what the harness can see of a Managed identity setup that Essbase does not accept: the Essbase
        /// container's own account of its identity setup, whether the domain trusts the gateway's certificate authority,
        /// the identity store's errors in the managed server's log, and the requests the gateway received. The containers
        /// are removed when the run ends, so this is the only record a CI run keeps.
        /// </summary>
        private static async Task LogManagedDiagnosisAsync( IMessageSink sink, CancellationToken cancellationToken )
        {
            async Task<string> ExecEssbase( string command ) =>
                (await IntegrationTestFactory.ExecAsync(IntegrationTestFactory.EssbaseContainerId, new[] { "/bin/bash", "-lc", command }, cancellationToken)).stdout?.Trim();

            // Written to the console too: a test runner may not show diagnostic messages (the CI runner does not), and
            // this is the only record of why a CI run's identity setup failed.
            void Write( string title, string text )
            {
                var message = $@"OAuth diagnosis - {title}:{Environment.NewLine}{(string.IsNullOrWhiteSpace(text) ? "(nothing)" : text)}";

                sink?.OnMessage(new DiagnosticMessage(message));
                Console.WriteLine(message);
            }

            try
            {
                using var client = IntegrationTestFactory.GetDockerClient();

                var essbase = await client.Containers.InspectContainerAsync(IntegrationTestFactory.EssbaseContainerId, cancellationToken);
                Write("Essbase identity environment", string.Join(Environment.NewLine, (essbase.Config?.Env ?? new List<string>()).Where(variable => variable.StartsWith("IDENTITY_PROVIDER=") || variable.StartsWith("IDCS_HOST=") || variable.StartsWith("IDCS_TENANT=") || variable.StartsWith("IDCS_PORT="))) + Environment.NewLine + "command: " + string.Join(" ", essbase.Config?.Cmd ?? new List<string>()));

                var domain = @"/u01/config/domains/infra_domain";
                Write("Essbase identity setup", await ExecEssbase($@"grep -n 'trustOciGateway' /u01/container-scripts/_createDomain.sh; grep -c LOGOUT_URL /u01/container-scripts/wlst/enable_idcs_mode.py; ls -la /opt/oci-certs; echo 'CA entries in the domain trust store:'; grep -c 'oci-gateway-ca' {domain}/config/fmwconfig/keystores.xml; grep -n -A12 'IdentityCloudServiceIntegrator' {domain}/config/config.xml | head -14"));
                Write("name resolution from Essbase", await ExecEssbase($@"getent hosts {IntegrationTestOciGateway.TenantFor(IntegrationTestFactory.EssbaseProfile.ContainerName)}.identity.local"));
                Write("identity store errors in the managed server log", await ExecEssbase($@"grep -h -i 'PKIX\|IGF-\|LIBOVD\|SCIMPlugin\|Error while finding\|idstore' {domain}/servers/essbase_server1/logs/essbase_server1.out 2>/dev/null | cut -c1-300 | tail -15"));

                var gatewayLog = await IntegrationTestOciGateway.ReadLogAsync(cancellationToken);
                Write("OCI gateway log (last 40 lines)", string.Join(Environment.NewLine, gatewayLog.Split('\n').Select(line => line.TrimEnd()).Where(line => line.Length > 0).TakeLast(40).Select(line => line.Length > 300 ? line.Substring(0, 300) : line)));
            }
            catch (Exception error) when (error is not OperationCanceledException)
            {
                sink?.OnMessage(new DiagnosticMessage($@"OAuth diagnosis failed: {error.Message}"));
            }
        }

        /// <summary>
        /// The discovery document next to a token endpoint: <c>/.well-known/openid-configuration</c> on the same host.
        /// </summary>
        private static string DiscoveryUrl( string tokenUrl ) => new Uri(new Uri(tokenUrl), "/.well-known/openid-configuration").ToString();

        /// <summary>
        /// Returns whether the Essbase container was created with the identity service as its identity provider.
        /// </summary>
        private static async Task<bool> IsEssbaseInIdentityCloudModeAsync( CancellationToken cancellationToken)
        {
            using var client = IntegrationTestFactory.GetDockerClient();

            var inspected = await client.Containers.InspectContainerAsync(IntegrationTestFactory.EssbaseContainerId, cancellationToken);

            return inspected.Config?.Env?.Contains("IDENTITY_PROVIDER=IDCS") is true;
        }

        #endregion
    }
}
