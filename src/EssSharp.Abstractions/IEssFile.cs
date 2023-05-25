using System;
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
        /// Download a file to a Stream
        /// </summary>
        public Stream Download();

        /// <summary>
        /// Download a file to a Stream
        /// </summary>
        /// <param name="cancellationToken"></param>
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
        /// <param name="newFilePath"></param>
        /// <param name="overwrite"></param>
        /// <param name="cancellationToken"></param
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
}
