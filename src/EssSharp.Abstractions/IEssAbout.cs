using System.Threading.Tasks;
using System.Threading;
using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssAbout
    {
        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// Gets or Sets _Version
        /// </summary>
        public string Version { get; }
        /// <summary>
        ///  Gets or Sets Build
        /// </summary>
        public string Build { get; }

    }
    
}
