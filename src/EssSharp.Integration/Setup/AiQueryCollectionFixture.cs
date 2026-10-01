using System.Threading.Tasks;

using Xunit;
using Xunit.Sdk;

namespace EssSharp.Integration.Setup
{
    /// <summary>
    /// Builds the environment the AI query tests need when the AI query collection starts.
    /// </summary>
    /// <remarks>
    /// This is not part of the assembly fixture because it needs the sample application, which the tests that create
    /// the sample applications only create once they have run. The containers it starts are removed with the assembly
    /// fixture, by <see cref="IntegrationTestFactory.DisposeAsync" />.
    /// </remarks>
    /// <param name="messageSink" />
    public class AiQueryCollectionFixture( IMessageSink messageSink ) : IAsyncLifetime
    {
        /// <summary />
        private readonly IMessageSink _messageSink = messageSink;

        /// <inheritdoc />
        public ValueTask InitializeAsync() => new(IntegrationTestAiEnvironment.EnsureAsync(_messageSink, TestContext.Current.CancellationToken));

        /// <inheritdoc />
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
