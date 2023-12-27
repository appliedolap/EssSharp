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

        /// <inheritdoc />
        public List<string> Members { get; set; }

        /// <inheritdoc />
        public EssDimensionType DimensionTag => _cube.GetMember(Name).DimensionType;
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

        /// inheritdoc />
        /// <returns></returns>
        public List<IEssGeneration> GetGenerations() => GetGenerationsAsync().GetAwaiter().GetResult();

        /// inheritdoc />
        /// <returns></returns>
        public async Task<List<IEssGeneration>> GetGenerationsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<DimensionsApi>();

                if ( await api.DimensionsListDimGenerationsAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, dimensionName: Name).ConfigureAwait(false) is not { } generations )
                    throw new Exception("Cannot get generations"); // TODO: update later

                return generations.ToEssSharpList() ?? new List<IEssGeneration>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get list of generations from dimension ""{Name}"". {e.Message}", e);
            }
        }

        /// inheritdoc />
        /// <returns></returns>
        public List<IEssGeneration> GetGenerationLevels() => GetGenerationLevelsAsync().GetAwaiter().GetResult();

        /// inheritdoc />
        /// <returns></returns>
        public async Task<List<IEssGeneration>> GetGenerationLevelsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<DimensionsApi>();

                if ( await api.DimensionsListDimGenerationsAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, dimensionName: Name).ConfigureAwait(false) is not { } generations )
                    throw new Exception("Cannot get generations"); // TODO: update later

                return generations.ToEssSharpList() ?? new List<IEssGeneration>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get list of generations from dimension ""{Name}"". {e.Message}", e);
            }
        }

        /// <inheritDoc />
        /// <returns></returns>
        public List<IEssGeneration> GetLevels() => GetLevelsAsync().GetAwaiter().GetResult();

        /// <inheritDoc />
        /// <returns></returns>
        public async Task<List<IEssGeneration>> GetLevelsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<DimensionsApi>();

                if ( await api.DimensionsListDimLevelsAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, dimensionName: Name).ConfigureAwait(false) is not { } generations )
                    throw new Exception("Cannot get generations"); // TODO: update later

                return generations.ToEssSharpList() ?? new List<IEssGeneration>();
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get list of generations from dimension ""{Name}"". {e.Message}", e);
            }
        }
    }
}
