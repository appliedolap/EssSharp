using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace EssSharp
{
    /// <summary>
    /// A factory for creating new <see cref="EssServer"/> instances.
    /// </summary>
    public class EssServerFactory : IEssServerFactory
    {
        /// <inheritdoc />
        public ILogger Logger { get; set; }

        /// <inheritdoc />
        /// <remarks>The default number of concurrent requests is <c>4</c>.</remarks>
        public int MaxDegreeOfParallelism { get; set; } = 4;

        /// <inheritdoc />
        /// <remarks>The default timout is <see cref="int.MaxValue"/> milliseconds.</remarks>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMilliseconds(int.MaxValue);

        /// <inheritdoc />
        /// <remarks>The default user agent is EssSharp/{version}.</remarks>
        public string UserAgent { get; set; } = @$"{nameof(EssSharp)}/{typeof(EssServer).Assembly.GetName().Version}";

        /// <inheritdoc />
        /// <returns>An <see cref="EssServer" /> object.</returns>
        public IEssServer CreateEssServer( string server, string oauthToken, bool connect = true )
            => CreateEssServerAsync(server, oauthToken, connect).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssServer" /> object.</returns>
        public async Task<IEssServer> CreateEssServerAsync( string server, string oauthToken, bool connect = true, CancellationToken cancellationToken = default )
        {
            var essServer = new EssServer(server, oauthToken);
            {
                essServer.Configuration.Logger                 = Logger;
                essServer.Configuration.MaxDegreeOfParallelism = MaxDegreeOfParallelism;
                essServer.Configuration.Timeout                = Timeout;
                essServer.Configuration.UserAgent              = UserAgent;
            }

            if ( connect )
                await essServer.SignInAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return essServer;
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssServer" /> object.</returns>
        public IEssServer CreateEssServer( string server, string username, string password, bool connect = true ) 
            => CreateEssServerAsync(server, username, password, connect).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssServer" /> object.</returns>
        public async Task<IEssServer> CreateEssServerAsync( string server, string username, string password, bool connect = true, CancellationToken cancellationToken = default )
        {
            var essServer = new EssServer(server, username, password);
            {
                essServer.Configuration.Logger                 = Logger;
                essServer.Configuration.MaxDegreeOfParallelism = MaxDegreeOfParallelism;
                essServer.Configuration.Timeout                = Timeout;
                essServer.Configuration.UserAgent              = UserAgent;
            }

            if ( connect )
                await essServer.SignInAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return essServer;
        }
    }
}
