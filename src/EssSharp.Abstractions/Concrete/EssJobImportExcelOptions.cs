using System;

namespace EssSharp
{
    /// <summary />
    public class EssJobImportExcelOptions : EssJobOptions
    {
        /// <summary />
        public EssJobImportExcelOptions( EssBuildOption? buildOption = null, string catalogExcelPath = null, bool? createFiles = true, bool? deleteExcelOnSuccess = null, bool? executeScripts = true, string importExcelFilename = null, bool? loadData = true, bool? overwrite = null, bool? recreateApp = false )
        {
            BuildOption          = buildOption;
            CatalogExcelPath     = catalogExcelPath;
            CreateFiles          = createFiles;
            DeleteExcelOnSuccess = deleteExcelOnSuccess;
            ExecuteScripts       = executeScripts;
            ImportExcelFilename  = importExcelFilename;
            LoadData             = loadData;
            Overwrite            = overwrite;
            RecreateApp          = recreateApp;
        }

        /// <summary />
        /// <param name="essFile" />
        /// <param name="buildOption" />
        /// <param name="deleteExcelOnSuccess" />
        public EssJobImportExcelOptions( IEssFile essFile, EssBuildOption? buildOption = null, bool? createFiles = true, bool? deleteExcelOnSuccess = null, bool? executeScripts = true, bool? loadData = true, bool? overwrite = null, bool? recreateApp = false )
        {
            if ( essFile is null )
                throw new ArgumentNullException($@"An {nameof(IEssFile)} file is required to create an {nameof(EssJobImportExcelOptions)} with this constructor.", nameof(essFile));

            BuildOption          = buildOption;
            CatalogExcelPath     = essFile.ParentPath;
            CreateFiles          = createFiles;
            DeleteExcelOnSuccess = deleteExcelOnSuccess;
            ExecuteScripts       = executeScripts;
            ImportExcelFilename  = essFile.Name;
            LoadData             = loadData;
            Overwrite            = overwrite;
            RecreateApp          = recreateApp;
        }

        #region IEssJobOptions Overrides

        /// <inheritdoc />
        public override bool? CreateFiles { get; set; } = true;

        /// <inheritdoc />
        public override bool? ExecuteScripts { get; set; } = true;

        /// <inheritdoc />
        public override bool? LoadData { get; set; } = true;

        /// <inheritdoc />
        public override bool? RecreateApp { get; set; } = false;

        #endregion
    }
}
