using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    /// <summary />
    public class EssGridLayoutData
    {
        /// <inheritdoc />
        public List<List<string>> Statuses { get; set; }

        /// <inheritdoc />
        public List<List<string>> Texts { get; set; }

        /// <inheritdoc />
        public List<List<string>> EnumIds { get; set; }

        /// <inheritdoc />
        public List<List<string>> DataFormats { get; set; }

        /// <inheritdoc />
        public List<List<string>> Types { get; set; }

        /// <inheritdoc />
        public List<List<string>> Filters { get; set; }

        /// <inheritdoc />
        public List<List<string>> Values { get; set; }
    }
}
