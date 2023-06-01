using System;

namespace EssSharp
{
    /// <summary />
    public class EssJobLoadDataOptions : EssJobOptions, IEssJobOptions
    {
        public EssJobLoadDataOptions( string fileName = null, string applicationName = null, string cubeName = null, bool abortOnError = true ) : base(EssJobType.Dataload)
        {
            ApplicationName = applicationName;
            CubeName = cubeName;

            File = fileName;
            AbortOnError = abortOnError.ToString().ToLowerInvariant();
        }

        /// <summary />
        public EssJobLoadDataOptions( IEssFile essFile, string applicationName = null, string cubeName = null, bool abortOnError = true ): base( EssJobType.Dataload )
        {
            ApplicationName       = applicationName;
            CubeName              = cubeName;

            File            = essFile.Name;
            AbortOnError    = abortOnError.ToString().ToLowerInvariant();
        }

        #region IEssJobOptions EssJobType.Clear Members

        string IEssJobOptions.Option { get; set; }

        string IEssJobOptions.PartialDataExpression { get; set; }

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
        
        #region IEssJobOptions EssJobType.Calc and EssJobType.DataLoad Members

        public string File { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.DataLoad Members

        public string AbortOnError { get; set; }

        #endregion
    }
}
