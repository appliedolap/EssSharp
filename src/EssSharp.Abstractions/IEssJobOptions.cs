namespace EssSharp
{
    /// <summary />
    public interface IEssJobOptions
    {
        #region EssJobType.importExcel Members

        /// <summary>
        /// Returns or sets the build option for an <see cref="EssJobType.importExcel" /> job.
        /// </summary>
        public EssBuildOption? BuildOption { get; set; }

        /// <summary>
        /// Returns or sets the catalog excel path option for an <see cref="EssJobType.importExcel" /> job.
        /// </summary>
        public string CatalogExcelPath { get; set; }

        /// <summary>
        /// Returns or sets the create files option for an <see cref="EssJobType.importExcel" /> job.
        /// </summary>
        public bool? CreateFiles { get; set; }

        /// <summary>
        /// Returns or sets the delete excel file (on success) option for an <see cref="EssJobType.importExcel" /> job.
        /// </summary>
        public bool? DeleteExcelOnSuccess { get; set; }

        /// <summary>
        /// Returns or sets the execute scripts option for an <see cref="EssJobType.importExcel" /> job.
        /// </summary>
        public bool? ExecuteScripts { get; set; }

        /// <summary>
        /// Returns or sets the import excel file name option for an <see cref="EssJobType.importExcel" /> job.
        /// </summary>
        public string ImportExcelFilename { get; set; }

        /// <summary>
        /// Returns or sets the load data option for an <see cref="EssJobType.importExcel" /> job.
        /// </summary>
        public bool? LoadData { get; set; }

        /// <summary>
        /// Returns or sets the overwrite option for an <see cref="EssJobType.importExcel" /> job.
        /// </summary>
        public bool? Overwrite { get; set; }

        /// <summary>
        /// Returns or sets the recreate app option for an <see cref="EssJobType.importExcel" /> job.
        /// </summary>
        public bool? RecreateApp { get; set; }

        #endregion
    }
}
