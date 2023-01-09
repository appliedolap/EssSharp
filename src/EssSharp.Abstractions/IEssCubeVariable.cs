using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssCubeVariable : IEssObject
    {
        /// <summary>
        /// Deletes the current variable.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task DeleteAsync( CancellationToken cancellationToken = default );
        
        /// <summary>
        /// Returns the scope of the current variable.
        /// </summary>
        public CubeVariableScope Scope { get; }
    }

    /// <summary>
    /// The <see cref="IEssVariable" /> scope.
    /// </summary>
    public enum CubeVariableScope
    {
        /// <summary />
        CUBE,
    }
}
