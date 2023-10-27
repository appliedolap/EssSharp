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
        //private readonly DimensionMember _member;
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
        /*
        /// <summary />
        internal EssMember( MemberBean memberBean, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _memberBean = memberBean ??
                throw new ArgumentNullException(nameof(memberBean), $"An API model {nameof(memberBean)} is required to create an {nameof(EssMember)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssServer)} {nameof(cube)} is required to create an {nameof(EssMember)}.");
        }
        */
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
        public string DimensionType => _memberBean.DimensionType;

        /// inheritDoc />
        public string DataStorageType => _memberBean.DataStorageType;

        /// inheritDoc />
        public string FormatString => _memberBean.FormatString;

        /// inheritDoc />
        public string DimensionStorageType => _memberBean.DimStorageType;

        /// inheritDoc />
        public string CurrencyConversionCategory => _memberBean.CurrencyConversionCategory;

        /// inheritDoc />       
        public int GenerationNumber => _memberBean.GenerationNumber;

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

                if ( await api.OutlineGetMembersAsync(app: _cube.Application.Name, cube: _cube.Name, parentUniqueName: UniqueName).ConfigureAwait(false) is not { } children )
                    throw new Exception("Cannot get children.");

                return children.ToEssSharpList(_cube) ?? new List<IEssMember>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get list of children from Member ""{Name}"". {e.Message}", e);
            }
        }

        #endregion
    }
}
