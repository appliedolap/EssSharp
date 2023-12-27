using System;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssGeneration : IEssGeneration
    {
        #region Private Data

        private readonly EssServer  _server;
        private readonly GenerationLevel _generationLevel;

        #endregion

        #region Constructors

        /// <summary />
        internal EssGeneration( GenerationLevel generationLevel )
        {
            _generationLevel = generationLevel ?? 
                throw new ArgumentNullException(nameof(generationLevel), $"An API model {nameof(generationLevel)} is required to create an {nameof(EssGeneration)}.");
        }

        #endregion

        #region IEssUrl Members

        /// <inheritdoc />
        public string Name => _generationLevel?.Name;

        /// <inheritdoc />
        public string ActualName => _generationLevel?.ActualName;

        /// <inheritdoc />
        public int Number => _generationLevel.Number;

        #endregion
    }
}
