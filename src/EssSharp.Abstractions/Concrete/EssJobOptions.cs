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

        /// <summary>
        /// Name of <see cref="IEssScript"/>./>
        /// </summary>
        string IEssJobOptions.Script { get; set; }

        /// <summary>
        /// Name of File.
        /// </summary>
        List<string> IEssJobOptions.File { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.Clear Members

        EssClearOption? IEssJobOptions.Option { get; set; }

        string IEssJobOptions.PartialDataExpression { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.DataLoad Members

        /// <summary>
        /// Stop job if error occures
        /// </summary>
        bool? IEssJobOptions.AbortOnError { get; set; }

        /// <summary>
        /// Name of Rule file
        /// </summary>
        List<string> IEssJobOptions.Rule { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.ExportExcel Members

        /// <summary>
        /// <see cref="EssBuildMethod"/>
        /// </summary>
        EssBuildMethod? IEssJobOptions.BuildMethod { get; set; }

        bool? IEssJobOptions.Calc { get; set; }

        bool? IEssJobOptions.Data { get; set; }

        /// <summary>
        /// Include Member ID's
        /// </summary>
        bool? IEssJobOptions.MemberIds { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.ImportExcel Members

        /// <summary>
        /// <see cref="EssBuildOption"/>
        /// </summary>
        EssBuildOption? IEssJobOptions.BuildOption { get; set; }

        /// <summary>
        /// path to folder that holds excel file
        /// </summary>
        string IEssJobOptions.CatalogExcelPath { get; set; }

        /// <summary>
        /// Create files on cube
        /// </summary>
        bool? IEssJobOptions.CreateFiles { get; set; }

        /// <summary>
        /// Delete excel files after Cube creation
        /// </summary>
        bool? IEssJobOptions.DeleteExcelOnSuccess { get; set; }

        /// <summary>
        /// Execute scripts after creation
        /// </summary>
        bool? IEssJobOptions.ExecuteScripts { get; set; }

        /// <summary>
        /// Name of excel file to import
        /// </summary>
        string IEssJobOptions.ImportExcelFilename { get; set; }

        /// <summary>
        /// Load data to cube
        /// </summary>
        bool? IEssJobOptions.LoadData { get; set; }

        /// <summary>
        /// If file with same name exists, overwrite it
        /// </summary>
        bool? IEssJobOptions.Overwrite { get; set; }

        /// <summary>
        /// If application exists, recreate it
        /// </summary>
        bool? IEssJobOptions.RecreateApp { get; set; }

        #endregion

        #region Explicit IEssJobOptions EssJobType.ExecuteReport Members

        /// <summary>
        /// Name of report script
        /// </summary>
        string IEssJobOptions.ReportScriptFilename { get; set; }

        /// <summary>
        /// Lock script while executing
        /// </summary>
        bool? IEssJobOptions.LockForUpdate { get; set; }

        #endregion
    }
}
