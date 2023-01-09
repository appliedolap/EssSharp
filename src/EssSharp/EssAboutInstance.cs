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

        ///// <inheritdoc />
        //public override string Name => aboutinstance?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.About;

        #endregion

        #region IESSAboutInstance Mmebers
        /// <inheritdoc />
        public bool ProvisioningSupported => (bool)_aboutinstance?.ProvisioningSupported;
        /// <inheritdoc />
        public bool ResetPasswordSupported => (bool)_aboutinstance?.ResetPasswordSupported;
        /// <inheritdoc />
        public bool EasInstalled => (bool)_aboutinstance?.EasInstalled;
        #endregion




    }
}
