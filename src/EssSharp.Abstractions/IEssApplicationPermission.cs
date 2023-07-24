using System;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssApplicationPermission : IEssObject
    {
        /// <summary>
        /// Users full name.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Access held by user.
        /// </summary>
        public EssApplicationRole Role { get; }

        /// <summary>
        /// If true, consider roles derived through parent groups. 
        /// </summary>
        public EssPermissionType PermissionType { get; }

        /// <summary>
        /// Removes user permissions from application.
        /// </summary>
        public void RemovePermissions();

        /// <summary>
        /// Removes user permissions from application.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task RemovePermissionsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Update user permissions for specified cube.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="isGroup">If creating a group permission, true</param>
        public IEssApplicationPermission UpdatePermissions( EssApplicationRole role, bool isGroup = false );

        /// <summary>
        /// Asynchronously update user permissions for specified cube.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="isGroup">If creating a group permission, true</param>
        /// <param name="cancellationToken"></param>
        public Task<IEssApplicationPermission> UpdatePermissionsAsync( EssApplicationRole role, bool isGroup = false, CancellationToken cancellationToken = default );
    }
}
