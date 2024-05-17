using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    /// <summary />
    public class EssQuery
    {
        /// <summary />
        /// <param name="discription"></param>
        /// <param name="spec"></param>
        /// <param name="preferences"></param>
        public EssQuery( string discription = null, string spec = null, EssQueryPreferences preferences = default)
        {
            Discription = discription;
            Spec = spec;
            Preferences = preferences;
        }

        /// <inheritdoc />
        public string Discription { get; set; }

        /// <inheritdoc />
        public string Spec { get; set; }

        /// <inheritdoc />
        public EssQueryPreferences Preferences { get; set; } = new EssQueryPreferences();
    }
}
