using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary>
    /// The members of a group: its users and its groups.
    /// </summary>
    /// <remarks>
    /// The server describes a member in brief. A user member has its ID, name and email, and a group member has its name and
    /// description; neither has a role or groups, so the <see cref="IEssUser.Role"/> of a user member and the
    /// <see cref="IEssGroup.Role"/> of a group member are <see cref="EssServerRole.Unkown"/>. Get the user or group from
    /// the server to see the rest.
    /// </remarks>
    public class EssGroupMembers
    {
        /// <summary />
        /// <param name="users">The user members.</param>
        /// <param name="groups">The group members.</param>
        public EssGroupMembers( List<IEssUser> users, List<IEssGroup> groups )
        {
            Users  = users  ?? new List<IEssUser>();
            Groups = groups ?? new List<IEssGroup>();
        }

        /// <summary>
        /// The group members.
        /// </summary>
        public List<IEssGroup> Groups { get; }

        /// <summary>
        /// The user members.
        /// </summary>
        public List<IEssUser> Users { get; }
    }
}
