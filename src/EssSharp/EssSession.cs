using System;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssSession : EssObject, IEssSession
    {
        #region Private Data

        private readonly SessionAttributes _session;
        
        #endregion

        #region Constructors

        /// <summary />
        internal EssSession( SessionAttributes session, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _session = session ??
                throw new ArgumentNullException(nameof(session), $"An API model {nameof(session)} is required to create an {nameof(EssSession)}.");

            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssSession)}.");
        }

        #endregion

        #region IEssSession Members

        /// <inheritdoc />
        public string Application => _session.Application;

        /// <inheritdoc />
        public string ConnectionSource => _session.ConnectionSource;

        /// <inheritdoc />
        public string Database => _session.Database;

        /// <inheritdoc />
        public string LoginTimeInSeconds => _session.LoginTimeInSeconds;

        /// <inheritdoc />
        public long SessionId => long.Parse(_session.SessionId);

        /// <inheritdoc />
        public IEssSession.EssSessionType SessionType => !string.IsNullOrEmpty(Database) 
            ? IEssSession.EssSessionType.Grid 
            : IEssSession.EssSessionType.Server;

        /// <inheritdoc />
        public string UserId => _session.UserId;

        /// <inheritdoc />
        public async Task KillAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<SessionsApi>();
                await api.SessionsDeleteSessionWithIdAsync(SessionId, true, 0, cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception )
            {
                throw;
            }
        }
        #endregion
    }
}
