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
        DrillthroughReportsForCells = 2
    }
}
