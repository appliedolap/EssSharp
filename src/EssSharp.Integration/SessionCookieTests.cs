using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
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
            var first = await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(HttpStatusCode.OK, first.StatusCode);

            // Assert that a session cookie set with a JSESSIONID was pooled, whether parsed or recovered.
            Assert.Contains(client.SessionCookies, sessionCookies => sessionCookies.Cast<Cookie>().Any(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase)));

            // Get a session again, riding the pooled set, and assert success.
            var second = await api.UserSessionGetSessionWithHttpInfoAsync(token: true, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(HttpStatusCode.OK, second.StatusCode);

            // Assert that a session cookie set with a JSESSIONID was pooled again from the second response.
            Assert.Contains(client.SessionCookies, sessionCookies => sessionCookies.Cast<Cookie>().Any(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase)));
        }
    }
}
