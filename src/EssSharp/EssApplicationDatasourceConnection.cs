using System;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssApplicationDatasourceConnection : EssDatasourceConnection, IEssApplicationDatasourceConnection
    {
        #region Private Data

        private readonly EssApplication _application;

        #endregion

        #region Constructors

        /// <summary />
        internal EssApplicationDatasourceConnection( Connection connection, EssApplication application ) : base(connection, application?.Server as EssServer)
        {
            _application = application ??
                throw new ArgumentNullException(nameof(application), $"An {nameof(EssServer)} {nameof(application)} is required to create an {nameof(EssApplicationDatasourceConnection)}.");
        }

        #endregion

        #region IEssApplicationDatasourceConnection Properties

        /// <inheritdoc />
        public IEssApplication Application => _application;

        #endregion

        #region IEssDatasourceConnection Override Methods

        /// <inheritdoc />
        public override void DeleteConnection() => DeleteConnectionAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public override async Task DeleteConnectionAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationConnectionsApi>();
                await api.ApplicationConnectionsDeleteConnectionAsync(Application.Name, Name, 0, cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc />
        public override void TestConnection() => TestConnectionAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public override async Task TestConnectionAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationConnectionsApi>();
                await api.ApplicationConnectionsTestConnectionExistingAsync(Application.Name, Name, 0, cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
