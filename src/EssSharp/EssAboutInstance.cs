using EssSharp.Api;
using EssSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace EssSharp
{
    /// <summary />
    public class EssAboutInstance : EssObject, IEssAboutInstance
    {
        #region Private Data

        private readonly EssServer server;
        private readonly AboutInstance         aboutinstance;

        #endregion

        #region Constructors

        /// <summary />
        public EssAboutInstance( EssServer server, AboutInstance aboutinstance ) : base(server?.Configuration, server?.Client)
        {
            this.server = server;
            this.aboutinstance = aboutinstance;
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
        public bool ProvisioningSupported => (bool)aboutinstance?.ProvisioningSupported;
        /// <inheritdoc />
        public bool ResetPasswordSupported => (bool)aboutinstance?.ResetPasswordSupported;
        /// <inheritdoc />
        public bool EasInstalled => (bool)aboutinstance?.EasInstalled;
        #endregion




    }
}
