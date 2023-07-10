using System.ComponentModel;
using System.Runtime.Serialization;

namespace EssSharp
{
    /// <summary />
    public enum EssJobType
    {
        /// <summary>Unknown</summary>
        [Description("Unknown")]
        Unknown = 0,

        /// <summary>Data Load</summary>
        [Description("Data Load")]
        Dataload = 1,

        /// <summary>Dimension Build</summary>
        [Description("Dimension Build")]
        Dimbuild = 2,

        /// <summary>Calc Execution</summary>
        [Description("Calc Execution")]
        Calc = 3,

        /// <summary>Clear Data</summary>
        [Description("Clear Data")]
        Clear = 4,

        /// <summary>Import Excel</summary>
        [Description("Import Excel")]
        ImportExcel = 5,

        /// <summary>Export Excel</summary>
        [Description("Export Excel")]
        ExportExcel = 6,

        /// <summary>LCM Export</summary>
        [Description("LCM Export")]
        LcmExport = 7,

        /// <summary>LCM Import</summary>
        [Description("LCM Import")]
        LcmImport = 8,

        /// <summary>Clear Aggregation</summary>
        [Description("Clear Aggregation")]
        ClearAggregation = 9,

        /// <summary>Build Aggregation</summary>
        [Description("Build Aggregation")]
        BuildAggregation = 10,

        /// <summary>ASO Buffer Data Load</summary>
        [Description("ASO Buffer Data Load")]
        [EnumMember(Value = "asoBufferDataLoad")]
        AsoBufferDataLoad = 11,

        /// <summary>ASO Buffer Commit</summary>
        [Description("ASO Buffer Commit")]
        [EnumMember(Value = "asoBufferCommit")]
        AsoBufferCommit = 12,

        /// <summary>Export Data</summary>
        [Description("Export Data")]
        ExportData = 13,

        /// <summary>MDX Script</summary>
        [Description("MDX Script")]
        MdxScript = 14,

        /// <summary>Report Script</summary>
        [Description("Report Script")]
        ExecuteReport = 15,

        /// <summary>MAXL Script</summary>
        [Description("MAXL Script")]
        MAXLScript = 16

    }
}
