using System;
using System.Collections.Concurrent;
using System.Net;
using System.Linq;

using EssSharp.Api;

using RestSharp;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace EssSharp.Client
{
    /// <summary />
    public partial class ApiClient
    {
        /// <summary />
        public ConcurrentBag<Cookie> SessionCookies { get; } = new ConcurrentBag<Cookie>();

        /// <summary />
        public ConcurrentDictionary<string, EssGridPreferences> SessionPreferences { get; } = new ConcurrentDictionary<string, EssGridPreferences>();

        #region Partial Methods

        /// <summary>
        /// Allows for extending request processing for <see cref="ApiClient"/> generated code.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        /// <param name="configuration">The per-client configuration.</param>
        /// <param name="options">The per-request options.</param>
        /// <param name="cancellationToken" />
        private partial async Task InterceptRequestAsync( RestRequest request, IReadableConfiguration configuration, RequestOptions options, CancellationToken cancellationToken )
        {
            // Return if the request is null.
            if ( request is null )
                return;

            // Return if cached session cookies will not be applied.
            if ( configuration?.ApplyCookies is false )
                return;

            // If we have a free JSESSIONID for the user, remove any authorization headers and use it.
            if ( SessionCookies.TryTake(out Cookie cookie) && cookie is not null )
            {
                request.Parameters?.RemoveParameter("Authorization");
                request.AddCookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
            }

            // If this is a grid request, compare the configured preferences to those tracked against the JSESSIONID.
            //if ( request.Resource.Contains("/grid/") || request.Resource.EndsWith("/grid", StringComparison.Ordinal) )
            //{
                // If there are no configured preferences, we are finished 
                if ( (options?.Preferences as EssGridPreferences)?.Clone() is not EssGridPreferences configuredPreferences )
                    return;

                // If there is a session cookie...
                if ( cookie is not null )
                {
                    // If there are preferences tracked against the JSESSIONID...
                    if ( SessionPreferences.TryGetValue(cookie.Value, out var sessionPreferences) && sessionPreferences is not null )
                    {
                        // If the preferences for this session match the configured preferences, we are finished.
                        if ( sessionPreferences.Equals(configuredPreferences) )
                            return;
                    }

                    // Set the grid preferences for the session.
                    await setGridPreferencesAsync(configuredPreferences, cookie);

                }
                else
                {
                    // Set the grid preferences for a new session.
                    await setGridPreferencesAsync(configuredPreferences);
                }
            //}

            async Task setGridPreferencesAsync( EssGridPreferences preferences, Cookie cookie = null )
            {
                var config = new Configuration()
                {
                    ApplyCookies  = false,
                    RetainCookies = false,

                    BasePath      = configuration.BasePath,
                    Username      = configuration.Username,
                    Password      = configuration.Password,
                    Timeout       = configuration.Timeout,
                    UserAgent     = configuration.UserAgent,
                };

                var api = ApiFactory.GetApiAndClient<GridPreferencesApi>(config).Api;

                var sessionID = (await api.GridPreferencesSetForSessionAsync(preferences.ToModelObject(), cookie)).Cookies.FirstOrDefault(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase) && !cookie.Expired )?.Value;

                if ( !string.IsNullOrEmpty(sessionID) )
                {
                    // Update the grid preferences tracked against the JSESSIONID.
                    SessionPreferences.AddOrUpdate(
                        key:                            sessionID,
                        addValue:                       preferences,
                        updateValueFactory: ( _, _ ) => preferences);
                }
            }
        }

        /// <summary>
        /// Allows for extending response processing for <see cref="ApiClient"/> generated code.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        /// <param name="response">The RestSharp response object</param>
        /// <param name="configuration">The per-client configuration.</param>
        /// <param name="options">The per-request options.</param>
        /// <param name="cancellationToken" />
        private partial Task InterceptResponseAsync( RestRequest request, RestResponse response, IReadableConfiguration configuration, RequestOptions options, CancellationToken cancellationToken )
        {
            // Return if the response is null.
            if ( response is null )
                return Task.CompletedTask;

            // If configured to do so, retain any session cookies
            if ( configuration?.RetainCookies is true )
            {
                // If we have an unexpired JSESSIONID, retain it as a session cookie.
                if ( response.Cookies?.Cast<Cookie>().FirstOrDefault(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase)) is { Expired: false } cookie )
                    SessionCookies.Add(cookie);
            }

            // If the response was not successful and an exception is available, throw it.
            if ( !response.IsSuccessful() && response.ErrorException is WebException webException )
                throw webException;

            // Return.
            return Task.CompletedTask;
        }

        #endregion
    }
}
