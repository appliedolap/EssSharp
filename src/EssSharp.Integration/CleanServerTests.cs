using System;
using System.Linq;
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

        [Fact(DisplayName = "CleanServerTests - 04 - Essbase_AfterConnection_CanRemoveUsers"), Priority(04)]
        public async Task Essbase_AfterConnection_CanRemoveUsers()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get and delete all existing users except admin.
            foreach ( var user in (await server.GetUsersAsync()).Where(u => !string.Equals(u.Name, "admin")) )
            {
                try
                {
                    // Attempt to delete the user...
                    await user.DeleteAsync();
                }
                catch ( Exception e )
                {
                    // Unless the user only partially exists, which can and does happen with persistent containers, throw.
                    if ( e.Message?.EndsWith("User does not exist.", StringComparison.OrdinalIgnoreCase) is not true )
                        throw;

                    try
                    {
                        // Attempt to recreate the user and then delete it as a workaround.
                        await user.Server.CreateUserAsync(new EssUserCreationOptions(id: user.Name, password: Guid.NewGuid().ToString("D").Substring(0, 20).TrimEnd('-'), user.Role));
                        await user.DeleteAsync();
                    }
                    catch
                    {
                        // Throw the original exception.
                        throw e;
                    }
                }
            }

            // Get the full list of users.
            var users = await server.GetUsersAsync();

            // Assert that the (refreshed) list of users contains a single user.
            Assert.Single(await server.GetUsersAsync());

            // Assert that the name of the only remaining user is "admin".
            Assert.Equal("admin", users.First()?.Name);
        }

        [Fact(DisplayName = "CleanServerTests - 05 - Essbase_AfterConnection_CanRemoveUserPermissions"), Priority(05)]
        public async Task Essbase_AfterConnection_CanRemoveUserPermissions()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the list of existing applications.
            foreach ( var application in await server.GetApplicationsAsync() )
            {
             // Get the list of existing locks and unlock them.
                foreach ( var essPermission in await application.GetPermissionsAsync() )
                    await essPermission.RemovePermissionsAsync();

                // Assert that the (refreshed) list of existing locks is empty.
                Assert.Empty(await application.GetPermissionsAsync());   
            }
        }

        [Fact(DisplayName = "CleanServerTests - 06 - Essbase_AfterConnection_CanRemoveGroups"), Priority(06)]
        public async Task Essbase_AfterConnection_CanRemoveGroups()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the list of existing applications.
            foreach ( var group in await server.GetGroupsAsync() )
            {
                try
                {
                    // Attempt to delete the group...
                    await group.DeleteAsync();
                }
                catch ( Exception e )
                {
                    // Unless the group only partially exists, which can and does happen with persistent containers, throw.
                    if ( e.Message?.EndsWith("Group does not exist.", StringComparison.OrdinalIgnoreCase) is not true )
                        throw;

                    try
                    {
                        // Attempt to recreate the group and then delete it as a workaround.
                        await group.Server.CreateGroupAsync(name: group.Name, role: group.Role);
                        await group.DeleteAsync();
                    }
                    catch
                    {
                        // Throw the original exception.
                        throw e;
                    }
                }
            }

            // Assert that the (refreshed) list of existing locks is empty.
            Assert.Empty(await server.GetGroupsAsync());
        }
    }
}
