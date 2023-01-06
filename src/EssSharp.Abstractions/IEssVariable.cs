using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssVariable : IEssObject
    {
        /// <summary>
        /// summary.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public  Task DeleteAsync( CancellationToken cancellationToken );
        
        /// <summary>
        /// 
        /// </summary>
        public VariableScope Scope { get; }
    }

    /// <summary>
    /// 
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
