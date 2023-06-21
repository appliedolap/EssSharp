using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssQueryReport
    {
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
