using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssDatasource : EssObject, IEssDatasource
    {
        #region Private Data

        private readonly EssServer  _server;
        private readonly Datasource _datasource;

        #endregion

        #region Constructors

        /// <summary />
        internal EssDatasource( Datasource datasource, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _datasource = datasource ?? 
                throw new ArgumentNullException(nameof(datasource), $"An API model {nameof(datasource)} is required to create an {nameof(EssDatasource)}.");

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssDatasource)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _datasource?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Datasource;

        #endregion
        
        #region IEssDatasource Members 

        /// <inheritdoc />
        public IEssServer Server => _server;

        /// <inheritdoc />
        public string ConnectionName => _datasource?.Connection;

        /// <inheritdoc />
        public EssDatasourceType DatasourceType => Enum.IsDefined(typeof(EssDatasourceType), _datasource?.Type) ? (EssDatasourceType) _datasource?.Type : EssDatasourceType.UNKNOWN;

        /// <inheritdoc />
        /// <returns>An <see cref="EssDatasourceConnection"/>.</returns>
        public IEssDatasourceConnection GetConnection() => GetConnectionAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssDatasourceConnection"/>.</returns>
        public async Task<IEssDatasourceConnection> GetConnectionAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<GlobalConnectionsApi>();
            var connection = await api.GlobalConnectionsGetConnectionDetailsAsync(connectionName: _datasource?.Connection, cancellationToken: cancellationToken).ConfigureAwait(false);

            return new EssDatasourceConnection( connection, Server as EssServer );
        }
        
        /// <inheritdoc />
        public string Query( IEssDatasourceQueryInfo queryInfo ) => QueryAsync(queryInfo)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<string> QueryAsync( IEssDatasourceQueryInfo queryInfo, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GlobalDatasourcesApi>();
                var results = await api.GlobalDatasourcesGetResultsAsync(body: new DatasourceQueryInfo(queryInfo.Query, queryInfo.Delimiter), cancellationToken: cancellationToken).ConfigureAwait(false);

                string streamId = null;

                if (Uri.TryCreate(results?.Links?.FirstOrDefault()?.Href?.TrimEnd('/'), UriKind.RelativeOrAbsolute, out Uri streamUri) )
                    streamId = streamUri?.Segments?.Last();

                if ( string.IsNullOrEmpty(streamId) )
                    throw new Exception("Unable to capture a valid stream ID.");

                if ( await api.GlobalDatasourcesGetDataWithHttpInfoAsync(streamId: streamId, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } response )
                    throw new Exception("Received an empty or invalid response.");

                return response.RawContent;
            }
            catch ( Exception e ) 
            {
                throw new Exception($@"Unable to query the ""{this}"" datasource. {e.Message}", e);
            }
        }

        #endregion

        #region System.Object Overrides

        /// <inheritdoc />
        /// <remarks>Provides the name of this datasource in the following format:<br />
        /// <b>&lt;datasource&gt;</b>.</remarks>
        public override string ToString() => $@"{_datasource?.Name}";

        #endregion
    }
}
