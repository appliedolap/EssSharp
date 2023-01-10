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
                Timeout   = TimeSpan.FromMilliseconds(int.MaxValue).Milliseconds,
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
        /// <remarks>The number of returned applications is limited to the value of <see cref="_maxApplications"/>.</remarks>
        public List<IEssApplication> GetApplications() => GetApplicationsAsync()?.GetAwaiter().GetResult() ?? new List<IEssApplication>();

        /// <inheritdoc />
        public IEssApplication GetApplication(string applicationName)
        {
            try
            {
                var api = GetApi<ApplicationsApi>();
                var application = api.ApplicationsGetApplication(applicationName);
                if (application != null)
                {
                    return new EssApplication(this, application);
                }
                else
                {
                    // TODO: replace with custom exception or something more appropriate
                    throw new ArgumentException($"No such app {applicationName}");
                }
            }
            catch ( Exception )
            {
                throw;
            }
        }
        
        /// <inheritdoc />
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
        public async Task<List<IEssVariable>> GetVariablesAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<ServerVariablesApi>();
                // false here means we'll get server scoped variables only. Setting true would mean we get everything,
                // including app and cube variables (qualified with app and cube names). TODO: enhance API to allow
                // getting all
                var variables = (await api.VariablesListServerVariablesAsync(false.ToString().ToLowerInvariant(), 0, cancellationToken))?.Items?
                    .Select(variable => new EssVariable(this, variable) as IEssVariable)?.ToList() ?? new List<IEssVariable>();
                return variables;
            }
            catch ( Exception )
            {
                throw;
            }
        }


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
        public async Task<List<IEssUrl>> GetURLsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<URLsApi>();
                var urls = (await api.URLsGetAsync(0, cancellationToken))?.Items?
                    .Select(url => new EssUrl(url) as IEssUrl)?.ToList() ?? new List<IEssUrl>();

                return urls;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<List<IEssSession>> GetSessionsAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var api = GetApi<SessionsApi>();
                var sessions = (await api.SessionsGetAllActiveSessionsAsync(null,null,null, 0, cancellationToken))?
                    .Select(sessionAttributes => new EssSession(sessionAttributes) as IEssSession)?.ToList() ?? new List<IEssSession>();

                return sessions;
            }
            catch ( Exception )
            {
                throw;
            }
        }

        #endregion
    }
}
