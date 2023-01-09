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
        internal EssVariable( EssObject parent, Variable variable ) : base(parent.Configuration, parent.Client)
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
        public virtual VariableScope Scope => VariableScope.Server;

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            await GetApi<ServerVariablesApi>().VariablesDeleteServerVariableAsync(_variable?.Name, 0, cancellationToken);
        }

        /// <inheritdoc />
        public string Value => _variable.Value;

        #endregion
        
        /// <summary>
        /// Returns a textual description of this variable.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"EssVariable {{ Name = {Name}, Value = {Value} }}";
        }
    }
}
