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

        [Fact(DisplayName = "FeatureSupportTests - SupportsAsync_WithFlagsReportedTrue_AnswersSupported")]
        public async Task SupportsAsync_WithFlagsReportedTrue_AnswersSupported()
        {
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(AISpecification, instance: @"{""aiEnabled"":true,""aiMdxEnabled"":true}") };

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.True(await server.SupportsAsync(EssFeature.AI, TestContext.Current.CancellationToken));
            Assert.True(await server.SupportsAsync(EssFeature.AIMdxGeneration, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "FeatureSupportTests - SupportsAsync_WithMasterSwitchOff_AnswersNotSupported")]
        public async Task SupportsAsync_WithMasterSwitchOff_AnswersNotSupported()
        {
            // The capability's own flag is on, but AI as a whole is off.
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(AISpecification, instance: @"{""aiEnabled"":false,""aiMdxEnabled"":true}") };

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.False(await server.SupportsAsync(EssFeature.AI, TestContext.Current.CancellationToken));
            Assert.False(await server.SupportsAsync(EssFeature.AIMdxGeneration, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "FeatureSupportTests - SupportsAsync_WithFlagNotReported_AnswersNotSupported")]
        public async Task SupportsAsync_WithFlagNotReported_AnswersNotSupported()
        {
            // The server declares the operation, but does not report the capability's flag at all.
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(AISpecification, instance: @"{""aiEnabled"":true}") };

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.True(await server.SupportsAsync(EssFeature.AI, TestContext.Current.CancellationToken));
            Assert.False(await server.SupportsAsync(EssFeature.AIMdxGeneration, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "FeatureSupportTests - SupportsAsync_WithFlagReportedUnderItsOlderName_AnswersSupported")]
        public async Task SupportsAsync_WithFlagReportedUnderItsOlderName_AnswersSupported()
        {
            // Essbase 21 reports one provisioning flag, which Essbase 26 replaced with one for each kind of provisioning.
            var specification = Specification(("put", "/permissions/{id}"), ("put", "/applications/{app}/permissions/{id}"));

            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(specification, instance: @"{""provisioningSupported"":true,""resetPasswordSupported"":false}") };

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.True(await server.SupportsAsync(EssFeature.ServiceRoleProvisioning, TestContext.Current.CancellationToken));
            Assert.True(await server.SupportsAsync(EssFeature.ApplicationRoleProvisioning, TestContext.Current.CancellationToken));
            Assert.False(await server.SupportsAsync(EssFeature.PasswordReset, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "FeatureSupportTests - SupportsAsync_WithoutInstanceDetails_DecidesFromTheSpecification")]
        public async Task SupportsAsync_WithoutInstanceDetails_DecidesFromTheSpecification()
        {
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(AISpecification) };

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.True(await server.SupportsAsync(EssFeature.AIMdxGeneration, TestContext.Current.CancellationToken));
            Assert.False(await server.SupportsAsync(EssFeature.AICalcGeneration, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "FeatureSupportTests - SupportsAsync_ForAFeatureWithoutOperations_DecidesFromItsFlags")]
        public async Task SupportsAsync_ForAFeatureWithoutOperations_DecidesFromItsFlags()
        {
            // Ask Essbase has no operation of its own, so it follows its flags even without a specification.
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(specification: null, instance: @"{""aiEnabled"":true,""aiAskEssbaseEnabled"":false,""aiCalcEnabled"":true}") };

            var server = new EssServer(stub.BasePath, "admin", "password");

            Assert.False(await server.SupportsAsync(EssFeature.AIAskEssbase, TestContext.Current.CancellationToken));
            Assert.True(await server.SupportsAsync(EssFeature.AICalcGeneration, TestContext.Current.CancellationToken));
        }

        [Fact(DisplayName = "FeatureSupportTests - SupportsAsync_ForSeveralFeatures_ReadsTheInstanceDetailsOnce")]
        public async Task SupportsAsync_ForSeveralFeatures_ReadsTheInstanceDetailsOnce()
        {
            using var stub = new LoopbackEssbaseStub() { ResponseProvider = Respond(AISpecification, instance: @"{""aiEnabled"":true,""aiMdxEnabled"":true}") };

            var server = new EssServer(stub.BasePath, "admin", "password");

            foreach (var feature in new[] { EssFeature.AI, EssFeature.AIMdxGeneration, EssFeature.AICalcGeneration, EssFeature.PasswordReset })
                await server.SupportsAsync(feature, TestContext.Current.CancellationToken);

            Assert.Single(stub.Requests, request => request.Contains("/about/instance"));
        }

        /// <summary>
        /// A specification that declares the AI connection and MDX generation operations, but not calculation generation.
        /// </summary>
        private static string AISpecification => Specification(
            ("get",  "/ai/connection"),
            ("post", "/ai/applications/{applicationName}/databases/{databaseName}/mdxgenerator"));

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
        /// Returns a stub response provider for a server that serves the given specification (or none, when null) and instance
        /// details (or none, when null), has the Sample.Basic cube with one drillthrough report, and answers an execution of
        /// that report with the given status code.
        /// </summary>
        /// <param name="specification">The specification the server serves, or null when it serves none.</param>
        /// <param name="executeStatusCode">The status code the server answers an execution of the report with.</param>
        /// <param name="instance">The instance details the server reports, or null when it does not answer for them.</param>
        private static Func<string, (HttpStatusCode StatusCode, string Body)?> Respond( string specification, HttpStatusCode executeStatusCode = HttpStatusCode.OK, string instance = null ) => requestLine =>
        {
            var parts  = requestLine.Split(' ');
            var method = parts.ElementAtOrDefault(0);
            var path   = parts.ElementAtOrDefault(1)?.Split('?')[0] ?? string.Empty;

            return path switch
            {
                _ when path.EndsWith("/openapi.json")                                   => specification is null ? (HttpStatusCode.NotFound, @"{""message"":""Not Found""}") : (HttpStatusCode.OK, specification),
                _ when path.EndsWith("/about/instance")                                 => instance is null ? (HttpStatusCode.NotFound, @"{""message"":""Not Found""}") : (HttpStatusCode.OK, instance),
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
