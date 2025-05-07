using System;
using System.Collections.Generic;
using System.IO;

namespace EssSharp
{
    /// <summary />
    public class EssJobLoadDataOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobLoadDataOptions(string dataFilePath = null, string ruleFilePath = null, string applicationName = null, string cubeName = null, bool? abortOnError = false, string connection = null, string password = null, string username = null ) : base(EssJobType.Dataload)
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

            AbortOnError = abortOnError;

            Connection = connection;

            if ( !string.IsNullOrEmpty(connection) )
                UseConnection = true;

            User = username;
            Password = password;
        }

        /// <summary />
        public EssJobLoadDataOptions(IEssFile essDataFile = null, IEssFile essRuleFile = null, string applicationName = null, string cubeName = null, bool? abortOnError = false, string connection = null, string password = null, string username = null ): base( EssJobType.Dataload )
        {
            if ( essDataFile is null && essRuleFile is null )
                throw new ArgumentNullException(nameof(essDataFile), $@"A server data {nameof(IEssFile)} must be given to this constructor.");

            ApplicationName = applicationName;
            CubeName        = cubeName;

            File            = essDataFile is not null ? new List<string>() { $@"catalog{essDataFile.FullPath}" } : null;
            Rule            = essRuleFile is not null ? new List<string>() { $@"catalog{essRuleFile.FullPath}" } : null;
            AbortOnError    = abortOnError;

            Connection = connection;

            if ( !string.IsNullOrEmpty(connection) )    
                UseConnection = true;

            User = username;
            Password = password;
        }

        /// <summary />
        public EssJobLoadDataOptions( FileStream localDataFileStream, FileStream localRuleFileStream = null, string applicationName = null, string cubeName = null, bool? abortOnError = false, string connection = null, string password = null, string username = null ) : base(EssJobType.Dataload)
        {
            if ( localDataFileStream is null )
                throw new ArgumentNullException(nameof(localDataFileStream), $@"A local data {nameof(FileStream)} must be given to this constructor.");

            ApplicationName     = applicationName;
            CubeName            = cubeName;

            LocalDataFileStream = localDataFileStream;
            LocalRuleFileStream = localRuleFileStream;
            AbortOnError        = abortOnError;

            Connection = connection;

            if ( !string.IsNullOrEmpty(connection) )
                UseConnection = true;

            User = username;
            Password = password;
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

        #region IEssJobOptions EssJobType.DataLoad Members

        /// <inheritdoc />
        public bool? AbortOnError { get; set; }

        /// <inheritdoc />
        public List<string> File { get; set; }

        /// <inheritdoc />
        public List<string> Rule { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.Dimbuild Members

        /// <inheritdoc />
        public string Connection { get; set; }

        /// <inheritdoc />
        public string Password { get; set; }

        /// <inheritdoc />
        public bool? UseConnection { get; set; }

        /// <inheritdoc />
        public string User { get; set; }

        #endregion
    }
}
