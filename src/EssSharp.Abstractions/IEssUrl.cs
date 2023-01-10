using System.Threading.Tasks;
using System.Threading;
using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssUrl
    {
        /// <summary>
        /// Gets the URL as a string
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Gets the Name as a string
        /// </summary>
        public string Name { get; }
    }
}
