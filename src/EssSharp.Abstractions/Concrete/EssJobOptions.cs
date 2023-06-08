using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssJobOptions : IEssJobOptions
    {
        /// <summary />
        public EssJobOptions( EssJobType jobType )
        {
            JobType = jobType;
        }

        #region Public Properties

        /// <summary>
        /// The name of the application on which to perform the job.
        /// </summary> />
        public string ApplicationName { get; set; }

        /// <summary>
        /// Returns or sets the name of the cube on which to perform the job.
        /// </summary>/>
        public string CubeName { get; set; }

        /// <summary>
        /// Returns the job type.
        /// </summary>
        public EssJobType JobType { get; private set; } = EssJobType.Unknown;

        #endregion

        #region Explicit IEssJobOptions EssJobType.Unknown (Multiple) Members

        List<string> IEssJobOptions.File { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.Clear Members

        EssClearOption? IEssJobOptions.Option { get; set; }

        string IEssJobOptions.PartialDataExpression { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.DataLoad Members

        bool? IEssJobOptions.AbortOnError { get; set; }

        List<string> IEssJobOptions.Rule { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.ExportExcel Members

        EssBuildMethod? IEssJobOptions.BuildMethod { get; set; }

        bool? IEssJobOptions.Calc { get; set; }

        bool? IEssJobOptions.Data { get; set; }

        bool? IEssJobOptions.MemberIds { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.ImportExcel Members

        EssBuildOption? IEssJobOptions.BuildOption { get; set; }

        string IEssJobOptions.CatalogExcelPath { get; set; }

        bool? IEssJobOptions.CreateFiles { get; set; }

        bool? IEssJobOptions.DeleteExcelOnSuccess { get; set; }

        bool? IEssJobOptions.ExecuteScripts { get; set; }

        string IEssJobOptions.ImportExcelFilename { get; set; }

        bool? IEssJobOptions.LoadData { get; set; }

        bool? IEssJobOptions.Overwrite { get; set; }

        bool? IEssJobOptions.RecreateApp { get; set; }

        #endregion
    }
}
