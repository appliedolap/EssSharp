using System;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssApplicationConfiguration : IEssApplicationConfiguration
    {
        #region Private Data

        private readonly EssApplication _application;
        private readonly string _key;
        private readonly string _value;

        #endregion

        #region Constructors

        /// <summary />
        internal EssApplicationConfiguration( EssApplication application, string key, string value )
        {
            _application = application ?? 
                throw new ArgumentNullException(nameof(application), $"An API model {nameof(application)} is required to create an {nameof(EssApplicationConfiguration)}.");

            _key = key ??
                throw new ArgumentNullException(nameof(key), $"An {nameof(EssServer)} {nameof(key)} is required to create an {nameof(EssApplicationConfiguration)}.");

            _value = value ??
               throw new ArgumentNullException(nameof(value), $"An {nameof(EssServer)} {nameof(value)} is required to create an {nameof(EssApplicationConfiguration)}.");
        }


        #endregion

        #region IEssApplicatonConfiguration Members

        /// <inheritdoc />
        public IEssApplication Application => _application;

        /// <inheritdoc />
        public string Key => _key;

        /// <inheritdoc />
        public string value => _value;

        #endregion

    }
}
