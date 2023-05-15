using System;

namespace EssSharp
{
    /// <summary />
    public interface IEssApplicationDatasourceConnection : IEssDatasourceConnection
    {
        /// <summary>
        /// Returns the parent application of the connection.
        /// </summary>
        public IEssApplication Application { get; }
    }
}
