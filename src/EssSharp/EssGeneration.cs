using System;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssGeneration : IEssGeneration
    {
        #region Private Data

        private readonly GenerationLevel _generation;

        #endregion

        #region Constructors

        /// <summary />
        internal EssGeneration( GenerationLevel generation )
        {
            _generation = generation ?? 
                throw new ArgumentNullException(nameof(generation), $"An API model {nameof(GenerationLevel)} is required to create an {nameof(EssGeneration)}.");
        }

        #endregion

        #region IEssGeneration Members

        /// <inheritdoc />
        public string Name => _generation?.Name;

        /// <inheritdoc />
        public string ActualName => _generation?.ActualName;

        /// <inheritdoc />
        public int Number => _generation.Number;

        #endregion
    }
}
