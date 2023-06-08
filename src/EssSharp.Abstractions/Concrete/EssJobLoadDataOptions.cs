using System;
using System.Collections.Generic;
using System.IO;

namespace EssSharp
{
    /// <summary />
    public class EssJobLoadDataOptions : EssJobOptions, IEssJobOptions
    {
        public EssJobLoadDataOptions( string dataFilePath = null, string ruleFilePath = null, string applicationName = null, string cubeName = null, bool? abortOnError = false ) : base(EssJobType.Dataload)
        {
            ApplicationName = applicationName;
            CubeName = cubeName;

            if ( !string.IsNullOrEmpty(dataFilePath) )
            {
                File = new List<string>() { $@"catalog/{dataFilePath}" };
                Rule = new List<string>() { ruleFilePath is null ? "" : $@"catalog/{ruleFilePath}" };
            }

            AbortOnError = abortOnError;
        }

        /// <summary />
        public EssJobLoadDataOptions( IEssFile essDataFile, IEssFile essRuleFile = null, string applicationName = null, string cubeName = null, bool? abortOnError = false ): base( EssJobType.Dataload )
        {
            if ( essDataFile is null )
                throw new ArgumentNullException();

            ApplicationName = applicationName;
            CubeName        = cubeName;

            File            = new List<string>() { $@"catalog{essDataFile.FullPath}" };
            Rule            = new List<string>() { essRuleFile is null ? "" : $@"catalog{essRuleFile?.FullPath}" };
            AbortOnError    = abortOnError;
        }

        #region Public Properties

        /// <summary />
        public string DataFileLocalPath { get; set; }

        /// <summary />
        public string RuleFileLocalPath { get; set; }

        /// <summary />
        public FileStream DataFileStream { get; set; }

        /// <summary />
        public FileStream RuleFileStream { get; set; }

        #endregion

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

        public List<string> File { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.DataLoad Members

        public List<string> Rule { get; set; }

        public bool? AbortOnError { get; set; }

        #endregion
    }
}
