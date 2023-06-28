using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public class EssQuery
    {
        public EssQuery( string discription = null, string spec = null, EssQueryPreferences preferences = default)
        {
            Discription = discription;
            Spec = spec;
            Preferences = preferences;
        }

        public string Discription { get; set; }

        public string Spec { get; set; }

        public EssQueryPreferences Preferences { get; set; } = new EssQueryPreferences();
    }
}
