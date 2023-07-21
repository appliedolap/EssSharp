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
        public EssUserRole Role { get; }

        /// <summary>
        /// Adds a group or groups by id to a group.
        /// </summary>
        /// <param name="groupIds"></param>
        public List<IEssGroup> AddGroups( List<string> groupIds );
        
        /// <summary>
        /// Asynchronously adds a group or groups by id to a group.
        /// </summary>
        /// <param name="groupIds"></param>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssGroup>> AddGroupsAsync( List<string> groupIds, CancellationToken cancellationToken = default );

        /// <summary>
        /// Add user or users by id to group.
        /// </summary>
        /// <param name="userIds"></param>
        public List<IEssUser> AddUsers( List<string> userIds );

        /// <summary>
        /// Asynchronously add user by id to group.
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssUser>> AddUsersAsync( List<string> userIds, CancellationToken cancellationToken = default );

        /// <summary>
        /// Edits the groups name, role, or description.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="description"></param>
        public IEssGroup Edit( EssUserRole? role = null, string description = null );

        /// <summary>
        /// Asynchronously edits the groups role or description.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGroup> EditAsync( EssUserRole? role = null, string description = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Delete group.
        /// </summary>
        public void Delete();

        /// <summary>
        /// Asynchronously delete group.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task DeleteAsync( CancellationToken cancellationToken = default );

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
        public List<IEssGroup> GetGroups();

        /// <summary>
        /// Gets child groups of a parent group
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssGroup>> GetGroupsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Remove group or groups from group.
        /// </summary>
        /// <param name="groupIds"></param>
        public void RemoveGroups( List<string> groupIds );

        /// <summary>
        /// Asynchronously remove group or groups from group.
        /// </summary>
        /// <param name="groupIds"></param>
        /// <param name="cancellationToken"></param>
        public Task RemoveGroupsAsync( List<string> groupIds, CancellationToken cancellationToken = default );

        /// <summary>
        /// Remove a user or users from group.
        /// </summary>
        /// <param name="userIds"></param>
        public void RemoveUsers( List<string> userIds );

        /// <summary>
        /// Asynchronously remove a user or users from group.
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="cancellationToken"></param>
        public Task RemoveUsersAsync( List<string> userIds, CancellationToken cancellationToken = default );

    }
}
