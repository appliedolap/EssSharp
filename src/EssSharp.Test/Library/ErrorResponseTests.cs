using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using EssSharp.Client;
using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of the messages that failed requests produce, against a loopback stub that scripts the
    /// server's responses.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class ErrorResponseTests
    {
        /// <summary>
        /// The error page that the web server in front of Essbase serves while Essbase is still starting.
        /// </summary>
        private const string _serviceUnavailablePage = @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Draft//EN"">
<HTML>
<HEAD>
<TITLE>Error 503--Service Unavailable</TITLE>
</HEAD>
<BODY bgcolor=""white"">
<H2>Error 503--Service Unavailable</H2>
</BODY>
</HTML>
";

        [Fact(DisplayName = "ErrorResponseTests - GetApplicationAsync_WithHtmlErrorPage_DescribesTheStatusCode")]
        public async Task GetApplicationAsync_WithHtmlErrorPage_DescribesTheStatusCode()
        {
            using var stub = new LoopbackEssbaseStub() { ContentType = "text/html", ResponseProvider = Respond(HttpStatusCode.ServiceUnavailable, _serviceUnavailablePage) };

            var server = new EssServer(stub.BasePath, "admin", "password");

            var exception = await Assert.ThrowsAsync<Exception>(() => server.GetApplicationAsync("Sample", TestContext.Current.CancellationToken));

            Assert.Equal(@"Unable to get the application ""Sample"". Error calling ApplicationsGetApplication: The request failed with status code 503 (ServiceUnavailable). The server may still be starting, or may be temporarily unavailable.", exception.Message);

            // Assert that the page itself is still available to callers that want it.
            Assert.Equal(_serviceUnavailablePage, Assert.IsType<ApiException>(exception.InnerException).ErrorContent);
        }

        /// <summary>
        /// Answers the request for the application with the given status code and body.
        /// </summary>
        /// <param name="statusCode">The status code of the application response.</param>
        /// <param name="body">The body of the application response.</param>
        private static Func<string, (HttpStatusCode StatusCode, string Body)?> Respond( HttpStatusCode statusCode, string body ) => requestLine =>
        {
            var path = requestLine.Split(' ').ElementAtOrDefault(1)?.Split('?')[0] ?? string.Empty;

            return path.EndsWith("/applications/Sample") ? (statusCode, body) : null;
        };
    }
}
