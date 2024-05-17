using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    /// <summery />
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

        /// <inheritdoc />
        public Dictionary<string, ColumnMapping> ColumnMappings { get; set; }

        /// <inheritdoc />
        public List<string> Columns { get; set; }

        /// <inheritdoc />
        public string DataSourceName { get; set; }

        /// <inheritdoc />
        public List<string> DrillableRegions { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Type { get; set; }

        /// <inheritdoc />
        public string Url { get; set; }

        /// <inheritdoc />
        public bool UseTempTables { get; set; }
    }
}
