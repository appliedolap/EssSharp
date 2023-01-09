using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssAbout : IEssAbout
    {
        #region Private Data

        private readonly About _about;

        #endregion

        #region Constructors

        /// <summary />
        public EssAbout(About about )
        {
            _about  = about;
        }

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
