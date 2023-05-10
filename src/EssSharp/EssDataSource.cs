using System;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssDataSource : EssObject, IEssDataSource
    {
        #region Private Data

        private readonly EssServer  _server;
        private readonly Datasource _dataSource;

        #endregion

        #region Constructors

        /// <summary />
        internal EssDataSource( Datasource dataSource, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _dataSource = dataSource ?? 
                throw new ArgumentNullException(nameof(dataSource), $"An API model {nameof(dataSource)} is required to create an {nameof(EssDataSource)}.");

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssDataSource)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _dataSource?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.DataSource;

        #endregion
        
        #region IEssDataSource Members 

        /// <inheritdoc />
        public IEssServer Server => _server;

        #endregion 
    }
}
