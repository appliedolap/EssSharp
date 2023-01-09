using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssAboutInstance : IEssAboutInstance
    {
        #region Private Data

        private readonly AboutInstance _aboutInstance;

        #endregion

        #region Constructors

        /// <summary />
        public EssAboutInstance( AboutInstance aboutInstance )
        {
            _aboutInstance = aboutInstance;
        }

        #endregion

        #region IESSAboutInstance Members

        /// <inheritdoc />
        public bool ProvisioningSupported => _aboutInstance?.ProvisioningSupported ?? false;
        /// <inheritdoc />
        public bool ResetPasswordSupported => _aboutInstance?.ResetPasswordSupported ?? false;
        /// <inheritdoc />
        public bool EasInstalled => _aboutInstance?.EasInstalled ?? false;

        #endregion

    }
}
