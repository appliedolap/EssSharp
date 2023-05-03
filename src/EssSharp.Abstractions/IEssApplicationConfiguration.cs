using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssApplicationConfiguration
    {
        /// <summary>
        /// Returns the application that this configuration is associated with
        /// </summary>
        IEssApplication Application { get; }

        /// <summary>
        /// Returns the key for this configuration
        /// </summary>
        public string Key { get; }  

        /// <summary>
        /// returns the value for this configuration!
        /// </summary>
        public string value { get; }
    }
}
