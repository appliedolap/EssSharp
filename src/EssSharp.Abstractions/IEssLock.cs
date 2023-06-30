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

        public void Unlock();

        public Task UnlockAsync( CancellationToken cancellationToken = default );

    }
}
