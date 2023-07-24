using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssGroup : EssObject, IEssGroup
    {
        #region Private Data

        private readonly EssServer  _server;
        private GroupBean _group;

        #endregion

        #region Constructors

        /// <summary />
        internal EssGroup( GroupBean group, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _group = group ?? 
                throw new ArgumentNullException(nameof(group), $"An API model {nameof(group)} is required to create an {nameof(EssGroup)}.");

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssGroup)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _group?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Group;

        #endregion

        #region IEssServer Properties

        /// <inheritdoc />
        public IEssServer Server => _server;

        /// <inheritdoc />
        public string Description => _group?.Description;

        /// <inheritdoc />
        public List<string> GroupNames => _group.Groups;

        /// <inheritdoc />
        public EssServerRole Role => _group.Role.ToEssServerRole();

        #endregion

        #region IEssGroup Members

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUser"/> objects.</returns>
        public List<IEssUser> AddUsers( List<string> userIds ) => AddUsersAsync( userIds ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUser"/> objects.</returns>
        public async Task<List<IEssUser>> AddUsersAsync( List<string> userIds, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();

                if ( await api.GroupsAddUserMembersToGroupAsync(id: Name, body: userIds, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } users )
                    throw new Exception("Cannot add user(s)");

                return users?.ToEssSharpList(_server) ?? new List<IEssUser>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to add user to group ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssGroup"/> objects.</returns>
        public List<IEssGroup> AddGroups( List<string> groupIds ) => AddGroupsAsync(groupIds).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssGroup"/> objects.</returns>
        public async Task<List<IEssGroup>> AddGroupsAsync( List<string> groupIds, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();

                if ( await api.GroupsAddGroupMembersToGroupAsync(id: Name, body: groupIds, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } groups )
                    throw new Exception("Cannot add user(s)");

                return groups?.ToEssSharpList(_server) ?? new List<IEssGroup>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to add user to group ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A <see cref="IEssGroup"/> object.</returns>
        public IEssGroup Edit( EssServerRole? role = null, string description = null ) => EditAsync( role, description ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A <see cref="IEssGroup"/> object.</returns>
        public async Task<IEssGroup> EditAsync( EssServerRole? role = null, string description = null, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();

                var body = new GroupBean()
                {
                    Role = role?.EssServerRoleToString() ?? null,
                    Description = description
                };

                if ( await api.GroupsEditAsync(id: Name, body: body, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } groupBean )
                    throw new Exception("cannot edit group.");

                _group = groupBean;

                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to edit group ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public void Delete() => DeleteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();

                await api.GroupsDeleteAsync(id: Name, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to delete group ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUser"/> objects.</returns>
        public List<IEssUser> GetUsers() => GetUsersAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUser"/> objects.</returns>
        public async Task<List<IEssUser>> GetUsersAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();
                if (await api.GroupsGetUserMembersOfGroupAsync(id: Name, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } users )
                    throw new Exception("Could not get members of group.");

                return users?.ToEssSharpList(Server as EssServer) ?? new List<IEssUser>();
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get members of group: ""{Name}"". {e.Message}", e);
            }
        }
        
        /// <inheritdoc />
        /// <returns> a List of <see cref="IEssGroup"/> objects. </returns>
        public List<IEssGroup> GetGroups() => GetGroupsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A Task<list> of <see cref="IEssGroup"/> objects. </list></returns>
        public async Task<List<IEssGroup>> GetGroupsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();
                if ( await api.GroupsGetGroupMembersOfGroupAsync(id: Name, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } groups )
                    throw new Exception("Could not get groups.");

                return groups?.ToEssSharpList(Server as EssServer) ?? new List<IEssGroup>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get group ""{Name}"" groups. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public void RemoveGroups( List<string> groupIds ) => RemoveGroupsAsync(groupIds).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task RemoveGroupsAsync( List<string> groupIds, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();

                await api.GroupsRemoveGroupMembersFromGroupAsync(id: Name, body: groupIds, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to remove group(s) from group ""{Name}"" groups. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public void RemoveUsers(List<string> userIds ) => RemoveUsersAsync( userIds ).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task RemoveUsersAsync( List<string> userIds, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GroupsApi>();

                await api.GroupsRemoveUserMembersFromGroupAsync(id: Name, body: userIds, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to remove user(s) from group ""{Name}"" groups. {e.Message}", e);
            }
        }

        #endregion
    }
}
