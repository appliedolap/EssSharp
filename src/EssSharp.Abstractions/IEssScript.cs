using System;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssScript : IEssObject
    {
        #region Properties 

        /// <summary>
        /// Returns the content of the script
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Returns the cube that holds the script
        /// </summary>
        IEssCube Cube { get; }

        /// <summary>
        /// Returns not sure
        /// </summary>
        public long ModifiedTime { get; }

        /// <summary>
        /// Returns the size of the script in bytes
        /// </summary>
        public long Size { get; }

        /// <summary >
        /// Returns the type of the script.
        /// </summary>
        public EssScriptType ScriptType { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Synchronously deletes a script from the cube.
        /// </summary>
        void Delete();

        /// <summary>
        /// Asynchronously deletes a script from the cube.
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Synchronously executes a script.
        /// </summary>
        IEssJob Execute( );

        /// <summary>
        /// Asynchronously executes a script.
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task<IEssJob> ExecuteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Syncronously returns and sets the script content.
        /// </summary>
        /// <returns></returns>
        public string GetContent();

        /// <summary>
        /// Asyncronously retrieves and sets the script content.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetContentAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Sycronously Saves script to the cube.
        /// </summary>
        /// <returns></returns>
        public T Save<T>() where T: class, IEssScript;

        /// <summary>
        /// Asycronously Saves script to the cube.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> SaveAsync<T>( CancellationToken cancellationToken = default ) where T : class, IEssScript;
        #endregion
    }
}
