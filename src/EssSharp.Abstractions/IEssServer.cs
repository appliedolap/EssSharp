using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssServer : IEssObject
    {
        /// <summary>
        /// Gets the list of applications for this server available to the currently connected user.
        /// </summary>
        /// <returns> A list of <see cref="IEssApplication"/> objects.</returns>
        public Task<List<IEssApplication>> GetApplicationsAsync( CancellationToken cancellationToken );
        public Task<List<IEssVariable>> GetVariablesAsync( CancellationToken cancellationToken );
    }
}
