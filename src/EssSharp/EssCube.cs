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

        private readonly EssApplication application;
        private readonly Cube           cube;

        #endregion

        #region Constructors

        /// <summary />
        public EssCube( EssApplication application, Cube cube ) : base(application?.Configuration, application?.Client)
        {
            this.application = application;
            this.cube        = cube;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => cube?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Cube;

        #endregion

        #region IEssCube Members

        /// <inheritdoc />
        public async Task<List<IEssVariable>> GetVariablesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<VariablesApi>();
                //var variables = (await api.VariablesListVariablesAsync(application?.Name, cube?.Name, 0, cancellationToken))?.Items?
                    //.Select(variable => new EssCubeVariable(this, variable) as IEssVariable)?.ToList() ?? new List<IEssVariable>();

                return null;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        #endregion
    }
}
