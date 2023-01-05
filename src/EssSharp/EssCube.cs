using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssCube : EssObject, IEssCube
    {
        private readonly EssApplication application;
        private readonly Cube           cube;

        /// <summary />
        public EssCube( EssApplication application, Cube cube ) : base(application.Configuration, application.Client)
        {
            this.application = application;
            this.cube        = cube;
        }

        /// <inheritdoc />
        public override string Name => cube?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.Cube;
    }
}
