namespace EssSharp.Api
{
    /// <summary />
    public partial class AboutEssbaseApi
    {
        /// <summary>
        /// Get Specification &lt;p&gt;Returns the OpenAPI (or Swagger) document the server serves to describe its own REST API.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (the specification as JSON text)</returns>
        public async System.Threading.Tasks.Task<EssSharp.Client.ApiResponse<string>> AboutGetSpecificationWithHttpInfoAsync( int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default )
        {
            EssSharp.Client.RequestOptions localVarRequestOptions = new EssSharp.Client.RequestOptions();

            localVarRequestOptions.HeaderParameters.Add("Accept", "application/json");

            localVarRequestOptions.Operation = "AboutEssbaseApi.AboutGetSpecification";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (OAuth2) required
            // oauth required
            if (!localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                if (!string.IsNullOrEmpty(this.Configuration.AccessToken))
                {
                    localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
                }
                else if (!string.IsNullOrEmpty(this.Configuration.OAuthTokenUrl) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientId) &&
                         !string.IsNullOrEmpty(this.Configuration.OAuthClientSecret) &&
                         this.Configuration.OAuthFlow != null)
                {
                    localVarRequestOptions.OAuth = true;
                }
            }
            // authentication (basicAuth) required
            // http basic authentication required
            if ((!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password)) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + EssSharp.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<string>("/openapi.json", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                System.Exception _exception = this.ExceptionFactory("AboutGetSpecification", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }
    }
}
