using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssSession
    {
        /// <summary>
        /// Returns the application name.
        /// </summary>
        /// <remarks>
        /// This property only has a value if this session is a grid session.
        /// </remarks>
        public string Application { get; }

        /// <summary>
        /// Returns the connection source.
        /// </summary>
        public string ConnectionSource { get; }

        /// <summary>
        /// Returns the database name.
        /// </summary>
        /// <remarks>
        /// This property only has a value if this session is a grid session.
        /// </remarks>
        public string Database { get; }
        
        /// <summary>
        /// Returns the login time in seconds.
        /// </summary>
        public string LoginTimeInSeconds { get; }

        /// <summary>
        /// Returns the session ID.
        /// </summary>
        public long SessionId { get; }

        /// <summary>
        /// Returns the type of this session.
        /// </summary>
        public EssSessionType SessionType { get; }

        /// <summary>
        /// Returns the user ID.
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Asynchronously kills the session.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task KillAsync( CancellationToken cancellationToken = default );

        /// <summary />
        public enum EssSessionType
        {
            /// <summary />
            Server,
            /// <summary />
            Grid
        }
    }
}
