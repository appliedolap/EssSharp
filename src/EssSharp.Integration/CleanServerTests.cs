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

        [Fact(DisplayName = "CleanServerTests - 04 - Essbase_AfterConnection_CanRemoveApplications"), Priority(04)]
        public async Task Essbase_AfterConnection_CanRemoveUsers()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get and delete all existing applications.
            var adminConnections = GetEssConnection(EssUserRole.ServiceAdministrator);
            var puConnections = GetEssConnection(EssUserRole.PowerUser);
            var userConnections = GetEssConnection(EssUserRole.User);

            // Check for admin users and delete any that are not named "admin"
            try
            {
                var admin = await server.GetUserAsync(adminConnections.Username).ConfigureAwait(false); 
                    
                if (!string.Equals("admin", admin.Name))
                    await admin.DeleteAsync().ConfigureAwait(false);
            }  
            catch { }

            // Try to get and delete any power users
            try
            {
                var powerUser = await server.GetUserAsync(puConnections.Username).ConfigureAwait(false);
                await powerUser.DeleteAsync().ConfigureAwait(false);
            } 
            catch { }

            // try to get and delete any users
            try
            {
                var user = await server.GetUserAsync(userConnections.Username).ConfigureAwait(false);
                await user.DeleteAsync().ConfigureAwait(false);
            }
            catch { }

            // Assert that the (refreshed) list of applications is empty.
            Assert.True((await server.GetUsersAsync()).Count == 1);
        }
    }
}
