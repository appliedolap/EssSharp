namespace EssSharp
{
    /// <summary>
    /// Options for generating an MDX query from natural language.
    /// </summary>
    public class EssAiQueryOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EssAiQueryOptions"/> class.
        /// </summary>
        /// <param name="startNewConversation">Whether the query starts a new conversation.</param>
        /// <param name="includeAttributes">Whether attribute dimensions should be included when interpreting the natural-language query.</param>
        /// <param name="prompt">An optional prompt to provide to the MDX generator.</param>
        public EssAiQueryOptions( bool startNewConversation = true, bool includeAttributes = false, string prompt = null )
        {
            StartNewConversation = startNewConversation;
            IncludeAttributes    = includeAttributes;
            Prompt               = prompt;
        }

        /// <summary>
        /// Returns or sets whether the query starts a new conversation.
        /// </summary>
        public bool StartNewConversation { get; set; } = true;

        /// <summary>
        /// Returns or sets whether attribute dimensions should be included when interpreting the natural-language query.
        /// </summary>
        public bool IncludeAttributes { get; set; }

        /// <summary>
        /// Returns or sets an optional prompt to provide to the MDX generator.
        /// </summary>
        public string Prompt { get; set; }
    }
}
