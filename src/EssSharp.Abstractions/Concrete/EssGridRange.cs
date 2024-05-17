using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
 {
    /// <summary />
    public class EssGridRange
    {
        /// <inheritdoc />
        public List<string> DataFormats { get; set; }

        /// <inheritdoc />
        public int End { get; set; }

        /// <inheritdoc />
        public List<string> EnumIds { get; set; }

        /// <inheritdoc />
        public List<string> Filters { get; set; }

        /// <inheritdoc />
        public int Start { get; set; }

        /// <inheritdoc />
        public List<string> Statuses { get; set; }

        /// <inheritdoc />
        public List<string> Texts { get; set; }

        /// <inheritdoc />
        public List<string> Types { get; set; }

        /// <inheritdoc />
        public List<string> Values { get; set; }

    }
}
