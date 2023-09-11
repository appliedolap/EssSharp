using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssDimension : EssObject, IEssDimension
    {
        #region Private Data

        private readonly EssCube       _cube;
        private readonly DimensionBean _dimension;

        #endregion

        #region Constructors

        /// <summary />
        internal EssDimension( DimensionBean dimension, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _dimension = dimension ??
                throw new ArgumentNullException(nameof(dimension), $"An API model {nameof(dimension)} is required to create an {nameof(EssDimension)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssCube)}.");
        }

        #endregion

        #region IEssDimensionMembers

        /// <inheritdoc />
        public override string Name => _dimension?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Dimension;

        /// <inheritdoc />
        public string DimensionType => _dimension?.Type;

        /// <inheritdoc />
        public int MemberCount => _dimension.Members;

        /// <inheritdoc />
        public int StoredMemberCount => _dimension.StoredMembers;

        public List<string> Members { get; set; }

        #endregion

        /// <inheritdoc />
        /// <returns>A List of <see cref="IEssMember"/> of Member Names.</returns>
        public List<IEssMember> GetChildren() => GetChildrenAsync().GetAwaiter().GetResult();

        /// TODO: finish implementation 
        /// <inheritdoc />
        /// <returns>A List of <see cref="IEssMember"/> of Member Names.</returns>
        public async Task<List<IEssMember>> GetChildrenAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<OutlineViewerApi>();

                if ( await api.OutlineGetMembersAsync(app: _cube.Application.Name, cube: _cube.Name, parent: Name).ConfigureAwait(false) is not { } members )
                    throw new Exception("Cannot get members"); // TODO: update later

                return members.ToEssSharpList(_cube) ?? new List<IEssMember>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get list of members from dimension ""{Name}"". {e.Message}", e);
            }
        }
    }
}
