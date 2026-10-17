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
        /// List of aliases.
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
        /// A solve order, if assigned for this dimension.
        /// </summary>
        public int DimensionSolveOrder { get; }

        /// <summary>
        /// The dimension tag of a dimension member: <see cref="EssDimensionType.REGULAR"/> for a dimension with no tag, which
        /// Essbase leaves out of the response. <see cref="EssDimensionType.Unknown"/> for a member that is not a dimension and
        /// whose response does not give its dimension's tag.
        /// </summary>
        public EssDimensionType DimensionType { get; }

        /// <summary>
        /// Method of storing data.
        /// </summary>
        public string DataStorageType { get; }

        /// <summary>
        /// 
        /// </summary>
        public string FormatString { get; }

        /// <summary>
        /// Method of storing data.
        /// </summary>
        public EssDimStorageType DimensionStorageType { get; }

        /// <summary>
        /// Specifies how different types of accounts should be converted from one currency to another.
        /// </summary>
        public string CurrencyConversionCategory { get; }

        /// <summary>
        /// The generation number of this member.
        /// </summary>
        public int GenerationNumber { get; }

        /// <summary>
        /// Current active alias name
        /// </summary>
        public string ActiveAliasName { get; }

        /// <summary>
        /// True if member name is unique.
        /// </summary>
        public bool HasUniqueName { get; }

        /// <summary>
        /// Unique ID for this member.
        /// </summary>
        public string UniqueId { get; }

        /// <summary>
        /// A solve order, if assigned for this member.
        /// </summary>
        public int MemberSolveOrder { get; }

        /// <summary>
        /// Name of dimension this member belongs to
        /// </summary>
        public string DimensionName { get; }

        /// <summary>
        /// True if member is part of an account dimension
        /// </summary>
        public bool Account { get; }

        /// <summary>
        /// User Defined Attribute.
        /// </summary>
        public List<string> Uda { get; }

        /// <summary>
        /// Name of this members parent member.
        /// </summary>
        public string ParentName { get; }

        /// <summary>
        /// True if member is an attribute dimension
        /// </summary>
        public bool IsAttributeDimension { get; }

        /// <summary>
        /// True if the member is shared
        /// </summary>
        public bool IsSharedMember { get; }

        /// <summary>
        /// The member's consolidation operator: <c>+</c>, <c>-</c>, <c>*</c>, <c>/</c>, <c>%</c>, <c>~</c>, or <c>^</c>.
        /// Essbase leaves out the default, <c>+</c>, which this gives when the member's response has none.
        /// </summary>
        public string Consolidation { get; }

        /// <summary>
        /// The member's formula, or null when it has none. Essbase returns it only when <see cref="EssMemberFields.formula"/>
        /// is requested.
        /// </summary>
        public string Formula { get; }

        /// <summary>
        /// True if the member is calculated in a second pass. Essbase returns it only when
        /// <see cref="EssMemberFields.twoPassCalc"/> is requested, and leaves out false.
        /// </summary>
        public bool TwoPassCalc { get; }

        /// <summary>
        /// The member's time balance: <c>NONE</c>, <c>FIRST</c>, <c>LAST</c>, or <c>AVERAGE</c>. Essbase leaves out the default,
        /// <c>NONE</c>, which this gives when the member's response has none.
        /// </summary>
        public string TimeBalance { get; }

        /// <summary>
        /// Which values the member's time balance skips: <c>NONE</c>, <c>MISSING</c>, <c>ZEROS</c>, or <c>MISSING_AND_ZEROS</c>.
        /// Essbase leaves out the default, <c>NONE</c>, which this gives when the member's response has none.
        /// </summary>
        public string TimeBalanceSkipOption { get; }

        /// <summary>
        /// True if the member is reported as an expense. Essbase leaves out false.
        /// </summary>
        public bool Expense { get; }

        /// <summary>
        /// Gets a list of ancestors.
        /// </summary>
        /// <param name="fields"></param>
        public List<IEssMember> GetAncestors( EssMemberFields? fields = null );

        /// <summary>
        /// Asynchronously gets a list of ancestors.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssMember>> GetAncestorsAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        public List<IEssMember> GetBottomLevelDescendants( EssMemberFields? fields = null );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssMember>> GetBottomLevelDescendantsAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets a list of children.
        /// </summary>
        /// <param name="fields"></param>
        public List<IEssMember> GetChildren( EssMemberFields? fields = null );

        /// <summary>
        /// Asynchronously gets a list of children.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssMember>> GetChildrenAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the dimension this member belongs to.
        /// </summary>
        /// <param name="fields"></param>
        public IEssMember GetDimension( EssMemberFields? fields = null );

        /// <summary>
        /// Asynchronously gets the dimension this member belongs to.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="cancellationToken"></param>
        public Task<IEssMember> GetDimensionAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets the descendants for this member.
        /// </summary>
        /// <param name="fields"></param>
        public List<IEssMember> GetDescendants( EssMemberFields? fields = null );

        /// <summary>
        /// Asynchronously gets the descendants for this member.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssMember>> GetDescendantsAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default );

        /// <summary>
        /// Gets members of the same generation level.
        /// </summary>
        /// <param name="levelNumber"></param>
        /// <param name="fields"></param>
        /// <param name="limit"></param>
        public List<IEssMember> GetSameGenerationMembers( EssMemberFields? fields = null, int limit = 50 );

        /// <summary>
        /// Asynchronously gets members of the same generation level.
        /// </summary>
        /// <param name="levelNumber"></param>
        /// <param name="fields"></param>
        /// <param name="limit"></param>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssMember>> GetSameGenerationMembersAsync( EssMemberFields? fields = null, int limit = 50, CancellationToken cancellationToken = default );

        /// <summary>
        /// Get the siblings of this member.
        /// </summary>
        /// <param name="fields"></param>
        public List<IEssMember> GetSiblings( EssMemberFields? fields = null );

        /// <summary>
        /// Asynchronously gets the siblings of this member.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="cancellationToken"></param>
        public Task<List<IEssMember>> GetSiblingsAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default );
    }

    /// <summary>
    /// Fluent extensions for <see cref="EssSharp" />.
    /// </summary>
    public static partial class FluentExtensions
    {
        /// <summary>
        /// Asynchronously gets the descendants for this member.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="cancellationToken"></param>
        public static async Task<List<IEssMember>> GetDescendantsAsync( this Task<IEssMember> memberTask, EssMemberFields? fields = null, CancellationToken cancellationToken = default ) =>
            await (await memberTask.ConfigureAwait(false)).GetDescendantsAsync(fields, cancellationToken).ConfigureAwait(false);
    }
}
