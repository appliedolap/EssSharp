using System;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary>
    /// An Essbase script.
    /// </summary>
    public interface IEssScript : IEssObject
    {
        #region IEssObject Properties

        /// <summary>
        /// Returns or sets the name of the script.
        /// </summary>
        public new string Name { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Returns or sets the content of the script.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Returns the cube that holds the script.
        /// </summary>
        IEssCube Cube { get; }

        /// <summary>
        /// Returns the date and time the script was last modified.
        /// </summary>
        public DateTime ModifiedTime { get; }

        /// <summary>
        /// Returns the size of the script in bytes.
        /// </summary>
        public long Size { get; }

        /// <summary >
        /// Returns the type of the script.
        /// </summary>
        public EssScriptType ScriptType { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Copies this script and returns the copy.
        /// </summary>
        /// <param name="newName">The name of the copy.</param>
        /// <remarks>Creates a copy of this <see cref="IEssScript"/> of the specific given type <typeparamref name="T"/>.</remarks>
        public T Copy<T>( string newName ) where T : class, IEssScript;

        /// <summary>
        /// Asynchronously copies this script and returns the copy.
        /// </summary>
        /// <param name="newName">The name of the copy.</param>
        /// <param name="cancellationToken" />
        /// <remarks>Creates a copy of this <see cref="IEssScript"/> of the specific given type <typeparamref name="T"/>.</remarks>
        public Task<T> CopyAsync<T>( string newName, CancellationToken cancellationToken = default ) where T : class, IEssScript;

        /// <summary>
        /// Deletes this script from the cube.
        /// </summary>
        void Delete();

        /// <summary>
        /// Asynchronously deletes this script from the cube.
        /// </summary>
        /// <param name="cancellationToken" />
        Task DeleteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Executes this script.
        /// </summary>
        IEssJob Execute();

        /// <summary>
        /// Asynchronously executes this script.
        /// </summary>
        /// <param name="cancellationToken" />
        Task<IEssJob> ExecuteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets whether this script exists on the cube.
        /// </summary>
        public bool Exists();

        /// <summary>
        /// Asynchronously gets whether this script exists on the cube.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<bool> ExistsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the content of this script.
        /// </summary>
        public string GetContent();

        /// <summary>
        /// Asynchronously gets the content of this script.
        /// </summary>
        /// <param name="cancellationToken" />
        public Task<string> GetContentAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Renames this script and saves the changes to the cube.
        /// </summary>
        /// <param name="newName">The name of the renamed script.</param>
        public void Rename( string newName );

        /// <summary>
        /// Renames this script and saves the changes to the cube.
        /// </summary>
        /// <param name="newName">The name of the renamed script.</param>
        /// <param name="cancellationToken" />
        public Task RenameAsync( string newName, CancellationToken cancellationToken = default );

        /// <summary>
        /// Saves any changes to the cube.
        /// </summary>
        /// <remarks>If the <see cref="Name" /> of this script has changed, a "Save As" operation is performed.</remarks>
        public void Save();

        /// <summary>
        /// Asynchronously saves any changes to the cube.
        /// </summary>
        /// <param name="cancellationToken" />
        /// <remarks>If the <see cref="Name" /> of this script has changed, a "Save As" operation is performed.</remarks>
        public Task SaveAsync( CancellationToken cancellationToken = default );

        #endregion
    }

    /// <summary>
    /// Fluent extensions for <see cref="EssSharp" />.
    /// </summary>
    public static partial class FluentExtensions
    {
        /// <summary>
        /// Asynchronously deletes this script from the cube.
        /// </summary>
        /// <param name="scriptTask" />
        /// <param name="cancellationToken" />
        public static async Task DeleteAsync<T>(this Task<T> scriptTask, CancellationToken cancellationToken = default) where T : class, IEssScript =>
            await (await scriptTask.ConfigureAwait(false)).DeleteAsync(cancellationToken).ConfigureAwait(false);
    }
}
