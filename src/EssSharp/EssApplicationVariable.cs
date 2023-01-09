using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssApplicationVariable : EssObject, IEssApplicationVariable
    {
        private readonly EssServer essServer;
        private readonly Variable  variable;
        private readonly EssApplication application;

        #region Constructors

        /// <summary />
        public EssApplicationVariable( EssServer essServer,EssApplication application, Variable variable ) : base(essServer?.Configuration, essServer?.Client)
        {
            this.essServer = essServer;
            this.application = application;
            this.variable = variable;
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
        public ApplicationVariableScope Scope => ApplicationVariableScope.APPLICATION;

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            await GetApi<VariablesApi>().VariablesDeleteAppVariableAsync(application?.Name, variable?.Name, 0, cancellationToken);
        }

        #endregion
    }
}
