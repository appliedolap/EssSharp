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
        PasswordReset = 10
    }
}
