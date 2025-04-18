using System;
using System.Collections.Generic;
using System.IO;

namespace EssSharp
{
    public class EssJobBuildDimensionOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobBuildDimensionOptions( string dataFilePath = null, string ruleFilePath = null, string applicationName = null, string connection = null, string cubeName = null, bool? forceDimBuild = null, EssRestructureOption? restructureOption = null ) : base(EssJobType.Dimbuild)
        {
            if ( !string.IsNullOrEmpty(dataFilePath) && System.IO.File.Exists(dataFilePath) )
                throw new ArgumentException($@"A server data file path must be given to this constructor. Use the {nameof(LocalDataFilePath)} or {nameof(LocalDataFileStream)} property to load data from a local file.");

            if ( !string.IsNullOrEmpty(ruleFilePath) && System.IO.File.Exists(ruleFilePath) )
                throw new ArgumentException($@"A server rule file path must be given to this constructor. Use the {nameof(LocalRuleFilePath)} or {nameof(LocalRuleFileStream)} property to load rules from a local file.");

            if ( string.IsNullOrEmpty(dataFilePath) && string.IsNullOrEmpty(ruleFilePath) )
                throw new ArgumentException($@"A server data and/or rule file path must be given to this constructor.");

            ApplicationName = applicationName;
            CubeName = cubeName;

            if ( !string.IsNullOrEmpty(dataFilePath) )
            {
                File = new List<string>() { $@"catalog/{dataFilePath.TrimStart('/')}" };
                Rule = new List<string>() { !string.IsNullOrEmpty(ruleFilePath) ? $@"catalog/{ruleFilePath.TrimStart('/')}" : "" };
            }
            else if ( !string.IsNullOrEmpty(ruleFilePath) )
            {
                Rule = new List<string>() { $@"catalog/{ruleFilePath.TrimStart('/')}" };
            }

            Connection = connection;

            if ( !string.IsNullOrEmpty(connection) )
                UseConnection = true;

            ForceDimBuild = forceDimBuild;
            RestructureOption = restructureOption;
        }

        /// <summary />
        public EssJobBuildDimensionOptions( IEssFile essDataFile = null, IEssFile essRuleFile = null, string applicationName = null, string connection = null, string cubeName = null, bool? forceDimBuild = null, EssRestructureOption? restructureOption = null ) : base(EssJobType.Dimbuild)
        {
            if ( essDataFile is null && essRuleFile is null )
                throw new ArgumentException($@"A server data and/or rule {nameof(IEssFile)} must be given to this constructor.");

            ApplicationName = applicationName;
            CubeName = cubeName;

            if ( essDataFile is not null )
            {
                File = new List<string>() { $@"catalog{essDataFile.FullPath}" };
                Rule = new List<string>() { essRuleFile is not null ? $@"catalog{essRuleFile.FullPath}" : "" };
            }
            else if ( essRuleFile is not null )
            {
                Rule = new List<string>() { $@"catalog{essRuleFile.FullPath}" };
            }

            Connection = connection;

            if ( !string.IsNullOrEmpty(connection) )
                UseConnection = true;

            ForceDimBuild = forceDimBuild;
            RestructureOption = restructureOption;
        }

        /// <summary />
        public EssJobBuildDimensionOptions( FileStream localDataFileStream = null, FileStream localRuleFileStream = null, string applicationName = null, string connection = null, string cubeName = null, bool? forceDimBuild = null, EssRestructureOption? restructureOption = null ) : base(EssJobType.Dimbuild)
        {
            if ( localDataFileStream is null && LocalRuleFileStream is null )
                throw new ArgumentException($@"A local data and/or rule {nameof(FileStream)} must be given to this constructor.");

            ApplicationName = applicationName;
            CubeName = cubeName;

            LocalDataFileStream = localDataFileStream;
            LocalRuleFileStream = localRuleFileStream;

            Connection = connection;

            if ( !string.IsNullOrEmpty(connection) )
                UseConnection = true;

            ForceDimBuild = forceDimBuild;
            RestructureOption = restructureOption;
        }

        #region Public Properties

        /// <summary />
        public string LocalDataFilePath { get; set; }

        /// <summary />
        public string LocalRuleFilePath { get; set; }

        /// <summary />
        public FileStream LocalDataFileStream { get; set; }

        /// <summary />
        public FileStream LocalRuleFileStream { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.Dimbuild Members

        /// <inheritdoc />
        public string Connection { get; set; }

        /// <inheritdoc />
        public bool? ForceDimBuild { get; set; }

        /// <inheritdoc />
        public string Password { get; set; }

        /// <inheritdoc />
        public EssRestructureOption? RestructureOption { get; set; }

        /// <inheritdoc />
        public bool? UseConnection { get; set; }

        /// <inheritdoc />
        public string User { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.Unknown Members

        /// <inheritdoc />
        public List<string> File { get; set; }

        /// <inheritdoc />
        public List<string> Rule { get; set; }

        #endregion
    }
}
