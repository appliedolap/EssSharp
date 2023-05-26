using System;

namespace EssSharp
{
    /// <summary />
    public class EssJobSciptOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobSciptOptions( string applicationName = null, string cubeName = null, string file = null ) : base( EssJobType.Calc )
        {
            ApplicationName      = applicationName;
            CubeName             = cubeName;

            File                 = file;
        }

        #region IEssJobOptions EssJobType.Calc Members

        public string File { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.ImportExcel Members

        /// <inheritdoc />
        EssBuildOption? IEssJobOptions.BuildOption { get; set; }

        /// <inheritdoc />
        string IEssJobOptions.CatalogExcelPath { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.CreateFiles { get; set; } = true;

        /// <inheritdoc />
        bool? IEssJobOptions.DeleteExcelOnSuccess { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.ExecuteScripts { get; set; } = true;

        /// <inheritdoc />
        bool? IEssJobOptions.LoadData { get; set; } = true;

        /// <inheritdoc />
        string IEssJobOptions.ImportExcelFilename { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.Overwrite { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.RecreateApp { get; set; } = false;

        #endregion

        #region Explicit IEssJobOptions Members

        EssBuildMethod? IEssJobOptions.BuildMethod { get; set; }

        bool? IEssJobOptions.Calc { get; set; }

        bool? IEssJobOptions.Data { get; set; }
        
        bool? IEssJobOptions.MemberIds { get; set; }

        #endregion
    }
}
