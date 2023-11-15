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

        /// inheritDoc />
        public override EssType Type => EssType.Member;

        #endregion

        #region IEssMember Attributes

        /// inheritDoc />
        public int NumberOfChildren => _memberBean.NumberOfChildren;

        /// inheritDoc />
        public int LevelNumber => _memberBean.LevelNumber;

        /// inheritDoc />
        public Dictionary<string, string> Aliases => _memberBean.Aliases;

        /// inheritDoc />
        public string UniqueName => _memberBean.UniqueName;

        /// inheritDoc />
        public string MemberId => _memberBean.MemberId;

        /// inheritDoc />
        public int PerviousSiblingCount => _memberBean.PreviousSiblingsCount;

        /// inheritDoc />
        public long DescentantsCount => _memberBean.DescendantsCount;

        /// inheritDoc />
        public bool Dimension => _memberBean.Dimension;

        /// inheritDoc />
        public int DimensionSolveOrder => _memberBean.DimSolveOrder;

        /// inheritDoc />
        public EssDimensionType DimensionType => _memberBean.DimensionType.ToEssEnum();

        /// inheritDoc />
        public string DataStorageType => _memberBean.DataStorageType;

        /// inheritDoc />
        public string FormatString => _memberBean.FormatString;

        /// inheritDoc />
        public EssDimStorageType DimensionStorageType => _memberBean.DimStorageType.ToEssEnum();

        /// inheritDoc />
        public string CurrencyConversionCategory => _memberBean.CurrencyConversionCategory;

        /// inheritDoc />       
        public int GenerationNumber => _memberBean.GenerationNumber == 0 ? 1 : _memberBean.GenerationNumber;

        /// inheritDoc />
        public string activeAliasName => _memberBean.ActiveAliasName;

        /// inheritDoc />
        public bool HasUniqueName => _memberBean.MemberHasUniqueName;

        /// inheritDoc />
        public string UniqueId => _memberBean.UniqueId;

        /// inheritDoc />
        public int MemberSolveOrder => _memberBean.MemberSolveOrder;

        /// inheritDoc />
        public string DimensionName => _memberBean.DimensionName;

        /// inheritDoc />
        public bool Attribute => _memberBean.Attribute;

        /// inheritDoc />
        public bool Account => _memberBean.Account;

        /// inheritDoc />
        public List<string> Uda => _memberBean.Uda;

        /// inheritDoc />
        public string ParentName => _memberBean.ParentName;

        /// inheritDoc />
        public bool IsSharedMember => _memberBean.IsSharedMember;

        #endregion

        #region IEssMember Methods

        /// inheritDoc />
        /// <returns>A List of <see cref="IEssMember"/> objects.</returns>
        public List<IEssMember> GetAncestors() => GetAncestorsAsync().GetAwaiter().GetResult();

        /// inheritDoc />
        /// <returns>A List of <see cref="IEssMember"/> objects.</returns>
        public async Task<List<IEssMember>> GetAncestorsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<OutlineViewerApi>();

                if ( await api.OutlineGetAncestorsMemberInfoAsync(app: _cube.Application.Name, cube: _cube.Name, memberUniqueName: UniqueName, cancellationToken: cancellationToken ).ConfigureAwait(false) is not { } ancestor )
                    throw new Exception("Cannot get ancestors.");

                return ancestor.ToEssSharpList(_cube) ?? new List<IEssMember>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get list of ancestors from Member ""{Name}"". {e.Message}", e);
            }
        }

        /// inheritDoc />
        /// <returns>A List of <see cref="IEssMember"/> objects.</returns>
        public List<IEssMember> GetChildren() => GetChildrenAsync().GetAwaiter().GetResult();

        /// inheritDoc />
        /// <returns>A List of <see cref="IEssMember"/> objects.</returns>
        public async Task<List<IEssMember>> GetChildrenAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<OutlineViewerApi>();

                if ( await api.OutlineGetMembersAsync(app: _cube.Application.Name, cube: _cube.Name, parent: Name).ConfigureAwait(false) is not { } children )
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
        public List<IEssMember> GetDimensions() => GetDimensionsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<List<IEssMember>> GetDimensionsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<OutlineViewerApi>();

                if ( await api.OutlineGetMembersAsync(app: _cube.Application.Name, cube: _cube.Name).ConfigureAwait(false) is not { } children )
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
        public List<IEssMember> GetDescendants() => GetDescendantsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<List<IEssMember>> GetDescendantsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var descendants = new List<IEssMember>();

                var children = await GetChildrenAsync().ConfigureAwait(false);

                foreach ( var member in children )
                {
                    if ( member.NumberOfChildren > 0 )
                    {
                        (await member.GetDescendantsAsync().ConfigureAwait(false)).ForEach(mem => descendants.Add(mem));
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
        public List<IEssMember> GetSiblings() => GetSiblingsAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns></returns>
        public async Task<List<IEssMember>> GetSiblingsAsync( CancellationToken cancellationToken = default )
        {
            try
            {                
                var memberList = await (await _cube.GetMemberAsync(ParentName)).GetChildrenAsync();

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
