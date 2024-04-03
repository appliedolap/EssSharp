using Microsoft.Extensions.Logging;

namespace EssSharp
{
    /// <summary>
    /// An <see cref="EventId.Id" /> type enumeration for <see cref="EssSharp"/>.
    /// </summary>
    public enum EssSharpLogEventType
    {
        /// <summary>
        /// A standard logged message.
        /// </summary>
        Message = 0,

        /// <summary>
        /// A logged error message.
        /// </summary>
        Error = 1,

        /// <summary>
        /// A logged request.
        /// </summary>
        Request = 2,

        /// <summary>
        /// A logged response.
        /// </summary>
        Response = 3,
    }
}
