using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssCube : EssObject, IEssCube
    {
        #region Private Data

        private readonly EssApplication application;
        private readonly Cube           cube;

        #endregion

        #region Constructors

        /// <summary />
        public EssCube( EssApplication application, Cube cube ) : base(application?.Configuration, application?.Client)
        {
            this.application = application;
            this.cube        = cube;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => cube?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Cube;

        #endregion
    }
}
