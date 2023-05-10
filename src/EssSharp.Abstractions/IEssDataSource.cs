using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssDatasource : IEssObject
    {
        /// <summary>
        /// Returns the server that contains this job.
        /// </summary>
        public IEssServer Server { get; }
    }
}
