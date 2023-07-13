using System;
using System.Net;
using System.Linq;

using EssSharp.Api;

using RestSharp;

namespace EssSharp.Client
{
    /// <summary />
    public partial class ApiClient
    {
        /// <summary />
        public Cookie SessionCookie { get; internal set; } = null;

        #region Partial Methods

        /// <summary>
        /// Allows for extending request processing for <see cref="ApiClient"/> generated code.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        partial void InterceptRequest( RestRequest request )
        {
            // Return if the request is null.
            if ( request is null )
                return;

            // NOTE: We are waiting on the OpenAPI Generator to bring in RestSharp v109+ to address an issue with cookie management.
            // https://github.com/restsharp/RestSharp/issues/1792

            // If we have a JSESSIONID, remove any authorization headers.
            if ( SessionCookie is not null )
            {
                request.Parameters?.RemoveParameter("Authorization");
                request.AddCookie(SessionCookie.Name, SessionCookie.Value, SessionCookie.Path, SessionCookie.Domain);
            }
        }

        /// <summary>
        /// Allows for extending response processing for <see cref="ApiClient"/> generated code.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        /// <param name="response">The RestSharp response object</param>
        partial void InterceptResponse( RestRequest request, RestResponse response )
        {
            // Return if the response is null.
            if ( response is null )
                return;

            // If we have a JSESSIONID, retain the session cookie.
            if ( response.Cookies?.Cast<Cookie>().FirstOrDefault(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase)) is { } cookie )
                SessionCookie = cookie;

            // If the response was not successful and an exception is available, throw it.
            if ( !response.IsSuccessful() && response.ErrorException is WebException webException )
                throw webException;
        }

        #endregion
    }
}
