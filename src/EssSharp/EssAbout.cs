using System;

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
        internal EssAbout( About about )
        {
            _about  = about ??
                throw new ArgumentNullException(nameof(about), $"An API model {nameof(about)} is required to create an {nameof(EssAbout)}.");
        }

        #endregion

        #region IEssAboutMembers

        /// <inheritdoc />
        public string Build       => _about?.Build;

        /// <inheritdoc />
        public string Description => _about?.Description;

        /// <inheritdoc />
        public string Version     => _about?._Version;

        #endregion

    }
}
