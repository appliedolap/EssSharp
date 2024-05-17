using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssFile : EssObject, IEssFile
    {
        #region Private Data

        private readonly EssServer _server;
        private FileBean _file;

        #endregion

        #region Constructors

        /// <summary />
        internal EssFile( FileBean file, EssServer server ) : base( server?.Configuration, server?.Client ) 
        {
            _server = server ??
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssFile)}.");

            _file = file ?? throw new ArgumentNullException(nameof(file), $"An API model {nameof(file)} is required to create an {nameof(EssFile)}.");
        }

        #endregion

        #region Internal Members 

        /// <summary />
        internal FileBean FileBean
        { 
            get => _file; 
            set => _file = value ??
                throw new ArgumentNullException(nameof(FileBean), $"An API model file is required to update an {nameof(EssFile)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc/>
        public override string Name => _file.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.File;

        #endregion

        #region IEssFile Properties

        /// <inheritdoc/>
        public IEssServer Server => _server;

        /// <inheritdoc/>
        public string FullPath => _file.FullPath;

        /// <inheritdoc />
        /// <returns>A <see cref="string"/>.</returns>
        public string ParentPath
        {
            get
            {
                var pathComponents = _file.FullPath?.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                return pathComponents?.Length > 1
                    ? $"/{string.Join(@"/", pathComponents.Take(pathComponents.Length - 1))}"
                    : string.Empty;
            }
        }

        #endregion

        #region IEssFile Methods

        /// <inheritdoc/>
        public virtual void Copy( string newFilePath, bool overwrite = false ) =>
            CopyAsync( newFilePath, overwrite ).GetAwaiter().GetResult();

        /// <inheritdoc/>
        public virtual async Task CopyAsync( string newFilePath, bool overwrite = false, CancellationToken cancellationToken = default )
        {
            try
            {
                if (string.IsNullOrEmpty(newFilePath))
                    throw new ArgumentNullException(nameof(newFilePath), $"An {nameof(newFilePath)} is required.");

                var api = GetApi<FilesApi>();
                var filePathDetails = new FilePathDetail(newFilePath, FullPath);
                await api.FilesCopyResourceAsync(filePathDetails, overwrite, 0, cancellationToken).ConfigureAwait(false);

                if (await Server.GetFileAsync(filePathDetails.To, cancellationToken).ConfigureAwait(false) is not EssFile file)
                    throw new Exception("Renamed file not found.");
            }
            catch ( OperationCanceledException ) { throw; }
            catch (Exception e)
            {
                throw new Exception($@"Unable to rename the file ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc/>
        /// <returns> A <see cref="Stream"/> object.</returns>
        public Stream Download() => DownloadAsync().GetAwaiter().GetResult();

        /// <inheritdoc/>
        /// <returns> A <see cref="Stream"/> object.</returns>
        public async Task<Stream> DownloadAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<FilesApi>();
                return await api.FilesDownloadFileAsync(FullPath.Trim('/'), cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public void Delete() => DeleteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<FilesApi>();
                await api.FilesDeleteFileAsync(FullPath?.Trim('/')).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public virtual void Move( string newFilePath, bool overwrite = false ) => MoveAsync( newFilePath, overwrite )?.GetAwaiter().GetResult();

        /// <inheritdoc/>
        public virtual async Task MoveAsync( string newFilePath, bool overwrite = false, CancellationToken cancellationToken = default )
        {
            try
            {
                if ( string.IsNullOrEmpty(newFilePath) )
                    throw new ArgumentNullException(nameof(newFilePath), $"An {nameof(newFilePath)} is required.");

                var api = GetApi<FilesApi>();
                var newPathDetail = new FilePathDetail(newFilePath, FullPath);
                await api.FilesMoveResourceAsync(newPathDetail, overwrite, 0, cancellationToken).ConfigureAwait(false);

                if ( await Server.GetFileAsync(newPathDetail.To, cancellationToken).ConfigureAwait(false) is not EssFile file )
                    throw new Exception("Renamed file not found.");

                // Update our model FileBean with the move FileBean.
                FileBean = file.FileBean;
            }
            catch ( OperationCanceledException ) { throw; }
            catch (Exception e)
            {
                throw new Exception($@"Unable to rename the file ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc/>
        public void Rename( string newFilePath, bool overwrite = false ) => RenameAsync( newFilePath, overwrite )?.GetAwaiter().GetResult();

        /// <inheritdoc/>
        public virtual async Task RenameAsync( string newFileName, bool overwrite = false, CancellationToken cancellationToken = default )
        {
            try
            {
                if ( string.IsNullOrEmpty(newFileName) )
                    throw new ArgumentNullException(nameof(newFileName), $"An {nameof(newFileName)} is required.");

                var api = GetApi<FilesApi>();
                var newPathDetail = new FilePathDetail($@"{ParentPath}/{newFileName}", FullPath);
                await api.FilesMoveResourceAsync(newPathDetail, overwrite, 0, cancellationToken).ConfigureAwait(false);

                if ( await Server.GetFileAsync(newPathDetail.To, cancellationToken).ConfigureAwait(false) is not EssFile file )
                    throw new Exception("Renamed file not found.");

                // Update our model FileBean with the renamed FileBean.
                FileBean = file.FileBean;
            }
            catch ( OperationCanceledException ) { throw; }
            catch (Exception e)
            {
                throw new Exception($@"Unable to rename the file ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc/>
        public virtual void Extract( bool overwrite = false ) => ExtractAsync( overwrite ).GetAwaiter().GetResult();

        /// <inheritdoc/>
        public virtual async Task ExtractAsync( bool overwrite = false, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<FilesApi>();
                var zipFileDetails = new ZipFileDetails(FullPath);
                await api.FilesExtractAsync(zipFileDetails, overwrite, 0, cancellationToken).ConfigureAwait(false);
            }
            catch ( OperationCanceledException ) { throw; }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
