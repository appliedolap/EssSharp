using System;

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

        #region Explicit IEssJobOptions Members

        /// <inheritdoc />
        EssBuildOption? IEssJobOptions.BuildOption { get; set; }

        /// <inheritdoc />
        string IEssJobOptions.CatalogExcelPath { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.CreateFiles { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.DeleteExcelOnSuccess { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.ExecuteScripts { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.LoadData { get; set; }

        /// <inheritdoc />
        string IEssJobOptions.ImportExcelFilename { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.Overwrite { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.RecreateApp { get; set; }

        #endregion
    }
}
