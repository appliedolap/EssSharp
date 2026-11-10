namespace EssSharp
{
    /// <summary>
    /// Where an Essbase server's users and groups come from, as the server reports in its instance details. Use
    /// <see cref="IEssServer.GetSecurityMode"/> to read it.
    /// </summary>
    public enum EssSecurityMode
    {
        /// <summary>
        /// The server's security mode could not be read: the server did not answer for its instance details.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The server is not in Identity Cloud mode (its instance details do not report <c>idcs</c> as true): its users and
        /// groups come from its own security provider, which an administrator manages through the server's user and group
        /// operations.
        /// </summary>
        Native = 1,

        /// <summary>
        /// The server is in Identity Cloud mode: it looks its users and groups up in an identity service (OCI IAM or Oracle
        /// Identity Cloud Service) and keeps only their roles. It finds a single user or group by name, but refuses to
        /// list, create, edit, or delete users and groups, and to list or change a group's members.
        /// </summary>
        IdentityCloud = 2
    }
}
