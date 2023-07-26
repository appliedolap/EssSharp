using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace EssSharp
{
    /// <summary />
    public interface IEssFile : IEssObject
    {
        #region Properties

        /// <summary>
        /// Returns the server that contains this file.
        /// </summary>
        IEssServer Server { get; }

        /// <summary>
        /// Returns the full path of the file.
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// Returns the full path of the parent folder.
        /// </summary>
        string ParentPath { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Copy a file from source to destination
        /// </summary>
        /// <param name="newFilePath"></param>
        /// <param name="overwrite"></param>
        public void Copy( string newFilePath, bool overwrite = default );

        /// <summary>
        /// Copy a file from source to destination
        /// </summary>
        /// <param name="newFilePath"></param>
        /// <param name="overwrite"></param>
        /// <param name="cancellationToken"></param>
        public Task CopyAsync( string newFilePath, bool overwrite = default, CancellationToken cancellationToken = default );

        /// <summary>
        /// Downloads a file to a stream.
        /// </summary>
        public Stream Download();

        /// <summary>
        /// Asynchronously downloads a file to a stream.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<Stream> DownloadAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Delete a specific file
        /// </summary>
        public void Delete();

        /// <summary>
        /// Delete a specific file
        /// </summary>
        public Task DeleteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Moves a file from source to destination
        /// </summary>
        /// <param name="newFilePath"></param>
        /// <param name="overwrite"></param>
        public void Move( string newFilePath, bool overwrite = false );

        /// <summary>
        /// Moves a file from source to destination
        /// </summary>
        /// <param name="newFilePath" />
        /// <param name="overwrite" />
        /// <param name="cancellationToken" />
        public Task MoveAsync( string newFilePath, bool overwrite = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Renames a file or folder
        /// </summary>
        /// <param name="newFileName"></param>
        /// <param name="overwrite"></param>
        public void Rename( string newFileName, bool overwrite = false );

        /// <summary>
        /// Renames a file or folder
        /// </summary>
        /// <param name="newFilePath"></param>
        /// <param name="overwrite"></param>
        /// <param name="cancellationToken"></param>
        public Task RenameAsync( string newFilePath, bool overwrite = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Extract a zip file on same location
        /// </summary>
        /// <param name="overwrite"></param>
        public void Extract( bool overwrite = false );

        /// <summary>
        /// Extract a zip file on same location
        /// </summary>
        /// <param name="overwrite"></param>
        /// <param name="cancellationToken"></param>
        public Task ExtractAsync( bool overwrite = false, CancellationToken cancellationToken = default );

        #endregion
    }

    /// <summary>
    /// Fluent extensions for <see cref="EssSharp" />.
    /// </summary>
    public static partial class FluentExtensions
    {
        /// <summary>
        /// Asynchronously downloads a file to a stream.
        /// </summary>
        /// <param name="fileTask" />
        /// <param name="cancellationToken" />
        public static async Task<Stream> DownloadAsync( this Task<IEssFile> fileTask, CancellationToken cancellationToken = default ) =>
            await (await fileTask.ConfigureAwait(false)).DownloadAsync(cancellationToken).ConfigureAwait(false);
    }
}
