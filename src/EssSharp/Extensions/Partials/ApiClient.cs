using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using EssSharp.Api;

using RestSharp;

namespace EssSharp.Client
{
    /// <summary />
    public partial class ApiClient
    {
        #region Private Fields

        private int _maxDegreeOfParallelism = 4;

        #endregion

        #region Public Properties

        /// <summary>
        /// The session cookies retained for this client, keyed by cookie name. The full set is replayed
        /// on subsequent requests in place of the authorization header once a session is established.
        /// </summary>
        public ConcurrentDictionary<string, Cookie> SessionCookies { get; } = new ConcurrentDictionary<string, Cookie>(StringComparer.OrdinalIgnoreCase);

        /// <summary />
        public ConcurrentDictionary<string, EssGridPreferences> SessionPreferences { get; } = new ConcurrentDictionary<string, EssGridPreferences>();

        #endregion

        #region Private Properties

        /// <summary />
        private SemaphoreSlim RequestSemaphore { get; set; } = null;

        /// <summary />
        private int MaxDegreeOfParallelism
        {
            set
            {
                if ( _maxDegreeOfParallelism != value || value >= 0 && RequestSemaphore is null )
                     RequestSemaphore = (_maxDegreeOfParallelism = value) >= 0
                        ? new SemaphoreSlim(_maxDegreeOfParallelism, _maxDegreeOfParallelism) 
                        : null;
            }
        }

        #endregion

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
            {
                // Write the request to any configured logger and return;
                request?.WriteLogMessage(configuration);
                return;
            }

            // Snapshot the retained session cookies and capture the JSESSIONID, if one is retained.
            var sessionCookies = SessionCookies.Values.ToArray();
            var cookie = sessionCookies.FirstOrDefault(retained => string.Equals(retained?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase));

            // If a session is established, remove any authorization headers in favor of the session cookies.
            if ( cookie is not null )
                request.Parameters?.RemoveParameter("Authorization");

            // Apply the full set of retained cookies, since gateways in front of Essbase can require
            // their own token cookies (e.g. gatewayToken and brokerToken) on every request.
            foreach ( var sessionCookie in sessionCookies )
                request.AddCookie(sessionCookie.Name, sessionCookie.Value, sessionCookie.Path, sessionCookie.Domain);

            // If there are no configured preferences, we are finished 
            if ( (options?.Preferences as EssGridPreferences)?.Clone() is not EssGridPreferences configuredPreferences )
            {
                // Write the request to any configured logger and return;
                request?.WriteLogMessage(configuration);
                return;
            }

            // If there is a session cookie...
            if ( cookie is not null )
            {
                // If there are preferences tracked against the JSESSIONID...
                if ( SessionPreferences.TryGetValue(cookie.Value, out var sessionPreferences) && sessionPreferences is not null )
                {
                    // If the preferences for this session match the configured preferences, we are finished.
                    if ( sessionPreferences.Equals(configuredPreferences) )
                    {
                        // Write the request to any configured logger and return;
                        request?.WriteLogMessage(configuration);
                        return;
                    }
                }

                // Set the grid preferences for the session.
                await setGridPreferencesAsync(configuredPreferences, cookie).ConfigureAwait(false);
            }
            else
            {
                // Set the grid preferences for a new session.
                await setGridPreferencesAsync(configuredPreferences).ConfigureAwait(false);
            }

            // Write the request to any configured logger.
            request?.WriteLogMessage(configuration); 

            async Task setGridPreferencesAsync( EssGridPreferences preferences, Cookie cookie = null )
            {
                var config = new Configuration()
                {
                    ApplyCookies  = false,
                    RetainCookies = false,

                    Logger                 = configuration.Logger,
                    BasePath               = configuration.BasePath,
                    MaxDegreeOfParallelism = configuration.MaxDegreeOfParallelism,
                    Username               = configuration.Username,
                    Password               = configuration.Password,
                    Timeout                = configuration.Timeout,
                    UserAgent              = configuration.UserAgent,
                };

                var api = ApiFactory.GetApiAndClient<GridPreferencesApi>(config).Api;

                var response = await api.GridPreferencesSetForSessionAsync(preferences.ToModelObject(), cookie, cancellationToken: cancellationToken).ConfigureAwait(false);

                // Capture the response cookies, recovering any that cookie processing rejected.
                var responseCookies = response.Cookies
                    .Concat(RecoverDroppedCookies(response.Cookies, GetSetCookieValues(response.Headers), config))
                    .Where(returned => returned is { Expired: false })
                    .ToArray();

                // Capture the returned session cookie: the new session created for the preferences when no cookie
                // was given or, on servers where the grid API forks the session, the fork carrying the preferences.
                var responseCookie = responseCookies.FirstOrDefault(returned => string.Equals(returned?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase));

                if ( responseCookie?.Value is { Length: > 0 } sessionID )
                {
                    // Update the grid preferences tracked against the JSESSIONID.
                    SessionPreferences.AddOrUpdate(
                        key:                            sessionID,
                        addValue:                       preferences,
                        updateValueFactory: ( _, _ ) => preferences);

                    // If the preferences created a new session, retain its full cookie set and ride it on this
                    // request in place of any authorization header, so the request observes the preferences just
                    // applied instead of authenticating into a fresh, undecorated session.
                    if ( cookie is null )
                    {
                        request.Parameters?.RemoveParameter("Authorization");

                        foreach ( var returned in responseCookies )
                        {
                            RetainCookie(returned);
                            request.AddCookie(returned.Name, returned.Value, returned.Path, returned.Domain);
                        }
                    }
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

            // If configured to do so, retain the response's session cookies.
            if ( configuration?.RetainCookies is true )
            {
                // If this is a successful logout, clear the retained cookies and their tracked preferences.
                if ( IsSuccessfulLogout(request, response) )
                {
                    foreach ( var retained in SessionCookies.Values )
                        SessionPreferences.TryRemove(retained?.Value ?? string.Empty, out _);

                    SessionCookies.Clear();
                }
                else
                {
                    // Retain every parsed cookie, dropping any the response expired.
                    foreach ( var parsed in response.Cookies?.Cast<Cookie>() ?? Enumerable.Empty<Cookie>() )
                        RetainCookie(parsed);

                    // Recover and retain cookies that cookie processing rejected, e.g. for an empty Domain= attribute.
                    foreach ( var recovered in RecoverDroppedCookies(response, configuration) )
                        RetainCookie(recovered);
                }
            }

            // Write the response to any configured logger.
            response.WriteLogMessage(configuration);

            // If the response was not successful and an exception is available, throw it.
            if ( !response.IsSuccessful() && response.ErrorException is WebException webException )
                throw webException;

            // Return.
            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Retains the given cookie in <see cref="SessionCookies"/>, or removes the retained cookie of the
        /// same name (together with any grid preferences tracked against its value) when the given cookie
        /// is expired, which is how a server deletes a cookie.
        /// </summary>
        /// <param name="cookie">The parsed or recovered cookie to retain.</param>
        private void RetainCookie( Cookie cookie )
        {
            // Return if the cookie has no name.
            if ( cookie?.Name is not { Length: > 0 } name )
                return;

            // If the cookie is expired, remove the retained cookie and any preferences tracked against its value.
            if ( cookie.Expired )
            {
                if ( SessionCookies.TryRemove(name, out var removed) )
                    SessionPreferences.TryRemove(removed?.Value ?? string.Empty, out _);

                return;
            }

            // Retain the cookie by name.
            SessionCookies[name] = cookie;
        }

        /// <summary>
        /// Returns whether the given request is a successful logout, after which no session state should be retained.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        /// <param name="response">The RestSharp response object</param>
        private static bool IsSuccessfulLogout( RestRequest request, RestResponse response ) =>
            request?.Method is RestSharp.Method.Delete &&
            string.Equals(request?.Resource, @"/session", StringComparison.OrdinalIgnoreCase) &&
            response.IsSuccessful();

        /// <summary>
        /// Recovers cookies from the raw Set-Cookie response headers that cookie processing rejected.
        /// Some fronting proxies and load balancers (including the Essbase on Autonomous Database gateway)
        /// emit legacy attribute tails such as <c>;Version=1;Comment=;Domain=;</c>, whose empty
        /// <c>Domain=</c> attribute RFC 6265 (section 5.2.3) directs clients to ignore but the .NET
        /// <see cref="CookieContainer"/> rejects with a <see cref="CookieException"/>, dropping the whole
        /// cookie. Recovered cookies are scoped host-only to the configured base address host.
        /// </summary>
        /// <param name="response">The RestSharp response object</param>
        /// <param name="configuration">The per-client configuration.</param>
        /// <returns>The recovered cookies, if any, with Max-Age expirations marked.</returns>
        private static IEnumerable<Cookie> RecoverDroppedCookies( RestResponse response, IReadableConfiguration configuration ) =>
            RecoverDroppedCookies(
                response.Cookies?.Cast<Cookie>(),
                response.Headers?.Where(header => string.Equals(header?.Name, @"Set-Cookie", StringComparison.OrdinalIgnoreCase)).Select(header => header?.Value?.ToString()),
                configuration);

        /// <summary>
        /// Recovers cookies from the given raw Set-Cookie header values that cookie processing rejected.
        /// </summary>
        /// <param name="parsedCookies">The cookies that parsed normally.</param>
        /// <param name="setCookieHeaders">The raw Set-Cookie header values.</param>
        /// <param name="configuration">The per-client configuration.</param>
        /// <returns>The recovered cookies, if any, with Max-Age expirations marked.</returns>
        private static IEnumerable<Cookie> RecoverDroppedCookies( IEnumerable<Cookie> parsedCookies, IEnumerable<string> setCookieHeaders, IReadableConfiguration configuration )
        {
            // Yield nothing if an absolute base uri cannot be constructed, since recovered cookies are scoped to its host.
            if ( !Uri.TryCreate(configuration?.BasePath, UriKind.Absolute, out var baseUri) )
                yield break;

            // Capture the names of the cookies that parsed, so only dropped headers are recovered.
            var parsedNames = new HashSet<string>((parsedCookies ?? Enumerable.Empty<Cookie>()).Select(parsed => parsed?.Name).Where(name => !string.IsNullOrEmpty(name)), StringComparer.OrdinalIgnoreCase);

            foreach ( var setCookie in setCookieHeaders ?? Enumerable.Empty<string>() )
            {
                // Skip anything but a non-empty header value.
                if ( setCookie is not { Length: > 0 } )
                    continue;

                // Split the header into the cookie-pair and its attributes.
                var segments = setCookie.Split(';');

                // Split the cookie-pair into a name and a value, skipping the header unless both are present.
                if ( segments[0].Split(new[] { '=' }, 2) is not { Length: 2 } pair || pair[0].Trim() is not { Length: > 0 } name || pair[1].Trim() is not { Length: > 0 } value )
                    continue;

                // Skip the header if its cookie parsed normally.
                if ( parsedNames.Contains(name) )
                    continue;

                var path = @"/";
                var expired = false;

                // Process the cookie attributes, capturing a rooted path and a Max-Age expiration
                // and ignoring the malformed attributes the cookie was rejected for.
                foreach ( var segment in segments.Skip(1) )
                {
                    var attribute = segment.Split(new[] { '=' }, 2);
                    var attributeName = attribute[0].Trim();
                    var attributeValue = attribute.Length is 2 ? attribute[1].Trim() : string.Empty;

                    // Capture a rooted path attribute.
                    if ( string.Equals(attributeName, @"Path", StringComparison.OrdinalIgnoreCase) && attributeValue.StartsWith(@"/") )
                        path = attributeValue;
                    // Consider the cookie expired when a Max-Age attribute is zero or negative.
                    else if ( string.Equals(attributeName, @"Max-Age", StringComparison.OrdinalIgnoreCase) && int.TryParse(attributeValue, out int maxAge) && maxAge <= 0 )
                        expired = true;
                }

                // Yield the recovered cookie, if one can be constructed.
                if ( CreateRecoveredCookie(name, value, path, baseUri.Host, expired) is { } recovered )
                    yield return recovered;
            }
        }

        /// <summary>
        /// Returns the raw Set-Cookie header values from the given response headers.
        /// </summary>
        /// <param name="headers">The response headers, keyed by header name.</param>
        private static IEnumerable<string> GetSetCookieValues( Multimap<string, string> headers ) =>
            headers?.Where(header => string.Equals(header.Key, @"Set-Cookie", StringComparison.OrdinalIgnoreCase)).SelectMany(header => header.Value ?? (IList<string>)Array.Empty<string>()) ?? Enumerable.Empty<string>();

        /// <summary>
        /// Creates a recovered cookie, returning null when the name or value cannot be represented.
        /// </summary>
        /// <param name="name">The cookie name.</param>
        /// <param name="value">The cookie value.</param>
        /// <param name="path">The cookie path.</param>
        /// <param name="host">The host to scope the cookie to.</param>
        /// <param name="expired">Whether the cookie carried an elapsed Max-Age.</param>
        private static Cookie CreateRecoveredCookie( string name, string value, string path, string host, bool expired )
        {
            try
            {
                return new Cookie(name, value, path, host) { Expired = expired };
            }
            catch ( CookieException )
            {
                // Return null for a cookie that cannot be represented.
                return null;
            }
        }

        #endregion
    }
}
