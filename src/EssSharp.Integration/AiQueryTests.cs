using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using EssSharp.Client;
using EssSharp.Integration.Setup;

using Microsoft.Extensions.Logging;

using Xunit;
using Xunit.Abstractions;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />.
    /// </summary>
    [CollectionDefinition(nameof(AiQueryTests), DisableParallelization = true), CollectionPriority(6)]
    public class AiQueryTestsCollection : ICollectionFixture<CollectionFixture> { }

    /// <summary>
    /// Integration tests for AI Query operations against supported and unsupported Essbase servers.
    /// </summary>
    [Collection(nameof(AiQueryTests)), Trait("type", "execute")]
    public class AiQueryTests : IntegrationTestBase
    {
        private static readonly Version _firstSupportedVersion = new(major: 21, minor: 8);

        /// <summary />
        /// <param name="output" />
        public AiQueryTests( ITestOutputHelper output ) : base(output) { }

        [Fact(DisplayName = @"AiQueryTests - 01 - Essbase_CanGenerateMdxFromNaturalLanguage"), Priority(01)]
        public async Task Essbase_CanGenerateMdxFromNaturalLanguage()
        {
            var (cube, version) = await GetAiQueryContextAsync();

            if ( version.CompareTo(_firstSupportedVersion) < 0 )
            {
                await AssertAiQueryNotSupportedAsync(() => cube.GenerateMdxFromNaturalLanguageAsync(
                    GetNaturalLanguageQuery(), GetProfileName()));
                return;
            }

            var result = await cube.GenerateMdxFromNaturalLanguageAsync(
                GetNaturalLanguageQuery(),
                GetProfileName(),
                new EssAiQueryOptions(startNewConversation: true, includeAttributes: false));

            Assert.NotNull(result);
            Assert.Equal(GetNaturalLanguageQuery(), result.NaturalLanguageQuery);
            Assert.Equal(GetProfileName(), result.ProfileName);
            Assert.False(string.IsNullOrWhiteSpace(result.Mdx));
            Assert.Contains("SELECT", result.Mdx, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("FROM", result.Mdx, StringComparison.OrdinalIgnoreCase);
            Assert.False(string.IsNullOrWhiteSpace(result.RawResponse));

            OutputLogger.LogInformation("AI MDX generator response: {Response}", result.RawResponse);
        }

        [Fact(DisplayName = @"AiQueryTests - 02 - Essbase_CanExecuteNaturalLanguageQuery"), Priority(02)]
        public async Task Essbase_CanExecuteNaturalLanguageQuery()
        {
            var (cube, version) = await GetAiQueryContextAsync();

            if ( version.CompareTo(_firstSupportedVersion) < 0 )
            {
                await AssertAiQueryNotSupportedAsync(() => cube.ExecuteNaturalLanguageQueryAsync(
                    GetNaturalLanguageQuery(), GetProfileName()));
                return;
            }

            var result = await cube.ExecuteNaturalLanguageQueryAsync(
                GetNaturalLanguageQuery(),
                GetProfileName(),
                new EssAiQueryOptions(startNewConversation: true, includeAttributes: false));

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
                await AssertAiQueryNotSupportedAsync(() => cube.GetAiQuerySamplesAsync());
                return;
            }

            var samples = await cube.GetAiQuerySamplesAsync();

            Assert.NotNull(samples);
            Assert.NotEmpty(samples);
            Assert.All(samples, sample => Assert.False(string.IsNullOrWhiteSpace(sample)));

            OutputLogger.LogInformation("AI sample queries: {Samples}", string.Join(Environment.NewLine, samples));
        }

        [Fact(DisplayName = @"AiQueryTests - 04 - Essbase_CanClearAiQueryConversation"), Priority(04)]
        public async Task Essbase_CanClearAiQueryConversation()
        {
            var (cube, version) = await GetAiQueryContextAsync();

            if ( version.CompareTo(_firstSupportedVersion) < 0 )
            {
                await AssertAiQueryNotSupportedAsync(() => cube.ClearAiQueryConversationAsync(GetProfileName()));
                return;
            }

            await cube.GenerateMdxFromNaturalLanguageAsync(
                GetNaturalLanguageQuery(),
                GetProfileName(),
                new EssAiQueryOptions(startNewConversation: true));

            await cube.ClearAiQueryConversationAsync(GetProfileName());
        }

        /// <summary>
        /// Returns the configured cube and its major/minor server version.
        /// </summary>
        private async Task<(IEssCube cube, Version version)> GetAiQueryContextAsync()
        {
            var server = GetEssServer();
            var about  = await server.GetAboutAsync();
            var version = new Version(string.Join('.', about?.Version?
                .Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Take(2)));

            var cube = await server
                .GetApplicationAsync(AiQuerySettings.ApplicationName)
                .GetCubeAsync(AiQuerySettings.CubeName);

            return (cube, version);
        }

        /// <summary>
        /// Returns a query suitable for exercising an unsupported endpoint.
        /// </summary>
        private static string GetNaturalLanguageQuery() =>
            string.IsNullOrWhiteSpace(AiQuerySettings?.NaturalLanguageQuery)
                ? "Show actual sales by year."
                : AiQuerySettings.NaturalLanguageQuery;

        /// <summary>
        /// Returns a profile name suitable for exercising an unsupported endpoint.
        /// </summary>
        private static string GetProfileName() =>
            string.IsNullOrWhiteSpace(AiQuerySettings?.ProfileName)
                ? "integration-test-profile"
                : AiQuerySettings.ProfileName;

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
