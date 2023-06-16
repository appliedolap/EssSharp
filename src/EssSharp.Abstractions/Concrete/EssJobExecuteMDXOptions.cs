using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssJobExecuteMDXOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        /// <param name="scriptName">The name of script to execute. Maps to <see cref="File" />.</param>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        public EssJobExecuteMDXOptions( string scriptName, string applicationName = null, string cubeName = null ) : base(EssJobType.Calc)
        {
            if ( string.IsNullOrEmpty(scriptName) )
                throw new ArgumentNullException($@"The name of a script is required to create an {nameof(EssJobScriptOptions)} with this constructor.", nameof(scriptName));

            ApplicationName = applicationName;
            CubeName        = cubeName;
            
        }

        #region IEssJobOptions EssJobType.Calc Members



        #endregion
    }
}
