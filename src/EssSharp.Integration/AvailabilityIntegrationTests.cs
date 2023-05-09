using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace EssSharp.Integration
{
    public class AvailabilityIntegrationTests : IntegrationTestBase
    {
        public AvailabilityIntegrationTests( IntegrationTestFactory<Program, DbContext> factory ) : base(factory) { }

        [Fact]
        public void DatabaseContainer_AfterCreation_IsNotNull()
        {
            // Create containers.
            Factory.CreateContainers();

            // Assert.
            Assert.NotNull(Database);
        }

        [Fact]
        public async Task DatabaseContainer_AfterCreation_IsRunning()
        {
            // Get a docker client.
            using var client = Factory.CreateDockerClient();

            // Get the state of the database container.
            var state = (await client.Containers.InspectContainerAsync(Database?.Id))?.State;

            // Assert
            Assert.True(state?.Running);
        }
    }
}
