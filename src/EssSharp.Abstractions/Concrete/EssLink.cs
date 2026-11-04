namespace EssSharp
{
    /// <summary>
    /// A link the server returns with a user or group, to an operation on it.
    /// </summary>
    public class EssLink
    {
        /// <summary />
        /// <param name="rel">What the link is for, such as "get", "edit" or "delete".</param>
        /// <param name="href">The URL of the operation.</param>
        /// <param name="method">The HTTP method of the operation.</param>
        public EssLink( string rel, string href, string method )
        {
            Rel    = rel;
            Href   = href;
            Method = method;
        }

        /// <summary>
        /// The URL of the operation.
        /// </summary>
        public string Href { get; }

        /// <summary>
        /// The HTTP method of the operation.
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// What the link is for, such as "get", "edit", "delete" or "provisionReport".
        /// </summary>
        public string Rel { get; }
    }
}
