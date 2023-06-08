using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssJobExportExcelOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobExportExcelOptions( string applicationName = null, EssBuildMethod? buildMethod = EssBuildMethod.ParentChild, bool? calc = false, string cubeName = null, bool? data = false, bool? memberIds = false ) : base( EssJobType.ExportExcel )
        {
            ApplicationName = applicationName;
            CubeName        = cubeName;

            BuildMethod     = buildMethod;
            Calc            = calc;
            Data            = data;
            MemberIds       = memberIds;
        }

        #region IEssJobOptions EssJobType.ExportExcel Members

        /// <inheritdoc />
        public EssBuildMethod? BuildMethod { get; set; } = EssBuildMethod.ParentChild;

        /// <inheritdoc />
        public bool? Calc { get; set; } = false;

        /// <inheritdoc />
        public bool? Data { get; set; } = false;

        /// <inheritdoc />
        public bool? MemberIds { get; set; } = false;

        #endregion
    }
}
