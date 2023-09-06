using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public interface IEssDimension
    {
        /// <summary>
        /// Gets the Name as a string
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the deminsion type.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets dimension members.
        /// </summary>
        public int MemberCount { get; }

        /// <summary>
        /// Gets stored deminsion members.
        /// </summary>
        public int StoredMemberCount { get; }

        /// <summary>
        /// Get members names.
        /// </summary>
        /// <returns></returns>
        public List<string> GetMembers();

        /// <summary>
        /// Asynchronously get members names.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<string>> GetMembersAsync(CancellationToken cancellationToken = default);
    }
}
