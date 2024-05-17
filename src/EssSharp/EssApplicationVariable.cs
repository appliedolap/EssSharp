using System;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents a variable that is specific to a particular application.
    /// </summary>
    public class EssApplicationVariable : EssServerVariable, IEssApplicationVariable
    {
        #region Private Data

        private readonly EssApplication _application;

        #endregion

        #region Constructors

        /// <summery />
        internal EssApplicationVariable( Variable variable, EssApplication application ) : base(variable, application?.Server as EssServer)
        {
            _application = application ??
                throw new ArgumentNullException(nameof(application), $"An {nameof(EssApplication)} {nameof(application)} is required to create an {nameof(EssApplicationVariable)}.");
        }

        #endregion

        #region IEssApplicationVariable Members

        /// <inheritdoc />
        public IEssApplication Application => _application;

        /// <inheritdoc />
        public override VariableScope Scope => VariableScope.Application;

        /// <inheritdoc />
        public override Task DeleteAsync( CancellationToken cancellationToken = default ) =>
            GetApi<VariablesApi>().VariablesDeleteAppVariableAsync(Application.Name, Name, 0, cancellationToken);

        #endregion

        /// <inheritdoc />
        /// <returns>A <see cref="string"/>.</returns>
        public override string ToString() =>
            $"{nameof(EssApplicationVariable)} {{ {nameof(Application)} = {Application.Name}, {nameof(Name)} = {Name}, {nameof(Value)} = {Value} }}";
    }
}
