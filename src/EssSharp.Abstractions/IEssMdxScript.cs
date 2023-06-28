using System;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssMdxScript : IEssObject, IEssScript
    {
        #region Properties 

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferences"></param>
        /// <param name="getContent"></param>
        public EssQueryReport Query( EssQueryPreferences preferences = null );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="preferences"></param>
        /// <param name="getContent"></param>
        /// <param name="cancellationToken"></param>
        public Task<EssQueryReport> QueryAsync( EssQueryPreferences preferences = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        public IEssGrid GetGrid();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> GetGridAsync( CancellationToken cancellationToken = default );

        #endregion
    }
}
