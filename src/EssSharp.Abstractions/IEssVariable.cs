using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssVariable : IEssObject
    {
        /// <summary>
        /// Deletes the current variable.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task DeleteAsync( CancellationToken cancellationToken = default );
        
        /// <summary>
        /// Returns the scope of the current variable.
        /// </summary>
        public VariableScope Scope { get; }
    }

    /// <summary>
    /// The <see cref="IEssVariable" /> scope.
    /// </summary>
    public enum VariableScope
    {
        /// <summary />
        SERVER,
        /// <summary />
        APPLICATION,
        /// <summary />
        CUBE
    }
}
