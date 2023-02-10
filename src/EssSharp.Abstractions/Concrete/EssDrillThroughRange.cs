using System;
using System.Collections.Generic;

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

        #endregion

        #region IEssDrillThroughRange Members

        /// <inheritdoc />
        public Dictionary<string, List<string>> DimensionMemberSets { get; set; } = new Dictionary<string, List<string>>();

        #endregion
    }
}

