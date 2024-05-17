using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    /// <summary />
    public class EssGridSlice
    {
        /// <inheritdoc />
        public int Columns { get; set; }

        /// <inheritdoc />
        public EssGridSliceData Data { get; set; } = new EssGridSliceData();

        /// <inheritdoc />
        public List<int> DirtyCells { get; set; } = new List<int>();

        /// <inheritdoc />
        public List<int> DirtyTexts { get; set; } = new List<int>();

        /// <inheritdoc />
        public int Rows { get; set; }

    }
}
