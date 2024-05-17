using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    /// <summary />
    public class EssDrillthroughDetails
    {
        /// <summary />
        public class ColumnMapping
        {
            /// <summary>
            /// Gets or Sets Dimension
            /// </summary>
            public string Dimension { get; set; }

            /// <summary>
            /// Gets or Sets Generation
            /// </summary>
            public string Generation { get; set; }

            /// <summary>
            /// Gets or Sets Level
            /// </summary>
            public string Level { get; set; }

            /// <summary>
            /// Gets or Sets GenerationNumber
            /// </summary>
            public int GenerationNumber { get; set; }
        }

        /// <summary />
        public Dictionary<string, ColumnMapping> ColumnMappings { get; set; }

        /// <summary />
        public List<string> Columns { get; set; }

        /// <summary />
        public string DataSourceName { get; set; }

        /// <summary />
        public List<string> DrillableRegions { get; set; }

        /// <summary />
        public string Name { get; set; }

        /// <summary />
        public string Type { get; set; }

        /// <summary />
        public string Url { get; set; }

        /// <summary />
        public bool UseTempTables { get; set; }
    }
}
