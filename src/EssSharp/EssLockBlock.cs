using System;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssLockBlock : EssLock, IEssLockBlock
    {
        #region Private Data

        private readonly EssCube _cube;
        private readonly LockBlock _lockBlock;

        #endregion

        #region Constructors

        /// <summary />
        internal EssLockBlock( LockBlock lockBlock, EssCube cube ) : base(lockBlock, cube )
        {
            _lockBlock = lockBlock ?? 
                throw new ArgumentNullException(nameof(lockBlock), $"An API model {nameof(lockBlock)} is required to create an {nameof(EssLock)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssServer)} {nameof(cube)} is required to create an {nameof(EssLock)}.");
        }

        #endregion

        #region IEssObject Members Properties

        /// <inheritdoc />
        public override EssType Type => EssType.Lock;

        #endregion

        #region IEssLock Members Properties

        /// <inheritdoc />
        public override EssLockType LockType => EssLockType.Block;

        /// <inheritdoc />
        public string User => _lockBlock?.User;

        /// <inheritdoc />
        public int Duration => _lockBlock.Duration;

        /// <inheritdoc />
        public int Count => _lockBlock.Count;

        #endregion

        #region IEssLock Methods 

        #endregion

    }
}
