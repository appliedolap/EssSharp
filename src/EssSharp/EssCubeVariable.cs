using System;
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
        #region Private Data

        private readonly EssCube _cube;

        #endregion

        #region Constructors

        /// <summary />
        internal EssCubeVariable( Variable variable, EssCube cube ) : base(variable, cube?.Application as EssApplication)
        {
            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssCubeVariable)}.");
        }

        #endregion

        #region IEssCubeVariable members

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public override VariableScope Scope => VariableScope.Cube;

        /// <inheritdoc />
        public override Task DeleteAsync( CancellationToken cancellationToken = default ) =>
            GetApi<VariablesApi>().VariablesDeleteVariableAsync(Application.Name, Cube.Name, Name, 0, cancellationToken);

        #endregion

        /// <inheritdoc />
        public override string ToString() =>
            $"{nameof(EssCubeVariable)} {{ {nameof(Cube)} = {Cube.Name}, {nameof(Application)} = {Application.Name}, {nameof(Name)} = {Name}, {nameof(Value)} = {Value} }}";
    }
}
