using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />
    /// </summary>
    [CollectionDefinition(nameof(CreateServerObjectTests), DisableParallelization = true), CollectionPriority(4)]
    public class CreateServerObjectTestsCollection : ICollectionFixture<CollectionFixture> { }

    /// <summary>
    /// Tests for ability to create server objects/stand up a server for subsequent testing.
    /// </summary>
    [Collection(nameof(CreateServerObjectTests)), Trait("type", "create")]
    public class CreateServerObjectTests : IntegrationTestBase
    {
        /// <summary />
        /// <param name="output" />
        public CreateServerObjectTests( ITestOutputHelper output ) : base(output) { }

        [Fact(DisplayName = "CreateServerObjectTests - 01 - Essbase_AfterClean_CanCreateSampleApplications"), Priority(01)]
        public async Task Essbase_AfterClean_CanCreateSampleApplications()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the list of applications.
            var applications = await server.GetApplicationsAsync(TestContext.Current.CancellationToken);

            // Assert that server contains no applications before we create them.
            Assert.Empty(applications);

            // Get the application workbook for Sample.Basic.
            var sampleWorkbook = await server.GetFileAsync(@"/gallery/Applications/Demo Samples/Block Storage/Sample_Basic.xlsx", TestContext.Current.CancellationToken);
            // Create the application.
            await server.CreateApplicationFromWorkbookAsync("Sample", "Basic", new EssJobImportExcelOptions(sampleWorkbook, buildOption: EssBuildOption.NONE), TestContext.Current.CancellationToken);

            // Get the application workbook for Demo.Basic.
            var demoWorkbook = await server.GetFileAsync(@"/gallery/Applications/Demo Samples/Block Storage/Demo_Basic.xlsx", TestContext.Current.CancellationToken);
            // Create the application.
            await server.CreateApplicationFromWorkbookAsync("Demo", "Basic", new EssJobImportExcelOptions(demoWorkbook, buildOption: EssBuildOption.NONE), TestContext.Current.CancellationToken);

            // Get the application workbook and data file for ASOSamp.Basic.
            var asoSampleWorkbook = await server.GetFileAsync(@"/gallery/Applications/Demo Samples/Aggregate Storage/ASO_Sample.xlsx", TestContext.Current.CancellationToken);
            var asoSampleDataFile = await server.GetFileAsync(@"/gallery/Applications/Demo Samples/Aggregate Storage/ASO_Sample_Data.txt", TestContext.Current.CancellationToken);

            // Create the application and get the Basic cube.
            var asoSample = await server.CreateApplicationFromWorkbookAsync("ASOSamp", "Basic", new EssJobImportExcelOptions(asoSampleWorkbook, buildOption: EssBuildOption.NONE, loadData: false), TestContext.Current.CancellationToken)
                .GetCubeAsync("Basic", TestContext.Current.CancellationToken);

            // Load the data into the Basic cube.
            await asoSample.LoadDataToCubeAsync(new EssJobLoadDataOptions(essDataFile: asoSampleDataFile), TestContext.Current.CancellationToken);

            // Get the list of applications.
            applications = await server.GetApplicationsAsync(TestContext.Current.CancellationToken);

            // Assert that server now contains 3 applications (Sample, Demo, and ASOSamp).
            Assert.Equal(3, applications?.Count);
        }

        [Fact(DisplayName = @"CreateServerObjectTests - 02 - Essbase_AfterClean_CanCreateGroup"), Priority(02)]
        public async Task Essbase_AfterClean_CanCreateGroups()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Assert that a server in Identity Cloud mode refuses to create a group.
            if (await IsIdentityCloudAsync(server))
            {
                await AssertUserManagementRefusedAsync(() => server.CreateGroupAsync("Test_Group", EssServerRole.PowerUser, "test group", TestContext.Current.CancellationToken));
                return;
            }

            // TODO: create Group Creation Class and a proper To

            // Create group one
            var group1 = await server.CreateGroupAsync("Test_Group", EssServerRole.PowerUser, "test group", TestContext.Current.CancellationToken);

            // Create group 2 - will add to group one in PerformServerFunctionTest
            var group2 = await server.CreateGroupAsync("Test_Group_2", EssServerRole.PowerUser, "test group 2", TestContext.Current.CancellationToken);

            // Assert that the group name, role, and description is correct is correct
            Assert.Equal("Test_Group", group1.Name);
            Assert.Equal(EssServerRole.PowerUser, group1.Role);
            Assert.Equal("test group", group1.Description);


            Assert.Equal("Test_Group_2", group2.Name);
            Assert.Equal(EssServerRole.PowerUser, group2.Role);
            Assert.Equal("test group 2", group2.Description);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 03 - Essbase_AfterCubeCreation_CanCreateUser"), Priority(03)]
        public async Task Essbase_AfterCubeCreation_CanCreateUser()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var userConnection = GetEssConnection(EssServerRole.User);

            // Create EssUserCreationOptions
            var options = new EssUserCreationOptions(userConnection.Username, userConnection.Password, userConnection.Role);

            // Assert that a server in Identity Cloud mode refuses to create a user.
            if (await IsIdentityCloudAsync(server))
            {
                await AssertUserManagementRefusedAsync(() => server.CreateUserAsync(options, TestContext.Current.CancellationToken));
                return;
            }

            var newUser = await server.CreateUserAsync(options, TestContext.Current.CancellationToken);

            Assert.Equal(userConnection.Username, newUser.Name);

            Assert.Equal(userConnection.Role, newUser.Role);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 04 - Essbase_AfterCubeCreation_CanCreateUserPermissions"), Priority(04)]
        public async Task Essbase_AfterCubeCreation_CanCreateUserPermissions()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var userConnection = GetEssConnection(EssServerRole.User);

            var app = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken);

            var userPermissions = await app.CreatePermissionsAsync(userConnection.Username, EssApplicationRole.db_access, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(EssApplicationRole.db_access, userPermissions.Role);

            Assert.Equal(userConnection.Username, userPermissions.Name);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 05 - Essbase_AfterCubeCreation_CanCreateGroupPermissions"), Priority(05)]
        public async Task Essbase_AfterCubeCreation_CanCreateGroupPermissions()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // A server in Identity Cloud mode cannot create a group, but gives application permissions to the identity
            // service's groups.
            var group = await server.GetGroupAsync(await GetTestGroupNameAsync(server), TestContext.Current.CancellationToken);

            var app = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken);

            var groupPermissions = await app.CreatePermissionsAsync(group.Name, EssApplicationRole.db_update, true, TestContext.Current.CancellationToken);

            Assert.Equal(EssApplicationRole.db_update, groupPermissions.Role);

            Assert.Equal(group.Name, groupPermissions.Name);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 05 - Essbase_AfterCubeCreation_CanCreateMdxScript"), Priority(05)]
        public async Task Essbase_AfterCubeCreation_CanCreateMdxScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube.
            var cube = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken)
                .GetCubeAsync("Basic", TestContext.Current.CancellationToken);

            // Assert that the cube does not contain an MDX script called "test" before we create one.
            Assert.DoesNotContain(collection: await cube.GetScriptsAsync<IEssMdxScript>(cancellationToken: TestContext.Current.CancellationToken), mdx => string.Equals(mdx?.Name, "test", StringComparison.Ordinal));

            // Create some script content.
            var content = @"SELECT {[Market]} ON COLUMNS, {[YEAR]} ON ROWS";

            // Create a script on the server with the given content.
            await cube.CreateScriptAsync<IEssMdxScript>("test", content, cancellationToken: TestContext.Current.CancellationToken);

            // Get the script back from the server.
            var script = await cube.GetScriptAsync<IEssMdxScript>("test", getContent: true, TestContext.Current.CancellationToken);

            // Assert that the script exists and contains the content we gave it.
            Assert.Equal(content, script?.Content);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 06 - Essbase_AfterCubeCreation_CanCreateReportScript"), Priority(06)]
        public async Task Essbase_AfterCubeCreation_CanCreateReportScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube.
            var cube = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken)
                .GetCubeAsync("Basic", TestContext.Current.CancellationToken);

            // Assert that the cube does not contain an report script called "test" before we create one.
            Assert.DoesNotContain(collection: await cube.GetScriptsAsync<IEssReportScript>(cancellationToken: TestContext.Current.CancellationToken), report => string.Equals(report?.Name, "test", StringComparison.Ordinal));

            // Create some script content.
            var content = 
@"<PAGE(""Measures"")
""Sales""
<COLUMN(""Scenario"", ""Year"")
""Scenario""
""Jan"" ""Feb"" ""Mar"" ""Apr""
<ROW(""Market"", ""Product"")
""New York""
""Product"" ""100"" ""100-10""
!";

            // Create a script on the server with the given content.
            await cube.CreateScriptAsync<IEssReportScript>("test", content, cancellationToken: TestContext.Current.CancellationToken);

            // Get the script back from the server.
            var script = await cube.GetScriptAsync<IEssReportScript>("test", getContent: true, TestContext.Current.CancellationToken);

            // Assert that the script exists and contains the content we gave it.
            Assert.Equal(content, script?.Content);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 07 - Essbase_AfterCubeCreation_CanCreateMaxLScript"), Priority(07)]
        public async Task Essbase_AfterCubeCreation_CanCreateMaxLScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube.
            var cube = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken)
                .GetCubeAsync("Basic", TestContext.Current.CancellationToken);

            // Assert that the cube does not contain an MaxL script called "test" before we create one.
            Assert.DoesNotContain(collection: await cube.GetScriptsAsync<IEssMaxlScript>(cancellationToken: TestContext.Current.CancellationToken), maxl => string.Equals(maxl?.Name, "test", StringComparison.Ordinal));

            // Create some script content.
            var content = @"query database sample.basic get dbstats dimension;";

            // Create a script on the server with the given content.
            await cube.CreateScriptAsync<IEssMaxlScript>("test", content, cancellationToken: TestContext.Current.CancellationToken);

            // Get the script back from the server.
            var script = await cube.GetScriptAsync<IEssMaxlScript>("test", getContent: true, TestContext.Current.CancellationToken);

            // Assert that the script exists and contains the content we gave it.
            Assert.Equal(content, script?.Content);
        }

        [Fact(DisplayName = @"CreateServerObjectTests - 08 - Essbase_AfterCubeCreation_CanCreateLockOnScript"), Priority(08)]
        public async Task Essbase_AfterCubeCreation_CanCreateLockOnScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the "CalcAll" script from Sample.Basic.
            var script = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken)
                .GetCubeAsync("Basic", TestContext.Current.CancellationToken)
                .GetScriptAsync<IEssCalcScript>("CalcAll", cancellationToken: TestContext.Current.CancellationToken);

            // Create lock options object.
            var options = new EssLockOptions(script.Name, EssLockedFileType.CALCSCRIPT);

            // Lock Script on server.
            var lockedScript = await script.Cube.CreateLockObjectAsync(options, TestContext.Current.CancellationToken);

            // Assert that the lock object name is the same as the one we passed.
            Assert.Equal("CalcAll", lockedScript.Name);
        }

        [Fact(DisplayName = @"CreateServerObjectTests - 09 - Essbase_AfterCubeCreation_CanCreateCalcScriptWithRuntimeSubstitutionVariables"), Priority(09)]
        public async Task Essbase_AfterCubeCreation_CanCreateCalcScriptWithRuntimeSubstitutionVariables()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube.
            var cube = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken)
                .GetCubeAsync("Basic", TestContext.Current.CancellationToken);

            // Read the calc script the scoop essbase stack adds to Sample.Basic when it sets up, from the same definition:
            // CalcProduct, which calculates one product (the runtime substitution variable CalcProduct, a single Product member)
            // in the markets the user chooses (CalcMarkets, one or more Market members).
            using var definition = JsonDocument.Parse(await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "Scripts", "Essbase", "json", "create-calc-product-script.json"), TestContext.Current.CancellationToken));

            var name    = definition.RootElement.GetProperty("name").GetString();
            var content = definition.RootElement.GetProperty("content").GetString();

            // Assert that the cube does not contain the calc script before we create it.
            Assert.DoesNotContain(collection: await cube.GetScriptsAsync<IEssCalcScript>(cancellationToken: TestContext.Current.CancellationToken), calc => string.Equals(calc?.Name, name, StringComparison.Ordinal));

            // Create the script on the server with the definition's content.
            await cube.CreateScriptAsync<IEssCalcScript>(name, content, cancellationToken: TestContext.Current.CancellationToken);

            // Get the script back from the server.
            var script = await cube.GetScriptAsync<IEssCalcScript>(name, getContent: true, TestContext.Current.CancellationToken);

            // Assert that the script exists and contains the content we gave it, runtime substitution variables included.
            Assert.Equal(content, script?.Content);
            Assert.Contains("SET RUNTIMESUBVARS", script?.Content, StringComparison.Ordinal);
        }
    }
}
