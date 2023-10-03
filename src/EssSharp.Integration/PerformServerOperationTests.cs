﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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

            await group.AddUsersAsync(new List<string>() {userConnection.Username });

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

            await group.RemoveUsersAsync(new List<string>() {userConnection.Username });

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

        [Fact(DisplayName = @"PerformServerFunctionTests - 19 - Essbase_AfterDefaultGrid_CanRefreshGrid"), Priority(19)]
        public async Task Essbase_AfterDefaultGrid_CanRefreshGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            var refreshGrid = await defaultGrid.RefreshAsync();
            
            Assert.Equal(3, refreshGrid.Slice.Rows);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 20 - Essbase_AfterDefaultGrid_CanZoomInGrid"), Priority(20)]
        public async Task Essbase_AfterDefaultGrid_CanZoomInGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            var zoomInGrid = await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));

            Assert.Equal(7, zoomInGrid.Slice.Rows);

            Assert.True(string.Equals("27107.0", zoomInGrid.Slice.Data.Ranges[0].Values[13]));
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 21 - Essbase_AfterDefaultGrid_CanZoomOutGrid"), Priority(21)]
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

        [Fact(DisplayName = @"PerformServerFunctionTests - 22 - Essbase_AfterDefaultGrid_CanKeepOnlyGrid"), Priority(22)]
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

        [Fact(DisplayName = @"PerformServerFunctionTests - 23 - Essbase_AfterDefaultGrid_CanRemoveOnlyGrid"), Priority(23)]
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


        [Fact(DisplayName = @"PerformServerFunctionTests - 24 - Essbase_AfterDefaultGrid_CanPivotToPovWithSelectionAttributeGrid"), Priority(24)]
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

        
        [Fact(DisplayName = @"PerformServerFunctionTests - 25 - Essbase_AfterDefaultGrid_CanPivotToPovGrid"), Priority(25)]
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

            var pivotPovGrid = await defaultGrid.PivotAsync(  new EssGridSelection(2, 0), new EssGridSelection(0, 2));

            Assert.Equal(4, pivotPovGrid.Slice.Rows);

            Assert.True(string.Equals("New York", pivotPovGrid.Slice.Data.Ranges[0].Values[2]));

            pivotPovGrid = await defaultGrid.PivotAsync(  new EssGridSelection(0, 2) );

            Assert.Equal(3, pivotPovGrid.Slice.Rows);

            Assert.True(string.Equals("New York", pivotPovGrid.Slice.Data.Ranges[0].Values[8]));
        }
        
        
        [Fact(DisplayName = @"PerformServerFunctionTests - 26 - Essbase_AfterDefaultGrid_CanSubmitNewValueGrid"), Priority(26)]
        public async Task Essbase_AfterDefaultGrid_CanSubmitNewValueGrid()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();
            
            // Year > Qtr1 > Jan
            await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));
            await defaultGrid.ZoomAsync( EssGridZoomType.ZOOMIN, new EssGridSelection(2, 0));
            
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

            defaultGrid.Slice.Data.Ranges[0].Values[11] = "680";

            var submitGrid = await defaultGrid.SubmitNewValueAsync( );

            (await cube.GetScriptAsync<IEssCalcScript>("CalcAll").ConfigureAwait(false)).Execute();

            await submitGrid.RefreshAsync();

            Assert.Equal(3, submitGrid.Slice.Rows);

            Assert.True(string.Equals("680.0", submitGrid.Slice.Data.Ranges[0].Values[11]));

            defaultGrid.Slice.Data.Ranges[0].Values[11] = "678";

            submitGrid = await defaultGrid.SubmitNewValueAsync( );

            await (await cube.GetScriptAsync<IEssCalcScript>("CalcAll").ConfigureAwait(false)).ExecuteAsync();
        }
        
        [Fact(DisplayName = @"PerformServerFunctionTests - 27 - Essbase_AfterDefaultGrid_CanZoomInWithSelectionAttributeGrid"), Priority(27)]
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

        [Fact(DisplayName = @"PerformServerFunctionTests - 28 - Essbase_AfterDefaultGrid_CanZoomOutWithSelectionAttributeGrid"), Priority(28)]
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

            Assert.True(string.Equals("105522.0", zoomOutGrid.Slice.Data.Ranges[0].Values[9]));
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 29 - Essbase_AfterDefaultGrid_CanSetGridPreferences"), Priority(29)]
        public async Task Essbase_AfterDefaultGrid_CanSetGridPreferences()
        {
            // Get an unconnected server.
            var cube = GetEssServer().GetApplication("Sample").GetCube("Basic");

            var defaultGrid = await cube.GetDefaultGridAsync();

            await defaultGrid.GetGridPreferencesAsync();

            defaultGrid.Preferences.ZoomIn.Ancestor = ZoomInAncestor.BOTTOM;

            defaultGrid.Preferences.ZoomIn.Mode = ZoomInMode.BASE;

            await defaultGrid.SetGridPreferencesAsync();

            Assert.True(defaultGrid.Preferences.ZoomIn.Ancestor == ZoomInAncestor.BOTTOM);

            Assert.True(defaultGrid.Preferences.ZoomIn.Mode == ZoomInMode.BASE);
        }

        [Fact(DisplayName = @"PerformServerFunctionTests - 30 - Essbase_AfterReportCreation_CanExecuteDrillthroughReport"), Priority(30)]
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
            if ( version.CompareTo(new Version(major: 21, minor: 4)) >= 0  )
            {
                // Execute the report for the given drillthrough range.
                var (report, columnTypes) = await drillthroughReport
                    .ExecuteAsync(new EssDrillthroughRange(dimensionMemberSets: new()
                    {
                        ["Year"    ] = new() { "Year",     "Year"       },
                        ["Product" ] = new() { "Cola",     "Cola"       },
                        ["Measures"] = new() { "Sales",    "Sales"      },
                        ["Market"  ] = new() { "New York", "California" },
                        ["Scenario"] = new() { "Actual",   "Actual"     }
                    }), new EssDrillthroughOptions(returnTypedValues: true, prefixStringValuesForExcel: true));

                // Assert the column type, header name, and first row value for the fifth column.
                Assert.Equal(expected: "double", actual:       columnTypes[4], ignoreCase: true);
                Assert.Equal(expected: "amount", actual: (string)report[0, 4], ignoreCase: true);
                Assert.Equal(expected:   501.72, actual: (double)report[1, 4]);
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
    }
}
