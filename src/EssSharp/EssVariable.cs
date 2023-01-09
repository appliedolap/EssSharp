using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssVariable : EssObject, IEssVariable
    {
        private readonly EssServer essServer;
        private readonly Variable  variable;

        #region Constructors

        /// <summary />
        public EssVariable( EssServer essServer, Variable variable ) : base(essServer?.Configuration, essServer?.Client)
        {
            this.essServer = essServer;
            this.variable  = variable;
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
        public VariableScope Scope => VariableScope.SERVER;

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            switch ( Scope )
            {
                case VariableScope.SERVER:
                    await GetApi<ServerVariablesApi>().VariablesDeleteServerVariableAsync(variable?.Name, 0, cancellationToken);
                    break;
                //case VariableScope.APPLICATION:
                    //await GetApi<VariablesApi>().VariablesDeleteAppVariableAsync(Parent?.Name, variable?.Name, 0, cancellationToken);
                    //break;
                //case VariableScope.CUBE:
                    //await GetApi<VariablesApi>().VariablesDeleteVariableAsync(Parent?.Parent?.Name, Parent?.Name, variable?.Name, 0, cancellationToken);
                    //break;
            }
        }

        #endregion
    }
}
