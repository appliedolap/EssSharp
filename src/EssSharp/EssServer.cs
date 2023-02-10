﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace EssSharp
{
    /// <summary />
    public class EssServer : EssObject, IEssServer
    {
        #region Private Data

        private const string _defaultRestApiPath = "/rest/v1";
        private const int    _maxApplications    = 100;
        private readonly string _server;

        #endregion

        #region Constructors

        /// <summary />
        internal EssServer( Client.Configuration configuration, Client.ApiClient client ) : base(configuration, client) 
        {
            if ( !Uri.TryCreate(configuration?.BasePath, UriKind.Absolute, out _) )
                throw new ArgumentException("A fully qualified server REST endpoint must be set on the configuration.", nameof(configuration));

            int defaultRestApiPathIndex = configuration.BasePath.ToLowerInvariant().LastIndexOf(_defaultRestApiPath);

            if ( defaultRestApiPathIndex >= 0 )
                _server = configuration.BasePath.Substring(0, defaultRestApiPathIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public EssServer( string server, string username, string password )
        {
            _server = server?.TrimEnd('/');

            var basePath = $@"{_server}{_defaultRestApiPath}";

            if ( !Uri.TryCreate(basePath, UriKind.Absolute, out _) )
                throw new ArgumentException("A fully qualified server URL is required.", nameof(server));

            if ( string.IsNullOrEmpty(username) )
                throw new ArgumentException("A username is required.", nameof(username));

            Client        = new Client.ApiClient(basePath);
            Configuration = new Client.Configuration()
            {
                BasePath  = basePath,
                Username  = username,
                Password  = password,
                Timeout   = int.MaxValue,
                UserAgent = $"{nameof(EssSharp)}/{typeof(EssServer).Assembly.GetName().Version}",
                //Proxy = new WebProxy("localhost", 8070)
            };
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string  Name => _server;

        /// <inheritdoc />
        public override EssType Type => EssType.Server;

        #endregion

        #region IEssServer Members

        /// <inheritdoc />
        public async Task<IEssAbout> GetAboutAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var   api = GetApi<AboutEssbaseApi>();
                var about = await api.AboutGetAboutAsync(0, cancellationToken) ?? new About();
                return new EssAbout(about);
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IEssAboutInstance> GetAboutInstanceAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<AboutEssbaseApi>();
                var aboutInstance = await api.AboutGetInstanceDetailsAsync(0, cancellationToken) ?? new AboutInstance(); ;
                return new EssAboutInstance(aboutInstance);
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        /// <returns>An <see cref="EssApplication"/> object.</returns>
        public IEssApplication GetApplication( string applicationName ) => GetApplicationAsync(applicationName)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        /// <returns>An <see cref="EssApplication"/> object.</returns>
        public async Task<IEssApplication> GetApplicationAsync( string applicationName, CancellationToken cancellationToken = default )
        {
            if ( string.IsNullOrWhiteSpace(applicationName) )
                throw new ArgumentException($"An application name is required to get an {nameof(EssApplication)}.", nameof(applicationName));

            try
            {
                var api = GetApi<ApplicationsApi>();
                
                if ( await api.ApplicationsGetApplicationAsync(applicationName, null, 0, cancellationToken).ConfigureAwait(false) is { } application )
                    return new EssApplication(application, this);

                throw new Exception("Received an empty or invalid response.");
            }
            catch ( Exception e )
            {
                throw new Exception($@"Unable to get the application ""{applicationName}"". {e.Message}", e);
            }
        }

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <see cref="_maxApplications"/>.</remarks>
        public List<IEssApplication> GetApplications() => GetApplicationsAsync()?.GetAwaiter().GetResult() ?? new List<IEssApplication>();

        /// <inheritdoc />
        /// <returns>A list of <see cref="EssApplication"/> objects.</returns>
        /// <remarks>The number of returned applications is limited to the value of <see cref="_maxApplications"/>.</remarks>
        public async Task<List<IEssApplication>> GetApplicationsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                var applications = await api.ApplicationsGetApplicationsAsync(null, null, _maxApplications, null, null, null, 0, cancellationToken).ConfigureAwait(false);

                return applications?.ToEssSharpList(this) ?? new List<IEssApplication>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public IEssUserSession GetUserSession( bool includeToken = true, bool includeGroups = true ) => GetUserSessionAsync(includeToken, includeGroups)?.GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task<IEssUserSession> GetUserSessionAsync( bool includeToken = true, bool includeGroups = true, CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<UserSessionApi>();
                if ( await api.UserSessionGetSessionAsync(includeToken, includeGroups, 0, cancellationToken).ConfigureAwait(false) is { } userSession )
                    return new EssUserSession(userSession);

                throw new Exception("Received an empty or invalid response.");
            }
            catch ( Exception e )
            {
                if ( !string.IsNullOrWhiteSpace(Configuration?.Username) )
                    throw new Exception($@"Unable to create a new session for user ""{Configuration.Username}"". {e.Message}", e);
                else
                    throw new Exception($"Unable to create a new session. {e.Message}", e);
            }
        }

        /// <inheritdoc />
        public List<IEssSession> GetSessions() => GetSessionsAsync()?.GetAwaiter().GetResult() ?? new List<IEssSession>();

        /// <inheritdoc />
        public async Task<List<IEssSession>> GetSessionsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<SessionsApi>();
                var sessions = (await api.SessionsGetAllActiveSessionsAsync(null, null, null, 0, cancellationToken).ConfigureAwait(false))?
                    .Select(sessionAttributes => new EssSession(sessionAttributes) as IEssSession)?.ToList() ?? new List<IEssSession>();
                return sessions;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public List<IEssServerVariable> GetVariables() => GetVariablesAsync()?.GetAwaiter().GetResult() ?? new List<IEssServerVariable>();

        /// <inheritdoc />
        public async Task<List<IEssServerVariable>> GetVariablesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                // Setting getAllVariables to true would mean we get everything, including app and cube variables (qualified with app and cube names).
                // TODO: Enhance API to support getting all variables via the server.
                bool getAllVariables = false;

                var api = GetApi<ServerVariablesApi>();
                var variables = await api.VariablesListServerVariablesAsync(getAllVariables.ToString().ToLowerInvariant(), 0, cancellationToken).ConfigureAwait(false);

                return variables?.ToEssSharpList<IEssServerVariable>(this) ?? new List<IEssServerVariable>();
            }
            catch ( Exception )
            {
                throw;
            }
        }


        /// <inheritdoc />
        public async Task<List<IEssUrl>> GetURLsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<URLsApi>();
                var urls = await api.URLsGetAsync(0, cancellationToken).ConfigureAwait(false);

                return urls?.ToEssSharpList(this) ?? new List<IEssUrl>();
            }
            catch ( Exception )
            {
                throw;
            }
        }

        #endregion
    }
}
