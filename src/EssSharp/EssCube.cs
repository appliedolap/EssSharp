using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssCube : EssObject, IEssCube
    {
        #region Private Data

        private readonly EssApplication _application;
        private readonly Cube           _cube;

        #endregion

        #region Constructors

        /// <summary />
        internal EssCube(EssApplication application, Cube cube ) : base(application.Configuration, application.Client)
        {
            _application = application;
            _cube        = cube;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _cube?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Cube;

        #endregion

        #region IEssCube Members

        /// <inheritdoc />
        public IEssApplication Application => _application;

        /// <inheritdoc />
        public async Task<List<IEssCubeVariable>> GetVariablesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<VariablesApi>();
                return (await api.VariablesListVariablesAsync(_application?.Name, _cube?.Name, 0, cancellationToken))?.Items?
                    .Select(variable => new EssCubeVariable(this, variable) as IEssCubeVariable)?.ToList() ?? new List<IEssCubeVariable>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        #endregion
    }
}
