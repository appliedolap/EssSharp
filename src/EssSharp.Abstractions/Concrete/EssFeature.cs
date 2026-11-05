namespace EssSharp
{
    /// <summary>
    /// A feature that some Essbase servers support and others do not. Use <see cref="IEssServer.Supports(EssFeature)"/>
    /// to find out whether a server supports it before calling the members that need it.
    /// </summary>
    public enum EssFeature
    {
        /// <summary>
        /// Executing a drillthrough report with <see cref="IEssDrillthroughReport"/>.Execute (Essbase 21.4 and later).
        /// </summary>
        DrillthroughReportExecution = 1,

        /// <summary>
        /// Getting the drillthrough reports for a set of grid cells with <see cref="IEssGrid"/>.GetDrillThroughReportForCells
        /// (Essbase 21.7 and later).
        /// </summary>
        DrillthroughReportsForCells = 2,

        /// <summary>
        /// The AI features as a whole, which the server reports as enabled or not. Each AI capability below also needs it.
        /// </summary>
        AI = 3,

        /// <summary>
        /// Generating MDX from a question in natural language, with AI.
        /// </summary>
        AIMdxGeneration = 4,

        /// <summary>
        /// Ask Essbase, which answers questions about a cube in natural language, with AI.
        /// </summary>
        AIAskEssbase = 5,

        /// <summary>
        /// Generating calculation scripts, with AI.
        /// </summary>
        AICalcGeneration = 6,

        /// <summary>
        /// Searching an outline by meaning rather than by name, with AI.
        /// </summary>
        AISemanticSearch = 7,

        /// <summary>
        /// Provisioning users and groups with service roles. Not available in EPM Shared Services security mode.
        /// </summary>
        ServiceRoleProvisioning = 8,

        /// <summary>
        /// Provisioning users and groups with application roles. Not available in EPM Shared Services security mode.
        /// </summary>
        ApplicationRoleProvisioning = 9,

        /// <summary>
        /// Resetting a user's password. Not available in EPM Shared Services security mode.
        /// </summary>
        PasswordReset = 10,

        /// <summary>
        /// Managing the users and groups the server holds: updating, deleting several at once, searching, importing and exporting
        /// them, and managing group members. Not available in Identity Cloud mode, where the users and groups live in the identity
        /// service: such a server reports the mode, so it is reported as not supported, and its members throw
        /// <see cref="System.NotSupportedException"/>.
        /// </summary>
        /// <remarks>
        /// Whether the server supports this does not show what the connected user may do: managing users and groups takes service
        /// administrator privilege, and the server refuses any other user. Nor is EPM Shared Services security mode, which the server
        /// does not report, ruled out in advance.
        /// </remarks>
        UserGroupManagement = 11,

        /// <summary>
        /// Getting the provisioning report of a user or group, which lists the roles, filters and scripts it holds. Available in
        /// Identity Cloud mode too.
        /// </summary>
        /// <remarks>
        /// Whether the server supports this does not show what the connected user may read: a user who is not a service administrator
        /// gets an empty report for another user.
        /// </remarks>
        ProvisioningReports = 12
    }
}
