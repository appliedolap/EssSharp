using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssJobImportLcmOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobImportLcmOptions( string applicationName = null, string cubeName = null, string targetApplicationName = null, bool? overwrite = false, bool? includeServerLevel = false, string zipFileName = null ) : base( EssJobType.LcmImport )
        {
            ApplicationName       = applicationName;
            CubeName              = cubeName;

            TargetApplicationName = TargetApplicationName;
            Overwrite = Overwrite;
            IncludeServerLevel = includeServerLevel;
            ZipFileName = zipFileName;
    }

        public string TargetApplicationName { get; set; }

        public bool? Overwrite { get; set; }

        public bool? IncludeServerLevel { get; set; }

        public string ZipFileName { get; set; }
    }
}
