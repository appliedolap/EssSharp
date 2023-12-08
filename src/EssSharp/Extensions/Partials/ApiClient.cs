using System;
using System.Collections.Concurrent;
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
        public ConcurrentBag<Cookie> SessionCookies { get; } = new ConcurrentBag<Cookie>();

        /*
        /// <summary />
        public static ConcurrentBag<Cookie> GridCookies { get; } = new ConcurrentBag<Cookie>();
        */

        #region Partial Methods

        /// <summary>
        /// Allows for extending request processing for <see cref="ApiClient"/> generated code.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        /// <param name="configuration">A per-request configuration object.</param>
        partial void InterceptRequest( RestRequest request, IReadableConfiguration configuration )
        {
            // Return if the request is null.
            if ( request is null )
                return;

            // If we have a free JSESSIONID for the user, remove any authorization headers and use it.
            if ( SessionCookies.TryTake( out var cookie ) )
            {
                request.Parameters?.RemoveParameter("Authorization");
                request.AddCookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
            }
        }

        /// <summary>
        /// Allows for extending response processing for <see cref="ApiClient"/> generated code.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        /// <param name="response">The RestSharp response object</param>
        /// <param name="configuration">A per-request configuration object.</param>
        partial void InterceptResponse( RestRequest request, RestResponse response, IReadableConfiguration configuration )
        {
            // Return if the response is null.
            if ( response is null )
                return;

            // If we have an unexpired JSESSIONID, retain it as a session cookie.
            if ( response.Cookies?.Cast<Cookie>().FirstOrDefault(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase)) is { Expired: false } cookie )
                SessionCookies.Add(cookie);

            // If the response was not successful and an exception is available, throw it.
            if ( !response.IsSuccessful() && response.ErrorException is WebException webException )
                throw webException;
        }

        #endregion
    }
}
