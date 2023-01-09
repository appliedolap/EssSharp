using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssVariable : EssObject, IEssVariable
    {
        private readonly Variable _variable;

        #region Constructors

        /// <summary />
        public EssVariable( EssServer essServer, Variable variable ) : base(essServer?.Configuration, essServer?.Client)
        {
            _variable  = variable;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name  => _variable?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Variable;

        #endregion

        #region IEssVariable Members

        /// <inheritdoc />
        public VariableScope Scope => VariableScope.Server;

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            switch ( Scope )
            {
                case VariableScope.Server:
                    await GetApi<ServerVariablesApi>().VariablesDeleteServerVariableAsync(_variable?.Name, 0, cancellationToken);
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
