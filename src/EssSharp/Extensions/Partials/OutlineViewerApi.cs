using System;
using System.Collections.Generic;
using EssSharp.Model;

namespace EssSharp.Api
{
    /// <summary />
    public partial class OutlineViewerApi
    {
        /// <summary>
        /// Get Member Info &lt;p&gt;Returns either all member properties, or requested member properties.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="app">&lt;p&gt;Application name.&lt;/p&gt;</param>
        /// <param name="cube">&lt;p&gt;Database name.&lt;/p&gt;</param>
        /// <param name="memberUniqueName">&lt;p&gt;Unique member name (fully qualified name). Can be a member name, a member ID, or an alias. If the member name is non unique (in a duplicate member enabled outline), use a fully qualified member name or use the member ID.&lt;/p&gt;</param>
        /// <param name="connection">&lt;p&gt;Essbase connection name.&lt;/p&gt; (optional)</param>
        /// <param name="applicationNameForConnection">&lt;p&gt;Application name for connection.&lt;/p&gt; (optional)</param>
        /// <param name="fields">&lt;p&gt;Comma-separated list of member properties to fetch.&lt;/p&gt; (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of MemberBean</returns>
        public async System.Threading.Tasks.Task<List<MemberBean>> OutlineGetDynamicTimeSeriesMemberInfoAsync( string app, string cube, string fields = default(string), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken) )
        {
            EssSharp.Client.ApiResponse<List<MemberBean>> localVarResponse = await OutlineGetDynamicTimeSeriesMemberInfoWithHttpInfoAsync(app, cube, fields, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get Member Info &lt;p&gt;Returns either all member properties, or requested member properties.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="app">&lt;p&gt;Application name.&lt;/p&gt;</param>
        /// <param name="cube">&lt;p&gt;Database name.&lt;/p&gt;</param>
        /// <param name="memberUniqueName">&lt;p&gt;Unique member name (fully qualified name). Can be a member name, a member ID, or an alias. If the member name is non unique (in a duplicate member enabled outline), use a fully qualified member name or use the member ID.&lt;/p&gt;</param>
        /// <param name="connection">&lt;p&gt;Essbase connection name.&lt;/p&gt; (optional)</param>
        /// <param name="applicationNameForConnection">&lt;p&gt;Application name for connection.&lt;/p&gt; (optional)</param>
        /// <param name="fields">&lt;p&gt;Comma-separated list of member properties to fetch.&lt;/p&gt; (optional)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (MemberBean)</returns>
        public async System.Threading.Tasks.Task<EssSharp.Client.ApiResponse<List<MemberBean>>> OutlineGetDynamicTimeSeriesMemberInfoWithHttpInfoAsync( string app, string cube, string fields = default(string), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken) )
        {
            // verify the required parameter 'app' is set
            if ( app == null )
            {
                throw new EssSharp.Client.ApiException(400, "Missing required parameter 'app' when calling OutlineViewerApi->OutlineGetMemberInfo");
            }

            // verify the required parameter 'cube' is set
            if ( cube == null )
            {
                throw new EssSharp.Client.ApiException(400, "Missing required parameter 'cube' when calling OutlineViewerApi->OutlineGetMemberInfo");
            }

            EssSharp.Client.RequestOptions localVarRequestOptions = new EssSharp.Client.RequestOptions();

            string[] _contentTypes = new string[] {
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

            localVarRequestOptions.PathParameters.Add("app", EssSharp.Client.ClientUtils.ParameterToString(app)); // path parameter
            localVarRequestOptions.PathParameters.Add("cube", EssSharp.Client.ClientUtils.ParameterToString(cube)); // path parameter
            localVarRequestOptions.PathParameters.Add("memberUniqueName", "memberSelection"); // path parameter
            localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "queryType", "DTSMEMBERS")); // Query parameter
            localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "queryOptions", "MEMBERSANDALIASES")); // Query parameter
            if ( fields != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "fields", fields));
            }

            localVarRequestOptions.Operation = "OutlineViewerApi.OutlineGetMemberInfo";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (basicAuth) required
            // http basic authentication required
            if ( !string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization") )
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + EssSharp.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<List < MemberBean >>("/outline/{app}/{cube}/{memberUniqueName}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if ( this.ExceptionFactory != null )
            {
                Exception _exception = this.ExceptionFactory("OutlineGetMemberInfo", localVarResponse);
                if ( _exception != null )
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }
    }
}