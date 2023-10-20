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
        public int PerviousSiblingCount { get; }

        /// <summary>
        /// 
        /// </summary>
        public long DescentantsCount { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool Dimension { get; }

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
        public string DataStorageType { get; }

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

        /// <summary>
        /// 
        /// </summary>
        public int GenerationNumber { get; }

        /// <summary>
        /// 
        /// </summary>
        public string activeAliasName { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool HasUniqueName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string UniqueId { get; }

        /// <summary>
        /// 
        /// </summary>
        public int MemberSolveOrder { get; }

        /// <summary>
        /// 
        /// </summary>
        public string DimensionName { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool Attribute { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool Account { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Uda { get; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentName { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSharedMember { get; }

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
