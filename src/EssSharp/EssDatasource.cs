using System;
using System.Linq;

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
        public string Query( string query, string delimiter = "," )
        {
            var datasourcesApi = GetApi<GlobalDatasourcesApi>();
            var results = datasourcesApi.GlobalDatasourcesGetResults(body: new DatasourceQueryInfo(query, delimiter));

            string streamId = null;

            // Capture the streamed result ID from the first result link.
            if ( Uri.TryCreate(results?.Links?.FirstOrDefault()?.Href?.TrimEnd('/'), UriKind.RelativeOrAbsolute, out Uri streamUri) )
                streamId = streamUri?.Segments?.Last();

            if ( string.IsNullOrEmpty(streamId) )
                throw new Exception("unable to get stream id");

            //var result = datasourcesApi.GlobalDatasourcesGetData(streamId);
            return datasourcesApi.GlobalDatasourcesGetDataWithHttpInfo(streamId)?.RawContent;
        }

        #endregion 
    }
}
