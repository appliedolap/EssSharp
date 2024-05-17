using System;
using System.Collections.Generic;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssApplicationConfiguration : EssObject, IEssApplicationConfiguration
    {
        #region Private Data

        private readonly EssApplication _application;
        private readonly ApplicationConfigEntry _appConfigEntity;

        #endregion

        #region Constructors

        /// <summary />
        internal EssApplicationConfiguration(EssApplication application, ApplicationConfigEntry appConfigEntity) : base(application?.Configuration, application?.Client)
        {
            _application = application ??
                throw new ArgumentNullException(nameof(application), $"An API model {nameof(application)} is required to create an {nameof(EssApplicationConfiguration)}.");

            _appConfigEntity = appConfigEntity ??
                throw new ArgumentNullException(nameof(appConfigEntity), $"An {nameof(EssServer)} {nameof(appConfigEntity)} is required to create an {nameof(EssApplicationConfiguration)}.");
        }


        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        /// <returns>A <see cref="String"/>.</returns>
        public override string Name => _appConfigEntity.Key;

        /// <inheritdoc />
        /// <returns>A <see cref="EssType"/> object.</returns>
        public override EssType Type => EssType.ApplicationConfiguration;

        #endregion

        #region IEssApplicatonConfiguration Members

        /// <inheritdoc />
        /// <returns>A <see cref="IEssApplication"/> object.</returns>
        public IEssApplication Application => _application;

        /// <inheritdoc />
        /// <returns>A <see cref="string"/>.</returns>
        public string Key => _appConfigEntity.Key;

        /// <inheritdoc />
        /// /// <returns>A <see cref="string"/>.</returns>
        public string Value => _appConfigEntity.Value;

        #endregion

    }
}
