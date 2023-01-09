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
        /// Gets the value of this variable.
        /// </summary>
        public string Value { get; }
        
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
        Server,
        /// <summary />
        Application,
        /// <summary />
        Cube
    }
}
