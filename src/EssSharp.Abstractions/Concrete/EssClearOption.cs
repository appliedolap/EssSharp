using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EssSharp
{
    /// <summary>
    /// Defines ClearOptions
    /// </summary>
    public enum EssClearOption
    {
        /// <summary>
        /// All data, linked objects, and the outline are cleared
        /// </summary>
        ALL_DATA = 0,

        /// <summary>
        /// Upper lever blocks are cleared
        /// </summary>
        UPPER_LEVEL_BLOCKS = 1,

        /// <summary>
        /// Level zero blocks are cleared
        /// </summary>
        LEVEL_ZERO_BLOCKS = 2,

        /// <summary>
        /// Non input blocks are cleared
        /// </summary>
        NON_INPUT_BLOCKS = 3

    }

    public enum EssAggregateClear
    {
        /// <summary>
        /// All data, linked objects, and the outline are cleared
        /// </summary>
        ALL_DATA = 0,

        /// <summary>
        /// only specified data region(s) cleared 
        /// </summary>
        PARTIAL_DATA = 1,

        /// <summary>
        /// all aggrefated data is cleared
        /// </summary>
        ALL_AGGREGATIONS = 2
    }
}
