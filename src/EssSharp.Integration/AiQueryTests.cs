using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using EssSharp.Client;
using EssSharp.Integration.Setup;

using Microsoft.Extensions.Logging;

using Xunit;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />.
    /// </summary>
    [CollectionDefinition(nameof(AiQueryTests), DisableParallelization = true), CollectionPriority(7)]
    public class AiQueryTestsCollection : ICollectionFixture<AiQueryCollectionFixture> { }

    /// <summary>
    /// Integration tests for AI Query operations against supported and unsupported Essbase servers.
    /// </summary>
    [Collection(nameof(AiQueryTests)), Trait("type", "execute")]
    public class AiQueryTests : IntegrationTestBase, IAsyncLifetime
    {
        private static readonly Version _firstSupportedVersion = new(major: 21, minor: 8);

        /// <summary>
        /// The AI profile used by this test. Essbase creates a database profile with this name for each new
        /// conversation and will not replace an existing one, so every test needs a name of its own. It must also be
        /// a valid database identifier, which rules out hyphens.
        /// </summary>
        private readonly string _profileName;

        /// <summary />
        /// <param name="output" />
        public AiQueryTests( ITestOutputHelper output ) : base(output)
        {
            var prefix = string.IsNullOrWhiteSpace(AiQuerySettings?.ProfileName) ? "INTEGRATION_TEST" : AiQuerySettings.ProfileName;

            _profileName = $@"{prefix}_{Guid.NewGuid():N}".Substring(0, prefix.Length + 9).ToUpperInvariant();
        }

        /// <inheritdoc />
        public ValueTask InitializeAsync() => ValueTask.CompletedTask;

        /// <summary>
        /// Drops the conversation (and so the database profile) that the test started, if it started one.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            try
            {
                var (cube, version) = await GetAiQueryContextAsync();

                if ( version.CompareTo(_firstSupportedVersion) >= 0 )
                    await cube.ClearAiQueryConversationAsync(_profileName, TestContext.Current.CancellationToken);
            }
            catch ( Exception )
            {
                // The test may have been skipped or may not have started a conversation, so there may be nothing to drop.
            }
        }

        [Fact(DisplayName = @"AiQueryTests - 01 - Essbase_CanGenerateMdxFromNaturalLanguage"), Priority(01)]
        public async Task Essbase_CanGenerateMdxFromNaturalLanguage()
        {
            var (cube, version) = await GetAiQueryContextAsync();

            if ( version.CompareTo(_firstSupportedVersion) < 0 )
            {
                await AssertAiQueryNotSupportedAsync(() => cube.GenerateMdxFromNaturalLanguageAsync(
                    GetNaturalLanguageQuery(), GetProfileName(), cancellationToken: TestContext.Current.CancellationToken));
                return;
            }

            var result = await cube.GenerateMdxFromNaturalLanguageAsync(
                GetNaturalLanguageQuery(),
                GetProfileName(),
                new EssAiQueryOptions(startNewConversation: true, includeAttributes: false),
                TestContext.Current.CancellationToken);

            Assert.NotNull(result);
            Assert.Equal(GetNaturalLanguageQuery(), result.NaturalLanguageQuery);
            Assert.Equal(GetProfileName(), result.ProfileName);
            Assert.False(string.IsNullOrWhiteSpace(result.Mdx));
            Assert.Contains("SELECT", result.Mdx, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FROM", result.Mdx, StringComparison.OrdinalIgnoreCase);
            Assert.False(string.IsNullOrWhiteSpace(result.RawResponse));

            // The harness's AI gateway answers this question with a fixed point of view (Scripts/Ai/gateway/answers.json):
            // actual sales for each quarter of the year. Essbase looks up each of its words as a member and builds the MDX,
            // so the members show that the whole chain worked, not just that some MDX came back.
            if (IntegrationTestAiEnvironment.ServesCannedAnswers && GetNaturalLanguageQuery().Contains("actual sales by year", StringComparison.OrdinalIgnoreCase))
            {
                Assert.Contains("[Actual]", result.Mdx, StringComparison.OrdinalIgnoreCase);
                Assert.Contains("[Sales]", result.Mdx, StringComparison.OrdinalIgnoreCase);
                Assert.Contains("[Year].CHILDREN", result.Mdx, StringComparison.OrdinalIgnoreCase);
            }

            OutputLogger.LogInformation("AI MDX generator response: {Response}", result.RawResponse);
        }

        [Fact(DisplayName = @"AiQueryTests - 02 - Essbase_CanExecuteNaturalLanguageQuery"), Priority(02)]
        public async Task Essbase_CanExecuteNaturalLanguageQuery()
        {
            var (cube, version) = await GetAiQueryContextAsync();

            if ( version.CompareTo(_firstSupportedVersion) < 0 )
            {
                await AssertAiQueryNotSupportedAsync(() => cube.ExecuteNaturalLanguageQueryAsync(
                    GetNaturalLanguageQuery(), GetProfileName(), cancellationToken: TestContext.Current.CancellationToken));
                return;
            }

            var result = await cube.ExecuteNaturalLanguageQueryAsync(
                GetNaturalLanguageQuery(),
                GetProfileName(),
                new EssAiQueryOptions(startNewConversation: true, includeAttributes: false),
                TestContext.Current.CancellationToken);

            Assert.NotNull(result?.Query);
            Assert.NotNull(result.Grid);
            Assert.False(string.IsNullOrWhiteSpace(result.Query.Mdx));
            Assert.True(result.Grid.Slice.Rows > 0);
            Assert.True(result.Grid.Slice.Columns > 0);
        }

        [Fact(DisplayName = @"AiQueryTests - 03 - Essbase_CanGetAiQuerySamples"), Priority(03)]
        public async Task Essbase_CanGetAiQuerySamples()
        {
            var (cube, version) = await GetAiQueryContextAsync();

            if ( version.CompareTo(_firstSupportedVersion) < 0 )
            {
                await AssertAiQueryNotSupportedAsync(() => cube.GetAiQuerySamplesAsync(TestContext.Current.CancellationToken));
                return;
            }

            var samples = await cube.GetAiQuerySamplesAsync(TestContext.Current.CancellationToken);

            Assert.NotNull(samples);
            Assert.NotEmpty(samples);
            Assert.All(samples, sample => Assert.False(string.IsNullOrWhiteSpace(sample)));

            // The samples are natural-language queries. A response that was read wrongly shows up as JSON or MDX.
            Assert.All(samples, sample =>
            {
                Assert.DoesNotContain("{", sample, StringComparison.Ordinal);
                Assert.DoesNotContain("\"", sample, StringComparison.Ordinal);
                Assert.False(sample.TrimStart().StartsWith("SELECT ", StringComparison.OrdinalIgnoreCase) || sample.TrimStart().StartsWith("WITH ", StringComparison.OrdinalIgnoreCase));
            });

            OutputLogger.LogInformation("AI sample queries: {Samples}", string.Join(Environment.NewLine, samples));
        }

        [Fact(DisplayName = @"AiQueryTests - 04 - Essbase_CanClearAiQueryConversation"), Priority(04)]
        public async Task Essbase_CanClearAiQueryConversation()
        {
            var (cube, version) = await GetAiQueryContextAsync();

            if ( version.CompareTo(_firstSupportedVersion) < 0 )
            {
                await AssertAiQueryNotSupportedAsync(() => cube.ClearAiQueryConversationAsync(GetProfileName(), TestContext.Current.CancellationToken));
                return;
            }

            await cube.GenerateMdxFromNaturalLanguageAsync(
                GetNaturalLanguageQuery(),
                GetProfileName(),
                new EssAiQueryOptions(startNewConversation: true),
                TestContext.Current.CancellationToken);

            // Essbase keeps the conversation under the profile name until it is cleared, and then has nothing for it.
            Assert.Contains(GetNaturalLanguageQuery(), await GetConversationHistoryAsync(), StringComparison.Ordinal);

            await cube.ClearAiQueryConversationAsync(GetProfileName(), TestContext.Current.CancellationToken);

            Assert.Equal(string.Empty, await GetConversationHistoryAsync());
        }

        /// <summary>
        /// Returns the configured cube and its major/minor server version.
        /// </summary>
        private async Task<(IEssCube cube, Version version)> GetAiQueryContextAsync()
        {
            Assert.SkipWhen(AiQuerySettings?.Mode == IntegrationTestAiQueryMode.Disabled, "The AI query tests are disabled by the test settings (AiQuery:Mode).");

            var server = GetEssServer();
            var about  = await server.GetAboutAsync(TestContext.Current.CancellationToken);
            var version = new Version(string.Join('.', about?.Version?
                .Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Take(2)));

            var cube = await server
                .GetApplicationAsync(AiQuerySettings.ApplicationName, TestContext.Current.CancellationToken)
                .GetCubeAsync(AiQuerySettings.CubeName, TestContext.Current.CancellationToken);

            return (cube, version);
        }

        /// <summary>
        /// Returns the conversation Essbase keeps for this test's profile, read over the REST API because EssSharp has
        /// no call for it: the questions and the MDX generated for them, or an empty string when there is none.
        /// </summary>
        private async Task<string> GetConversationHistoryAsync()
        {
            using var http = IntegrationTestAiEnvironment.CreateEssbaseClient();

            var application = Uri.EscapeDataString(AiQuerySettings.ApplicationName);
            var cube        = Uri.EscapeDataString(AiQuerySettings.CubeName);

            using var response = await http.GetAsync($@"ai/applications/{application}/databases/{cube}/conversationHistory?profileName={Uri.EscapeDataString(GetProfileName())}", TestContext.Current.CancellationToken);

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken)).Trim();
        }

        /// <summary>
        /// Returns a query suitable for exercising an unsupported endpoint.
        /// </summary>
        private static string GetNaturalLanguageQuery() =>
            string.IsNullOrWhiteSpace(AiQuerySettings?.NaturalLanguageQuery)
                ? "Show actual sales by year."
                : AiQuerySettings.NaturalLanguageQuery;

        /// <summary>
        /// Returns the profile name unique to this test.
        /// </summary>
        private string GetProfileName() => _profileName;

        /// <summary>
        /// Asserts that an older server rejects an AI Query operation because its endpoint is unavailable.
        /// </summary>
        private static async Task AssertAiQueryNotSupportedAsync( Func<Task> operation )
        {
            var exception = await Assert.ThrowsAsync<NotSupportedException>(operation);
            var inner     = exception.InnerException;

            Assert.True(
                inner is ApiException { ErrorCode: 404 or 405 } ||
                inner is WebException
                {
                    Response: EssSharp.Api.WebExceptionRestResponse
                    {
                        StatusCode: HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed
                    }
                });
        }
    }
}
