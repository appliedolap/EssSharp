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
            var api = GetApi<GroupsApi>();

            var users = await _server.RequestAsync(EssFeature.UserGroupManagement,
                () => api.GroupsAddUserMembersToGroupAsync(id: Name, body: userIds, cancellationToken: cancellationToken),
                $@"Unable to add users to group ""{Name}"".", cancellationToken).ConfigureAwait(false);

            return users?.ToEssSharpList(_server) ?? new List<IEssUser>();
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssGroup"/> objects.</returns>
        public List<IEssGroup> AddGroups( List<string> groupIds ) => AddGroupsAsync(groupIds).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssGroup"/> objects.</returns>
        public async Task<List<IEssGroup>> AddGroupsAsync( List<string> groupIds, CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupsApi>();

            var groups = await _server.RequestAsync(EssFeature.UserGroupManagement,
                () => api.GroupsAddGroupMembersToGroupAsync(id: Name, body: groupIds, cancellationToken: cancellationToken),
                $@"Unable to add groups to group ""{Name}"".", cancellationToken).ConfigureAwait(false);

            return groups?.ToEssSharpList(_server) ?? new List<IEssGroup>();
        }

        /// <inheritdoc />
        /// <returns>A <see cref="IEssGroup"/> object.</returns>
        public IEssGroup Edit( EssServerRole? role = null, string description = null ) => EditAsync( role, description ).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A <see cref="IEssGroup"/> object.</returns>
        public async Task<IEssGroup> EditAsync( EssServerRole? role = null, string description = null, CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupsApi>();

            return await _server.RequestAsync(EssFeature.UserGroupManagement, async () =>
            {
                // The server replaces the whole group, and requires a role, so what the caller leaves out is read from the group as the server has it.
                // That includes the groups this group is in: a group sent without them is taken out of every group it is in.
                var current = await api.GroupsGetAsync(id: Name, cancellationToken: cancellationToken).ConfigureAwait(false) ?? _group;

                var body = new GroupBean()
                {
                    Role = role?.EssServerRoleToString() ?? current.Role.ToServerRoleName(),
                    Description = description ?? current.Description,
                    Groups = current.Groups
                };

                if ( await api.GroupsEditAsync(id: Name, body: body, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } groupBean )
                    throw new Exception("cannot edit group.");

                _group = groupBean;

                return this as IEssGroup;
            }, $@"Unable to edit group ""{Name}"".", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public void Delete() => DeleteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupsApi>();

            await _server.RequestAsync(EssFeature.UserGroupManagement, async () =>
            {
                await api.GroupsDeleteAsync(id: Name, cancellationToken: cancellationToken).ConfigureAwait(false);
                return true;
            }, $@"Unable to delete group ""{Name}"".", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUser"/> objects.</returns>
        public List<IEssUser> GetUsers() => GetUsersAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="IEssUser"/> objects.</returns>
        public async Task<List<IEssUser>> GetUsersAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupsApi>();

            var users = await _server.RequestAsync(EssFeature.UserGroupManagement,
                () => api.GroupsGetUserMembersOfGroupAsync(id: Name, cancellationToken: cancellationToken),
                $@"Unable to get the users in group ""{Name}"".", cancellationToken).ConfigureAwait(false);

            return users?.ToEssSharpList(_server) ?? new List<IEssUser>();
        }
        
        /// <inheritdoc />
        /// <returns> a List of <see cref="IEssGroup"/> objects. </returns>
        public List<IEssGroup> GetGroups() => GetGroupsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A Task<list> of <see cref="IEssGroup"/> objects. </list></returns>
        public async Task<List<IEssGroup>> GetGroupsAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupsApi>();

            var groups = await _server.RequestAsync(EssFeature.UserGroupManagement,
                () => api.GroupsGetGroupMembersOfGroupAsync(id: Name, cancellationToken: cancellationToken),
                $@"Unable to get the groups in group ""{Name}"".", cancellationToken).ConfigureAwait(false);

            return groups?.ToEssSharpList(_server) ?? new List<IEssGroup>();
        }

        /// <inheritdoc />
        public void RemoveGroups( List<string> groupIds ) => RemoveGroupsAsync(groupIds).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task RemoveGroupsAsync( List<string> groupIds, CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupsApi>();

            await _server.RequestAsync(EssFeature.UserGroupManagement, async () =>
            {
                await api.GroupsRemoveGroupMembersFromGroupAsync(id: Name, body: groupIds, cancellationToken: cancellationToken).ConfigureAwait(false);
                return true;
            }, $@"Unable to remove groups from group ""{Name}"".", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public void RemoveUsers(List<string> userIds ) => RemoveUsersAsync( userIds ).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task RemoveUsersAsync( List<string> userIds, CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupsApi>();

            await _server.RequestAsync(EssFeature.UserGroupManagement, async () =>
            {
                await api.GroupsRemoveUserMembersFromGroupAsync(id: Name, body: userIds, cancellationToken: cancellationToken).ConfigureAwait(false);
                return true;
            }, $@"Unable to remove users from group ""{Name}"".", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public EssGroupMembers GetMembers() => GetMembersAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<EssGroupMembers> GetMembersAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupsApi>();

            // The server's own answer to a request for the members of a group is two links, with paths that do not work,
            // so ask for each kind of member.
            var users = await _server.RequestAsync(EssFeature.UserGroupManagement,
                () => api.GroupsGetUserMembersOfGroupAsync(id: Name, cancellationToken: cancellationToken),
                $@"Unable to get the members of group ""{Name}"".", cancellationToken).ConfigureAwait(false);

            var groups = await _server.RequestAsync(EssFeature.UserGroupManagement,
                () => api.GroupsGetGroupMembersOfGroupAsync(id: Name, cancellationToken: cancellationToken),
                $@"Unable to get the members of group ""{Name}"".", cancellationToken).ConfigureAwait(false);

            return new EssGroupMembers(users?.ToEssSharpList(_server), groups?.ToEssSharpList(_server));
        }

        /// <inheritdoc />
        public EssApplicationProvisioning GetApplicationProvisioningReport( string applicationName ) =>
            GetApplicationProvisioningReportAsync(applicationName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<EssApplicationProvisioning> GetApplicationProvisioningReportAsync( string applicationName, CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.GroupProvisioningReportGetReportForApplicationAsync(application: applicationName, groupId: Name, expand: "all", cancellationToken: cancellationToken),
                $@"Unable to get the provisioning report of group ""{Name}"" for application ""{applicationName}"".", cancellationToken).ConfigureAwait(false)).ToEssApplicationProvisioning();
        }

        /// <inheritdoc />
        public List<EssApplicationProvisioning> GetApplicationProvisioningReports() =>
            GetApplicationProvisioningReportsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<List<EssApplicationProvisioning>> GetApplicationProvisioningReportsAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.GroupProvisioningReportGetReportForAllApplicationsAsync(groupId: Name, expand: "all", cancellationToken: cancellationToken),
                $@"Unable to get the provisioning report of group ""{Name}"" for the applications.", cancellationToken).ConfigureAwait(false)).ToEssApplicationProvisionings();
        }

        /// <inheritdoc />
        public List<EssDatabaseProvisioning> GetProvisionedFilters( string applicationName ) =>
            GetProvisionedFiltersAsync(applicationName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<List<EssDatabaseProvisioning>> GetProvisionedFiltersAsync( string applicationName, CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.GroupProvisioningReportGetFiltersAsync(application: applicationName, groupId: Name, cancellationToken: cancellationToken),
                $@"Unable to get the filters of group ""{Name}"" in application ""{applicationName}"".", cancellationToken).ConfigureAwait(false)).ToEssDatabaseProvisionings();
        }

        /// <inheritdoc />
        public List<EssProvisionedItem> GetProvisionedRoles( string applicationName ) =>
            GetProvisionedRolesAsync(applicationName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<List<EssProvisionedItem>> GetProvisionedRolesAsync( string applicationName, CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.GroupProvisioningReportGetRolesAsync(application: applicationName, groupId: Name, cancellationToken: cancellationToken),
                $@"Unable to get the roles of group ""{Name}"" in application ""{applicationName}"".", cancellationToken).ConfigureAwait(false)).ToEssProvisionedItems();
        }

        /// <inheritdoc />
        public List<EssDatabaseProvisioning> GetProvisionedScripts( string applicationName ) =>
            GetProvisionedScriptsAsync(applicationName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<List<EssDatabaseProvisioning>> GetProvisionedScriptsAsync( string applicationName, CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.GroupProvisioningReportGetScriptsAsync(application: applicationName, groupId: Name, cancellationToken: cancellationToken),
                $@"Unable to get the scripts of group ""{Name}"" in application ""{applicationName}"".", cancellationToken).ConfigureAwait(false)).ToEssDatabaseProvisionings();
        }

        /// <inheritdoc />
        public EssProvisioningReport GetProvisioningReport() => GetProvisioningReportAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<EssProvisioningReport> GetProvisioningReportAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<GroupProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.GroupProvisioningReportGetFullReportAsync(groupId: Name, expand: "all", cancellationToken: cancellationToken),
                $@"Unable to get the provisioning report of group ""{Name}"".", cancellationToken).ConfigureAwait(false)).ToEssProvisioningReport();
        }

        /// <inheritdoc />
        public EssServerRole? GetServiceRole() => GetServiceRoleAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public Task<EssServerRole?> GetServiceRoleAsync( CancellationToken cancellationToken = default ) =>
            _server.GetServiceRoleAsync(Name, EssPermissionType.Group, cancellationToken);

        /// <inheritdoc />
        public void RemoveServiceRole() => RemoveServiceRoleAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task RemoveServiceRoleAsync( CancellationToken cancellationToken = default )
        {
            await _server.RemoveServiceRoleAsync(Name, EssPermissionType.Group, cancellationToken).ConfigureAwait(false);

            _group.Role = null;
        }

        /// <inheritdoc />
        public void SetServiceRole( EssServerRole role ) => SetServiceRoleAsync(role).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task SetServiceRoleAsync( EssServerRole role, CancellationToken cancellationToken = default )
        {
            await _server.SetServiceRoleAsync(Name, EssPermissionType.Group, role, cancellationToken).ConfigureAwait(false);

            _group.Role = role.EssServerRoleToString();
        }

        #endregion
    }
}
