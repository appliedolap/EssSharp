using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    /// <summary />
    public class EssGridSelection
    {
        /// <summary />
        /// <param name="startRow"></param>
        /// <param name="startColumn"></param>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        public EssGridSelection(int startRow, int startColumn, int rowCount = 1, int columnCount = 1 )
        {
            this.startRow = startRow;

            this.startColumn = startColumn;

            this.rowCount = rowCount;

            this.columnCount = columnCount;
        }

        /// <inheritdoc />
        public int startRow { get; set; }

        /// <inheritdoc />
        public int startColumn { get; set; }

        /// <inheritdoc />
        public int rowCount { get; set; }

        /// <inheritdoc />
        public int columnCount { get; set; }
    }
}
