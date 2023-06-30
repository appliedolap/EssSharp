using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssLockBlock : IEssLock
    {

        /// <summary>
        /// 
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Returns the time 
        /// </summary>
        public int Duration { get; }

        /// <summary>
        /// Returns the user 
        /// </summary>
        public string User { get;  }

    }
}
