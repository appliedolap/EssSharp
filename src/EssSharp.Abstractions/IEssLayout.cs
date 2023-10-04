using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    public interface IEssLayout : IEssObject
    {
        public string Alias { get; }

        public EssGridLayoutData Data { get; }

        public List<EssGridDimension> Dimension { get; }
    }
}
