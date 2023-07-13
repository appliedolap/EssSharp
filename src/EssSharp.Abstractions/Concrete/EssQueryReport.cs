using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssQueryReport
    {
        /// <summary />
        [Flags]
        public enum ReportAxes
        {
            // These are defined with integer values rather than 
            // bit-shifts to allow docfx to sort on values.
            None                = 0,  // 0
            Columns             = 1,  // 1 << 0
            Rows                = 2,  // 1 << 1
            ColumnsAndRows      = 3,  // Columns | Rows
            Pages               = 4,  // 1 << 2
            ColumnsRowsAndPages = 7,  // Columns | Rows | Pages
            Sections            = 8,  // 1 << 3
            Chapters            = 16, // 1 << 4
            All                 = 23  // Columns | Rows | Pages | Sections | Chapters
        }

        /// <summary>
        /// Defines MemberIdentifierType
        /// </summary>
        public enum ReportMemberIdentifier
        {
            Name       = 1,
            Alias      = 2,
            UniqueName = 3
        }

        /// <summary />
        public class ReportMetadata
        {
            /// <summary />
            public List<string> ColumnDimensionMembers { get; set; }

            /// <summary />
            public List<string> PageDimensionMembers { get; set; }

            /// <summary />
            public List<string> RowDimensionMembers { get; set; }
        }

        /// <summary />
        public ReportMetadata Metadata { get; set; }

        /// <summary />
        public object[,] Data { get; set; }
    }
}
