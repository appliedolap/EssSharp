using System.Threading.Tasks;
using System.Threading;
using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssSession
    {
        /// <summary>
        /// Gets or Sets UserId
        /// </summary>
        public string UserId { get; }
       
        /// <summary>
        /// Gets or Sets SessionId
        /// </summary>
        public long SessionId { get; }

        /// <summary>
        /// Gets or Sets LoginTimeInSeconds
        /// </summary>
        public string LoginTimeInSeconds { get; }

        /// <summary>
        /// Gets or Sets ConnectionSource
        /// </summary>
        public string ConnectionSource { get; }

        /// <summary>
        /// Kill the session.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task KillAsync( bool logoff, CancellationToken cancellationToken = default );

    }


}
