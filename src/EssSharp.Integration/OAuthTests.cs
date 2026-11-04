using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
    /// refused, and the identity service's users also sign in with their passwords.
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

        #region Helpers

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
