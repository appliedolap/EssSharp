using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public class EssGridSelection
    {
        public EssGridSelection(int startRow, int startColumn, int rowCount = 1, int columnCount = 1 )
        {
            this.startRow = startRow;

            this.startColumn = startColumn;

            this.rowCount = rowCount;

            this.columnCount = columnCount;
        } 

        public int startRow { get; set; }

        public int startColumn { get; set; }

        public int rowCount { get; set; }

        public int columnCount { get; set; }
    }
}
