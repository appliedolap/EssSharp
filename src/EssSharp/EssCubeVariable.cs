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

        internal EssCubeVariable( Variable variable, EssCube cube = null ) : base(variable, cube?.Application as EssApplication)
        {
            _cube = cube;
        }

        #region IEssCubeVariable members

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public override VariableScope Scope => VariableScope.Cube;

        /// <inheritdoc />
        public override Task DeleteAsync( CancellationToken cancellationToken = default ) =>
            GetApi<VariablesApi>().VariablesDeleteVariableAsync(_cube?.Application?.Name, _cube?.Name, Name, 0, cancellationToken);

        #endregion

        /// <inheritdoc />
        public override string ToString() =>
            $"{nameof(EssCubeVariable)} {{ {nameof(Cube)} = {_cube?.Name}, {nameof(Application)} = {_cube?.Application?.Name}, {nameof(Name)} = {Name}, {nameof(Value)} = {Value} }}";
    }
}
