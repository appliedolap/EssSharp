using System;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssUserPermission : IEssObject
    {
        /// <summary>
        /// Users full name.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Access held by user.
        /// </summary>
        public EssUserPermissionRole Role { get; }

        /// <summary>
        /// TODO: UPDATE? --copied from docs.oracle.com 
        /// 
        /// If true, consider roles derived through parent groups. 
        /// </summary>
        public bool Group { get; }

        /// <summary>
        /// Update user permissions for specified cube.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="role"></param>
        public IEssUserPermission UpdatePermissions( EssUserPermissionRole role );

        /// <summary>
        /// Asynchronously update user permissions for specified cube.
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssUserPermission> UpdatePermissionsAsync( EssUserPermissionRole role, CancellationToken cancellationToken = default );

        /// <summary>
        /// Removes user permissions from application.
        /// </summary>
        public void RemovePermissions();

        /// <summary>
        /// Removes user permissions from application.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task RemovePermissionsAsync( CancellationToken cancellationToken = default );
    }
}
