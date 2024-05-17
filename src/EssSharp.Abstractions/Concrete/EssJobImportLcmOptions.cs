using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssJobImportLcmOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        /// <param name="applicationName"></param>
        /// <param name="cubeName"></param>
        /// <param name="targetApplicationName"></param>
        /// <param name="overwrite"></param>
        /// <param name="includeServerLevel"></param>
        /// <param name="zipFileName"></param>
        public EssJobImportLcmOptions( string applicationName = null, string cubeName = null, string targetApplicationName = null, bool? overwrite = false, bool? includeServerLevel = false, string zipFileName = null ) : base( EssJobType.LcmImport )
        {
            ApplicationName       = applicationName;
            CubeName              = cubeName;

            TargetApplicationName = TargetApplicationName;
            Overwrite = Overwrite;
            IncludeServerLevel = includeServerLevel;
            ZipFileName = zipFileName;
    }
        /// <inheritdoc />
        public string TargetApplicationName { get; set; }

        /// <inheritdoc />
        public bool? Overwrite { get; set; }

        /// <inheritdoc />
        public bool? IncludeServerLevel { get; set; }

        /// <inheritdoc />
        public string ZipFileName { get; set; }
    }
}
