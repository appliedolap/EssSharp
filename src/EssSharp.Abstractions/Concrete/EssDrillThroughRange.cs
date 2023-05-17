using System;
using System.Collections.Generic;
using System.Linq;

namespace EssSharp
{
	public class EssDrillthroughRange : IEssDrillthroughRange
	{
        #region Constructors

        /// <summary />
        public EssDrillthroughRange() { }

		/// <summary />
		/// <param name="dimensionMemberSets" />
		public EssDrillthroughRange( Dictionary<string, List<string>> dimensionMemberSets ) { DimensionMemberSets = dimensionMemberSets; }

        /// <summary />
        /// <param name="dimensionMemberSet" />
        public EssDrillthroughRange( Dictionary<string, string> dimensionMemberSet )
        {
            DimensionMemberSets = dimensionMemberSet?.ToDictionary(kvp => kvp.Key, kvp => new List<string>() { kvp.Value });
        }

        #endregion

        #region IEssDrillthroughRange Members

        /// <inheritdoc />
        public Dictionary<string, List<string>> DimensionMemberSets { get; set; } = new Dictionary<string, List<string>>();

        #endregion
    }
}

