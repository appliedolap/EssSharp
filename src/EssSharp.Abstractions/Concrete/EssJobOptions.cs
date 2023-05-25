using System.ComponentModel;

namespace EssSharp
{
    /// <summary />
    public class EssJobOptions : IEssJobOptions
    {
        #region EssJobType.importExcel Members

        /// <inheritdoc />
        public virtual EssBuildOption? BuildOption { get; set; }

        /// <inheritdoc />
        public virtual string CatalogExcelPath { get; set; }

        /// <inheritdoc />
        public virtual bool? CreateFiles { get; set; }

        /// <inheritdoc />
        public virtual bool? DeleteExcelOnSuccess { get; set; }

        /// <inheritdoc />
        public virtual bool? ExecuteScripts { get; set; }

        /// <inheritdoc />
        public virtual string ImportExcelFilename { get; set; }

        /// <inheritdoc />
        public virtual bool? LoadData { get; set; }

        /// <inheritdoc />
        public virtual bool? Overwrite { get; set; }

        /// <inheritdoc />
        public virtual bool? RecreateApp { get; set; }

        #endregion
    }
}
