using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssUtility : EssObject, IEssUtility
    {
        #region Private Data

        private readonly EssServer  _server;
        private readonly Resource _resource;

        #endregion

        #region Constructors

        /// <summary />
        internal EssUtility( Resource resource, EssServer server ) : base(server?.Configuration, server?.Client)
        {
            _resource = resource ?? 
                throw new ArgumentNullException(nameof(resource), $"An API model {nameof(resource)} is required to create an {nameof(EssUtility)}.");

            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssUtility)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _resource?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Utility;

        #endregion

        #region IEssUtility Members

        /// <inheritdoc />
        public IEssServer Server => _server;

        /// <inheritdoc />
        /// <remarks>Returns false if the links collection is null or empty.</remarks>
        public bool IsDownloadable => _resource?.Links?.Count > 0;

        /// <inheritdoc />
        public string Path => _resource?.Url;

        /// <inheritdoc />
        public Uri Url
        {
            get
            {
                if ( !Uri.TryCreate($@"{_server?.Name}/{_resource?.Url}", UriKind.Absolute, out var url) )
                    throw new Exception("Unable to construct an absolute URL for the resource.");

                return url;
            }
        }

        /// <inheritdoc />
        public Stream Download() => DownloadAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<Stream> DownloadAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var api = GetApi<TemplatesAndUtilitiesApi>();
                var fileStream = await api.ResourcesDownloadUtilityFileStreamAsync(_resource?.Id, 0, cancellationToken).ConfigureAwait(false);

                return fileStream;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
