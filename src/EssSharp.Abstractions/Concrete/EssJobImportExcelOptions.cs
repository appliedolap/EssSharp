﻿using System;

namespace EssSharp
{
    /// <summary />
    public class EssJobImportExcelOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobImportExcelOptions( string applicationName = null, EssBuildOption ? buildOption = null, string catalogExcelPath = null, bool? createFiles = true, string cubeName = null, bool ? deleteExcelOnSuccess = null, bool? executeScripts = true, string importExcelFilename = null, bool? loadData = true, bool? overwrite = null, bool? recreateApp = false ) : base( EssJobType.ImportExcel )
        {
            ApplicationName      = applicationName;
            CubeName             = cubeName;

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
        public EssJobImportExcelOptions( IEssFile essFile, string applicationName = null, EssBuildOption? buildOption = null, bool? createFiles = true, string cubeName = null, bool ? deleteExcelOnSuccess = null, bool? executeScripts = true, bool? loadData = true, bool? overwrite = null, bool? recreateApp = false ) : base( EssJobType.ImportExcel )
        {
            if ( essFile is null )
                throw new ArgumentNullException($@"An {nameof(IEssFile)} file is required to create an {nameof(EssJobImportExcelOptions)} with this constructor.", nameof(essFile));

            ApplicationName      = applicationName;
            CubeName             = cubeName;

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

        #region IEssJobOptions EssJobType.ImportExcel Members

        /// <inheritdoc />
        public EssBuildOption? BuildOption { get; set; }

        /// <inheritdoc />
        public string CatalogExcelPath { get; set; }

        /// <inheritdoc />
        public bool? CreateFiles { get; set; } = true;

        /// <inheritdoc />
        public bool? DeleteExcelOnSuccess { get; set; }

        /// <inheritdoc />
        public bool? ExecuteScripts { get; set; } = true;

        /// <inheritdoc />
        public bool? LoadData { get; set; } = true;

        /// <inheritdoc />
        public string ImportExcelFilename { get; set; }

        /// <inheritdoc />
        public bool? Overwrite { get; set; }

        /// <inheritdoc />
        public bool? RecreateApp { get; set; } = false;

        #endregion
    }
}
