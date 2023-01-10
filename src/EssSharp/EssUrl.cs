using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssUrl : EssObject
    {
        #region Private Data

        private readonly EssApplication _application;
        private readonly EssbaseURL           _url;

        #endregion

        #region Constructors

        /// <summary />
        public EssUrl( EssApplication application, EssbaseURL url ) : base(application?.Configuration, application?.Client)
        {
            _application = application;
            _url        = url;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => string.Empty;

        /// <inheritdoc />
        public override EssType Type => EssType.Url;

        #endregion

        #region IEssCube Members

        /// <inheritdoc />
        public IEssApplication Application => _application;



        #endregion
        #region IEssUrl Members

        /// <inheritdoc />
        public string Url => _url?.Url;

        #endregion
    }
}
