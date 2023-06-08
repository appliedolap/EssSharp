using System;
using System.Collections.Generic;
using System.IO;

namespace EssSharp
{
    /// <summary />
    public class EssJobLoadDataOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobLoadDataOptions( string dataFilePath = null, string ruleFilePath = null, string applicationName = null, string cubeName = null, bool? abortOnError = false ) : base(EssJobType.Dataload)
        {
            ApplicationName = applicationName;
            CubeName = cubeName;

            if ( !string.IsNullOrEmpty(dataFilePath) )
            {
                File = new List<string>() { $@"catalog/{dataFilePath}" };
                Rule = new List<string>() { !string.IsNullOrEmpty(ruleFilePath) ? $@"catalog/{ruleFilePath}" : "" };
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
            Rule            = new List<string>() { essRuleFile is not null ? $@"catalog{essRuleFile?.FullPath}" : "" };
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

        #region IEssJobOptions EssJobType.DataLoad Members

        /// <inheritdoc />
        public bool? AbortOnError { get; set; }

        /// <inheritdoc />
        public List<string> File { get; set; }

        /// <inheritdoc />
        public List<string> Rule { get; set; }

        #endregion
    }
}
