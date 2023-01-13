using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssCube : IEssObject
    {
        /// <summary>
        /// Returns the parent application of the cube.
        /// </summary>
        public IEssApplication Application { get; }

        /// <summary>
        /// Gets the list of cube-scoped variables available to the connected user.
        /// </summary>
        /// <returns>A list of <see cref="IEssServerVariable"/> objects.</returns>
        public List<IEssCubeVariable> GetVariables();

        /// <summary>
        /// Asynchronously gets the list of cube-scoped variables available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssServerVariable"/> objects.</returns>
        public Task<List<IEssCubeVariable>> GetVariablesAsync( CancellationToken cancellationToken = default );
        
        /// <summary>
        /// Gets the list of dimensions.
        /// </summary>
        public List<IEssDimension> GetDimensions();

        /// <summary>
        /// Gets the list of dimensions.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssDimension"/> objects.</returns>
        public Task<List<IEssDimension>> GetDimensionsAsync( CancellationToken cancellationToken = default );
    }
}
