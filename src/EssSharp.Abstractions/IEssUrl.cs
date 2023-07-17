using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssUrl : IEssObject
    {
        /// <summary>
        /// Returns the URL path of the resource as a string.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Returns the absolute URL of the resource.
        /// </summary>
        public Uri Url { get; }
    }
}
