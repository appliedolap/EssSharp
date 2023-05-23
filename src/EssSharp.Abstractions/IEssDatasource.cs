
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

    /// <summary>
    /// Fluent extensions for <see cref="IEssDatasource"/>.
    /// </summary>
    public static class IEssDatasourceExtensions
    {
        /// <summary>
        /// Asynchronously executes the datasource query and returns the records.
        /// </summary>
        /// <param name="queryInfo" />
        /// <param name="cancellationToken" />
        public static async Task<string> QueryAsync( this Task<IEssDatasource> datasourceTask, IEssDatasourceQueryInfo queryInfo, CancellationToken cancellationToken = default ) =>
            await (await datasourceTask.ConfigureAwait(false)).QueryAsync(queryInfo, cancellationToken).ConfigureAwait(false);
    }
}
