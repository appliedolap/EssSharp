using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of <see cref="IEssJob"/> execution against a loopback stub that scripts the server's responses.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class JobTests
    {
        [Fact(DisplayName = "JobTests - ExecuteAsync_WhenAPollReturnsNoJob_FailsWithTheMissingJobInformation")]
        public async Task ExecuteAsync_WhenAPollReturnsNoJob_FailsWithTheMissingJobInformation()
        {
            var polls = 0;

            // The job is in progress on the first poll, and the second poll returns no job record at all.
            using var stub = new LoopbackEssbaseStub()
            {
                ResponseProvider = requestLine => requestLine.Split(' ') switch
                {
                    [ "POST", var path, .. ] when path.EndsWith("/jobs")   => (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":100}"),
                    [ "GET",  var path, .. ] when path.EndsWith("/jobs/7") => Interlocked.Increment(ref polls) is 1
                                                                                  ? (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":100}")
                                                                                  : (HttpStatusCode.OK, string.Empty),
                    _                                                     => null
                }
            };

            var server = new EssServer(stub.BasePath, "admin", "password");

            var exception = await Assert.ThrowsAnyAsync<Exception>(() => server.CreateJob(new EssJobClearDataOptions("Sample", "Basic")).ExecuteAsync(TestContext.Current.CancellationToken));

            Assert.Contains("Failed to retrieve job information.", exception.Message);
            Assert.DoesNotContain(Flatten(exception), inner => inner is NullReferenceException);
        }

        [Fact(DisplayName = "JobTests - ExecuteAsync_WhenCancelledWhileWaiting_StopsWithoutWaitingOutThePollDelay")]
        public async Task ExecuteAsync_WhenCancelledWhileWaiting_StopsWithoutWaitingOutThePollDelay()
        {
            // The job stays in progress on every poll.
            using var stub = new LoopbackEssbaseStub()
            {
                ResponseProvider = requestLine => requestLine.Split(' ') switch
                {
                    [ "POST", var path, .. ] when path.EndsWith("/jobs")   => (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":100}"),
                    [ "GET",  var path, .. ] when path.EndsWith("/jobs/7") => (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":100}"),
                    _                                                     => null
                }
            };

            var server = new EssServer(stub.BasePath, "admin", "password");

            // Cancel shortly after the wait begins, well inside the one-second delay between polls.
            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
            cancellationSource.CancelAfter(TimeSpan.FromMilliseconds(250));

            var started = DateTime.UtcNow;

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => server.CreateJob(new EssJobClearDataOptions("Sample", "Basic")).ExecuteAsync(cancellationSource.Token));

            Assert.True(DateTime.UtcNow - started < TimeSpan.FromMilliseconds(900), "The wait did not stop until the poll delay had elapsed.");
        }

        /// <summary>
        /// Returns the given exception and all of its inner exceptions.
        /// </summary>
        /// <param name="exception">The exception.</param>
        private static Exception[] Flatten( Exception exception )
        {
            var exceptions = new System.Collections.Generic.List<Exception>();

            for (var current = exception; current is not null; current = current.InnerException)
                exceptions.Add(current);

            return exceptions.ToArray();
        }
    }
}
