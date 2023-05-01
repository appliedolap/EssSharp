using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssFolder : IEssFile
    {
        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// create a subfolder in this folder
        /// </summary>
        /// <param name="subfolderName"></param>
        /// <param name="append"></param>
        public IEssFolder CreateSubfolder( string subfolderName, bool append = default );

        /// <summary>
        /// create a subfolder in this folder
        /// </summary>
        /// <param name="subfolderName"></param>
        /// <param name="append"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssFolder> CreateSubfolderAsync( string subfolderName, bool append = default, CancellationToken cancellationToken = default );

        /// <summary>
        /// Upload file to Server using a Filepath
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="overwrite"></param>
        public IEssFile UploadFile( string path, string fileName = default, bool overwrite = default );

        /// <summary>
        /// Upload file to Server using FileStream 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="overwrite"></param>
        public IEssFile UploadFile( FileStream stream, string fileName = default, bool overwrite = default );

        /// <summary>
        /// Upload file to Server using Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="overwrite"></param>
        public IEssFile UploadFile( Stream stream, string fileName = default, bool overwrite = default );

        /// <summary>
        /// Upload file to Server using a Filepath
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="overwrite"></param>
        public Task<IEssFile> UploadFileAsync( string path, string fileName = default, bool overwrite = default, CancellationToken cancellationToken = default );

        /// <summary>
        /// Upload file to Server using FileStream 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="overwrite"></param>
        public Task<IEssFile> UploadFileAsync( FileStream stream, string fileName = default, bool overwrite = default, CancellationToken cancellationToken = default );

        /// <summary>
        /// Upload file to Server using Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="overwrite"></param>
        public Task<IEssFile> UploadFileAsync( Stream stream, string fileName = default, bool overwrite = default, CancellationToken cancellationToken = default );

        /// <summary>
        /// Get a lst of all Files in specified folder
        /// </summary>
        /// <param name="nameFilter"></param>
        public List<IEssFile> GetFiles( string nameFilter = null );

        /// <summary>
        /// Get a list of all Files in specified folder
        /// </summary>
        /// <param name="nameFilter"></param>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssFile>> GetFilesAsync( string nameFilter = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Get a list of all Folders in specified folder
        /// </summary>
        /// <param name="nameFilter"></param>
        public List<IEssFolder> GetFolders( string nameFilter = null );

        /// <summary>
        /// Get a list of all Folders in specified folder
        /// </summary>
        /// <param name="nameFilter"></param>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssFolder>> GetFoldersAsync( string nameFilter = null, CancellationToken cancellationToken = default );

        #endregion
    }
}
