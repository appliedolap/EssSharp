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
    public class EssSession : EssObject, IEssSession
    {
        #region Private Data
        private readonly SessionAttributes  _sessionAttributes;
        #endregion

        #region Constructors
        /// <summary />
        public EssSession(SessionAttributes sessionAttributes )
        {
            _sessionAttributes = sessionAttributes;
        }
        #endregion

        #region IEssSession Members

        /// <inheritdoc />
        public string UserId => _sessionAttributes.UserId;

        /// <inheritdoc />
        public long SessionId => long.Parse(_sessionAttributes.SessionId);

        /// <inheritdoc />
        public string LoginTimeInSeconds => _sessionAttributes.LoginTimeInSeconds;
        /// <inheritdoc />
        public string ConnectionSource => _sessionAttributes.ConnectionSource;

        /// <inheritdoc />
        public async Task KillAsync( bool logoff, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<SessionsApi>();
                await api.SessionsDeleteSessionWithIdAsync(SessionId, logoff, 0, cancellationToken).ConfigureAwait(false);
            }
            catch ( Exception )
            {
                throw;
            }
        }
        #endregion
    }
}
