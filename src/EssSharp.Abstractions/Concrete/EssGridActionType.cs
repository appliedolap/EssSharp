using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EssSharp
{
    public enum EssGridActionType
    {
        /// <summary>
        /// enum Unknown for value: unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Enum Zoomin for value: zoomin
        /// </summary>
        Zoomin = 1,

        /// <summary>
        /// Enum Zoomout for value: zoomout
        /// </summary>
        Zoomout = 2,

        /// <summary>
        /// Enum Keeponly for value: keeponly
        /// </summary>
        Keeponly = 3,

        /// <summary>
        /// Enum Removeonly for value: removeonly
        /// </summary>
        Removeonly = 4,

        /// <summary>
        /// Enum Refresh for value: refresh
        /// </summary>
        Refresh = 5,

        /// <summary>
        /// Enum Pivot for value: pivot
        /// </summary>
        Pivot = 6,

        /// <summary>
        /// Enum PivotToPOV for value: pivotToPOV
        /// </summary>
        PivotToPOV = 7,

        /// <summary>
        /// Enum Submit for value: submit
        /// </summary>
        Submit = 8
    }
}
