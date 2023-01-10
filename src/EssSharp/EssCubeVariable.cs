using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary>
    /// Represents a variable that is specific to a particular cube.
    /// </summary>
    public class EssCubeVariable : EssApplicationVariable, IEssCubeVariable
    {
        private readonly EssCube _cube;
  
        internal EssCubeVariable( EssCube cube, Variable variable ) : base( cube?.Application as EssApplication, variable )
        {
            _cube = cube;
        }

        #region IEssCubeVariable members

        /// <summary>
        /// Gets the cube for this variable.
        /// </summary>
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public override VariableScope Scope => VariableScope.Cube;

        /// <inheritdoc />
        public override Task DeleteAsync( CancellationToken cancellationToken = default ) =>
            GetApi<VariablesApi>().VariablesDeleteVariableAsync(_cube?.Application?.Name, _cube?.Name, Name, 0, cancellationToken);

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return $"EssCubeVariable {{ Cube = {_cube?.Name}, Application = {_cube?.Application?.Name}, Name = {Name}, Value = {Value} }}";
        }
    }
}
