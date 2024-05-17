using System.Collections.Generic;

namespace EssSharp
{
    /// <summary>
    /// The layout grid for the requested grid.
    /// </summary>
    public interface IEssLayout : IEssObject
    {
        /// <summary>
        /// Returns the alias.
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// Returns the data for this layout.
        /// </summary>
        public EssGridLayoutData Data { get; }

        /// <summary>
        /// Returns list of dimensions.
        /// </summary>
        public List<EssGridDimension> Dimension { get; }
    }
}
