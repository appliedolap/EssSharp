using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using HttpMethod = System.Net.Http.HttpMethod;

using EssSharp.Client;
using EssSharp.Api;
using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />.
    /// </summary>
    [CollectionDefinition(nameof(OAuthTests), DisableParallelization = true), CollectionPriority(8)]
    public class OAuthTestsCollection : ICollectionFixture<OAuthCollectionFixture> { }

    /// <summary>
    /// Integration tests for OAuth bearer tokens against an Essbase server that takes an identity service for its
    /// identity provider: a token from that service signs the library in as the token's user, a bad or expired token is
    /// refused, and the identity service's users also sign in with their passwords. When the identity service is the
    /// harness's own OCI gateway, the tests also hold it to the checks its clients rely on (passwords, token lifetimes,
    /// signatures, redirect addresses, codes bound to their client, an escaped sign-in page) and check that its host
    /// port does not serve the AI actions.
    /// </summary>
    /// <remarks>
    /// The whole collection is skipped, with the reason, when the server does not accept such tokens: the OAuth tests
    /// are disabled, or the Essbase container was not created in Identity Cloud mode (see <c>OAuth:Mode</c> in the
    /// settings and <see cref="IntegrationTestIdentity" />).
    /// </remarks>
    [Collection(nameof(OAuthTests)), Trait("type", "execute")]
    public class OAuthTests : IntegrationTestBase
    {
        /// <summary />
        /// <param name="output" />
        public OAuthTests( ITestOutputHelper output ) : base(output) { }

        [Fact(DisplayName = @"OAuthTests - 01 - Essbase_WithBearerToken_SignsInAsTokenUser"), Priority(01)]
        public async Task Essbase_WithBearerToken_SignsInAsTokenUser()
        {
            SkipUnlessAvailable();

            var user   = IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator);
            var server = await GetTokenServerAsync(user);

            // The session Essbase opens for the token is the token's user, with the role the identity user was given.
            var session = await server.GetUserSessionAsync(includeToken: false, includeGroups: true, TestContext.Current.CancellationToken);

            Assert.NotNull(session);
            Assert.Equal(user.Username, session.UserId, ignoreCase: true);
        }

        [Fact(DisplayName = @"OAuthTests - 02 - Essbase_WithBearerToken_ListsApplications"), Priority(02)]
        public async Task Essbase_WithBearerToken_ListsApplications()
        {
            SkipUnlessAvailable();

            var server       = await GetTokenServerAsync(IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator));
            var applications = await server.GetApplicationsAsync(TestContext.Current.CancellationToken);

            // The sample application, which the create collection made, is there for a service administrator.
            Assert.Contains(applications, application => string.Equals(application.Name, "Sample", StringComparison.OrdinalIgnoreCase));
        }

        [Fact(DisplayName = @"OAuthTests - 03 - Essbase_WithBearerToken_RetrievesGrid"), Priority(03)]
        public async Task Essbase_WithBearerToken_RetrievesGrid()
        {
            SkipUnlessAvailable();

            var server = await GetTokenServerAsync(IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator));
            var cube   = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken).GetCubeAsync("Basic", TestContext.Current.CancellationToken);

            // A grid retrieve is the first thing a Dodeca view does with a connection, so it is the operation to prove.
            var grid      = await cube.GetDefaultGridAsync(cancellationToken: TestContext.Current.CancellationToken);
            var refreshed = await grid.RefreshAsync(TestContext.Current.CancellationToken);

            Assert.NotNull(refreshed?.Slice);
            Assert.True(refreshed.Slice.Rows > 0, "The refreshed grid has no rows.");
        }

        [Fact(DisplayName = @"OAuthTests - 04 - Essbase_WithUserRoleToken_SeesGrantedApplications"), Priority(04)]
        public async Task Essbase_WithUserRoleToken_SeesGrantedApplications()
        {
            SkipUnlessAvailable();

            var user          = IntegrationTestIdentity.GetUser(EssServerRole.User);
            var administrator = IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator);

            var userServer          = await GetTokenServerAsync(user);
            var administratorServer = await GetTokenServerAsync(administrator);

            var userApplications          = (await userServer.GetApplicationsAsync(TestContext.Current.CancellationToken)).Select(application => application.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
            var administratorApplications = (await administratorServer.GetApplicationsAsync(TestContext.Current.CancellationToken)).Select(application => application.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

            // A user sees the applications it was given a role on - at least the ones the settings grant - and no more than
            // a service administrator, who sees them all.
            foreach (var application in user.Applications?.Keys ?? Enumerable.Empty<string>())
                Assert.Contains(application, userApplications);

            Assert.Superset(userApplications, administratorApplications);
        }

        [Fact(DisplayName = @"OAuthTests - 05 - Essbase_RefusesForgedBearerToken"), Priority(05)]
        public async Task Essbase_RefusesForgedBearerToken()
        {
            SkipUnlessAvailable();

            var administrator = IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator);
            var user          = IntegrationTestIdentity.GetUser(EssServerRole.User);
            var token         = await IntegrationTestIdentity.GetTokenAsync(administrator.Username, administrator.Password, cancellationToken: TestContext.Current.CancellationToken);

            // A token whose claims were changed after it was signed: the service administrator's token, re-addressed to the
            // user. Its signature no longer matches, so the server must refuse it rather than sign anyone in.
            var forged = Forge(token, administrator.Username, user.Username);
            var server = new EssServerFactory().CreateEssServer(GetEssConnection().Server, oauthToken: forged, connect: false);

            var error = await Assert.ThrowsAnyAsync<Exception>(() => server.GetUserSessionAsync(includeToken: false, includeGroups: false, TestContext.Current.CancellationToken));

            AssertUnauthorized(error);
        }

        [Fact(DisplayName = @"OAuthTests - 06 - Essbase_RefusesExpiredBearerToken"), Priority(06)]
        public async Task Essbase_RefusesExpiredBearerToken()
        {
            SkipUnlessAvailable();

            var administrator = IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator);

            // A token that expired an hour ago: well past the clock skew the Identity Cloud integrator allows (two minutes).
            var expired = await IntegrationTestIdentity.GetTokenAsync(administrator.Username, administrator.Password, expiresIn: -3600, cancellationToken: TestContext.Current.CancellationToken);
            var server  = new EssServerFactory().CreateEssServer(GetEssConnection().Server, oauthToken: expired, connect: false);

            var error = await Assert.ThrowsAnyAsync<Exception>(() => server.GetUserSessionAsync(includeToken: false, includeGroups: false, TestContext.Current.CancellationToken));

            AssertUnauthorized(error);
        }

        [Fact(DisplayName = @"OAuthTests - 07 - Essbase_WithBearerToken_SignsOut"), Priority(07)]
        public async Task Essbase_WithBearerToken_SignsOut()
        {
            SkipUnlessAvailable();

            var server = await GetTokenServerAsync(IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator));

            await server.SignInAsync(cancellationToken: TestContext.Current.CancellationToken);
            await server.SignOutAsync(cancellationToken: TestContext.Current.CancellationToken);

            // A new session can be opened with the same token after signing out.
            Assert.NotNull(await server.GetUserSessionAsync(includeToken: false, includeGroups: false, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = @"OAuthTests - 08 - Essbase_IdentityUser_SignsInWithPassword"), Priority(08)]
        public async Task Essbase_IdentityUser_SignsInWithPassword()
        {
            SkipUnlessAvailable();

            var user   = IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator);
            var server = new EssServerFactory().CreateEssServer(GetEssConnection().Server, username: user.Username, password: user.Password, connect: false);

            // An identity user's password is checked by the identity service too (through the integrator's password
            // authenticator), so basic credentials keep working beside the tokens.
            var session = await server.GetUserSessionAsync(includeToken: false, includeGroups: false, TestContext.Current.CancellationToken);

            Assert.Equal(user.Username, session.UserId, ignoreCase: true);
        }

        [Fact(DisplayName = @"OAuthTests - 09 - Essbase_RefusesIdentityUser_WithWrongPassword"), Priority(09)]
        public async Task Essbase_RefusesIdentityUser_WithWrongPassword()
        {
            SkipUnlessAvailable();

            var user   = IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator);
            var server = new EssServerFactory().CreateEssServer(GetEssConnection().Server, username: user.Username, password: user.Password + "-wrong", connect: false);

            var error = await Assert.ThrowsAnyAsync<Exception>(() => server.GetUserSessionAsync(includeToken: false, includeGroups: false, TestContext.Current.CancellationToken));

            AssertUnauthorized(error);
        }

        [Fact(DisplayName = @"OAuthTests - 10 - IdentityService_RefusesWrongPassword"), Priority(10)]
        public async Task IdentityService_RefusesWrongPassword()
        {
            SkipUnlessAvailable();

            var user     = IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator);
            var response = await IntegrationTestIdentity.GetTokenResponseAsync(user.Username, user.Password + "-wrong", cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact(DisplayName = @"OAuthTests - 11 - IdentityService_RefusesPasswordCheckWithoutAPassword"), Priority(11)]
        public async Task IdentityService_RefusesPasswordCheckWithoutAPassword()
        {
            SkipUnlessHarnessGateway();

            var user = IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator);

            // The integrator's password check for basic-auth users must not pass for a request that names a user and
            // carries no password at all.
            using var response = await SendAsync(HttpMethod.Post, "/sso/v1/sdk/authenticate", new StringContent(JsonSerializer.Serialize(new { userName = user.Username }), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact(DisplayName = @"OAuthTests - 12 - IdentityService_GivesEachTokenItsOwnLifetime_UnderParallelRequests"), Priority(12)]
        public async Task IdentityService_GivesEachTokenItsOwnLifetime_UnderParallelRequests()
        {
            SkipUnlessHarnessGateway();

            var user            = IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator);
            var defaultLifetime = await GetLifetimeAsync(user, null);

            // Tokens asked for with different lifetimes at the same time: each must get its own, and the service's
            // default must be the same afterwards.
            var asked   = Enumerable.Range(0, 24).Select(index => (index % 3) switch { 0 => -5, 1 => 7, _ => (int?)null }).ToArray();
            var answers = await Task.WhenAll(asked.Select(async lifetime => (Asked: lifetime, Given: await GetLifetimeAsync(user, lifetime))));

            Assert.All(answers, answer => Assert.Equal(answer.Asked ?? defaultLifetime, answer.Given));
            Assert.Equal(defaultLifetime, await GetLifetimeAsync(user, null));
        }

        [Fact(DisplayName = @"OAuthTests - 13 - IdentityService_IntrospectRefusesForgedToken"), Priority(13)]
        public async Task IdentityService_IntrospectRefusesForgedToken()
        {
            SkipUnlessHarnessGateway();

            var administrator = IntegrationTestIdentity.GetUser(EssServerRole.ServiceAdministrator);
            var user          = IntegrationTestIdentity.GetUser(EssServerRole.User);
            var token         = await IntegrationTestIdentity.GetTokenAsync(user.Username, user.Password, cancellationToken: TestContext.Current.CancellationToken);

            Assert.True(await IsActiveAsync(token));

            // The user's token re-addressed to the service administrator, with its signature left as it was.
            Assert.False(await IsActiveAsync(Forge(token, user.Username, administrator.Username)));
        }

        [Fact(DisplayName = @"OAuthTests - 14 - IdentityService_RefusesRedirectOffTheLocalMachine"), Priority(14)]
        public async Task IdentityService_RefusesRedirectOffTheLocalMachine()
        {
            SkipUnlessHarnessGateway();

            // A client that registers no redirect addresses may send its codes to the local machine only, as native
            // applications do; anywhere else would hand the code to whoever owns that address.
            using var elsewhere = await SendAsync(HttpMethod.Get, AuthorizeQuery("esssharp", "https://example.com/callback"));
            using var local     = await SendAsync(HttpMethod.Get, AuthorizeQuery("esssharp", "http://localhost:51234/callback"));
            using var unknown   = await SendAsync(HttpMethod.Get, AuthorizeQuery("no-such-client", "http://localhost/"));

            Assert.Equal(HttpStatusCode.BadRequest, elsewhere.StatusCode);
            Assert.Equal(HttpStatusCode.OK, local.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, unknown.StatusCode);
        }

        [Fact(DisplayName = @"OAuthTests - 15 - IdentityService_RefusesCodeRedeemedByAnotherClient"), Priority(15)]
        public async Task IdentityService_RefusesCodeRedeemedByAnotherClient()
        {
            SkipUnlessHarnessGateway();

            var user = IntegrationTestIdentity.GetUser(EssServerRole.User);

            // A code issued to one client cannot be turned into a token by another; its own client can.
            using var stolen = await RedeemAsync("esssharp", await GetCodeAsync("dodeca", user));
            using var own    = await RedeemAsync("dodeca", await GetCodeAsync("dodeca", user));

            Assert.Equal(HttpStatusCode.BadRequest, stolen.StatusCode);
            Assert.Equal(HttpStatusCode.OK, own.StatusCode);
        }

        [Fact(DisplayName = @"OAuthTests - 16 - IdentityService_SignInPage_EscapesTheRequest"), Priority(16)]
        public async Task IdentityService_SignInPage_EscapesTheRequest()
        {
            SkipUnlessHarnessGateway();

            using var response = await SendAsync(HttpMethod.Get, AuthorizeQuery("esssharp", "http://localhost/") + "&state=" + Uri.EscapeDataString(@"""><script>alert(1)</script>"));
            var page = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.DoesNotContain("<script>", page, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("&lt;script&gt;", page, StringComparison.Ordinal);
        }

        [Theory(DisplayName = @"OAuthTests - 17 - OciGateway_HostPort_DoesNotServeAiActions"), Priority(17)]
        [InlineData("/20231130/actions/chat")]
        [InlineData("/20231130/actions/embedText")]
        public async Task OciGateway_HostPort_DoesNotServeAiActions( string path )
        {
            SkipUnlessHarnessGateway();

            // The gateway's port on the host serves identity and URL drill-through only: a chat call goes out with the
            // chat model's API key, so the AI actions answer inside the stack's network alone.
            using var response = await SendAsync(HttpMethod.Post, path, new StringContent(@"{""chatRequest"":{},""inputs"":[""a""]}", Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        #region Helpers

        private static readonly HttpClient _identityHttp = new(new HttpClientHandler() { AllowAutoRedirect = false }) { Timeout = TimeSpan.FromMinutes(1) };

        /// <summary>
        /// Skips the test unless the identity service is the harness's own OCI gateway, whose behaviour these tests pin
        /// down (an external identity service is somebody else's).
        /// </summary>
        private static void SkipUnlessHarnessGateway()
        {
            SkipUnlessAvailable();

            Assert.SkipUnless(IntegrationTestOciGateway.IsRunning && string.Equals(IntegrationTestIdentity.TokenUrl, IntegrationTestOciGateway.TokenUrl, StringComparison.OrdinalIgnoreCase), "The identity service is not the harness's OCI gateway.");
        }

        /// <summary>
        /// Sends a request to the harness's OCI gateway on its host port, without following redirects.
        /// </summary>
        private static Task<HttpResponseMessage> SendAsync( HttpMethod method, string pathAndQuery, HttpContent content = null )
        {
            var gateway = new Uri(IntegrationTestOciGateway.TokenUrl).GetLeftPart(UriPartial.Authority);

            return _identityHttp.SendAsync(new HttpRequestMessage(method, gateway + pathAndQuery) { Content = content }, TestContext.Current.CancellationToken);
        }

        private static string AuthorizeQuery( string clientId, string redirectUri ) =>
            $@"/oauth2/v1/authorize?response_type=code&scope=openid&client_id={Uri.EscapeDataString(clientId)}&redirect_uri={Uri.EscapeDataString(redirectUri)}";

        /// <summary>
        /// Returns the lifetime (expires_in) of a token asked for with the given lifetime, or with the default.
        /// </summary>
        private static async Task<int> GetLifetimeAsync( IntegrationTestOAuthUser user, int? lifetime )
        {
            var response = await IntegrationTestIdentity.GetTokenResponseAsync(user.Username, user.Password, expiresIn: lifetime, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var document = JsonDocument.Parse(response.Body);

            return document.RootElement.GetProperty("expires_in").GetInt32();
        }

        private static async Task<bool> IsActiveAsync( string token )
        {
            using var response = await SendAsync(HttpMethod.Post, "/oauth2/v1/introspect", new FormUrlEncodedContent(new Dictionary<string, string>() { ["token"] = token }));
            using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken));

            return document.RootElement.GetProperty("active").GetBoolean();
        }

        /// <summary>
        /// Signs the user in on the sign-in page for the client and returns the code it redirects with.
        /// </summary>
        private static async Task<string> GetCodeAsync( string clientId, IntegrationTestOAuthUser user )
        {
            using var response = await SendAsync(HttpMethod.Post, "/oauth2/v1/authorize", new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                ["response_type"] = "code",
                ["client_id"]     = clientId,
                ["redirect_uri"]  = "http://localhost/",
                ["scope"]         = "openid",
                ["username"]      = user.Username,
                ["password"]      = user.Password
            }));

            Assert.Equal(HttpStatusCode.Found, response.StatusCode);

            var query = response.Headers.Location?.Query.TrimStart('?') ?? string.Empty;

            return query.Split('&').Select(pair => pair.Split('=', 2)).Where(pair => pair[0] == "code").Select(pair => Uri.UnescapeDataString(pair[1])).Single();
        }

        private static Task<HttpResponseMessage> RedeemAsync( string clientId, string code ) =>
            SendAsync(HttpMethod.Post, "/oauth2/v1/token", new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                ["grant_type"]   = "authorization_code",
                ["client_id"]    = clientId,
                ["code"]         = code,
                ["redirect_uri"] = "http://localhost/"
            }));

        /// <summary>
        /// Skips the test, with the fixture's reason, when the server does not accept the identity service's tokens.
        /// </summary>
        private static void SkipUnlessAvailable() => Assert.SkipUnless(IntegrationTestIdentity.Available, IntegrationTestIdentity.Reason ?? "The identity service is not available.");

        /// <summary>
        /// Returns an unconnected server that authenticates with a bearer token for the given identity user.
        /// </summary>
        /// <param name="user" />
        private async Task<IEssServer> GetTokenServerAsync( IntegrationTestOAuthUser user)
        {
            var token = await IntegrationTestIdentity.GetTokenAsync(user.Username, user.Password, cancellationToken: TestContext.Current.CancellationToken);

            return new EssServerFactory().CreateEssServer(GetEssConnection().Server, oauthToken: token, connect: false);
        }

        /// <summary>
        /// Returns the token with its subject claim changed and its signature left as it was, which no verifier may accept.
        /// </summary>
        private static string Forge( string token, string subject, string replacement)
        {
            var parts   = token.Split('.');
            var payload = Encoding.UTF8.GetString(FromBase64Url(parts[1])).Replace($@"""sub"":""{subject}""", $@"""sub"":""{replacement}""", StringComparison.Ordinal);

            return $@"{parts[0]}.{ToBase64Url(Encoding.UTF8.GetBytes(payload))}.{parts[2]}";
        }

        /// <summary>
        /// Asserts that an exception from the library carries a 401, searching its inner exceptions for the API
        /// exception that holds the status; the failure message describes the chain that was thrown instead.
        /// </summary>
        private static void AssertUnauthorized( Exception error )
        {
            var chain = new System.Collections.Generic.List<string>();

            for (var current = error; current is not null; current = current.InnerException)
            {
                // The library reports a refused request as its ApiException, or as a WebException that names the status.
                if (current is ApiException api && (HttpStatusCode)api.ErrorCode == HttpStatusCode.Unauthorized)
                    return;

                if (current is System.Net.WebException { Response: WebExceptionRestResponse { StatusCode: HttpStatusCode.Unauthorized } })
                    return;

                chain.Add($@"{current.GetType().FullName}: {current.Message}");
            }

            Assert.Fail($@"Expected a 401 from Essbase, but the library threw:{Environment.NewLine}{string.Join(Environment.NewLine, chain)}");
        }

        private static byte[] FromBase64Url( string text ) => Convert.FromBase64String(text.Replace('-', '+').Replace('_', '/').PadRight(text.Length + (4 - text.Length % 4) % 4, '='));

        private static string ToBase64Url( byte[] bytes ) => Convert.ToBase64String(bytes).TrimEnd('=').Replace('+', '-').Replace('/', '_');

        #endregion
    }
}
