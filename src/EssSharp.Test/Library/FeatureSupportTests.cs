using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of <see cref="IEssServer.SupportsAsync(EssFeature, System.Threading.CancellationToken)"/> and of
    /// the members that need a feature, against a loopback stub that scripts the server's responses.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class FeatureSupportTests
    {
        private const string _reportsPath = "/applications/Sample/databases/Basic/reports";

        [Fact(DisplayName = "FeatureSupportTests - SupportsAsync_WithServedSpecification_AnswersFromTheDeclaredOperations")]
        public async Task SupportsAsync_WithServedSpecification_AnswersFromTheDeclaredOperations()
        {
            // Declare report execution under parameter names that differ from the ones EssSharp uses, and no lookup by cells.
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(Specification(("post", "/applications/{app}/databases/{cube}/reports/{report}"))) };

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.True(await server.SupportsAsync(EssFeature.DrillthroughReportExecution, TestContext.Current.CancellationToken));
            Assert.False(await server.SupportsAsync(EssFeature.DrillthroughReportsForCells, TestContext.Current.CancellationToken));

            // Assert that the specification was read only once.
            Assert.Single(stub.Requests, request => request.Contains("/openapi.json"));
        }

        [Fact(DisplayName = "FeatureSupportTests - SupportsAsync_WithoutServedSpecification_ReportsSupported")]
        public async Task SupportsAsync_WithoutServedSpecification_ReportsSupported()
        {
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(specification: null) };

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.True(await server.SupportsAsync(EssFeature.DrillthroughReportExecution, TestContext.Current.CancellationToken));
            Assert.True(await server.SupportsAsync(EssFeature.DrillthroughReportsForCells, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "FeatureSupportTests - ExecuteAsync_WithoutServedSpecification_TranslatesTheFailureAndRemembersIt")]
        public async Task ExecuteAsync_WithoutServedSpecification_TranslatesTheFailureAndRemembersIt()
        {
            // A server without report execution answers its POST with a 405 (method not allowed).
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(specification: null, executeStatusCode: HttpStatusCode.MethodNotAllowed) };

            var server = new EssServer(stub.BasePath, "admin", "password");

            var exception = await Assert.ThrowsAsync<NotSupportedException>(() => ExecuteReportAsync(server));

            Assert.True(exception.InnerException is WebException { Response: EssSharp.Api.WebExceptionRestResponse { StatusCode: HttpStatusCode.MethodNotAllowed } });
            Assert.False(await server.SupportsAsync(EssFeature.DrillthroughReportExecution, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "FeatureSupportTests - ExecuteAsync_WithServedSpecificationDeclaringTheOperation_DoesNotTranslateTheFailure")]
        public async Task ExecuteAsync_WithServedSpecificationDeclaringTheOperation_DoesNotTranslateTheFailure()
        {
            // The server declares report execution, so the failure is not a missing operation, even with the status code that
            // a server without the operation answers with.
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(Specification(("post", "/applications/{applicationName}/databases/{databaseName}/reports/{name}")), HttpStatusCode.MethodNotAllowed) };

            var server = new EssServer(stub.BasePath, "admin", "password");

            var exception = await Assert.ThrowsAnyAsync<Exception>(() => ExecuteReportAsync(server));

            Assert.IsNotType<NotSupportedException>(exception);
            Assert.True(await server.SupportsAsync(EssFeature.DrillthroughReportExecution, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "FeatureSupportTests - ExecuteAsync_WithServedSpecification_TranslatesAnUndeclaredOperation")]
        public async Task ExecuteAsync_WithServedSpecification_TranslatesAnUndeclaredOperation()
        {
            // The server declares the report path, but not its POST, as Essbase 21.3 and earlier do.
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(Specification(("get", "/applications/{applicationName}/databases/{databaseName}/reports/{name}")), HttpStatusCode.MethodNotAllowed) };

            var server = new EssServer(stub.BasePath, "admin", "password");

            await Assert.ThrowsAsync<NotSupportedException>(() => ExecuteReportAsync(server));
        }

        /// <summary>
        /// Gets the Sample.Basic drillthrough report from the given server and executes it.
        /// </summary>
        /// <param name="server">The server.</param>
        private static async Task ExecuteReportAsync( IEssServer server )
        {
            var application = await server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken);
            var cube        = await application.GetCubeAsync("Basic", TestContext.Current.CancellationToken);
            var report      = (await cube.GetDrillthroughReportsAsync(false, TestContext.Current.CancellationToken)).Single();

            await report.ExecuteAsync(new EssDrillthroughRange(dimensionMemberSets: new() { ["Product"] = new() { "Cola" } }), cancellationToken: TestContext.Current.CancellationToken);
        }

        /// <summary>
        /// Returns a stub response provider for a server that serves the given specification (or none, when null), has the
        /// Sample.Basic cube with one drillthrough report, and answers an execution of that report with the given status code.
        /// </summary>
        /// <param name="specification">The specification the server serves, or null when it serves none.</param>
        /// <param name="executeStatusCode">The status code the server answers an execution of the report with.</param>
        private static Func<string, (HttpStatusCode StatusCode, string Body)?> Respond( string specification, HttpStatusCode executeStatusCode = HttpStatusCode.OK ) => requestLine =>
        {
            var parts  = requestLine.Split(' ');
            var method = parts.ElementAtOrDefault(0);
            var path   = parts.ElementAtOrDefault(1)?.Split('?')[0] ?? string.Empty;

            return path switch
            {
                _ when path.EndsWith("/openapi.json")                                   => specification is null ? (HttpStatusCode.NotFound, @"{""message"":""Not Found""}") : (HttpStatusCode.OK, specification),
                _ when path.EndsWith($"{_reportsPath}/DT1") && method == "POST"         => (executeStatusCode, executeStatusCode == HttpStatusCode.OK ? "[]" : @"{""message"":""Failed""}"),
                _ when path.EndsWith(_reportsPath)                                      => (HttpStatusCode.OK, @"{""items"":[{""name"":""DT1""}]}"),
                _ when path.EndsWith("/applications/Sample/databases/Basic")            => (HttpStatusCode.OK, @"{""name"":""Basic"",""application"":""Sample""}"),
                _ when path.EndsWith("/applications/Sample")                            => (HttpStatusCode.OK, @"{""name"":""Sample""}"),
                _                                                                       => null
            };
        };

        /// <summary>
        /// Returns a minimal specification that declares the given operations.
        /// </summary>
        /// <param name="operations">The operations, as an HTTP method and a path template.</param>
        private static string Specification( params (string Method, string Path)[] operations ) =>
            "{\"openapi\":\"3.0.1\",\"paths\":{" +
            string.Join(",", operations.GroupBy(operation => operation.Path).Select(path => $"\"{path.Key}\":{{{string.Join(",", path.Select(operation => $"\"{operation.Method}\":{{}}"))}}}")) +
            "}}";
    }
}
