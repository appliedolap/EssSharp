using System;
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
            // Get a docker client.
            using var client = GetClient();

            // Poll for the REST API and capture a status code for an unauthorized request.
            var statusCode = await PollForRestfulApiAndReturnStatusCodeAsync();

            // Assert that the REST API is available and returns a 401 status code (for unauthorized requests).
            Assert.Equal(401, statusCode);

            // Local polling function.
            async Task<int> PollForRestfulApiAndReturnStatusCodeAsync()
            {
                // Wait up to 20 minutes for the REST API to become available.
                for ( int i = 0; i < (TimeSpan.FromMinutes(20).TotalSeconds / 5); i++ )
                {
                    // Poll the /rest/v1/ endpoint for the availability of the REST API.
                    var (details, stdout) = await ExecAsync(Essbase, new[] { @"curl", "--output", "/dev/null", "--silent", "--head", "--fail", "--write-out", "%{http_code}", "http://localhost:9000/essbase/rest/v1/" });

                    // Exit code 22 means that the server is available and returning a 40x status code.
                    if ( details?.ExitCode is 22 )
                    {
                        if ( int.TryParse(stdout?.Trim(), out var statusCode) )
                            return statusCode;
                    }

                    // Wait 5 seconds.
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }

                return 0;
            }
        }

        [Fact(DisplayName = "AvailableServerTests - 02 - Essbase_AfterStartup_CanConnect"), Priority(02)]
        public async Task Essbase_AfterStartup_CanConnect()
        {
            // Get a connected server.
            var server = await new EssServerFactory().CreateEssServerAsync(server: @"http://localhost:9000/essbase", username: "admin", password: "password1", connect: true);

            // Assert that a connected server is returned.
            Assert.NotEqual(default, server);
        }
    }
}
