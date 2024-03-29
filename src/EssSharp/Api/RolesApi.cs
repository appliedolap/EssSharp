/*
 * The REST API for Oracle Essbase enables you to automate management of Essbase resources and operations. All requests and responses are communicated over secured HTTP.
 *
 * The version of the OpenAPI document: 1.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using EssSharp.Client;
using EssSharp.Model;

namespace EssSharp.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IRolesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Get Essbase Roles
        /// </summary>
        /// <remarks>
        /// &lt;p&gt;Returns the roles available in Essbase. Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;. If type is empty, then both &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt; roles are returned.&lt;/p&gt; &lt;p&gt;If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.&lt;/p&gt;
        /// </remarks>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="type">&lt;p&gt;Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Roles</returns>
        Roles RolesGetRoles(string type = default(string), int operationIndex = 0);

        /// <summary>
        /// Get Essbase Roles
        /// </summary>
        /// <remarks>
        /// &lt;p&gt;Returns the roles available in Essbase. Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;. If type is empty, then both &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt; roles are returned.&lt;/p&gt; &lt;p&gt;If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.&lt;/p&gt;
        /// </remarks>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="type">&lt;p&gt;Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Roles</returns>
        ApiResponse<Roles> RolesGetRolesWithHttpInfo(string type = default(string), int operationIndex = 0);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IRolesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Get Essbase Roles
        /// </summary>
        /// <remarks>
        /// &lt;p&gt;Returns the roles available in Essbase. Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;. If type is empty, then both &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt; roles are returned.&lt;/p&gt; &lt;p&gt;If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.&lt;/p&gt;
        /// </remarks>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="type">&lt;p&gt;Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Roles</returns>
        System.Threading.Tasks.Task<Roles> RolesGetRolesAsync(string type = default(string), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get Essbase Roles
        /// </summary>
        /// <remarks>
        /// &lt;p&gt;Returns the roles available in Essbase. Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;. If type is empty, then both &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt; roles are returned.&lt;/p&gt; &lt;p&gt;If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.&lt;/p&gt;
        /// </remarks>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="type">&lt;p&gt;Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Roles)</returns>
        System.Threading.Tasks.Task<ApiResponse<Roles>> RolesGetRolesWithHttpInfoAsync(string type = default(string), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IRolesApi : IRolesApiSync, IRolesApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class RolesApi : IRolesApi
    {
        private EssSharp.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public RolesApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public RolesApi(string basePath)
        {
            this.Configuration = EssSharp.Client.Configuration.MergeConfigurations(
                EssSharp.Client.GlobalConfiguration.Instance,
                new EssSharp.Client.Configuration { BasePath = basePath }
            );
            this.Client = new EssSharp.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new EssSharp.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = EssSharp.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public RolesApi(EssSharp.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = EssSharp.Client.Configuration.MergeConfigurations(
                EssSharp.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new EssSharp.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new EssSharp.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = EssSharp.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public RolesApi(EssSharp.Client.ISynchronousClient client, EssSharp.Client.IAsynchronousClient asyncClient, EssSharp.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = EssSharp.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public EssSharp.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public EssSharp.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public EssSharp.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public EssSharp.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Get Essbase Roles &lt;p&gt;Returns the roles available in Essbase. Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;. If type is empty, then both &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt; roles are returned.&lt;/p&gt; &lt;p&gt;If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="type">&lt;p&gt;Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>Roles</returns>
        public Roles RolesGetRoles(string type = default(string), int operationIndex = 0)
        {
            EssSharp.Client.ApiResponse<Roles> localVarResponse = RolesGetRolesWithHttpInfo(type);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Essbase Roles &lt;p&gt;Returns the roles available in Essbase. Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;. If type is empty, then both &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt; roles are returned.&lt;/p&gt; &lt;p&gt;If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="type">&lt;p&gt;Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of Roles</returns>
        public EssSharp.Client.ApiResponse<Roles> RolesGetRolesWithHttpInfo(string type = default(string), int operationIndex = 0)
        {
            EssSharp.Client.RequestOptions localVarRequestOptions = new EssSharp.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/xml"
            };

            var localVarContentType = EssSharp.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = EssSharp.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (type != null)
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "type", type));
            }

            localVarRequestOptions.Operation = "RolesApi.RolesGetRoles";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + EssSharp.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Roles>("/roles", localVarRequestOptions, this.Configuration);
            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RolesGetRoles", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get Essbase Roles &lt;p&gt;Returns the roles available in Essbase. Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;. If type is empty, then both &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt; roles are returned.&lt;/p&gt; &lt;p&gt;If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="type">&lt;p&gt;Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Roles</returns>
        public async System.Threading.Tasks.Task<Roles> RolesGetRolesAsync(string type = default(string), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            EssSharp.Client.ApiResponse<Roles> localVarResponse = await RolesGetRolesWithHttpInfoAsync(type, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Essbase Roles &lt;p&gt;Returns the roles available in Essbase. Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;. If type is empty, then both &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt; roles are returned.&lt;/p&gt; &lt;p&gt;If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="type">&lt;p&gt;Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Roles)</returns>
        public async System.Threading.Tasks.Task<EssSharp.Client.ApiResponse<Roles>> RolesGetRolesWithHttpInfoAsync(string type = default(string), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            EssSharp.Client.RequestOptions localVarRequestOptions = new EssSharp.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/xml"
            };

            var localVarContentType = EssSharp.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = EssSharp.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null)
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            if (type != null)
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "type", type));
            }

            localVarRequestOptions.Operation = "RolesApi.RolesGetRoles";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (basicAuth) required
            // http basic authentication required
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + EssSharp.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<Roles>("/roles", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("RolesGetRoles", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
