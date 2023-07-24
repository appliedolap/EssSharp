using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Threading;

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
        public string FullName { get; }

        /// <summary>
        /// Return the user email
        /// </summary>
        public string Email { get; }

        public EssServerRole Role { get; }

        public List<string> GroupNames { get; }

        /// <summary>
        /// Deletes a user from the server.
        /// </summary>
        public void Delete();

        /// <summary>
        /// Asynchronously deletes a user from the server.
        /// </summary>
        public Task DeleteAsync( CancellationToken cancellationToken = default );
    }
}
