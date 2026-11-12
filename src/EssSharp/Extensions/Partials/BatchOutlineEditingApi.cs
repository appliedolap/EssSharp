using System;

using EssSharp.Model;

using Newtonsoft.Json.Linq;

namespace EssSharp.Api
{
    /// <summary />
    public partial class BatchOutlineEditingApi
    {
        /// <summary>
        /// Run Batch Outline Edit with the edit actions given as JSON. The generated <see cref="OtlEditMain"/> describes each
        /// edit action as a name and a value, which the server does not accept: it reads each action as an object with one
        /// property, named for the action, such as <c>smartListAdd</c>.
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="application">Application name.</param>
        /// <param name="database">Database name.</param>
        /// <param name="body">The batch outline edit, with its <c>editActions</c> array.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of BOEOutput</returns>
        public async System.Threading.Tasks.Task<BOEOutput> BatchOutlineEditingExecuteJsonAsync( string application, string database, JObject body, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default )
        {
            if (application == null)
                throw new EssSharp.Client.ApiException(400, "Missing required parameter 'application' when calling BatchOutlineEditingApi->BatchOutlineEditingExecute");

            if (database == null)
                throw new EssSharp.Client.ApiException(400, "Missing required parameter 'database' when calling BatchOutlineEditingApi->BatchOutlineEditingExecute");

            if (body == null)
                throw new EssSharp.Client.ApiException(400, "Missing required parameter 'body' when calling BatchOutlineEditingApi->BatchOutlineEditingExecute");

            EssSharp.Client.RequestOptions localVarRequestOptions = new EssSharp.Client.RequestOptions();

            localVarRequestOptions.HeaderParameters.Add("Content-Type", "application/json");
            localVarRequestOptions.HeaderParameters.Add("Accept", "application/json");

            localVarRequestOptions.PathParameters.Add("application", EssSharp.Client.ClientUtils.ParameterToString(application)); // path parameter
            localVarRequestOptions.PathParameters.Add("database", EssSharp.Client.ClientUtils.ParameterToString(database)); // path parameter
            localVarRequestOptions.Data = body;

            localVarRequestOptions.Operation = "BatchOutlineEditingApi.BatchOutlineEditingExecute";
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
            if (!string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization"))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + EssSharp.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<BOEOutput>("/applications/{application}/databases/{database}/boe", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("BatchOutlineEditingExecute", localVarResponse);
                if (_exception != null)
                {
                    throw _exception;
                }
            }

            return localVarResponse.Data;
        }
    }
}
