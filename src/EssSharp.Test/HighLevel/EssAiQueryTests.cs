using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.HighLevel
{
    public class EssAiQueryTests
    {
        [Fact]
        public async Task GenerateMdxFromNaturalLanguageAsync_ReturnsModeledResult()
        {
            const string mdx = "SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]";

            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, $@"{{""error"":"""",""info"":""Generated from the outline."",""mdx"":{Quote(mdx)}}}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var result = await cube.GenerateMdxFromNaturalLanguageAsync(
                "Show sales",
                "Dodeca",
                new EssAiQueryOptions(startNewConversation: true, includeAttributes: true, prompt: "Additional context"));

            Assert.Equal("Show sales", result.NaturalLanguageQuery);
            Assert.Equal("Dodeca", result.ProfileName);
            Assert.Equal(mdx, result.Mdx);
            Assert.Equal("Generated from the outline.", result.Explanation);
            Assert.Contains(stub.Requests, request => request.Contains("profileName=Dodeca", StringComparison.Ordinal));
            Assert.Contains(stub.Requests, request => request.Contains("isConvStart=true", StringComparison.OrdinalIgnoreCase));
            Assert.Contains(stub.Requests, request => request.Contains("includeAttributesInNlq=true", StringComparison.OrdinalIgnoreCase));
            Assert.Contains(stub.Requests, request => request.Contains("nlq=Show%20sales", StringComparison.OrdinalIgnoreCase));
            Assert.DoesNotContain(stub.Requests, request => request.Contains("/about", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public async Task ExecuteNaturalLanguageQueryAsync_ReturnsGeneratedQueryAndGrid()
        {
            const string mdx = "SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]";

            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, $@"{{""generatedMdx"":{Quote(mdx)}}}");

                if ( request.StartsWith("POST /essbase/rest/v1/applications/Sample/databases/Basic/grid/mdx", StringComparison.Ordinal) )
                    return (200, "{}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var result = await cube.ExecuteNaturalLanguageQueryAsync("Show sales", "Dodeca");

            Assert.Equal(mdx, result.Query.Mdx);
            Assert.NotNull(result.Grid);
            Assert.Same(cube, result.Grid.Cube);
            Assert.Contains(stub.Requests, request =>
                request.StartsWith("POST /essbase/rest/v1/applications/Sample/databases/Basic/grid/mdx", StringComparison.Ordinal) &&
                request.Contains(mdx, StringComparison.Ordinal));
        }

        [Theory]
        [InlineData(null, "Dodeca")]
        [InlineData("", "Dodeca")]
        [InlineData("Show sales", null)]
        [InlineData("Show sales", " ")]
        public async Task ExecuteNaturalLanguageQueryAsync_RejectsMissingArgumentsWithoutWrappingThem( string query, string profileName )
        {
            using var stub = new AiQueryEssbaseStub(GetObjectResponse);

            var cube = await GetCubeAsync(stub);

            // ThrowsAsync<T> only accepts that exact type, so a wrapping Exception fails this.
            await Assert.ThrowsAsync<ArgumentException>(() => cube.ExecuteNaturalLanguageQueryAsync(query, profileName));

            Assert.DoesNotContain(stub.Requests, request => request.Contains("/ai/", StringComparison.Ordinal));
        }

        [Fact]
        public async Task ExecuteNaturalLanguageQueryAsync_ReportsTheGeneratedMdxWhenItFailsToRun()
        {
            const string mdx = "SELECT {[Measures].[Nothing]} ON COLUMNS FROM [Sample].[Basic]";

            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, $@"{{""generatedMdx"":{Quote(mdx)}}}");

                if ( request.StartsWith("POST /essbase/rest/v1/applications/Sample/databases/Basic/grid/mdx", StringComparison.Ordinal) )
                    return (400, @"{""message"":""Unknown member [Nothing]""}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                cube.ExecuteNaturalLanguageQueryAsync("Show nothing", "Dodeca"));

            Assert.Contains(mdx, exception.Message, StringComparison.Ordinal);
            Assert.Equal(mdx, exception.Data["Mdx"]);
            Assert.NotNull(exception.InnerException);
        }

        [Fact]
        public async Task GenerateMdxFromNaturalLanguageAsync_RejectsErrorResponseWithoutMdx()
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, @"{""error"":""Error while generating MDX"",""info"":"""",""mdx"":""""}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca"));

            Assert.IsType<InvalidOperationException>(exception.InnerException);
            Assert.Contains("Received an empty or invalid MDX generator response", exception.Message, StringComparison.Ordinal);
            Assert.Contains("Error while generating MDX", exception.Message, StringComparison.Ordinal);
        }

        [Theory]
        [InlineData(@"{""error"":"""",""info"":""Please select a measure from the list"",""mdx"":""""}", "Please select a measure from the list")]
        [InlineData(@"{""query"":""show sales from selected region"",""mdx"":""""}", "show sales from selected region")]
        [InlineData(@"{""errorMessage"":""Select a profile from the settings""}", "The server reported: Select a profile from the settings")]
        public async Task GenerateMdxFromNaturalLanguageAsync_DoesNotTreatProseAsMdx( string body, string expectedInMessage )
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, body);

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca"));

            Assert.IsType<InvalidOperationException>(exception.InnerException);
            Assert.Contains(expectedInMessage, exception.Message, StringComparison.Ordinal);
            Assert.DoesNotContain(stub.Requests, request => request.Contains("/grid/mdx", StringComparison.Ordinal));
        }

        [Theory]
        [InlineData("text/plain")]
        [InlineData("*/*")]
        [InlineData("application/xml")]
        public async Task GenerateMdxFromNaturalLanguageAsync_ReadsJsonSentWithAnotherContentType( string contentType )
        {
            const string mdx = "SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]";

            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, $@"{{""error"":"""",""info"":""Generated from the outline."",""mdx"":{Quote(mdx)}}}");

                return GetObjectResponse(request);
            }) { ContentType = contentType };

            var cube = await GetCubeAsync(stub);
            var result = await cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca");

            Assert.Equal(mdx, result.Mdx);
            Assert.Equal("Generated from the outline.", result.Explanation);
        }

        [Fact]
        public async Task GetAiQuerySamplesAsync_KeepsSamplesThatReadLikeMdx()
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/listSampleQueries", StringComparison.Ordinal) )
                    return (200, @"{""sampleQueries"":[{""nlq"":""Select the top 5 products from last year""},{""nlq"":""Show sales by market""}]}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var samples = await cube.GetAiQuerySamplesAsync();

            Assert.Equal(new[] { "Select the top 5 products from last year", "Show sales by market" }, samples);
        }

        [Fact]
        public async Task GenerateMdxFromNaturalLanguageAsync_SendsTheDefaultOptions()
        {
            const string mdx = "SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]";

            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, $@"{{""mdx"":{Quote(mdx)}}}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            await cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca");

            var generate = Assert.Single(stub.Requests, request => request.Contains("/mdxgenerator?", StringComparison.Ordinal));
            Assert.Contains("isConvStart=true", generate, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("includeAttributesInNlq=false", generate, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("prompt=", generate, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task GenerateMdxFromNaturalLanguageAsync_ContinuesAConversationAndSendsThePrompt()
        {
            const string mdx = "SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]";

            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, $@"{{""mdx"":{Quote(mdx)}}}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            await cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca", new EssAiQueryOptions(startNewConversation: false, prompt: "Use the Actual scenario"));

            var generate = Assert.Single(stub.Requests, request => request.Contains("/mdxgenerator?", StringComparison.Ordinal));
            Assert.Contains("isConvStart=false", generate, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("prompt=Use%20the%20Actual%20scenario", generate, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData("generate", 404)]
        [InlineData("generate", 405)]
        [InlineData("execute", 404)]
        [InlineData("execute", 405)]
        [InlineData("samples", 404)]
        [InlineData("samples", 405)]
        [InlineData("clear", 404)]
        [InlineData("clear", 405)]
        public async Task EveryAiQueryOperation_TranslatesAMissingEndpointToNotSupportedAndKeepsTheStatus( string operation, int statusCode )
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.Contains("/essbase/rest/v1/ai/", StringComparison.Ordinal) )
                    return (statusCode, @"{""message"":""Endpoint unavailable""}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);

            Task Call() => operation switch
            {
                "generate" => cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca"),
                "execute"  => cube.ExecuteNaturalLanguageQueryAsync("Show sales", "Dodeca"),
                "samples"  => cube.GetAiQuerySamplesAsync(),
                _          => cube.ClearAiQueryConversationAsync("Dodeca")
            };

            var exception = await Assert.ThrowsAsync<NotSupportedException>(Call);

            Assert.StartsWith("AI Query is not supported by this Essbase server.", exception.Message, StringComparison.Ordinal);
            Assert.Contains($"status code {statusCode}", exception.Message, StringComparison.Ordinal);
            Assert.NotNull(exception.InnerException);
        }

        [Fact]
        public async Task TheSynchronousMethodsReturnTheSameResultsAsTheAsynchronousOnes()
        {
            const string mdx = "SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]";

            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, $@"{{""mdx"":{Quote(mdx)}}}");

                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/listSampleQueries", StringComparison.Ordinal) )
                    return (200, @"[""Show sales""]");

                if ( request.StartsWith("POST /essbase/rest/v1/applications/Sample/databases/Basic/grid/mdx", StringComparison.Ordinal) )
                    return (200, "{}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);

            Assert.Equal(mdx, cube.GenerateMdxFromNaturalLanguage("Show sales", "Dodeca").Mdx);
            Assert.Equal(mdx, cube.ExecuteNaturalLanguageQuery("Show sales", "Dodeca").Query.Mdx);
            Assert.Equal(new[] { "Show sales" }, cube.GetAiQuerySamples());

            cube.ClearAiQueryConversation("Dodeca");

            Assert.Contains(stub.Requests, request => request.StartsWith("DELETE /essbase/rest/v1/ai/applications/Sample/databases/Basic/conversationHistory?", StringComparison.Ordinal));
        }

        [Fact]
        public async Task TheFluentExtensionsChainFromACubeTask()
        {
            const string mdx = "SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]";

            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, $@"{{""mdx"":{Quote(mdx)}}}");

                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/listSampleQueries", StringComparison.Ordinal) )
                    return (200, @"[""Show sales""]");

                if ( request.StartsWith("POST /essbase/rest/v1/applications/Sample/databases/Basic/grid/mdx", StringComparison.Ordinal) )
                    return (200, "{}");

                return GetObjectResponse(request);
            });

            var server = await new EssServerFactory().CreateEssServerAsync(stub.BasePath, "admin", "password", connect: false);
            Task<IEssCube> CubeTask() => server.GetApplicationAsync("Sample").GetCubeAsync("Basic");

            Assert.Equal(mdx, (await CubeTask().GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca")).Mdx);
            Assert.Equal(mdx, (await CubeTask().ExecuteNaturalLanguageQueryAsync("Show sales", "Dodeca")).Query.Mdx);
            Assert.Equal(new[] { "Show sales" }, await CubeTask().GetAiQuerySamplesAsync());

            await CubeTask().ClearAiQueryConversationAsync("Dodeca");

            Assert.Contains(stub.Requests, request => request.StartsWith("DELETE /essbase/rest/v1/ai/applications/Sample/databases/Basic/conversationHistory?", StringComparison.Ordinal));
        }

        [Fact]
        public async Task GetAiQuerySamplesAsync_ReadsAListOfStrings()
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/listSampleQueries", StringComparison.Ordinal) )
                    return (200, @"[ ""Using Jan, display Sales."", ""Using Jan and Sales, display top 10 Product."" ]");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);

            Assert.Equal(new[] { "Using Jan, display Sales.", "Using Jan and Sales, display top 10 Product." }, await cube.GetAiQuerySamplesAsync());
        }

        [Fact]
        public async Task GetAiQuerySamplesAsync_ReadsAListHeldByAPropertyWithoutTheOtherStrings()
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/listSampleQueries", StringComparison.Ordinal) )
                    return (200, @"{""status"":""SUCCESS"",""sampleQueries"":[""Show sales"",""Show profit""]}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);

            Assert.Equal(new[] { "Show sales", "Show profit" }, await cube.GetAiQuerySamplesAsync());
        }

        [Theory]
        [InlineData("[]")]
        [InlineData("{}")]
        [InlineData(@"{""status"":""SUCCESS"",""queries"":[]}")]
        public async Task GetAiQuerySamplesAsync_ReturnsAnEmptyListForAnEmptyAnswer( string body )
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/listSampleQueries", StringComparison.Ordinal) )
                    return (200, body);

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);

            Assert.Empty(await cube.GetAiQuerySamplesAsync());
        }

        [Fact]
        public async Task GetAiQuerySamplesAsync_RejectsContentThatIsNotAListOfQueries()
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/listSampleQueries", StringComparison.Ordinal) )
                    return (200, @"{""status"":""FAILED"",""detail"":""The profile is missing""}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var exception = await Assert.ThrowsAsync<Exception>(() => cube.GetAiQuerySamplesAsync());

            Assert.IsType<InvalidOperationException>(exception.InnerException);
            Assert.Contains("The profile is missing", exception.Message, StringComparison.Ordinal);
        }

        [Theory]
        [InlineData("```mdx\\nSELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]\\n```")]
        [InlineData("```mdx\\nSELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]")]
        [InlineData("```SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]```")]
        [InlineData("```mdx SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]```")]
        [InlineData("```\\nSELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]\\n```")]
        public async Task GenerateMdxFromNaturalLanguageAsync_RemovesCodeFences( string fencedMdx )
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, $@"{{""mdx"":""{fencedMdx}""}}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var result = await cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca");

            Assert.Equal("SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]", result.Mdx);
        }

        [Fact]
        public async Task GenerateMdxFromNaturalLanguageAsync_DoesNotRepeatTheMdxAsItsExplanation()
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (200, @"{""response"":""```mdx\nSELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]\n```""}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var result = await cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca");

            Assert.Equal("SELECT {[Measures].[Sales]} ON COLUMNS FROM [Sample].[Basic]", result.Mdx);
            Assert.Null(result.Explanation);
        }

        [Fact]
        public async Task GetAiQuerySamplesAsync_ReturnsNaturalLanguageQueries()
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/listSampleQueries", StringComparison.Ordinal) )
                    return (200, @"{""sampleQueries"":[{""nlq"":""Show sales by market""},{""prompt"":""Compare actual to budget""}]}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var samples = await cube.GetAiQuerySamplesAsync();

            Assert.Equal(new[] { "Show sales by market", "Compare actual to budget" }, samples);
        }

        [Fact]
        public async Task ClearAiQueryConversationAsync_DeletesProfileHistory()
        {
            using var stub = new AiQueryEssbaseStub(GetObjectResponse);

            var cube = await GetCubeAsync(stub);
            await cube.ClearAiQueryConversationAsync("Dodeca");

            Assert.Contains(stub.Requests, request =>
                request.StartsWith("DELETE /essbase/rest/v1/ai/applications/Sample/databases/Basic/conversationHistory?", StringComparison.Ordinal) &&
                request.Contains("profileName=Dodeca", StringComparison.Ordinal));
        }

        [Theory]
        [InlineData(404)]
        [InlineData(405)]
        public async Task GenerateMdxFromNaturalLanguageAsync_TranslatesMissingEndpointToNotSupported( int statusCode )
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (statusCode, @"{""message"":""Endpoint unavailable""}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var exception = await Assert.ThrowsAsync<NotSupportedException>(() =>
                cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca"));

            Assert.DoesNotContain("version", exception.Message, StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain(stub.Requests, request => request.Contains("/about", StringComparison.OrdinalIgnoreCase));
        }

        [Theory]
        [InlineData(400)]
        [InlineData(401)]
        [InlineData(403)]
        [InlineData(500)]
        public async Task GenerateMdxFromNaturalLanguageAsync_DoesNotTreatOtherFailuresAsUnsupported( int statusCode )
        {
            using var stub = new AiQueryEssbaseStub(request =>
            {
                if ( request.StartsWith("POST /essbase/rest/v1/ai/applications/Sample/databases/Basic/mdxgenerator?", StringComparison.Ordinal) )
                    return (statusCode, @"{""message"":""AI query failed""}");

                return GetObjectResponse(request);
            });

            var cube = await GetCubeAsync(stub);
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca"));

            Assert.IsNotType<NotSupportedException>(exception);
        }

        [Fact]
        public async Task GenerateMdxFromNaturalLanguageAsync_PreservesCancellation()
        {
            using var stub = new AiQueryEssbaseStub(GetObjectResponse);
            var cube = await GetCubeAsync(stub);
            using var cancellation = new System.Threading.CancellationTokenSource();
            cancellation.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
                cube.GenerateMdxFromNaturalLanguageAsync("Show sales", "Dodeca", cancellationToken: cancellation.Token));
        }

        private static async Task<IEssCube> GetCubeAsync( AiQueryEssbaseStub stub )
        {
            var server = await new EssServerFactory().CreateEssServerAsync(stub.BasePath, "admin", "password", connect: false);
            return await server.GetApplicationAsync("Sample").GetCubeAsync("Basic");
        }

        private static (int statusCode, string body) GetObjectResponse( string request )
        {
            if ( request.StartsWith("GET /essbase/rest/v1/applications/Sample/databases/Basic", StringComparison.Ordinal) )
                return (200, @"{""name"":""Basic"",""application"":""Sample"",""type"":""BSO""}");

            if ( request.StartsWith("GET /essbase/rest/v1/applications/Sample", StringComparison.Ordinal) )
                return (200, @"{""name"":""Sample"",""type"":""BSO""}");

            return (200, "{}");
        }

        private static string Quote( string value ) => $@"""{value.Replace("\\", "\\\\").Replace("\"", "\\\"")}""";
    }

    internal sealed class AiQueryEssbaseStub : IDisposable
    {
        private readonly TcpListener _listener;
        private readonly Func<string, (int statusCode, string body)> _responseProvider;
        private volatile bool _disposed;

        public AiQueryEssbaseStub( Func<string, (int statusCode, string body)> responseProvider )
        {
            _responseProvider = responseProvider ?? throw new ArgumentNullException(nameof(responseProvider));
            _listener = new TcpListener(IPAddress.Loopback, 0);
            _listener.Start();

            BasePath = $"http://{IPAddress.Loopback}:{((IPEndPoint)_listener.LocalEndpoint).Port}/essbase/rest/v1";
            _ = Task.Run(AcceptLoopAsync);
        }

        public string BasePath { get; }

        /// <summary>
        /// The Content-Type the stub answers the AI endpoints with. Everything else is JSON, which the client needs to
        /// get the application and cube the AI endpoints are called on.
        /// </summary>
        public string ContentType { get; set; } = "application/json";

        private readonly List<string> _requests = new List<string>();

        /// <summary>
        /// The requests received so far, as a snapshot: the stub answers on other threads.
        /// </summary>
        public IReadOnlyList<string> Requests { get { lock ( _requests ) return _requests.ToList(); } }

        public void Dispose()
        {
            _disposed = true;
            try { _listener.Stop(); } catch { }
        }

        private async Task AcceptLoopAsync()
        {
            while ( !_disposed )
            {
                TcpClient client;

                try
                {
                    client = await _listener.AcceptTcpClientAsync();
                }
                catch
                {
                    break;
                }

                _ = Task.Run(() => HandleConnectionAsync(client));
            }
        }

        private async Task HandleConnectionAsync( TcpClient client )
        {
            try
            {
                using ( client )
                using ( var stream = client.GetStream() )
                {
                    var request = await ReadRequestAsync(stream);
                    lock ( _requests )
                        _requests.Add(request);

                    var (statusCode, body) = _responseProvider(request);
                    var contentType = request.StartsWith("POST /essbase/rest/v1/ai/", StringComparison.Ordinal) ? ContentType : "application/json";
                    var reason = statusCode switch
                    {
                        404 => "Not Found",
                        405 => "Method Not Allowed",
                        _   => "OK"
                    };

                    var bodyBytes = Encoding.UTF8.GetBytes(body ?? string.Empty);
                    var headers = Encoding.ASCII.GetBytes(
                        $"HTTP/1.1 {statusCode} {reason}\r\n" +
                        $"Content-Type: {contentType}\r\n" +
                        $"Content-Length: {bodyBytes.Length}\r\n" +
                        "Connection: close\r\n\r\n");

                    await stream.WriteAsync(headers, 0, headers.Length);
                    await stream.WriteAsync(bodyBytes, 0, bodyBytes.Length);
                    await stream.FlushAsync();
                }
            }
            catch
            {
                // Test assertions expose any failed or missing request.
            }
        }

        private static async Task<string> ReadRequestAsync( NetworkStream stream )
        {
            var buffer = new byte[8192];
            using var content = new MemoryStream();
            var headerEnd = -1;
            var contentLength = 0;

            while ( true )
            {
                var read = await stream.ReadAsync(buffer, 0, buffer.Length);
                if ( read <= 0 )
                    break;

                content.Write(buffer, 0, read);
                var request = Encoding.ASCII.GetString(content.ToArray());

                if ( headerEnd < 0 && (headerEnd = request.IndexOf("\r\n\r\n", StringComparison.Ordinal)) >= 0 )
                {
                    var contentLengthHeader = request
                        .Substring(0, headerEnd)
                        .Split(new[] { "\r\n" }, StringSplitOptions.None)
                        .FirstOrDefault(line => line.StartsWith("Content-Length:", StringComparison.OrdinalIgnoreCase));

                    if ( contentLengthHeader is not null )
                        int.TryParse(contentLengthHeader.Split(':')[1].Trim(), out contentLength);
                }

                if ( headerEnd >= 0 && content.Length >= headerEnd + 4 + contentLength )
                    break;
            }

            return Encoding.ASCII.GetString(content.ToArray());
        }
    }
}
