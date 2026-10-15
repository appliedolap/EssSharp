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
        /// <param name="startNewConversation">Whether the query starts a new conversation under the profile name (the default), rather than continuing one that already exists.</param>
        /// <param name="includeAttributes">Whether attribute dimensions should be included when interpreting the natural-language query.</param>
        /// <param name="prompt">An optional prompt, passed to the MDX generator with the natural-language query as the request's prompt parameter. Essbase decides how it is used.</param>
        public EssAiQueryOptions( bool startNewConversation = true, bool includeAttributes = false, string prompt = null )
        {
            StartNewConversation = startNewConversation;
            IncludeAttributes    = includeAttributes;
            Prompt               = prompt;
        }

        /// <summary>
        /// Returns or sets whether the query starts a new conversation.
        /// </summary>
        public bool StartNewConversation { get; set; }

        /// <summary>
        /// Returns or sets whether attribute dimensions should be included when interpreting the natural-language query.
        /// </summary>
        public bool IncludeAttributes { get; set; }

        /// <summary>
        /// Returns or sets an optional prompt, passed to the MDX generator with the natural-language query as the request's prompt parameter.
        /// </summary>
        public string Prompt { get; set; }
    }
}
