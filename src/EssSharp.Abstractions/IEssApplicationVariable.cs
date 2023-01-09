using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssApplicationVariable : IEssObject
    {
        /// <summary>
        /// Deletes the current variable.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task DeleteAsync( CancellationToken cancellationToken = default );
        
        /// <summary>
        /// Returns the scope of the current variable.
        /// </summary>
        public ApplicationVariableScope Scope { get; }
    }

    /// <summary>
    /// The <see cref="IEssApplicationVariable" /> scope.
    /// </summary>
    public enum ApplicationVariableScope
    {
        /// <summary />
        APPLICATION,
    }
}
