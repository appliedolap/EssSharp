# EssSharp.Api.StreamingDimensionBuildApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DimensionBuildEnd**](StreamingDimensionBuildApi.md#dimensionbuildend) | **DELETE** /applications/{applicationName}/databases/{databaseName}/dimbuild/{streamId} | End Streaming Dimension Build |
| [**DimensionBuildEndDimBuild**](StreamingDimensionBuildApi.md#dimensionbuildenddimbuild) | **DELETE** /applications/{applicationName}/databases/{databaseName}/dimbuild/{streamId}/{ruleFileName} | End Dimension Build |
| [**DimensionBuildStart**](StreamingDimensionBuildApi.md#dimensionbuildstart) | **POST** /applications/{applicationName}/databases/{databaseName}/dimbuild | Start Streaming Dimension Build |
| [**DimensionBuildStartDimBuild**](StreamingDimensionBuildApi.md#dimensionbuildstartdimbuild) | **POST** /applications/{applicationName}/databases/{databaseName}/dimbuild/{streamId}/{ruleFileName} | Start Dimension Build&lt;/p&gt; |
| [**DimensionBuildStreamDimBuildData**](StreamingDimensionBuildApi.md#dimensionbuildstreamdimbuilddata) | **POST** /applications/{applicationName}/databases/{databaseName}/dimbuild/{streamId} | Push Dimensions |

<a id="dimensionbuildend"></a>
# **DimensionBuildEnd**
> StreamProcessEndResponse DimensionBuildEnd (string applicationName, string databaseName, string streamId)

End Streaming Dimension Build

<p>Ends an incremental dimension build and triggers a restructuring of the cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionBuildEndExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new StreamingDimensionBuildApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var streamId = "streamId_example";  // string | <p>Stream ID.</p>

            try
            {
                // End Streaming Dimension Build
                StreamProcessEndResponse result = apiInstance.DimensionBuildEnd(applicationName, databaseName, streamId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StreamingDimensionBuildApi.DimensionBuildEnd: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionBuildEndWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // End Streaming Dimension Build
    ApiResponse<StreamProcessEndResponse> response = apiInstance.DimensionBuildEndWithHttpInfo(applicationName, databaseName, streamId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StreamingDimensionBuildApi.DimensionBuildEndWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **streamId** | **string** | &lt;p&gt;Stream ID.&lt;/p&gt; |  |

### Return type

[**StreamProcessEndResponse**](StreamProcessEndResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Dimension build ended successfully; includes status.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to end dimension build. The stream ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dimensionbuildenddimbuild"></a>
# **DimensionBuildEndDimBuild**
> StreamProcessEndResponse DimensionBuildEndDimBuild (string applicationName, string databaseName, string streamId, string ruleFileName)

End Dimension Build

<p>Ends a dimension build with a rules file.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionBuildEndDimBuildExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new StreamingDimensionBuildApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var streamId = "streamId_example";  // string | <p>Stream ID.</p>
            var ruleFileName = "ruleFileName_example";  // string | <p>Rules file name.</p>

            try
            {
                // End Dimension Build
                StreamProcessEndResponse result = apiInstance.DimensionBuildEndDimBuild(applicationName, databaseName, streamId, ruleFileName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StreamingDimensionBuildApi.DimensionBuildEndDimBuild: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionBuildEndDimBuildWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // End Dimension Build
    ApiResponse<StreamProcessEndResponse> response = apiInstance.DimensionBuildEndDimBuildWithHttpInfo(applicationName, databaseName, streamId, ruleFileName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StreamingDimensionBuildApi.DimensionBuildEndDimBuildWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **streamId** | **string** | &lt;p&gt;Stream ID.&lt;/p&gt; |  |
| **ruleFileName** | **string** | &lt;p&gt;Rules file name.&lt;/p&gt; |  |

### Return type

[**StreamProcessEndResponse**](StreamProcessEndResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Dimension build ended successfully. Returns status. If &lt;code&gt;links&#x3D;true&lt;/code&gt; parameter is passed, also includes links to restructure.&lt;/p&gt; |  -  |
| **400** | Validation failed. For example, specified stream id is invalid or dimension build is not started |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dimensionbuildstart"></a>
# **DimensionBuildStart**
> StreamProcessStartResponse DimensionBuildStart (string applicationName, string databaseName, DimBuildStartPayload body = null)

Start Streaming Dimension Build

<p>Starts an incremental dimension build.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionBuildStartExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new StreamingDimensionBuildApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new DimBuildStartPayload(); // DimBuildStartPayload | <p>Dimension build attributes, such as the restructure option. If empty, the default value for restructure option is  <code>PRESERVE_ALL_DATA</code>. (optional) 

            try
            {
                // Start Streaming Dimension Build
                StreamProcessStartResponse result = apiInstance.DimensionBuildStart(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StreamingDimensionBuildApi.DimensionBuildStart: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionBuildStartWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start Streaming Dimension Build
    ApiResponse<StreamProcessStartResponse> response = apiInstance.DimensionBuildStartWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StreamingDimensionBuildApi.DimensionBuildStartWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**DimBuildStartPayload**](DimBuildStartPayload.md) | &lt;p&gt;Dimension build attributes, such as the restructure option. If empty, the default value for restructure option is  &lt;code&gt;PRESERVE_ALL_DATA&lt;/code&gt;. | [optional]  |

### Return type

[**StreamProcessStartResponse**](StreamProcessStartResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Dimension build started successfully; includes unique stream id which can be passed to subsequent requests. If &lt;code&gt;links&#x3D;true&lt;/code&gt; parameter is passed, also includes links to start dimension build with rules file.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dimensionbuildstartdimbuild"></a>
# **DimensionBuildStartDimBuild**
> StreamProcessStartResponse DimensionBuildStartDimBuild (string applicationName, string databaseName, string streamId, string ruleFileName)

Start Dimension Build</p>

<p>Starts a dimension build with a rule file.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionBuildStartDimBuildExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new StreamingDimensionBuildApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var streamId = "streamId_example";  // string | <p>Stream ID.</p>
            var ruleFileName = "ruleFileName_example";  // string | <p>Rule File Name.</p>

            try
            {
                // Start Dimension Build</p>
                StreamProcessStartResponse result = apiInstance.DimensionBuildStartDimBuild(applicationName, databaseName, streamId, ruleFileName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StreamingDimensionBuildApi.DimensionBuildStartDimBuild: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionBuildStartDimBuildWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start Dimension Build</p>
    ApiResponse<StreamProcessStartResponse> response = apiInstance.DimensionBuildStartDimBuildWithHttpInfo(applicationName, databaseName, streamId, ruleFileName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StreamingDimensionBuildApi.DimensionBuildStartDimBuildWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **streamId** | **string** | &lt;p&gt;Stream ID.&lt;/p&gt; |  |
| **ruleFileName** | **string** | &lt;p&gt;Rule File Name.&lt;/p&gt; |  |

### Return type

[**StreamProcessStartResponse**](StreamProcessStartResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Dimension build started successfully. If &lt;code&gt;links&#x3D;true&lt;/code&gt; parameter is passed, also includes links to push more data.&lt;/p&gt; |  -  |
| **400** | Validation failed. For example, specified stream id or rule file name is invalid |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dimensionbuildstreamdimbuilddata"></a>
# **DimensionBuildStreamDimBuildData**
> StreamProcessStartResponse DimensionBuildStreamDimBuildData (string applicationName, string databaseName, string streamId, string body = null)

Push Dimensions

<p>Pushes data for streaming dimension build.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionBuildStreamDimBuildDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new StreamingDimensionBuildApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var streamId = "streamId_example";  // string | <p>Stream ID.</p>
            var body = "body_example";  // string | <p>CSV data.</p> (optional) 

            try
            {
                // Push Dimensions
                StreamProcessStartResponse result = apiInstance.DimensionBuildStreamDimBuildData(applicationName, databaseName, streamId, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StreamingDimensionBuildApi.DimensionBuildStreamDimBuildData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionBuildStreamDimBuildDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Push Dimensions
    ApiResponse<StreamProcessStartResponse> response = apiInstance.DimensionBuildStreamDimBuildDataWithHttpInfo(applicationName, databaseName, streamId, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StreamingDimensionBuildApi.DimensionBuildStreamDimBuildDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **streamId** | **string** | &lt;p&gt;Stream ID.&lt;/p&gt; |  |
| **body** | **string** | &lt;p&gt;CSV data.&lt;/p&gt; | [optional]  |

### Return type

[**StreamProcessStartResponse**](StreamProcessStartResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: text/plain, text/csv
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Data pushed successfully; includes links to push more data and end dimension build if &lt;code&gt;links&#x3D;true&lt;/code&gt; parameter is passed.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to push data. The stream ID may be invalid, or the dimension build may not have started.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

