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
        public IEssFolder CreateSubfolder(string subfolderName, bool append = default );

        /// <summary>
        /// create a subfolder in this folder
        /// </summary>
        /// <param name="subFolderName"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="append"></param>
        public Task<IEssFolder> CreateSubfolderAsync( string subfolderName, bool append = default, CancellationToken cancellationToken = default);

        public IEssFile UploadFile(string path, string fileName = default, bool overwrite = default);

        public IEssFile UploadFile(FileStream stream, string fileName = default, bool overwrite = default);

        public IEssFile UploadFile(Stream stream, string fileName = default, bool overwrite = default);

        public Task<IEssFile> UploadFileAsync(string path, string fileName = default, bool overwrite = default, CancellationToken cancellationToken = default );

        public Task<IEssFile> UploadFileAsync(FileStream stream, string fileName = default, bool overwrite = default, CancellationToken cancellationToken = default);

        public Task<IEssFile> UploadFileAsync(Stream stream, string fileName = default, bool overwrite = default, CancellationToken cancellationToken = default);

        public List<IEssFile> GetFiles(string nameFilter = null);

        public Task<List<IEssFile>> GetFilesAsync( string nameFilter = null, CancellationToken cancellationToken = default );

        public List<IEssFolder> GetFolders( string nameFilter = null );

        public Task<List<IEssFolder>> GetFoldersAsync( string nameFilter = null, CancellationToken cancellationToken = default );

        #endregion
    }
}
