using System;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssLock : IEssObject
    {
        #region IEssLock Member Properties

        /// <summary>
        /// 
        /// </summary>
        public EssLockType LockType { get; }

        #endregion

        /// <summary>
        /// Unlocks a locked object or block.
        /// </summary>
        public void Unlock();

        /// <summary>
        /// Asynchronously unlocks a locked object or block.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task UnlockAsync( CancellationToken cancellationToken = default );

    }
}
