using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;
using EssSharp.Model;
using Xunit;

namespace EssSharp.Integration
{
    [Collection("EssSharp Integration Tests"), Trait("type", "get"), CollectionPriority(5)]
    public class GetServerObjectTests : IntegrationTestBase
    {
        [Fact(DisplayName = $@"GetServerObjectTests - 01 - Essbase_AfterAppCreation_CanGetAppConfigurations"), Priority(01)]
        public async Task Essbase_AfterAppCreation_CanGetAppConfigurations()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the configurations for the sample application.
            var configurations = await (await server.GetApplicationAsync("Sample"))
                .GetConfigurationsAsync();

            // Assert that the collection of configurations is not empty.
            Assert.NotEmpty(configurations);
        }

        [Fact(DisplayName = $@"GetServerObjectTests - 02 - Essbase_AfterLockCreation_CanGetLocks"), Priority(02)]
        public async Task Essbase_AfterLockCreation_CanGetLocks()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the locked object from the server.
            var lockedObject = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic")
                .GetLockedObjectAsync("CalcAll");

            // Assert that the lock object name is the same as the one we passed. --Make better 
            Assert.Equal("CalcAll", lockedObject.Name);
        }

        [Fact(DisplayName = $@"GetServerObjectTests - 03 - Essbase_AfterScriptCreation_CannotGetContentOfNonExistentScript"), Priority(03)]
        public async Task Essbase_AfterScriptCreation_CannotGetContentOfNonExistentScript()
        {
            // Get an unconnected server.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server.GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            // Create an ephemeral mdx script in memory and verify that its content cannot be gotten (since it doesn't exist on the cube).
            var inMemoryScript = await cube.CreateScriptAsync<IEssMdxScript>("test99", saveToCube: false);

            // Assert that an Exception is thrown when we try to get the content of a script that does not exist on the server,
            // and capture the base exception, since this is not supported by the server.
            var exception = (await Assert.ThrowsAsync<Exception>(async () => await inMemoryScript.GetContentAsync())).GetBaseException();

            // Assert that the base exception is a WebException with a WebExceptionRestResponse with status code 400 (bad request).
            Assert.True(exception is WebException { Response: EssSharp.Api.WebExceptionRestResponse { StatusCode: HttpStatusCode.BadRequest } });
        }

        [Fact(DisplayName = @"GetServerObjectTests - 04 - Essbase_AfterReportCreation_CanGetDrillthroughReport"), Priority(04)]
        public async Task Essbase_AfterReportCreation_CanGetDrillthroughReport()
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

            // Assert that we were able to get the drillthrough report.
            Assert.NotNull(drillthroughReport);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 05 - Essbase_AfterReportCreation_CanGetDrillthroughReportDetails"), Priority(05)]
        public async Task Essbase_AfterReportCreation_CanGetDrillthroughReportDetails()
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

            // Get the full report specification details.
            await drillthroughReport.GetDetailsAsync();

            // Assert that we were able to get the drillthrough report specification details.
            Assert.NotNull(drillthroughReport.Details);
            // Assert that we have column mappings.
            Assert.NotEmpty(drillthroughReport.Details.ColumnMappings);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 06 - Essbase_AfterReportCreation_CannotGetDrillthroughReportDetailsAsUser"), Priority(06)]
        public async Task Essbase_AfterReportCreation_CannotGetDrillthroughReportDetailsAsUser()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer(EssServerRole.User);

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

            // Assert that an exception is thrown when we try to get the drillthrough report specification details
            // as an unprivileged user, and capture the base exception.
            var exception = (await Assert.ThrowsAsync<Exception>(async () => await drillthroughReport.GetDetailsAsync())).GetBaseException();

            // Assert that the base exception is a WebException with a WebExceptionRestResponse with status code 401 (unauthorized).
            Assert.True(exception is WebException { Response: EssSharp.Api.WebExceptionRestResponse { StatusCode: HttpStatusCode.Unauthorized } });
        }

        [Fact(DisplayName = @"GetServerObjectTests - 07 - Essbase_AfterReportCreation_CanGetDefaultGridPrefernces"), Priority(07)]
        public async Task Essbase_AfterReportCreation_CanGetDefaultGridPrefernces()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            var preferences = await server.GetDefaultGridPreferencesAsync();

            Assert.True(preferences != null);

            Assert.True(preferences.RowSupression.NoAccess == false);

            Assert.True(preferences.ZoomIn.Ancestor == ZoomInAncestor.TOP);

            Assert.True(preferences.CellText == true);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 08 - Essbase_AfterReportCreation_CanGetMembers"), Priority(08)]
        public async Task Essbase_AfterReportCreation_CanGetMembers()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            //var mem = await (await cube.GetMemberAsync("South").ConfigureAwait(false)).GetChildrenAsync().ConfigureAwait(false);

            var memberList = await cube.GetMembersAsync() ;

            Assert.NotNull(memberList);

            Assert.Equal(11, memberList.Count);

            Assert.Equal("Market", memberList[3].Name);

            Assert.True(memberList[10].DescentantsCount == 5);

            var childList = memberList[1].GetChildren();

            Assert.NotNull(childList);

            Assert.True(childList.Count == 3);

            Assert.True(string.Equals("Product", memberList[2].Name));

            Assert.True(memberList[1].DescentantsCount == 16);

            var memList = await cube.GetMembersSearchedAsync("new");

            Assert.NotNull(memList);

            Assert.Equal(3, memList.Count);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 09 - Essbase_AfterReportCreation_CanGetMember"), Priority(09)]
        public async Task Essbase_AfterReportCreation_CanGetMember()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var member = await cube.GetMemberAsync("Year");

            Assert.NotNull(member);

            Assert.Equal("Year", member.Name);

            Assert.Equal(EssDimensionType.TIME, member.DimensionType);

            Assert.NotNull(member.Aliases);

            Assert.Equal(6, member.Aliases.Count);

            member = await cube.GetMemberAsync("Shared Diet Cola");

            Assert.NotNull(member);

            Assert.Equal("100-20", member.Name);

            Assert.True(member.IsSharedMember);

            Assert.NotNull(member.Aliases);

            Assert.NotEmpty(member.Aliases);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 10 - Essbase_AfterReportCreation_CanGetAncestor"), Priority(10)]
        public async Task Essbase_AfterReportCreation_CanGetAncestor()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var member = await cube.GetMemberAsync("Year");

            var children = await member.GetChildrenAsync();

            var ancestor = await children[0].GetAncestorsAsync();

            Assert.NotNull(ancestor);

            Assert.True(ancestor.Count == 1);

            Assert.True(string.Equals("Year", ancestor[0].Name));

            Assert.Equal(EssDimensionType.TIME, member.DimensionType);

            Assert.True(ancestor[0].Aliases.Count == 6);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 11 - Essbase_AfterReportCreation_CanGetDescendants"), Priority(11)]
        public async Task Essbase_AfterReportCreation_CanGetDescendants()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var member = await cube.GetMemberAsync("Market");

            var descendants = await member.GetDescendantsAsync();

            Assert.NotNull(descendants);

            Assert.True(descendants.Count == 24);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 12 - Essbase_AfterReportCreation_CanGetSiblings"), Priority(12)]
        public async Task Essbase_AfterReportCreation_CanGetSiblings()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var member = await cube.GetMemberAsync("Qtr1");

            var siblings = await member.GetSiblingsAsync();

            Assert.NotNull(siblings);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 13 - Essbase_AfterReportCreation_CanGetMemberWithFields"), Priority(13)]
        public async Task Essbase_AfterReportCreation_CanGetMemberWithFields()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var member = await cube.GetMemberAsync(uniqueName: "Shared Diet Cola", fields: EssMemberFields.activeAliasName | EssMemberFields.aliases);

            Assert.True(member.IsSharedMember);

            Assert.NotNull(member.Aliases);

            Assert.Equal("Shared Diet Cola", member.activeAliasName);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 14 - Essbase_AfterReportCreation_CanGetMember_returnLevelNumber"), Priority(14)]
        public async Task Essbase_AfterReportCreation_CanGetMember_returnLevelNumber()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var member = await cube.GetMemberAsync(uniqueName: "Colas");

            Assert.Equal(1, member.LevelNumber);

            Assert.NotNull(member.Aliases);

            Assert.Equal("Colas", member.activeAliasName);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 15 - Essbase_AfterReportCreation_CanGetMember_returnGenerationNumber"), Priority(15)]
        public async Task Essbase_AfterReportCreation_CanGetMember_returnGenerationNumber()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var member = await cube.GetMemberAsync(uniqueName: "Year");

            Assert.Equal(1, member.GenerationNumber);

            Assert.NotNull(member.Aliases);

            Assert.Null(member.activeAliasName);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 16 - Essbase_AfterReportCreation_CanGetMemberByGen"), Priority(16)]
        public async Task Essbase_AfterReportCreation_CanGetMemberByGen()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var genMembers = await cube.GetMembersByGenerationAsync("Market", 3);

            Assert.Equal(20, genMembers.Count);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 16 - Essbase_AfterReportCreation_CanGetMemberByLevel"), Priority(16)]
        public async Task Essbase_AfterReportCreation_CanGetMemberByLevel()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var genMembers = await cube.GetMembersByLevelAsync("Market", 0);

            Assert.Equal(20, genMembers.Count);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 17 - Essbase_AfterReportCreation_CanGetDimensionMembers"), Priority(17)]
        public async Task Essbase_AfterReportCreation_CanGetDimensionMembers()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var dimMembers = await cube.GetDimensionMembersAsync();

            Assert.Equal(11, dimMembers.Count);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 18 - Essbase_AfterReportCreation_CanGetDimensionFromMember"), Priority(18)]
        public async Task Essbase_AfterReportCreation_CanGetDimensionFromMember()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var member = await cube.GetMemberAsync("Market");

            var dim = await member.GetDimensionAsync();

            Assert.Equal("Market", dim.Name);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 19 - Essbase_AfterReportCreation_CanGetSameGenerationMembers"), Priority(19)]
        public async Task Essbase_AfterReportCreation_CanGetSameGenerationMembers()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var member = await cube.GetMemberAsync("New York");

            var sameGenFromMember = await member.GetSameGenerationMembersAsync();

            var sameGenFromCube = await cube.GetMembersByLevelAsync("Market", 0);

            Assert.Equal(20, sameGenFromMember.Count);
            Assert.Equal(20, sameGenFromCube.Count);

            Assert.All(new[] { sameGenFromMember, sameGenFromCube }, members =>
            {
                Assert.Equal("New York", members[0]?.Name);
                Assert.Equal("Massachusetts", members[1]?.Name);
                Assert.Equal("Florida", members[2]?.Name);
                Assert.Equal("Connecticut", members[3]?.Name);
            });
        }

        [Fact(DisplayName = @"GetServerObjectTests - 20 - Essbase_AfterReportCreation_CanGetDynamicTimeSeriesMembersAsync"), Priority(20)]
        public async Task Essbase_AfterReportCreation_CanGetDynamicTimeSeriesMembersAsync()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer(EssServerRole.User);

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var dtsMembers = await cube.GetDynamicTimeSeriesMembersAsync();

            Assert.Equal(2, dtsMembers.Count);

            Assert.Equal("H-T-D", dtsMembers[0]?.Name);

            Assert.Equal("Q-T-D", dtsMembers[1]?.Name);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 21 - Essbase_AfterReportCreation_CanGetDimensionsAsync"), Priority(21)]
        public async Task Essbase_AfterReportCreation_CanGetDimensionsAsync()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer(EssServerRole.User);

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var dimensions = await cube.GetDimensionsAsync();

            Assert.Equal(10, dimensions.Count);

            Assert.Equal("Year", dimensions[0]?.Name);
            Assert.Equal(EssDimensionType.TIME, dimensions[0].DimensionTag);

            Assert.Equal("Measures", dimensions[1]?.Name);
            Assert.Equal(EssDimensionType.ACCOUNTS, dimensions[1].DimensionTag);

            Assert.Equal("Product", dimensions[2]?.Name);
            Assert.Equal(EssDimensionType.Unknown, dimensions[2].DimensionTag);

            Assert.Equal("Caffeinated", dimensions[5]?.Name);
            Assert.Equal(EssDimensionType.ATTRIBUTE, dimensions[5].DimensionTag);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 22 - Essbase_AfterReportCreation_CanGetDimensionGenerationsAsync"), Priority(22)]
        public async Task Essbase_AfterReportCreation_CanGetDimensionGenerationsAsync()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer(EssServerRole.User);

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var dimension = (await cube.GetDimensionsAsync())[0];

            var generations = await dimension.GetGenerationsAsync();

            Assert.Equal(3, generations.Count);

            Assert.Equal("History", generations[0].Name);
            Assert.Equal(1, generations[0].Number);

            Assert.Equal("Quarter", generations[1].Name);
            Assert.Equal(2, generations[1].Number);

            Assert.Equal("Months", generations[2].Name);
            Assert.Equal(3, generations[2].Number);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 22 - Essbase_AfterReportCreation_CanGetDimensionLevelsAsync"), Priority(22)]
        public async Task Essbase_AfterReportCreation_CanGetDimensionLevelsAsync()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer(EssServerRole.User);

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var dimension = (await cube.GetDimensionsAsync())[0];

            var levels = await dimension.GetLevelsAsync();

            Assert.Equal(3, levels.Count);

            Assert.Equal("Lev0", levels[0].Name);
            Assert.Equal(0, levels[0].Number);

            Assert.Equal("Lev1", levels[1].Name);
            Assert.Equal(1, levels[1].Number);

            Assert.Equal("Lev2", levels[2].Name);
            Assert.Equal(2, levels[2].Number);
        }

        [Fact(DisplayName = @"GetServerObjectTests - 23 - Essbase_AfterReportCreation_CanGetBottemLevelDescendants"), Priority(23)]
        public async Task Essbase_AfterReportCreation_CanGetBottemLevelDescendants()
        {
            // Get an unconnected server as a regular user.
            var server = GetEssServer();

            // Get the Sample.Basic cube from the server.
            var cube = await server
                .GetApplicationAsync("Sample")
                .GetCubeAsync("Basic");

            var bottomLevelMembers = await (await cube.GetMemberAsync("Year")).GetBottomLevelDescendantsAsync();

            Assert.Equal(12, bottomLevelMembers.Count);

            Assert.Equal("Jan", bottomLevelMembers[0].Name);
            Assert.Equal("Apr", bottomLevelMembers[3].Name);
            Assert.Equal("Jul", bottomLevelMembers[6].Name);
            Assert.Equal("Oct", bottomLevelMembers[9].Name);

            Assert.All(bottomLevelMembers, member =>
            {
                Assert.Equal(0, member.LevelNumber);
                Assert.Equal("Year", member.DimensionName);
            });
        }
    }
}
