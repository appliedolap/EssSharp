using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public class EssDataChanges
    {
        /// <summary />
        public string Server { get; set; }

        /// <summary />
        public string Application { get; set; }

        /// <summary />
        public string Database { get; set; }

        /// <summary />
        public string User { get; set; }

        /// <summary />
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary />
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

