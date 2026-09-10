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
                    return (200, $@"{{""mdx"":{Quote(mdx)},""explanation"":""Generated from the outline.""}}");

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

        public List<string> Requests { get; } = new List<string>();

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
                    lock ( Requests )
                        Requests.Add(request);

                    var (statusCode, body) = _responseProvider(request);
                    var reason = statusCode switch
                    {
                        404 => "Not Found",
                        405 => "Method Not Allowed",
                        _   => "OK"
                    };

                    var bodyBytes = Encoding.UTF8.GetBytes(body ?? string.Empty);
                    var headers = Encoding.ASCII.GetBytes(
                        $"HTTP/1.1 {statusCode} {reason}\r\n" +
                        "Content-Type: application/json\r\n" +
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
