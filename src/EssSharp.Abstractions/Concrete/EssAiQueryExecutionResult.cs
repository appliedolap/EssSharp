namespace EssSharp
{
    /// <summary>
    /// The generated AI query and its resulting Essbase grid.
    /// </summary>
    public class EssAiQueryExecutionResult
    {
        /// <summary>
        /// Returns or sets the generated query details.
        /// </summary>
        public EssAiQueryResult Query { get; set; }

        /// <summary>
        /// Returns or sets the grid produced by executing the generated MDX query.
        /// </summary>
        public IEssGrid Grid { get; set; }
    }
}
