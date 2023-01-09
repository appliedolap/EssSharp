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
        /// <summary>
        /// Gets the List of Variables
        /// </summary>
        public Task<List<IEssVariable>> GetVariablesAsync( CancellationToken cancellationToken );
        /// <summary>
        /// Gets the about information of this server
        /// </summary>

        public Task<IEssAbout> GetAboutAsync( CancellationToken cancellationToken );
        /// <summary>
        /// Gets the about information of this server
        /// </summary>

        public Task<IEssAboutInstance> GetAboutInstanceAsync( CancellationToken cancellationToken );

    }
}
