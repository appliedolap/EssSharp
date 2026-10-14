using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Client;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Tests of how the client chooses sessions around grid work, and of sign-out, against a loopback stub. Essbase splits
    /// off a second session behind a session's first grid request, and signing out does not end it, so the client keeps
    /// grid requests on the sessions that have already crossed into the grid.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class GridSessionTests
    {
        [Fact(DisplayName = "GridSessionTests - ApiClient_WithACrossedAndAnUncrossedSession_KeepsGridRequestsOnTheCrossedSession")]
        public async Task ApiClient_WithACrossedAndAnUncrossedSession_KeepsGridRequestsOnTheCrossedSession()
        {
            // The first response opens the session that the first grid request crosses into the grid.
            using var stub = new LoopbackEssbaseStub(index => index is 0 ? new[] { @"JSESSIONID=CrossedX; Path=/; HttpOnly" } : Array.Empty<string>());

            var (client, configuration) = CreateClient(stub);
            var gridApi  = new GridApi(client, client, configuration);
            var aboutApi = new AboutEssbaseApi(client, client, configuration);

            await gridApi.GridGetDefaultAsync("Sample", "Basic", cancellationToken: TestContext.Current.CancellationToken);

            // Pool a second session that has not crossed into the grid, as reading the default grid preferences does.
            client.SessionCookies.Add(new CookieCollection() { new Cookie("JSESSIONID", "PlainX", "/", IPAddress.Loopback.ToString()) });

            await gridApi.GridGetDefaultAsync("Sample", "Basic", cancellationToken: TestContext.Current.CancellationToken);
            await aboutApi.AboutGetAboutAsync(cancellationToken: TestContext.Current.CancellationToken);

            // Assert that the grid request rode the crossed session, and the other request the session that has not crossed.
            Assert.Contains("JSESSIONID=CrossedX", stub.Requests[1]);
            Assert.Contains("JSESSIONID=PlainX", stub.Requests[2]);
        }

        [Fact(DisplayName = "GridSessionTests - SignOutAsync_WithNoSessionHeld_SendsNothing")]
        public async Task SignOutAsync_WithNoSessionHeld_SendsNothing()
        {
            using var stub = new LoopbackEssbaseStub();

            var server = new EssServer(stub.BasePath, "admin", "password");

            await server.SignOutAsync(cancellationToken: TestContext.Current.CancellationToken);
            await server.SignOutAsync(allSessions: true, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Empty(stub.Requests);
        }

        [Fact(DisplayName = "GridSessionTests - SignOutAsync_ForAllSessions_LogsOutOnlyTheSessionsHeld")]
        public async Task SignOutAsync_ForAllSessions_LogsOutOnlyTheSessionsHeld()
        {
            // The first response opens a session, which sign-out then ends.
            using var stub = new LoopbackEssbaseStub(index => index is 0 ? new[] { @"JSESSIONID=HeldX; Path=/; HttpOnly" } : Array.Empty<string>());

            var server = new EssServer(stub.BasePath, "admin", "password");

            await server.GetAboutAsync(TestContext.Current.CancellationToken);
            await server.SignOutAsync(allSessions: true, cancellationToken: TestContext.Current.CancellationToken);

            var logouts = stub.Requests.Where(request => request.StartsWith("DELETE ")).ToList();

            // Assert that the one held session was logged out, and that no logout went without a session.
            Assert.Single(logouts);
            Assert.Contains("JSESSIONID=HeldX", logouts[0]);
        }

        /// <summary>
        /// Returns a client and a configuration for the given stub.
        /// </summary>
        /// <param name="stub">The stub.</param>
        private static (ApiClient client, Configuration configuration) CreateClient( LoopbackEssbaseStub stub ) =>
            (new ApiClient(stub.BasePath), new Configuration() { BasePath = stub.BasePath, Username = "admin", Password = "password1" });
    }
}
