using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssGroup : IEssObject
    {
        /// <summary>
        /// Returns the Server associated with the group
        /// </summary>
        public IEssServer Server { get; }

        /// <summary>
        /// Returns the description of the group
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Returns the groups in this Group
        /// </summary>
        public List<string> GroupNames { get; }

        /// <summary>
        /// Returns the role of this group
        /// </summary>
        public string Role { get; }

        /// <summary>
        /// Returns a list of <see cref="IEssUser"/> objects.
        /// </summary>
        public List<IEssUser> GetUsers();

        /// <summary>
        /// Returns a list of <see cref="IEssUser"/> objects.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssUser>> GetUsersAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Get child groups of a parent Group
        /// </summary>
        /// <returns></returns>
        public List<IEssGroup> GetGroups();

        /// <summary>
        /// Gets child groups of a parent group
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<IEssGroup>> GetGroupsAsync( CancellationToken cancellationToken = default );

    }
}
