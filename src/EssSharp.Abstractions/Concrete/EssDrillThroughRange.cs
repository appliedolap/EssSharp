using System;
using System.Collections.Generic;
using System.Linq;

namespace EssSharp
{
	public class EssDrillThroughRange : IEssDrillThroughRange
	{
        #region Constructors

        /// <summary />
        public EssDrillThroughRange() { }

		/// <summary />
		/// <param name="dimensionMemberSets" />
		public EssDrillThroughRange( Dictionary<string, List<string>> dimensionMemberSets ) { DimensionMemberSets = dimensionMemberSets; }

        /// <summary />
        /// <param name="dimensionMemberSet" />
        public EssDrillThroughRange( Dictionary<string, string> dimensionMemberSet )
        {
            DimensionMemberSets = dimensionMemberSet?.ToDictionary(kvp => kvp.Key, kvp => new List<string>() { kvp.Value });
        }

        #endregion

        #region IEssDrillThroughRange Members

        /// <inheritdoc />
        public Dictionary<string, List<string>> DimensionMemberSets { get; set; } = new Dictionary<string, List<string>>();

        #endregion
    }
}

