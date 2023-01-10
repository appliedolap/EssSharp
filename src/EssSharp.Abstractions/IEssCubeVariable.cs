namespace EssSharp
{
    /// <summary />
    public interface IEssCubeVariable : IEssApplicationVariable
    {
        /// <summary>
        /// Returns the cube of this variable.
        /// </summary>
        public IEssCube Cube { get; } 
    }
}