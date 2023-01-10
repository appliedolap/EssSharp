using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents a variable that is specific to a particular server.
    /// </summary>
    public class EssVariable : EssObject, IEssVariable
    {
        private readonly Variable _variable;

        #region Constructors

        /// <summary />
        internal EssVariable( EssServer server, Variable variable ) : base(server?.Configuration, server?.Client)
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
        public virtual string Value => _variable.Value;

        /// <inheritdoc />
        public virtual void Delete() => DeleteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public virtual Task DeleteAsync( CancellationToken cancellationToken = default ) =>
            GetApi<ServerVariablesApi>().VariablesDeleteServerVariableAsync(_variable?.Name, 0, cancellationToken);

        #endregion

        /// <summary>
        /// Returns a textual description of this variable.
        /// </summary>
        public override string ToString()
        {
            return $"EssVariable {{ Name = {Name}, Value = {Value} }}";
        }
    }
}
