using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Client;

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

        [Fact(DisplayName = "JobTests - ExecuteAsync_WhileTheJobRuns_PollsOnAnotherSession")]
        public async Task ExecuteAsync_WhileTheJobRuns_PollsOnAnotherSession()
        {
            var polls = 0;

            // Every response opens a distinct session, and the job runs through two polls before completing.
            using var stub = new LoopbackEssbaseStub(index => new[] { $"JSESSIONID=S{index}X; Path=/; HttpOnly" })
            {
                ResponseProvider = requestLine => requestLine.Split(' ') switch
                {
                    [ "POST", var path, .. ] when path.EndsWith("/jobs")   => (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":100}"),
                    [ "GET",  var path, .. ] when path.EndsWith("/jobs/7") => (HttpStatusCode.OK, $@"{{""job_ID"":7,""statusCode"":{(Interlocked.Increment(ref polls) < 3 ? 100 : 200)}}}"),
                    _                                                     => null
                }
            };

            var server = new EssServer(stub.BasePath, "admin", "password");

            await server.CreateJob(new EssJobClearDataOptions("Sample", "Basic")).ExecuteAsync(TestContext.Current.CancellationToken);

            // Assert that no status poll rode the session that started the job.
            var jobSession = JobSession(stub);

            Assert.Equal(3, polls);
            Assert.DoesNotContain(stub.Requests, request => request.StartsWith("GET ") && request.Contains("/jobs/7") && request.Contains(jobSession));
        }

        [Fact(DisplayName = "JobTests - ExecuteAsync_WhenCancelledWhileWaiting_NeverReusesTheJobSession")]
        public async Task ExecuteAsync_WhenCancelledWhileWaiting_NeverReusesTheJobSession()
        {
            var jobs = 0;

            // Every response opens a distinct session. The first job never completes; the second completes at once.
            using var stub = new LoopbackEssbaseStub(index => new[] { $"JSESSIONID=S{index}X; Path=/; HttpOnly" })
            {
                ResponseProvider = requestLine => requestLine.Split(' ') switch
                {
                    [ "POST", var path, .. ] when path.EndsWith("/jobs")   => Interlocked.Increment(ref jobs) is 1
                                                                                  ? (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":100}")
                                                                                  : (HttpStatusCode.OK, @"{""job_ID"":8,""statusCode"":200}"),
                    [ "GET",  var path, .. ] when path.EndsWith("/jobs/7") => (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":100}"),
                    [ "GET",  var path, .. ] when path.EndsWith("/jobs/8") => (HttpStatusCode.OK, @"{""job_ID"":8,""statusCode"":200}"),
                    _                                                     => null
                }
            };

            var server = new EssServer(stub.BasePath, "admin", "password");

            using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(TestContext.Current.CancellationToken);
            cancellationSource.CancelAfter(TimeSpan.FromMilliseconds(250));

            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => server.CreateJob(new EssJobClearDataOptions("Sample", "Basic")).ExecuteAsync(cancellationSource.Token));

            var jobSession = JobSession(stub);
            var cancelledAt = stub.Requests.Count;

            // Run another job on the same server, which takes whatever sessions the pool holds.
            await server.CreateJob(new EssJobClearDataOptions("Sample", "Basic")).ExecuteAsync(TestContext.Current.CancellationToken);

            // Assert that no later request rode the session the cancelled job may still be running on.
            Assert.DoesNotContain(stub.Requests.Skip(cancelledAt), request => request.Contains(jobSession));
        }

        [Fact(DisplayName = "JobTests - ApiClient_ForAJobStart_HoldsTheSessionUntilItIsReturned")]
        public async Task ApiClient_ForAJobStart_HoldsTheSessionUntilItIsReturned()
        {
            using var stub = new LoopbackEssbaseStub(@"JSESSIONID=JobSession; Path=/; HttpOnly")
            {
                ResponseProvider = requestLine => requestLine.StartsWith("POST ") ? (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":100}") : null
            };

            var (client, api) = CreateJobsApi(stub);

            await api.JobsExecuteByJobIdAsync(id: 7, cancellationToken: TestContext.Current.CancellationToken);

            // Assert that the job's session is held, not pooled.
            Assert.True(client.JobSessionCookies.ContainsKey(7));
            Assert.Empty(client.SessionCookies);

            client.ReturnJobSession(7);

            // Assert that returning it pools it.
            Assert.Empty(client.JobSessionCookies);
            Assert.Contains(client.SessionCookies, sessionCookies => sessionCookies.Cast<Cookie>().Any(cookie => cookie.Value == "JobSession"));
        }

        [Fact(DisplayName = "JobTests - ApiClient_ForAJobSessionThatIsForgotten_NeverPoolsIt")]
        public async Task ApiClient_ForAJobSessionThatIsForgotten_NeverPoolsIt()
        {
            using var stub = new LoopbackEssbaseStub(@"JSESSIONID=JobSession; Path=/; HttpOnly")
            {
                ResponseProvider = requestLine => requestLine.StartsWith("POST ") ? (HttpStatusCode.OK, @"{""job_ID"":7,""statusCode"":100}") : null
            };

            var (client, api) = CreateJobsApi(stub);

            await api.JobsExecuteByJobIdAsync(id: 7, cancellationToken: TestContext.Current.CancellationToken);

            client.ForgetJobSession(7);

            Assert.Empty(client.JobSessionCookies);
            Assert.Empty(client.SessionCookies);
        }

        /// <summary>
        /// Returns a jobs API around a new client for the given stub.
        /// </summary>
        /// <param name="stub">The stub.</param>
        private static (ApiClient client, JobsApi api) CreateJobsApi( LoopbackEssbaseStub stub )
        {
            var configuration = new Configuration()
            {
                BasePath = stub.BasePath,
                Username = "admin",
                Password = "password1"
            };

            var client = new ApiClient(stub.BasePath);

            return (client, new JobsApi(client, client, configuration));
        }

        /// <summary>
        /// Returns the cookie of the session that the first job start opened, as a request carries it.
        /// </summary>
        /// <param name="stub">The stub, whose response at each index opened the session S{index}X.</param>
        private static string JobSession( LoopbackEssbaseStub stub ) =>
            $"JSESSIONID=S{stub.Requests.FindIndex(request => request.StartsWith("POST ") && request.Split(' ')[1].EndsWith("/jobs"))}X";

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
