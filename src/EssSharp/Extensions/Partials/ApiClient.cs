
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
            if ( !string.IsNullOrEmpty(SessionID) )
            {
                // Since we have a JSESSIONID, remove any authorization headers.
                request?.Parameters.GetParameters(ParameterType.HttpHeader).RemoveParameter("Authorization");
            }
        }

        /// <summary>
        /// Allows for extending response processing for <see cref="ApiClient"/> generated code.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        /// <param name="response">The RestSharp response object</param>
        /// <param name="configuration">The configuration object</param>
        protected partial RestResponse<T> InterceptResponse<T>( RestRequest request, RestResponse<T> response, IReadableConfiguration configuration )
        {
            // If the response was not successful and an exception is available, throw it.
            if ( !response.IsSuccessful() && response.ErrorException is WebException webException )
                throw webException;

            return response;
        }

        #endregion
    }
}
