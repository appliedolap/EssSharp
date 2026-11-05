using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />
    /// </summary>
    [CollectionDefinition(nameof(UserGroupManagementTests), DisableParallelization = true), CollectionPriority(11)]
    public class UserGroupManagementTestsCollection : ICollectionFixture<CollectionFixture> { }

    /// <summary>
    /// Tests of managing the users and groups of a server: editing users, searching, importing, exporting and deleting
    /// several, group members, and provisioning reports. Each test creates the users and groups it needs, under names that
    /// no other run uses, and removes them afterwards, so the tests leave the server as they found it and need no other test's
    /// objects. The provisioning report test uses an application the server already has, and removes the permission it gives.
    /// </summary>
    [Collection(nameof(UserGroupManagementTests)), Trait("type", "execute")]
    public class UserGroupManagementTests : IntegrationTestBase
    {
        private const string _prefix   = "esstest_";
        private const string _password = "Passw0rd!2026";

        private readonly string _suffix = Guid.NewGuid().ToString("N").Substring(0, 8);

        private readonly ITestOutputHelper _output;

        /// <summary />
        /// <param name="output" />
        public UserGroupManagementTests( ITestOutputHelper output ) : base(output) => _output = output;

        [Fact(DisplayName = "UserGroupManagementTests - 01 - Essbase_CanSupportUserGroupManagementAndProvisioningReports"), Priority(01)]
        public async Task Essbase_CanSupportUserGroupManagementAndProvisioningReports()
        {
            SkipUnlessServerManagesUsers();

            var server = GetEssServer();

            Assert.True(await server.SupportsAsync(EssFeature.UserGroupManagement, TestContext.Current.CancellationToken));
            Assert.True(await server.SupportsAsync(EssFeature.ProvisioningReports, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "UserGroupManagementTests - 02 - Essbase_CanEditUser"), Priority(02)]
        public async Task Essbase_CanEditUser()
        {
            SkipUnlessServerManagesUsers();

            var server = GetEssServer();
            var id     = Id("edit");

            try
            {
                var user = await server.CreateUserAsync(new EssUserCreationOptions(id, _password, EssServerRole.User), TestContext.Current.CancellationToken);

                // Edit the name and email.
                var edited = await user.EditAsync(fullName: "Edit Test", email: "edit@example.com", cancellationToken: TestContext.Current.CancellationToken);

                Assert.Same(user, edited);
                Assert.Equal("Edit Test",         edited.FullName);
                Assert.Equal("edit@example.com",  edited.Email);
                Assert.Equal(EssServerRole.User,  edited.Role);

                // Assert that the edit left the password as it was.
                await AssertCanSignInAsync(id, _password, EssServerRole.User);

                // Edit the email alone, and assert that the server kept the name it replaces whole users with.
                await user.EditAsync(email: "again@example.com", cancellationToken: TestContext.Current.CancellationToken);

                var reread = await GetUserAsync(server, id);

                Assert.Equal("Edit Test",         reread.FullName);
                Assert.Equal("again@example.com", reread.Email);
                Assert.Equal(EssServerRole.User,  reread.Role);

                // Edit the role, to each role.
                foreach ( var role in new[] { EssServerRole.PowerUser, EssServerRole.ServiceAdministrator, EssServerRole.User } )
                {
                    await user.EditAsync(role: role, cancellationToken: TestContext.Current.CancellationToken);

                    Assert.Equal(role, user.Role);
                    var changed = await GetUserAsync(server, id);

                    Assert.Equal(role,        changed.Role);
                    Assert.Equal("Edit Test", changed.FullName);
                }

                // Edit the password, and assert that only the new one signs in.
                await user.EditAsync(password: _password + "2", cancellationToken: TestContext.Current.CancellationToken);

                await AssertCanSignInAsync(id, _password + "2", EssServerRole.User);
                await Assert.ThrowsAnyAsync<Exception>(() => AssertCanSignInAsync(id, _password, EssServerRole.User));
            }
            finally
            {
                await CleanUpAsync(server);
            }
        }

        [Fact(DisplayName = "UserGroupManagementTests - 03 - Essbase_CanSearchUsersAndGroupsWithFilterAndLimit"), Priority(03)]
        public async Task Essbase_CanSearchUsersAndGroupsWithFilterAndLimit()
        {
            SkipUnlessServerManagesUsers();

            var server = GetEssServer();

            string[] userIds    = { Id("find_a"), Id("find_b") };
            string[] groupNames = { Id("find_g1"), Id("find_g2") };

            try
            {
                foreach ( var id in userIds )
                    await server.CreateUserAsync(new EssUserCreationOptions(id, _password), TestContext.Current.CancellationToken);

                foreach ( var name in groupNames )
                    await server.CreateGroupAsync(name, EssServerRole.User, null, TestContext.Current.CancellationToken);

                // Search with a pattern.
                var users = await server.GetUsersAsync(_prefix + "find_*", cancellationToken: TestContext.Current.CancellationToken);

                Assert.Equal(userIds.OrderBy(id => id), Mine(users).Select(user => user.Name).OrderBy(id => id));

                // Search with a name alone, which matches only that user.
                Assert.Equal(userIds[0], Assert.Single(await server.GetUsersAsync(userIds[0], cancellationToken: TestContext.Current.CancellationToken)).Name);
                Assert.Empty(await server.GetUsersAsync(_prefix + "nobody_" + _suffix, cancellationToken: TestContext.Current.CancellationToken));

                // Search with a limit, which the server does not apply.
                Assert.Single(await server.GetUsersAsync(_prefix + "find_*", limit: 1, cancellationToken: TestContext.Current.CancellationToken));
                Assert.Single(server.GetUsers(_prefix + "find_*", 1));

                // Search groups the same way.
                var groups = await server.GetGroupsAsync(_prefix + "find_*", cancellationToken: TestContext.Current.CancellationToken);

                Assert.Equal(groupNames.OrderBy(name => name), Mine(groups).Select(group => group.Name).OrderBy(name => name));
                Assert.Equal(groupNames[1], Assert.Single(await server.GetGroupsAsync(groupNames[1], cancellationToken: TestContext.Current.CancellationToken)).Name);
                Assert.Single(await server.GetGroupsAsync(_prefix + "find_*", limit: 1, cancellationToken: TestContext.Current.CancellationToken));
            }
            finally
            {
                await CleanUpAsync(server);
            }
        }

        [Fact(DisplayName = "UserGroupManagementTests - 04 - Essbase_CanImportAndExportUsersAndGroups"), Priority(04)]
        public async Task Essbase_CanImportAndExportUsersAndGroups()
        {
            SkipUnlessServerManagesUsers();

            var server = GetEssServer();

            var group = Id("csv_g");
            var userA = Id("csv_a");
            var userB = Id("csv_b");

            try
            {
                // Import a group, and assert that the server kept its role and description.
                var groupResults = await server.ImportGroupsAsync(Csv("name,description,role,groups", $"{group},Imported group,Power User,"), TestContext.Current.CancellationToken);

                var groupResult = Assert.Single(groupResults);

                Assert.Equal(group, groupResult.Id);
                Assert.True(groupResult.Succeeded, groupResult.Message);

                var imported = await server.GetGroupAsync(group, TestContext.Current.CancellationToken);

                Assert.Equal("Imported group",       imported.Description);
                Assert.Equal(EssServerRole.PowerUser, imported.Role);

                // Import two users, one with a role that does not exist. The server answers both successfully.
                var csv = new[]
                {
                    "id,name,email,password,role,groups",
                    $"{userA},User A,a@example.com,{_password},User,{group}",
                    $"{userB},,,{_password},Wizard,"
                };

                var userResults = await server.ImportUsersAsync(Csv(csv), TestContext.Current.CancellationToken);

                Assert.Equal(new[] { userA, userB }, userResults.Select(result => result.Id));
                Assert.True(userResults[0].Succeeded, userResults[0].Message);
                Assert.False(userResults[1].Succeeded);
                Assert.NotEmpty(userResults[1].Message);

                var importedUser = await GetUserAsync(server, userA);

                Assert.Equal("User A",           importedUser.FullName);
                Assert.Equal("a@example.com",    importedUser.Email);
                Assert.Equal(EssServerRole.User, importedUser.Role);
                Assert.Contains(group,           importedUser.GroupNames);
                Assert.Empty(await server.GetUsersAsync(userB, cancellationToken: TestContext.Current.CancellationToken));

                // Import the first user again, which edits it.
                var again = Assert.Single(await server.ImportUsersAsync(Csv(csv.Take(2).ToArray()), TestContext.Current.CancellationToken));

                Assert.True(again.Succeeded, again.Message);
                Assert.Contains("edited", again.Message, StringComparison.OrdinalIgnoreCase);

                // Export the users and the groups, all of them, as the server exports every one whatever the filter.
                using ( var users = await server.ExportUsersAsync(TestContext.Current.CancellationToken) )
                {
                    var lines = (await new StreamReader(users).ReadToEndAsync(TestContext.Current.CancellationToken)).Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    Assert.Equal("id,name,email,password,role,groups", lines[0]);
                    Assert.Single(lines, line => line.StartsWith($"{userA},User A,a@example.com,,User,"));
                    Assert.DoesNotContain(lines, line => line.StartsWith($"{userB},"));
                }

                using ( var groups = server.ExportGroups() )
                {
                    var lines = (await new StreamReader(groups).ReadToEndAsync(TestContext.Current.CancellationToken)).Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    Assert.Equal("name,description,role,groups", lines[0]);
                    Assert.Single(lines, line => line.StartsWith($"{group},"));
                }
            }
            finally
            {
                await CleanUpAsync(server);
            }
        }

        [Fact(DisplayName = "UserGroupManagementTests - 05 - Essbase_CanDeleteSeveralUsersAndGroups"), Priority(05)]
        public async Task Essbase_CanDeleteSeveralUsersAndGroups()
        {
            SkipUnlessServerManagesUsers();

            var server = GetEssServer();

            string[] userIds    = { Id("del_a"), Id("del_b") };
            string[] groupNames = { Id("del_g1"), Id("del_g2") };

            try
            {
                foreach ( var id in userIds )
                    await server.CreateUserAsync(new EssUserCreationOptions(id, _password), TestContext.Current.CancellationToken);

                foreach ( var name in groupNames )
                    await server.CreateGroupAsync(name, EssServerRole.User, null, TestContext.Current.CancellationToken);

                // Delete the users, and one that does not exist. The server answers all of them successfully.
                var missingUser = Id("del_nobody");
                var userResults = await server.DeleteUsersAsync(userIds.Append(missingUser), TestContext.Current.CancellationToken);

                Assert.Equal(userIds.Append(missingUser), userResults.Select(result => result.Id));
                Assert.Equal(new[] { true, true, false }, userResults.Select(result => result.Succeeded));
                Assert.Empty(Mine(await server.GetUsersAsync(_prefix + "del_*", cancellationToken: TestContext.Current.CancellationToken)));

                // Delete the groups, and one that does not exist.
                var missingGroup = Id("del_nobody_g");
                var groupResults = server.DeleteGroups(groupNames.Append(missingGroup));

                Assert.Equal(groupNames.Append(missingGroup), groupResults.Select(result => result.Id));
                Assert.Equal(new[] { true, true, false }, groupResults.Select(result => result.Succeeded));
                Assert.Empty(Mine(await server.GetGroupsAsync(_prefix + "del_*", cancellationToken: TestContext.Current.CancellationToken)));

                // Delete nothing.
                Assert.Empty(await server.DeleteUsersAsync(Array.Empty<string>(), TestContext.Current.CancellationToken));
            }
            finally
            {
                await CleanUpAsync(server);
            }
        }

        [Fact(DisplayName = "UserGroupManagementTests - 06 - Essbase_CanGetGroupMembersAndEditGroup"), Priority(06)]
        public async Task Essbase_CanGetGroupMembersAndEditGroup()
        {
            SkipUnlessServerManagesUsers();

            var server = GetEssServer();

            var parentName = Id("mem_p");
            var childName  = Id("mem_c");
            var userId     = Id("mem_u");
            var grandName  = Id("mem_gp");

            try
            {
                var parent = await server.CreateGroupAsync(parentName, EssServerRole.PowerUser, "parent", TestContext.Current.CancellationToken);
                await server.CreateGroupAsync(childName, EssServerRole.User, null, TestContext.Current.CancellationToken);
                await server.CreateUserAsync(new EssUserCreationOptions(userId, _password), TestContext.Current.CancellationToken);

                // Put the group in another group, which an edit of the group has to leave it in.
                var grand = await server.CreateGroupAsync(grandName, EssServerRole.User, null, TestContext.Current.CancellationToken);

                await grand.AddGroupsAsync(new List<string> { parentName }, TestContext.Current.CancellationToken);

                // Assert that there are no members yet.
                var none = await parent.GetMembersAsync(TestContext.Current.CancellationToken);

                Assert.Empty(none.Users);
                Assert.Empty(none.Groups);

                await parent.AddUsersAsync(new List<string> { userId }, TestContext.Current.CancellationToken);
                await parent.AddGroupsAsync(new List<string> { childName }, TestContext.Current.CancellationToken);

                // Get the users and groups together.
                var members = parent.GetMembers();

                Assert.Equal(userId,    Assert.Single(members.Users).Name);
                Assert.Equal(childName, Assert.Single(members.Groups).Name);

                // Edit the user who is a member, and assert that the edit left the user in the group.
                var member = await GetUserAsync(server, userId);

                await member.EditAsync(role: EssServerRole.PowerUser, cancellationToken: TestContext.Current.CancellationToken);

                Assert.Contains(parentName, (await GetUserAsync(server, userId)).GroupNames);

                // Edit the group's description alone, which keeps its role.
                var edited = await parent.EditAsync(description: "Described", cancellationToken: TestContext.Current.CancellationToken);

                Assert.Equal("Described",             edited.Description);
                Assert.Equal(EssServerRole.PowerUser, edited.Role);

                // Edit the group's role, to the one the server rejected before.
                await parent.EditAsync(role: EssServerRole.ServiceAdministrator, cancellationToken: TestContext.Current.CancellationToken);

                var reread = await server.GetGroupAsync(parentName, TestContext.Current.CancellationToken);

                Assert.Equal(EssServerRole.ServiceAdministrator, reread.Role);
                Assert.Equal("Described",                        reread.Description);

                // Assert that the edits left the group in the group it was in, and its members in it.
                Assert.Contains(grandName, reread.GroupNames);

                var after = await reread.GetMembersAsync(TestContext.Current.CancellationToken);

                Assert.Equal(userId,    Assert.Single(after.Users).Name);
                Assert.Equal(childName, Assert.Single(after.Groups).Name);
            }
            finally
            {
                await CleanUpAsync(server);
            }
        }

        [Fact(DisplayName = "UserGroupManagementTests - 07 - Essbase_CanGetUserAndGroupProvisioningReports"), Priority(07)]
        public async Task Essbase_CanGetUserAndGroupProvisioningReports()
        {
            SkipUnlessServerManagesUsers();

            var server       = GetEssServer();
            var applications = await server.GetApplicationsAsync(TestContext.Current.CancellationToken);
            var application  = applications.FirstOrDefault(application => application.Name == "Sample") ?? applications.FirstOrDefault();

            Assert.SkipUnless(application is not null, "The server has no application to report on.");

            var groupName = Id("prov_g");
            var userId    = Id("prov_u");

            IEssApplicationPermission permission = null;

            try
            {
                var group = await server.CreateGroupAsync(groupName, EssServerRole.PowerUser, "provisioning", TestContext.Current.CancellationToken);
                var user  = await server.CreateUserAsync(new EssUserCreationOptions(userId, _password), TestContext.Current.CancellationToken);

                await group.AddUsersAsync(new List<string> { userId }, TestContext.Current.CancellationToken);

                permission = await application.CreatePermissionsAsync(groupName, EssApplicationRole.db_update, isGroup: true, cancellationToken: TestContext.Current.CancellationToken);

                // The group holds the role directly.
                var groupRoles = await group.GetProvisionedRolesAsync(application.Name, TestContext.Current.CancellationToken);

                Assert.Contains(groupRoles, role => role.Name == "db_update" && role.InheritedFromGroups.Count == 0);

                // The user holds it through the group.
                var userRoles = user.GetProvisionedRoles(application.Name);

                Assert.Contains(userRoles, role => role.Name == "db_update" && role.InheritedFromGroups.Contains(groupName));

                // Get the filters and scripts, of which the user and group hold none.
                Assert.Empty(await user.GetProvisionedFiltersAsync(application.Name, TestContext.Current.CancellationToken));
                Assert.Empty(await user.GetProvisionedScriptsAsync(application.Name, TestContext.Current.CancellationToken));
                Assert.Empty(await group.GetProvisionedFiltersAsync(application.Name, TestContext.Current.CancellationToken));
                Assert.Empty(await group.GetProvisionedScriptsAsync(application.Name, TestContext.Current.CancellationToken));

                // Get the report for the application.
                var userApplication = await user.GetApplicationProvisioningReportAsync(application.Name, TestContext.Current.CancellationToken);

                Assert.Equal(application.Name, userApplication.Name);
                Assert.Contains(userApplication.Roles, role => role.Name == "db_update" && role.InheritedFromGroups.Contains(groupName));

                var groupApplication = group.GetApplicationProvisioningReport(application.Name);

                Assert.Equal(application.Name, groupApplication.Name);
                Assert.Contains(groupApplication.Roles, role => role.Name == "db_update");

                // Get the reports for all applications.
                Assert.Contains(await user.GetApplicationProvisioningReportsAsync(TestContext.Current.CancellationToken),
                    report => report.Name == application.Name && report.Roles.Any(role => role.Name == "db_update"));

                Assert.Contains(group.GetApplicationProvisioningReports(),
                    report => report.Name == application.Name && report.Roles.Any(role => role.Name == "db_update"));

                // Get the full reports, which list the service roles and the applications.
                var userReport  = await user.GetProvisioningReportAsync(TestContext.Current.CancellationToken);
                var groupReport = group.GetProvisioningReport();

                Assert.NotEmpty(groupReport.ServiceRoles);
                Assert.Contains(userReport.Applications,  report => report.Name == application.Name && report.Roles.Any(role => role.Name == "db_update"));
                Assert.Contains(groupReport.Applications, report => report.Name == application.Name && report.Roles.Any(role => role.Name == "db_update"));
            }
            finally
            {
                // Remove the permission the test gave the application, before the group.
                if ( permission is not null )
                    await RetryAsync($"Removing the permission of {groupName}", () => permission.RemovePermissionsAsync(CancellationToken.None));

                await CleanUpAsync(server);
            }
        }

        [Fact(DisplayName = "UserGroupManagementTests - 08 - Essbase_InIdentityCloudMode_HasNoUserGroupManagement_ButGivesProvisioningReports"), Priority(08)]
        public async Task Essbase_InIdentityCloudMode_HasNoUserGroupManagement_ButGivesProvisioningReports()
        {
            Assert.SkipWhen(ServerManagesUsers, "The server manages its own users and groups, so it is not in Identity Cloud mode.");

            var server = GetEssServer();

            // The server reports the mode, so the support check rules user and group management out, and its members say so.
            Assert.False(await server.SupportsAsync(EssFeature.UserGroupManagement, TestContext.Current.CancellationToken));
            Assert.True(await server.SupportsAsync(EssFeature.ProvisioningReports, TestContext.Current.CancellationToken));

            await Assert.ThrowsAsync<NotSupportedException>(() => server.GetUsersAsync(TestContext.Current.CancellationToken));
            await Assert.ThrowsAsync<NotSupportedException>(() => server.GetGroupsAsync(_prefix + "*", cancellationToken: TestContext.Current.CancellationToken));

            // The server still answers for one user, and gives that user's provisioning report.
            var userId = (await server.SignInAsync(cancellationToken: TestContext.Current.CancellationToken)).UserId;
            var user   = await server.GetUserAsync(userId, TestContext.Current.CancellationToken);
            var report = await user.GetProvisioningReportAsync(TestContext.Current.CancellationToken);

            Assert.Equal(userId, user.Name, ignoreCase: true);
            Assert.Contains(report.ServiceRoles, role => role.Name == "service_administrator");
        }

        [Fact(DisplayName = "UserGroupManagementTests - 09 - Essbase_CanSetGetAndRemoveServiceRolesOfUsersAndGroups"), Priority(09)]
        public async Task Essbase_CanSetGetAndRemoveServiceRolesOfUsersAndGroups()
        {
            SkipUnlessServerManagesUsers();

            var server    = GetEssServer();
            var userId    = Id("role_u");
            var groupName = Id("role_g");

            try
            {
                var user  = await server.CreateUserAsync(new EssUserCreationOptions(userId, _password, EssServerRole.User), TestContext.Current.CancellationToken);
                var group = await server.CreateGroupAsync(groupName, EssServerRole.User, "service roles", TestContext.Current.CancellationToken);

                Assert.Equal(EssServerRole.User, await user.GetServiceRoleAsync(TestContext.Current.CancellationToken));

                // Give the user a role, and assert that the server, its user record and the search all say so.
                await user.SetServiceRoleAsync(EssServerRole.PowerUser, TestContext.Current.CancellationToken);

                Assert.Equal(EssServerRole.PowerUser, user.Role);
                Assert.Equal(EssServerRole.PowerUser, await user.GetServiceRoleAsync(TestContext.Current.CancellationToken));
                Assert.Equal(EssServerRole.PowerUser, (await GetUserAsync(server, userId)).Role);

                var assignments = await server.GetServiceRoleAssignmentsAsync(userId, EssPermissionType.User, cancellationToken: TestContext.Current.CancellationToken);

                Assert.Contains(assignments, assignment => assignment.Id == userId && assignment.Type == EssPermissionType.User && assignment.Role == EssServerRole.PowerUser);

                // Give the group a role, find it among the groups, and remove it.
                group.SetServiceRole(EssServerRole.ServiceAdministrator);

                Assert.Equal(EssServerRole.ServiceAdministrator, group.GetServiceRole());
                Assert.Contains(await server.GetServiceRoleAssignmentsAsync(groupName, EssPermissionType.Group, new[] { EssServerRole.ServiceAdministrator }, TestContext.Current.CancellationToken),
                    assignment => assignment.Id == groupName && assignment.Type == EssPermissionType.Group);

                await group.RemoveServiceRoleAsync(TestContext.Current.CancellationToken);

                Assert.Null(await group.GetServiceRoleAsync(TestContext.Current.CancellationToken));
                Assert.DoesNotContain(await server.GetServiceRoleAssignmentsAsync(groupName, EssPermissionType.Group, cancellationToken: TestContext.Current.CancellationToken),
                    assignment => assignment.Id == groupName);
            }
            finally
            {
                await CleanUpAsync(server);
            }
        }

        [Fact(DisplayName = "UserGroupManagementTests - 10 - Essbase_InIdentityCloudMode_CanSetGetAndRemoveTheServiceRoleOfAUser"), Priority(10)]
        public async Task Essbase_InIdentityCloudMode_CanSetGetAndRemoveTheServiceRoleOfAUser()
        {
            Assert.SkipWhen(ServerManagesUsers, "The server manages its own users and groups, so it is not in Identity Cloud mode.");

            // A user of the identity service whom the suite gives no service role.
            var userId = IntegrationTestFactory.OAuth?.Users?.FirstOrDefault(user => user.Role == EssServerRole.Unkown)?.Username;

            Assert.SkipWhen(string.IsNullOrEmpty(userId), "The settings name no identity service user without a service role.");

            var server = GetEssServer();
            var user   = await server.GetUserAsync(userId, TestContext.Current.CancellationToken);

            Assert.True(await server.SupportsAsync(EssFeature.ServiceRoleProvisioning, TestContext.Current.CancellationToken));
            Assert.Null(await user.GetServiceRoleAsync(TestContext.Current.CancellationToken));

            // Editing the user is not supported here, but giving the user a service role is.
            await Assert.ThrowsAsync<NotSupportedException>(() => user.EditAsync(role: EssServerRole.PowerUser, cancellationToken: TestContext.Current.CancellationToken));

            try
            {
                await user.SetServiceRoleAsync(EssServerRole.PowerUser, TestContext.Current.CancellationToken);

                Assert.Equal(EssServerRole.PowerUser, await user.GetServiceRoleAsync(TestContext.Current.CancellationToken));
                Assert.Contains(await server.GetServiceRoleAssignmentsAsync(userId, EssPermissionType.User, cancellationToken: TestContext.Current.CancellationToken),
                    assignment => string.Equals(assignment.Id, userId, StringComparison.OrdinalIgnoreCase) && assignment.Role == EssServerRole.PowerUser);
            }
            finally
            {
                // Leave the user without a role, as the suite gave it.
                await RetryAsync($"Removing the service role of {userId}", () => user.RemoveServiceRoleAsync(CancellationToken.None));
            }

            Assert.Null(await user.GetServiceRoleAsync(TestContext.Current.CancellationToken));
        }

        #region Helpers

        /// <summary>
        /// Returns the ID or name that the given stem has in this test run.
        /// </summary>
        /// <param name="stem">The stem of the ID or name.</param>
        private string Id( string stem ) => $"{_prefix}{stem}_{_suffix}";

        /// <summary>
        /// Returns the users made by this test run among the given users.
        /// </summary>
        /// <param name="users">The users.</param>
        private IEnumerable<IEssUser> Mine( IEnumerable<IEssUser> users ) => users.Where(user => user.Name.EndsWith(_suffix, StringComparison.Ordinal));

        /// <summary>
        /// Returns the groups made by this test run among the given groups.
        /// </summary>
        /// <param name="groups">The groups.</param>
        private IEnumerable<IEssGroup> Mine( IEnumerable<IEssGroup> groups ) => groups.Where(group => group.Name.EndsWith(_suffix, StringComparison.Ordinal));

        /// <summary>
        /// Returns the given lines as a stream of CSV.
        /// </summary>
        /// <param name="lines">The lines, including the header.</param>
        private static Stream Csv( params string[] lines ) => new MemoryStream(new UTF8Encoding(false).GetBytes(string.Join("\n", lines) + "\n"));

        /// <summary>
        /// Asserts that the given user signs in to the server with the given password.
        /// </summary>
        /// <param name="id">The user's ID.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="role">The user's role.</param>
        private async Task AssertCanSignInAsync( string id, string password, EssServerRole role )
        {
            var connection = new IntegrationTestSettingsConnection() { Server = GetEssConnection().Server, Username = id, Password = password, Role = role };

            Assert.Equal(id, (await GetEssServer(connection).SignInAsync(cancellationToken: TestContext.Current.CancellationToken)).UserId, ignoreCase: true);
        }

        /// <summary>
        /// Removes the users and groups that this test run made, whatever the test did.
        /// </summary>
        /// <param name="server">The server.</param>
        private Task CleanUpAsync( IEssServer server ) => RetryAsync($"Removing the users and groups of test run {_suffix}", async () =>
        {
            foreach ( var user in Mine(await server.GetUsersAsync(_prefix + "*", cancellationToken: CancellationToken.None)).ToList() )
                await user.DeleteAsync(CancellationToken.None);

            foreach ( var group in Mine(await server.GetGroupsAsync(_prefix + "*", cancellationToken: CancellationToken.None)).ToList() )
                await group.DeleteAsync(CancellationToken.None);
        });

        /// <summary>
        /// Returns the one user with the given ID, as the server's search finds it. The search is lighter on the server than
        /// <see cref="IEssServer.GetUserAsync(string, CancellationToken)" />, which lists every user with the groups of each.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="id">The user's ID.</param>
        private static async Task<IEssUser> GetUserAsync( IEssServer server, string id ) =>
            Assert.Single(await server.GetUsersAsync(id, cancellationToken: TestContext.Current.CancellationToken));

        /// <summary>
        /// Runs the given clean up, and runs it again after a pause when it fails. The server's embedded directory service fails
        /// for a minute or so now and then after a burst of user and group requests, and a clean up that gave up at once would
        /// leave the users, groups and permissions of the test on a server that other tests share. A clean up that still fails
        /// fails the test, so that what it leaves behind is noticed.
        /// </summary>
        /// <param name="description">What the clean up does, for the output.</param>
        /// <param name="cleanUp">The clean up, which must do no harm when it is run again.</param>
        private async Task RetryAsync( string description, Func<Task> cleanUp )
        {
            for ( var attempt = 1; ; attempt++ )
            {
                try
                {
                    await cleanUp();
                    return;
                }
                catch ( Exception e )
                {
                    _output.WriteLine($"{description} failed on attempt {attempt}. {e.Message}");

                    if ( attempt >= 8 )
                        throw new Exception($"{description} failed {attempt} times, so the test left what it made on the server. {e.Message}", e);
                }

                await Task.Delay(TimeSpan.FromSeconds(15), CancellationToken.None);
            }
        }

        #endregion
    }
}
