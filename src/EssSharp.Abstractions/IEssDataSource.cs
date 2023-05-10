using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssDataSource : IEssObject
    {
        /// <summary>
        /// Returns the server that contains this job.
        /// </summary>
        public IEssServer Server { get; }
    }
}
