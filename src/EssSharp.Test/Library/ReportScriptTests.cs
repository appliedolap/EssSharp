using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of report script results against a loopback stub that scripts the server's responses.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class ReportScriptTests
    {
        [Fact(DisplayName = "ReportScriptTests - ExecuteReportQueryAsync_WithCellTypes_TypesMembersDataAndBlanksAsAGridDoes")]
        public async Task ExecuteReportQueryAsync_WithCellTypes_TypesMembersDataAndBlanksAsAGridDoes()
        {
            // The report: a blank corner, quoted column and row members, and data, including a formatted number and #Missing.
            const string reportOutput = "\t\"Qtr1\"\t\"Qtr2\"\n\"Cola\"\t2,479\t#Missing\n";

            using var stub = new LoopbackEssbaseStub()
            {
                ResponseProvider = requestLine => requestLine.Split(' ') switch
                {
                    [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample")                 => (HttpStatusCode.OK, @"{""name"":""Sample""}"),
                    [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample/databases/Basic") => (HttpStatusCode.OK, @"{""name"":""Basic""}"),
                    [ "GET",  var path, .. ] when path.Contains("/scripts")                             => (HttpStatusCode.OK, @"{""items"":[]}"),
                    [ "POST", var path, .. ] when path.EndsWith("/jobs")                                => (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":100}"),
                    [ "GET",  var path, .. ] when path.EndsWith("/jobs/7")                              => (HttpStatusCode.OK, $@"{{""job_ID"":7,""statusCode"":200,""jobOutputInfo"":{{""REPORT_OUTPUT"":{System.Text.Json.JsonSerializer.Serialize(reportOutput)}}}}}"),
                    _                                                                                   => null
                }
            };

            var server = new EssServer(stub.BasePath, "admin", "password");
            var cube   = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken).GetCubeAsync("Basic", TestContext.Current.CancellationToken);

            var report = await cube.ExecuteReportQueryAsync(@"<ROW(""Product"") <COL(""Year"") !", new EssQueryPreferences() { CaptureCellTypes = true }, TestContext.Current.CancellationToken);

            // The cell types of an Essbase grid: 0 for a member, 2 for data, and 7 for a blank cell.
            Assert.Equal(new[,] { { 7, 0, 0 }, { 0, 2, 2 } }, report.Types);
            Assert.Equal(new object[,] { { "", "Qtr1", "Qtr2" }, { "Cola", "2,479", "#Missing" } }, report.Data);
        }
    }
}
