using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssServer : IEssObject
    {
        /// <summary>
        /// Gets the list of applications for this server available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssApplication"/> objects.</returns>
        public Task<List<IEssApplication>> GetApplicationsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of server-scoped variables available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssVariable"/> objects.</returns>
        public Task<List<IEssVariable>> GetVariablesAsync( CancellationToken cancellationToken = default );


        /// <summary>
        /// Gets the about information of this server
        /// </summary>

        public Task<IEssAbout> GetAboutAsync( CancellationToken cancellationToken = default );
        /// <summary>
        /// Gets the about information of this server
        /// </summary>

        public Task<IEssAboutInstance> GetAboutInstanceAsync( CancellationToken cancellationToken = default );

    }
}
