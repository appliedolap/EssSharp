using System;
using System.Net;

using EssSharp.Model;

namespace EssSharp.Api
{
    /// <summary />
    public partial class GridApi
    {
        /// <summary>
        /// Execute Grid Operation &lt;p&gt;Returns the grid for specified operation. Supported grid operations are Zoom In (zoomin), Zoom Out (zoomout), Refresh (refresh), Keep Only (keeponly), Remove Only (removeonly), Submit (submit), Pivot (pivot), and Pivot To POV (pivotToPOV).&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="applicationName">&lt;p&gt;Application name for grid operation.&lt;/p&gt;</param>
        /// <param name="databaseName">&lt;p&gt;Database/Cube name for grid operation.&lt;/p&gt;</param>
        /// <param name="body">&lt;p&gt;Grid Operation to be performed.&lt;/p&gt;</param>
        /// <param name="preferences">&lt;p&gt;Preferences with which to perform the operation.&lt;/p&gt;</param></param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Grid)</returns>
        public async System.Threading.Tasks.Task<Grid> GridExecuteAsync( string applicationName, string databaseName, GridOperation body, object preferences, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken) )
        {
            body ??= default(GridOperation);

            // verify the required parameter 'applicationName' is set
            if ( applicationName == null )
            {
                throw new EssSharp.Client.ApiException(400, "Missing required parameter 'applicationName' when calling GridApi->GridExecute");
            }

            // verify the required parameter 'databaseName' is set
            if ( databaseName == null )
            {
                throw new EssSharp.Client.ApiException(400, "Missing required parameter 'databaseName' when calling GridApi->GridExecute");
            }

            EssSharp.Client.RequestOptions localVarRequestOptions = new EssSharp.Client.RequestOptions();

            // Assign any preferences that were given.
            localVarRequestOptions.Preferences = preferences;

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json",
                "application/xml"
            };

            var localVarContentType = EssSharp.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if ( localVarContentType != null )
            {
                localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);
            }

            var localVarAccept = EssSharp.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if ( localVarAccept != null )
            {
                localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);
            }

            localVarRequestOptions.PathParameters.Add("applicationName", EssSharp.Client.ClientUtils.ParameterToString(applicationName)); // path parameter
            localVarRequestOptions.PathParameters.Add("databaseName", EssSharp.Client.ClientUtils.ParameterToString(databaseName)); // path parameter
            localVarRequestOptions.Data = body;

            localVarRequestOptions.Operation = "GridApi.GridExecute";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (basicAuth) required
            // http basic authentication required
            if ( !string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization") )
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + EssSharp.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PostAsync<Grid>("/applications/{applicationName}/databases/{databaseName}/grid", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if ( this.ExceptionFactory != null )
            {
                Exception _exception = this.ExceptionFactory("GridExecute", localVarResponse);
                if ( _exception != null )
                {
                    throw _exception;
                }
            }

            return localVarResponse?.Data;
        }

    }
}
