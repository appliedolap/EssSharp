using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssApplication : IEssObject
    {
        /// <summary>
        /// Gets the list of cubes for this application available to the currently connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssCube" /> objects under this application.</returns>
        public Task<List<IEssCube>> GetCubesAsync( CancellationToken cancellationToken );

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task StartAsync( CancellationToken cancellationToken );

        /// <summary>
        /// Stops the application.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task StopAsync( CancellationToken cancellationToken );

        /// <summary>
        /// Gets The Variables
        /// </summary>    
        public Task<List<IEssVariable>> GetVariablesAsync( CancellationToken cancellationToken );
    }
}
