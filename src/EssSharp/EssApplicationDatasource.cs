using System;

using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssApplicationDatasource : EssDatasource, IEssApplicationDatasource
    {
        #region Private Data

        private readonly EssApplication _application;

        #endregion

        #region Constructors

        /// <summary />
        internal EssApplicationDatasource( Datasource datasource, EssApplication application ) : base(datasource, application?.Server as EssServer)
        {
            _application = application ??
                throw new ArgumentNullException(nameof(application), $"An {nameof(EssServer)} {nameof(application)} is required to create an {nameof(EssApplicationDatasource)}.");
        }

        #endregion

        #region IEssApplicationDatasource Members 

        /// <inheritdoc />
        /// <returns>An <see cref="IEssApplication"/> object.</returns>
        public IEssApplication Application => _application;

        #endregion 

        #region System.Object Overrides

        /// <inheritdoc />
        /// <remarks>Provides the name of this datasource in the following format:<br />
        /// <b>&lt;application&gt;.&lt;datasource&gt;</b>.</remarks>
        /// <returns>A <see cref="string"/>.</returns>
        public override string ToString() => $@"{_application?.Name}.{Name}".Trim('.');

        #endregion
    }
}
