using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public interface IEssGrid : IEssObject
    {

        /// <summary>
        /// 
        /// </summary>
        public IEssCube Cube { get; }

        /// <summary>
        /// Gets the Name as a string
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<EssGridDimension> Dimensions { get; }

        /// <summary>
        /// 
        /// </summary>
        public EssGridSlice Slice { get; }
    }
}
