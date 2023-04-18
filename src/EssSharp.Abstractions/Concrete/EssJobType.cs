using System.ComponentModel;

namespace EssSharp
{
    /// <summary >
    public enum EssJobType
    {
        /// <summary>Unknown</summary>
        [Description("Unknown")]
        unknown = 0,
        /// <summary>Data Load</summary>
        [Description("Data Load")]
        dataload,
        /// <summary>Dimension Build</summary>
        [Description("Dimension Build")]
        dimbuild,
        /// <summary>Calc Execution</summary>
        [Description("Calc Execution")]
        calc,
        /// <summary>Clear Data</summary>
        [Description("Clear Data")]
        clear,
        /// <summary>Import Excel</summary>
        [Description("Import Excel")]
        importExcel,
        /// <summary>Export Excel</summary>
        [Description("Export Excel")]
        exportExcel,
        /// <summary>LCM Export</summary>
        [Description("LCM Export")]
        lcmExport,
        /// <summary>LCM Import</summary>
        [Description("LCM Import")]
        lcmImport,
        /// <summary>Build Aggregatio</summary>
        [Description("Build Aggregation")]
        buildAggregation,
        /// <summary>Clear Aggregation</summary>
        [Description("Clear Aggregation")]
        clearAggregation,
        /// <summary>Export Data</summary>
        [Description("Export Data")]
        exportData,
        /// <summary>MDX Script</summary>
        [Description("MDX Script")]
        mdxScript
    }
}
