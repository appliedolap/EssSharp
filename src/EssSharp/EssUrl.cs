using System;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssUrl : EssObject, IEssUrl
    {
        #region Private Data

        private readonly EssServer  _server;
        private readonly EssbaseURL _url;

        #endregion

        #region Constructors

        /// <summary />
        internal EssUrl( EssbaseURL url, EssServer server = null ) : base(server?.Configuration, server?.Client)
        {
            _url = url ?? 
                throw new ArgumentNullException(nameof(url), $"An API model {nameof(url)} is required to create an {nameof(EssUrl)}.");

            _server = server;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _url?.Application;

        /// <inheritdoc />
        public override EssType Type => EssType.Url;

        #endregion

        #region IEssUrl Members

        /// <inheritdoc />
        public string Path => _url?.Url;

        /// <inheritdoc />
        public Uri Url
        {
            get
            {
                if ( !Uri.TryCreate($@"{_server?.Name}/{_url?.Url}", UriKind.Absolute, out var url) )
                    throw new Exception("Unable to construct an absolute URL for the resource.");

                return url;
            }
        }

        #endregion
    }
}
