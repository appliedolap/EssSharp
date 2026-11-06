using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />
    /// </summary>
    [CollectionDefinition(nameof(AvailableServerTests), DisableParallelization = true), CollectionPriority(2)]
    public class AvailableServerTestsCollection : ICollectionFixture<CollectionFixture> { }

    /// <summary>
    /// Tests for server availability.
    /// </summary>
    [Collection(nameof(AvailableServerTests)), Trait("type", "server")]
    public class AvailableServerTests : IntegrationTestBase
    {
        /// <summary />
        /// <param name="output" />
        public AvailableServerTests( ITestOutputHelper output ) : base(output) { }

        [Fact(DisplayName = "AvailableServerTests - 01 - Essbase_AfterStartup_IsRestApiReady"), Priority(01)]
        public async Task Essbase_AfterStartup_IsRestApiReady()
        {
            // Get a server connection.
            var connection = GetEssConnection();

            // Poll for the REST API and capture a status code for an unauthorized request.
            var statusCode = await PollForRestfulApiAndReturnStatusCodeAsync();

            // Assert that the REST API is available and returns a 401 status code (for unauthorized requests).
            Assert.Equal(HttpStatusCode.Unauthorized, statusCode);

            // A new domain in Identity Cloud mode answers before the administrator's service role has settled: its first
            // sign-in can be refused (403, "Service role [SERVICE_ADMINISTRATOR] assignment failed") and the next one
            // succeeds. Wait for that too, so that the tests after this one start against a server that is ready.
            if (IntegrationTestFactory.EssbaseInIdentityCloudMode is true)
                await WaitForAdministratorSessionAsync();

            // Local polling function for the administrator's first session.
            async Task WaitForAdministratorSessionAsync()
            {
                using var client = new HttpClient() { BaseAddress = new Uri($"{connection.Server.TrimEnd('/')}/") };

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{connection.Username}:{connection.Password}")));

                for (int i = 0; i < TimeSpan.FromMinutes(2).TotalSeconds / 5; i++)
                {
                    using var response = await client.GetAsync(@"rest/v1/session");

                    if (response.IsSuccessStatusCode)
                        return;

                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }

            // Local polling function.
            async Task<HttpStatusCode> PollForRestfulApiAndReturnStatusCodeAsync()
            {
                // Get a new client with the connection's base address.
                using var client = new HttpClient() { BaseAddress = new Uri($"{connection.Server.TrimEnd('/')}/") };

                // Wait up to 20 minutes for the REST API to become available.
                for ( int i = 0; i < TimeSpan.FromMinutes(20).TotalSeconds / 5; i++ )
                {
                    try
                    {
                        // Poll the "rest/v1/" endpoint for a proper response.
                        var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri(client.BaseAddress, @"rest/v1/")));

                        // Return any status code except a GatewayTimeout...
                        if ( response.StatusCode is not HttpStatusCode.GatewayTimeout )
                            return response.StatusCode;
                    }
                    catch ( HttpRequestException hre )
                    {
                        // The HttpRequestException's StatusCode will be null until the server becomes available.
                        if ( hre.StatusCode.HasValue )
                            return hre.StatusCode.Value;
                    }

                    // Wait 5 seconds.
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }

                // Return service unavailable.
                return HttpStatusCode.ServiceUnavailable;
            }
        }

        [Fact(DisplayName = "AvailableServerTests - 02 - Essbase_AfterStartup_CanConnect"), Priority(02)]
        public async Task Essbase_AfterStartup_CanConnect()
        {
            // Get a user session (for the service admin).
            var session = await GetEssServer().SignInAsync(cancellationToken: TestContext.Current.CancellationToken);

            // Get the configured username (for the service admin).
            var username = GetEssConnection().Username;

            // Assert that the configured username matches the session's user ID.
            Assert.Equal(username, session.UserId);
        }

        [Fact(DisplayName = "AvailableServerTests - 03 - Essbase_AfterStartup_ReportsTheSecurityModeItWasCreatedIn"), Priority(03)]
        public async Task Essbase_AfterStartup_ReportsTheSecurityModeItWasCreatedIn()
        {
            // Only a container the harness started or adopted says how the server was created.
            var identityCloud = IntegrationTestFactory.EssbaseInIdentityCloudMode;

            Assert.SkipUnless(identityCloud.HasValue, "The server under test is not an Essbase container of the harness's, so there is no security mode to expect.");

            var expected = identityCloud is true ? EssSecurityMode.IdentityCloud : EssSecurityMode.Native;

            // Assert that the server reports the security mode its container was created in (IDENTITY_PROVIDER).
            Assert.Equal(expected, await GetEssServer().GetSecurityModeAsync(TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "AvailableServerTests - 04 - Essbase_AfterStartup_CannotConnectWithBadCredentials"), Priority(04)]
        public async Task Essbase_AfterStartup_CannotConnectWithBadCredentials()
        {
            // Get a connection (and invalidate the credentials).
            var connection = GetEssConnection();
            {
                connection.Username = "badmin";
                connection.Password = "badword";
            }

            // Assert that the sign in attempt throws an exception.
            var exception = await Assert.ThrowsAsync<Exception>(async () =>
            {
                // Attempt to sign in with bad credentials.
                await new EssServerFactory()
                    .CreateEssServer(connection.Server, connection.Username, connection.Password, connect: false)
                    .SignInAsync(cancellationToken: TestContext.Current.CancellationToken);
            });

            // Assert that the message is appropriate.
            Assert.Contains(@"Verify that the credentials are valid", exception.Message);

            // Assert that the inner exception is a WebException.
            var webException = Assert.IsType<WebException>(exception.InnerException);

            // Assert that the response is a WebExceptionRestResponse.
            var response = Assert.IsType<WebExceptionRestResponse>(webException.Response);

            // Assert that the status code was 401/unauthorized.
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact(DisplayName = "AvailableServerTests - 05 - Essbase_AfterDeletingUsersInARow_StillAnswers"), Priority(05)]
        public async Task Essbase_AfterDeletingUsersInARow_StillAnswers()
        {
            // More users than the domain's default pool of ten connections to the embedded LDAP has room for: each user the
            // server deletes holds one of them for three minutes.
            const int count = 15;

            await SkipIfIdentityCloudAsync("A server in Identity Cloud mode cannot create or delete users.");

            // With a pool too small for the deletes, the test would shut the server out for three minutes, failing the tests
            // after it too, so it runs only where the pool holds them (wrapper.sh gives it 100).
            Assert.SkipUnless(await GetIdentityPoolSizeAsync(TestContext.Current.CancellationToken) is > count + 5,
                "The Essbase server's pool of connections to the embedded LDAP is too small for this test, or could not be read.");

            var server = GetEssServer();
            var prefix = $"esspool_{Guid.NewGuid():N}".Substring(0, 16);

            for (var i = 1; i <= count; i++)
            {
                var user = await server.CreateUserAsync(new EssUserCreationOptions($"{prefix}_{i}", "Passw0rd!2026", EssServerRole.User), TestContext.Current.CancellationToken);

                await user.DeleteAsync(TestContext.Current.CancellationToken);
            }

            // A server whose pool is used up waits ten seconds for a connection and then refuses the request (400 or 403).
            var watch = System.Diagnostics.Stopwatch.StartNew();

            await server.GetGroupsAsync(TestContext.Current.CancellationToken);
            await server.GetUsersAsync(TestContext.Current.CancellationToken);

            Assert.True(watch.Elapsed < TimeSpan.FromSeconds(5), $"Listing the groups and users took {watch.Elapsed.TotalSeconds:F1} seconds after {count} users were deleted.");
        }
    }
}
