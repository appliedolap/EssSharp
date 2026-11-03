using System.Threading.Tasks;

using Xunit;
using Xunit.Sdk;

namespace EssSharp.Integration.Setup
{
    /// <summary>
    /// Resolves the identity service the OAuth tests take their tokens from when the OAuth collection starts, and gives
    /// its users their Essbase roles.
    /// </summary>
    /// <remarks>
    /// This runs after the collections that create the sample applications, which the application roles refer to. The
    /// tests themselves skip, with the fixture's reason, when the server does not accept the service's tokens.
    /// </remarks>
    /// <param name="messageSink" />
    public class OAuthCollectionFixture( IMessageSink messageSink ) : IAsyncLifetime
    {
        /// <summary />
        private readonly IMessageSink _messageSink = messageSink;

        /// <inheritdoc />
        public async ValueTask InitializeAsync()
        {
            await IntegrationTestIdentity.ResolveAsync(_messageSink, TestContext.Current.CancellationToken);

            if (IntegrationTestIdentity.Available)
                await IntegrationTestIdentity.ProvisionUsersAsync(TestContext.Current.CancellationToken);
        }

        /// <inheritdoc />
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}
