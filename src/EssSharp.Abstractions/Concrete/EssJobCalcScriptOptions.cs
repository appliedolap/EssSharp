using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary>
    /// Options required to execute an <see cref="IEssCalcScript" />, including the values of the runtime substitution
    /// variables it declares.
    /// </summary>
    public class EssJobCalcScriptOptions : EssJobScriptOptions, IEssJobOptions
    {
        /// <summary />
        /// <param name="fileName">The name of the calc script to execute, with its <c>.csc</c> extension. Maps to <see cref="EssJobScriptOptions.Script" />.</param>
        /// <param name="applicationName">Name of application.</param>
        /// <param name="cubeName">Name of cube</param>
        public EssJobCalcScriptOptions( string fileName, string applicationName = null, string cubeName = null ) : base(fileName, applicationName, cubeName)
        {
            if (JobType is not EssJobType.Calc)
                throw new ArgumentException($@"{nameof(EssJobCalcScriptOptions)} requires the {nameof(fileName)} parameter to name a calc script (.csc).", nameof(fileName));
        }

        /// <summary />
        /// <param name="calcScript">The calc script to execute.</param>
        /// <param name="applicationName" />
        /// <param name="cubeName" />
        public EssJobCalcScriptOptions( IEssCalcScript calcScript, string applicationName = null, string cubeName = null ) : base(calcScript, applicationName, cubeName) { }

        #region IEssJobOptions EssJobType.Calc Members

        /// <inheritdoc />
        public Dictionary<string, string> RuntimeSubstitutionVariables { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        #endregion
    }
}
