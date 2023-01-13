namespace EssSharp
{
    /// <summary />
    public interface IEssApplicationVariable : IEssServerVariable
    {
        /// <summary>
        /// Returns the application of this variable.
        /// </summary>
        public IEssApplication Application { get; }
    }
}
