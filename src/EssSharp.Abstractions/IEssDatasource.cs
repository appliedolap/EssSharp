
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssDatasource : IEssObject
    {
        /// <summary>
        /// Returns the server that contains this job.
        /// </summary>
        public IEssServer Server { get; }

        /// <summary>
        /// Returns the name of the Connection
        /// </summary>
        public string ConnectionName { get; }

        /// <summary>
        /// Returns the Datasource type
        /// </summary>
        public EssDatasourceType DatasourceType { get; }

        public IEssDatasourceConnection GetConnection();

        public Task<IEssDatasourceConnection> GetConnectionAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public string Query( IEssDatasourceQueryInfo queryInfo );

        public Task<string> QueryAsync( IEssDatasourceQueryInfo queryInfo, CancellationToken cancellationToken = default );
    }
}
