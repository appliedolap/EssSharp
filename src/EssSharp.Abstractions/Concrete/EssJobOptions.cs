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
        public EssJobType JobType { get; protected set; } = EssJobType.Unknown;

        #endregion

        #region Explicit IEssJobOptions EssJobType.Unknown (Multiple) Members

        /// <inheritdoc />
        string IEssJobOptions.Script { get; set; }

        /// <inheritdoc />
        List<string> IEssJobOptions.File { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.Clear Members

        /// <inheritdoc />
        EssClearOption? IEssJobOptions.Option { get; set; }

        /// <inheritdoc />
        string IEssJobOptions.PartialDataExpression { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.DataLoad Members

        /// <inheritdoc />
        bool? IEssJobOptions.AbortOnError { get; set; }

        /// <inheritdoc />
        List<string> IEssJobOptions.Rule { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.ExecuteReport Members

        /// <inheritdoc />
        bool? IEssJobOptions.IsScriptContent { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.LockForUpdate { get; set; }

        /// <inheritdoc />
        string IEssJobOptions.ReportScriptFilename { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.ExportExcel Members

        /// <inheritdoc />
        EssBuildMethod? IEssJobOptions.BuildMethod { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.Calc { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.Data { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.MemberIds { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.ImportExcel Members

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
        string IEssJobOptions.ImportExcelFilename { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.LoadData { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.Overwrite { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.RecreateApp { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.LMCExport Members

        bool? IEssJobOptions.AllApp { get; set; }

        bool? IEssJobOptions.Generateartifactlist { get; set; }

        bool? IEssJobOptions.SkipData { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.LMCExport Members

        string IEssJobOptions.TargetApplicationName { get; set; }

        //bool? IEssJobOptions.Overwrite { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.LMCExport/LCMImport Shared Members

        bool? IEssJobOptions.IncludeServerLevel { get; set; }

        string IEssJobOptions.ZipFileName { get; set; }

        #endregion
    }
}
