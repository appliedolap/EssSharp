using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents a variable that is specific to a particular application.
    /// </summary>
    public class EssApplicationVariable : EssVariable, IEssApplicationVariable
    {
        #region Private Data

        private readonly EssApplication _application;

        #endregion

        #region Constructors

        internal EssApplicationVariable( Variable variable, EssApplication application = null ) : base(variable, application?.Server as EssServer)
        {
            _application = application;
        }

        #endregion

        #region IEssApplicationVariable Members

        /// <inheritdoc />
        public IEssApplication Application  => _application;

        /// <inheritdoc />
        public override VariableScope Scope => VariableScope.Application;

        /// <inheritdoc />
        public override Task DeleteAsync( CancellationToken cancellationToken = default ) =>
            GetApi<VariablesApi>().VariablesDeleteAppVariableAsync(_application?.Name, Name, 0, cancellationToken);

        #endregion

        /// <inheritdoc />
        public override string ToString() =>
            $"{nameof(EssApplicationVariable)} {{ {nameof(Application)} = {_application?.Name}, {nameof(Name)} = {Name}, {nameof(Value)} = {Value} }}";
    }
}
