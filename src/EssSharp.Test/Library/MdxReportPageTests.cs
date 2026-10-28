using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of an MDX report's page axis, against a loopback stub that scripts the server's response. Essbase gives
    /// each page dimension as an object with its name and its point-of-view member (pov), the member the query's slicer chose.
    /// The report's metadata lists the page dimensions by name, and its page row shows their pov members, as the Essbase grid
    /// does (#71).
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class MdxReportPageTests
    {
        /// <summary>
        /// Essbase 26's response to a query of Jan and Feb by Sales and COGS whose slicer is a calculated SelectedMarkets, Colas
        /// (by its alias), and Actual.
        /// </summary>
        private const string Response =
            @"{""metadata"":{""page"":[{""id"":0,""name"":""Market"",""pov"":""SelectedMarkets"",""expand"":""0""}," +
            @"{""id"":1,""name"":""Product"",""pov"":""Colas"",""expand"":""0""},{""id"":2,""name"":""Scenario"",""pov"":""Actual"",""expand"":""0""}]," +
            @"""column"":[""Year""],""row"":[""Measures""]}," +
            @"""data"":[["""",""Jan"",""Feb""],[""Sales"",""2636.0"",""2554.0""],[""COGS"",""1063.0"",""1059.0""]]}";

        [Fact(DisplayName = "MdxReportPageTests - GetReportAsync_ShowsEachPageDimensionsPovMemberInThePageRow")]
        public async Task GetReportAsync_ShowsEachPageDimensionsPovMemberInThePageRow()
        {
            using var stub = Stub(Response);

            var report = await GetReportAsync(stub);

            Assert.Equal(new object[] { "", "SelectedMarkets", "Colas", "Actual" }, Row(report.Data, 0));
            Assert.Equal(new object[] { "", "Jan", "Feb", "" }, Row(report.Data, 1));
        }

        [Fact(DisplayName = "MdxReportPageTests - GetReportAsync_ListsThePageDimensionsByName")]
        public async Task GetReportAsync_ListsThePageDimensionsByName()
        {
            using var stub = Stub(Response);

            var report = await GetReportAsync(stub);

            Assert.Equal(new[] { "Market", "Product", "Scenario" }, report.Metadata.PageDimensionMembers);
        }

        [Fact(DisplayName = "MdxReportPageTests - GetReportAsync_ShowsAPageDimensionsNameWhenItHasNoPov")]
        public async Task GetReportAsync_ShowsAPageDimensionsNameWhenItHasNoPov()
        {
            // A response from before 21.8, whose page field lists the page dimensions by name alone.
            using var stub = Stub(@"{""metadata"":{""page"":[""Market"",""Product""],""column"":[""Year""],""row"":[""Measures""]}," +
                                  @"""data"":[["""",""Jan""],[""Sales"",""2636.0""]]}");

            var report = await GetReportAsync(stub);

            Assert.Equal(new object[] { "", "Market", "Product" }, Row(report.Data, 0));
            Assert.Equal(new[] { "Market", "Product" }, report.Metadata.PageDimensionMembers);
        }

        /// <summary>Returns a stub that answers the application and cube requests, and the MDX query with the given response.</summary>
        /// <param name="mdxJson">The server's response to the query.</param>
        private static LoopbackEssbaseStub Stub( string mdxJson ) => new LoopbackEssbaseStub()
        {
            ResponseProvider = requestLine => requestLine.Split(' ') switch
            {
                [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample")                     => (HttpStatusCode.OK, @"{""name"":""Sample""}"),
                [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample/databases/Basic")     => (HttpStatusCode.OK, @"{""name"":""Basic""}"),
                [ "POST", var path, .. ] when path.EndsWith("/applications/Sample/databases/Basic/mdx") => (HttpStatusCode.OK, mdxJson),
                _                                                                                        => null
            }
        };

        /// <summary>Runs an MDX query against Sample.Basic on the given stub and returns its report with every axis.</summary>
        /// <param name="stub">The stub.</param>
        private static async Task<EssQueryReport> GetReportAsync( LoopbackEssbaseStub stub )
        {
            var cube = await new EssServer(stub.BasePath, "admin", "password").GetApplicationAsync("Sample", TestContext.Current.CancellationToken).GetCubeAsync("Basic", TestContext.Current.CancellationToken);
            var script = await cube.CreateScriptAsync<IEssMdxScript>("query", "SELECT {} ON COLUMNS FROM Sample.Basic", saveToCube: false, cancellationToken: TestContext.Current.CancellationToken);

            return await script.GetReportAsync(new EssQueryPreferences() { Axes = EssQueryReport.ReportAxes.ColumnsRowsAndPages }, TestContext.Current.CancellationToken);
        }

        /// <summary>Returns the given row of a report's data.</summary>
        private static object[] Row( object[,] data, int row ) => Enumerable.Range(0, data.GetLength(1)).Select(column => data[row, column]).ToArray();
    }
}
