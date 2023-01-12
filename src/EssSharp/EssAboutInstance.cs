using System;

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
            _aboutInstance = aboutInstance ??
                throw new ArgumentNullException(nameof(aboutInstance), $"An API model {nameof(aboutInstance)} is required to create an {nameof(EssAboutInstance)}.");
        }

        #endregion

        #region IESSAboutInstance Members

        /// <inheritdoc />
        public bool EasInstalled => _aboutInstance?.EasInstalled ?? false;

        /// <inheritdoc />
        public bool ProvisioningSupported  => _aboutInstance?.ProvisioningSupported ?? false;

        /// <inheritdoc />
        public bool ResetPasswordSupported => _aboutInstance?.ResetPasswordSupported ?? false;

        #endregion
    }
}
