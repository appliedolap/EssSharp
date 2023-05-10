using System;

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

        #endregion 
    }
}
