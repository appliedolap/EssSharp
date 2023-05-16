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
        private readonly Connection _connection;

        #endregion

        #region Constructors

        /// <summary />
        internal EssApplicationDatasourceConnection( Connection connection, EssApplication application ) : base( connection, application?.Server as EssServer)
        {
            _connection = connection ?? 
                throw new ArgumentNullException(nameof(connection), $"An API model {nameof(connection)} is required to create an {nameof(EssApplicationDatasourceConnection)}.");

            _application = application ??
                throw new ArgumentNullException(nameof(application), $"An {nameof(EssServer)} {nameof(application)} is required to create an {nameof(EssApplicationDatasourceConnection)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _connection?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.ApplicationDatasourceConnection;

        #endregion

        #region IEssApplicationDatasourceConnection Properties

        /// <inheritdoc />
        public IEssApplication Application => _application;

        #endregion

        #region IEssApplicationDatasourceConnection override methods


        /// <inheritdoc />
        public override void TestConnection() => TestConnectionAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public override async Task TestConnectionAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationConnectionsApi>();
                await api.ApplicationConnectionsTestConnectionExistingAsync(Application.Name, Name, 0, cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc />
        public override void DeleteConnection() => DeleteConnectionAsync().GetAwaiter().GetResult();


        /// <inheritdoc />
        public override async Task DeleteConnectionAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationConnectionsApi>();
                await api.ApplicationConnectionsDeleteConnectionAsync(Application.Name, Name, 0, cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
