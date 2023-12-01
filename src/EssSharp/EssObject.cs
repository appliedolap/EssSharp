﻿using System;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;

using Polly;
using RestSharp;

namespace EssSharp
{
    /// <inheritdoc />
    public abstract class EssObject : IEssObject
    {
        #region Constructors

        /// <summary />
        internal EssObject() => EstablishRetryConfiguration();

        /// <summary />
        /// <param name="configuration" />
        /// <param name="client" />
        internal EssObject( EssSharp.Client.Configuration configuration, EssSharp.Client.ApiClient client = null ) : this()
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Client        = client ?? new EssSharp.Client.ApiClient(configuration.BasePath);
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public virtual string Name => throw new NotImplementedException();

        /// <inheritdoc />
        public virtual EssType Type => throw new NotImplementedException();

        #endregion

        #region Internal Properties and Methods

        /// <summary>
        /// The <see cref="EssSharp.Client.ApiClient"/> assigned to this <see cref="IEssObject"/> implementation.
        /// </summary>
        internal virtual EssSharp.Client.ApiClient Client { get; set; }

        /// <summary>
        /// The <see cref="EssSharp.Client.Configuration"/> assigned to this <see cref="IEssObject"/> implementation.
        /// </summary>
        internal virtual EssSharp.Client.Configuration Configuration { get; set; }

        /// <summary>
        /// Gets the requested API using the <see cref="EssSharp.Client.Configuration"/> and <see cref="EssSharp.Client.ApiClient"/> 
        /// assigned to this <see cref="IEssObject"/> implementation.
        /// </summary>
        /// <typeparam name="T">An <see cref="EssSharp.Client.IApiAccessor"/> (API) from the <see cref="EssSharp.Api"/> namespace.</typeparam>
        /// <param name="callerPath" />
        /// <param name="callerName" />
        /// <returns>An <see cref="EssSharp.Client.IApiAccessor"/> (API) from the <see cref="EssSharp.Api"/> namespace.</returns>
        internal T GetApi<T>( [CallerFilePath] string callerPath = null, [CallerMemberName] string callerName = null ) where T : EssSharp.Client.IApiAccessor, new() => 
            ApiFactory.GetApi<T>(Configuration, Client, callerPath, callerName);

        #endregion

        #region Private Methods

        private void EstablishRetryConfiguration()
        {
            // Assign a RetryPolicy if one has not already been assigned.
            EssSharp.Client.RetryConfiguration.RetryPolicy ??= Policy<RestResponse>
                .HandleResult(processResponse)
                .Retry(1, ( _, _, context ) => resetSession(context));

            // Assign an AsyncRetryPolicy if one has not already been assigned.
            EssSharp.Client.RetryConfiguration.AsyncRetryPolicy ??= Policy<RestResponse>
                .HandleResult(processResponse)
                .RetryAsync(1, (_, _, context) => resetSession(context));

            // Process the response.
            bool processResponse( RestResponse response )
            {
                // If a request with a session cookie is unauthorized, retry the request using basic authorization.
                if ( response.StatusCode is HttpStatusCode.Unauthorized &&
                     response.Request.Parameters.GetParameters(ParameterType.HttpHeader).Any(h => string.Equals(h?.Name, "Authorization") is false) )
                {
                    return true;
                }

                // If this is a successful logout request, remove any session cookies from the response.
                if ( response.Request.Method is Method.Delete &&
                     response.StatusCode is HttpStatusCode.OK &&
                     string.Equals(response.Request.Resource, "/session", StringComparison.OrdinalIgnoreCase) )
                {
                    // If the response contains cookies and an absolute Uri can be constructed from the configurations BasePath...
                    if ( response.Cookies?.Count > 0 && Uri.TryCreate(Configuration?.BasePath, UriKind.Absolute, out var baseUri) )
                    {
                        // Remove all session cookies from the request's CookieCollection by marking it expired.
                        foreach ( Cookie cookie in response.Cookies )
                            if ( string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase) )
                                cookie.Expired = true;
                    }
                }

                return false;
            }

            // Reset the session.
            static void resetSession( Context context )
            {
                // If the context contains a RestRequest, remove the JSESSIONID and restore the basic auth header from it.
                if ( context.TryGetValue("request", out var requestValue) && requestValue is RestRequest request )
                {
                    // If the context contains the configuration, use it to capture the base Uri and credentials for basic authentication.
                    if ( context.TryGetValue("configuration", out var configValue) && configValue is EssSharp.Client.IReadableConfiguration configuration )
                    {
                        // If the request contains cookies and an absolute Uri can be constructed from the configurations BasePath...
                        if ( request.CookieContainer?.Count > 0 && Uri.TryCreate(configuration?.BasePath, UriKind.Absolute, out var baseUri) )
                        {
                            foreach ( Cookie cookie in request.CookieContainer.GetCookies(baseUri) )
                            {
                                // Remove the session cookie from the request's CookieContainer by marking it expired.
                                if ( string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase) )
                                {
                                    cookie.Expired = true;
                                    break;
                                }
                            }
                        }

                        // Reapply the basic authorization header.
                        request.AddHeader(@"Authorization", $@"Basic {EssSharp.Client.ClientUtils.Base64Encode($@"{configuration.Username}:{configuration.Password}")}");
                    }
                }
            }
        }

        #endregion
    }
}
