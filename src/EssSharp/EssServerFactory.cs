using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary>
    /// A factory for creating new <see cref="EssServer"/> instances.
    /// </summary>
    public class EssServerFactory : IEssServerFactory
    {
        /// <inheritdoc />
        /// <returns>An <see cref="EssServer" /> object.</returns>
        public IEssServer CreateEssServer( string server, string username, string password, bool connect = true ) 
            => CreateEssServerAsync(server, username, password, connect).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssServer" /> object.</returns>
        public async Task<IEssServer> CreateEssServerAsync( string server, string username, string password, bool connect = true, CancellationToken cancellationToken = default )
        {
            var essServer = new EssServer(server, username, password);

            if ( connect )
                await essServer.SignInAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return essServer;
        }
    }
}
