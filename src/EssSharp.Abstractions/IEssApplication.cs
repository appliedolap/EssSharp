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
        /// <returns>A list of <see cref="IEssCube" /> objects under this application.</returns>
        public List<IEssCube> GetCubes();

        /// <summary>
        /// Gets the list of cubes for this application available to the currently connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssCube" /> objects under this application.</returns>
        public Task<List<IEssCube>> GetCubesAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task StartAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Stops the application.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task StopAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of application-scoped variables available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssVariable"/> objects.</returns>
        public Task<List<IEssVariable>> GetVariablesAsync( CancellationToken cancellationToken = default );
    }
}
