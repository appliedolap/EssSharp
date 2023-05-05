using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssLock : IEssObject
    {
        /// <summary>
        /// Returns the Lock Object Type
        /// </summary>
        public EssLockType LockType { get; }

        /// <summary>
        /// Returns the user 
        /// </summary>
        public string User { get;  }

        /// <summary>
        /// Returns the time 
        /// </summary>
        public DateTime Time { get; }

    }
}
