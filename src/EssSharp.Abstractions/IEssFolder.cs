using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp
{
    /// <summary />
    public interface IEssFolder : IEssFile
    {
        #region Methods

        /// <summary>
        /// Creates a subfolder with the given <paramref name="subfolderName" /> if it does not already exist.
        /// If the subfolder already exists, it is returned.
        /// </summary>
        /// <param name="subfolderName" />
        public IEssFolder CreateSubfolder( string subfolderName );

        /// <summary>
        /// Asynchronously creates a subfolder with the given <paramref name="subfolderName" /> if it does not already exist.
        /// If the subfolder already exists, it is returned.
        /// </summary>
        /// <param name="subfolderName" />
        /// <param name="cancellationToken" />
        public Task<IEssFolder> CreateSubfolderAsync( string subfolderName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the immediate file of this folder with the given <paramref name="filename" />.
        /// </summary>
        /// <param name="filename" />
        public IEssFile GetFile( string filename );

        /// <summary>
        /// Asynchronously gets the immediate file of this folder with the given <paramref name="filename" />.
        /// </summary>
        /// <param name="filename" />
        /// <param name="cancellationToken" />
        public Task<IEssFile> GetFileAsync( string filename, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a list of the immediate files of this folder, optionally matching a given <paramref name="nameFilter" />.
        /// </summary>
        /// <param name="nameFilter" />
        public List<IEssFile> GetFiles( string nameFilter = null );

        /// <summary>
        /// Asynchronously gets a list of the immediate files of this folder, optionally matching a given <paramref name="nameFilter" />.
        /// </summary>
        /// <param name="nameFilter" />
        /// <param name="cancellationToken" />
        public Task<List<IEssFile>> GetFilesAsync( string nameFilter = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the immediate subfolder of this folder with the given <paramref name="folderName" />.
        /// </summary>
        /// <param name="folderName" />
        public IEssFolder GetFolder( string folderName );

        /// <summary>
        /// Asynchronously gets the immediate subfolder of this folder with the given <paramref name="folderName" />.
        /// </summary>
        /// <param name="folderName" />
        /// <param name="cancellationToken" />
        public Task<IEssFolder> GetFolderAsync( string folderName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a list of the immediate subfolders of this folder, optionally matching a given <paramref name="nameFilter" />.
        /// </summary>
        /// <param name="nameFilter" />
        public List<IEssFolder> GetFolders( string nameFilter = null );

        /// <summary>
        /// Asynchronously gets a list of the immediate subfolders of this folder, optionally matching a given <paramref name="nameFilter" />.
        /// </summary>
        /// <param name="nameFilter" />
        /// <param name="cancellationToken" />
        public Task<List<IEssFolder>> GetFoldersAsync( string nameFilter = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Uploads a local file with the given <paramref name="path"/> to the server with the given <paramref name="filename"/>, 
        /// optionally overwriting an existing file on the server if it already exists.
        /// </summary>
        /// <param name="path" />
        /// <param name="filename" />
        /// <param name="overwrite" />
        public IEssFile UploadFile( string path, string filename = null, bool overwrite = false );

        /// <summary>
        /// Asynchronously uploads a local file with the given <paramref name="path"/> to the server with the given <paramref name="filename"/>, 
        /// optionally overwriting an existing file on the server if it already exists.
        /// </summary>
        /// <param name="path" />
        /// <param name="filename" />
        /// <param name="overwrite" />
        /// <param name="cancellationToken" />
        public Task<IEssFile> UploadFileAsync( string path, string filename = null, bool overwrite = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Uploads a local file <paramref name="stream"/> to the server with the given <paramref name="filename"/>, 
        /// optionally overwriting an existing file on the server if it already exists.
        /// </summary>
        /// <param name="stream" />
        /// <param name="filename" />
        /// <param name="overwrite" />
        public IEssFile UploadFile( FileStream stream, string filename = null, bool overwrite = false );

        /// <summary>
        /// Asynchronously uploads a local file <paramref name="stream"/> to the server with the given <paramref name="filename"/>, 
        /// optionally overwriting an existing file on the server if it already exists.
        /// </summary>
        /// <param name="stream" />
        /// <param name="filename" />
        /// <param name="overwrite" />
        /// <param name="cancellationToken" />
        public Task<IEssFile> UploadFileAsync( FileStream stream, string filename = null, bool overwrite = false, CancellationToken cancellationToken = default );

        /// <summary>
        /// Upload file to Server using Stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filename"></param>
        /// <param name="overwrite"></param>
        public IEssFile UploadFile( Stream stream, string filename = null, bool overwrite = false );

        /// <summary>
        /// Asynchronously uploads an arbitrary <paramref name="stream"/> to the server with the given <paramref name="filename"/>, 
        /// optionally overwriting an existing file on the server if it already exists.
        /// </summary>
        /// <param name="stream" />
        /// <param name="filename" />
        /// <param name="overwrite" />
        /// <param name="cancellationToken" />
        public Task<IEssFile> UploadFileAsync( Stream stream, string filename = null, bool overwrite = false, CancellationToken cancellationToken = default );

        #endregion
    }

    /// <summary>
    /// Fluent extensions for <see cref="EssSharp" />.
    /// </summary>
    public static partial class FluentExtensions
    {
        /// <summary>
        /// Asynchronously creates a subfolder with the given <paramref name="subfolderName" /> if it does not already exist.
        /// If the subfolder already exists, it is returned.
        /// </summary>
        /// <param name="folderTask" />
        /// <param name="subfolderName" />
        /// <param name="cancellationToken" />
        public static async Task<IEssFolder> CreateSubfolderAsync( this Task<IEssFolder> folderTask, string subfolderName, CancellationToken cancellationToken = default ) =>
            await (await folderTask.ConfigureAwait(false)).CreateSubfolderAsync(subfolderName, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously uploads a local file with the given <paramref name="path"/> to the server with the given <paramref name="filename"/>, 
        /// optionally overwriting an existing file on the server if it already exists.
        /// </summary>
        /// <param name="folderTask" />
        /// <param name="path" />
        /// <param name="filename" />
        /// <param name="overwrite" />
        /// <param name="cancellationToken" />
        public static async Task<IEssFile> UploadFileAsync( this Task<IEssFolder> folderTask, string path, string filename = default, bool overwrite = default, CancellationToken cancellationToken = default ) =>
            await(await folderTask.ConfigureAwait(false)).UploadFileAsync(path, filename, overwrite, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously uploads a local file <paramref name="stream"/> to the server with the given <paramref name="filename"/>, 
        /// optionally overwriting an existing file on the server if it already exists.
        /// </summary>
        /// <param name="folderTask" />
        /// <param name="stream" />
        /// <param name="filename" />
        /// <param name="overwrite" />
        /// <param name="cancellationToken" />
        public static async Task<IEssFile> UploadFileAsync( this Task<IEssFolder> folderTask, FileStream stream, string filename = default, bool overwrite = default, CancellationToken cancellationToken = default ) =>
            await (await folderTask.ConfigureAwait(false)).UploadFileAsync(stream, filename, overwrite, cancellationToken).ConfigureAwait(false);

        /// <summary>
        /// Asynchronously uploads an arbitrary <paramref name="stream"/> to the server with the given <paramref name="filename"/>, 
        /// optionally overwriting an existing file on the server if it already exists.
        /// </summary>
        /// <param name="folderTask" />
        /// <param name="stream" />
        /// <param name="filename" />
        /// <param name="overwrite" />
        /// <param name="cancellationToken" />
        public static async Task<IEssFile> UploadFileAsync( this Task<IEssFolder> folderTask, Stream stream, string filename = default, bool overwrite = default, CancellationToken cancellationToken = default ) =>
            await (await folderTask.ConfigureAwait(false)).UploadFileAsync(stream, filename, overwrite, cancellationToken).ConfigureAwait(false);
    }
}
