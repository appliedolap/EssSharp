﻿using System;
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
        public string Content { get; }

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
        void Execute();

        /// <summary>
        /// Executes a script
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task ExecuteAsync( CancellationToken cancellationToken = default );
        #endregion
    }
}