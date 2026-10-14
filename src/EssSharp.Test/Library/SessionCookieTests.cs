using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Client;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Offline tests for session cookie retention, recovery, and pooling against a loopback stub,
    /// including Set-Cookie headers with an empty Domain= attribute, which some fronting proxies and
    /// load balancers (including the Essbase on Autonomous Database gateway) emit and cookie parsing
    /// rejects with a CookieException. These tests require no Essbase server or container.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class SessionCookieTests
    {
        private const string ExpectedMdxAcceptHeader = @"Accept: application/octet-stream,text/html,application/json;charset=UTF-8";

        [Fact(DisplayName = "SessionCookieTests - ApiClient_WithWellFormedSessionCookie_RetainsAndReusesIt")]
        public async Task ApiClient_WithWellFormedSessionCookie_RetainsAndReusesIt()
        {
            // Create a stub server that returns a well-formed session cookie.
            using var stub = new LoopbackEssbaseStub(@"JSESSIONID=WellFormed456; Path=/essbase; HttpOnly");

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session and assert that the session cookie was retained.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            AssertRetainedSessionCookie(client, @"WellFormed456");

            // Get a session again and assert that the retained cookie rode the request in place of authorization.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            AssertSecondRequestRodeTheSession(stub, @"JSESSIONID=WellFormed456");
        }

        [Fact(DisplayName = "SessionCookieTests - ApiClient_WithEmptyDomainSessionCookie_RecoversAndReusesIt")]
        public async Task ApiClient_WithEmptyDomainSessionCookie_RecoversAndReusesIt()
        {
            // Create a stub server that returns a session cookie with an empty Domain= attribute.
            using var stub = new LoopbackEssbaseStub(@"JSESSIONID=EmptyDomain123; Path=/essbase; Domain=; HttpOnly");

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session and assert that the session cookie was recovered despite the malformed domain.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            var cookie = AssertRetainedSessionCookie(client, @"EmptyDomain123");

            // Assert that the recovered cookie is scoped host-only to the stub host with the cookie path.
            Assert.Equal(IPAddress.Loopback.ToString(), cookie.Domain);
            Assert.Equal(@"/essbase", cookie.Path);

            // Get a session again and assert that the recovered cookie rode the request in place of authorization.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            AssertSecondRequestRodeTheSession(stub, @"JSESSIONID=EmptyDomain123");

            // Assert that the recovered cookie was retained again from the second response.
            AssertRetainedSessionCookie(client, @"EmptyDomain123");
        }

        [Fact(DisplayName = "SessionCookieTests - ApiClient_WithGatewayCookieSet_ReplaysTheFullSet")]
        public async Task ApiClient_WithGatewayCookieSet_ReplaysTheFullSet()
        {
            // Create a stub server that returns the Essbase on Autonomous Database gateway cookie set:
            // three well-formed cookies plus two token cookies with legacy attribute tails whose empty
            // Domain= attribute cookie parsing rejects.
            using var stub = new LoopbackEssbaseStub(
                @"JSESSIONID=AdbSession123; path=/; SameSite=Strict; HttpOnly",
                @"essbaseToken=AdbEssbase456; path=/essbase; HttpOnly",
                @"sessionExpiry=1787783644677; path=/; SameSite=Strict; HttpOnly",
                @"brokerToken=AdbBroker789;Version=1;Comment=;Domain=;Path=/essbase;Max-Age=2147483647;HttpOnly",
                @"gatewayToken=AdbGateway000;Version=1;Comment=;Domain=;Path=/essbase;Max-Age=2147483647;HttpOnly");

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session and assert that the full cookie set was retained, parsed and recovered alike.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);

            var sessionCookies = Assert.Single(client.SessionCookies);

            foreach ( var name in new[] { @"JSESSIONID", @"essbaseToken", @"sessionExpiry", @"brokerToken", @"gatewayToken" } )
                Assert.Contains(sessionCookies.Cast<Cookie>(), cookie => string.Equals(cookie?.Name, name, StringComparison.OrdinalIgnoreCase));

            // Get a session again and assert that the full set rode the request in place of authorization.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            AssertSecondRequestRodeTheSession(stub, @"JSESSIONID=AdbSession123", @"essbaseToken=AdbEssbase456", @"brokerToken=AdbBroker789", @"gatewayToken=AdbGateway000");
        }

        [Fact(DisplayName = "SessionCookieTests - ApiClient_AfterSignoff_RetainsNoSessionCookies")]
        public async Task ApiClient_AfterSignoff_RetainsNoSessionCookies()
        {
            // Create a stub server that returns a session cookie with an empty Domain= attribute.
            using var stub = new LoopbackEssbaseStub(@"JSESSIONID=LogoutCookie789; Path=/essbase; Domain=; HttpOnly");

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session and assert that the session cookie was retained.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            AssertRetainedSessionCookie(client, @"LogoutCookie789");

            // Sign off and assert that no session cookies remain, even though the logout response sets one.
            await api.UserSessionSignoffWithHttpInfoAsync(cancellationToken: TestContext.Current.CancellationToken);
            Assert.Empty(client.SessionCookies);
        }

        [Fact(DisplayName = "SessionCookieTests - ApiClient_AgainstForkingServer_FollowsTheNewestSessionCookie")]
        public async Task ApiClient_AgainstForkingServer_FollowsTheNewestSessionCookie()
        {
            // Create a stub server that forks the session on every request, like the Essbase grid API:
            // each response returns a new JSESSIONID.
            using var stub = new LoopbackEssbaseStub(index => new[] { $@"JSESSIONID=Fork{index}; Path=/; HttpOnly" });

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session three times.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(3, stub.Requests.Count);

            // Assert that each request rode the cookie from the previous response.
            Assert.Contains(@"jsessionid=fork0", stub.Requests[1].ToLowerInvariant());
            Assert.Contains(@"jsessionid=fork1", stub.Requests[2].ToLowerInvariant());

            // Assert that the newest fork is the retained cookie.
            AssertRetainedSessionCookie(client, @"Fork2");
        }

        [Fact(DisplayName = "SessionCookieTests - ApiClient_AgainstNonForkingServer_KeepsRidingTheSameSession")]
        public async Task ApiClient_AgainstNonForkingServer_KeepsRidingTheSameSession()
        {
            // Create a stub server that issues a session cookie once and never again, like a server
            // where the grid API no longer forks the session.
            using var stub = new LoopbackEssbaseStub(index => index is 0 ? new[] { @"JSESSIONID=Stable123; Path=/; HttpOnly" } : Array.Empty<string>());

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session three times.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(3, stub.Requests.Count);

            // Assert that the session cookie kept riding even though later responses never re-issued it.
            Assert.Contains(@"jsessionid=stable123", stub.Requests[1].ToLowerInvariant());
            Assert.Contains(@"jsessionid=stable123", stub.Requests[2].ToLowerInvariant());
            Assert.DoesNotContain(@"authorization:", stub.Requests[2].ToLowerInvariant());

            // Assert that the session cookie is still retained.
            AssertRetainedSessionCookie(client, @"Stable123");
        }

        [Fact(DisplayName = "SessionCookieTests - ApiClient_WithGridPreferencesAndNoSession_RidesThePreferenceSession")]
        public async Task ApiClient_WithGridPreferencesAndNoSession_RidesThePreferenceSession()
        {
            // Create a stub server that issues a session cookie only for the first (preference) request.
            using var stub = new LoopbackEssbaseStub(index => index is 0 ? new[] { @"JSESSIONID=PrefSession42; Path=/; HttpOnly" } : Array.Empty<string>());

            // Create a client and a configuration for the stub.
            var configuration = new Configuration() { BasePath = stub.BasePath, Username = "admin", Password = "password1" };
            var client        = new ApiClient(stub.BasePath);

            // Get a session with configured grid preferences.
            await client.GetAsync<object>(@"/session", CreatePreferenceOptions(), configuration, TestContext.Current.CancellationToken);

            Assert.Equal(2, stub.Requests.Count);

            var preferencesRequest = stub.Requests[0].ToLowerInvariant();
            var mainRequest        = stub.Requests[1].ToLowerInvariant();

            // Assert that the preferences were set first, on a new session, with basic authorization.
            Assert.StartsWith(@"put /essbase/rest/v1/preferences/grid", preferencesRequest);
            Assert.Contains(@"authorization: basic", preferencesRequest);

            // Assert that the main request rode the new preference session in place of authorization.
            Assert.StartsWith(@"get /essbase/rest/v1/session", mainRequest);
            Assert.Contains(@"jsessionid=prefsession42", mainRequest);
            Assert.DoesNotContain(@"authorization:", mainRequest);

            // Assert that the preference session cookie was retained.
            AssertRetainedSessionCookie(client, @"PrefSession42");
        }

        [Fact(DisplayName = "SessionCookieTests - ApiClient_WithMatchingGridPreferences_SkipsResettingThem")]
        public async Task ApiClient_WithMatchingGridPreferences_SkipsResettingThem()
        {
            // Create a stub server that issues a session cookie only for the first (preference) request.
            using var stub = new LoopbackEssbaseStub(index => index is 0 ? new[] { @"JSESSIONID=PrefSession42; Path=/; HttpOnly" } : Array.Empty<string>());

            // Create a client and a configuration for the stub.
            var configuration = new Configuration() { BasePath = stub.BasePath, Username = "admin", Password = "password1" };
            var client        = new ApiClient(stub.BasePath);

            // Get a session with configured grid preferences twice.
            await client.GetAsync<object>(@"/session", CreatePreferenceOptions(), configuration, TestContext.Current.CancellationToken);
            await client.GetAsync<object>(@"/session", CreatePreferenceOptions(), configuration, TestContext.Current.CancellationToken);

            // Assert that the preferences were set only once: the second request matched the preferences
            // tracked for the retained session and rode it directly.
            Assert.Equal(3, stub.Requests.Count);

            var secondRequest = stub.Requests[2].ToLowerInvariant();

            Assert.StartsWith(@"get /essbase/rest/v1/session", secondRequest);
            Assert.Contains(@"jsessionid=prefsession42", secondRequest);
            Assert.DoesNotContain(@"authorization:", secondRequest);
        }

        [Fact(DisplayName = "SessionCookieTests - ApiClient_WithGridPreferencesAndEmptyDomainSessionCookie_RecoversAndRidesIt")]
        public async Task ApiClient_WithGridPreferencesAndEmptyDomainSessionCookie_RecoversAndRidesIt()
        {
            // Create a stub server that issues a malformed session cookie only for the first (preference) request.
            using var stub = new LoopbackEssbaseStub(index => index is 0 ? new[] { @"JSESSIONID=PrefBroken77; Path=/; Domain=; HttpOnly" } : Array.Empty<string>());

            // Create a client and a configuration for the stub.
            var configuration = new Configuration() { BasePath = stub.BasePath, Username = "admin", Password = "password1" };
            var client        = new ApiClient(stub.BasePath);

            // Get a session with configured grid preferences.
            await client.GetAsync<object>(@"/session", CreatePreferenceOptions(), configuration, TestContext.Current.CancellationToken);

            Assert.Equal(2, stub.Requests.Count);

            var mainRequest = stub.Requests[1].ToLowerInvariant();

            // Assert that the main request rode the recovered preference session in place of authorization.
            Assert.Contains(@"jsessionid=prefbroken77", mainRequest);
            Assert.DoesNotContain(@"authorization:", mainRequest);

            // Assert that the recovered preference session cookie was retained.
            AssertRetainedSessionCookie(client, @"PrefBroken77");
        }

        [Fact(DisplayName = "SessionCookieTests - ApiClient_WithConcurrentRequests_RidesDistinctSessions")]
        public async Task ApiClient_WithConcurrentRequests_RidesDistinctSessions()
        {
            // Create a stub server that issues a distinct session cookie per request and holds every
            // response until both requests have arrived, so the requests are genuinely concurrent.
            using var stub = new LoopbackEssbaseStub(index => new[] { $@"JSESSIONID=Parallel{index}; Path=/; HttpOnly" }) { HoldResponsesUntilRequestCount = 2 };

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get two sessions concurrently.
            await Task.WhenAll(
                api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken),
                api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken));

            Assert.Equal(2, stub.Requests.Count);

            // Assert that both requests authenticated with basic authorization: neither found a pooled
            // session, so neither rode one, and Essbase never sees concurrent requests on one session.
            Assert.Contains(@"authorization: basic", stub.Requests[0].ToLowerInvariant());
            Assert.Contains(@"authorization: basic", stub.Requests[1].ToLowerInvariant());

            // Assert that two distinct sessions were pooled.
            Assert.Equal(2, client.SessionCookies.Count);

            var values = client.SessionCookies
                .Select(sessionCookies => sessionCookies.Cast<Cookie>().FirstOrDefault(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase))?.Value)
                .OrderBy(value => value, StringComparer.Ordinal)
                .ToArray();

            Assert.Equal(new[] { @"Parallel0", @"Parallel1" }, values);
        }

        [Fact(DisplayName = "SessionCookieTests - MDXExecuteMDXAsync_AcceptsAllRequiredResponseTypes")]
        public async Task MDXExecuteMDXAsync_AcceptsAllRequiredResponseTypes()
        {
            using var stub = new LoopbackEssbaseStub();
            var api = CreateMdxApi(stub);

            await api.MDXExecuteMDXWithHttpInfoAsync(@"Sample", @"Basic", cancellationToken: TestContext.Current.CancellationToken);

            var request = Assert.Single(stub.Requests);

            Assert.Contains(ExpectedMdxAcceptHeader, request, StringComparison.OrdinalIgnoreCase);
        }

        [Fact(DisplayName = "SessionCookieTests - MDXExecuteMDX_AcceptsAllRequiredResponseTypes")]
        public void MDXExecuteMDX_AcceptsAllRequiredResponseTypes()
        {
            using var stub = new LoopbackEssbaseStub();
            var api = CreateMdxApi(stub);

            api.MDXExecuteMDXWithHttpInfo(@"Sample", @"Basic");

            var request = Assert.Single(stub.Requests);

            Assert.Contains(ExpectedMdxAcceptHeader, request, StringComparison.OrdinalIgnoreCase);
        }

        [Fact(DisplayName = "SessionCookieTests - SelectHeaderAccept_ForExistingMultiFormatEndpoints_StillPrefersJson")]
        public void SelectHeaderAccept_ForExistingMultiFormatEndpoints_StillPrefersJson()
        {
            var accept = ClientUtils.SelectHeaderAccept(new[]
            {
                @"application/zip",
                @"application/octet-stream",
                @"application/json",
                @"application/xml"
            });

            Assert.Equal(@"application/json", accept);
        }

        /// <summary>
        /// Creates a direct MDX API for the given stub.
        /// </summary>
        /// <param name="stub">The loopback stub server.</param>
        private static ExecuteMDXApi CreateMdxApi( LoopbackEssbaseStub stub )
        {
            return new ExecuteMDXApi(new Configuration
            {
                BasePath = stub.BasePath,
                Username = @"admin",
                Password = @"password1"
            });
        }

        /// <summary>
        /// Creates request options with configured grid preferences and a basic authorization header,
        /// as the generated API surfaces add one before the request is intercepted.
        /// </summary>
        private static RequestOptions CreatePreferenceOptions()
        {
            var options = new RequestOptions() { Preferences = new EssGridPreferences() };

            options.HeaderParameters.Add(@"Authorization", @"Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(@"admin:password1")));

            return options;
        }

        /// <summary>
        /// Creates a session API around a new <see cref="ApiClient"/> and <see cref="Configuration"/> for the given stub.
        /// </summary>
        /// <param name="stub">The loopback stub server.</param>
        private static (ApiClient client, UserSessionApi api) CreateSessionApi( LoopbackEssbaseStub stub )
        {
            var configuration = new Configuration()
            {
                BasePath = stub.BasePath,
                Username = "admin",
                Password = "password1"
            };

            var client = new ApiClient(stub.BasePath);

            return (client, new UserSessionApi(client, client, configuration));
        }

        /// <summary>
        /// Asserts that the client pooled exactly one session cookie set whose JSESSIONID carries the given value.
        /// </summary>
        /// <param name="client">The API client.</param>
        /// <param name="value">The expected cookie value.</param>
        private static Cookie AssertRetainedSessionCookie( ApiClient client, string value )
        {
            var sessionCookies = Assert.Single(client.SessionCookies);
            var cookie         = sessionCookies.Cast<Cookie>().FirstOrDefault(retained => string.Equals(retained?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase));

            Assert.NotNull(cookie);
            Assert.Equal(value, cookie.Value);

            return cookie;
        }

        /// <summary>
        /// Asserts that the first request carried basic authorization and that the second request
        /// carried the given cookie pairs in place of an authorization header.
        /// </summary>
        /// <param name="stub">The loopback stub server.</param>
        /// <param name="cookiePairs">The name=value cookie pairs expected on the second request.</param>
        private static void AssertSecondRequestRodeTheSession( LoopbackEssbaseStub stub, params string[] cookiePairs )
        {
            Assert.Equal(2, stub.Requests.Count);

            var first  = stub.Requests[0].ToLowerInvariant();
            var second = stub.Requests[1].ToLowerInvariant();

            // Assert that the first request authenticated with basic authorization.
            Assert.Contains(@"authorization: basic", first);

            // Assert that the second request carried the cookie pairs and no authorization header.
            foreach ( var cookiePair in cookiePairs )
                Assert.Contains(cookiePair.ToLowerInvariant(), second);

            Assert.DoesNotContain(@"authorization:", second);
        }
    }
}
