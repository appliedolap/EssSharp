using System;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssUserPermission : EssObject, IEssUserPermission
    {
        #region Private Data

        private readonly EssApplication _application;
        private UserGroupProvisionInfo _provisions;

        #endregion

        #region Constructors

        /// <summary />
        internal EssUserPermission( UserGroupProvisionInfo provisions, EssApplication application ) : base(application?.Configuration, application?.Client)
        {
            _provisions = provisions ?? 
                throw new ArgumentNullException(nameof(provisions), $"An API model {nameof(provisions)} is required to create an {nameof(EssUserPermission)}.");

            _application = application ??
                throw new ArgumentNullException(nameof(application), $"An {nameof(EssServer)} {nameof(application)} is required to create an {nameof(EssUserPermission)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _provisions?.Id;

        /// <inheritdoc />
        public override EssType Type => EssType.UserProvisions;

        #endregion

        #region IEssUserProvisions Member Propeties

        /// <inheritdoc />
        public string FullName => _provisions?.Name;

        /// <inheritdoc />
        public EssUserPermissionRole Role => _provisions.Role.ToEssUserProvisionRole();

        /// <inheritdoc />
        public bool Group => _provisions.Group;

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

                await api.ApplicationRoleProvisioningDeprovisionAsync(_application.Name, Name).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to remove permissions for user {Name} on application ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns></returns>
        public IEssUserPermission UpdatePermissions( EssUserPermissionRole role) => UpdatePermissionsAsync( role).GetAwaiter().GetResult();
        
        /// <inheritdoc />
        /// <returns></returns>
        public async Task<IEssUserPermission> UpdatePermissionsAsync( EssUserPermissionRole role, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationRoleProvisioningApi>();

                var body = new UserGroupProvisionInfo()
                {
                    Id = Name,
                    Role = role.ToString() ??
                    throw new ArgumentException($@"{nameof(role)} must be set.")
                };

                await api.ApplicationRoleProvisioningProvisionAsync(app: _application.Name, id: Name, body: body, cancellationToken: cancellationToken).ConfigureAwait(false);

                _provisions = await api.ApplicationRoleProvisioningGetProvisionAsync(app: _application.Name, id: Name, cancellationToken: cancellationToken).ConfigureAwait(false);

                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to update permissions for user {Name} on application ""{Name}"". {e.Message}", e);
            }
        }

        #endregion
    }
}
