using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of <see cref="EssJobCalcScriptOptions"/>: what a calc job sends the server for the runtime
    /// substitution variables it is given, against a loopback stub that records the requests.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class CalcScriptOptionsTests
    {
        [Fact(DisplayName = "CalcScriptOptionsTests - ExecuteAsync_WithRuntimeSubstitutionVariables_SendsEachAsTheJobsRtsv")]
        public async Task ExecuteAsync_WithRuntimeSubstitutionVariables_SendsEachAsTheJobsRtsv()
        {
            var body = await PostedJobAsync(new EssJobCalcScriptOptions("CalcProduct.csc", "Sample", "Basic")
            {
                RuntimeSubstitutionVariables =
                {
                    ["CalcProduct"] = @"""100-20""",
                    ["CalcMarkets"] = @"""New York"",""California"""
                }
            });

            Assert.Equal("calc", (string)body["jobtype"]);
            Assert.Equal("CalcProduct.csc", (string)body["parameters"]["script"]);
            Assert.Equal(new[] { ("CalcProduct", @"""100-20"""), ("CalcMarkets", @"""New York"",""California""") },
                         body["parameters"]["rtsv"].Select(rtsv => ((string)rtsv["name"], (string)rtsv["value"])));
        }

        [Fact(DisplayName = "CalcScriptOptionsTests - ExecuteAsync_WithoutRuntimeSubstitutionVariables_SendsNoRtsv")]
        public async Task ExecuteAsync_WithoutRuntimeSubstitutionVariables_SendsNoRtsv()
        {
            var body = await PostedJobAsync(new EssJobCalcScriptOptions("CalcAll.csc", "Sample", "Basic"));

            Assert.Equal("CalcAll.csc", (string)body["parameters"]["script"]);
            Assert.Null(body["parameters"]["rtsv"]);
        }

        [Fact(DisplayName = "CalcScriptOptionsTests - Constructor_WithAScriptThatIsNotACalcScript_Throws")]
        public void Constructor_WithAScriptThatIsNotACalcScript_Throws()
        {
            Assert.Throws<ArgumentException>(() => new EssJobCalcScriptOptions("test.mdx", "Sample", "Basic"));
        }

        /// <summary>
        /// Executes a job with the given options against a stub that completes it at once, and returns the JSON body the job
        /// was posted with.
        /// </summary>
        private static async Task<JObject> PostedJobAsync( EssJobCalcScriptOptions options )
        {
            using var stub = new LoopbackEssbaseStub()
            {
                ResponseProvider = requestLine => requestLine.Split(' ') switch
                {
                    [ "POST", var path, .. ] when path.EndsWith("/jobs")   => (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":200}"),
                    [ "GET",  var path, .. ] when path.EndsWith("/jobs/7") => (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":200}"),
                    _                                                     => null
                }
            };

            var server = new EssServer(stub.BasePath, "admin", "password");

            await server.CreateJob(options).ExecuteAsync(TestContext.Current.CancellationToken);

            var request = stub.Requests.Single(r => r.StartsWith("POST ") && r.Split(' ')[1].EndsWith("/jobs"));

            return JObject.Parse(request.Substring(request.IndexOf("\r\n\r\n", StringComparison.Ordinal) + 4));
        }
    }
}
