using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssAbout : EssObject, IEssAbout
    {
        #region Private Data

        private readonly About _about;

        #endregion

        #region Constructors

        /// <summary />
        public EssAbout( EssServer server, About about ) : base(server?.Configuration, server?.Client)
        {
            _about  = about;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _about?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.About;

        #endregion

        #region IEssAboutMembers

        /// <inheritdoc />
        public string Description => _about?.Description;
        /// <inheritdoc />
        public string Version => _about?._Version;
        /// <inheritdoc />
        public string Build => _about?.Build;
        #endregion





    }
}
