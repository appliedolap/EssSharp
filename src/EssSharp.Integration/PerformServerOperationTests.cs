using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), Trait("type", "execute"), CollectionPriority(6)]
    public class PerformServerFunctionTests : IntegrationTestBase
    {
        [Fact(DisplayName = @"PerformServerFunctionTests - 01 - Essbase_AfterScriptCreation_CanExecuteMdxScript"), Priority(01)]
        public async Task Essbase_AfterScriptCreation_CanExecuteMdxScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the test mdx script from the server.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssMdxScript>("test");

            // Execute the mdx job and capture the results.
            var job = await script.ExecuteAsync();

            // Assert that the run mdx job completed without warnings.
            Assert.Equal(EssJobStatus.Completed, job?.JobStatus);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 02 - Essbase_AfterScriptCreation_CanGetMdxQueryReport"), Priority(02)]
        public async Task Essbase_AfterScriptCreation_CanGetMdxQueryReport()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the test mdx script from the server.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssMdxScript>("test");

            // Execute the mdx query and capture the report.
            var report = await script.GetReportAsync();

            // Assert that the "Market" dimension is the first column dimension member.
            Assert.Equal("Market", report.Metadata.ColumnDimensionMembers.FirstOrDefault());
            // Assert that the "Year" dimensions is the first row dimension member.
            Assert.Equal("Year", report.Metadata.RowDimensionMembers.FirstOrDefault());
            // Assert that the data cell at row 3, column 2 equals "105522.0".
            Assert.Equal("105522.0", report.Data[2,1]);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 03 - Essbase_AfterScriptCreation_CanGetMdxGrid"), Priority(03)]
        public async Task Essbase_AfterScriptCreation_CanGetMdxGrid()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the test mdx script from the server.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssMdxScript>("test");

            // Execute the mdx query and capture an Essbase grid.
            var grid = await script.GetGridAsync();

            // Assert that the "Market" dimension is the first column dimension member.
            Assert.Equal("Market", grid.Dimensions
                .Where  (d => d.Column is -1 && d.Row >= 0)
                .OrderBy(d => d.Row)
                .FirstOrDefault()
                .Name);

            // Assert that the "Year" dimensions is the first row dimension member.
            Assert.Equal("Year", grid.Dimensions
                .Where  (d => d.Row is -1 && d.Column >= 0)
                .OrderBy(d => d.Column)
                .FirstOrDefault()
                .Name);

            // Assert that the last data cell equals "105522.0".
            Assert.Equal("105522.0", grid.Slice.Data.Ranges.FirstOrDefault().Values.LastOrDefault());
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 04 - Essbase_AfterScriptCreation_CanExecuteReportScript"), Priority(04)]
        public async Task Essbase_AfterScriptCreation_CanExecuteReportScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the test Report script from the server.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssReportScript>("test");

            // Execute the Report job and capture the results.
            var job = await script.ExecuteAsync();

            // Assert that the run Report job completed without warnings.
            Assert.Equal(EssJobStatus.Completed, job?.JobStatus);
        }
    }


}
