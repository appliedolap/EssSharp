namespace EssSharp
{
    /// <summary />
    public interface IEssLockBlock : IEssLock
    {

        /// <summary>
        /// Returns the number of locked blocks.
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
