using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssUser : EssObject, IEssUser
    {
        #region Private Data

        private readonly EssServer  _server;
        private UserBean _user;

        #endregion

        #region Constructors

        /// <summary />
        internal EssUser( UserBean user, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _user = user ??
                throw new ArgumentNullException(nameof(user), $"An API model {nameof(user)} is required to create an {nameof(EssUser)}.");

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssUser)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _user?.Id;

        /// <inheritdoc />
        public override EssType Type => EssType.User;

        #endregion

        #region IEssUser Properties

        /// <inheritdoc />
        public IEssServer Server => _server;

        /// <inheritdoc />
        public string FullName => _user?.Name;

        /// <inheritdoc />
        public string Email => _user?.Email;

        /// <inheritdoc />
        public EssServerRole Role => _user.Role.ToEssServerRole();

        /// <inheritdoc />
        public List<string> GroupNames => _user?.Groups;

        #endregion

        #region IEssUser Members

        /// <inheritdoc />
        public void Delete() => DeleteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<UsersApi>();

                await api.UsersDeleteAsync(id: Name, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to delete user ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>This <see cref="IEssUser"/>, as edited.</returns>
        public IEssUser Edit( string fullName = null, string email = null, EssServerRole? role = null, string password = null ) =>
            EditAsync(fullName, email, role, password).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>This <see cref="IEssUser"/>, as edited.</returns>
        public async Task<IEssUser> EditAsync( string fullName = null, string email = null, EssServerRole? role = null, string password = null, CancellationToken cancellationToken = default )
        {
            var api = GetApi<UsersApi>();

            return await _server.RequestAsync(EssFeature.UserGroupManagement, async () =>
            {
                // The server replaces the whole user, and requires a role, so what the caller leaves out is read from the user as the server has it.
                // A user sent without a name or email is left with none (the server stores the name as the text "null"). The server's answer for
                // one user is not always as complete as its list of users (for the built-in admin user it leaves out the email), so a value it
                // leaves empty is taken from this user as it was last read. The groups are not: a user sent without groups is taken out of every
                // group they are in, and the server leaves the groups out of its answer for a user in none, so what it says is what is sent.
                var current = await api.UsersGetAsync(id: Name, cancellationToken: cancellationToken).ConfigureAwait(false) ?? _user;

                var body = new UserBean()
                {
                    Name     = fullName ?? (string.IsNullOrEmpty(current.Name)  ? _user.Name  : current.Name),
                    Email    = email    ?? (string.IsNullOrEmpty(current.Email) ? _user.Email : current.Email),
                    Role     = role?.EssServerRoleToString() ?? (string.IsNullOrEmpty(current.Role) ? _user.Role : current.Role).ToServerRoleName(),
                    Password = password,
                    Groups   = current.Groups
                };

                if ( await api.UsersPUTAsync(id: Name, body: body, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } user )
                    throw new Exception("The server did not return the edited user.");

                // The server answers with the password it was given. Do not keep it.
                user.Password = null;

                _user = user;

                return this as IEssUser;
            }, $@"Unable to edit user ""{Name}"".", cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public EssApplicationProvisioning GetApplicationProvisioningReport( string applicationName ) =>
            GetApplicationProvisioningReportAsync(applicationName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<EssApplicationProvisioning> GetApplicationProvisioningReportAsync( string applicationName, CancellationToken cancellationToken = default )
        {
            var api = GetApi<UserProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.UserProvisioningReportGetReportForApplicationAsync(application: applicationName, userId: Name, expand: "all", cancellationToken: cancellationToken),
                $@"Unable to get the provisioning report of user ""{Name}"" for application ""{applicationName}"".", cancellationToken).ConfigureAwait(false)).ToEssApplicationProvisioning();
        }

        /// <inheritdoc />
        public List<EssApplicationProvisioning> GetApplicationProvisioningReports() =>
            GetApplicationProvisioningReportsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<List<EssApplicationProvisioning>> GetApplicationProvisioningReportsAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<UserProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.UserProvisioningReportGetReportForAllApplicationsAsync(userId: Name, expand: "all", cancellationToken: cancellationToken),
                $@"Unable to get the provisioning report of user ""{Name}"" for the applications.", cancellationToken).ConfigureAwait(false)).ToEssApplicationProvisionings();
        }

        /// <inheritdoc />
        public List<EssDatabaseProvisioning> GetProvisionedFilters( string applicationName ) =>
            GetProvisionedFiltersAsync(applicationName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<List<EssDatabaseProvisioning>> GetProvisionedFiltersAsync( string applicationName, CancellationToken cancellationToken = default )
        {
            var api = GetApi<UserProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.UserProvisioningReportGetFiltersAsync(application: applicationName, userId: Name, cancellationToken: cancellationToken),
                $@"Unable to get the filters of user ""{Name}"" in application ""{applicationName}"".", cancellationToken).ConfigureAwait(false)).ToEssDatabaseProvisionings();
        }

        /// <inheritdoc />
        public List<EssProvisionedItem> GetProvisionedRoles( string applicationName ) =>
            GetProvisionedRolesAsync(applicationName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<List<EssProvisionedItem>> GetProvisionedRolesAsync( string applicationName, CancellationToken cancellationToken = default )
        {
            var api = GetApi<UserProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.UserProvisioningReportGetRolesAsync(application: applicationName, userId: Name, cancellationToken: cancellationToken),
                $@"Unable to get the roles of user ""{Name}"" in application ""{applicationName}"".", cancellationToken).ConfigureAwait(false)).ToEssProvisionedItems();
        }

        /// <inheritdoc />
        public List<EssDatabaseProvisioning> GetProvisionedScripts( string applicationName ) =>
            GetProvisionedScriptsAsync(applicationName).GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<List<EssDatabaseProvisioning>> GetProvisionedScriptsAsync( string applicationName, CancellationToken cancellationToken = default )
        {
            var api = GetApi<UserProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.UserProvisioningReportGetScriptsAsync(application: applicationName, userId: Name, cancellationToken: cancellationToken),
                $@"Unable to get the scripts of user ""{Name}"" in application ""{applicationName}"".", cancellationToken).ConfigureAwait(false)).ToEssDatabaseProvisionings();
        }

        /// <inheritdoc />
        public EssProvisioningReport GetProvisioningReport() => GetProvisioningReportAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<EssProvisioningReport> GetProvisioningReportAsync( CancellationToken cancellationToken = default )
        {
            var api = GetApi<UserProvisioningReportApi>();

            return (await _server.RequestAsync(EssFeature.ProvisioningReports,
                () => api.UserProvisioningReportGetFullReportAsync(userId: Name, expand: "all", cancellationToken: cancellationToken),
                $@"Unable to get the provisioning report of user ""{Name}"".", cancellationToken).ConfigureAwait(false)).ToEssProvisioningReport();
        }

        #endregion
    }
}
