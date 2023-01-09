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
    public class EssAbout : EssObject, IEssAbout
    {
        #region Private Data

        private readonly EssServer server;
        private readonly About           about;

        #endregion

        #region Constructors

        /// <summary />
        public EssAbout( EssServer server, About about ) : base(server?.Configuration, server?.Client)
        {
            this.server = server;
            this.about        = about;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => about?.Name;

        /// <inheritdoc />
        public override EssType Type => EssType.About;

        #endregion

        #region IEssAboutMembers

        /// <inheritdoc />
        public string Description => about?.Description;
        /// <inheritdoc />
        public string Version => about?._Version;
        /// <inheritdoc />
        public string Build => about?.Build;

        #endregion





    }
}
