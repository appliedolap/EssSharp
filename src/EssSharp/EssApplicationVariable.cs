using EssSharp.Model;

namespace EssSharp;

/// <summary>
/// Models an application-level variable.
/// </summary>
public class EssApplicationVariable : EssVariable, IEssApplicationVariable
{
    private readonly EssApplication _application;
    
    internal EssApplicationVariable(EssApplication application, Variable variable) : base(application, variable)
    {
        _application = application;
    }

    /// <inheritdoc />
    public override VariableScope Scope => VariableScope.Application;

    /// <summary>
    /// Gets the application for this variable.
    /// </summary>
    public IEssApplication Application => _application;

    /// <summary>
    /// Returns a textual description of this variable.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"EssApplicationVariable {{ Application = {_application.Name}, Name = {Name}, Value = {Value} }}";
    }
}
