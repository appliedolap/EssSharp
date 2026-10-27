using System;
using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of the values a drill-through report returns, against a loopback stub that scripts the server's
    /// responses: the column types, the column names, then the data, every cell a string. Essbase types a date column as a
    /// string and its text is the JDBC driver's, in the escape form the JDBC specification fixes, so with typed values on a
    /// string column that is dates throughout comes back as dates (#68).
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

        [Fact(DisplayName = "DrillthroughReportDateTests - ExecuteAsync_ReturnsAColumnOfDriverDatesAsDates")]
        public async Task ExecuteAsync_ReturnsAColumnOfDriverDatesAsDates()
        {
            using var stub = Stub(Report);

            var (report, columnTypes) = await ExecuteAsync(stub, new EssDrillthroughOptions(returnTypedValues: true));

            Assert.Equal(new DateTime(2025, 1, 3), report[1, 0]);
            Assert.Equal(new DateTime(2025, 1, 13, 10, 20, 30).AddTicks(1234567), report[2, 0]);
            Assert.Equal(new DateTime(2025, 1, 3), report[1, 1]);
            Assert.Equal(new DateTime(2025, 2, 1), report[2, 1]);

            // The headings stay text, a double column is doubles, a column with other text stays text, and empties stay empty.
            Assert.Equal("TRANSDATE", report[0, 0]);
            Assert.Equal(224.64, report[1, 2]);
            Assert.Equal("2025-01-03 or so", report[1, 3]);
            Assert.Equal("x", report[2, 3]);
            Assert.Null(report[1, 4]);

            // The column types are the server's, untouched.
            Assert.Equal(new[] { "STRING", "STRING", "DOUBLE", "STRING", "STRING" }, columnTypes);
        }

        [Fact(DisplayName = "DrillthroughReportDateTests - ExecuteAsync_KeepsDatesAsTextWithoutTypedValues")]
        public async Task ExecuteAsync_KeepsDatesAsTextWithoutTypedValues()
        {
            using var stub = Stub(Report);

            var (report, _) = await ExecuteAsync(stub, new EssDrillthroughOptions(returnTypedValues: false));

            Assert.Equal("2025-01-03 00:00:00", report[1, 0]);
            Assert.Equal("2025-01-03", report[1, 1]);
            Assert.Equal("224.64", report[1, 2]);
        }

        [Fact(DisplayName = "DrillthroughReportDateTests - ExecuteAsync_ReturnsDatesWithoutTheExcelPrefix")]
        public async Task ExecuteAsync_ReturnsDatesWithoutTheExcelPrefix()
        {
            using var stub = Stub(Report);

            var (report, _) = await ExecuteAsync(stub, new EssDrillthroughOptions(returnTypedValues: true, prefixStringValuesForExcel: true));

            // A date needs no apostrophe to keep Excel from reading it; the text column keeps its prefix.
            Assert.Equal(new DateTime(2025, 1, 3), report[1, 0]);
            Assert.Equal(new DateTime(2025, 2, 1), report[2, 1]);
            Assert.Equal("'2025-01-03 or so", report[1, 3]);
            Assert.Equal(224.64, report[1, 2]);
        }

        [Fact(DisplayName = "DrillthroughReportDateTests - ExecuteAsync_PrefixesStringValuesForExcelWithoutTypedValues")]
        public async Task ExecuteAsync_PrefixesStringValuesForExcelWithoutTypedValues()
        {
            using var stub = Stub(Report);

            var (report, _) = await ExecuteAsync(stub, new EssDrillthroughOptions(returnTypedValues: false, prefixStringValuesForExcel: true));

            // Every string column is prefixed, dates included since nothing is typed, and the double column is plain text (#69).
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

        [Fact(DisplayName = "DrillthroughReportDateTests - ExecuteAsync_KeepsAColumnAsTextWhenOneValueIsNotADate")]
        public async Task ExecuteAsync_KeepsAColumnAsTextWhenOneValueIsNotADate()
        {
            using var stub = Stub(@"[[""STRING"",""STRING""],[""WHEN"",""LABEL""],[""2025-01-03"",""a""],[""n/a"",""b""],[""2025-01-05"",""c""]]");

            var (report, _) = await ExecuteAsync(stub, new EssDrillthroughOptions(returnTypedValues: true));

            Assert.Equal("2025-01-03", report[1, 0]);
            Assert.Equal("n/a", report[2, 0]);
            Assert.Equal("2025-01-05", report[3, 0]);
        }

        [Fact(DisplayName = "DrillthroughReportDateTests - ExecuteAsync_KeepsEmptyCellsEmptyInADateColumn")]
        public async Task ExecuteAsync_KeepsEmptyCellsEmptyInADateColumn()
        {
            using var stub = Stub(@"[[""STRING""],[""WHEN""],[""2025-01-03""],[null],[""""],[""2025-01-05 23:59:59""]]");

            var (report, _) = await ExecuteAsync(stub, new EssDrillthroughOptions(returnTypedValues: true, prefixStringValuesForExcel: true));

            Assert.Equal(new DateTime(2025, 1, 3), report[1, 0]);
            Assert.True(report[2, 0] is null or "", "A null cell is not a date.");
            Assert.True(report[3, 0] is null or "", "An empty cell is not a date.");
            Assert.Equal(new DateTime(2025, 1, 5, 23, 59, 59), report[4, 0]);
        }

        [Fact(DisplayName = "DrillthroughReportDateTests - ExecuteAsync_LeavesAColumnWithNoTextAlone")]
        public async Task ExecuteAsync_LeavesAColumnWithNoTextAlone()
        {
            using var stub = Stub(@"[[""STRING""],[""WHEN""],[null],[""""]]");

            var (report, _) = await ExecuteAsync(stub, new EssDrillthroughOptions(returnTypedValues: true));

            Assert.True(report[1, 0] is null or "");
            Assert.True(report[2, 0] is null or "");
        }

        [Theory(DisplayName = "DrillthroughReportDateTests - ExecuteAsync_ReadsOnlyTheDriversForm")]
        [InlineData("2025-1-3")]
        [InlineData("01/03/2025")]
        [InlineData("2025-01-03 00:00")]
        [InlineData("2025-01-03 00:00:00.")]
        [InlineData("2025-01-03 00:00:00.1234567890")]
        [InlineData("2025-13-03")]
        [InlineData("20250103")]
        public async Task ExecuteAsync_ReadsOnlyTheDriversForm( string text )
        {
            using var stub = Stub(@"[[""STRING""],[""WHEN""],[""" + text + @"""]]");

            var (report, _) = await ExecuteAsync(stub, new EssDrillthroughOptions(returnTypedValues: true));

            Assert.Equal(text, report[1, 0]);
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
