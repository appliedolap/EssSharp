using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssSession : EssObject,IEssSession
    {
        #region Private Data
        private readonly SessionAttributes  _sessionattributes;
        #endregion

        #region Constructors
        /// <summary />
        public EssSession(SessionAttributes sessionattributes )
        {
            _sessionattributes = sessionattributes;
        }
        #endregion

        #region IEssSession Members

        /// <inheritdoc />
        public string UserId =>_sessionattributes.UserId;

        /// <inheritdoc />
        public long SessionId => long.Parse(_sessionattributes.SessionId);
        /// <inheritdoc />
        public string LoginTimeInSeconds => _sessionattributes.LoginTimeInSeconds;
        /// <inheritdoc />
        public string ConnectionSource => _sessionattributes.ConnectionSource;

        /// <inheritdoc />
        public async Task KillAsync( bool logoff, CancellationToken cancellationToken = default )
        {
            try
            {
               // logger.info("Killing session {}, logging off: {}", SessionId, logoff);
                var api = GetApi<SessionsApi>();
                await api.SessionsDeleteSessionWithIdAsync(SessionId, logoff, 0, cancellationToken);
            }
            catch ( Exception )
            {
                throw;
            }
        }

        #endregion
    }
}
