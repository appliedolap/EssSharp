using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssLockObject : IEssLock
    {
        /// <summary>
        /// Returns the Lock Object Type
        /// </summary>
        public EssLockedFileType LockedFileType { get; }

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
