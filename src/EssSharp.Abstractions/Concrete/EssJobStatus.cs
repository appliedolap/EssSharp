using System.ComponentModel;

namespace EssSharp
{
    /// <summary />
    public enum EssJobStatus
    {
        /// <summary>New</summary>
        [Description("New")]
        New = -1,
        /// <summary>Unknown</summary>
        [Description("Unknown")]
        Unknown,
        /// <summary>Completed</summary>
        [Description("Completed")]
        Completed,
        /// <summary>Completed With Warnings</summary>
        [Description("Completed With Warnings")]
        CompletedWithWarnings,
        /// <summary>Failed</summary>
        [Description("Failed")]
        Failed,
        /// <summary>In Progress</summary>
        [Description("In Progress")]
        InProgress,
    }
}
