using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssJobScriptOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptName">The name of script to execute. Maps to <see cref="File" />.</param>
        /// <param name="applicationName"></param>
        /// <param name="cubeName"></param>
        public EssJobScriptOptions( string scriptName, string applicationName = null, string cubeName = null ) : base(EssJobType.Calc)
        {
            if ( string.IsNullOrEmpty(scriptName) )
                throw new ArgumentNullException($@"The name of a script is required to create an {nameof(EssJobScriptOptions)} with this constructor.", nameof(scriptName));

            ApplicationName = applicationName;
            CubeName        = cubeName;

            File            = new List<string>() { scriptName };
        }

        #region IEssJobOptions EssJobType.Calc Members

        /// <inheritdoc />
        public List<string> File { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.ImportExcel Members

        /// <inheritdoc />
        EssBuildOption? IEssJobOptions.BuildOption { get; set; }

        /// <inheritdoc />
        string IEssJobOptions.CatalogExcelPath { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.CreateFiles { get; set; } = true;

        /// <inheritdoc />
        bool? IEssJobOptions.DeleteExcelOnSuccess { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.ExecuteScripts { get; set; } = true;

        /// <inheritdoc />
        bool? IEssJobOptions.LoadData { get; set; } = true;

        /// <inheritdoc />
        string IEssJobOptions.ImportExcelFilename { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.Overwrite { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.RecreateApp { get; set; } = false;

        #endregion

        #region Explicit IEssJobOptions Members
        /// <inheritdoc />
        EssBuildMethod? IEssJobOptions.BuildMethod { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.Calc { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.Data { get; set; }

        /// <inheritdoc />
        bool? IEssJobOptions.MemberIds { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.Clear and EssJobType.DataLoad Members

        string IEssJobOptions.Option { get; set; }

        string IEssJobOptions.PartialDataExpression { get; set; }

        #endregion

        #region IEssJobOptions EssJobType.DataLoad Members

        List<string> IEssJobOptions.Rule { get; set; }

        bool? IEssJobOptions.AbortOnError { get; set; }

        #endregion
    }
}
