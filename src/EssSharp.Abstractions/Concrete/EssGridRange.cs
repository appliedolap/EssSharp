using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public class EssGridRange
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> DataFormats { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> EnumIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Filters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Statuses { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Texts { get; set; }

        /// <summary>
        /// Each cell's type, by the same index as <see cref="Values"/>: "0" for a member, "2" for data, and "7" for a blank
        /// cell. A grid types each cell it sends by its value, so a caller need not give types; a type a caller gives settles
        /// a numeric member name ("0") and a blank data cell ("2"), which a value alone cannot. A grid Essbase returns carries
        /// its own types.
        /// </summary>
        public List<string> Types { get; set; }

        /// <summary>
        /// Each cell's value, row by row. Text is a member, and a member name that reads as a number is written with a leading
        /// apostrophe ('100) unless its type is given. A number is data, written in the invariant culture as Essbase writes
        /// it (1710.5); text in another culture's format (1,710.5) reads as a member. The grid's missing and no-access labels
        /// are data, and an empty value is a blank cell.
        /// </summary>
        public List<string> Values { get; set; }

    }
}
