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
        /// Gets cube.
        /// </summary>
        public IEssCube Cube { get; }

        /// <summary>
        /// Gets the Name as a string
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// Gets grid dimension.
        /// </summary>
        public List<EssGridDimension> Dimensions { get; }

        /// <summary>
        /// Gets grid slice.
        /// </summary>
        public EssGridSlice Slice { get; }
    }
}
