using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public class EssGridSlice
    {
        /// <summary>
        /// 
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EssGridSliceData Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> DirtyCells { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> DirtyTexts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Rows { get; set; }

    }
}
