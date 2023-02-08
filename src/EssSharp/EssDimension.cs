using System;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssDimension : IEssDimension
    {
        #region Private Data

        private readonly EssCube       _cube;
        private readonly DimensionBean _dimension;

        #endregion

        #region Constructors

        /// <summary />
        internal EssDimension( DimensionBean dimension, EssCube cube )
        {
            _dimension = dimension ??
                throw new ArgumentNullException(nameof(dimension), $"An API model {nameof(dimension)} is required to create an {nameof(EssDimension)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssCube)}.");
        }

        #endregion

        #region IEssDimensionMembers

        /// <inheritdoc />
        public string Name => _dimension?.Name;

        #endregion
    }
}
