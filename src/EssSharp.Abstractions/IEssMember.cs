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
        /// The number of children this member has.
        /// </summary>
        public int NumberOfChildren { get; }

        /// <summary>
        /// The level number of this member.
        /// </summary>
        public int LevelNumber { get; }

        /// <summary>
        /// List of avaiable aliases 
        /// </summary>
        public Dictionary<string, string> Aliases { get; }

        /// <summary>
        /// The unique name for this member in the outline.
        /// </summary>
        public string UniqueName { get; }

        /// <summary>
        /// A unique identifier, seperate from member name.
        /// </summary>
        public string MemberId { get; }

        /// <summary>
        /// Number of siblings that precede this member in the outline.
        /// </summary>
        public int PerviousSiblingCount { get; }

        /// <summary>
        /// Number of descendants this member has.
        /// </summary>
        public long DescentantsCount { get; }

        /// <summary>
        /// true if this member is a top-level dimension member.
        /// </summary>
        public bool Dimension { get; }

        /// <summary>
        /// 
        /// </summary>
        public int DimensionSolveOrder { get; }

        /// <summary>
        /// 
        /// </summary>
        public EssDimensionType DimensionType { get; }

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
        public EssDimStorageType DimensionStorageType { get; }

        /// <summary>
        /// 
        /// </summary>
        public string CurrencyConversionCategory { get; }

        /// <summary>
        /// The generation number of this member.
        /// </summary>
        public int GenerationNumber { get; }

        /// <summary>
        /// Current active alias name
        /// </summary>
        public string activeAliasName { get; }

        /// <summary>
        /// True if member name is unique.
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
        /// Name of dimension this member belongs to
        /// </summary>
        public string DimensionName { get; }

        /// <summary>
        /// True if member is an attribute dimension
        /// </summary>
        public bool Attribute { get; }

        /// <summary>
        /// True if member is part of an account dimension
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
        /// Gets a list of ancestors.
        /// </summary>
        public List<IEssMember> GetAncestors();

        /// <summary>
        /// Asynchronously gets a list of ancestors.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssMember>> GetAncestorsAsync( CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a list of children.
        /// </summary>
        public List<IEssMember> GetChildren();

        /// <summary>
        /// Asynchronously gets a list of children.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssMember>> GetChildrenAsync( CancellationToken cancellationToken = default );

        public List<IEssMember> GetDescendants();

        public Task<List<IEssMember>> GetDescendantsAsync( CancellationToken cancellationToken = default );

        public List<IEssMember> GetSiblings();

        public Task<List<IEssMember>> GetSiblingsAsync( CancellationToken cancellation = default );
    }
}
