using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssUser : IEssObject
    {
        /// <summary>
        /// Return the server associated with the user
        /// </summary>
        public IEssServer Server { get; }

        /// <summary>
        /// Return the display name
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Return the user email
        /// </summary>
        public string Email { get; }

        public EssServerRole Role { get; }

        public List<string> GroupNames { get; }

        /// <summary>
        /// Deletes a user from the server.
        /// </summary>
        /// <remarks>Service administrator privilege is required. The server refuses any other user with a "not authorized" error, which this throws as an exception.</remarks>
        public void Delete();

        /// <summary>
        /// Asynchronously deletes a user from the server.
        /// </summary>
        /// <remarks>Service administrator privilege is required. The server refuses any other user with a "not authorized" error, which this throws as an exception.</remarks>
        public Task DeleteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Edits the user's display name, email, role or password. What is left out (null) is kept as it is.
        /// </summary>
        /// <param name="fullName">The new display name.</param>
        /// <param name="email">The new email.</param>
        /// <param name="role">The new role.</param>
        /// <param name="password">The new password.</param>
        /// <returns>This user, as edited.</returns>
        /// <exception cref="NotSupportedException">The server does not support managing users and groups (<see cref="EssFeature.UserGroupManagement"/>).</exception>
        /// <remarks>Service administrator privilege is required. The server refuses any other user with a "not authorized" error, which this throws as an exception.</remarks>
        public IEssUser Edit( string fullName = null, string email = null, EssServerRole? role = null, string password = null );

        /// <summary>
        /// Asynchronously edits the user's display name, email, role or password. What is left out (null) is kept as it is.
        /// </summary>
        /// <param name="fullName">The new display name.</param>
        /// <param name="email">The new email.</param>
        /// <param name="role">The new role.</param>
        /// <param name="password">The new password.</param>
        /// <param name="cancellationToken" />
        /// <returns>This user, as edited.</returns>
        /// <exception cref="NotSupportedException">The server does not support managing users and groups (<see cref="EssFeature.UserGroupManagement"/>).</exception>
        /// <remarks>Service administrator privilege is required. The server refuses any other user with a "not authorized" error, which this throws as an exception.</remarks>
        public Task<IEssUser> EditAsync( string fullName = null, string email = null, EssServerRole? role = null, string password = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns what the user holds in one application: its roles, and its filters and scripts by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public EssApplicationProvisioning GetApplicationProvisioningReport( string applicationName );

        /// <summary>
        /// Asynchronously returns what the user holds in one application: its roles, and its filters and scripts by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public Task<EssApplicationProvisioning> GetApplicationProvisioningReportAsync( string applicationName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns what the user holds in each application.
        /// </summary>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        /// <remarks>For a user who is not a service administrator the server answers with an empty report instead of refusing, so an empty report does not show that nothing is provisioned.</remarks>
        public List<EssApplicationProvisioning> GetApplicationProvisioningReports();

        /// <summary>
        /// Asynchronously returns what the user holds in each application.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        /// <remarks>For a user who is not a service administrator the server answers with an empty report instead of refusing, so an empty report does not show that nothing is provisioned.</remarks>
        public Task<List<EssApplicationProvisioning>> GetApplicationProvisioningReportsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the filters the user holds in an application, by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public List<EssDatabaseProvisioning> GetProvisionedFilters( string applicationName );

        /// <summary>
        /// Asynchronously returns the filters the user holds in an application, by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public Task<List<EssDatabaseProvisioning>> GetProvisionedFiltersAsync( string applicationName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the application roles the user holds in an application.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public List<EssProvisionedItem> GetProvisionedRoles( string applicationName );

        /// <summary>
        /// Asynchronously returns the application roles the user holds in an application.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public Task<List<EssProvisionedItem>> GetProvisionedRolesAsync( string applicationName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the calculation scripts the user holds in an application, by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public List<EssDatabaseProvisioning> GetProvisionedScripts( string applicationName );

        /// <summary>
        /// Asynchronously returns the calculation scripts the user holds in an application, by cube.
        /// </summary>
        /// <param name="applicationName">The name of the application.</param>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        public Task<List<EssDatabaseProvisioning>> GetProvisionedScriptsAsync( string applicationName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Returns the user's provisioning report: the service roles it holds, and what it holds in each application.
        /// </summary>
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        /// <remarks>For a user who is not a service administrator the server answers with an empty report instead of refusing, so an empty report does not show that nothing is provisioned.</remarks>
        public EssProvisioningReport GetProvisioningReport();

        /// <summary>
        /// Asynchronously returns the user's provisioning report: the service roles it holds, and what it holds in each application.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <exception cref="NotSupportedException">The server does not support provisioning reports (<see cref="EssFeature.ProvisioningReports"/>).</exception>
        /// <remarks>For a user who is not a service administrator the server answers with an empty report instead of refusing, so an empty report does not show that nothing is provisioned.</remarks>
        public Task<EssProvisioningReport> GetProvisioningReportAsync( CancellationToken cancellationToken = default );
    }
}
