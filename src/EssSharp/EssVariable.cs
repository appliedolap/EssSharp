using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssVariable : EssObject, IEssVariable
    {
        private readonly Variable variable;

        #region Constructors

        /// <summary />
        public EssVariable( EssObject essObject, Variable variable ) : base(essObject?.Configuration, essObject?.Client)
        {
            Scope = essObject switch
            {
                EssServer      _ => VariableScope.SERVER,
                EssApplication _ => VariableScope.APPLICATION,
                EssCube        _ => VariableScope.CUBE,
                               _ => throw new ArgumentException("An invalid parent type was given.", nameof(essObject))
            };

            this.Parent   = essObject;
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
        public VariableScope Scope { get; private set; } = VariableScope.SERVER;

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken )
        {
            switch ( Scope )
            {
                case VariableScope.SERVER:
                    await GetApi<ServerVariablesApi>().VariablesDeleteServerVariableAsync(variable?.Name, 0, cancellationToken);
                    break;
                case VariableScope.APPLICATION:
                    await GetApi<VariablesApi>().VariablesDeleteAppVariableAsync(Parent?.Name, variable?.Name, 0, cancellationToken);
                    break;
                case VariableScope.CUBE:
                    await GetApi<VariablesApi>().VariablesDeleteVariableAsync(Parent?.Parent?.Name, Parent?.Name, variable?.Name, 0, cancellationToken);
                    break;
            }
        }

        #endregion
    }
}
