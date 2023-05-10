namespace EssSharp
{
    /// <summary />
    public interface IEssDatasource : IEssObject
    {
        /// <summary>
        /// Returns the server that contains this job.
        /// </summary>
        public IEssServer Server { get; }

        //public EssDatasourceType DatasourceType { get; }

        //public string ConnectionName { get; }

        //public async Task<IEssDatasourceConnection> GetConnectionAsync( CancellationToken cancellationToken = default );

        public string Query( string query, string delimiter = "," );
    }
}
