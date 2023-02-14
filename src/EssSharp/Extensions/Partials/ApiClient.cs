
using Polly;
using RestSharp.Serializers;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

using EssSharp.Api;

namespace EssSharp.Client
{
    /// <summary />
    public partial class ApiClient
    {
        /// <summary />
        public string SessionID { get; private set; } = null;

        #region Partial Methods

        /// <summary>
        /// Allows for extending request processing for <see cref="ApiClient"/> generated code.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        partial void InterceptRequest( RestRequest request )
        {
            // NOTE: We are waiting on RestSharp v109 to address an issue with cookie management.
            // https://github.com/restsharp/RestSharp/issues/1792
            /*
            // If we have a JSESSIONID, remove any authorization headers.
            if ( !string.IsNullOrEmpty(SessionID) )
            {
                request?.Parameters.GetParameters(ParameterType.HttpHeader).RemoveParameter("Authorization");
                request?.AddCookie("JSESSIONID", SessionID);
            }
            */
        }

        /// <summary>
        /// Allows for extending response processing for <see cref="ApiClient"/> generated code.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        /// <param name="response">The RestSharp response object</param>
        partial void InterceptResponse( RestRequest request, RestResponse response )
        {
            // If the response was not successful and an exception is available, throw it.
            if ( !response.IsSuccessful() && response.ErrorException is WebException webException )
                throw webException;

            // NOTE: We are waiting on RestSharp v109 to address an issue with cookie management.
            // https://github.com/restsharp/RestSharp/issues/1792
            /*
            // If we have a JSESSIONID, retain it.
            if ( response.Cookies?.Cast<Cookie>().FirstOrDefault(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase)) is { } cookie )
                SessionID = cookie.Value;
            */
        }

        #endregion
    }
}
