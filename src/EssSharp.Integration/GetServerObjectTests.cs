using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), Trait("type", "get"), CollectionPriority(5)]
    public class GetServerObjectTests : IntegrationTestBase
    {
        [Fact(DisplayName = $@"GetServerObjectTests - 01 - Essbase_AfterAppCreation_CanGetAppConfigurations"), Priority(01)]
        public async Task Essbase_AfterAppCreation_CanGetAppConfigurations()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the configurations for the sample application.
            var configurations = await (await server.GetApplicationAsync("Sample"))
                .GetConfigurationsAsync();

            // Assert that the collection of configurations is not empty.
            Assert.NotEmpty(configurations);
        }

        [Fact(DisplayName = $@"GetServerObjectTests - 02 - Essbase_AfterLockCreation_CanGetLocks"), Priority(02)]
        public async Task Essbase_AfterLockCreation_CanGetLocks()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the locked object from the server.
            var lockedObject = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetLockedObjectAsync("CalcAll");

            // Assert that the lock object name is the same as the one we passed. --Make better 
            Assert.Equal("CalcAll", lockedObject.Name);
        }
    }
}
