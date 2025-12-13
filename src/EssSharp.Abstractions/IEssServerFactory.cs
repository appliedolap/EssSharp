using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace EssSharp
{
    /// <summary />
    public interface IEssServerFactory
    {
        /// <summary>
        /// Gets or sets the <see cref="ILogger"/> used to log information, warnings, and errors from any <see cref="IEssServer"/> created by this factory.
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of concurrent requests by any <see cref="IEssServer"/> created by this factory.
        /// </summary>
        public int MaxDegreeOfParallelism { get; set; }

        /// <summary>
        /// Gets or sets the tmeout of requests by any <see cref="IEssServer"/> created by this factory.
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// Gets or sets the user agent of any <see cref="IEssServer"/> created by this factory.
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Creates a new server and, optionally, connects with the given credentials.
        /// </summary>
        /// <param name="server" />
        /// <param name="oauthToken" />
        /// <param name="connect">(optional) Whether to create a user session.</param>
        /// <returns>A new <see cref="IEssServer"/> instance.</returns>
        public IEssServer CreateEssServer( string server, string oauthToken, bool connect = true );

        /// <summary>
        /// Creates a new server and, optionally, connects asynchronously with the given credentials.
        /// </summary>
        /// <param name="server" />
        /// <param name="oauthToken" />
        /// <param name="connect">(optional) Whether to asynchronously create a user session.</param>/>
        /// <param name="cancellationToken" />
        public Task<IEssServer> CreateEssServerAsync( string server, string oauthToken, bool connect = true, CancellationToken cancellationToken = default );

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
