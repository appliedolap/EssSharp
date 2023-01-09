using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssAboutInstance : EssObject, IEssAboutInstance
    {
        #region Private Data

        private readonly AboutInstance _aboutinstance;

        #endregion

        #region Constructors

        /// <summary />
        public EssAboutInstance( EssServer server, AboutInstance aboutinstance ) : base(server?.Configuration, server?.Client)
        {
            _aboutinstance = aboutinstance;
        }

        #endregion

        #region IEssObject Members

        ////// <summary >
        ///
        ////// <summary />
        public override string Name => string.Empty;

        /// <inheritdoc />
        public override EssType Type => EssType.AboutInstance;

        #endregion

        #region IESSAboutInstance Members

        /// <inheritdoc />
        public bool ProvisioningSupported => _aboutinstance?.ProvisioningSupported ?? false;
        /// <inheritdoc />
        public bool ResetPasswordSupported => _aboutinstance?.ResetPasswordSupported ?? false;
        /// <inheritdoc />
        public bool EasInstalled => _aboutinstance?.EasInstalled ?? false;


        #endregion




    }
}
