using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Integration.Setup;

using Xunit;
using Xunit.Abstractions;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />
    /// </summary>
    [CollectionDefinition(nameof(SessionCookieClientTests), DisableParallelization = true), CollectionPriority(0)]
    public class SessionCookieClientTestsCollection : ICollectionFixture<CollectionFixture> { }

    /// <summary>
    /// Offline tests for session cookie retention, recovery, and pooling against a loopback stub,
    /// including Set-Cookie headers with an empty Domain= attribute, which some fronting proxies and
    /// load balancers (including the Essbase on Autonomous Database gateway) emit and cookie parsing
    /// rejects with a CookieException. These tests require no Essbase server or container.
    /// </summary>
    [Collection(nameof(SessionCookieClientTests)), Trait("type", "client")]
    public class SessionCookieClientTests
    {
        [Fact(DisplayName = "SessionCookieClientTests - 01 - ApiClient_WithWellFormedSessionCookie_RetainsAndReusesIt"), Priority(01)]
        public async Task ApiClient_WithWellFormedSessionCookie_RetainsAndReusesIt()
        {
            // Create a stub server that returns a well-formed session cookie.
            using var stub = new LoopbackEssbaseStub(@"JSESSIONID=WellFormed456; Path=/essbase; HttpOnly");

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session and assert that the session cookie was retained.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            AssertRetainedSessionCookie(client, @"WellFormed456");

            // Get a session again and assert that the retained cookie rode the request in place of authorization.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            AssertSecondRequestRodeTheSession(stub, @"JSESSIONID=WellFormed456");
        }

        [Fact(DisplayName = "SessionCookieClientTests - 02 - ApiClient_WithEmptyDomainSessionCookie_RecoversAndReusesIt"), Priority(02)]
        public async Task ApiClient_WithEmptyDomainSessionCookie_RecoversAndReusesIt()
        {
            // Create a stub server that returns a session cookie with an empty Domain= attribute.
            using var stub = new LoopbackEssbaseStub(@"JSESSIONID=EmptyDomain123; Path=/essbase; Domain=; HttpOnly");

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session and assert that the session cookie was recovered despite the malformed domain.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            var cookie = AssertRetainedSessionCookie(client, @"EmptyDomain123");

            // Assert that the recovered cookie is scoped host-only to the stub host with the cookie path.
            Assert.Equal(IPAddress.Loopback.ToString(), cookie.Domain);
            Assert.Equal(@"/essbase", cookie.Path);

            // Get a session again and assert that the recovered cookie rode the request in place of authorization.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            AssertSecondRequestRodeTheSession(stub, @"JSESSIONID=EmptyDomain123");

            // Assert that the recovered cookie was retained again from the second response.
            AssertRetainedSessionCookie(client, @"EmptyDomain123");
        }

        [Fact(DisplayName = "SessionCookieClientTests - 03 - ApiClient_WithGatewayCookieSet_ReplaysTheFullSet"), Priority(03)]
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
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);

            var sessionCookies = Assert.Single(client.SessionCookies);

            foreach ( var name in new[] { @"JSESSIONID", @"essbaseToken", @"sessionExpiry", @"brokerToken", @"gatewayToken" } )
                Assert.Contains(sessionCookies.Cast<Cookie>(), cookie => string.Equals(cookie?.Name, name, StringComparison.OrdinalIgnoreCase));

            // Get a session again and assert that the full set rode the request in place of authorization.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            AssertSecondRequestRodeTheSession(stub, @"JSESSIONID=AdbSession123", @"essbaseToken=AdbEssbase456", @"brokerToken=AdbBroker789", @"gatewayToken=AdbGateway000");
        }

        [Fact(DisplayName = "SessionCookieClientTests - 04 - ApiClient_AfterSignoff_RetainsNoSessionCookies"), Priority(04)]
        public async Task ApiClient_AfterSignoff_RetainsNoSessionCookies()
        {
            // Create a stub server that returns a session cookie with an empty Domain= attribute.
            using var stub = new LoopbackEssbaseStub(@"JSESSIONID=LogoutCookie789; Path=/essbase; Domain=; HttpOnly");

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session and assert that the session cookie was retained.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            AssertRetainedSessionCookie(client, @"LogoutCookie789");

            // Sign off and assert that no session cookies remain, even though the logout response sets one.
            await api.UserSessionSignoffWithHttpInfoAsync();
            Assert.Empty(client.SessionCookies);
        }

        [Fact(DisplayName = "SessionCookieClientTests - 05 - ApiClient_AgainstForkingServer_FollowsTheNewestSessionCookie"), Priority(05)]
        public async Task ApiClient_AgainstForkingServer_FollowsTheNewestSessionCookie()
        {
            // Create a stub server that forks the session on every request, like the Essbase grid API:
            // each response returns a new JSESSIONID.
            using var stub = new LoopbackEssbaseStub(index => new[] { $@"JSESSIONID=Fork{index}; Path=/; HttpOnly" });

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session three times.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);

            Assert.Equal(3, stub.Requests.Count);

            // Assert that each request rode the cookie from the previous response.
            Assert.Contains(@"jsessionid=fork0", stub.Requests[1].ToLowerInvariant());
            Assert.Contains(@"jsessionid=fork1", stub.Requests[2].ToLowerInvariant());

            // Assert that the newest fork is the retained cookie.
            AssertRetainedSessionCookie(client, @"Fork2");
        }

        [Fact(DisplayName = "SessionCookieClientTests - 06 - ApiClient_AgainstNonForkingServer_KeepsRidingTheSameSession"), Priority(06)]
        public async Task ApiClient_AgainstNonForkingServer_KeepsRidingTheSameSession()
        {
            // Create a stub server that issues a session cookie once and never again, like a server
            // where the grid API no longer forks the session.
            using var stub = new LoopbackEssbaseStub(index => index is 0 ? new[] { @"JSESSIONID=Stable123; Path=/; HttpOnly" } : Array.Empty<string>());

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get a session three times.
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            await api.UserSessionGetSessionWithHttpInfoAsync(token: true);

            Assert.Equal(3, stub.Requests.Count);

            // Assert that the session cookie kept riding even though later responses never re-issued it.
            Assert.Contains(@"jsessionid=stable123", stub.Requests[1].ToLowerInvariant());
            Assert.Contains(@"jsessionid=stable123", stub.Requests[2].ToLowerInvariant());
            Assert.DoesNotContain(@"authorization:", stub.Requests[2].ToLowerInvariant());

            // Assert that the session cookie is still retained.
            AssertRetainedSessionCookie(client, @"Stable123");
        }

        [Fact(DisplayName = "SessionCookieClientTests - 07 - ApiClient_WithGridPreferencesAndNoSession_RidesThePreferenceSession"), Priority(07)]
        public async Task ApiClient_WithGridPreferencesAndNoSession_RidesThePreferenceSession()
        {
            // Create a stub server that issues a session cookie only for the first (preference) request.
            using var stub = new LoopbackEssbaseStub(index => index is 0 ? new[] { @"JSESSIONID=PrefSession42; Path=/; HttpOnly" } : Array.Empty<string>());

            // Create a client and a configuration for the stub.
            var configuration = new Configuration() { BasePath = stub.BasePath, Username = "admin", Password = "password1" };
            var client        = new ApiClient(stub.BasePath);

            // Get a session with configured grid preferences.
            await client.GetAsync<object>(@"/session", CreatePreferenceOptions(), configuration);

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

        [Fact(DisplayName = "SessionCookieClientTests - 08 - ApiClient_WithMatchingGridPreferences_SkipsResettingThem"), Priority(08)]
        public async Task ApiClient_WithMatchingGridPreferences_SkipsResettingThem()
        {
            // Create a stub server that issues a session cookie only for the first (preference) request.
            using var stub = new LoopbackEssbaseStub(index => index is 0 ? new[] { @"JSESSIONID=PrefSession42; Path=/; HttpOnly" } : Array.Empty<string>());

            // Create a client and a configuration for the stub.
            var configuration = new Configuration() { BasePath = stub.BasePath, Username = "admin", Password = "password1" };
            var client        = new ApiClient(stub.BasePath);

            // Get a session with configured grid preferences twice.
            await client.GetAsync<object>(@"/session", CreatePreferenceOptions(), configuration);
            await client.GetAsync<object>(@"/session", CreatePreferenceOptions(), configuration);

            // Assert that the preferences were set only once: the second request matched the preferences
            // tracked for the retained session and rode it directly.
            Assert.Equal(3, stub.Requests.Count);

            var secondRequest = stub.Requests[2].ToLowerInvariant();

            Assert.StartsWith(@"get /essbase/rest/v1/session", secondRequest);
            Assert.Contains(@"jsessionid=prefsession42", secondRequest);
            Assert.DoesNotContain(@"authorization:", secondRequest);
        }

        [Fact(DisplayName = "SessionCookieClientTests - 09 - ApiClient_WithGridPreferencesAndEmptyDomainSessionCookie_RecoversAndRidesIt"), Priority(09)]
        public async Task ApiClient_WithGridPreferencesAndEmptyDomainSessionCookie_RecoversAndRidesIt()
        {
            // Create a stub server that issues a malformed session cookie only for the first (preference) request.
            using var stub = new LoopbackEssbaseStub(index => index is 0 ? new[] { @"JSESSIONID=PrefBroken77; Path=/; Domain=; HttpOnly" } : Array.Empty<string>());

            // Create a client and a configuration for the stub.
            var configuration = new Configuration() { BasePath = stub.BasePath, Username = "admin", Password = "password1" };
            var client        = new ApiClient(stub.BasePath);

            // Get a session with configured grid preferences.
            await client.GetAsync<object>(@"/session", CreatePreferenceOptions(), configuration);

            Assert.Equal(2, stub.Requests.Count);

            var mainRequest = stub.Requests[1].ToLowerInvariant();

            // Assert that the main request rode the recovered preference session in place of authorization.
            Assert.Contains(@"jsessionid=prefbroken77", mainRequest);
            Assert.DoesNotContain(@"authorization:", mainRequest);

            // Assert that the recovered preference session cookie was retained.
            AssertRetainedSessionCookie(client, @"PrefBroken77");
        }

        [Fact(DisplayName = "SessionCookieClientTests - 10 - ApiClient_WithConcurrentRequests_RidesDistinctSessions"), Priority(10)]
        public async Task ApiClient_WithConcurrentRequests_RidesDistinctSessions()
        {
            // Create a stub server that issues a distinct session cookie per request and holds every
            // response until both requests have arrived, so the requests are genuinely concurrent.
            using var stub = new LoopbackEssbaseStub(index => new[] { $@"JSESSIONID=Parallel{index}; Path=/; HttpOnly" }) { HoldResponsesUntilRequestCount = 2 };

            // Create a session API around a client for the stub.
            var (client, api) = CreateSessionApi(stub);

            // Get two sessions concurrently.
            await Task.WhenAll(
                api.UserSessionGetSessionWithHttpInfoAsync(token: true),
                api.UserSessionGetSessionWithHttpInfoAsync(token: true));

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

    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />
    /// </summary>
    [CollectionDefinition(nameof(SessionCookieServerTests), DisableParallelization = true), CollectionPriority(2)]
    public class SessionCookieServerTestsCollection : ICollectionFixture<CollectionFixture> { }

    /// <summary>
    /// Read-only tests for session cookie retention against the configured server. These issue only
    /// GET /session requests, so they are safe to run against a server that is not otherwise disposable.
    /// </summary>
    [Collection(nameof(SessionCookieServerTests)), Trait("type", "server")]
    public class SessionCookieServerTests : IntegrationTestBase
    {
        /// <summary />
        /// <param name="output" />
        public SessionCookieServerTests( ITestOutputHelper output ) : base(output) { }

        [Fact(DisplayName = "SessionCookieServerTests - 01 - Essbase_AfterConnect_RetainsSessionCookieAcrossRequests"), Priority(01)]
        public async Task Essbase_AfterConnect_RetainsSessionCookieAcrossRequests()
        {
            // Get a server connection (for the service admin).
            var connection = GetEssConnection();

            // Create a configuration for the connection.
            var configuration = new Configuration()
            {
                BasePath = $"{connection.Server?.TrimEnd('/')}/rest/v1",
                Username = connection.Username,
                Password = connection.Password
            };

            // Apply an access token, if one is configured.
            if ( connection.AccessToken is { Length: > 0 } accessToken )
                configuration.AccessToken = accessToken;

            // Create a session API around a client for the configuration.
            var client = new ApiClient(configuration.BasePath);
            var api    = new UserSessionApi(client, client, configuration);

            // Get a session and assert success.
            var first = await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            Assert.Equal(HttpStatusCode.OK, first.StatusCode);

            // Assert that a session cookie set with a JSESSIONID was pooled, whether parsed or recovered.
            Assert.Contains(client.SessionCookies, sessionCookies => sessionCookies.Cast<Cookie>().Any(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase)));

            // Get a session again, riding the pooled set, and assert success.
            var second = await api.UserSessionGetSessionWithHttpInfoAsync(token: true);
            Assert.Equal(HttpStatusCode.OK, second.StatusCode);

            // Assert that a session cookie set with a JSESSIONID was pooled again from the second response.
            Assert.Contains(client.SessionCookies, sessionCookies => sessionCookies.Cast<Cookie>().Any(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase)));
        }
    }

    /// <summary>
    /// A minimal loopback HTTP server that answers every request with a small JSON body and scripted
    /// Set-Cookie headers emitted verbatim, so malformed cookie values can be exercised byte-for-byte
    /// on the wire. Connections are handled concurrently, and responses can be held until a given
    /// number of requests have arrived to force genuine request concurrency.
    /// </summary>
    internal sealed class LoopbackEssbaseStub : IDisposable
    {
        private readonly TcpListener _listener;
        private volatile bool _disposed;

        /// <summary />
        /// <param name="setCookieHeaders">The Set-Cookie header values to emit verbatim on every response.</param>
        public LoopbackEssbaseStub( params string[] setCookieHeaders )
            : this(_ => setCookieHeaders) { }

        /// <summary />
        /// <param name="setCookieProvider">Returns the Set-Cookie header values to emit for the request at a given index.</param>
        public LoopbackEssbaseStub( Func<int, string[]> setCookieProvider )
        {
            SetCookieProvider = setCookieProvider ?? (_ => Array.Empty<string>());

            _listener = new TcpListener(IPAddress.Loopback, 0);
            _listener.Start();

            BasePath = $"http://{IPAddress.Loopback}:{((IPEndPoint)_listener.LocalEndpoint).Port}/essbase/rest/v1";

            _ = Task.Run(AcceptLoopAsync);
        }

        /// <summary>
        /// The stub base path, e.g. http://127.0.0.1:{port}/essbase/rest/v1.
        /// </summary>
        public string BasePath { get; }

        /// <summary>
        /// Holds every response until at least this many requests have arrived (0 = respond immediately).
        /// </summary>
        public int HoldResponsesUntilRequestCount { get; set; }

        /// <summary>
        /// The raw text of each received request, in order of arrival.
        /// </summary>
        public List<string> Requests { get; } = new List<string>();

        /// <summary>
        /// Returns the Set-Cookie header values to emit for the request at a given index.
        /// </summary>
        public Func<int, string[]> SetCookieProvider { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            _disposed = true;

            try { _listener.Stop(); } catch { /* ignored */ }
        }

        /// <summary>
        /// Accepts connections until disposed, handling each concurrently.
        /// </summary>
        private async Task AcceptLoopAsync()
        {
            while ( !_disposed )
            {
                TcpClient client = null;

                try
                {
                    client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);
                }
                catch
                {
                    client?.Dispose();
                    break;
                }

                _ = Task.Run(() => HandleConnectionAsync(client));
            }
        }

        /// <summary>
        /// Reads one request from the given connection and answers it.
        /// </summary>
        /// <param name="client">The accepted connection.</param>
        private async Task HandleConnectionAsync( TcpClient client )
        {
            try
            {
                using var stream = client.GetStream();

                // Read the request through its headers and any body.
                var request = await ReadRequestAsync(stream).ConfigureAwait(false);

                if ( string.IsNullOrEmpty(request) )
                    return;

                int index;

                lock ( Requests )
                {
                    Requests.Add(request);
                    index = Requests.Count - 1;
                }

                // Hold the response until the configured number of requests has arrived.
                while ( !_disposed && HoldResponsesUntilRequestCount > 0 )
                {
                    lock ( Requests )
                    {
                        if ( Requests.Count >= HoldResponsesUntilRequestCount )
                            break;
                    }

                    await Task.Delay(10).ConfigureAwait(false);
                }

                // Compose a small JSON body that deserializes as a UserBean (or is ignored).
                var body = @"{""id"":""admin"",""name"":""admin"",""token"":""stub-token""}";

                var builder = new StringBuilder()
                    .Append("HTTP/1.1 200 OK\r\n")
                    .Append("Content-Type: application/json\r\n")
                    .Append($"Content-Length: {Encoding.UTF8.GetByteCount(body)}\r\n");

                foreach ( var setCookieHeader in SetCookieProvider(index) ?? Array.Empty<string>() )
                    builder.Append($"Set-Cookie: {setCookieHeader}\r\n");

                var bytes = Encoding.UTF8.GetBytes(builder.Append("Connection: close\r\n\r\n").Append(body).ToString());

                await stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                await stream.FlushAsync().ConfigureAwait(false);
            }
            catch
            {
                // Ignore failed connections; the test assertions will surface any missing exchange.
            }
            finally
            {
                client?.Dispose();
            }
        }

        /// <summary>
        /// Reads a request through its headers and, when a Content-Length is present, its body.
        /// </summary>
        /// <param name="stream">The connection stream.</param>
        private static async Task<string> ReadRequestAsync( NetworkStream stream )
        {
            var buffer  = new byte[8192];
            var builder = new StringBuilder();

            // Read through the end of the headers.
            while ( builder.ToString().IndexOf("\r\n\r\n", StringComparison.Ordinal) < 0 )
            {
                var read = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

                if ( read <= 0 )
                    return builder.ToString();

                builder.Append(Encoding.ASCII.GetString(buffer, 0, read));
            }

            var text        = builder.ToString();
            var headerEnd   = text.IndexOf("\r\n\r\n", StringComparison.Ordinal) + 4;
            var contentLine = text.Split(new[] { "\r\n" }, StringSplitOptions.None).FirstOrDefault(line => line.StartsWith("Content-Length:", StringComparison.OrdinalIgnoreCase));

            // Drain any remaining body bytes indicated by a Content-Length header, so the connection
            // is not closed with unread data (which can reset the client).
            if ( contentLine?.Split(':') is { Length: 2 } parts && int.TryParse(parts[1].Trim(), out var contentLength) )
            {
                var remaining = contentLength - Encoding.ASCII.GetByteCount(text.Substring(headerEnd));

                while ( remaining > 0 )
                {
                    var read = await stream.ReadAsync(buffer, 0, Math.Min(buffer.Length, remaining)).ConfigureAwait(false);

                    if ( read <= 0 )
                        break;

                    builder.Append(Encoding.ASCII.GetString(buffer, 0, read));
                    remaining -= read;
                }
            }

            return builder.ToString();
        }
    }
}
