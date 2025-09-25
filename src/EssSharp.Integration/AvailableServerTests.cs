using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Integration.Setup;

using Xunit;
using Xunit.Abstractions;

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
            var session = await GetEssServer().SignInAsync();

            // Get the configured username (for the service admin).
            var username = GetEssConnection().Username;

            // Assert that the configured username matches the session's user ID.
            Assert.Equal(username, session.UserId);
        }

        [Fact(DisplayName = "AvailableServerTests - 03 - Essbase_AfterStartup_CannotConnectWithBadCredentials"), Priority(03)]
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
                    .SignInAsync();
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
    }
}
