using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of the values a drill-through report returns, against a loopback stub that scripts the server's
    /// responses: the column types, the column names, then the data, every cell a string.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class DrillthroughReportDateTests
    {
        /// <summary>
        /// The server's rows for a report: TRANSDATE is a date with a time, WHEN a date alone, AMOUNT a double, NOTE text that
        /// merely starts like a date, and EMPTY null.
        /// </summary>
        private const string Report = @"[[""STRING"",""STRING"",""DOUBLE"",""STRING"",""STRING""],[""TRANSDATE"",""WHEN"",""AMOUNT"",""NOTE"",""EMPTY""]," +
                                      @"[""2025-01-03 00:00:00"",""2025-01-03"",""224.64"",""2025-01-03 or so"",null]," +
                                      @"[""2025-01-13 10:20:30.123456789"",""2025-02-01"",""9.36"",""x"",null]]";

        [Fact(DisplayName = "DrillthroughReportDateTests - ExecuteAsync_PrefixesStringValuesForExcelWithoutTypedValues")]
        public async Task ExecuteAsync_PrefixesStringValuesForExcelWithoutTypedValues()
        {
            using var stub = Stub(Report);

            var (report, _) = await ExecuteAsync(stub, new EssDrillthroughOptions(returnTypedValues: false, prefixStringValuesForExcel: true));

            // Every string column is prefixed, and the double column is plain text (#69).
            Assert.Equal("'2025-01-03 00:00:00", report[1, 0]);
            Assert.Equal("'2025-01-03 or so", report[1, 3]);
            Assert.Equal("224.64", report[1, 2]);
            Assert.Equal("TRANSDATE", report[0, 0]);
        }

        [Fact(DisplayName = "DrillthroughReportDateTests - ExecuteAsync_PrefixesStringValuesForExcelWithTypedValues")]
        public async Task ExecuteAsync_PrefixesStringValuesForExcelWithTypedValues()
        {
            using var stub = Stub(Report);

            var (report, _) = await ExecuteAsync(stub, new EssDrillthroughOptions(returnTypedValues: true, prefixStringValuesForExcel: true));

            // The string columns keep their prefix beside the typed double column (#69).
            Assert.Equal("'2025-01-03 or so", report[1, 3]);
            Assert.Equal(224.64, report[1, 2]);
        }

        /// <summary>
        /// Returns a stub that answers the application, cube, and report list requests, and the report's execution with the
        /// given rows.
        /// </summary>
        /// <param name="reportJson">The rows the server returns for the report.</param>
        private static LoopbackEssbaseStub Stub( string reportJson ) => new LoopbackEssbaseStub()
        {
            ResponseProvider = requestLine => requestLine.Split(' ') switch
            {
                [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample")                                    => (HttpStatusCode.OK, @"{""name"":""Sample""}"),
                [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample/databases/Basic")                    => (HttpStatusCode.OK, @"{""name"":""Basic""}"),
                [ "GET",  var path, .. ] when path.Contains("/applications/Sample/databases/Basic/reports")            => (HttpStatusCode.OK, @"{""items"":[{""name"":""drillthrough_samplebasic""}]}"),
                [ "POST", var path, .. ] when path.EndsWith("/applications/Sample/databases/Basic/reports/drillthrough_samplebasic") => (HttpStatusCode.OK, reportJson),
                _                                                                                                       => null
            }
        };

        /// <summary>
        /// Executes Sample.Basic's report on the given stub for one cell, with the given options.
        /// </summary>
        /// <param name="stub">The stub.</param>
        /// <param name="options">The options.</param>
        private static async Task<(object[,] report, string[] columnTypes)> ExecuteAsync( LoopbackEssbaseStub stub, EssDrillthroughOptions options )
        {
            var cube = await new EssServer(stub.BasePath, "admin", "password").GetApplicationAsync("Sample", TestContext.Current.CancellationToken).GetCubeAsync("Basic", TestContext.Current.CancellationToken);
            var report = await cube.GetDrillthroughReportAsync("drillthrough_samplebasic", cancellationToken: TestContext.Current.CancellationToken);

            return await report.ExecuteAsync(new EssDrillthroughRange(dimensionMemberSets: new() { ["Year"] = new() { "Jan", "Jan" } }), options, TestContext.Current.CancellationToken);
        }
    }
}
