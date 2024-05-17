using System.ComponentModel;

namespace EssSharp
{
    /// <summary />
    public enum EssJobStatus
    {
        /// <summary>
        /// New
        /// </summary>
        New = -1,
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown,
        /// <summary>
        /// Completed
        /// </summary>
        Completed,
        /// <summary>
        /// Completed With Warnings
        /// </summary>
        CompletedWithWarnings,
        /// <summary>
        /// Failed
        /// </summary>
        Failed,
        /// <summary>
        /// In Progress
        /// </summary>
        InProgress,
    }
}
