using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    /// <summary />
    public class EssGridDimension
    {
        /// <inheritdoc />
        public int Column { get; set; }

        /// <inheritdoc />
        public int Row { get; set; }

        /// <inheritdoc />
        public string DisplayName { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Number { get; set; }

        /// <inheritdoc />
        public string Pov { get; set; }

        /// <inheritdoc />
        public bool Expanded { get; set; }

        /// <inheritdoc />
        public bool Hidden { get; set; }
    }
}
