using System;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents a runtime substitution variable that a calc script declares.
    /// </summary>
    public class EssRuntimeSubstitutionVariable : IEssRuntimeSubstitutionVariable
    {
        #region Private Data

        private readonly EssCalcScript _script;
        private readonly RTSV          _rtsv;

        #endregion

        #region Constructors

        /// <summary />
        internal EssRuntimeSubstitutionVariable( RTSV rtsv, EssCalcScript script )
        {
            _rtsv = rtsv ??
                throw new ArgumentNullException(nameof(rtsv), $"An API model {nameof(rtsv)} is required to create an {nameof(EssRuntimeSubstitutionVariable)}.");

            _script = script ??
                throw new ArgumentNullException(nameof(script), $"An {nameof(EssCalcScript)} {nameof(script)} is required to create an {nameof(EssRuntimeSubstitutionVariable)}.");
        }

        #endregion

        #region IEssRuntimeSubstitutionVariable Members

        /// <inheritdoc />
        public string Name => _rtsv.Name;

        /// <inheritdoc />
        public string Description => _rtsv.Description ?? string.Empty;

        /// <inheritdoc />
        public EssRuntimeSubstitutionVariableType Type => _rtsv.Type switch
        {
            RTSV.TypeEnum.STRING => EssRuntimeSubstitutionVariableType.String,
            RTSV.TypeEnum.NUMBER => EssRuntimeSubstitutionVariableType.Number,
            RTSV.TypeEnum.DATE   => EssRuntimeSubstitutionVariableType.Date,
            RTSV.TypeEnum.MEMBER => EssRuntimeSubstitutionVariableType.Member,
            _                    => EssRuntimeSubstitutionVariableType.Unknown
        };

        /// <inheritdoc />
        public string Dimension => Type is EssRuntimeSubstitutionVariableType.Member ? _rtsv.Dimension : null;

        /// <inheritdoc />
        public bool SingleChoice => _rtsv.SingleChoice;

        /// <inheritdoc />
        public bool AllowMissing => _rtsv.AllowMissing;

        /// <inheritdoc />
        public string DefaultValue => _rtsv.Value?.ToString();

        /// <inheritdoc />
        public IEssCalcScript Script => _script;

        #endregion

        /// <inheritdoc />
        public override string ToString() =>
            $"{nameof(EssRuntimeSubstitutionVariable)} {{ {nameof(Script)} = {Script.Name}, {nameof(Name)} = {Name}, {nameof(Type)} = {Type}, {nameof(DefaultValue)} = {DefaultValue} }}";
    }
}
