using System;
using System.Collections.Generic;

namespace EssSharp
{
    public class EssDataChanges
    {
        public string Server { get; set; }

        public string Application { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<EssDataChange> DataChanges = new List<EssDataChange>();
    }

    public class EssDataChange
    {
        /// <summary />
        public double OldValue { get; set; }

        /// <summary />
        public double NewValue { get; set; }

        /// <summary />
        public List<EssDataPoint> DataPoints = new List<EssDataPoint>();
    }

    public class EssDataPoint
    {
        /// <summary />
        public string DimensionName { get; set; }

        /// <summary />
        public int DimensionNumber { get; set; }

        /// <summary />
        public string Member { get; set; }

        /// <summary />
        public string Alias { get; set; }
    }
}

