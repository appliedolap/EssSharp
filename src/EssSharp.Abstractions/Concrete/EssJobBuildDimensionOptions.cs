using System;
using System.Collections.Generic;
using System.IO;

namespace EssSharp
{
    /// <summary />
    public class EssJobBuildDimensionOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobBuildDimensionOptions( string dataFilePath = null, string ruleFilePath = null, string applicationName = null, string cubeName = null ) : base(EssJobType.Dimbuild)
        {
            if ( !string.IsNullOrEmpty(dataFilePath) && System.IO.File.Exists(dataFilePath) )
                throw new ArgumentException($@"A server data file path must be given to this constructor. Use the {nameof(LocalDataFilePath)} or {nameof(LocalDataFileStream)} property to load data from a local file.");

            if ( !string.IsNullOrEmpty(ruleFilePath) && System.IO.File.Exists(ruleFilePath) )
                throw new ArgumentException($@"A server rule file path must be given to this constructor. Use the {nameof(LocalRuleFilePath)} or {nameof(LocalRuleFileStream)} property to load rules from a local file.");

            ApplicationName = applicationName;
            CubeName = cubeName;

            if ( !string.IsNullOrEmpty(dataFilePath) )
            {
                File = new List<string>() { $@"catalog/{dataFilePath.TrimStart('/')}" };
                Rule = new List<string>() { !string.IsNullOrEmpty(ruleFilePath) ? $@"catalog/{ruleFilePath.TrimStart('/')}" : "" };
            }
        }

        /// <summary />
        public EssJobBuildDimensionOptions( IEssFile essDataFile, IEssFile essRuleFile = null, string applicationName = null, string cubeName = null ) : base(EssJobType.Dimbuild)
        {
            if ( essDataFile is null )
                throw new ArgumentNullException(nameof(essDataFile), $@"A server data {nameof(IEssFile)} must be given to this constructor.");

            ApplicationName = applicationName;
            CubeName = cubeName;

            File = new List<string>() { $@"catalog{essDataFile.FullPath}" };
            Rule = new List<string>() { essRuleFile is not null ? $@"catalog{essRuleFile.FullPath}" : "" };
        }

        /// <summary />
        public EssJobBuildDimensionOptions( FileStream localDataFileStream, FileStream localRuleFileStream = null, string applicationName = null, string cubeName = null ) : base(EssJobType.Dimbuild)
        {
            if ( localDataFileStream is null )
                throw new ArgumentNullException(nameof(localDataFileStream), $@"A local data {nameof(FileStream)} must be given to this constructor.");

            ApplicationName = applicationName;
            CubeName = cubeName;

            LocalDataFileStream = localDataFileStream;
            LocalRuleFileStream = localRuleFileStream;
        }

        #region Public Properties

        /// <inheritdoc />
        public string LocalDataFilePath { get; set; }

        /// <inheritdoc />
        public string LocalRuleFilePath { get; set; }

        /// <inheritdoc />
        public FileStream LocalDataFileStream { get; set; }

        /// <inheritdoc />
        public FileStream LocalRuleFileStream { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.DataLoad Members

        /// <inheritdoc />
        public List<string> File { get; set; }

        /// <inheritdoc />
        public List<string> Rule { get; set; }

        #endregion
    }
}
