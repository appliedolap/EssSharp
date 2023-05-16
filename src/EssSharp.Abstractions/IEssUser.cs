using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssUser : IEssObject
    {
        /// <summary>
        /// Return the server associated with the user
        /// </summary>
        public IEssServer Server { get; }

        /// <summary>
        /// Return the display name
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Return the user email
        /// </summary>
        public string Email { get; }
    }
}
