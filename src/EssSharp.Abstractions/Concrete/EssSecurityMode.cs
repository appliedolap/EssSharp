namespace EssSharp
{
    /// <summary>
    /// Where an Essbase server's users and groups come from, as the server reports in its instance details. Use
    /// <see cref="IEssServer.GetSecurityMode"/> to read it.
    /// </summary>
    public enum EssSecurityMode
    {
        /// <summary>
        /// The server does not report its security mode (it does not report <c>idcs</c> in its instance details, or does
        /// not answer for them).
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The server is not in Identity Cloud mode: its users and groups come from its own security provider, which an
        /// administrator manages through the server's user and group operations.
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
