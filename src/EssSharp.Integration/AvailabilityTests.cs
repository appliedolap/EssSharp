using System;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;


namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), CollectionPriority(1)]
    public class AvailabilityTests : IntegrationTestBase
    {
        public AvailabilityTests() : base() { }

        [Fact(DisplayName = $@"01 - {nameof(DatabaseContainer_AfterInitialization_IsNotNull)}"), Priority(01)]
        public void DatabaseContainer_AfterInitialization_IsNotNull()
        {
            // Assert.
            Assert.NotNull(Database);
        }

        [Fact(DisplayName = $@"02 - {nameof(DatabaseContainer_AfterInitialization_IsRunning)}"), Priority(02)]
        public async Task DatabaseContainer_AfterInitialization_IsRunning()
        {
            // Get a docker client.
            using var client = GetClient();

            // Get the state of the database container.
            var state = (await client.Containers.InspectContainerAsync(Database))?.State;

            // Assert
            Assert.True(state?.Running);
        }

        [Fact(DisplayName = $@"03 - {nameof(EssbaseContainer_AfterInitialization_IsNotNull)}"), Priority(03)]
        public void EssbaseContainer_AfterInitialization_IsNotNull()
        {
            // Assert.
            Assert.NotNull(Essbase);
        }

        [Fact(DisplayName = $@"04 - {nameof(EssbaseContainer_AfterInitialization_IsRunning)}"), Priority(04)]
        public async Task EssbaseContainer_AfterInitialization_IsRunning()
        {
            // Get a docker client.
            using var client = GetClient();

            // Get the state of the essbase container.
            var state = (await client.Containers.InspectContainerAsync(Essbase))?.State;

            // Assert
            Assert.True(state?.Running);
        }

        [Fact(DisplayName = $@"05 - {nameof(EssbaseContainer_AfterInitialization_IsRestApiReady)}"), Priority(05)]
        public async Task EssbaseContainer_AfterInitialization_IsRestApiReady()
        {
            // Get a docker client.
            using var client = GetClient();

            // Poll for the REST API and capture a status code for an unauthorized request.
            var statusCode = await PollForRestfulApiAndReturnStatusCodeAsync();

            // Assert that the REST API is available and returns a 401 status code (for unauthorized requests).
            Assert.Equal(401, statusCode);

            // Local polling function.
            async Task<int> PollForRestfulApiAndReturnStatusCodeAsync()
            {
                // Wait up to 20 minutes for the REST API to become available.
                for ( int i = 0; i < (TimeSpan.FromMinutes(20).TotalSeconds / 5); i++ )
                {
                    // Poll the /rest/v1/ endpoint for the availability of the REST API.
                    var (details, stdout) = await ExecAsync(Essbase, new[] { @"curl", "--output", "/dev/null", "--silent", "--head", "--fail", "--write-out", "%{http_code}", "http://localhost:9000/essbase/rest/v1/" });

                    // Exit code 22 means that the server is available and returning a 40x status code.
                    if ( details?.ExitCode is 22 )
                    {
                        if ( int.TryParse(stdout?.Trim(), out var statusCode) )
                            return statusCode;
                    }

                    // Wait 5 seconds.
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }

                return 0;
            }
        }

        [Fact(DisplayName = $@"06 - {nameof(EssbaseContainer_AfterInitialization_CanConnect)}"), Priority(06)]
        public async Task EssbaseContainer_AfterInitialization_CanConnect()
        {
            // Get a connected IEssServer.
            var server = await new EssServerFactory().CreateEssServerAsync(server: @"http://localhost:9000/essbase", username: "admin", password: "password1", connect: true);

            // Assert that a connected server is returned.
            Assert.NotEqual(default, server);
        }

        [Fact(DisplayName = $@"07 - {nameof(EssbaseContainer_AfterConnection_CanRemoveApplications)}"), Priority(07)]
        public async Task EssbaseContainer_AfterConnection_CanRemoveApplications()
        {
            var server = new EssServerFactory().CreateEssServer(server: @"http://localhost:9000/essbase", username: "admin", password: "password1", connect: false);

            // Get the list of existing applications.
            foreach ( var application in await server.GetApplicationsAsync() )
            {
                // Unlock any locked objects on any of the cubes (that may prevent the application from being deleted).
                foreach ( var cube in await application.GetCubesAsync() )
                    foreach ( var lockedObject in await cube.GetLockedObjectsAsync() )
                        lockedObject.ToString(); // await lockedObject.DeleteAsync();

                // Delete the application.
                await application.DeleteAsync();
            }

            // Get the list of applications.
            var applications = await server.GetApplicationsAsync();

            // Assert that the returned collection of applications is empty.
            Assert.Empty(applications);
        }

        [Fact(DisplayName = $@"08 - {nameof(EssbaseContainer_AfterReset_CanCreateSampleApplications)}"), Priority(08)]
        public async Task EssbaseContainer_AfterReset_CanCreateSampleApplications()
        {
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

        [Fact(DisplayName = $@"09 - {nameof(EssbaseContainer_AfterCubeCreation_CanRemoveScripts)}"), Priority(09)]
        public async Task EssbaseContainer_AfterCubeCreation_CanRemoveScripts()
        {
            var server = new EssServerFactory().CreateEssServer(server: @"http://localhost:9000/essbase", username: "admin", password: "password1", connect: false);

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

                    // Assert that the returned collection of calc scripts is empty.
                    Assert.Empty(await cube.GetScriptsAsync<IEssCalcScript>());

                    // Get and delete all existing maxl scripts.
                    foreach ( var script in await cube.GetScriptsAsync<IEssMaxlScript>() )
                        await script.DeleteAsync();

                    // Assert that the returned collection of maxl scripts is empty.
                    Assert.Empty(await cube.GetScriptsAsync<IEssMaxlScript>());

                    // Get and delete all existing mdx scripts.
                    foreach ( var script in await cube.GetScriptsAsync<IEssMdxScript>() )
                        await script.DeleteAsync();

                    // Assert that the returned collection of mdx scripts is empty.
                    Assert.Empty(await cube.GetScriptsAsync<IEssMdxScript>());

                    // Get and delete all existing report scripts.
                    foreach ( var script in await cube.GetScriptsAsync<IEssReportScript>() )
                        await script.DeleteAsync();

                    // Assert that the returned collection of report scripts is empty.
                    Assert.Empty(await cube.GetScriptsAsync<IEssReportScript>());
                }
            }
        }

        [Fact(DisplayName = "10 - EssbaseContainer_AfterCubeCreation_CanCreateMdxScript"), Priority(10)]
        public async Task EssbaseContainer_AfterCubeCreation_CanCreateMdxScript()
        {
            var server = new EssServerFactory().CreateEssServer(server: @"http://localhost:9000/essbase", username: "admin", password: "password1", connect: false);

            // Get the Sample.Basic cube.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Create some script content.
            var content = @"SELECT {[Market]} ON COLUMNS, {[YEAR]} ON ROWS";

            // Create a script on the server with the given content.
            await cube.CreateScriptAsync<IEssMdxScript>("test", content);

            // Get the script back from the server.
            var script = await cube.GetScriptAsync<IEssMdxScript>("test", getContent: true);

            // Assert that the script is available and contains the content we gave it.
            Assert.Equal(content, script?.Content);
        }

        [Fact(DisplayName = $@"11 - {nameof(EssbaseContainer_AfterScriptCreation_CanExecuteMdxScript)}"), Priority(11)]
        public async Task EssbaseContainer_AfterScriptCreation_CanExecuteMdxScript()
        {
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
        
    }
}
