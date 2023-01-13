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
        /// <returns>A list of <see cref="IEssApplication"/> objects.</returns>
        public List<IEssApplication> GetApplications();

        /// <summary>
        /// Gets the application with the given name.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns>An application object.</returns>
        public IEssApplication GetApplication(string applicationName);
        
        /// <summary>
        /// Gets the list of applications for this server available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssApplication"/> objects.</returns>
        public Task<List<IEssApplication>> GetApplicationsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a user session from the server for the configured user.
        /// </summary>
        /// <param name="includeToken">Whether to capture a session token.</param>
        /// <param name="includeGroups">Whether to capture the user's groups.</param>
        /// <param name="cancellationToken" />
        /// <returns>A user session.</returns>
        public IEssUserSession GetUserSession( bool includeToken = true, bool includeGroups = true );

        /// <summary>
        /// Asynchronously gets a user session from the server for the configured user.
        /// </summary>
        /// <param name="includeToken">Whether to capture a session token.</param>
        /// <param name="includeGroups">Whether to capture the user's groups.</param>
        /// <param name="cancellationToken" />
        /// <returns>A user session.</returns>
        public Task<IEssUserSession> GetUserSessionAsync( bool includeToken = true, bool includeGroups = true, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the list of server-scoped variables available to the connected user.
        /// </summary>
        /// <returns>A list of <see cref="IEssServerVariable"/> objects.</returns>
        public List<IEssServerVariable> GetVariables();

        /// <summary>
        /// Asynchronously gets the list of server-scoped variables available to the connected user.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <returns>A list of <see cref="IEssServerVariable"/> objects.</returns>
        public Task<List<IEssServerVariable>> GetVariablesAsync( CancellationToken cancellationToken = default );
        
        /// <summary>
        /// Gets the about information of this server
        /// </summary>

        public Task<IEssAbout> GetAboutAsync( CancellationToken cancellationToken = default );
        /// <summary>
        /// Gets the about information of this server
        /// </summary>

        public Task<IEssAboutInstance> GetAboutInstanceAsync( CancellationToken cancellationToken = default );
        /// <summary>
        /// Gets the Urls from API
        /// </summary>
        public Task<List<IEssUrl>> GetURLsAsync( CancellationToken cancellationToken = default );
        
        /// <summary>
        /// Gets the list of applications for this server available to the connected user.
        /// </summary>
        /// <returns>A list of <see cref="IEssSession"/> objects.</returns>
        public List<IEssSession> GetSessions();

        /// <summary>
        /// Gets the Sessions
        /// </summary>
        public Task<List<IEssSession>> GetSessionsAsync( CancellationToken cancellationToken = default );
    }
}
