using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />
    /// </summary>
    [CollectionDefinition(nameof(DrillthroughUrlReportTests), DisableParallelization = true), CollectionPriority(10)]
    public class DrillthroughUrlReportTestsCollection : ICollectionFixture<CollectionFixture> { }

    /// <summary>
    /// URL drill-through reports, such as Drillbridge and FDMEE deploy, against Essbase: a URL report on Sample.Basic over
    /// Actual is listed with its type and URL, for the cube and for its cells, to a user who may read its definition, and
    /// its definition is refused to a user who may only read the database. Clients such as Dodeca read a report's type and
    /// URL this way over REST, to open the report's page with the cell's members filled in. The tests define the report
    /// themselves, over REST, and delete it at the end.
    /// </summary>
    [Collection(nameof(DrillthroughUrlReportTests)), Trait("type", "execute")]
    public class DrillthroughUrlReportTests : IntegrationTestBase
    {
        /// <summary />
        /// <param name="output" />
        public DrillthroughUrlReportTests( ITestOutputHelper output ) : base(output) { }

        private const string ApplicationName = @"Sample";
        private const string CubeName        = @"Basic";
        private const string ReportName      = @"esssharp_url_drillthrough";

        /// <summary>
        /// The report's URL: Drillbridge's <c>$ATTR(...)$</c> token and FDMEE's <c>$$&lt;dimension&gt;-VALUE$$</c> form. The
        /// tests never open it, so it need not answer.
        /// </summary>
        private const string ReportUrl = @"http://localhost/drill/esssharp?$ATTR(ds,id,pos,gen,level.edge)$&year=$$Year-VALUE$$&market=$$Market-VALUE$$";

        /// <summary>
        /// New York's Sales of Cola in Year, within Actual, where the report drills.
        /// </summary>
        private static List<EssDrillthroughRange> CellWithinActual => Cell(@"Actual");

        [Fact(DisplayName = @"DrillthroughUrlReportTests - 01 - Essbase_UrlReport_IsListedWithItsTypeAndUrl"), Priority(01)]
        public async Task Essbase_UrlReport_IsListedWithItsTypeAndUrl()
        {
            await EnsureReportAsync();

            var cube = await GetCubeAsync(GetEssServer());

            var report = Assert.Single(await cube.GetDrillthroughReportsAsync(getDetails: true, TestContext.Current.CancellationToken), candidate => candidate.Name == ReportName);

            Assert.Equal(@"URL", report.Details?.Type, ignoreCase: true);
            Assert.Equal(ReportUrl, report.Details?.Url);
        }

        [Fact(DisplayName = @"DrillthroughUrlReportTests - 02 - Essbase_UrlReport_IsListedForACellWithinItsRegion_WithItsUrl"), Priority(02)]
        public async Task Essbase_UrlReport_IsListedForACellWithinItsRegion_WithItsUrl()
        {
            var server = GetEssServer();

            Assert.SkipUnless(await server.SupportsAsync(EssFeature.DrillthroughReportsForCells, TestContext.Current.CancellationToken), @"The server cannot list the drill-through reports of given cells (Essbase 21.7 and later can).");

            await EnsureReportAsync();

            var grid = await (await GetCubeAsync(server)).GetDefaultGridAsync(cancellationToken: TestContext.Current.CancellationToken);

            var report = Assert.Single(await grid.GetDrillThroughReportForCellsAsync(CellWithinActual, getDetails: true, TestContext.Current.CancellationToken), candidate => candidate?.Name == ReportName);

            Assert.Equal(@"URL", report.Details?.Type, ignoreCase: true);
            Assert.Equal(ReportUrl, report.Details?.Url);

            // Outside the report's region, within Budget, it is not listed.
            Assert.DoesNotContain(await grid.GetDrillThroughReportForCellsAsync(Cell(@"Budget"), getDetails: false, TestContext.Current.CancellationToken), candidate => candidate?.Name == ReportName);
        }

        [Fact(DisplayName = @"DrillthroughUrlReportTests - 03 - Essbase_UrlReportDefinition_IsRefusedToAUserWithDatabaseAccess"), Priority(03)]
        public async Task Essbase_UrlReportDefinition_IsRefusedToAUserWithDatabaseAccess()
        {
            var admin = GetEssServer();

            Assert.SkipUnless(await admin.SupportsAsync(EssFeature.DrillthroughReportsForCells, TestContext.Current.CancellationToken), @"The server cannot list the drill-through reports of given cells (Essbase 21.7 and later can).");

            await EnsureReportAsync();

            // The user is given database access to Sample by CreateServerObjectTests; without it there is nothing to show here.
            var grid = default(IEssGrid);
            var listed = default(List<IEssDrillthroughReport>);

            try
            {
                grid   = await (await GetCubeAsync(GetEssServer(EssServerRole.User))).GetDefaultGridAsync(cancellationToken: TestContext.Current.CancellationToken);
                listed = await grid.GetDrillThroughReportForCellsAsync(CellWithinActual, getDetails: false, TestContext.Current.CancellationToken);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                Assert.Skip($@"The {EssServerRole.User} connection cannot read Sample.Basic, which CreateServerObjectTests gives it access to: {e.Message}");
            }

            // The user is told the report's name for the cell...
            Assert.Contains(listed, candidate => candidate?.Name == ReportName);

            // ...but not its definition, which holds its type and URL: Essbase allows that to a database manager only.
            var refused = await Assert.ThrowsAnyAsync<Exception>(() => grid.GetDrillThroughReportForCellsAsync(CellWithinActual, getDetails: true, TestContext.Current.CancellationToken));

            TestContext.Current.TestOutputHelper?.WriteLine($@"Refused: {refused.Message}");
        }

        [Fact(DisplayName = @"DrillthroughUrlReportTests - 04 - Essbase_UrlReport_CanBeDeleted"), Priority(04)]
        public async Task Essbase_UrlReport_CanBeDeleted()
        {
            await EnsureReportAsync();

            var cube = await GetCubeAsync(GetEssServer());

            await (await cube.GetDrillthroughReportAsync(ReportName, getDetails: false, TestContext.Current.CancellationToken)).DeleteAsync(TestContext.Current.CancellationToken);

            Assert.DoesNotContain(await cube.GetDrillthroughReportsAsync(getDetails: false, TestContext.Current.CancellationToken), candidate => candidate.Name == ReportName);
        }

        #region Private Methods

        /// <summary>
        /// New York's Sales of Cola in Year, within the given scenario.
        /// </summary>
        private static List<EssDrillthroughRange> Cell( string scenario ) => new()
        {
            new EssDrillthroughRange(dimensionMemberSets: new()
            {
                ["Year"]     = new() { "Year" },
                ["Product"]  = new() { "Cola" },
                ["Measures"] = new() { "Sales" },
                ["Market"]   = new() { "New York" },
                ["Scenario"] = new() { scenario }
            })
        };

        private static async Task<IEssCube> GetCubeAsync( IEssServer server ) =>
            await server.GetApplicationAsync(ApplicationName, TestContext.Current.CancellationToken).GetCubeAsync(CubeName, TestContext.Current.CancellationToken);

        /// <summary>
        /// Defines the report on Sample.Basic over Actual, or updates its URL, through the Essbase REST API as the service
        /// administrator: EssSharp reads drill-through definitions but does not write them.
        /// </summary>
        private async Task EnsureReportAsync()
        {
            var connection = GetEssConnection();

            using var client = new HttpClient() { BaseAddress = new Uri($@"{connection.Server.TrimEnd('/')}/rest/v1/applications/{ApplicationName}/databases/{CubeName}/") };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));
            client.DefaultRequestHeaders.Authorization = connection.AccessToken is { Length: > 0 } token
                ? new AuthenticationHeaderValue(@"Bearer", token)
                : new AuthenticationHeaderValue(@"Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($@"{connection.Username}:{connection.Password}")));

            var definition = new StringContent(JsonSerializer.Serialize(new { name = ReportName, type = @"URL", url = ReportUrl, drillableRegions = new[] { @"Actual" } }), Encoding.UTF8, @"application/json");

            using var existing = await client.GetAsync($@"reports/{ReportName}", TestContext.Current.CancellationToken);

            using var answer = existing.StatusCode is HttpStatusCode.OK
                ? await client.PutAsync($@"reports/{ReportName}", definition, TestContext.Current.CancellationToken)
                : await client.PostAsync(@"reports", definition, TestContext.Current.CancellationToken);

            Assert.True(answer.IsSuccessStatusCode, $@"Essbase did not save the URL drill-through report {ReportName}: HTTP {(int)answer.StatusCode} {await answer.Content.ReadAsStringAsync(TestContext.Current.CancellationToken)}");
        }

        #endregion
    }
}
