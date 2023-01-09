using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents a variable that is specific to a particular cube.
    /// </summary>
    public class EssCubeVariable : EssApplicationVariable, IEssCubeVariable
    {

        private readonly EssCube _cube;

        internal EssCubeVariable(EssApplication application, EssCube cube, Variable variable) : base(application,
            variable)
        {
            _cube = cube;
        }

        /// <inheritdoc />
        public override VariableScope Scope => VariableScope.Cube;

        /// <summary>
        /// Gets the cube for this variable.
        /// </summary>
        public IEssCube Cube => _cube;

        /// <summary>
        /// Returns a textual description of this variable.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return
                $"EssCubeVariable {{ Cube = {_cube.Name}, Application = {_cube.Application.Name}, Name = {Name}, Value = {Value} }}";
        }
    }
}
