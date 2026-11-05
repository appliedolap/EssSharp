namespace EssSharp
{
    /// <summary>
    /// The service role that a user or group holds on the server, as the server's service role provisioning reports it.
    /// </summary>
    public class EssServiceRoleAssignment
    {
        /// <summary />
        /// <param name="id">The ID of the user, or the name of the group.</param>
        /// <param name="type">Whether the role is held by a user or a group.</param>
        /// <param name="role">The service role.</param>
        public EssServiceRoleAssignment( string id, EssPermissionType type, EssServerRole role )
        {
            Id   = id;
            Type = type;
            Role = role;
        }

        /// <summary>
        /// The ID of the user, or the name of the group.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The service role.
        /// </summary>
        public EssServerRole Role { get; }

        /// <summary>
        /// Whether the role is held by a user (<see cref="EssPermissionType.User"/>) or a group (<see cref="EssPermissionType.Group"/>).
        /// </summary>
        public EssPermissionType Type { get; }

        /// <inheritdoc />
        public override string ToString() => $"{Type} {Id}: {Role}";
    }
}
