using System;
using System.Collections.Generic;

namespace EssSharp
{
    public class EssDatasourceQueryInfo : IEssDatasourceQueryInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EssDatasourceQueryInfo"/> class.
        /// </summary>
        public EssDatasourceQueryInfo() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EssDatasourceQueryInfo"/> class with a fully articulated <paramref name="query"/>.
        /// </summary>
        /// <param name="query">The datasource query.</param>
        /// <param name="delimiter">(optional) The delimiter for the query results.</param>
        /// <param name="parameters">(optional) The query parameters.</param>
        public EssDatasourceQueryInfo( string query, string delimiter = ",", Dictionary<string, List<string>> parameters = null )
        {
            Query      = query;
            Delimiter  = delimiter;
            Parameters = parameters;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EssDatasourceQueryInfo"/> class with a <paramref name="datasource"/> and <paramref name="select"/> clause.
        /// </summary>
        /// <param name="datasource">The <see cref="IEssDatasource"/> against which the query will be executed.</param>
        /// <param name="select">The select clause of the query, with or without the <langword>SELECT</langword> keyword.</param>
        /// <param name="where">(optional) The where clause of the query, with or without the <langword>WHERE</langword> keyword.</param>
        /// <param name="delimiter">(optional) The delimiter for the query results.</param>
        /// <param name="parameters">(optional) The query parameters.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EssDatasourceQueryInfo( IEssDatasource datasource, string select, string where = null, string delimiter = ",", Dictionary<string, List<string>> parameters = null ) : this(select, delimiter, parameters)
        {
            if ( datasource is null )
                throw new ArgumentNullException(nameof(datasource), $"An {nameof(IEssDatasource)} {nameof(datasource)} is required to create an {nameof(EssDatasourceQueryInfo)} with this overload.");

            if ( string.IsNullOrEmpty(select) )
                throw new ArgumentNullException(nameof(select), $"A select clause is required to create an {nameof(EssDatasourceQueryInfo)} with this overload.");

            if ( (select = select.Trim()).StartsWith("select", StringComparison.OrdinalIgnoreCase) is false )
                select = $@"SELECT {select}";

            if ( !string.IsNullOrEmpty(where = where?.Trim()) && where.StartsWith("where", StringComparison.OrdinalIgnoreCase) is false )
                where = $@"WHERE {where}";

            string identifier = datasource switch
            {
                IEssApplicationDatasource ads => $@"{ads.Application?.Name}.{ads.Name}".Trim('.'),
                                            _ => $@"{datasource.Name}"
            };

            Query = $@"{select} FROM {identifier} {where}".Trim();
        }

        #endregion

        #region IEssDatasourceQueryInfo Members

        /// <inheritdoc />
        public string Delimiter { get; set; }

        /// <inheritdoc />
        public Dictionary<string, List<string>> Parameters { get; set; } = new Dictionary<string, List<string>>();

        /// <inheritdoc />
        public string Query { get; set; }

        #endregion
    }
}

