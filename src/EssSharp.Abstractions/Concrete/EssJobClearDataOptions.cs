using System;

namespace EssSharp
{
    /// <summary />
    public class EssJobClearDataOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobClearDataOptions( string applicationName = null, string cubeName = null, EssClearOption option = EssClearOption.ALL_DATA , string dataExpression = null ): base( EssJobType.Clear )
        {
            ApplicationName       = applicationName;
            CubeName              = cubeName;

            Option                = option.ToString();
            PartialDataExpression = dataExpression;
        }

        #region IEssJobOptions EssJobType.Clear Members

        public string Option { get; set; }

        public string PartialDataExpression { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.ExportExcel Members

        /// <inheritdoc />
        EssBuildMethod? IEssJobOptions.BuildMethod { get; set; } = EssBuildMethod.ParentChild;

        /// <inheritdoc />
        bool? IEssJobOptions.Calc { get; set; } = false;

        /// <inheritdoc />
        bool? IEssJobOptions.Data { get; set; } = false;

        /// <inheritdoc />
        bool? IEssJobOptions.MemberIds { get; set; } = false;

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
        
        #region IEssJobOptions EssJobType.Calc Members

        string IEssJobOptions.File { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.DataLoad Members

        string IEssJobOptions.AbortOnError { get; set; }

        #endregion
    }
}
