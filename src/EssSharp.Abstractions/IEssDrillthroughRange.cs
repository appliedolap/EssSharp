using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public interface IEssDrillthroughRange
    {
        #region Properties

        /// <summary>
        /// Returns or sets the list of values for each member in the contiguous drill-through range.
        /// </summary>
        Dictionary<string, List<string>> DimensionMemberSets { get; set; }

        #endregion
    }
}
