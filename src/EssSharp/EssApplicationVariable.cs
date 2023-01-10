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
        private readonly EssApplication _application;

        internal EssApplicationVariable( EssApplication application, Variable variable ) : base(application?.Server as EssServer, variable)
        {
            _application = application;
        }

        #region IEssApplicationVariable Members

        /// <inheritdoc />
        public IEssApplication Application => _application;

        /// <inheritdoc />
        public override VariableScope Scope => VariableScope.Application;

        /// <inheritdoc />
        public override Task DeleteAsync( CancellationToken cancellationToken = default ) =>
            GetApi<VariablesApi>().VariablesDeleteAppVariableAsync(_application?.Name, Name, 0, cancellationToken);

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"EssApplicationVariable {{ Application = {_application?.Name}, Name = {Name}, Value = {Value} }}";
        }
    }
}
