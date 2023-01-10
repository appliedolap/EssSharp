using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssUrl : EssObject, IEssUrl
    {
        #region Private Data

        private readonly EssbaseURL  _url;

        #endregion

        #region Constructors

        /// <summary />
        public EssUrl( EssbaseURL url )
        {
            _url = url;
        }

        #endregion

        #region IEssObject Members
        /// <inheritdoc />
        public override EssType Type => EssType.Url;
        #endregion
        
        #region IEssUrl Members

        /// <inheritdoc />
        public string Url => _url?.Url;

        /// <inheritdoc />
        public override string Name => string.Empty;
        #endregion
    }
}
