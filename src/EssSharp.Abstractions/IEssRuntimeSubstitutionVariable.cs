namespace EssSharp
{
    /// <summary>
    /// The kinds of value a runtime substitution variable takes.
    /// </summary>
    public enum EssRuntimeSubstitutionVariableType
    {
        /// <summary />
        Unknown = 0,
        /// <summary>A string.</summary>
        String = 1,
        /// <summary>A number.</summary>
        Number = 2,
        /// <summary>A date.</summary>
        Date = 3,
        /// <summary>One or more members of <see cref="IEssRuntimeSubstitutionVariable.Dimension" />.</summary>
        Member = 4
    };

    /// <summary>
    /// A runtime substitution variable a calc script declares in its <c>SET RUNTIMESUBVARS</c> block, whose value is given
    /// when the script runs (<see cref="EssJobCalcScriptOptions.RuntimeSubstitutionVariables" />), or else is its
    /// <see cref="DefaultValue" />.
    /// </summary>
    /// <remarks>
    /// The description, type, dimension, choice and missing-value setting come from the variable's <c>&lt;RTSV_HINT&gt;</c>
    /// prompt hint. A variable declared without one has no description and is a <see cref="EssRuntimeSubstitutionVariableType.String" />.
    /// </remarks>
    public interface IEssRuntimeSubstitutionVariable
    {
        /// <summary>
        /// Returns the name of the variable.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Returns the description the variable's prompt hint gives, or an empty string.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Returns the kind of value the variable takes.
        /// </summary>
        public EssRuntimeSubstitutionVariableType Type { get; }

        /// <summary>
        /// Returns the dimension whose members a <see cref="EssRuntimeSubstitutionVariableType.Member" /> variable takes, or
        /// <see langword="null" /> for a variable of another type.
        /// </summary>
        public string Dimension { get; }

        /// <summary>
        /// Returns whether a <see cref="EssRuntimeSubstitutionVariableType.Member" /> variable takes one member
        /// (<see langword="true" />) or one or more.
        /// </summary>
        public bool SingleChoice { get; }

        /// <summary>
        /// Returns whether the variable may be given <c>#Missing</c>.
        /// </summary>
        public bool AllowMissing { get; }

        /// <summary>
        /// Returns the value the script declares for the variable, exactly as the script writes it, for example
        /// <c>"100-10"</c>, quotes included.
        /// </summary>
        public string DefaultValue { get; }

        /// <summary>
        /// Returns the calc script that declares the variable.
        /// </summary>
        public IEssCalcScript Script { get; }
    }
}
