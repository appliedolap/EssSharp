using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), Trait("type", "clean"), CollectionPriority(3)]
    public class CleanServerTests : IntegrationTestBase
    {
        [Fact(DisplayName = "CleanServerTests - 01 - Essbase_AfterConnection_CanRemoveLocks"), Priority(01)]
        public async Task Essbase_AfterConnection_CanRemoveLocks()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the list of existing applications.
            foreach ( var application in await server.GetApplicationsAsync() )
            {
                // Get the list of existing cubes.
                foreach ( var cube in await application.GetCubesAsync() )
                {
                    // Get the list of existing locks and unlock them.
                    foreach ( var essLock in await cube.GetLockedObjectsAsync() )
                        await essLock.UnlockAsync();

                    // Assert that the (refreshed) list of existing locks is empty.
                    Assert.Empty(await cube.GetLockedObjectsAsync());
                }
            }
        }

        [Fact(DisplayName = "CleanServerTests - 02 - Essbase_AfterConnection_CanRemoveScripts"), Priority(02)]
        public async Task Essbase_AfterConnection_CanRemoveScripts()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the list of existing applications.
            foreach ( var application in await server.GetApplicationsAsync() )
            {
                // Get the list of existing cubes.
                foreach ( var cube in await application.GetCubesAsync() )
                {
                    // ASO cubes cannot carry scripts.
                    if ( cube.CubeType is EssCubeType.ASO )
                        continue;

                    // Get and delete all existing calc scripts.
                    foreach ( var script in await cube.GetScriptsAsync<IEssCalcScript>() )
                        await script.DeleteAsync();

                    // Assert that the (refreshed) list of calc scripts is empty.
                    Assert.Empty(await cube.GetScriptsAsync<IEssCalcScript>());

                    // Get and delete all existing maxl scripts.
                    foreach ( var script in await cube.GetScriptsAsync<IEssMaxlScript>() )
                        await script.DeleteAsync();

                    // Assert that the (refreshed) list of maxl scripts is empty.
                    Assert.Empty(await cube.GetScriptsAsync<IEssMaxlScript>());

                    // Get and delete all existing mdx scripts.
                    foreach ( var script in await cube.GetScriptsAsync<IEssMdxScript>() )
                        await script.DeleteAsync();

                    // Assert that the (refreshed) list of mdx scripts is empty.
                    Assert.Empty(await cube.GetScriptsAsync<IEssMdxScript>());

                    // Get and delete all existing report scripts.
                    foreach ( var script in await cube.GetScriptsAsync<IEssReportScript>() )
                        await script.DeleteAsync();

                    // Assert that the (refreshed) list of report scripts is empty..
                    Assert.Empty(await cube.GetScriptsAsync<IEssReportScript>());
                }
            }
        }

        [Fact(DisplayName = "CleanServerTests - 03 - Essbase_AfterConnection_CanRemoveApplications"), Priority(03)]
        public async Task Essbase_AfterConnection_CanRemoveApplications()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get and delete all existing applications.
            foreach ( var application in await server.GetApplicationsAsync() )
                await application.DeleteAsync();

            // Assert that the (refreshed) list of applications is empty.
            Assert.Empty(await server.GetApplicationsAsync());
        }
    }
}
