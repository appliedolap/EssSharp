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
        /// Synchronously executes MDX query on the cube.
        /// </summary>
        /// <param name="preferences"></param>
        public EssQueryReport Query( EssQueryPreferences preferences = null );

        /// <summary>
        /// Asynchronously executes MDX query on the cube.
        /// </summary>
        /// <param name="preferences"></param>
        /// <param name="getContent"></param>
        /// <param name="cancellationToken"></param>
        public Task<EssQueryReport> QueryAsync( EssQueryPreferences preferences = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Synchronously executes MDX query and generates a <see cref="IEssGrid"/>.
        /// </summary>
        public IEssGrid GetGrid();

        /// <summary>
        /// Asynchronously executes MDX query and generates a <see cref="IEssGrid"/>.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<IEssGrid> GetGridAsync( CancellationToken cancellationToken = default );

        #endregion
    }
}
