using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using EssSharp.Model;
using EssSharp.Api;

namespace EssSharp
{
    /// <summary />
    public class EssMember : EssObject, IEssMember
    {
        #region Private Data

        private readonly EssCube  _cube;
        private readonly DimensionMember _member;

        #endregion

        #region Constructors

        /// <summary />
        internal EssMember( DimensionMember member, EssCube cube) : base(cube?.Configuration, cube?.Client)
        {
            _member = member ?? 
                throw new ArgumentNullException(nameof(member), $"An API model {nameof(member)} is required to create an {nameof(EssMember)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssServer)} {nameof(cube)} is required to create an {nameof(EssMember)}.");
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _member.Name;

        /// inheritDoc />
        public override EssType Type => EssType.Member;

        #endregion

        #region IEssMember Attributes

        /// inheritDoc />
        public int NumberOfChildren => _member.NumberOfChildren;

        /// inheritDoc />
        public int LevelNumber => _member.LevelNumber;

        /// inheritDoc />
        public Dictionary<string, string> Aliases => _member.Aliases;

        /// inheritDoc />
        public string UniqueName => _member.UniqueName;

        /// inheritDoc />
        public string MemberId => _member.MemberId;

        /// inheritDoc />
        public int PerviousSublingCount => _member.PreviousSiblingsCount;

        /// inheritDoc />
        public int descentantsCount => _member.DescendantsCount;

        /// inheritDoc />
        public bool dimension => _member.Dimension;

        /// inheritDoc />
        public int DimensionSolveOrder => _member.DimSolveOrder;

        /// inheritDoc />
        public string DimensionType => _member.DimensionType;

        /// inheritDoc />
        public string dataStorageType => _member.DataStorageType;

        /// inheritDoc />
        public string FormatString => _member.FormatString;

        /// inheritDoc />
        public string DimensionStorageType => _member.DimStorageType;

        /// inheritDoc />
        public string CurrencyConversionCategory => _member.CurrencyConversionCategory;

        #endregion

        #region IEssMember Methods

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
                    throw new Exception("Cannot get children");

                return children.ToEssSharpList(_cube) ?? new List<IEssMember>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get list of members from Member ""{Name}"". {e.Message}", e);
            }
        }

        #endregion
    }
}
