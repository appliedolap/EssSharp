namespace EssSharp
{
    /// <summary />
    public interface IEssApplicationDatasource : IEssDatasource
    {
        /// <summary>
        /// Returns the application that contains this datasource.
        /// </summary>
        public IEssApplication Application { get; }
    }
}
