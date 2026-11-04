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
        /// Returns the description of the group. The server leaves the description out of its list of groups, so it is null
        /// for a group from <see cref="IEssServer.GetGroups()"/>; get the group with <see cref="IEssServer.GetGroup(string)"/> to see it.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Returns the groups in this Group
        /// </summary>
        public List<string> GroupNames { get; }

        /// <summary>
        /// Returns the role of this group
        /// </summary>
        public EssServerRole Role { get; }

        /// <summary>
        /// Returns the links the server gave with the group, to the operations on it.
        /// </summary>
        public List<EssLink> Links { get; }

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
        public IEssGroup Edit( EssServerRole? role = null, string description = null );

        /// <summary>
        /// Asynchronously edits the groups role or description.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssGroup> EditAsync( EssServerRole? role = null, string description = null, CancellationToken cancellationToken = default );

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

        /// <summary>
        /// Returns the members of the group, its users and its groups together.
        /// </summary>
        /// <exception cref="NotSupportedException">The server does not support managing users and groups (<see cref="EssFeature.UserGroupManagement"/>).</exception>
        public EssGroupMembers GetMembers();

        /// <summary>
        /// Asynchronously returns the members of the group, its users and its groups together.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support managing users and groups (<see cref="EssFeature.UserGroupManagement"/>).</exception>
        public Task<EssGroupMembers> GetMembersAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns what the group holds in one application: its roles, and its filters and scripts by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public EssApplicationProvisioning GetApplicationProvisioningReport( string applicationName );

        /// <summary>
        /// Asynchronously returns what the group holds in one application: its roles, and its filters and scripts by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public Task<EssApplicationProvisioning> GetApplicationProvisioningReportAsync( string applicationName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns what the group holds in each application.
        /// </summary>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public List<EssApplicationProvisioning> GetApplicationProvisioningReports();

        /// <summary>
        /// Asynchronously returns what the group holds in each application.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public Task<List<EssApplicationProvisioning>> GetApplicationProvisioningReportsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the group's provisioning report: the service roles it holds, and what it holds in each application.
        /// </summary>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public EssProvisioningReport GetProvisioningReport();

        /// <summary>
        /// Asynchronously returns the group's provisioning report: the service roles it holds, and what it holds in each application.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public Task<EssProvisioningReport> GetProvisioningReportAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the filters the group holds in an application, by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public List<EssDatabaseProvisioning> GetProvisionedFilters( string applicationName );

        /// <summary>
        /// Asynchronously returns the filters the group holds in an application, by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public Task<List<EssDatabaseProvisioning>> GetProvisionedFiltersAsync( string applicationName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the application roles the group holds in an application.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public List<EssProvisionedItem> GetProvisionedRoles( string applicationName );

        /// <summary>
        /// Asynchronously returns the application roles the group holds in an application.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public Task<List<EssProvisionedItem>> GetProvisionedRolesAsync( string applicationName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the calculation scripts the group holds in an application, by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public List<EssDatabaseProvisioning> GetProvisionedScripts( string applicationName );

        /// <summary>
        /// Asynchronously returns the calculation scripts the group holds in an application, by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public Task<List<EssDatabaseProvisioning>> GetProvisionedScriptsAsync( string applicationName, CancellationToken cancellationToken = default );
    }
}
