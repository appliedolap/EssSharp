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

        ////// <summary >
        ///
        ////// <summary />
        public override string Name => string.Empty;

        /// <inheritdoc />
        public override EssType Type => EssType.AboutInstance;

        #endregion

        #region IESSAboutInstance Mmebers
        /// <inheritdoc />

        public bool ProvisioningSupported => aboutinstance?.ProvisioningSupported ?? false;
        /// <inheritdoc />
        public bool ResetPasswordSupported => aboutinstance?.ResetPasswordSupported ?? false;
        /// <inheritdoc />
        public bool EasInstalled => aboutinstance?.EasInstalled ?? false;
        #endregion




    }
}
