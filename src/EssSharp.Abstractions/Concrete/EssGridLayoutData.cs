using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public class EssGridLayoutData
    {
        public List<List<string>> Statuses { get; set; }

        public List<List<string>> Texts { get; set; }

        public List<List<string>> EnumIds { get; set; }

        public List<List<string>> DataFormats { get; set; }

        public List<List<string>> Types { get; set; }

        public List<List<string>> Filters { get; set; }

        public List<List<string>> Values { get; set; }
    }
}
