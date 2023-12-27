using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;

namespace EssSharp
{
    /// <summary />
    public interface IEssDimension : IEssObject
    {
        /// <summary>
        /// Gets the deminsion type.
        /// </summary>
        public string DimensionType { get; }

        /// <summary>
        /// Gets dimension members.
        /// </summary>
        public int MemberCount { get; }

        /// <summary>
        /// Gets stored deminsion members.
        /// </summary>
        public int StoredMemberCount { get; }

        /// <summary>
        /// List of member ID's
        /// </summary>
        public List<string> Members { get; set; }

        public EssDimensionType DimensionTag { get; }

        /// <summary>
        /// Get members names.
        /// </summary>
        /// <returns></returns>
        public List<IEssMember> GetChildren();

        /// <summary>
        /// Asynchronously get members names.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<IEssMember>> GetChildrenAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        public List<IEssGeneration> GetGenerations();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssGeneration>> GetGenerationsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        public List<IEssGeneration> GetLevels();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssGeneration>> GetLevelsAsync( CancellationToken cancellationToken = default );
    }
}
