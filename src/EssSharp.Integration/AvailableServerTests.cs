using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;


namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), Trait("type", "server"), CollectionPriority(2)]
    public class AvailableServerTests : IntegrationTestBase
    {
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
            // Get a connected user session.
            var session = await GetEssServer().SignInAsync();

            // Assert that a connected user session is returned.
            Assert.NotEqual(default, session);
        }
    }
}
