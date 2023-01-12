using EssSharp.Client;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssDimension : IEssDimension
    {
        #region Private Data
        private readonly DimensionBean _dimensionBean;
        private readonly EssCube _cube;
        #endregion

        #region Constructors
        /// <summary />
        public EssDimension( EssCube cube, DimensionBean dimensionBean )
        {
            _cube = cube;
            _dimensionBean = dimensionBean;
        }
        #endregion
        #region IEssDimensionMembers
        /// <inheritdoc />
        public string Name =>_dimensionBean.Name;
        #endregion
    }
}
