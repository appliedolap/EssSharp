namespace EssSharp
{
    /// <summary>
    /// The result of generating an MDX query from natural language.
    /// </summary>
    public class EssAiQueryResult
    {
        /// <summary>
        /// Returns or sets the natural-language query submitted to Essbase.
        /// </summary>
        public string NaturalLanguageQuery { get; set; }

        /// <summary>
        /// Returns or sets the generated MDX query.
        /// </summary>
        public string Mdx { get; set; }

        /// <summary>
        /// Returns or sets an explanation returned with the generated query, if available.
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Returns or sets the AI chat profile used to generate the query.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Returns or sets the unprocessed response returned by Essbase.
        /// </summary>
        public string RawResponse { get; set; }
    }
}
