using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    public interface IEssServerFactory
    {
        /// <summary>
        /// Creates a new server and, optionally, connects with the given credentials.
        /// </summary>
        /// <param name="server" />
        /// <param name="username" />
        /// <param name="password" />
        /// <param name="connect">(optional) Whether to create a user session.</param>
        /// <returns>A new <see cref="IEssServer"/> instance.</returns>
        public IEssServer CreateEssServer( string server, string username, string password, bool connect = true );

        /// <summary>
        /// Creates a new server and, optionally, connects asynchronously with the given credentials.
        /// </summary>
        /// <param name="server" />
        /// <param name="username" />
        /// <param name="password" />
        /// <param name="connect">(optional) Whether to asynchronously create a user session.</param>/>
        /// <param name="cancellationToken" />
        public Task<IEssServer> CreateEssServerAsync( string server, string username, string password, bool connect = true, CancellationToken cancellationToken = default );
    }
}
