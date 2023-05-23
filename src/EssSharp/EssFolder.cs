using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssFolder : EssFile, IEssFolder
    { 
        #region Private Data
        #endregion

        #region Constructors

        /// <summary />
        internal EssFolder( FileBean folder, EssServer server ) : base(folder, server)
        {
            if ( server is null )
                throw new ArgumentNullException(nameof(server), $"An {nameof(EssServer)} {nameof(server)} is required to create an {nameof(EssFolder)}.");

            if ( folder is null )
                throw new ArgumentNullException(nameof(folder), $"An API model file is required to create an {nameof(EssFolder)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override EssType Type => EssType.Folder;

        #endregion

        #region IEssFile Members

        /// <inheritdoc/>
        /// <exception cref="NotImplementedException">Copying a folder is not supported.</exception>
        public override void Copy( string newFilePath, bool overwrite = false )
            => throw new NotImplementedException("Copying a folder is not supported.");

        /// <inheritdoc/>
        /// <exception cref="NotImplementedException">Copying a folder is not supported.</exception>
        public override Task CopyAsync( string newFilePath, bool overwrite = false, CancellationToken cancellationToken = default )
            => throw new NotImplementedException("Copying a folder is not supported.");

        /// <inheritdoc/>
        /// <exception cref="NotImplementedException">Moving a folder is not supported.</exception>
        public override void Move( string newFilePath, bool overwrite = false )
            => throw new NotImplementedException("Moving a folder is not supported.");

        /// <inheritdoc/>
        /// <exception cref="NotImplementedException">Moving a folder is not supported.</exception>
        public override Task MoveAsync( string newFilePath, bool overwrite = false, CancellationToken cancellationToken = default )
            => throw new NotImplementedException("Moving a folder is not supported.");

        /// <inheritdoc/>
        public override async Task RenameAsync( string newFolderName, bool overwrite = false, CancellationToken cancellationToken = default )
        {
            try
            {
                if ( string.IsNullOrEmpty(newFolderName) )
                    throw new ArgumentNullException(nameof(newFolderName), $"An {nameof(newFolderName)} is required.");

                var api = GetApi<FilesApi>();
                var newPathDetail = new FilePathDetail($@"{ParentPath}/{newFolderName}", FullPath);
                await api.FilesMoveResourceAsync(newPathDetail, overwrite, 0, cancellationToken).ConfigureAwait(false);

                if ( await Server.GetFolderAsync(newPathDetail.To, cancellationToken).ConfigureAwait(false) is not EssFolder folder )
                    throw new Exception("Renamed folder not found.");

                // Update our model FileBean with the renamed FileBean.
                FileBean = folder.FileBean;
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to rename the folder ""{Name}"". {e.Message}", e);
            }
        }

        #endregion

        #region IEssFolder Methods

        /// <inheritdoc/>
        /// <returns>An <see cref="EssFolder"/> object.</returns>
        public IEssFolder CreateSubfolder( string subfolderName ) => CreateSubfolderAsync(subfolderName)?.GetAwaiter().GetResult();

        /// <inheritdoc/>
        /// <returns>An <see cref="EssFolder"/> object.</returns>
        public async Task<IEssFolder> CreateSubfolderAsync( string subfolderName, CancellationToken cancellationToken = default )
        {
            try
            {
                // Trim the given subfolder name.
                subfolderName = subfolderName?.Trim('/');

                var api = GetApi<FilesApi>();

                // If the given subfolder already exists, return the existing subfolder.
                foreach ( var folder in await GetFoldersAsync(subfolderName, cancellationToken).ConfigureAwait(false) )
                    if ( string.Equals(subfolderName, folder?.Name, StringComparison.OrdinalIgnoreCase) )
                        return folder;

                await api.FilesCreateFolderAsync(path: $@"{FullPath}/{subfolderName}".TrimStart('/'), cancellationToken: cancellationToken).ConfigureAwait(false);

                // If the given subfolder was successfully created, return it.
                foreach ( var folder in await GetFoldersAsync(subfolderName, cancellationToken).ConfigureAwait(false) )
                    if ( string.Equals(subfolderName, folder?.Name, StringComparison.OrdinalIgnoreCase) )
                        return folder;

                throw new Exception("Folder not found.");
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create or get the subfolder ""{subfolderName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssFile"/> objects.</returns>
        public List<IEssFile> GetFiles( string nameFilter = null ) => GetFilesAsync()?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssFile"/> objects.</returns>
        public async Task<List<IEssFile>> GetFilesAsync( string nameFilter = null, CancellationToken cancellationToken = default )
        {
            var api = GetApi<FilesApi>();
            var path = FullPath?.TrimStart('/');
            var files = await api.FilesListFilesAsync(path: path, filter: nameFilter, recursive: false, cancellationToken: cancellationToken).ConfigureAwait(false);

            return files?.ToEssSharpList<IEssFile>(Server as EssServer) ?? new List<IEssFile>();
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssFolder"/> objects.</returns>
        public List<IEssFolder> GetFolders( string nameFilter = null ) => GetFoldersAsync(nameFilter)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssFolder"/> objects.</returns>
        public async Task<List<IEssFolder>> GetFoldersAsync( string nameFilter = null, CancellationToken cancellationToken = default )
        {
            var api = GetApi<FilesApi>();
            var path = FullPath?.TrimStart('/');
            var files = await api.FilesListFilesAsync(path: path, type: "folder", filter: nameFilter, recursive: false, cancellationToken: cancellationToken).ConfigureAwait(false);

            return files?.ToEssSharpList<IEssFolder>(Server as EssServer) ?? new List<IEssFolder>();
        }

        /// <inheritdoc/>
        /// <returns>An <see cref="EssFile"/> object.</returns>
        public IEssFile UploadFile( string path, string filename = null, bool overwrite = false ) => UploadFileAsync(path, filename, overwrite)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssFile"/> object.</returns>
        public async Task<IEssFile> UploadFileAsync( string path, string filename = null, bool overwrite = false, CancellationToken cancellationToken = default )
        {
            if ( !File.Exists(path) )
                throw new FileNotFoundException("Unable to find the given file.");

            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            return await UploadFileAsync(stream, filename, overwrite, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssFile"/> object.</returns>
        public IEssFile UploadFile( FileStream stream, string filename = null, bool overwrite = false ) => UploadFileAsync(stream, filename, overwrite)?.GetAwaiter().GetResult();

        /// <inheritdoc/>
        /// <returns>An <see cref="EssFile"/> object.</returns>
        public Task<IEssFile> UploadFileAsync( FileStream stream, string filename = null, bool overwrite = false, CancellationToken cancellationToken = default ) =>
            UploadFileAsync(stream as Stream, filename ?? Path.GetFileName(stream.Name), overwrite, cancellationToken);

        /// <inheritdoc />
        /// <returns>An <see cref="EssFile"/> object.</returns>
        public IEssFile UploadFile( Stream stream, string filename, bool overwrite = false ) => UploadFileAsync(stream, filename, overwrite)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssFile"/> object.</returns>
        public async Task<IEssFile> UploadFileAsync( Stream stream, string filename, bool overwrite = false, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<FilesApi>();

                // if overwrite is false, check for existing file and throw exception if it does, otherwise just overwrite it
                if ( !overwrite )
                {
                    foreach ( var file in await GetFilesAsync().ConfigureAwait(false) )
                        if ( string.Equals(filename, file?.Name, StringComparison.OrdinalIgnoreCase) )
                            throw new Exception("File already exists");
                }

                var path = $"{FullPath?.TrimStart('/')}/{filename}";
                await api.FilesAddFileAsync(path: path, overwrite: overwrite, stream: stream, cancellationToken: cancellationToken).ConfigureAwait(false);

                foreach ( var file in await GetFilesAsync().ConfigureAwait(false) )
                    if ( string.Equals(filename, file?.Name, StringComparison.OrdinalIgnoreCase) )
                        return file;

                throw new Exception("File not found.");
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to create the file ""{filename}"". {e.Message}", e);
            }
        }

        #endregion
    }
}
