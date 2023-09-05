using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Api;
using EssSharp.Model;


namespace EssSharp
{
    /// <summary />
    public class EssGrid : EssObject, IEssGrid
    {
        #region Private Data

        private readonly EssCube _cube;
        private Grid _grid;
         
        #endregion

        #region Constructors

        /// <summary />
        internal EssGrid( Grid grid, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _grid = grid ??
                throw new ArgumentNullException(nameof(grid), $"An API model {nameof(grid)} is required to create an {nameof(EssGrid)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssCube)}.");
        }

        #endregion

        #region EssObject Properties

        /// <inheritdoc />
        public override string Name => _grid.Alias;

        /// <inheritdoc />
        public override EssType Type => EssType.Grid;

        #endregion

        #region IEssGrid Members

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public string Alias => _grid.Alias;

        /// <inheritdoc />
        public List<EssGridDimension> Dimensions => _grid.Dimensions.ToEssGridDimension();

        /// <inheritdoc />
        public EssGridSlice Slice => _grid.Slice.ToEssGridSlice();

        #endregion

        #region IEssGrid Members

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public IEssGrid Refresh() => RefreshAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="IEssGrid"/> object.</returns>
        public async Task<IEssGrid> RefreshAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<GridApi>();

                var ranges = new List<List<int>>();

                Slice.Data.Ranges.ForEach(ran => ranges.Add(new List<int>() { ran.Start, ran.End }));

                var body = new GridOperation()
                {
                    Grid = this.ToModelBean(),
                    Action = GridOperation.ActionEnum.Refresh,
                    Alias = this.Alias,
                    Ranges = ranges
                };
                
                if ( await api.GridExecuteAsync(applicationName: _cube.Application.Name, databaseName: _cube.Name, body: body, cancellationToken: cancellationToken).ConfigureAwait(false) is not { } refreshedGrid )
                    throw new Exception($@"Cannot refresh grid ""{Name}"".");

                _grid = refreshedGrid;
                
                return this;
            }
            catch ( OperationCanceledException ) { throw; }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to refresh grid ""{Name}"". {e.Message}", e);
            }
        }

        #endregion
    }
}
