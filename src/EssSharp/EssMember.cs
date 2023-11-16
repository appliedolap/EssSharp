using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using EssSharp.Model;
using EssSharp.Api;
using System.Net;

namespace EssSharp
{
    /// <summary />
    public class EssMember : EssObject, IEssMember
    {
        #region Private Data

        private readonly EssCube  _cube;
        private readonly MemberBean _memberBean;

        #endregion

        #region Constructors

        /// <summary />
        internal EssMember( MemberBean memberBean, EssCube cube) : base(cube?.Configuration, cube?.Client)
        {
            _memberBean = memberBean ?? 
                throw new ArgumentNullException(nameof(memberBean), $"An API model {nameof(memberBean)} is required to create an {nameof(EssMember)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssServer)} {nameof(cube)} is required to create an {nameof(EssMember)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _memberBean.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Member;

        #endregion

        #region IEssMember Attributes

        /// <inheritdoc />
        public int NumberOfChildren => _memberBean.NumberOfChildren;

        /// <inheritdoc />
        public int LevelNumber => _memberBean.LevelNumber;

        /// <inheritdoc />
        public Dictionary<string, string> Aliases => _memberBean.Aliases;

        /// <inheritdoc />
        public string UniqueName => _memberBean.UniqueName;

        /// <inheritdoc />
        public string MemberId => _memberBean.MemberId;

        /// <inheritdoc />
        public int PerviousSiblingCount => _memberBean.PreviousSiblingsCount;

        /// <inheritdoc />
        public long DescentantsCount => _memberBean.DescendantsCount;

        /// <inheritdoc />
        public bool Dimension => _memberBean.Dimension;

        /// <inheritdoc />
        public int DimensionSolveOrder => _memberBean.DimSolveOrder;

        /// <inheritdoc />
        public EssDimensionType DimensionType => _memberBean.DimensionType.ToEssEnum();

        /// <inheritdoc />
        public string DataStorageType => _memberBean.DataStorageType;

        /// <inheritdoc />
        public string FormatString => _memberBean.FormatString;

        /// <inheritdoc />
        public EssDimStorageType DimensionStorageType => _memberBean.DimStorageType.ToEssEnum();

        /// <inheritdoc />
        public string CurrencyConversionCategory => _memberBean.CurrencyConversionCategory;

        /// <inheritdoc />       
        public int GenerationNumber => _memberBean.GenerationNumber == 0 ? 1 : _memberBean.GenerationNumber;

        /// <inheritdoc />
        public string activeAliasName => _memberBean.ActiveAliasName;

        /// <inheritdoc />
        public bool HasUniqueName => _memberBean.MemberHasUniqueName;

        /// <inheritdoc />
        public string UniqueId => _memberBean.UniqueId;

        /// <inheritdoc />
        public int MemberSolveOrder => _memberBean.MemberSolveOrder;

        /// <inheritdoc />
        public string DimensionName => _memberBean.DimensionName;

        /// <inheritdoc />
        public bool Attribute => _memberBean.Attribute;

        /// <inheritdoc />
        public bool Account => _memberBean.Account;

        /// <inheritdoc />
        public List<string> Uda => _memberBean.Uda;

        /// <inheritdoc />
        public string ParentName => _memberBean.ParentName;

        /// <inheritdoc />
        public bool IsSharedMember => _memberBean.IsSharedMember;

        #endregion

        #region IEssMember Methods

        /// <inheritdoc />
        /// <returns>A List of <see cref="IEssMember"/> objects.</returns>
        public List<IEssMember> GetAncestors( EssMemberFields? fields = null ) => GetAncestorsAsync(fields).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A List of <see cref="IEssMember"/> objects.</returns>
        public async Task<List<IEssMember>> GetAncestorsAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<OutlineViewerApi>();

                // If fields are given but lacking dataStorageType (needed for IsSharedMember), add it.
                if ( fields?.HasFlag(EssMemberFields.dataStorageType) is false )
                    fields |= EssMemberFields.dataStorageType;

                if ( await api.OutlineGetAncestorsMemberInfoAsync(app: _cube.Application.Name, cube: _cube.Name, memberUniqueName: UniqueName, fields: fields?.ToDelimitedString(), cancellationToken: cancellationToken ).ConfigureAwait(false) is not { } ancestor )
                    throw new Exception("Cannot get ancestors.");

                return ancestor.ToEssSharpList(_cube) ?? new List<IEssMember>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get list of ancestors from Member ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A List of <see cref="IEssMember"/> objects.</returns>
        public List<IEssMember> GetChildren( EssMemberFields? fields = null ) => GetChildrenAsync(fields).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>A List of <see cref="IEssMember"/> objects.</returns>
        public async Task<List<IEssMember>> GetChildrenAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<OutlineViewerApi>();

                // If fields are given but lacking dataStorageType (needed for IsSharedMember), add it.
                if ( fields?.HasFlag(EssMemberFields.dataStorageType) is false )
                    fields |= EssMemberFields.dataStorageType;

                if ( await api.OutlineGetMembersAsync(app: _cube.Application.Name, cube: _cube.Name, parent: Name, fields: fields?.ToDelimitedString()).ConfigureAwait(false) is not { } children )
                    throw new Exception("Cannot get children.");

                return children.ToEssSharpList(_cube) ?? new List<IEssMember>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get list of children from Member ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns></returns>
        public List<IEssMember> GetDimensions( EssMemberFields? fields = null ) => GetDimensionsAsync(fields).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<List<IEssMember>> GetDimensionsAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<OutlineViewerApi>();

                // If fields are given but lacking dataStorageType (needed for IsSharedMember), add it.
                if ( fields?.HasFlag(EssMemberFields.dataStorageType) is false )
                    fields |= EssMemberFields.dataStorageType;

                if ( await api.OutlineGetMembersAsync(app: _cube.Application.Name, cube: _cube.Name, fields: fields?.ToDelimitedString()).ConfigureAwait(false) is not { } children )
                    throw new Exception("Cannot get children.");

                return children.ToEssSharpList(_cube) ?? new List<IEssMember>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"{e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns></returns>
        public List<IEssMember> GetDescendants( EssMemberFields? fields = null ) => GetDescendantsAsync(fields).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<List<IEssMember>> GetDescendantsAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default )
        {
            try
            {
                // If fields are given but lacking dataStorageType (needed for IsSharedMember), add it.
                if ( fields?.HasFlag(EssMemberFields.dataStorageType) is false )
                    fields |= EssMemberFields.dataStorageType;

                var descendants = new List<IEssMember>();

                var children = await GetChildrenAsync(fields: fields).ConfigureAwait(false);

                foreach ( var member in children )
                {
                    if ( member.NumberOfChildren > 0 )
                    {
                        (await member.GetDescendantsAsync(fields: fields).ConfigureAwait(false)).ForEach(mem => descendants.Add(mem));
                    }
                    descendants.Add(member);
                }
                return descendants;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"{e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns></returns>
        public List<IEssMember> GetSiblings( EssMemberFields? fields = null ) => GetSiblingsAsync(fields).GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<List<IEssMember>> GetSiblingsAsync( EssMemberFields? fields = null, CancellationToken cancellationToken = default )
        {
            try
            {
                // If fields are given but lacking dataStorageType (needed for IsSharedMember), add it.
                if ( fields?.HasFlag(EssMemberFields.dataStorageType) is false )
                    fields |= EssMemberFields.dataStorageType;

                var memberList = await (await _cube.GetMemberAsync(ParentName, fields: fields)).GetChildrenAsync(fields: fields);

                return memberList;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"{e.Message}", e);
            }
        }
        #endregion
    }
}
