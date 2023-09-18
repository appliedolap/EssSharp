using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;
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
    }
}
