using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssUrl : IEssUrl
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
        #region IEssUrl Members

        /// <inheritdoc />
        public string Url => _url?.Url;

        #endregion
    }
}
