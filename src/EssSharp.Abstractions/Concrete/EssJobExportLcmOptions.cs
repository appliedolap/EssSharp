using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssJobExportLcmOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobExportLcmOptions( string applicationName = null, string cubeName = null, bool? allApp = null, bool? generateartifactlist = null, bool? includeServerLevel = null, string zipFileName = null, bool? skipData = null ) : base( EssJobType.LcmExport )
        {
            ApplicationName       = applicationName;
            CubeName              = cubeName;

            AllApp = allApp;
            Generateartifactlist = generateartifactlist;
            IncludeServerLevel = includeServerLevel;
            ZipFileName = zipFileName;
            SkipData = skipData;
    }

        #region Explicit IEssJobOptions EssJobType.LMCExport Members

        public bool? AllApp { get; set; }

        public bool? Generateartifactlist { get; set; }

        public bool? IncludeServerLevel { get; set; }

        public string ZipFileName { get; set; }

        public bool? SkipData { get; set; }

        #endregion
    }
}
