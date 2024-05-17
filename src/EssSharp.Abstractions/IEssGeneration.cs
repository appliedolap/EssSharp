using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssGeneration
    {
        /// <summary>
        /// Returns the nanme of this generation.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Returns the actual name of this generation.
        /// </summary>
        public string ActualName { get; }

        /// <summary>
        /// Returns the generation number.
        /// </summary>
        public int Number { get; }
    }
}
