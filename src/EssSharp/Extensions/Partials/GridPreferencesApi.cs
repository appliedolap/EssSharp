using System;
using System.Net;

using EssSharp.Model;

namespace EssSharp.Api
{
    /// <summary />
    public partial class GridPreferencesApi
    {
        /// <summary>
        /// Set Grid Preferences &lt;p&gt;Sets the grid preferences for the current session.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">&lt;p&gt;Grid preferences to be stored in the session.&lt;/p&gt;</param>
        /// <param name="cookie">The session cookie holding the JSESSIONID for which to set the preferences.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse</returns>
        public async System.Threading.Tasks.Task<EssSharp.Client.ApiResponse<Object>> GridPreferencesSetForSessionAsync( Preferences body, Cookie cookie, int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken) )
        {
            // verify the required parameter 'body' is set
            if ( body == null )
            {
                throw new EssSharp.Client.ApiException(400, "Missing required parameter 'body' when calling GridPreferencesApi->GridPreferencesSet");
            }

            EssSharp.Client.RequestOptions localVarRequestOptions = new EssSharp.Client.RequestOptions();

            string[] _contentTypes = new string[] {
                "application/json"
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
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

            localVarRequestOptions.Data = body;

            localVarRequestOptions.Operation = "GridPreferencesApi.GridPreferencesSet";
            localVarRequestOptions.OperationIndex = operationIndex;

            // If a session cookie was given, add it.
            if ( cookie is not null )
            {
                localVarRequestOptions.Cookies.Add(cookie);
            }
            // Otherwise, use http basic authentication
            else if ( !string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization") )
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + EssSharp.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }
            else
            {
                cookie = cookie;
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.PutAsync<Object>("/preferences/grid", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if ( this.ExceptionFactory != null )
            {
                Exception _exception = this.ExceptionFactory("GridPreferencesSet", localVarResponse);
                if ( _exception != null )
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }
    }
}
