using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EssSharp
{
    /// <summary />
    public interface IEssMember : IEssObject
    {
        /// <summary>
        /// 
        /// </summary>
        public int NumberOfChildren { get; }

        /// <summary>
        /// 
        /// </summary>
        public int LevelNumber { get; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Aliases { get; }

        /// <summary>
        /// 
        /// </summary>
        public string UniqueName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string MemberId { get; }

        /// <summary>
        /// 
        /// </summary>
        public int PerviousSublingCount { get; }

        /// <summary>
        /// 
        /// </summary>
        public long descentantsCount { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool dimension { get; }

        /// <summary>
        /// 
        /// </summary>
        public int DimensionSolveOrder { get; }

        /// <summary>
        /// 
        /// </summary>
        public string DimensionType { get; }

        /// <summary>
        /// 
        /// </summary>
        public string dataStorageType { get; }

        /// <summary>
        /// 
        /// </summary>
        public string FormatString { get; }

        /// <summary>
        /// 
        /// </summary>
        public string DimensionStorageType { get; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrencyConversionCategory { get; }

        public int GenerationNumber { get; }

        public string activeAliasName { get; }

        public bool HasUniqueName { get; }

        public string UniqueId { get; }

        public int MemberSolveOrder { get; }

        public string DimensionName { get; }

        public bool Attribute { get; }

        public bool Account { get; }

        public List<string> Uda { get; }

        public string ParentName { get; }

        /// <summary>
        /// Gets a list of members.
        /// </summary>
        public List<IEssMember> GetChildren();

        /// <summary>
        /// Asynchronously gets a list of members.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssMember>> GetChildrenAsync( CancellationToken cancellationToken = default );
    }
}
