using System;
using System.Linq;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;


namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), Trait("type", "create"), CollectionPriority(4)]
    public class CreateServerObjectTests : IntegrationTestBase
    {
        [Fact(DisplayName = "CreateServerObjectTests - 01 - Essbase_AfterClean_CanCreateSampleApplications"), Priority(01)]
        public async Task Essbase_AfterClean_CanCreateSampleApplications()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the list of applications.
            var applications = await server.GetApplicationsAsync();

            // Assert that server contains no applications before we create them.
            Assert.Empty(applications);

            // Get the application workbook for Sample.Basic.
            var sampleWorkbook = await server.GetFileAsync(@"/gallery/Applications/Demo Samples/Block Storage/Sample_Basic.xlsx");
            // Create the application.
            await server.CreateApplicationFromWorkbookAsync("Sample", "Basic", new EssJobImportExcelOptions(sampleWorkbook, buildOption: EssBuildOption.NONE));

            // Get the application workbook for Demo.Basic.
            var demoWorkbook = await server.GetFileAsync(@"/gallery/Applications/Demo Samples/Block Storage/Demo_Basic.xlsx");
            // Create the application.
            await server.CreateApplicationFromWorkbookAsync("Demo", "Basic", new EssJobImportExcelOptions(demoWorkbook, buildOption: EssBuildOption.NONE));

            // Get the application workbook and data file for ASOSamp.Basic.
            var asoSampleWorkbook = await server.GetFileAsync(@"/gallery/Applications/Demo Samples/Aggregate Storage/ASO_Sample.xlsx");
            var asoSampleDataFile = await server.GetFileAsync(@"/gallery/Applications/Demo Samples/Aggregate Storage/ASO_Sample_Data.txt");

            // Create the application and get the Basic cube.
            var asoSample = await server.CreateApplicationFromWorkbookAsync("ASOSamp", "Basic", new EssJobImportExcelOptions(asoSampleWorkbook, buildOption: EssBuildOption.NONE, loadData: false))
                .GetCubeAsync("Basic");

            // Load the data into the Basic cube.
            await asoSample.LoadDataToCubeAsync(new EssJobLoadDataOptions(asoSampleDataFile));

            // Get the list of applications.
            applications = await server.GetApplicationsAsync();

            // Assert that server now contains 3 applications (Sample, Demo, and ASOSamp).
            Assert.Equal(3, applications?.Count);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 02 - Essbase_AfterCubeCreation_CanCreateMdxScript"), Priority(02)]
        public async Task Essbase_AfterCubeCreation_CanCreateMdxScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Assert that the cube does not contain an MDX script called "test" before we create one.
            Assert.Empty((await cube.GetScriptsAsync<IEssMdxScript>()).Where(mdx => string.Equals(mdx?.Name, "test", StringComparison.Ordinal)));

            // Create some script content.
            var content = @"SELECT {[Market]} ON COLUMNS, {[YEAR]} ON ROWS";

            // Create a script on the server with the given content.
            await cube.CreateScriptAsync<IEssMdxScript>("test", content);

            // Get the script back from the server.
            var script = await cube.GetScriptAsync<IEssMdxScript>("test", getContent: true);

            // Assert that the script exists and contains the content we gave it.
            Assert.Equal(content, script?.Content);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 03 - Essbase_AfterCubeCreation_CanCreateReportScript"), Priority(03)]
        public async Task Essbase_AfterCubeCreation_CanCreateReportScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Assert that the cube does not contain an report script called "test" before we create one.
            Assert.Empty((await cube.GetScriptsAsync<IEssReportScript>()).Where(report => string.Equals(report?.Name, "test", StringComparison.Ordinal)));

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
            await cube.CreateScriptAsync<IEssReportScript>("test", content);

            // Get the script back from the server.
            var script = await cube.GetScriptAsync<IEssReportScript>("test", getContent: true);

            // Assert that the script exists and contains the content we gave it.
            Assert.Equal(content, script?.Content);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 04 - Essbase_AfterCubeCreation_CanCreateMaxLScript"), Priority(04)]
        public async Task Essbase_AfterCubeCreation_CanCreateMaxLScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Assert that the cube does not contain an MaxL script called "test" before we create one.
            Assert.Empty((await cube.GetScriptsAsync<IEssMaxlScript>()).Where(maxl => string.Equals(maxl?.Name, "test", StringComparison.Ordinal)));

            // Create some script content.
            var content = @"query database sample.basic get dbstats dimension;";

            // Create a script on the server with the given content.
            await cube.CreateScriptAsync<IEssMaxlScript>("test", content);

            // Get the script back from the server.
            var script = await cube.GetScriptAsync<IEssMaxlScript>("test", getContent: true);

            // Assert that the script exists and contains the content we gave it.
            Assert.Equal(content, script?.Content);
        }

        [Fact(DisplayName = @"CreateServerObjectTests - 05 - Essbase_AfterScriptCreation_CanCreateLockOnScript"), Priority(05)]
        public async Task Essbase_AfterScriptCreation_CanCreateLockOnScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the "CalcAll" script from Sample.Basic.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssCalcScript>("CalcAll");

            // Create lock options object.
            var options = new EssLockOptions(script.Name, EssLockedFileType.CALCSCRIPT);

            // Lock Script on server.
            var lockedScript = await script.Cube.CreateLockObjectAsync(options);

            // Assert that the lock object name is the same as the one we passed.
            Assert.Equal("CalcAll", lockedScript.Name);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 06 - Essbase_AfterClean_CanCreateUser"), Priority(06)]
        public async Task Essbase_AfterClean_CanCreateUser()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var userConnection = GetEssConnection(EssUserRole.User);

            // Create EssUserCreationOptions
            var options = new EssUserCreationOptions(userConnection.Username, userConnection.Password, userConnection.Role);

            var newUser = await server.CreateUserAsync(options);

            Assert.Equal(userConnection.Username, newUser.Name);

            Assert.Equal( userConnection.Role, newUser.Role);
        }

        [Fact(DisplayName = "CreateServerObjectTests - 07 - Essbase_AfterClean_CanGivePermissions"), Priority(07)]
        public async Task Essbase_AfterClean_CanGivePermissions()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var userConnection = GetEssConnection(EssUserRole.User);

            var app = await server.GetApplicationAsync("Sample");

            var userPermissions = await app.CreateUserPermissionsAsync(userConnection.Username, EssUserPermissionRole.db_update);

            Assert.Equal(EssUserPermissionRole.db_update, userPermissions.Role);

            Assert.Equal(userConnection.Username, userPermissions.Name);
        }
    }
}
