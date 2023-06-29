using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), Trait("type", "container"), CollectionPriority(1)]
    public class AvailableContainerTests : IntegrationTestBase
    {
        [Fact(DisplayName = "AvailableContainerTests - 01 - DatabaseContainer_AfterInitialization_IsNotNull"), Priority(01)]
        public void DatabaseContainer_AfterInitialization_IsNotNull()
        {
            // Assert.
            Assert.NotNull(Database);
        }

        [Fact(DisplayName = "AvailableContainerTests - 02 - DatabaseContainer_AfterInitialization_IsRunning"), Priority(02)]
        public async Task DatabaseContainer_AfterInitialization_IsRunning()
        {
            // Get a docker client.
            using var client = GetClient();

            // Get the state of the database container.
            var state = (await client.Containers.InspectContainerAsync(Database))?.State;

            // Assert
            Assert.True(state?.Running);
        }

        [Fact(DisplayName = "AvailableContainerTests - 03 - EssbaseContainer_AfterInitialization_IsNotNull"), Priority(03)]
        public void EssbaseContainer_AfterInitialization_IsNotNull()
        {
            // Assert.
            Assert.NotNull(Essbase);
        }

        [Fact(DisplayName = "AvailableContainerTests - 04 - EssbaseContainer_AfterInitialization_IsRunning"), Priority(04)]
        public async Task EssbaseContainer_AfterInitialization_IsRunning()
        {
            // Get a docker client.
            using var client = GetClient();

            // Get the state of the essbase container.
            var state = (await client.Containers.InspectContainerAsync(Essbase))?.State;

            // Assert
            Assert.True(state?.Running);
        }

    }
}
