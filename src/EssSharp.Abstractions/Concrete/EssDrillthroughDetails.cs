using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public class EssDrillthroughDetails
    {
        public Dictionary<string, EssColumnMapping> ColumnMapping { get; set; }

        public List<string> Columns { get; set; }

        public string DataSourceName { get; set; }

        public List<string> DrillableRegions { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public bool UseTempTables { get; set; }
    }
}
