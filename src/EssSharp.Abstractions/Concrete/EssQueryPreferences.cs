using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace EssSharp
{
    public class EssQueryPreferences
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="axes" />
        /// <param name="dataless" />
        /// <param name="formatString" />
        /// <param name="memberIdentifier" />
        public EssQueryPreferences( EssQueryReport.ReportAxes axes = EssQueryReport.ReportAxes.ColumnsRowsAndPages, bool dataless = false, bool formatString = true, EssQueryReport.ReportMemberIdentifier memberIdentifier = EssQueryReport.ReportMemberIdentifier.Name )
        {
            Axes             = axes;
            Dataless         = dataless;
            FormatString     = formatString;
            MemberIdentifier = memberIdentifier;
        }

        /// <summary>
        /// The axis (or axes) to return in the query report.
        /// </summary>
        /// <remarks>This is supported only for <see cref="IEssMdxScript" /> and <see cref="IEssReportScript"/> query reports. Defaults to <see cref="EssQueryReport.ReportAxes.ColumnsRowsAndPages" />.</remarks>
        public EssQueryReport.ReportAxes Axes { get; set; } = EssQueryReport.ReportAxes.ColumnsRowsAndPages;

        /// <summary>
        /// Whether to capture primitive cell types when processing the query report.
        /// </summary>
        /// <remarks>This is supported only for <see cref="IEssReportScript"/> query reports. Defaults to <see langword="false" />.</remarks>
        public bool CaptureCellTypes { get; set; }

        /// <summary>
        /// Whether data values should be omitted in the query report.
        /// </summary>
        /// <remarks>Defaults to <see langword="false" />.</remarks>
        public bool Dataless { get; set; } = false;

        /// <summary>
        /// Whether restricted data should be hidden in the query report.
        /// </summary>
        public bool HideRestrictedData { get; set; }

        /// <summary>
        /// Whether cell attributes should be returned in the query report.
        /// </summary>
        public bool CellAttributes { get; set; }

        /// <summary>
        /// Whether formatted values should be returned for text, date, and format string cells in the query report.
        /// </summary>
        /// <remarks>Defaults to <see langword="true" />.</remarks>
        public bool FormatString { get; set; } = true;

        /// <summary>
        /// Whether formatted values should be returned in the query report.
        /// </summary>
        public bool FormatValues { get; set; }

        /// <summary>
        /// Whether all blocks accessed by the report are locked for update.
        /// </summary>
        /// <remarks>This is supported only for <see cref="IEssReportScript"/> query reports. Defaults to <see langword="false" />.</remarks>
        public bool LockForUpdate { get; set; } = false;

        /// <summary>
        /// Whether invalid data should be returned in the query report.
        /// </summary>
        public bool MeaninglessCells { get; set; }

        /// <summary>
        /// The member identifier to return in the query report.
        /// </summary>
        /// <remarks>Defaults to <see cref="EssQueryReport.ReportMemberIdentifier.Name" />.</remarks>
        public EssQueryReport.ReportMemberIdentifier MemberIdentifier { get; set; } = EssQueryReport.ReportMemberIdentifier.Name;

        /// <summary>
        /// Whether MDX dimension property columns and rows will be relocated outside of the valid Essbase grid.
        /// </summary>
        /// <remarks>This is supported only for <see cref="IEssMdxScript" />.</remarks>
        public bool RelocateDimensionPropertyColumnsAndRows { get; set; } = false;

        /// <summary>
        /// Whether smart/text list information should be returned in the query report.
        /// </summary>
        public bool TextList { get; set; }

        /// <summary>
        /// Whether URL drillthrough information should be returned in the query report.
        /// </summary>
        public bool UrlDrillThrough { get; set; }
    }
}
