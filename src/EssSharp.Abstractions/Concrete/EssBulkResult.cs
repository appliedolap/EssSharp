namespace EssSharp
{
    /// <summary>
    /// What the server reported for one user or group in an operation that works on several at once: deleting several
    /// users or groups, or importing them from a file. The server answers such an operation successfully even when it
    /// fails for some of them, so check each result.
    /// </summary>
    public class EssBulkResult
    {
        /// <summary />
        /// <param name="id">The ID of the user, or the name of the group.</param>
        /// <param name="succeeded">Whether the operation succeeded for the user or group.</param>
        /// <param name="message">The message the server gave for the user or group.</param>
        public EssBulkResult( string id, bool succeeded, string message )
        {
            Id        = id;
            Succeeded = succeeded;
            Message   = message;
        }

        /// <summary>
        /// The ID of the user, or the name of the group.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The message the server gave for the user or group, such as "Deleted" or "Role with this name does not exist".
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Whether the operation succeeded for the user or group.
        /// </summary>
        /// <remarks>
        /// The server reports a row's outcome only as text, so this is read from that text: a deleted row's message is "Deleted",
        /// and an imported row's status is "Info" rather than "Error". These are the texts Essbase 26.1 answers with in English;
        /// a server that answers in other words reports every row as failed, and <see cref="Message"/> still says what happened.
        /// </remarks>
        public bool Succeeded { get; }

        /// <inheritdoc />
        public override string ToString() => $"{Id}: {Message}";
    }
}
