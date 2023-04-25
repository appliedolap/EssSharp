using System;

namespace EssSharp.Api
{
    public partial class FilesApi
    {
        /// <summary>
        /// List or Download Files &lt;p&gt;Returns a list of files, or downloads the specified file. To list files, use &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; for the Accept header. To download, use &lt;code&gt;Accept&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt; for the Accept header.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="path">&lt;p&gt;Catalog path, starting with &lt;code&gt;applications&lt;/code&gt;, &lt;code&gt;shared&lt;/code&gt;, or &lt;code&gt;users&lt;/code&gt;. If listing files, this is the folder path. If downloading files, this is the file path.&lt;/p&gt;&lt;p&gt;Examples:&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;applications/sample&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;shared&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;users/ksmith&lt;/code&gt;&lt;/li&gt;&lt;/ul&gt;</param>
        /// <param name="offset">&lt;p&gt;Number of items to omit from the start of the result set. Default value is 0. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="limit">&lt;p&gt;Maximum number of files to return. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="type">&lt;p&gt;List files by type. If type is not specified, returns all files. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="overwrite">&lt;p&gt;If true, overwrite files. If false, any existing file is validated but not overwritten. Applicable only with query parameters  &lt;code&gt;action&#x3D;validateUpload&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt; . Default value is false.&lt;/p&gt; (optional, default to false)</param>
        /// <param name="action">&lt;p&gt;Validates the upload. Supported action values are &lt;code&gt;validateUpload&lt;/code&gt; and &lt;code&gt;&#39;Accept&#x3D;application/json&#39;&lt;/code&gt; or &lt;code&gt;&#39;Accept&#x3D;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="fileSize">&lt;p&gt;Validates whether enough free space is available. Applicable only with query parameters &lt;code&gt;action&#x3D;&#39;validateUpload&#39;&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="filter">&lt;p&gt;Filter the list of files.&lt;/p&gt; (optional)</param>
        /// <param name="recursive">&lt;p&gt;Recursive param to get search result as recursive.&lt;/p&gt; (optional, default to false)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>System.IO.Stream</returns>
        public System.IO.Stream FilesDownloadFile( string path, int? offset = default(int?), int? limit = default(int?), string type = default(string), bool? overwrite = default(bool?), string action = default(string), long? fileSize = default(long?), string filter = default(string), bool? recursive = default(bool?), int operationIndex = 0 )
        {
            EssSharp.Client.ApiResponse<System.IO.Stream> localVarResponse = FilesDownloadFileWithHttpInfo(path, offset, limit, type, overwrite, action, fileSize, filter, recursive);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List or Download Files &lt;p&gt;Returns a list of files, or downloads the specified file. To list files, use &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; for the Accept header. To download, use &lt;code&gt;Accept&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt; for the Accept header.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="path">&lt;p&gt;Catalog path, starting with &lt;code&gt;applications&lt;/code&gt;, &lt;code&gt;shared&lt;/code&gt;, or &lt;code&gt;users&lt;/code&gt;. If listing files, this is the folder path. If downloading files, this is the file path.&lt;/p&gt;&lt;p&gt;Examples:&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;applications/sample&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;shared&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;users/ksmith&lt;/code&gt;&lt;/li&gt;&lt;/ul&gt;</param>
        /// <param name="offset">&lt;p&gt;Number of items to omit from the start of the result set. Default value is 0. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="limit">&lt;p&gt;Maximum number of files to return. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="type">&lt;p&gt;List files by type. If type is not specified, returns all files. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="overwrite">&lt;p&gt;If true, overwrite files. If false, any existing file is validated but not overwritten. Applicable only with query parameters  &lt;code&gt;action&#x3D;validateUpload&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt; . Default value is false.&lt;/p&gt; (optional, default to false)</param>
        /// <param name="action">&lt;p&gt;Validates the upload. Supported action values are &lt;code&gt;validateUpload&lt;/code&gt; and &lt;code&gt;&#39;Accept&#x3D;application/json&#39;&lt;/code&gt; or &lt;code&gt;&#39;Accept&#x3D;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="fileSize">&lt;p&gt;Validates whether enough free space is available. Applicable only with query parameters &lt;code&gt;action&#x3D;&#39;validateUpload&#39;&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="filter">&lt;p&gt;Filter the list of files.&lt;/p&gt; (optional)</param>
        /// <param name="recursive">&lt;p&gt;Recursive param to get search result as recursive.&lt;/p&gt; (optional, default to false)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <returns>ApiResponse of System.IO.Stream</returns>
        public EssSharp.Client.ApiResponse<System.IO.Stream> FilesDownloadFileWithHttpInfo( string path, int? offset = default(int?), int? limit = default(int?), string type = default(string), bool? overwrite = default(bool?), string action = default(string), long? fileSize = default(long?), string filter = default(string), bool? recursive = default(bool?), int operationIndex = 0 )
        {
            // verify the required parameter 'path' is set
            if ( path == null )
            {
                throw new EssSharp.Client.ApiException(400, "Missing required parameter 'path' when calling FilesApi->FilesListFiles");
            }

            EssSharp.Client.RequestOptions localVarRequestOptions = new EssSharp.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/octet-stream"
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

            localVarRequestOptions.PathParameters.Add("path", EssSharp.Client.ClientUtils.ParameterToString(path)); // path parameter
            if ( offset != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "offset", offset));
            }
            if ( limit != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "limit", limit));
            }
            if ( type != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "type", type));
            }
            if ( overwrite != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "overwrite", overwrite));
            }
            if ( action != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "action", action));
            }
            if ( fileSize != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "fileSize", fileSize));
            }
            if ( filter != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "filter", filter));
            }
            if ( recursive != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "recursive", recursive));
            }

            localVarRequestOptions.Operation = "FilesApi.FilesListFiles";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (basicAuth) required
            // http basic authentication required
            if ( !string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization") )
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + EssSharp.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<System.IO.Stream>("/files/{path}", localVarRequestOptions, this.Configuration);
            if ( this.ExceptionFactory != null )
            {
                Exception _exception = this.ExceptionFactory("FilesListFiles", localVarResponse);
                if ( _exception != null )
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

        /// <summary>
        /// List or Download Files &lt;p&gt;Returns a list of files, or downloads the specified file. To list files, use &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; for the Accept header. To download, use &lt;code&gt;Accept&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt; for the Accept header.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="path">&lt;p&gt;Catalog path, starting with &lt;code&gt;applications&lt;/code&gt;, &lt;code&gt;shared&lt;/code&gt;, or &lt;code&gt;users&lt;/code&gt;. If listing files, this is the folder path. If downloading files, this is the file path.&lt;/p&gt;&lt;p&gt;Examples:&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;applications/sample&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;shared&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;users/ksmith&lt;/code&gt;&lt;/li&gt;&lt;/ul&gt;</param>
        /// <param name="offset">&lt;p&gt;Number of items to omit from the start of the result set. Default value is 0. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="limit">&lt;p&gt;Maximum number of files to return. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="type">&lt;p&gt;List files by type. If type is not specified, returns all files. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="overwrite">&lt;p&gt;If true, overwrite files. If false, any existing file is validated but not overwritten. Applicable only with query parameters  &lt;code&gt;action&#x3D;validateUpload&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt; . Default value is false.&lt;/p&gt; (optional, default to false)</param>
        /// <param name="action">&lt;p&gt;Validates the upload. Supported action values are &lt;code&gt;validateUpload&lt;/code&gt; and &lt;code&gt;&#39;Accept&#x3D;application/json&#39;&lt;/code&gt; or &lt;code&gt;&#39;Accept&#x3D;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="fileSize">&lt;p&gt;Validates whether enough free space is available. Applicable only with query parameters &lt;code&gt;action&#x3D;&#39;validateUpload&#39;&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="filter">&lt;p&gt;Filter the list of files.&lt;/p&gt; (optional)</param>
        /// <param name="recursive">&lt;p&gt;Recursive param to get search result as recursive.&lt;/p&gt; (optional, default to false)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of System.IO.Stream</returns>
        public async System.Threading.Tasks.Task<System.IO.Stream> DownloadFileAsync( string path, int? offset = default(int?), int? limit = default(int?), string type = default(string), bool? overwrite = default(bool?), string action = default(string), long? fileSize = default(long?), string filter = default(string), bool? recursive = default(bool?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken) )
        {
            EssSharp.Client.ApiResponse<System.IO.Stream> localVarResponse = await DownloadFileWithHttpInfoAsync(path, offset, limit, type, overwrite, action, fileSize, filter, recursive, operationIndex, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// List or Download Files &lt;p&gt;Returns a list of files, or downloads the specified file. To list files, use &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; for the Accept header. To download, use &lt;code&gt;Accept&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt; for the Accept header.&lt;/p&gt;
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="path">&lt;p&gt;Catalog path, starting with &lt;code&gt;applications&lt;/code&gt;, &lt;code&gt;shared&lt;/code&gt;, or &lt;code&gt;users&lt;/code&gt;. If listing files, this is the folder path. If downloading files, this is the file path.&lt;/p&gt;&lt;p&gt;Examples:&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;applications/sample&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;shared&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;users/ksmith&lt;/code&gt;&lt;/li&gt;&lt;/ul&gt;</param>
        /// <param name="offset">&lt;p&gt;Number of items to omit from the start of the result set. Default value is 0. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="limit">&lt;p&gt;Maximum number of files to return. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="type">&lt;p&gt;List files by type. If type is not specified, returns all files. Applicable only for listing files.&lt;/p&gt; (optional)</param>
        /// <param name="overwrite">&lt;p&gt;If true, overwrite files. If false, any existing file is validated but not overwritten. Applicable only with query parameters  &lt;code&gt;action&#x3D;validateUpload&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt; . Default value is false.&lt;/p&gt; (optional, default to false)</param>
        /// <param name="action">&lt;p&gt;Validates the upload. Supported action values are &lt;code&gt;validateUpload&lt;/code&gt; and &lt;code&gt;&#39;Accept&#x3D;application/json&#39;&lt;/code&gt; or &lt;code&gt;&#39;Accept&#x3D;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="fileSize">&lt;p&gt;Validates whether enough free space is available. Applicable only with query parameters &lt;code&gt;action&#x3D;&#39;validateUpload&#39;&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; (optional)</param>
        /// <param name="filter">&lt;p&gt;Filter the list of files.&lt;/p&gt; (optional)</param>
        /// <param name="recursive">&lt;p&gt;Recursive param to get search result as recursive.&lt;/p&gt; (optional, default to false)</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (System.IO.Stream)</returns>
        public async System.Threading.Tasks.Task<EssSharp.Client.ApiResponse<System.IO.Stream>> DownloadFileWithHttpInfoAsync( string path, int? offset = default(int?), int? limit = default(int?), string type = default(string), bool? overwrite = default(bool?), string action = default(string), long? fileSize = default(long?), string filter = default(string), bool? recursive = default(bool?), int operationIndex = 0, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken) )
        {
            // verify the required parameter 'path' is set
            if ( path == null )
            {
                throw new EssSharp.Client.ApiException(400, "Missing required parameter 'path' when calling FilesApi->FilesListFiles");
            }


            EssSharp.Client.RequestOptions localVarRequestOptions = new EssSharp.Client.RequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/octet-stream"
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

            localVarRequestOptions.PathParameters.Add("path", EssSharp.Client.ClientUtils.ParameterToString(path)); // path parameter
            if ( offset != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "offset", offset));
            }
            if ( limit != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "limit", limit));
            }
            if ( type != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "type", type));
            }
            if ( overwrite != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "overwrite", overwrite));
            }
            if ( action != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "action", action));
            }
            if ( fileSize != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "fileSize", fileSize));
            }
            if ( filter != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "filter", filter));
            }
            if ( recursive != null )
            {
                localVarRequestOptions.QueryParameters.Add(EssSharp.Client.ClientUtils.ParameterToMultiMap("", "recursive", recursive));
            }

            localVarRequestOptions.Operation = "FilesApi.FilesListFiles";
            localVarRequestOptions.OperationIndex = operationIndex;

            // authentication (basicAuth) required
            // http basic authentication required
            if ( !string.IsNullOrEmpty(this.Configuration.Username) || !string.IsNullOrEmpty(this.Configuration.Password) && !localVarRequestOptions.HeaderParameters.ContainsKey("Authorization") )
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Basic " + EssSharp.Client.ClientUtils.Base64Encode(this.Configuration.Username + ":" + this.Configuration.Password));
            }

            // make the HTTP request
            var localVarResponse = await this.AsynchronousClient.GetAsync<System.IO.Stream>("/files/{path}", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if ( this.ExceptionFactory != null )
            {
                Exception _exception = this.ExceptionFactory("FilesListFiles", localVarResponse);
                if ( _exception != null )
                {
                    throw _exception;
                }
            }

            return localVarResponse;
        }

    }
}
