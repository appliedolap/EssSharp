using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssVariable : IEssObject
    {
        /// <summary>
        /// Deletes this variable.
        /// </summary>
        /// <param name="cancellationToken" />
        public void Delete();

        /// <summary>
        /// Asynchronously deletes this variable.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task DeleteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the scope of this variable.
        /// </summary>
        public VariableScope Scope { get; }

        /// <summary>
        /// Returns the value of this variable.
        /// </summary>
        public string Value { get; }

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
