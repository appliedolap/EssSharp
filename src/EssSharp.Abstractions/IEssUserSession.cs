using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssUserSession
    {
        /// <summary>
        /// Returns the token associated with this user session.
        /// </summary>
        /// <remarks>The token is only available if explicitly captured.</remarks>
        public string Token { get; }

        /// <summary>
        /// Returns the user ID associated with this user session.
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Returns the names of the groups that the user of this session belongs to.
        /// </summary>
        /// <remarks>The groups are only available if explicitly captured.</remarks>
        public List<string> GroupNames { get; }
    }

}
