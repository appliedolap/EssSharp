using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
        /// The pool of retained session cookie sets for this client. Each set carries the full group of
        /// cookies for one session (the JSESSIONID together with any gateway token cookies issued with it).
        /// A request takes a set from the pool and rides it in place of the authorization header, and its
        /// response pools the set (or its forked successor) again, so concurrent requests ride distinct
        /// sessions, since Essbase does not allow concurrent requests on one session.
        /// </summary>
        public ConcurrentBag<CookieCollection> SessionCookies { get; } = new ConcurrentBag<CookieCollection>();

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

            Cookie cookie = null;

            // If a free session cookie set is available, take it for this request, so concurrent
            // requests ride distinct sessions.
            if ( SessionCookies.TryTake(out CookieCollection sessionCookies) && sessionCookies is { Count: > 0 } )
            {
                // Capture the JSESSIONID session cookie, if the set contains one.
                cookie = sessionCookies.Cast<Cookie>().FirstOrDefault(retained => string.Equals(retained?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase));

                // If the set establishes a session, remove any authorization headers in favor of its cookies.
                if ( cookie is not null )
                    request.Parameters?.RemoveParameter("Authorization");

                // Apply the full cookie set, since gateways in front of Essbase can require their own
                // token cookies (e.g. gatewayToken and brokerToken) on every request.
                foreach ( Cookie sessionCookie in sessionCookies )
                    request.AddCookie(sessionCookie.Name, sessionCookie.Value, sessionCookie.Path, sessionCookie.Domain);
            }

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

                // Capture the cookies the response actually set, recovering any that cookie processing rejected:
                // the new session created for the preferences when no cookie was given or, on servers where the
                // grid API forks the session, the fork carrying the preferences.
                var responseCookies = BuildResponseCookieSet(response.Cookies, GetSetCookieValues(response.Headers), config).Where(returned => returned is { Expired: false }).ToArray();

                var responseCookie = responseCookies.FirstOrDefault(returned => string.Equals(returned?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase));

                if ( responseCookie?.Value is { Length: > 0 } sessionID )
                {
                    // Update the grid preferences tracked against the JSESSIONID.
                    SessionPreferences.AddOrUpdate(
                        key:                            sessionID,
                        addValue:                       preferences,
                        updateValueFactory: ( _, _ ) => preferences);

                    // If the preferences created a new session, ride its full cookie set on this request in
                    // place of any authorization header, so the request observes the preferences just applied
                    // instead of authenticating into a fresh, undecorated session. The response pools the
                    // ridden set for subsequent requests.
                    if ( cookie is null )
                    {
                        request.Parameters?.RemoveParameter("Authorization");

                        foreach ( var returned in responseCookies )
                            request.AddCookie(returned.Name, returned.Value, returned.Path, returned.Domain);
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

            // If configured to do so, retain the session cookie set for the request and response.
            if ( configuration?.RetainCookies is true )
                RetainSessionCookies(request, response, configuration);

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
        /// Pools the session cookie set carried by the given request and response: the unexpired cookies
        /// that rode the request (which continue the session on servers that do not reissue cookies),
        /// superseded by the cookies the response actually set (e.g. the successor session when the grid
        /// API forks). Nothing is pooled after a failed response, whose ridden session is not trusted, or
        /// after a successful logout, which ends the session.
        /// </summary>
        /// <param name="request">The RestSharp request object</param>
        /// <param name="response">The RestSharp response object</param>
        /// <param name="configuration">The per-client configuration.</param>
        private void RetainSessionCookies( RestRequest request, RestResponse response, IReadableConfiguration configuration )
        {
            // Return after a successful logout, which ends the session (the ridden set stays out of the pool).
            if ( IsSuccessfulLogout(request, response) )
                return;

            // Return without pooling for a failed response, e.g. a 401 whose ridden session is dead.
            if ( !response.IsSuccessful() )
                return;

            // Return if an absolute base uri cannot be constructed, since the ridden and recovered cookies
            // are resolved against it.
            if ( !Uri.TryCreate(configuration?.BasePath, UriKind.Absolute, out var baseUri) )
                return;

            var merged = new Dictionary<string, Cookie>(StringComparer.OrdinalIgnoreCase);

            // Start from the unexpired cookies that rode the request. (A retried 401 expires them.)
            foreach ( Cookie ridden in request?.CookieContainer?.GetCookies(baseUri) ?? new CookieCollection() )
            {
                if ( ridden is { Expired: false } )
                    merged[ridden.Name] = ridden;
            }

            // Supersede them with the cookies the response actually set, parsed when available and
            // recovered otherwise, removing any the response expired.
            foreach ( var setCookie in BuildResponseCookieSet(response.Cookies?.Cast<Cookie>(), GetSetCookieValues(response), configuration) )
            {
                if ( setCookie.Expired )
                    merged.Remove(setCookie.Name);
                else
                    merged[setCookie.Name] = setCookie;
            }

            // Return if the merged set carries no session.
            if ( !merged.Values.Any(cookie => string.Equals(cookie?.Name, @"JSESSIONID", StringComparison.OrdinalIgnoreCase)) )
                return;

            // Pool the merged session cookie set.
            var sessionCookies = new CookieCollection();

            foreach ( var cookie in merged.Values )
                sessionCookies.Add(cookie);

            SessionCookies.Add(sessionCookies);
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
        /// Builds the set of cookies a response actually set from its raw Set-Cookie header values,
        /// preferring the parsed cookie matching each header by name and value and tolerantly recovering
        /// the rest. Some fronting proxies and load balancers (including the Essbase on Autonomous
        /// Database gateway) emit legacy attribute tails such as <c>;Version=1;Comment=;Domain=;</c>,
        /// whose empty <c>Domain=</c> attribute RFC 6265 (section 5.2.3) directs clients to ignore but
        /// the .NET <see cref="CookieContainer"/> rejects with a <see cref="CookieException"/>, dropping
        /// the whole cookie. Recovered cookies are scoped host-only to the configured base address host,
        /// with elapsed Max-Age expirations marked.
        /// </summary>
        /// <param name="parsedCookies">The cookies that parsed normally.</param>
        /// <param name="setCookieHeaders">The raw Set-Cookie header values.</param>
        /// <param name="configuration">The per-client configuration.</param>
        private static List<Cookie> BuildResponseCookieSet( IEnumerable<Cookie> parsedCookies, IEnumerable<string> setCookieHeaders, IReadableConfiguration configuration )
        {
            var setCookies = new List<Cookie>();

            // Return an empty set if an absolute base uri cannot be constructed, since recovered cookies
            // are scoped to its host.
            if ( !Uri.TryCreate(configuration?.BasePath, UriKind.Absolute, out var baseUri) )
                return setCookies;

            // Capture the cookies that parsed normally.
            var parsed = (parsedCookies ?? Enumerable.Empty<Cookie>()).Where(candidate => candidate?.Name is { Length: > 0 }).ToArray();

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

                // Prefer the parsed cookie matching the header by name and value.
                if ( parsed.FirstOrDefault(candidate => string.Equals(candidate.Name, name, StringComparison.OrdinalIgnoreCase) && string.Equals(candidate.Value, value, StringComparison.Ordinal)) is { } parsedCookie )
                {
                    setCookies.Add(parsedCookie);
                    continue;
                }

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

                // Add the recovered cookie, if one can be constructed.
                if ( CreateRecoveredCookie(name, value, path, baseUri.Host, expired) is { } recovered )
                    setCookies.Add(recovered);
            }

            return setCookies;
        }

        /// <summary>
        /// Returns the raw Set-Cookie header values from the given response.
        /// </summary>
        /// <param name="response">The RestSharp response object</param>
        private static IEnumerable<string> GetSetCookieValues( RestResponse response ) =>
            response?.Headers?.Where(header => string.Equals(header?.Name, @"Set-Cookie", StringComparison.OrdinalIgnoreCase)).Select(header => header?.Value?.ToString()) ?? Enumerable.Empty<string>();

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
