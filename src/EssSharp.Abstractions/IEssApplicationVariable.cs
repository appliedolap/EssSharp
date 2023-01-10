namespace EssSharp
{
    /// <summary />
    public interface IEssApplicationVariable : IEssVariable
    {
        /// <summary>
        /// Returns the application of this variable.
        /// </summary>
        public IEssApplication Application { get; }
    }
}
