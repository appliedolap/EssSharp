using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

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

        public string Role { get; }

        public List<string> GroupNames { get; }
    }
}
