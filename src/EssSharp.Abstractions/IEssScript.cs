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
        /// Deletes a script from the cube
        /// </summary>
        void Delete();

        /// <summary>
        /// Deletes a script from the cube
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task DeleteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Executes a script
        /// </summary>
        IEssJob Execute( );

        /// <summary>
        /// Executes a script
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task<IEssJob> ExecuteAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetContent();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetContentAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Save<T>() where T: class, IEssScript;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<T> SaveAsync<T>( CancellationToken cancellationToken = default ) where T : class, IEssScript;
        #endregion
    }
}
