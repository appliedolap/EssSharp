using System;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssDatasourceConnection : EssObject, IEssDatasourceConnection
    {
        #region Private Data

        private readonly EssServer  _server;
        private readonly Connection _connection;

        #endregion

        #region Constructors

        /// <summary />
        internal EssDatasourceConnection( Connection connection, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _connection = connection ?? 
                throw new ArgumentNullException(nameof(connection), $"An API model {nameof(connection)} is required to create an {nameof(EssDatasourceConnection)}.");

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssDatasourceConnection)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _connection?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.DatasourceConnection;

        #endregion

        #region IEssDatasourceConnection Properties

        /// <inheritdoc />
        public IEssServer Server => _server;

        /// <inheritdoc />
        public EssDatasourceConnectionType ConnectionType => Enum.IsDefined(typeof(EssDatasourceConnectionType), _connection.Type) ? (EssDatasourceConnectionType) _connection.Type : EssDatasourceConnectionType.UNKNOWN;

        /// <inheritdoc />
        public EssDatasourceConnectionSubtype ConnectionSubtype => Enum.IsDefined(typeof(EssDatasourceConnectionSubtype), _connection?.Subtype) ? (EssDatasourceConnectionSubtype)_connection.Subtype : EssDatasourceConnectionSubtype.UNKNOWN;

        /// <inheritdoc />
        public string Description { get =>_connection.Description; set => _connection.Description = value; }

        /// <inheritdoc />
        public string Path { get => _connection.Path; }

        /// <inheritdoc />
        public bool Catalog { get => _connection.Catalog; }

        /// <inheritdoc />
        public string Host { get => _connection.Host; }

        /// <inheritdoc />
        public int Port { get => _connection.Port; }

        /// <inheritdoc />
        public string User { get => _connection.User; }

        /// <inheritdoc />
        //public string Password { get => _connection.Password; protected set => _connection.Password = value; }

        /// <inheritdoc />
        public bool Encrypted { get => _connection.Encrypted; }

        /// <inheritdoc />
        public string Token { get => _connection.Token; }
            
        /// <inheritdoc />
        public string Sid { get => _connection.Sid; }

        /// <inheritdoc />
        public string Service { get => _connection.Service; }
            
        /// <inheritdoc />
        public string Schema { get => _connection.Schema; }

        /// <inheritdoc />
        public string DbURL {  get => _connection.DbURL; }

        /// <inheritdoc />
        public string DbDriver { get => _connection.DbDriver; }

        /// <inheritdoc />
        public string DatasourceName { get => _connection.Datasource; }

        /// <inheritdoc />
        public string WalletPath { get => _connection.WalletPath; }

        /// <inheritdoc />
        public bool RepoWallet { get => _connection.RepoWallet;   }

        /// <inheritdoc />
        public int MinPoolSize { get => _connection.MinPoolSize; }

        /// <inheritdoc />
        public int MaxPoolSize { get => _connection.MinPoolSize;  }

        #endregion

        #region IEssDatasourceConnection Methods

        /// <inheritdoc />
        public void TestConnection() => TestConnectionAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task TestConnectionAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GlobalConnectionsApi>();
                await api.GlobalConnectionsTestConnectionExistingAsync( Name, 0, cancellationToken ).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc />
        public void DeleteConnection() => DeleteConnectionAsync().GetAwaiter().GetResult();


        /// <inheritdoc />
        public async Task DeleteConnectionAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GlobalConnectionsApi>();
                await api.GlobalConnectionsDeleteConnectionAsync(Name, 0, cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
