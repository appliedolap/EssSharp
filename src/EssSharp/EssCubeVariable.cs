using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssCubeVariable : EssObject, IEssCubeVariable
    {
        private readonly Variable  variable;
        private readonly EssApplication application;
        private readonly EssCube           cube;

        #region Constructors

        /// <summary />
        public EssCubeVariable( EssApplication application,EssCube cube, Variable variable ) : base(application?.Configuration, application?.Client)
        {
            this.variable  = variable;
            this.application = application;
            this.cube = cube;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => variable?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Variable;

        #endregion

        #region IEssVariable Members

        /// <inheritdoc />
        public CubeVariableScope Scope => CubeVariableScope.CUBE;

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            await GetApi<VariablesApi>().VariablesDeleteVariableAsync(application?.Name, cube?.Name, variable?.Name, 0, cancellationToken);
        }

        #endregion
    }
}
