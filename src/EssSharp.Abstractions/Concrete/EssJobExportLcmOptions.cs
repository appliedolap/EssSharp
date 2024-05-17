using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssJobExportLcmOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        /// <param name="applicationName"></param>
        /// <param name="cubeName"></param>
        /// <param name="allApp"></param>
        /// <param name="generateartifactlist"></param>
        /// <param name="includeServerLevel"></param>
        /// <param name="zipFileName"></param>
        /// <param name="skipData"></param>
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

        /// <inheritdoc />
        public bool? AllApp { get; set; }

        /// <inheritdoc />
        public bool? Generateartifactlist { get; set; }

        /// <inheritdoc />
        public bool? IncludeServerLevel { get; set; }

        /// <inheritdoc />
        public string ZipFileName { get; set; }

        /// <inheritdoc />
        public bool? SkipData { get; set; }

        #endregion
    }
}
