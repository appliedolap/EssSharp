using System.ComponentModel;
using System.Runtime.Serialization;

namespace EssSharp
{
    /// <summary />
    public enum EssJobType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Data Load
        /// </summary>
        Dataload = 1,

        /// <summary>
        /// Dimension Build
        /// </summary>
        Dimbuild = 2,

        /// <summary>
        /// Calc Execution
        /// </summary>
        Calc = 3,

        /// <summary>
        /// Clear Data
        /// </summary>
        Clear = 4,

        /// <summary>
        /// Import Excel
        /// </summary>
        ImportExcel = 5,

        /// <summary>
        /// Export Excel
        /// </summary>
        ExportExcel = 6,

        /// <summary>
        /// LCM Export
        /// </summary>
        LcmExport = 7,

        /// <summary>
        /// LCM Import
        /// </summary>
        LcmImport = 8,

        /// <summary>
        /// Clear Aggregation
        /// </summary>
        ClearAggregation = 9,

        /// <summary>
        /// Build Aggregation
        /// </summary>
        BuildAggregation = 10,

        /// <summary>
        /// ASO Buffer Data Load
        /// </summary>
        AsoBufferDataLoad = 11,

        /// <summary>
        /// ASO Buffer Commit
        /// </summary>
        AsoBufferCommit = 12,

        /// <summary>
        /// Export Data
        /// </summary>
        ExportData = 13,

        /// <summary>
        /// MDX Script
        /// </summary>
        MdxScript = 14,

        /// <summary>
        /// Report Script
        /// </summary>
        ExecuteReport = 15,

        /// <summary>
        /// MAXL Script
        /// </summary>
        Maxl = 16,

        /// <summary>
        /// Groovy Script
        /// </summary>
        Groovy = 17
    }
}
