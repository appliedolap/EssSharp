using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), Trait("type", "get"), CollectionPriority(5)]
    public class GetServerObjectTests : IntegrationTestBase
    {
        [Fact(DisplayName = $@"GetServerObjectTests - 01 - Essbase_AfterLockCreation_CanGetLocks"), Priority(01)]
        public async Task Essbase_AfterLockCreation_CanGetLocks()
        {
            // Get an unconnected server.
            var server = new EssServerFactory().CreateEssServer(server: @"http://localhost:9000/essbase", username: "admin", password: "password1", connect: false);

            // Get the locked object from the server.
            var lockedObject = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetLockedObjectAsync("CalcAll");

            // Assert that the lock object name is the same as the one we passed. --Make better 
            Assert.Equal("CalcAll", lockedObject.Name);
        }
    }
}
