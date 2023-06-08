using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssJobClearDataOptions : EssJobOptions, IEssJobOptions
    {
        /// <summary />
        public EssJobClearDataOptions( string applicationName = null, string cubeName = null, EssClearOption option = EssClearOption.ALL_DATA , string dataExpression = null ): base( EssJobType.Clear )
        {
            ApplicationName       = applicationName;
            CubeName              = cubeName;

            Option                = option;
            PartialDataExpression = dataExpression;
        }

        #region IEssJobOptions EssJobType.Clear Members

        /// <inheritdoc />
        public EssClearOption? Option { get; set; }

        /// <inheritdoc />
        public string PartialDataExpression { get; set; }

        #endregion
    }
}
