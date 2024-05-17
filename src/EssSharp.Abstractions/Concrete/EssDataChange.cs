using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <inheritdoc />
    public class EssDataChanges
    {
        /// <inheritdoc />
        public string Server { get; set; }

        /// <inheritdoc />
        public string Application { get; set; }

        /// <inheritdoc />
        public string Database { get; set; }

        /// <inheritdoc />
        public string User { get; set; }

        /// <inheritdoc />
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <inheritdoc />
        public List<EssDataChange> DataChanges = new List<EssDataChange>();
    }

    public class EssDataChange
    {
        /// <inheritdoc />
        public double OldValue { get; set; }

        /// <inheritdoc />
        public double NewValue { get; set; }

        /// <inheritdoc />
        public List<EssDataPoint> DataPoints = new List<EssDataPoint>();
    }

    public class EssDataPoint
    {
        /// <inheritdoc />
        public string DimensionName { get; set; }

        /// <inheritdoc />
        public int DimensionNumber { get; set; }

        /// <inheritdoc />
        public string Member { get; set; }

        /// <inheritdoc />
        public string Alias { get; set; }
    }
}

