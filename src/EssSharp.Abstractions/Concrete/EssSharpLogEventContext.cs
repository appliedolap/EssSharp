using System;

namespace EssSharp
{
    /// <summary />
    public class EssSharpLogEventContext
    {
        /// <summary />
        public string Path { get; set; }

        /// <summary />
        public DateTime Time { get; set; } = DateTime.UtcNow;
    }
}
