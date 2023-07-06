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
            var server = new EssServerFactory().CreateEssServer(server: @"http://localhost:9000/essbase", username: "admin", password: "password1", connect: false);

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
            var server = new EssServerFactory().CreateEssServer(server: @"http://localhost:9000/essbase", username: "admin", password: "password1", connect: false);

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

        [Fact(DisplayName = $@"CreateServerObjectTests - 03 - Essbase_AfterScriptCreation_CanExecuteMdxScript"), Priority(03)]
        public async Task Essbase_AfterScriptCreation_CanExecuteMdxScript()
        {
            // Get an unconnected server.
            var server = new EssServerFactory().CreateEssServer(server: @"http://localhost:9000/essbase", username: "admin", password: "password1", connect: false);

            // Get the test mdx script from the server.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssMdxScript>("test");

            // Execute the mdx job and capture the results.
            var job = await script.ExecuteAsync();

            // Assert that the run mdx job completed without warnings.
            Assert.Equal(EssJobStatus.Completed, job?.JobStatus);
        }

        [Fact(DisplayName = $@"GetServerObjectTests - 01 - EssBase_AfterLockCreation_CanGetLockedObjects"), Priority(04)]
        public async Task EssBase_AfterLockCreation_CanGetLockedObjects()
        {
            // Get an unconnected server.
            var server = new EssServerFactory().CreateEssServer(server: @"http://localhost:9000/essbase", username: "admin", password: "password1", connect: false);

            // Get the cube from the server.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Create lock options object
            var body = new EssLockOptions("CalcAll", EssLockedFileType.CALCSCRIPT);

            // Lock Script on server
            var lockedScript = await cube.CreateLockObjectAsync(body).ConfigureAwait(false);

            // Assert that the lock object name is the same as the one we passed. --Make better 
            Assert.Equal("CalcAll", lockedScript.Name);
        }


    }
}
