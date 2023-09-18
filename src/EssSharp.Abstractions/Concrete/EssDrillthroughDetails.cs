using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public class EssDrillthroughDetails
    {
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

        public Dictionary<string, ColumnMapping> ColumnMappings { get; set; }

        public List<string> Columns { get; set; }

        public string DataSourceName { get; set; }

        public List<string> DrillableRegions { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public bool UseTempTables { get; set; }
    }
}
