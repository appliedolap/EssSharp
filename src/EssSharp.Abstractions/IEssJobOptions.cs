using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public interface IEssJobOptions
    {
        #region IEssJobOptions EssJobType.Unknown (Multiple) Members

        /// <summary>
        /// Returns or sets the data file (or script file) to use for an <see cref="EssJobType.Dataload" />
        /// (or <inheritdoc cref="EssJobType.Calc" />/<see cref="EssJobType.MdxScript" />) job."/>
        /// </summary>
        public List<string> File { get; set; }

        /// <summary>
        /// Returns or sets the file to execute for an <see cref="EssJobType.Calc" /> job or 
        /// the file (or files) to load for an <see cref="EssJobType.Dataload" /> job.
        /// </summary>
        public string Script { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.Clear Members

        /// <summary>
        /// Returns or sets the clear option for an <see cref="EssJobType.Clear" /> job.
        /// </summary>
        public EssClearOption? Option { get; set; }

        /// <summary>
        /// Returns or sets the mdx expression specifying the region to clear for an ASO cube when the
        /// <see cref="EssClearOption.PARTIAL_DATA" /> is used for an <see cref="EssJobType.Clear"/> job.
        /// </summary>
        public string PartialDataExpression { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.DataLoad Members

        /// <summary>
        /// Returns or sets whether to abort the data load if an error is encountered for an <see cref="EssJobType.Dataload"/> job.
        /// </summary>
        public bool? AbortOnError { get; set; }

        /// <summary>
        /// Returns or sets the rule file (or files) to use for an <see cref="EssJobType.Dataload" /> job.
        /// </summary>
        public List<string> Rule { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.ExecuteReport Members

        /// <summary>
        /// Returns or sets whether the <see cref="ReportScriptFilename" /> property carries script content
        /// rather than the report script filename.
        /// </summary>
        public bool? IsScriptContent { get; set; }

        /// <summary>
        /// Returns or sets whether all blocks accessed by the report are locked for update.
        /// </summary>
        public bool? LockForUpdate { get; set; }

        /// <summary>
        /// Returns or sets the name of the report script file (or the script content, if <see cref="IsScriptContent" /> is set to <see langword="true" />).
        /// </summary>
        public string ReportScriptFilename { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.ExportExcel Members

        /// <summary>
        /// Returns or sets the build method for an <see cref="EssJobType.ExportExcel" /> job.
        /// </summary>
        public EssBuildMethod? BuildMethod { get; set; }

        /// <summary>
        /// Returns or sets the include calc option for an <see cref="EssJobType.ExportExcel" /> job.
        /// </summary>
        public bool? Calc { get; set; }

        /// <summary>
        /// Returns or sets the include data option for an <see cref="EssJobType.ExportExcel" /> job.
        /// </summary>
        public bool? Data { get; set; }

        /// <summary>
        /// Returns or sets the include member IDs options for an <see cref="EssJobType.ExportExcel" /> job.
        /// </summary>
        public bool? MemberIds { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.ImportExcel Members

        /// <summary>
        /// Returns or sets the build option for an <see cref="EssJobType.ImportExcel" /> job.
        /// </summary>
        public EssBuildOption? BuildOption { get; set; }

        /// <summary>
        /// Returns or sets the catalog excel path option for an <see cref="EssJobType.ImportExcel" /> job.
        /// </summary>
        public string CatalogExcelPath { get; set; }

        /// <summary>
        /// Returns or sets the create files option for an <see cref="EssJobType.ImportExcel" /> job.
        /// </summary>
        public bool? CreateFiles { get; set; }

        /// <summary>
        /// Returns or sets the delete excel file (on success) option for an <see cref="EssJobType.ImportExcel" /> job.
        /// </summary>
        public bool? DeleteExcelOnSuccess { get; set; }

        /// <summary>
        /// Returns or sets the execute scripts option for an <see cref="EssJobType.ImportExcel" /> job.
        /// </summary>
        public bool? ExecuteScripts { get; set; }

        /// <summary>
        /// Returns or sets the import excel file name option for an <see cref="EssJobType.ImportExcel" /> job.
        /// </summary>
        public string ImportExcelFilename { get; set; }

        /// <summary>
        /// Returns or sets the load data option for an <see cref="EssJobType.ImportExcel" /> job.
        /// </summary>
        public bool? LoadData { get; set; }

        /// <summary>
        /// Returns or sets the overwrite option for an <see cref="EssJobType.ImportExcel" /> job.
        /// </summary>
        public bool? Overwrite { get; set; }

        /// <summary>
        /// Returns or sets the recreate app option for an <see cref="EssJobType.ImportExcel" /> job.
        /// </summary>
        public bool? RecreateApp { get; set; }

        #endregion
    }
}
