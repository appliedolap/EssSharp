using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;
using EssSharp.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Xunit;
using Xunit.Abstractions;

namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), Trait("type", "execute"), CollectionPriority(6)]
    public class PerformServerFunctionTests : IntegrationTestBase
    {
        /// <summary />
        /// <param name="output" />
        public PerformServerFunctionTests( ITestOutputHelper output ) : base(output) { }

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
            Assert.Equal("105522.0", report.Data[2, 1]);
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
                .Where(d => d.Column is -1 && d.Row >= 0)
                .OrderBy(d => d.Row)
                .FirstOrDefault()
                .Name);

            // Assert that the "Year" dimensions is the first row dimension member.
            Assert.Equal("Year", grid.Dimensions
                .Where(d => d.Row is -1 && d.Column >= 0)
                .OrderBy(d => d.Column)
                .FirstOrDefault()
                .Name);

            // Assert that the last data cell equals "105522.0".
            Assert.Equal("105522.0", grid.Slice.Data.Ranges.LastOrDefault().Values.LastOrDefault());
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 04 - Essbase_AfterScriptCreation_CanRenameMdxScript"), Priority(04)]
        public async Task Essbase_AfterScriptCreation_CanRenameAndCopyMdxScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Get the "test" mdx script from the cube and capture its content.
            var script = await cube.GetScriptAsync<IEssMdxScript>("test", getContent: true);
            var content = script.Content;

            // Rename the "test" mdx script as "test2".
            await script.RenameAsync("test2");

            // Get all of the mdx scripts back from the cube.
            var scripts = await cube.GetScriptsAsync<IEssMdxScript>(getContent: true);

            // Assert that only a single mdx script remains on the server.
            Assert.Single(scripts);

            // Capture the renamed "test2" mdx script.
            var renamedScript = scripts.First();

            // Assert that the mdx script is named "test2".
            Assert.Equal("test2", renamedScript.Name);

            // Assert that the content of "test2" matches the original "test" mdx script.
            Assert.Equal(content, renamedScript.Content);

            // Copy the renamed script back to "test" mdx.
            await renamedScript.CopyAsync<IEssMdxScript>("test");

            // Get all of the mdx scripts back from the cube.
            scripts = await cube.GetScriptsAsync<IEssMdxScript>(getContent: true);

            // Assert that the cube now has 2 mdx scripts.
            Assert.Equal(2, scripts.Count);

            // Assert that both "test" and "test2" are now present on the cube.
            Assert.True(scripts.Any(s => string.Equals("test", s.Name)) && scripts.Any(s => string.Equals("test2", s.Name)));

            // Assert that both "test" and "test2" contain the original content.
            Assert.True(scripts.All(s => string.Equals(content, s.Content)));
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 05 - Essbase_AfterScriptRename_CanUpdateMdxScript"), Priority(05)]
        public async Task Essbase_AfterScriptCreation_CanUpdateMdxScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Create some updated script content.
            var content = @"SELECT {([Market], [Product])} ON COLUMNS, {[YEAR]} ON ROWS";

            // Get the "test2" mdx script from the cube and update the content.
            var script = await cube.GetScriptAsync<IEssMdxScript>("test2", getContent: true);
            script.Content = content;

            // Save the "test2" mdx script.
            await script.SaveAsync();

            // Get the updated script back from the cube.
            script = await cube.GetScriptAsync<IEssMdxScript>("test2", getContent: true);

            // Assert that the updated script exists and contains the updated content we saved to it.
            Assert.Equal(content, script?.Content);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 06 - Essbase_AfterScriptCreation_CanExecuteReportScript"), Priority(06)]
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

        [Fact(DisplayName = @"PerformServerFunctionTests - 07 - Essbase_AfterScriptCreation_CanGetReportQueryReport"), Priority(07)]
        public async Task Essbase_AfterScriptCreation_CanGetReportQueryReport()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the test Report script from the server.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssReportScript>("test");

            // Execute the report job and capture the results.
            var report = await script.GetReportAsync();

            // Assert that there is a single page dimension member.
            Assert.Single(report.Metadata.PageDimensionMembers);
            // Assert that the "Measures" dimension is the first page dimension member.
            Assert.Equal("Measures", report.Metadata.PageDimensionMembers.First());

            // Assert that there are 2 column dimension members.
            Assert.Equal(2, report.Metadata.ColumnDimensionMembers.Count);
            // Assert that the "Scenario" and "Year" dimension members are present.
            Assert.True(report.Metadata.ColumnDimensionMembers.Any(cdm => string.Equals(cdm, "Scenario")) && report.Metadata.ColumnDimensionMembers.Any(cdm => string.Equals(cdm, "Year")));

            // Assert that there are 2 row dimension members.
            Assert.Equal(2, report.Metadata.RowDimensionMembers.Count);
            // Assert that the "Market" and "Product" dimension members are present.
            Assert.True(report.Metadata.RowDimensionMembers.Any(rdm => string.Equals(rdm, "Market")) && report.Metadata.RowDimensionMembers.Any(rdm => string.Equals(rdm, "Product")));

            // Assert that the data cell at row 3, column 3 equals "2,479".
            Assert.Equal("2,479", report.Data[2, 2]);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 08 - Essbase_AfterScriptCreation_CanGetReportQueryReportAsUser"), Priority(08)]
        public async Task Essbase_AfterScriptCreation_CanGetReportQueryReportAsUser()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer(EssServerRole.User);

            // Get the test Report script from the server.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssReportScript>("test");

            // Execute the report job and capture the results.
            var report = await script.GetReportAsync();

            // Assert that there is a single page dimension member.
            Assert.Single(report.Metadata.PageDimensionMembers);
            // Assert that the "Measures" dimension is the first page dimension member.
            Assert.Equal("Measures", report.Metadata.PageDimensionMembers.First());

            // Assert that there are 2 column dimension members.
            Assert.Equal(2, report.Metadata.ColumnDimensionMembers.Count);
            // Assert that the "Scenario" and "Year" dimension members are present.
            Assert.True(report.Metadata.ColumnDimensionMembers.Any(cdm => string.Equals(cdm, "Scenario")) && report.Metadata.ColumnDimensionMembers.Any(cdm => string.Equals(cdm, "Year")));

            // Assert that there are 2 row dimension members.
            Assert.Equal(2, report.Metadata.RowDimensionMembers.Count);
            // Assert that the "Market" and "Product" dimension members are present.
            Assert.True(report.Metadata.RowDimensionMembers.Any(rdm => string.Equals(rdm, "Market")) && report.Metadata.RowDimensionMembers.Any(rdm => string.Equals(rdm, "Product")));

            // Assert that the data cell at row 3, column 3 equals "2,479".
            Assert.Equal("2,479", report.Data[2, 2]);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 09 - Essbase_AfterScriptCreation_CanGetReportGrid"), Priority(09)]
        public async Task Essbase_AfterScriptCreation_CanGetReportGrid()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the test Report script from the server.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssReportScript>("test");

            // Execute the report query and capture an Essbase grid.
            var grid = await script.GetGridAsync();

            // Assert that we got a grid back.
            Assert.NotNull(grid);

            // Assert that the "Year" dimension is the first column dimension member.
            // Note: The grid operation moves Scenario to the page axis.
            Assert.Equal("Year", grid.Dimensions
                .Where(d => d.Column is -1 && d.Row >= 0)
                .OrderBy(d => d.Row)
                .FirstOrDefault()
                .Name);

            // Assert that the "Market" dimensions is the first row dimension member.
            Assert.Equal("Market", grid.Dimensions
                .Where(d => d.Row is -1 && d.Column >= 0)
                .OrderBy(d => d.Column)
                .FirstOrDefault()
                .Name);

            // Assert that the member cell at row 2, column 3 equals "Jan".
            Assert.Equal("Jan", grid.Slice.Data.Ranges.LastOrDefault().Values[grid.Slice.Columns + 2]);

            // Assert that the last data cell equals "712.0".
            Assert.Equal("712.0", grid.Slice.Data.Ranges.LastOrDefault().Values.LastOrDefault());
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 10 - Essbase_AfterScriptCreation_CannotExecuteMaxLScript"), Priority(10)]
        public async Task Essbase_AfterScriptCreation_CannotExecuteMaxLScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the test MaxL script from the server.
            var script = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetScriptAsync<IEssMaxlScript>("test");

            // Assert that an Exception is thrown when we try to execute a MaxL script,
            // and capture the base exception, since this is not supported by the server.
            var exception = (await Assert.ThrowsAsync<Exception>(async () => await script.ExecuteAsync())).GetBaseException();

            // Assert that the base exception is a WebException with a WebExceptionRestResponse with status code 400 (bad request).
            Assert.True(exception is WebException { Response: EssSharp.Api.WebExceptionRestResponse { StatusCode: HttpStatusCode.BadRequest } });
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 11 - Essbase_AfterPermissionCreation_CanGetListOfUserPermissions"), Priority(11)]
        public async Task Essbase_AfterPermissionCreation_CanGetListOfUserPermissions()
        {
            var server = GetEssServer();

            var app = await server.GetApplicationAsync("Sample");

            var permissionsList = await app.GetPermissionsAsync(EssPermissionType.User, new[] { EssApplicationRole.db_access });

            Assert.NotEmpty(permissionsList);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 12 - Essbase_AfterPermissionCreation_CanGetListOfGroupPermissions"), Priority(12)]
        public async Task Essbase_AfterPermissionCreation_CanGetListOfPermissions()
        {
            var server = GetEssServer();

            var app = await server.GetApplicationAsync("Sample");

            var permissionsList = await app.GetPermissionsAsync(EssPermissionType.User, new[] { EssApplicationRole.db_access });

            Assert.NotEmpty(permissionsList);
        }


        [Fact(DisplayName = @"PerformServerFunctionTests - 13 - Essbase_AfterPermissionCreation_CanUpdateUserPermissions"), Priority(13)]
        public async Task Essbase_AfterPermissionCreation_CanUpdateUserPermissions()
        {
            var server = GetEssServer();

            var app = await server.GetApplicationAsync("Sample");

            var user = GetEssConnection(EssServerRole.User);

            var userPermissions = await app.UpdatePermissionsAsync(user.Username, EssApplicationRole.db_access);

            Assert.Equal(EssApplicationRole.db_access, userPermissions.Role);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 14 - Essbase_AfterGroupCreation_CanAddUser"), Priority(14)]
        public async Task Essbase_AfterGroupCreation_CanAddUser()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var userConnection = GetEssConnection(EssServerRole.User);

            var group = await server.GetGroupAsync("Test_Group");

            Assert.Empty(await group.GetUsersAsync());

            await group.AddUsersAsync(new List<string>() { userConnection.Username });

            Assert.NotEmpty(await group.GetUsersAsync());
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 15 - Essbase_AfterGroupCreation_CanAddGroup"), Priority(15)]
        public async Task Essbase_AfterGroupCreation_CanAddGroup()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var group = await server.GetGroupAsync("Test_Group");

            Assert.Empty(await group.GetGroupsAsync());

            await group.AddGroupsAsync(new List<string>() { "Test_Group_2" });

            Assert.NotEmpty(await group.GetGroupsAsync());
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 16 - Essbase_AfterGroupCreation_EditGroup"), Priority(16)]
        public async Task Essbase_AfterGroupCreation_EditGroup()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var group = await server.GetGroupAsync("Test_Group");

            var editedGroup = await group.EditAsync( EssServerRole.User, "Edited test group");

            Assert.Equal(EssServerRole.User, editedGroup.Role);
            Assert.Equal("Edited test group", editedGroup.Description);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 17 - Essbase_AfterGroupCreation_CanRemoveUser"), Priority(17)]
        public async Task Essbase_AfterGroupCreation_CanRemoveUser()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var userConnection = GetEssConnection(EssServerRole.User);

            var group = await server.GetGroupAsync("Test_Group");

            Assert.NotEmpty(await group.GetUsersAsync());

            await group.RemoveUsersAsync(new List<string>() { userConnection.Username });

            Assert.Empty(await group.GetUsersAsync());
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 18 - Essbase_AfterGroupCreation_CanRemoveGroup"), Priority(18)]
        public async Task Essbase_AfterGroupCreation_CanRemoveGroup()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var group = await server.GetGroupAsync("Test_Group");

            Assert.NotEmpty(await group.GetGroupsAsync());

            await group.RemoveGroupsAsync(new List<string>() { "Test_Group_2" });

            Assert.Empty(await group.GetGroupsAsync());
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 19 - Essbase_AfterReportCreation_CanExecuteDrillthroughReport"), Priority(19)]
        public async Task Essbase_AfterReportCreation_CanExecuteDrillthroughReport()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Declare a drillthrough report.
            var drillthroughReport = default(IEssDrillthroughReport);

            // Find the "drillthrough_samplebasic" report (if available).
            foreach ( var dtr in await cube.GetDrillthroughReportsAsync(false) )
                if ( string.Equals(dtr.Name, "drillthrough_samplebasic", StringComparison.Ordinal) )
                    drillthroughReport = dtr;

            // If the report is not available return.
            if ( drillthroughReport is null )
                return;

            // Capture the (x.x) server version.
            var version = new Version(string.Join('.', (await server.GetAboutAsync()).Version.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Take(2)));

            // If the server version is 21.4 or higher, execute the drillthrough report and validate the data.
            if ( version.CompareTo(new Version(major: 21, minor: 4)) >= 0 )
            {
                // Execute the report for the given drillthrough range.
                var (report, columnTypes) = await drillthroughReport
                    .ExecuteAsync(new EssDrillthroughRange(dimensionMemberSets: new()
                    {
                        ["Year"] = new() { "Year", "Year" },
                        ["Product"] = new() { "Cola", "Cola" },
                        ["Measures"] = new() { "Sales", "Sales" },
                        ["Market"] = new() { "New York", "California" },
                        ["Scenario"] = new() { "Actual", "Actual" }
                    }), new EssDrillthroughOptions(returnTypedValues: true, prefixStringValuesForExcel: true));

                // Assert the column type, header name, and first row value for the fifth column.
                Assert.Equal(expected: "double", actual: columnTypes[4], ignoreCase: true);
                Assert.Equal(expected: "amount", actual: (string)report[0, 4], ignoreCase: true);
                Assert.Equal(expected: 501.72, actual: (double)report[1, 4]);
            }
            // If the server version is 21.3 or lower, assert that a 405 (method not allowed) exception is thrown.
            else
            {
                // Assert that a NotSupportedException is thrown when we try to execute the drillthrough report,
                // and capture the inner exception to verify that this method is not allowed by the server.
                var exception = (await Assert.ThrowsAsync<NotSupportedException>(async () => await drillthroughReport
                    .ExecuteAsync(new EssDrillthroughRange(dimensionMemberSets: new()
                    {
                        ["Year"    ] = new() { "Year",     "Year"       },
                        ["Product" ] = new() { "Cola",     "Cola"       },
                        ["Measures"] = new() { "Sales",    "Sales"      },
                        ["Market"  ] = new() { "New York", "California" },
                        ["Scenario"] = new() { "Actual",   "Actual"     }
                    }), new EssDrillthroughOptions(returnTypedValues: true, prefixStringValuesForExcel: true))
                )).InnerException;

                // Assert that the base exception is a WebException with a WebExceptionRestResponse with status code 405 (method not allowed).
                Assert.True(exception is WebException { Response: EssSharp.Api.WebExceptionRestResponse { StatusCode: HttpStatusCode.MethodNotAllowed } });
            }
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 20 - Essbase_AfterDefaultGrid_CanRefreshGrid"), Priority(20)]
        public async Task Essbase_AfterDefaultGrid_CanRefreshGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            var refreshGrid = await defaultGrid.RefreshAsync();

            Assert.Equal(3, refreshGrid.Slice.Rows);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 21 - Essbase_AfterDefaultGrid_CanZoomInGrid"), Priority(21)]
        public async Task Essbase_AfterDefaultGrid_CanZoomInGrid()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            defaultGrid.Preferences = await server.GetDefaultGridPreferencesAsync();
            {
                defaultGrid.Preferences.ZoomIn.Ancestor = ZoomInAncestor.BOTTOM;
                defaultGrid.Preferences.ZoomIn.Mode = ZoomInMode.BASE;
            }

            var zoomInGrid = await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            Assert.Equal(15, zoomInGrid.Slice.Rows);

            Assert.True(string.Equals("8346.0", zoomInGrid.Slice.Data.Ranges[0].Values[13]));
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 22 - Essbase_AfterDefaultGrid_CanZoomOutGrid"), Priority(22)]
        public async Task Essbase_AfterDefaultGrid_CanZoomOutGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync(reset : true);

            var zoomInGrid = await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            var zoomOutGrid = await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMOUT, new EssGridSelection(6, 0));

            Assert.Equal(3, zoomOutGrid.Slice.Rows);

            Assert.True(string.Equals("105522.0", zoomOutGrid.Slice.Data.Ranges[0].Values[9]));
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 23 - Essbase_AfterDefaultGrid_CanKeepOnlyGrid"), Priority(23)]
        public async Task Essbase_AfterDefaultGrid_CanKeepOnlyGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync(reset : true);

            var zoomInGrid = await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            var keepOnlyGrid = await defaultGrid.KeepOnlyAsync( new EssGridSelection(3, 0));

            Assert.Equal(3, keepOnlyGrid.Slice.Rows);

            Assert.True(string.Equals("27107.0", keepOnlyGrid.Slice.Data.Ranges[0].Values[9]));
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 24 - Essbase_AfterDefaultGrid_CanRemoveOnlyGrid"), Priority(24)]
        public async Task Essbase_AfterDefaultGrid_CanRemoveOnlyGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync(reset : true);

            var zoomInGrid = await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            var removeOnlyGrid = await defaultGrid.RemoveOnlyAsync( new EssGridSelection(2, 0));

            Assert.Equal(6, removeOnlyGrid.Slice.Rows);

            Assert.True(!string.Equals("     Qtr1", removeOnlyGrid.Slice.Data.Ranges[0].Values[8]));
        }


        [Fact(DisplayName = @"PerformServerFunctionTests - 25 - Essbase_AfterDefaultGrid_CanPivotToPovWithSelectionAttributeGrid"), Priority(25)]
        public async Task Essbase_AfterDefaultGrid_CanPivotToPovWithSelectionAttributeGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            defaultGrid.Selection.Add(new EssGridSelection(startRow: 0, startColumn: 3));

            var pivotPovGrid = defaultGrid.Pivot( /*newPosition: new EssGridSelection(startRow: 0, startColumn: 5)*/);

            Assert.Equal(3, pivotPovGrid.Slice.Rows);

            Assert.True(string.Equals("Scenario", pivotPovGrid.Slice.Data.Ranges[0].Values[8]));

            defaultGrid.Selection[0].startRow = 2;
            defaultGrid.Selection[0].startColumn = 0;

            pivotPovGrid = defaultGrid.Pivot();

            Assert.Equal(4, pivotPovGrid.Slice.Rows);

            Assert.True(!string.Equals("Scenario", pivotPovGrid.Slice.Data.Ranges[0].Values[8]));
        }


        [Fact(DisplayName = @"PerformServerFunctionTests - 26 - Essbase_AfterDefaultGrid_CanPivotToPovGrid"), Priority(26)]
        public async Task Essbase_AfterDefaultGrid_CanPivotToPovGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            // Year > Qtr1 > Jan
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            // Product > Colas > Cola
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(0, 1));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            // Market > East > New York 
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(0, 2));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            // Scenario > Actual
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(0, 3));

            // Measures > Profit > Margin > Sales
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(1, 3));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(1, 3));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(1, 3));

            defaultGrid.Selection = new List<EssGridSelection>() { new EssGridSelection(0, 0, 3, 4) };
            await defaultGrid.KeepOnlyAsync();

            defaultGrid.Selection.Clear();
            defaultGrid.Selection.Add(new EssGridSelection(2, 0));
            defaultGrid.Selection.Add(new EssGridSelection(0, 2));

            var pivotPovGrid = await defaultGrid.PivotAsync();

            Assert.Equal(4, pivotPovGrid.Slice.Rows);

            Assert.True(string.Equals("New York", pivotPovGrid.Slice.Data.Ranges[0].Values[5]));

            defaultGrid.Selection.Clear();

            pivotPovGrid = await defaultGrid.PivotAsync(new EssGridSelection(1, 2));

            Assert.Equal(3, pivotPovGrid.Slice.Rows);

            Assert.True(string.Equals("New York", pivotPovGrid.Slice.Data.Ranges[0].Values[8]));
        }


        [Fact(DisplayName = @"PerformServerFunctionTests - 27 - Essbase_AfterDefaultGrid_CanSubmitNewValueGrid"), Priority(27)]
        public async Task Essbase_AfterDefaultGrid_CanSubmitNewValueGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            // Year > Qtr1 > Jan
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            // Product > Colas > Cola
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(0, 1));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            // Market > East > New York 
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(0, 2));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            // Scenario > Actual
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(0, 3));

            // Measures > Profit > Margin > Sales
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(1, 3));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(1, 3));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(1, 3));

            defaultGrid.Selection = new List<EssGridSelection>() { new EssGridSelection(0, 0, 3, 4) };
            await defaultGrid.KeepOnlyAsync();

            if ( string.Equals(defaultGrid.Slice.Data.Ranges[0].Values[10], "680.0") )
                defaultGrid.Slice.Data.Ranges[0].Values[11] = "678.0";
            else
                defaultGrid.Slice.Data.Ranges[0].Values[11] = "680.0";

            var submitGrid = await defaultGrid.SubmitAsync( );

            (await cube.GetScriptAsync<IEssCalcScript>("CalcAll").ConfigureAwait(false)).Execute();

            await submitGrid.RefreshAsync();

            Assert.Equal(3, submitGrid.Slice.Rows);

            Assert.True(string.Equals("680.0", submitGrid.Slice.Data.Ranges[0].Values[11]) || string.Equals("680.0", submitGrid.Slice.Data.Ranges[0].Values[11]));

            /*
            if ( string.Equals(defaultGrid.Slice.Data.Ranges[0].Values[10], "680.0") )
                defaultGrid.Slice.Data.Ranges[0].Values[11] = "678.0";
            else
                defaultGrid.Slice.Data.Ranges[0].Values[11] = "680.0";

            submitGrid = await defaultGrid.SubmitAsync();

            await (await cube.GetScriptAsync<IEssCalcScript>("CalcAll").ConfigureAwait(false)).ExecuteAsync();
            */
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 28 - Essbase_AfterDefaultGrid_CanZoomInWithSelectionAttributeGrid"), Priority(28)]
        public async Task Essbase_AfterDefaultGrid_CanZoomInWithSelectionAttributeGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            defaultGrid.Selection.Add(new EssGridSelection(2, 0));

            var zoomInGrid = await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMIN );

            Assert.Equal(7, zoomInGrid.Slice.Rows);

            Assert.True(string.Equals("24703.0", zoomInGrid.Slice.Data.Ranges[0].Values[9]));
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 29 - Essbase_AfterDefaultGrid_CanZoomOutWithSelectionAttributeGrid"), Priority(29)]
        public async Task Essbase_AfterDefaultGrid_CanZoomOutWithSelectionAttributeGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            defaultGrid.Selection.Add(new EssGridSelection(2, 0));

            var zoomInGrid = await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMIN );

            defaultGrid.Selection[0].startRow = 6;

            defaultGrid.Selection[0].startColumn = 0;

            var zoomOutGrid = await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMOUT);

            Assert.Equal(3, zoomOutGrid.Slice.Rows);

            Assert.True(string.Equals("105524.0", zoomOutGrid.Slice.Data.Ranges[0].Values[9]));
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 30 - Essbase_AfterDefaultGrid_CanZoomToBottomWithPreferences"), Priority(30)]
        public async Task Essbase_AfterDefaultGrid_CanZoomToBottomWithPreferences()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var cube = await server.GetApplicationAsync("Sample").GetCubeAsync("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            // Assign the default grid preferences from the server;
            defaultGrid.Preferences = await server.GetDefaultGridPreferencesAsync();

            defaultGrid.Preferences.ZoomIn.Ancestor = ZoomInAncestor.BOTTOM;

            defaultGrid.Preferences.ZoomIn.Mode = ZoomInMode.BASE;

            defaultGrid.Preferences.RepeatMemberLabels = false;

            /*
            await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(1, 1));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(0, 1));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(0, 2));
            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new EssGridSelection(0,3));
            */

            await defaultGrid.ZoomAsync(EssGridZoomType.ZOOMIN, new List<EssGridSelection>() { new EssGridSelection(0, 1), new EssGridSelection(0, 2), new EssGridSelection(0, 3), new EssGridSelection(2, 0) /*new EssGridSelection(0, 4)*/ });

            Assert.Equal(23206, defaultGrid.Slice.Rows);

            Assert.True(string.Equals("-208.0", defaultGrid.Slice.Data.Ranges[0].Values[214]));

            Assert.True(string.Equals("          Mar", defaultGrid.Slice.Data.Ranges[0].Values[18]));
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 31 - Essbase_AfterDefaultGrid_CanGetGridLayout"), Priority(31)]
        public async Task Essbase_AfterDefaultGrid_CanGetGridLayout()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            await defaultGrid.GetGridLayoutAsync();
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 32 - Essbase_AfterDefaultGrid_CanPerformParallelGridOperations"), Priority(32)]
        public async Task Essbase_AfterDefaultGrid_CanPerformParallelGridOperations()
        {
            // Get the Sample.Basic cube (as an end-user).
            var cube = await GetEssServer(EssServerRole.User)
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Construct a list for grid refresh tasks.
            var refreshTasks = new List<Task<IEssGrid>>();

            // Add 20 tasks that get and refresh the default grid.
            for ( int i = 0; i < 20; i++ )
                refreshTasks.Add(cube.GetDefaultGridAsync().RefreshAsync());

            // Await the completion of all the refresh tasks.
            var grids = await Task.WhenAll(refreshTasks);

            // Assert that all of the grids have three rows.
            Assert.All(grids, grid => Assert.Equal(3, grid.Slice.Rows));
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 33 - Essbase_AfterDefaultGrid_CanPerformParallelGridOperationsWithPrefs"), Priority(33)]
        public async Task Essbase_AfterDefaultGrid_CanPerformParallelGridOperationsWithPrefs()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the default grid preferences from the server.
            var preferences = await server.GetDefaultGridPreferencesAsync();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Construct a new random.
            var random = new Random();

            // Run the sequence twice.
            for ( int i = 0; i < 2; i++ )
            {
                // Construct a list for grid refresh tasks.
                var tasks = new List<Task<IEssGrid>>();

                // Add 20 tasks that get and refresh the default grid.
                for ( int j = 0; j < 20; j++ )
                    tasks.Add(gridTaskAsync());

                async Task<IEssGrid> gridTaskAsync()
                {
                    // Get the default grid and set the preferences to navigate
                    // with or without data on the basis of a random boolean.
                    var grid  = await cube.GetDefaultGridAsync();
                    {
                        grid.Preferences = preferences.Clone() as EssGridPreferences;
                        grid.Preferences.Navigate = random.NextDouble() < 0.5;
                    }

                    // Refresh the default grid.
                    return await grid.RefreshAsync();
                }

                // Await the completion of all the refresh tasks.
                var grids = await Task.WhenAll(tasks);

                // Assert that all of the grids have the expected three rows.
                Assert.All(grids, grid =>
                {
                    // Assert that the grid slice has three rows.
                    Assert.Equal(3, grid.Slice.Rows);

                    // Assert that the grid slice has data when navigate is true.
                    if ( grid.Preferences.Navigate )
                        Assert.Equal("105524.0", grid.Slice.Data.Ranges[0].Values[9]);
                    else
                        Assert.Equal(string.Empty, grid.Slice.Data.Ranges[0].Values[9]);
                });
            }
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 34 - Essbase_AfterDefaultGrid_CanKillRedundantSessions"), Priority(34)]
        public async Task Essbase_AfterDefaultGrid_CanKillRedundantSessions()
        {
            // Get an unconnected server (as the service admin).
            var server = GetEssServer(EssServerRole.ServiceAdministrator);

            // Get the username of the configured end-user.
            var username = GetEssConnection(EssServerRole.User).Username;

            // Capture the list of sessions for the end-user.
            var userSessions = (await server.GetSessionsAsync())
                .Where(session => string.Equals(session ?.UserId, username)).ToList();

            // DO NOT assert that there is at least one session for the end-user.
            //Assert.NotEmpty(userSessions);

            // Kill all open sessions for the end-user.
            await (server as EssServer).KillSessionsForUserAsync(username);

            // Refresh the list of sessions for the end-user.
            userSessions = (await server.GetSessionsAsync())
                .Where(session => string.Equals(session?.UserId, username)).ToList();

            // Assert that there are no sessions for the end-user.
            Assert.Empty(userSessions);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 35 - Essbase_AfterDefaultGrid_CanEnforceMaxDegreeOfParallelism"), Priority(35)]
        public async Task Essbase_AfterDefaultGrid_CanEnforceMaxDegreeOfParallelism()
        {
            // Build a new factory that creates connections that support only a single concurrent operation.
            var factory = new EssServerFactory() { MaxDegreeOfParallelism = 1 };

            // Get an unconnected server with the configured factory (as an end-user) 
            var server = GetEssServer(factory: factory, role: EssServerRole.User);

            // Get the default grid preferences for the server.
            await server.GetDefaultGridPreferencesAsync();

            // Get the Sample.Basic cube (as an end-user).
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Construct a list for grid refresh tasks.
            var refreshTasks = new List<Task<IEssGrid>>();

            // Add 20 tasks that get and refresh the default grid.
            for ( int i = 0; i < 20; i++ )
                refreshTasks.Add(cube.GetDefaultGridAsync().RefreshAsync());

            // Await the completion of all the refresh tasks.
            var grids = await Task.WhenAll(refreshTasks);

            // Assert that all of the grids have three rows.
            Assert.All(grids, grid => Assert.Equal(3, grid.Slice.Rows));

            // Get the end-user's username.
            var username = (await server.GetUserSessionAsync()).UserId;

            // Capture the list of sessions for the end-user.
            var userSessions = (await cube.Application.Server.GetSessionsAsync())
                .Where(session => string.Equals(session?.UserId, username)).ToList();

            // Assert that there are only two sessions (one for server access and one grid operations).
            Assert.True(userSessions.Where(session => session.SessionType is IEssSession.EssSessionType.Server).Count() <= 2);
            Assert.True(userSessions.Where(session => session.SessionType is IEssSession.EssSessionType.Grid).Count() <= 2);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 36 - Essbase_AfterDefaultGrid_CanLogRequestsAndResponses"), Priority(36)]
        public async Task Essbase_AfterDefaultGrid_CanLogRequestsAndResponses()
        {
            var builder = new StringBuilder();

            // Build a new factory that creates connections with a logger.
            var factory = new EssServerFactory() { Logger = new StringLogger(ref builder) };

            // Get the server base url, i.e., http://localhost:9000/essbase.
            var serverBaseUrl = GetEssConnection().Server.TrimEnd('/');

            // Get an unconnected server with the configured factory 
            var server = GetEssServer(factory: factory);

            await server.GetApplicationAsync("Sample");

            var requestSummary  = $@"# GET {serverBaseUrl}/rest/v1/applications/Sample HTTP/1.1";

            Assert.Equal(requestSummary, builder.ToString().Split(Environment.NewLine)[0]);

            var responseSummary = @"# HTTP/1.1 200 OK";

            Assert.Equal(responseSummary, builder.ToString().Split(Environment.NewLine)[2]);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 37 - Essbase_AfterDefaultGrid_CanRefreshGridUseAliases"), Priority(37)]
        public async Task Essbase_AfterDefaultGrid_CanRefreshGridUseAliases()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var cube = await server.GetApplicationAsync("Sample").GetCubeAsync("Basic");

            var grid = new Grid()
            {
                Dimensions = new List<GridDimension>()
                {
                    new GridDimension()
                    {
                        Name = "Year",
                        Row = -1,
                        Column = -1,
                        Pov = "Jan",
                        Hidden = false,
                        Expanded = false
                    },
                    new GridDimension()
                    {
                        Name = "Measures",
                        Row = -1,
                        Column = -1,
                        Pov = "Sales",
                        Hidden = false,
                        Expanded = false
                    },
                    new GridDimension()
                    {
                        Name = "Product",
                        Row = -1,
                        Column = 0,
                        Pov = "",
                        Hidden = false,
                        Expanded = false
                    },
                    new GridDimension() {
                        Name = "Market",
                        Row = -1,
                        Column = -1,
                        Pov = "New York",
                        Hidden = false,
                        Expanded = false
                    },
                    new GridDimension()
                    {
                        Name = "Scenario",
                        Row = -1,
                        Column = -1,
                        Pov = "Actual",
                        Hidden = false,
                        Expanded = false
                    }
                },
                Slice = new Slice()
                {
                    Columns = 5,
                    Rows = 2,
                    Data = new Data()
                    {
                        Ranges = new List<GridRange>()
                                    {
                                        new GridRange()
                                        {
                                                Start = 0,
                                                End = 9,
                                                Values = new List<string>() { "", "Jan", "Sales", "New York", "Actual", "Colas", "2479.0", "", "", "" },
                                                Types = new List<string>() { "7", "0", "0", "0", "0", "0", "2", "7", "7", "7" },
                                                Texts = new List<string>() { null, null, null, null, null, null, null, null, null, null },
                                                DataFormats = new List<string>() { },
                                                Statuses = new List<string>() { "0", "16", "134217744", "402653200", "536870928", "268435475", "2", "0", "0", "0" },
                                                Filters = new List<string>() { },
                                                EnumIds = new List<string>() { "", "", "", "", "", "", "", "", "", "" }
                                        }
                                    }
                    }
                }
            };

            var essGrid = new EssGrid(grid, cube as EssCube);

            var preferences = await server.GetDefaultGridPreferencesAsync();

            //preferences.ColumnSupression.Missing = true;
            //preferences.RowSupression.Missing = true;

            essGrid.Preferences = preferences;

            essGrid.Selection = new List<EssGridSelection> { new EssGridSelection(1, 0) };

            var zInGrid = await essGrid.ZoomAsync(zoomOption: EssGridZoomType.ZOOMIN);

            var zOutGrid = await essGrid.ZoomAsync(zoomOption: EssGridZoomType.ZOOMOUT);

            var rGrid = await essGrid.RefreshAsync();
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 38 - Essbase_AfterDefaultGrid_CanRefreshEmptyGrid"), Priority(38)]
        public async Task Essbase_AfterDefaultGrid_CanRefreshEmptyGrid()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            var cube = await server.GetApplicationAsync("Sample").GetCubeAsync("Basic");

            var grid = new Grid() { };

            var essGrid = new EssGrid(grid, cube as EssCube);

            var preferences = await server.GetDefaultGridPreferencesAsync();

            //preferences.ColumnSupression.Missing = true;
            //preferences.RowSupression.Missing = true;

            essGrid.Preferences = preferences;

            essGrid.Selection = new List<EssGridSelection> { new EssGridSelection(1, 0) };

            var rGrid = await essGrid.RefreshAsync();
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 39 - Essbase_AfterCubeCreation_CanBuildAndRefreshGrid"), Priority(39)]
        public async Task Essbase_AfterCubeCreation_CanBuildAndRefreshGrid()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the "CalcAll" script from Sample.Basic.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var grid = cube.GetGrid();

            grid.Alias = "Default";

            grid.Dimensions = new List<EssGridDimension>()
            {
                new EssGridDimension()
                {
                    Name = "Year",
                    Row = -1,
                    Column = 0,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Measures",
                    Row = 1,
                    Column = -1,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Product",
                    Row = -1,
                    Column = -1,
                    Pov = "Product",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Market",
                    Row = -1,
                    Column = -1,
                    Pov = "Market",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Scenario",
                    Row = -1,
                    Column = -1,
                    Pov = "Scenario",
                    Hidden = false,
                    Expanded = false
                }
            };

            grid.Slice = new EssGridSlice()
            {
                Columns = 4,
                Rows = 3,
                Data = new EssGridSliceData()
                {
                    Ranges = new List<EssGridRange>()
                    {
                        new EssGridRange()
                        {
                            Start = 0,
                            End = 11,
                            Values = new List<string>()
                            {
                                "", "Product", "Market", "Scenario", "", "Measures", "", "", "Year", "", "", ""
                            }
                        }
                    }
                }
            };

            await grid.RefreshAsync();

            Assert.Equal("105524.0", grid.Slice.Data.Ranges[0].Values[9]);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 40 - Essbase_AfterCubeCreation_CanTrackDataChanges"), Priority(40)]
        public async Task Essbase_AfterCubeCreation_CanTrackDataChanges()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the "CalcAll" script from Sample.Basic.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var grid = cube.GetGrid();

            grid.Alias = "Default";

            grid.Dimensions = new List<EssGridDimension>()
            {
                new EssGridDimension()
                {
                    Name = "Year",
                    Row = -1,
                    Column = 3,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Measures",
                    Row = -1,
                    Column = 2,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Product",
                    Row = -1,
                    Column = 1,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Market",
                    Row = -1,
                    Column = 0,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Scenario",
                    Row = 0,
                    Column = -1,
                    Pov = "Scenario",
                    Hidden = false,
                    Expanded = false
                }
            };

            grid.Slice = new EssGridSlice()
            {
                Columns = 5,
                Rows = 2,
                Data = new EssGridSliceData()
                {
                    Ranges = new List<EssGridRange>()
                    {
                        new EssGridRange()
                        {
                            Start = 0,
                            End = 9,
                            Values = new List<string>()
                            {
                                 "", "", "", "", "Actual", "New York", "Cola", "Sales", "Jan", "678.0"
                            },
                            Types = new List<string>()
                            {
                                "7", "7", "7", "7", "0", "0", "0", "0", "0", "2"
                            }
                        }
                    }
                }
            };

            await grid.RefreshAsync();

            grid.Preferences.TrackDataChanges = true;
            string newVal;

            if ( string.Equals(grid.Slice.Data.Ranges[0].Values[9], "680.0") )
            {
                grid.Slice.Data.Ranges[0].Values[9] = "678.0";
                newVal = "678.0";
            }
            else
            {
                grid.Slice.Data.Ranges[0].Values[9] = "680.0";
                newVal = "680.0";
            }
                

            await grid.SubmitAsync();

            Assert.Equal(newVal, grid.Slice.Data.Ranges[0].Values[9]);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 41 - Essbase_AfterCubeCreation_CanTrackDataChangesOnBiggerGrid"), Priority(41)]
        public async Task Essbase_AfterCubeCreation_CanTrackDataChangesOnBiggerGrid()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the "CalcAll" script from Sample.Basic.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var grid = cube.GetGrid();

            grid.Alias = "Default";

            grid.Dimensions = new List<EssGridDimension>()
            {
                new EssGridDimension()
                {
                    Name = "Year",
                    Row = -1,
                    Column = 3,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Measures",
                    Row = -1,
                    Column = 2,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Product",
                    Row = -1,
                    Column = 1,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Market",
                    Row = -1,
                    Column = 0,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Scenario",
                    Row = 0,
                    Column = -1,
                    Pov = "Scenario",
                    Hidden = false,
                    Expanded = false
                }
            };

            grid.Slice = new EssGridSlice()
            {
                Columns = 6,
                Rows = 4,
                Data = new EssGridSliceData()
                {
                    Ranges = new List<EssGridRange>()
                    {
                        new EssGridRange()
                        {
                            Start = 0,
                            End = 23,
                            Values = new List<string>()
                            {
                                 "", "", "", "", "Actual", "Budget", "New York", "Cola", "Sales", "Jan", "678.0", "675.0", "", "", "", "Feb", "645.0", "610.0", "", "", "", "Mar", "675.0", "640.0"
                            },
                            Types = new List<string>()
                            {
                                "7", "7", "7", "7", "0", "0", "0", "0", "0", "0", "2", "2", "7", "7", "7", "0", "2", "2", "7", "7", "7", "0", "2", "2"
                            }
                        }
                    }
                }
            };

            await grid.RefreshAsync();

            grid.Preferences.TrackDataChanges = true;

            if ( string.Equals(grid.Slice.Data.Ranges[0].Values[10], "680.0") )
                grid.Slice.Data.Ranges[0].Values[10] = "678.0";
            else
                grid.Slice.Data.Ranges[0].Values[10] = "680.0";

            if ( string.Equals(grid.Slice.Data.Ranges[0].Values[17], "610.0") )
                grid.Slice.Data.Ranges[0].Values[17] = "620.0";
            else
                grid.Slice.Data.Ranges[0].Values[17] = "610.0";

            await grid.SubmitAsync();

            Assert.NotEqual(grid.DataChanges.DataChanges[0].NewValue, grid.DataChanges.DataChanges[0].OldValue);

            Assert.Equal("Year", grid.DataChanges.DataChanges[0].DataPoints[0].DimensionName);
            Assert.Equal("Measures", grid.DataChanges.DataChanges[0].DataPoints[1].DimensionName);
            Assert.Equal("Product", grid.DataChanges.DataChanges[0].DataPoints[2].DimensionName);
            Assert.Equal("Market", grid.DataChanges.DataChanges[0].DataPoints[3].DimensionName);
            Assert.Equal("Scenario", grid.DataChanges.DataChanges[0].DataPoints[4].DimensionName);

            Assert.Equal("Jan", grid.DataChanges.DataChanges[0].DataPoints[0].Member);
            Assert.Equal("Sales", grid.DataChanges.DataChanges[0].DataPoints[1].Member);
            Assert.Equal("Cola", grid.DataChanges.DataChanges[0].DataPoints[2].Member);
            Assert.Equal("New York", grid.DataChanges.DataChanges[0].DataPoints[3].Member);
            Assert.Equal("Actual", grid.DataChanges.DataChanges[0].DataPoints[4].Member);

            Assert.NotEqual(grid.DataChanges.DataChanges[1].NewValue, grid.DataChanges.DataChanges[1].OldValue);

            Assert.Equal("Year", grid.DataChanges.DataChanges[1].DataPoints[0].DimensionName);
            Assert.Equal("Measures", grid.DataChanges.DataChanges[1].DataPoints[1].DimensionName);
            Assert.Equal("Product", grid.DataChanges.DataChanges[1].DataPoints[2].DimensionName);
            Assert.Equal("Market", grid.DataChanges.DataChanges[1].DataPoints[3].DimensionName);
            Assert.Equal("Scenario", grid.DataChanges.DataChanges[1].DataPoints[4].DimensionName);

            Assert.Equal("Feb", grid.DataChanges.DataChanges[1].DataPoints[0].Member);
            Assert.Equal("Sales", grid.DataChanges.DataChanges[1].DataPoints[1].Member);
            Assert.Equal("Cola", grid.DataChanges.DataChanges[1].DataPoints[2].Member);
            Assert.Equal("New York", grid.DataChanges.DataChanges[1].DataPoints[3].Member);
            Assert.Equal("Budget", grid.DataChanges.DataChanges[1].DataPoints[4].Member);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 42 - Essbase_AfterCubeCreation_CanTrackDataChangesMultipleColumnHeaders"), Priority(42)]
        public async Task Essbase_AfterCubeCreation_CanTrackDataChangesMultipleColumnHeaders()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the "CalcAll" script from Sample.Basic.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var grid = cube.GetGrid();

            grid.Alias = "Default";

            grid.Dimensions = new List<EssGridDimension>()
            {
                new EssGridDimension()
                {
                    Name = "Year",
                    Row = 2,
                    Column = -1,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Measures",
                    Row = -1,
                    Column = 0,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Product",
                    Row = 1,
                    Column = -1,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Market",
                    Row = 0,
                    Column = -1,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                },
                new EssGridDimension()
                {
                    Name = "Scenario",
                    Row = 3,
                    Column = -1,
                    Pov = "",
                    Hidden = false,
                    Expanded = false
                }
            };

            grid.Slice = new EssGridSlice()
            {
                Columns = 5,
                Rows = 12,
                Data = new EssGridSliceData()
                {
                    Ranges = new List<EssGridRange>()
                    {
                        new EssGridRange()
                        {
                            Start = 0,
                            End = 59,
                            Values = new List<string>()
                            {
                                 "", "", "New York", "", "", "", "", "Root Beer", "", "", "", "", "Jan", "", "", "", "Actual", "Budget", "Variance", "Variance %", "Sales", "551.0", "530.0", "21.0", "3.9622641509433962", "COGS", "333.0", "310.0", "-23.0", "-7.419354838709677", "     Margin", "218.0", "220.0", "-2.0", "-0.9090909090909091", "Marketing", "158.0", "140.0", "-18.0", "-12.857142857142856", "Payroll", "57.0", "50.0", "-7.0", "-14.000000000000002", "Misc", "0.0", "0.0", "0.0", "", "     Total Expenses", "215.0", "190.0", "-25.0", "-13.157894736842104", "          Profit", "3.0", "30.0", "-27.0", "-90.0"
                            },
                            Types = new List<string>()
                            {
                                "7", "7", "0", "7", "7", "7", "7", "0", "7", "7", "7", "7", "0", "7", "7", "7", "0", "0", "0", "0", "0", "2", "2", "2", "2", "0", "2", "2", "2", "2", "0", "2", "2", "2", "2", "0", "2", "2", "2", "2", "0", "2", "2", "2", "2", "0", "2", "2", "2", "2", "0", "2", "2", "2", "2", "0", "2", "2", "2", "2"
                            }
                        }
                    }
                }
            };

            grid.Preferences = new EssGridPreferences()
            {
                TrackDataChanges = true,
                MissingText = "",
                RepeatMemberLabels = false
            };



            await grid.RefreshAsync();

            /*
            if ( string.Equals(grid.Slice.Data.Ranges[0].Values[21], "1778.0") )
                grid.Slice.Data.Ranges[0].Values[21] = "1887.0";
            else
                grid.Slice.Data.Ranges[0].Values[21] = "1778.0";
            */
            if ( string.Equals(grid.Slice.Data.Ranges[0].Values[21], "551.0") )
                grid.Slice.Data.Ranges[0].Values[21] = "570";
            else
                grid.Slice.Data.Ranges[0].Values[21] = "551.0";

            await grid.SubmitAsync();

            Assert.NotNull(grid.DataChanges);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 43 - Essbase_AfterCubeCreation_CanExportAppToLcm"), Priority(43)]
        public async Task Essbase_AfterCubeCreation_CanExportAppToLcm()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the "CalcAll" script from Sample.Basic.
            var app = await server.GetApplicationAsync("Sample");

            var options = new EssJobExportLcmOptions()
            {
                AllApp = true,
                Generateartifactlist = false,
                IncludeServerLevel = false,
                ZipFileName = "exportedApps.zip",
                SkipData = true
            };

            var lcmStream = await app.ExportCubeToLcmAsync("Basic", options);

            Assert.NotNull(lcmStream);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 44 - Essbase_AfterCubeCreation_CanExportCubeToLcm"), Priority(44)]
        public async Task Essbase_AfterCubeCreation_CanExportToLcm()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the "CalcAll" script from Sample.Basic.
            var app = await server.GetApplicationAsync("Sample");

            var options = new EssJobExportLcmOptions()
            {
                AllApp = false,
                Generateartifactlist = true,
                IncludeServerLevel = true,
                ZipFileName = "exportedCubes.zip",
                SkipData = true
            };

            var lcmStream = await app.ExportCubeToLcmAsync("Basic", options);

            using ( FileStream output = File.OpenWrite(Path.Combine(Path.GetTempPath(), "exportedCubes.zip")) )
            {
                lcmStream.CopyTo(output);
            }

            Assert.NotNull(lcmStream);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 45 - Essbase_AfterCubeCreation_CanImportCubeWithLcm"), Priority(45)]
        public async Task Essbase_AfterCubeCreation_CanImportCubeToLcm()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the "CalcAll" script from Sample.Basic.
            var app = await server.GetApplicationAsync("Sample");

            var options = new EssJobImportLcmOptions()
            {
                ZipFileName = "exportedCubes.zip",
                IncludeServerLevel = false,
                TargetApplicationName = "Sample",
                Overwrite = true
            };

            var lcmStream = await app.CreateCubeFromLcmAsync("Basic", options);

            Assert.NotNull(lcmStream);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 46 - Essbase_AfterCubeCreation_CanImportCubeWithLcmFromStream"), Priority(46)]
        public async Task Essbase_AfterCubeCreation_CanImportCubeWithLcmFromStream()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the "CalcAll" script from Sample.Basic.
            var app = await server.GetApplicationAsync("Sample");

            var option = new EssJobImportLcmOptions()
            {
                Overwrite = true
            };

            var lcmStream = await app.CreateCubeFromLcmAsync(cubeName: "Basic", localLcmPath: Path.Combine(Path.GetTempPath(), "exportedCubes.zip"), options: option);

            File.Delete(Path.Combine(Path.GetTempPath(), "exportedCubes.zip"));

            Assert.NotNull(lcmStream);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 47 - Essbase_AfterDefaultGrid_CanLogRequestsAndResponsesToDirectory"), Priority(47)]
        public async Task Essbase_AfterDefaultGrid_CanLogRequestsAndResponsesToDirectory()
        {
            var outputDir = new DirectoryInfo(@"C:\Users\matth\Desktop");

            // Build a new factory that creates connections with a logger.
            var factory = new EssServerFactory() { Logger = new FileOutputLogger(outputDir) };

            // Get the server base url, i.e., http://localhost:9000/essbase.
            var serverBaseUrl = GetEssConnection().Server.TrimEnd('/');

            // Get an unconnected server with the configured factory 
            var server = GetEssServer(factory: factory);

            await server.GetApplicationAsync("Sample");

            var requestSummary  = $@"# GET {serverBaseUrl}/rest/v1/applications/Sample HTTP/1.1";

            //Assert.Equal(requestSummary, builder.ToString().Split(Environment.NewLine)[0]);

            var responseSummary = @"# HTTP/1.1 200 OK";

            //Assert.Equal(responseSummary, builder.ToString().Split(Environment.NewLine)[2]);
        }
    }
}

        /*
        [Fact(DisplayName = @"PerformServerFunctionTests - 41 - Essbase_AfterDefaultGrid_CanCreateAndSignOffSessions"), Priority(41)]
        public async Task Essbase_AfterDefaultGrid_CanCreateAndSignOffSessions()
        {
            // Get an unconnected server.
            var server = GetEssServer(EssServerRole.User);

            var beforeSessions = await server.GetSessionsAsync();

            var application = await server.GetApplicationAsync("Sample");

            var afterAppSessions = await server.GetSessionsAsync();

            var cube = await application.GetCubeAsync("Basic");

            var afterCubeSessions = await server.GetSessionsAsync();

            var cubeVariables = await cube.GetVariablesAsync();

            var afterVariablesSessions = await server.GetSessionsAsync();

            var grid = await cube.GetDefaultGridAsync();

            var afterGridSessions = await server.GetSessionsAsync();

            var refreshedGrid = await grid.RefreshAsync();

            var afterRefreshSessions = await server.GetSessionsAsync();

            await server.SignOutAsync(allSessions: true);

            var afterSignOutSessions = await server.GetSessionsAsync();

            Assert.Single(afterSignOutSessions);
        }
        */

        /*
        [Fact(DisplayName = @"PerformServerFunctionTests - 42 - Essbase_AfterDefaultGrid_CanSignOffAllSessions"), Priority(42)]
        public async Task Essbase_AfterDefaultGrid_CanSignOffAllSessions()
        {
            // Get an unconnected server.
            var server = GetEssServer(EssServerRole.User);

            var beforeSignOutSessions = await server.GetSessionsAsync();

            await server.SignOutAsync(allSessions: true);

            var afterSignOutSessions = await server.GetSessionsAsync();

            Assert.Single(afterSignOutSessions);
        }
        */

