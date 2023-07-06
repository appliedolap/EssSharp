using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), Trait("type", "execute"), CollectionPriority(6)]
    public class PerformServerFunctionTests : IntegrationTestBase
    {
        [Fact(DisplayName = @"PerformServerFunctionTests - 01 - Essbase_AfterScriptCreation_CanExecuteMdxScript"), Priority(01)]
        public async Task Essbase_AfterScriptCreation_CanExecuteMdxScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the test mdx script from the server.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssMdxScript>("test");

            // Execute the mdx job and capture the results.
            var job = await script.ExecuteAsync();

            // Assert that the run mdx job completed without warnings.
            Assert.Equal(EssJobStatus.Completed, job?.JobStatus);
        }
    }
}
