using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of the user and group members of <see cref="IEssServer"/>, <see cref="IEssUser"/> and
    /// <see cref="IEssGroup"/> against a loopback stub that scripts the server's responses and keeps the requests it receives.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class UserGroupTests
    {
        private const string _octetStream = "application/octet-stream";

        private const string _users = @"{""items"":[
            {""id"":""u1"",""name"":""One"",""email"":""one@example.com"",""role"":""Service Administrator"",""groups"":[""g1""]},
            {""id"":""u2"",""name"":""Two"",""email"":"""",""role"":""Power User""},
            {""id"":""u3"",""name"":"""",""email"":"""",""role"":""user""}]}";

        #region Delete Several

        [Fact(DisplayName = "UserGroupTests - DeleteUsersAsync_SendsTheIdsAsAFile_AndReturnsEachResult")]
        public async Task DeleteUsersAsync_SendsTheIdsAsAFile_AndReturnsEachResult()
        {
            using var stub = Stub((method: "POST", path: "/users/actions/delete", body: "u1,Deleted\r\nu2,User does not exist\r\n"));

            var server  = new EssServer(stub.BasePath, "admin", "password");
            var results = await server.DeleteUsersAsync(new[] { "u1", "u2" }, TestContext.Current.CancellationToken);

            var request = Assert.Single(stub.Requests);

            Assert.StartsWith("POST /essbase/rest/v1/users/actions/delete ", request);
            Assert.Contains($"Content-Type: {_octetStream}", request, StringComparison.OrdinalIgnoreCase);
            Assert.Equal("u1\nu2\n", Body(request));

            Assert.Collection(results,
                result => { Assert.Equal("u1", result.Id); Assert.True(result.Succeeded);  Assert.Equal("Deleted", result.Message); },
                result => { Assert.Equal("u2", result.Id); Assert.False(result.Succeeded); Assert.Equal("User does not exist", result.Message); });
        }

        [Fact(DisplayName = "UserGroupTests - DeleteGroupsAsync_SendsTheNamesAsAFile_AndReturnsEachResult")]
        public async Task DeleteGroupsAsync_SendsTheNamesAsAFile_AndReturnsEachResult()
        {
            using var stub = Stub((method: "POST", path: "/groups/actions/delete", body: "g1,Deleted\ng2,Group does not exist\n"));

            var server  = new EssServer(stub.BasePath, "admin", "password");
            var results = await server.DeleteGroupsAsync(new[] { "g1", "g2" }, TestContext.Current.CancellationToken);

            Assert.Equal("g1\ng2\n", Body(Assert.Single(stub.Requests)));

            Assert.Equal(new[] { true, false }, results.Select(result => result.Succeeded));
            Assert.Equal(new[] { "g1", "g2" },  results.Select(result => result.Id));
        }

        [Fact(DisplayName = "UserGroupTests - DeleteUsersAsync_WithNoIds_AsksTheServerNothing")]
        public async Task DeleteUsersAsync_WithNoIds_AsksTheServerNothing()
        {
            using var stub = Stub();

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.Empty(await server.DeleteUsersAsync(Array.Empty<string>(), TestContext.Current.CancellationToken));
            Assert.Empty(stub.Requests);
        }

        [Theory(DisplayName = "UserGroupTests - DeleteUsersAsync_WithAnIdThatCannotBeAFileLine_Throws")]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("u1\nu2")]
        public async Task DeleteUsersAsync_WithAnIdThatCannotBeAFileLine_Throws( string id )
        {
            using var stub = Stub();

            var server = new EssServer(stub.BasePath, "admin", "password");

            await Assert.ThrowsAsync<ArgumentException>(() => server.DeleteUsersAsync(new[] { id }, TestContext.Current.CancellationToken));
            Assert.Empty(stub.Requests);
        }

        #endregion

        #region Import and Export

        [Fact(DisplayName = "UserGroupTests - ImportUsersAsync_SendsTheCsv_AndReturnsEachResult")]
        public async Task ImportUsersAsync_SendsTheCsv_AndReturnsEachResult()
        {
            const string csv = "id,name,email,password,role,groups\nu4,,,Password1,User,\nu5,,,Password1,Wizard,\n";

            using var stub = Stub((method: "POST", path: "/users", body: "u4,Info,User created successfully\nu5,Error,Role with this name does not exist\n"));

            var server  = new EssServer(stub.BasePath, "admin", "password");
            var results = await server.ImportUsersAsync(new MemoryStream(Encoding.UTF8.GetBytes(csv)), TestContext.Current.CancellationToken);

            var request = Assert.Single(stub.Requests);

            Assert.StartsWith("POST /essbase/rest/v1/users ", request);
            Assert.Contains($"Content-Type: {_octetStream}", request, StringComparison.OrdinalIgnoreCase);
            Assert.Contains($"Accept: {_octetStream}",       request, StringComparison.OrdinalIgnoreCase);
            Assert.Equal(csv, Body(request));

            Assert.Collection(results,
                result => { Assert.Equal("u4", result.Id); Assert.True(result.Succeeded);  Assert.Equal("User created successfully", result.Message); },
                result => { Assert.Equal("u5", result.Id); Assert.False(result.Succeeded); Assert.Equal("Role with this name does not exist", result.Message); });
        }

        [Fact(DisplayName = "UserGroupTests - ImportGroupsAsync_SendsTheCsv_AndReturnsEachResult")]
        public async Task ImportGroupsAsync_SendsTheCsv_AndReturnsEachResult()
        {
            const string csv = "name,description,role,groups\ng3,Third,User,\n";

            using var stub = Stub((method: "POST", path: "/groups", body: "g3,Info,Group created successfully\n"));

            var server  = new EssServer(stub.BasePath, "admin", "password");
            var results = await server.ImportGroupsAsync(new MemoryStream(Encoding.UTF8.GetBytes(csv)), TestContext.Current.CancellationToken);

            var request = Assert.Single(stub.Requests);

            Assert.StartsWith("POST /essbase/rest/v1/groups ", request);
            Assert.Equal(csv, Body(request));
            Assert.True(Assert.Single(results).Succeeded);
        }

        [Fact(DisplayName = "UserGroupTests - ImportUsersAsync_WithoutAStream_Throws")]
        public async Task ImportUsersAsync_WithoutAStream_Throws()
        {
            using var stub = Stub();

            var server = new EssServer(stub.BasePath, "admin", "password");

            await Assert.ThrowsAsync<ArgumentNullException>(() => server.ImportUsersAsync(null, TestContext.Current.CancellationToken));
        }

        [Theory(DisplayName = "UserGroupTests - ExportAsync_AsksForTheFile_AndReturnsItsBytes")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ExportAsync_AsksForTheFile_AndReturnsItsBytes( bool users )
        {
            const string csv = "id,name\nu1,One\n";

            var path = users ? "/users" : "/groups";

            using var stub = Stub((method: "GET", path, body: csv));

            var server = new EssServer(stub.BasePath, "admin", "password");

            using var stream = users
                ? await server.ExportUsersAsync(TestContext.Current.CancellationToken)
                : await server.ExportGroupsAsync(TestContext.Current.CancellationToken);

            var request = Assert.Single(stub.Requests);

            Assert.StartsWith($"GET /essbase/rest/v1{path} ", request);
            Assert.Contains($"Accept: {_octetStream}", request, StringComparison.OrdinalIgnoreCase);
            Assert.Equal(csv, await new StreamReader(stream).ReadToEndAsync(TestContext.Current.CancellationToken));
        }

        #endregion

        #region Search

        [Fact(DisplayName = "UserGroupTests - GetUsersAsync_WithFilterAndLimit_PassesTheFilter_AndAppliesTheLimitItself")]
        public async Task GetUsersAsync_WithFilterAndLimit_PassesTheFilter_AndAppliesTheLimitItself()
        {
            // The server answers all three users whatever the limit, as Essbase does.
            using var stub = Stub((method: "GET", path: "/users", body: _users));

            var server = new EssServer(stub.BasePath, "admin", "password");

            var limited   = await server.GetUsersAsync("u*", limit: 2, cancellationToken: TestContext.Current.CancellationToken);
            var unlimited = await server.GetUsersAsync("u*", cancellationToken: TestContext.Current.CancellationToken);
            var all       = await server.GetUsersAsync(TestContext.Current.CancellationToken);

            Assert.Equal(new[] { "u1", "u2" }, limited.Select(user => user.Name));
            Assert.Equal(3, unlimited.Count);
            Assert.Equal(3, all.Count);

            Assert.Contains("filter=u", stub.Requests[0]);
            Assert.Contains("limit=2",  stub.Requests[0]);
            Assert.DoesNotContain("filter=", stub.Requests[2]);
        }

        [Fact(DisplayName = "UserGroupTests - GetGroupsAsync_WithFilterAndLimit_AppliesTheLimitItself")]
        public async Task GetGroupsAsync_WithFilterAndLimit_AppliesTheLimitItself()
        {
            using var stub = Stub((method: "GET", path: "/groups", body: @"{""items"":[{""name"":""g1"",""role"":""User""},{""name"":""g2"",""role"":""User""}]}"));

            var server = new EssServer(stub.BasePath, "admin", "password");

            var groups = await server.GetGroupsAsync("g*", limit: 1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal("g1", Assert.Single(groups).Name);
            Assert.Contains("filter=g", Assert.Single(stub.Requests));
        }

        #endregion

        #region Model

        [Fact(DisplayName = "UserGroupTests - GetUsersAsync_ReadsTheRoleOfEachUser")]
        public async Task GetUsersAsync_ReadsTheRoleOfEachUser()
        {
            using var stub = Stub((method: "GET", path: "/users", body: _users));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var users  = await server.GetUsersAsync(TestContext.Current.CancellationToken);

            Assert.Equal(new[] { EssServerRole.ServiceAdministrator, EssServerRole.PowerUser, EssServerRole.User }, users.Select(user => user.Role));
        }

        [Fact(DisplayName = "UserGroupTests - GetUsersAsync_ReadsEveryFormOfARoleName")]
        public async Task GetUsersAsync_ReadsEveryFormOfARoleName()
        {
            // Essbase names a role "Service Administrator" in a list of users, "ServiceAdministrator" for one user, and "service_administrator" in a session.
            using var stub = Stub((method: "GET", path: "/users", body: @"{""items"":[
                {""id"":""a"",""role"":""ServiceAdministrator""},{""id"":""b"",""role"":""service_administrator""},{""id"":""c"",""role"":""power_user""},
                {""id"":""d"",""role"":""POWER USER""},{""id"":""e"",""role"":""User""},{""id"":""f"",""role"":""auditor""},{""id"":""g""}]}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var users  = await server.GetUsersAsync(TestContext.Current.CancellationToken);

            Assert.Equal(
                new[] { EssServerRole.ServiceAdministrator, EssServerRole.ServiceAdministrator, EssServerRole.PowerUser, EssServerRole.PowerUser, EssServerRole.User, EssServerRole.Unkown, EssServerRole.Unkown },
                users.Select(user => user.Role));
        }

        [Fact(DisplayName = "UserGroupTests - GetMembersAsync_ReturnsUsersAndGroupsTogether_WithoutRoles")]
        public async Task GetMembersAsync_ReturnsUsersAndGroupsTogether_WithoutRoles()
        {
            // A member is described in brief: no role.
            using var stub = Stub(
                (method: "GET", path: "/groups/g1",                body: @"{""name"":""g1"",""role"":""User""}"),
                (method: "GET", path: "/groups/g1/members/users",  body: @"{""items"":[{""id"":""u1"",""name"":""One"",""email"":""one@example.com""}]}"),
                (method: "GET", path: "/groups/g1/members/groups", body: @"{""items"":[{""name"":""g2"",""description"":""Second""}]}"));

            var server  = new EssServer(stub.BasePath, "admin", "password");
            var group   = await server.GetGroupAsync("g1", TestContext.Current.CancellationToken);
            var members = await group.GetMembersAsync(TestContext.Current.CancellationToken);

            var user  = Assert.Single(members.Users);
            var child = Assert.Single(members.Groups);

            Assert.Equal("u1", user.Name);
            Assert.Equal(EssServerRole.Unkown, user.Role);
            Assert.Equal("g2", child.Name);
            Assert.Equal(EssServerRole.Unkown, child.Role);
            Assert.Equal("Second", child.Description);
        }

        #endregion

        #region Edit

        [Fact(DisplayName = "UserGroupTests - EditAsync_SendsTheWholeUser_KeepingWhatTheCallerLeavesOut")]
        public async Task EditAsync_SendsTheWholeUser_KeepingWhatTheCallerLeavesOut()
        {
            // The server replaces the whole user and requires a role. It answers a password with the password.
            using var stub = Stub(
                (method: "GET", path: "/users/u2", body: @"{""id"":""u2"",""name"":""Two"",""email"":""two@example.com"",""role"":""Power User""}"),
                (method: "PUT", path: "/users/u2", body: @"{""id"":""u2"",""name"":""Two"",""email"":""new@example.com"",""role"":""Power User"",""password"":""Secret123""}"),
                (method: "GET", path: "/users",    body: _users));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await server.GetUserAsync("u2", TestContext.Current.CancellationToken);

            var edited = await user.EditAsync(email: "new@example.com", cancellationToken: TestContext.Current.CancellationToken);

            var put  = Assert.Single(stub.Requests, request => request.StartsWith("PUT "));
            var body = JObject.Parse(Body(put));

            Assert.StartsWith("PUT /essbase/rest/v1/users/u2 ", put);
            Assert.Equal("Two",               (string)body["name"]);
            Assert.Equal("new@example.com",   (string)body["email"]);
            Assert.Equal("Power User",        (string)body["role"]);
            Assert.Null(body["password"]);

            Assert.Same(user, edited);
            Assert.Equal("new@example.com", edited.Email);
        }

        [Fact(DisplayName = "UserGroupTests - EditAsync_WithARoleAndAPassword_SendsThem")]
        public async Task EditAsync_WithARoleAndAPassword_SendsThem()
        {
            using var stub = Stub(
                (method: "GET", path: "/users/u2", body: @"{""id"":""u2"",""name"":""Two"",""email"":"""",""role"":""Power User""}"),
                (method: "PUT", path: "/users/u2", body: @"{""id"":""u2"",""name"":""Renamed"",""email"":"""",""role"":""Service Administrator"",""password"":""Secret123""}"),
                (method: "GET", path: "/users",    body: _users));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await server.GetUserAsync("u2", TestContext.Current.CancellationToken);

            await user.EditAsync("Renamed", role: EssServerRole.ServiceAdministrator, password: "Secret123", cancellationToken: TestContext.Current.CancellationToken);

            var body = JObject.Parse(Body(Assert.Single(stub.Requests, request => request.StartsWith("PUT "))));

            Assert.Equal("Renamed",               (string)body["name"]);
            Assert.Equal("Service Administrator", (string)body["role"]);
            Assert.Equal("Secret123",             (string)body["password"]);

            Assert.Equal("Renamed", user.FullName);
            Assert.Equal(EssServerRole.ServiceAdministrator, user.Role);
        }

        [Fact(DisplayName = "UserGroupTests - EditAsync_WhenTheServerAnswersForOneUserMoreBriefly_KeepsWhatItLeftOut")]
        public async Task EditAsync_WhenTheServerAnswersForOneUserMoreBriefly_KeepsWhatItLeftOut()
        {
            // For the built-in admin user, Essbase leaves the email out of its answer for one user, and names the role differently from its list.
            using var stub = Stub(
                (method: "GET", path: "/users",    body: @"{""items"":[{""id"":""u1"",""name"":""One"",""email"":""admin@example.com"",""role"":""Service Administrator""}]}"),
                (method: "GET", path: "/users/u1", body: @"{""id"":""u1"",""name"":"""",""email"":"""",""role"":""ServiceAdministrator""}"),
                (method: "PUT", path: "/users/u1", body: @"{""id"":""u1"",""name"":""Renamed"",""email"":""admin@example.com"",""role"":""Service Administrator""}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await GetListedUserAsync(server, "u1");

            await user.EditAsync(fullName: "Renamed", cancellationToken: TestContext.Current.CancellationToken);

            var body = JObject.Parse(Body(Assert.Single(stub.Requests, request => request.StartsWith("PUT "))));

            Assert.Equal("Renamed",               (string)body["name"]);
            Assert.Equal("admin@example.com",     (string)body["email"]);
            Assert.Equal("Service Administrator", (string)body["role"]);
        }

        [Fact(DisplayName = "UserGroupTests - EditAsync_SendsTheGroupsTheUserIsIn_SoTheServerKeepsThem")]
        public async Task EditAsync_SendsTheGroupsTheUserIsIn_SoTheServerKeepsThem()
        {
            // The server takes a user sent without groups out of every group they are in. Its answer for one user is the one to send
            // (here it has the user in g2 as well, since the list was read).
            using var stub = Stub(
                (method: "GET", path: "/users",    body: _users),
                (method: "GET", path: "/users/u1", body: @"{""id"":""u1"",""name"":""One"",""email"":""one@example.com"",""role"":""ServiceAdministrator"",""groups"":[""g1"",""g2""]}"),
                (method: "PUT", path: "/users/u1", body: @"{""id"":""u1"",""name"":""One"",""email"":""one@example.com"",""role"":""Power User"",""groups"":[""g1"",""g2""]}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await GetListedUserAsync(server, "u1");

            await user.EditAsync(role: EssServerRole.PowerUser, cancellationToken: TestContext.Current.CancellationToken);

            var body = JObject.Parse(Body(Assert.Single(stub.Requests, request => request.StartsWith("PUT "))));

            Assert.Equal(new[] { "g1", "g2" }, body["groups"].Select(group => (string)group));
            Assert.Equal("Power User",         (string)body["role"]);
            Assert.Equal(new[] { "g1", "g2" }, user.GroupNames);
        }

        [Fact(DisplayName = "UserGroupTests - EditAsync_TakesTheGroupsFromTheServersAnswerForOneUser_NotFromTheUserAsLastRead")]
        public async Task EditAsync_TakesTheGroupsFromTheServersAnswerForOneUser_NotFromTheUserAsLastRead()
        {
            // The list had the user in g1. Since then the user was taken out of it, and the server leaves the groups out of its answer for a user in none.
            using var stub = Stub(
                (method: "GET", path: "/users",    body: _users),
                (method: "GET", path: "/users/u1", body: @"{""id"":""u1"",""name"":""One"",""email"":""one@example.com"",""role"":""ServiceAdministrator""}"),
                (method: "PUT", path: "/users/u1", body: @"{""id"":""u1"",""name"":""One"",""email"":""one@example.com"",""role"":""Power User""}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await GetListedUserAsync(server, "u1");

            Assert.Equal(new[] { "g1" }, user.GroupNames);

            await user.EditAsync(role: EssServerRole.PowerUser, cancellationToken: TestContext.Current.CancellationToken);

            var body = JObject.Parse(Body(Assert.Single(stub.Requests, request => request.StartsWith("PUT "))));

            Assert.Empty(body["groups"] ?? new JArray());
            Assert.Empty(user.GroupNames ?? new List<string>());
        }

        [Fact(DisplayName = "UserGroupTests - EditAsync_OnAGroup_SendsTheGroupsTheGroupIsIn_SoTheServerKeepsThem")]
        public async Task EditAsync_OnAGroup_SendsTheGroupsTheGroupIsIn_SoTheServerKeepsThem()
        {
            // The server takes a group sent without groups out of every group it is in.
            using var stub = Stub(
                (method: "GET", path: "/groups/g1", body: @"{""name"":""g1"",""description"":""Old"",""role"":""Power User"",""groups"":[""parent""]}"),
                (method: "PUT", path: "/groups/g1", body: @"{""name"":""g1"",""description"":""New"",""role"":""Power User"",""groups"":[""parent""]}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var group  = await server.GetGroupAsync("g1", TestContext.Current.CancellationToken);

            await group.EditAsync(description: "New", cancellationToken: TestContext.Current.CancellationToken);

            var body = JObject.Parse(Body(Assert.Single(stub.Requests, request => request.StartsWith("PUT "))));

            Assert.Equal(new[] { "parent" }, body["groups"].Select(parent => (string)parent));
            Assert.Equal("Power User",       (string)body["role"]);
            Assert.Equal("New",              (string)body["description"]);
            Assert.Equal(new[] { "parent" }, group.GroupNames);
        }

        [Fact(DisplayName = "UserGroupTests - EditAsync_OnAGroup_KeepsTheRoleTheCallerLeavesOut")]
        public async Task EditAsync_OnAGroup_KeepsTheRoleTheCallerLeavesOut()
        {
            // The server requires a role, so a description alone is sent with the group's own role.
            using var stub = Stub(
                (method: "GET", path: "/groups/g1", body: @"{""name"":""g1"",""description"":""Old"",""role"":""Power User""}"),
                (method: "PUT", path: "/groups/g1", body: @"{""name"":""g1"",""description"":""New"",""role"":""Power User""}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var group  = await server.GetGroupAsync("g1", TestContext.Current.CancellationToken);

            var edited = await group.EditAsync(description: "New", cancellationToken: TestContext.Current.CancellationToken);

            var body = JObject.Parse(Body(Assert.Single(stub.Requests, request => request.StartsWith("PUT "))));

            Assert.Equal("Power User", (string)body["role"]);
            Assert.Equal("New",        (string)body["description"]);
            Assert.Equal("New",        edited.Description);
        }

        [Fact(DisplayName = "UserGroupTests - CreateGroupAsync_ForAServiceAdministrator_SendsTheRoleNameTheServerKnows")]
        public async Task CreateGroupAsync_ForAServiceAdministrator_SendsTheRoleNameTheServerKnows()
        {
            using var stub = Stub((method: "POST", path: "/groups", body: @"{""name"":""g1"",""role"":""Service Administrator""}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var group  = await server.CreateGroupAsync("g1", EssServerRole.ServiceAdministrator, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal("Service Administrator", (string)JObject.Parse(Body(Assert.Single(stub.Requests)))["role"]);
            Assert.Equal(EssServerRole.ServiceAdministrator, group.Role);
        }

        #endregion

        #region Provisioning Reports

        [Fact(DisplayName = "UserGroupTests - GetProvisioningReportAsync_AsksForTheWholeReport_AndConvertsIt")]
        public async Task GetProvisioningReportAsync_AsksForTheWholeReport_AndConvertsIt()
        {
            const string report = @"{""service"":{""roles"":{""items"":[{""name"":""power_user""}]}},""applications"":{""items"":[
                {""name"":""Sample"",""roles"":{""items"":[{""name"":""db_update"",""inheritedFromGroups"":[""g1""]}]},
                 ""filters"":{""items"":[{""database"":""Basic"",""items"":[{""name"":""F1""}]}]},
                 ""scripts"":{""items"":[]}},
                {""name"":""Demo"",""filters"":{""items"":[]},""scripts"":{""items"":[]}}]}}";

            using var stub = Stub(
                (method: "GET", path: "/users/u2/provisionReport", body: report),
                (method: "GET", path: "/users",                    body: _users));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await GetListedUserAsync(server, "u2");

            var result = await user.GetProvisioningReportAsync(TestContext.Current.CancellationToken);

            Assert.Contains("/users/u2/provisionReport?expand=all", Assert.Single(stub.Requests, request => request.Contains("provisionReport")));

            Assert.Equal("power_user", Assert.Single(result.ServiceRoles).Name);
            Assert.Equal(new[] { "Sample", "Demo" }, result.Applications.Select(application => application.Name));

            var sample = result.Applications[0];

            Assert.Equal("db_update", Assert.Single(sample.Roles).Name);
            Assert.Equal("g1",        Assert.Single(Assert.Single(sample.Roles).InheritedFromGroups));
            Assert.Equal("Basic",     Assert.Single(sample.Filters).Database);
            Assert.Equal("F1",        Assert.Single(Assert.Single(sample.Filters).Items).Name);
            Assert.Empty(sample.Scripts);
            Assert.Empty(result.Applications[1].Roles);
        }

        [Fact(DisplayName = "UserGroupTests - GetProvisionedRolesAsync_OnAGroup_AsksForTheRolesOfTheApplication")]
        public async Task GetProvisionedRolesAsync_OnAGroup_AsksForTheRolesOfTheApplication()
        {
            using var stub = Stub(
                (method: "GET", path: "/groups/g1/provisionReport/applications/Sample/roles", body: @"{""items"":[{""name"":""db_access""}]}"),
                (method: "GET", path: "/groups/g1",                                           body: @"{""name"":""g1"",""role"":""User""}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var group  = await server.GetGroupAsync("g1", TestContext.Current.CancellationToken);

            var roles = await group.GetProvisionedRolesAsync("Sample", TestContext.Current.CancellationToken);

            Assert.Equal("db_access", Assert.Single(roles).Name);
        }

        #endregion

        #region Permissions

        // What Essbase 26.1 answered to a User and a Power User, who may not manage users and groups: HTTP 400 with this message for most
        // operations, and for deleting several, HTTP 200 with the refusal in each row.

        private const string _notAuthorized = "User is not authorized to perform this operation";

        private const string _notAllowed = "oracle.essbase.security.EssbaseSecurityException: User [reg] is not allowed to perform operation.";

        [Theory(DisplayName = "UserGroupTests - DeleteAsync_WhenTheServerRefusesTheUser_ReturnsAFailedResultForEachRowWithoutThrowing")]
        [InlineData(true)]
        [InlineData(false)]
        public async Task DeleteAsync_WhenTheServerRefusesTheUser_ReturnsAFailedResultForEachRowWithoutThrowing( bool users )
        {
            using var stub = Stub((method: "POST", path: users ? "/users/actions/delete" : "/groups/actions/delete", body: $"a,{_notAllowed}\nb,{_notAllowed}\n"));

            var server  = new EssServer(stub.BasePath, "reg", "password");
            var results = users
                ? await server.DeleteUsersAsync(new[] { "a", "b" }, TestContext.Current.CancellationToken)
                : await server.DeleteGroupsAsync(new[] { "a", "b" }, TestContext.Current.CancellationToken);

            Assert.Equal(new[] { "a", "b" }, results.Select(result => result.Id));

            Assert.All(results, result =>
            {
                Assert.False(result.Succeeded);
                Assert.Equal(_notAllowed, result.Message);
            });
        }

        [Fact(DisplayName = "UserGroupTests - DeleteUsersAsync_WhenOnlySomeRowsAreRefused_ReportsEachRowOnItsOwn")]
        public async Task DeleteUsersAsync_WhenOnlySomeRowsAreRefused_ReportsEachRowOnItsOwn()
        {
            // The server never lets a user delete themselves, and answers that row alone.
            using var stub = Stub((method: "POST", path: "/users/actions/delete", body: $"u1,Deleted\nadmin,oracle.essbase.security.EssbaseSecurityException: Self delete operation not supported.\nu3,{_notAllowed}\n"));

            var server  = new EssServer(stub.BasePath, "admin", "password");
            var results = await server.DeleteUsersAsync(new[] { "u1", "admin", "u3" }, TestContext.Current.CancellationToken);

            Assert.Equal(new[] { true, false, false }, results.Select(result => result.Succeeded));
            Assert.Contains("Self delete operation not supported", results[1].Message);
            Assert.Contains("not allowed to perform operation",    results[2].Message);
        }

        [Fact(DisplayName = "UserGroupTests - GetUsersAsync_WhenTheServerRefusesTheUser_ThrowsWithTheServersMessage_NotNotSupported")]
        public async Task GetUsersAsync_WhenTheServerRefusesTheUser_ThrowsWithTheServersMessage_NotNotSupported()
        {
            using var stub = Stub();

            stub.ResponseProvider = Refuse(stub.ResponseProvider, "/users");

            var server = new EssServer(stub.BasePath, "reg", "password");

            var exception = await Assert.ThrowsAnyAsync<Exception>(() => server.GetUsersAsync(TestContext.Current.CancellationToken));

            Assert.IsNotType<NotSupportedException>(exception);
            Assert.Contains(_notAuthorized, exception.Message);
        }

        [Fact(DisplayName = "UserGroupTests - ImportUsersAsync_WhenTheServerRefusesTheUser_ThrowsWithTheServersMessage_NotNotSupported")]
        public async Task ImportUsersAsync_WhenTheServerRefusesTheUser_ThrowsWithTheServersMessage_NotNotSupported()
        {
            using var stub = Stub();

            stub.ResponseProvider = Refuse(stub.ResponseProvider, "/users");

            var server = new EssServer(stub.BasePath, "reg", "password");

            var exception = await Assert.ThrowsAnyAsync<Exception>(() => server.ImportUsersAsync(new MemoryStream(Encoding.UTF8.GetBytes("id,name,email,password,role,groups\nu4,,,Password1,User,\n")), TestContext.Current.CancellationToken));

            Assert.IsNotType<NotSupportedException>(exception);
            Assert.Contains(_notAuthorized, exception.Message);
        }

        [Fact(DisplayName = "UserGroupTests - EditAsync_WhenTheServerRefusesTheUser_ThrowsWithTheServersMessage_AndLeavesTheUserAsItWas")]
        public async Task EditAsync_WhenTheServerRefusesTheUser_ThrowsWithTheServersMessage_AndLeavesTheUserAsItWas()
        {
            using var stub = Stub((method: "GET", path: "/users", body: _users));

            stub.ResponseProvider = Refuse(stub.ResponseProvider, "/users/u2");

            var server = new EssServer(stub.BasePath, "reg", "password");
            var user   = await GetListedUserAsync(server, "u2");

            var exception = await Assert.ThrowsAnyAsync<Exception>(() => user.EditAsync(role: EssServerRole.ServiceAdministrator, cancellationToken: TestContext.Current.CancellationToken));

            Assert.IsNotType<NotSupportedException>(exception);
            Assert.Contains(@"Unable to edit user ""u2""", exception.Message);
            Assert.Contains(_notAuthorized,                exception.Message);

            Assert.Equal(EssServerRole.PowerUser, user.Role);
            Assert.DoesNotContain(stub.Requests, request => request.StartsWith("PUT "));
        }

        [Fact(DisplayName = "UserGroupTests - GetProvisioningReportAsync_WhenTheServerAnswersAUserWhoMayNotSeeIt_ReturnsAnEmptyReport")]
        public async Task GetProvisioningReportAsync_WhenTheServerAnswersAUserWhoMayNotSeeIt_ReturnsAnEmptyReport()
        {
            // The server answers a user who is not a service administrator, asking for the report of another user, with 200 and no applications.
            using var stub = Stub(
                (method: "GET", path: "/users/u2/provisionReport", body: @"{""applications"":{}}"),
                (method: "GET", path: "/users",                    body: _users));

            var server = new EssServer(stub.BasePath, "reg", "password");
            var user   = await GetListedUserAsync(server, "u2");

            var result = await user.GetProvisioningReportAsync(TestContext.Current.CancellationToken);

            Assert.Empty(result.ServiceRoles);
            Assert.Empty(result.Applications);
        }

        [Fact(DisplayName = "UserGroupTests - GetProvisionedRolesAsync_WhenTheServerRefusesTheApplication_ThrowsWithTheServersMessage")]
        public async Task GetProvisionedRolesAsync_WhenTheServerRefusesTheApplication_ThrowsWithTheServersMessage()
        {
            using var stub = Stub((method: "GET", path: "/users", body: _users));

            stub.ResponseProvider = Refuse(stub.ResponseProvider, "/users/u2/provisionReport/applications/Sample/roles");

            var server = new EssServer(stub.BasePath, "reg", "password");
            var user   = await GetListedUserAsync(server, "u2");

            var exception = await Assert.ThrowsAnyAsync<Exception>(() => user.GetProvisionedRolesAsync("Sample", TestContext.Current.CancellationToken));

            Assert.IsNotType<NotSupportedException>(exception);
            Assert.Contains(_notAuthorized, exception.Message);
        }

        #endregion

        #region Features

        [Fact(DisplayName = "UserGroupTests - GetUsersAsync_WithServedSpecificationLackingTheOperations_ThrowsNotSupported")]
        public async Task GetUsersAsync_WithServedSpecificationLackingTheOperations_ThrowsNotSupported()
        {
            // A server whose specification lacks the operations, and that answers them with 404.
            using var stub = Stub((method: "GET", path: "/openapi.json", body: @"{""openapi"":""3.0.1"",""paths"":{""/applications"":{""get"":{}}}}"));

            stub.ResponseProvider = Respond(stub.ResponseProvider, (HttpStatusCode.NotFound, "/users"), (HttpStatusCode.NotFound, "/groups/actions/delete"), (HttpStatusCode.NotFound, "/users/u1/provisionReport"));

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.False(await server.SupportsAsync(EssFeature.UserGroupManagement, TestContext.Current.CancellationToken));
            Assert.False(await server.SupportsAsync(EssFeature.ProvisioningReports, TestContext.Current.CancellationToken));

            await Assert.ThrowsAsync<NotSupportedException>(() => server.GetUsersAsync(TestContext.Current.CancellationToken));
            await Assert.ThrowsAsync<NotSupportedException>(() => server.DeleteGroupsAsync(new[] { "g1" }, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "UserGroupTests - GetUsersAsync_WithServedSpecificationDeclaringTheOperations_DoesNotTranslateTheFailure")]
        public async Task GetUsersAsync_WithServedSpecificationDeclaringTheOperations_DoesNotTranslateTheFailure()
        {
            var specification = @"{""openapi"":""3.0.1"",""paths"":{
                ""/users"":{""get"":{},""post"":{}},""/users/{id}"":{""put"":{}},""/users/actions/delete"":{""post"":{}},
                ""/groups"":{""get"":{},""post"":{}},""/groups/{id}"":{""put"":{}},""/groups/actions/delete"":{""post"":{}},
                ""/users/{userId}/provisionReport"":{""get"":{}},""/groups/{groupId}/provisionReport"":{""get"":{}}}}";

            using var stub = Stub((method: "GET", path: "/openapi.json", body: specification));

            stub.ResponseProvider = Respond(stub.ResponseProvider, (HttpStatusCode.NotFound, "/users"));

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.True(await server.SupportsAsync(EssFeature.UserGroupManagement, TestContext.Current.CancellationToken));
            Assert.True(await server.SupportsAsync(EssFeature.ProvisioningReports, TestContext.Current.CancellationToken));

            var exception = await Assert.ThrowsAnyAsync<Exception>(() => server.GetUsersAsync(TestContext.Current.CancellationToken));

            Assert.IsNotType<NotSupportedException>(exception);
        }

        [Fact(DisplayName = "UserGroupTests - SupportsAsync_InIdentityCloudMode_RulesOutUserGroupManagement_ButNotProvisioningReports")]
        public async Task SupportsAsync_InIdentityCloudMode_RulesOutUserGroupManagement_ButNotProvisioningReports()
        {
            using var stub = IdentityCloudStub();

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.False(await server.SupportsAsync(EssFeature.UserGroupManagement, TestContext.Current.CancellationToken));
            Assert.True(await server.SupportsAsync(EssFeature.ProvisioningReports, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "UserGroupTests - SupportsAsync_WithTheIdentityCloudFlagReportedFalseOrNotAtAll_AnswersSupported")]
        public async Task SupportsAsync_WithTheIdentityCloudFlagReportedFalseOrNotAtAll_AnswersSupported()
        {
            foreach (var instance in new[] { @"{""idcs"":false}", @"{""aiEnabled"":true}" })
            {
                using var stub = Stub((method: "GET", path: "/about/instance", body: instance));

                var server = new EssServer(stub.BasePath, "admin", "password");

                Assert.True(await server.SupportsAsync(EssFeature.UserGroupManagement, TestContext.Current.CancellationToken));
            }
        }

        [Fact(DisplayName = "UserGroupTests - Members_InIdentityCloudMode_ThrowNotSupported_WhatTheServerAnswered")]
        public async Task Members_InIdentityCloudMode_ThrowNotSupported_WhatTheServerAnswered()
        {
            // The server declares the operations and answers them with HTTP 400, so only its report of the mode shows it lacks them.
            using var stub = IdentityCloudStub((method: "GET", path: "/groups/g1", body: @"{""name"":""g1"",""role"":""User""}"));

            var server = new EssServer(stub.BasePath, "admin", "password");

            var exception = await Assert.ThrowsAsync<NotSupportedException>(() => server.GetUsersAsync(TestContext.Current.CancellationToken));

            Assert.Contains(_identityCloudRefusal, exception.Message);

            await Assert.ThrowsAsync<NotSupportedException>(() => server.GetGroupsAsync("g*", cancellationToken: TestContext.Current.CancellationToken));
            await Assert.ThrowsAsync<NotSupportedException>(() => server.ExportUsersAsync(TestContext.Current.CancellationToken));

            var group = await server.GetGroupAsync("g1", TestContext.Current.CancellationToken);

            await Assert.ThrowsAsync<NotSupportedException>(() => group.EditAsync(description: "Described", cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "UserGroupTests - GetUserAsync_InIdentityCloudMode_GetsTheOneUser_SoItsProvisioningReportCanBeRead")]
        public async Task GetUserAsync_InIdentityCloudMode_GetsTheOneUser_SoItsProvisioningReportCanBeRead()
        {
            using var stub = IdentityCloudStub(
                (method: "GET", path: "/users/admin",                 body: @"{""id"":""admin"",""name"":""Essbase Administrator"",""role"":""Service Administrator"",""groups"":[""essbase-admins""]}"),
                (method: "GET", path: "/users/admin/provisionReport", body: @"{""service"":{""roles"":{""items"":[{""name"":""service_administrator""}]}}}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await server.GetUserAsync("admin", TestContext.Current.CancellationToken);

            Assert.Equal("admin", user.Name);
            Assert.Equal(EssServerRole.ServiceAdministrator, user.Role);

            var report = await user.GetProvisioningReportAsync(TestContext.Current.CancellationToken);

            Assert.Equal("service_administrator", Assert.Single(report.ServiceRoles).Name);
        }

        [Fact(DisplayName = "UserGroupTests - RemovePermissionsAsync_ForAGroup_TellsTheServerItIsAGroup")]
        public async Task RemovePermissionsAsync_ForAGroup_TellsTheServerItIsAGroup()
        {
            using var stub = Stub(
                (method: "GET",    path: "/applications/Sample",                body: @"{""name"":""Sample""}"),
                (method: "GET",    path: "/applications/Sample/permissions/g1", body: @"{""id"":""g1"",""role"":""db_update"",""group"":true}"),
                (method: "DELETE", path: "/applications/Sample/permissions/g1", body: "{}"));

            var server      = new EssServer(stub.BasePath, "admin", "password");
            var application = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken);
            var permission  = await application.GetPermissionAsync("g1", group: true,TestContext.Current.CancellationToken);

            await permission.RemovePermissionsAsync(TestContext.Current.CancellationToken);

            var delete = Assert.Single(stub.Requests, request => request.StartsWith("DELETE ", StringComparison.Ordinal));

            Assert.Contains("/applications/Sample/permissions/g1?group=true ", delete);
        }

        #endregion

        #region Service Roles

        [Fact(DisplayName = "UserGroupTests - SetServiceRoleAsync_OnAUser_SendsTheRoleAsServiceRoleProvisioningNamesIt")]
        public async Task SetServiceRoleAsync_OnAUser_SendsTheRoleAsServiceRoleProvisioningNamesIt()
        {
            using var stub = Stub((method: "GET", path: "/users", body: _users), (method: "PUT", path: "/permissions/u3", body: "{}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await GetListedUserAsync(server, "u3");

            await user.SetServiceRoleAsync(EssServerRole.PowerUser, TestContext.Current.CancellationToken);

            var put  = Assert.Single(stub.Requests, request => request.StartsWith("PUT ", StringComparison.Ordinal));
            var body = JObject.Parse(Body(put));

            Assert.StartsWith("PUT /essbase/rest/v1/permissions/u3 ", put);
            Assert.Equal("u3",         (string)body["id"]);
            Assert.Equal("power_user", (string)body["role"]);
            Assert.False((bool)body["group"]);
            Assert.Equal(EssServerRole.PowerUser, user.Role);
        }

        [Fact(DisplayName = "UserGroupTests - SetServiceRoleAsync_OnAGroup_TellsTheServerItIsAGroup")]
        public async Task SetServiceRoleAsync_OnAGroup_TellsTheServerItIsAGroup()
        {
            using var stub = Stub((method: "GET", path: "/groups/g1", body: @"{""name"":""g1"",""role"":""User""}"), (method: "PUT", path: "/permissions/g1", body: "{}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var group  = await server.GetGroupAsync("g1", TestContext.Current.CancellationToken);

            await group.SetServiceRoleAsync(EssServerRole.ServiceAdministrator, TestContext.Current.CancellationToken);

            var body = JObject.Parse(Body(Assert.Single(stub.Requests, request => request.StartsWith("PUT ", StringComparison.Ordinal))));

            Assert.Equal("service_administrator", (string)body["role"]);
            Assert.True((bool)body["group"]);
            Assert.Equal(EssServerRole.ServiceAdministrator, group.Role);
        }

        [Fact(DisplayName = "UserGroupTests - SetServiceRoleAsync_WithUnkown_ThrowsWithoutAskingTheServer")]
        public async Task SetServiceRoleAsync_WithUnkown_ThrowsWithoutAskingTheServer()
        {
            using var stub = Stub((method: "GET", path: "/users", body: _users));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await GetListedUserAsync(server, "u3");

            stub.Requests.Clear();

            await Assert.ThrowsAsync<ArgumentException>(() => user.SetServiceRoleAsync(EssServerRole.Unkown, TestContext.Current.CancellationToken));
            Assert.Empty(stub.Requests);
        }

        [Fact(DisplayName = "UserGroupTests - GetServiceRoleAsync_ReadsTheRole_OrNullWhenTheServerReportsNone")]
        public async Task GetServiceRoleAsync_ReadsTheRole_OrNullWhenTheServerReportsNone()
        {
            // The server answers 404 for a group that holds no service role.
            using var stub = Stub(
                (method: "GET", path: "/users",          body: _users),
                (method: "GET", path: "/groups/g1",      body: @"{""name"":""g1""}"),
                (method: "GET", path: "/permissions/u1", body: @"{""id"":""u1"",""role"":""service_administrator""}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await GetListedUserAsync(server, "u1");
            var group  = await server.GetGroupAsync("g1", TestContext.Current.CancellationToken);

            Assert.Equal(EssServerRole.ServiceAdministrator, await user.GetServiceRoleAsync(TestContext.Current.CancellationToken));
            Assert.Null(await group.GetServiceRoleAsync(TestContext.Current.CancellationToken));

            Assert.Contains(stub.Requests, request => request.StartsWith("GET /essbase/rest/v1/permissions/g1?group=true ", StringComparison.Ordinal));
        }

        [Fact(DisplayName = "UserGroupTests - RemoveServiceRoleAsync_OnAGroup_TellsTheServerItIsAGroup")]
        public async Task RemoveServiceRoleAsync_OnAGroup_TellsTheServerItIsAGroup()
        {
            using var stub = Stub((method: "GET", path: "/groups/g1", body: @"{""name"":""g1"",""role"":""Power User""}"), (method: "DELETE", path: "/permissions/g1", body: "{}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var group  = await server.GetGroupAsync("g1", TestContext.Current.CancellationToken);

            await group.RemoveServiceRoleAsync(TestContext.Current.CancellationToken);

            Assert.Contains(stub.Requests, request => request.StartsWith("DELETE /essbase/rest/v1/permissions/g1?group=true ", StringComparison.Ordinal));
            Assert.Equal(EssServerRole.Unkown, group.Role);
        }

        [Fact(DisplayName = "UserGroupTests - GetServiceRoleAssignmentsAsync_AsksForUsersAndGroupsApart_AndSaysWhichEachIs")]
        public async Task GetServiceRoleAssignmentsAsync_AsksForUsersAndGroupsApart_AndSaysWhichEachIs()
        {
            using var stub = new LoopbackEssbaseStub();

            stub.ResponseProvider = requestLine => requestLine switch
            {
                _ when requestLine.Contains("/permissions?") && requestLine.Contains("filter=user")  => (HttpStatusCode.OK, @"{""items"":[{""id"":""alice"",""role"":""service_administrator""},{""id"":""bob"",""role"":""user""}]}"),
                _ when requestLine.Contains("/permissions?") && requestLine.Contains("filter=group") => (HttpStatusCode.OK, @"{""items"":[{""id"":""admins"",""role"":""power_user""}]}"),
                _                                                                                     => (HttpStatusCode.NotFound, @"{""message"":""Not Found""}")
            };

            var server      = new EssServer(stub.BasePath, "admin", "password");
            var assignments = await server.GetServiceRoleAssignmentsAsync("a*", roles: new[] { EssServerRole.ServiceAdministrator, EssServerRole.PowerUser }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Collection(assignments,
                assignment => { Assert.Equal("alice",  assignment.Id); Assert.Equal(EssPermissionType.User,  assignment.Type); Assert.Equal(EssServerRole.ServiceAdministrator, assignment.Role); },
                assignment => { Assert.Equal("bob",    assignment.Id); Assert.Equal(EssPermissionType.User,  assignment.Type); Assert.Equal(EssServerRole.User,                 assignment.Role); },
                assignment => { Assert.Equal("admins", assignment.Id); Assert.Equal(EssPermissionType.Group, assignment.Type); Assert.Equal(EssServerRole.PowerUser,            assignment.Role); });

            Assert.All(stub.Requests.Where(request => request.Contains("/permissions?")), request =>
            {
                var requestLine = Uri.UnescapeDataString(request.Split('\r')[0]);

                Assert.Contains("id=a*", requestLine);
                Assert.Contains("role=service_administrator,power_user", requestLine);
            });
        }

        [Fact(DisplayName = "UserGroupTests - SetServiceRoleAsync_InIdentityCloudMode_Works_WhereEditDoesNot")]
        public async Task SetServiceRoleAsync_InIdentityCloudMode_Works_WhereEditDoesNot()
        {
            using var stub = IdentityCloudStub(
                (method: "GET", path: "/users/carol",       body: @"{""id"":""carol"",""name"":""Carol""}"),
                (method: "PUT", path: "/permissions/carol", body: "{}"));

            var server = new EssServer(stub.BasePath, "admin", "password");
            var user   = await server.GetUserAsync("carol", TestContext.Current.CancellationToken);

            Assert.True(await server.SupportsAsync(EssFeature.ServiceRoleProvisioning, TestContext.Current.CancellationToken));

            await Assert.ThrowsAsync<NotSupportedException>(() => user.EditAsync(role: EssServerRole.PowerUser, cancellationToken: TestContext.Current.CancellationToken));

            await user.SetServiceRoleAsync(EssServerRole.PowerUser, TestContext.Current.CancellationToken);

            Assert.Equal(EssServerRole.PowerUser, user.Role);
        }

        #endregion

        #region Helpers

        private const string _identityCloudRefusal = "This operation is not supported. Contact administrator";

        /// <summary>
        /// Returns the user with the given ID as the server's list of users reads it. <see cref="IEssServer.GetUserAsync(string, System.Threading.CancellationToken)" />
        /// gets the one user instead, so a test of how a member treats the user as last read gets the user here.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="id">The user's ID.</param>
        private static async Task<IEssUser> GetListedUserAsync( IEssServer server, string id ) =>
            Assert.Single(await server.GetUsersAsync(TestContext.Current.CancellationToken), user => user.Name == id);

        /// <summary>
        /// Returns a stub of a server in Identity Cloud mode: it reports the mode in its instance details, answers each of the given
        /// operations with its body, refuses every other users and groups request as such a server does (HTTP 400), and answers
        /// every other request with a 404.
        /// </summary>
        /// <param name="operations">The HTTP method and path (after the base path), and the body to answer with.</param>
        private static LoopbackEssbaseStub IdentityCloudStub( params (string method, string path, string body)[] operations )
        {
            var stub  = Stub(operations.Append((method: "GET", path: "/about/instance", body: @"{""idcs"":true,""serviceRoleProvisioningSupported"":true,""appRoleProvisioningSupported"":true}")).ToArray());
            var inner = stub.ResponseProvider;

            stub.ResponseProvider = requestLine =>
            {
                var path = (requestLine.Split(' ').ElementAtOrDefault(1) ?? string.Empty).Split('?')[0];

                if (inner(requestLine) is { StatusCode: HttpStatusCode.OK } answer)
                    return answer;

                return path.StartsWith("/essbase/rest/v1/users", StringComparison.Ordinal) || path.StartsWith("/essbase/rest/v1/groups", StringComparison.Ordinal)
                    ? (HttpStatusCode.BadRequest, $@"{{""errorMessage"":""{_identityCloudRefusal}""}}")
                    : (HttpStatusCode.NotFound, @"{""message"":""Not Found""}");
            };

            return stub;
        }

        /// <summary>
        /// Returns a stub that answers each of the given operations with its body, as <c>application/octet-stream</c> where the
        /// body is CSV, and every other request with a 404.
        /// </summary>
        /// <param name="operations">The HTTP method and path (after the base path), and the body to answer with.</param>
        private static LoopbackEssbaseStub Stub( params (string method, string path, string body)[] operations )
        {
            var stub = new LoopbackEssbaseStub();

            stub.ResponseProvider = requestLine =>
            {
                var parts  = requestLine.Split(' ');
                var method = parts.ElementAtOrDefault(0);
                var path   = (parts.ElementAtOrDefault(1) ?? string.Empty).Split('?')[0];

                foreach ( var operation in operations )
                {
                    if ( method == operation.method && path == "/essbase/rest/v1" + operation.path )
                    {
                        stub.ContentType = operation.body.TrimStart().StartsWith("{") ? "application/json" : _octetStream;

                        return (HttpStatusCode.OK, operation.body);
                    }
                }

                return (HttpStatusCode.NotFound, @"{""message"":""Not Found""}");
            };

            return stub;
        }

        /// <summary>
        /// Returns a response provider that answers a GET of each of the given paths with the given status code, and every
        /// other request as the given provider does.
        /// </summary>
        /// <param name="inner">The provider for every other request.</param>
        /// <param name="failures">A status code, and the path (after the base path) to answer with it.</param>
        private static Func<string, (HttpStatusCode StatusCode, string Body)?> Respond( Func<string, (HttpStatusCode StatusCode, string Body)?> inner, params (HttpStatusCode statusCode, string path)[] failures ) => requestLine =>
        {
            var parts = requestLine.Split(' ');
            var path  = (parts.ElementAtOrDefault(1) ?? string.Empty).Split('?')[0];

            foreach ( var failure in failures )
            {
                if ( path == "/essbase/rest/v1" + failure.path )
                    return (failure.statusCode, @"{""message"":""Failed""}");
            }

            return inner(requestLine);
        };

        /// <summary>
        /// Returns a response provider that answers every request for each of the given paths, whatever its method, as Essbase answers
        /// a user who may not do it (HTTP 400, with a message), and every other request as the given provider does.
        /// </summary>
        /// <param name="inner">The provider for every other request.</param>
        /// <param name="paths">The paths (after the base path) to refuse.</param>
        private static Func<string, (HttpStatusCode StatusCode, string Body)?> Refuse( Func<string, (HttpStatusCode StatusCode, string Body)?> inner, params string[] paths ) => requestLine =>
        {
            var parts = requestLine.Split(' ');
            var path  = (parts.ElementAtOrDefault(1) ?? string.Empty).Split('?')[0];

            return paths.Any(refused => path == "/essbase/rest/v1" + refused)
                ? (HttpStatusCode.BadRequest, $@"{{""errorMessage"":""{_notAuthorized}""}}")
                : inner(requestLine);
        };

        /// <summary>
        /// Returns the body of the given raw request.
        /// </summary>
        /// <param name="request">The raw request.</param>
        private static string Body( string request ) =>
            request.Substring(request.IndexOf("\r\n\r\n", StringComparison.Ordinal) + 4);

        #endregion
    }
}
