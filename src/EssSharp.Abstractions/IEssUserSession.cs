namespace EssSharp
{
    /// <summary />
    public interface IEssUserSession
    {
        /// <summary>
        /// Returns the token associated with this user session.
        /// </summary>
        /// <remarks>The token is only available if explicitly captured.</remarks>
        public string Token { get; }

        /// <summary>
        /// Returns the user ID associated with this user session.
        /// </summary>
        public string UserId { get; }
    }

}
