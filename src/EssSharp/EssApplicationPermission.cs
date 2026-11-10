using System;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssApplicationPermission : EssObject, IEssApplicationPermission
    {
        #region Private Data

        private readonly EssApplication _application;
        private UserGroupProvisionInfo _provisionInfo;

        #endregion

        #region Constructors

        /// <summary />
        internal EssApplicationPermission( UserGroupProvisionInfo provisionInfo, EssApplication application ) : base(application?.Configuration, application?.Client)
        {
            _provisionInfo = provisionInfo ?? 
                throw new ArgumentNullException(nameof(provisionInfo), $"An API model {nameof(provisionInfo)} is required to create an {nameof(EssApplicationPermission)}.");

            _application = application ??
                throw new ArgumentNullException(nameof(application), $"An {nameof(EssServer)} {nameof(application)} is required to create an {nameof(EssApplicationPermission)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _provisionInfo?.Id;

        /// <inheritdoc />
        public override EssType Type => EssType.UserProvisions;

        #endregion

        #region IEssUserProvisions Member Propeties

        /// <inheritdoc />
        public string FullName => _provisionInfo?.Name;

        /// <inheritdoc />
        public EssApplicationRole Role => _provisionInfo.Role.ToEssApplicationRole();

        /// <inheritdoc />
        public EssPermissionType PermissionType => _provisionInfo.Group ? EssPermissionType.Group : EssPermissionType.User;

        #endregion

        #region IEssUserProvision Member Methods

        /// <inheritdoc />
        public void RemovePermissions() => RemovePermissionsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task RemovePermissionsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationRoleProvisioningApi>();

                // A group's permission is named with group=true; without it, Essbase looks for a user of that name, which
                // a server in Identity Cloud mode refuses for a group of the identity service.
                await api.ApplicationRoleProvisioningDeprovisionAsync(_application.Name, Name, group: PermissionType is EssPermissionType.Group ? true : null, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to remove permissions for {PermissionType.ToString().ToLowerInvariant()} {Name} on application ""{_application.Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplicationPermission"/> object.</returns>
        public IEssApplicationPermission UpdatePermissions( EssApplicationRole role, bool isGroup = false) => UpdatePermissionsAsync( role, isGroup).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplicationPermission"/> object.</returns>
        public async Task<IEssApplicationPermission> UpdatePermissionsAsync( EssApplicationRole role, bool isGroup = false, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationRoleProvisioningApi>();

                var body = new UserGroupProvisionInfo()
                {
                    Id = Name,
                    Role = role.ToString() ??
                        throw new ArgumentException($@"{nameof(role)} must be set."),
                    Group = isGroup
                };

                await api.ApplicationRoleProvisioningProvisionAsync(app: _application.Name, id: Name, body: body, cancellationToken: cancellationToken).ConfigureAwait(false);

                _provisionInfo = await api.ApplicationRoleProvisioningGetProvisionAsync(app: _application.Name, id: Name, group: isGroup ? true : null, cancellationToken: cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to update permissions for {(isGroup ? "group" : "user")} {Name} on application ""{_application.Name}"". {e.Message}", e);
            }
        }

        #endregion
    }
}
