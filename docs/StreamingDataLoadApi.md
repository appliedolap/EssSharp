# EssSharp.Api.StreamingDataLoadApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DataloadEnd**](StreamingDataLoadApi.md#dataloadend) | **DELETE** /applications/{applicationName}/databases/{databaseName}/dataload/{streamId} | End Data Load |
| [**DataloadStart**](StreamingDataLoadApi.md#dataloadstart) | **POST** /applications/{applicationName}/databases/{databaseName}/dataload | Start Data Load |
| [**DataloadStreamData**](StreamingDataLoadApi.md#dataloadstreamdata) | **POST** /applications/{applicationName}/databases/{databaseName}/dataload/{streamId} | Push Data |

<a id="dataloadend"></a>
# **DataloadEnd**
> StreamProcessEndResponse DataloadEnd (string applicationName, string databaseName, string streamId)

End Data Load

<p>Ends streaming data load.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DataloadEndExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new StreamingDataLoadApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var streamId = "streamId_example";  // string | <p>Stream ID.</p>

            try
            {
                // End Data Load
                StreamProcessEndResponse result = apiInstance.DataloadEnd(applicationName, databaseName, streamId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StreamingDataLoadApi.DataloadEnd: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DataloadEndWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // End Data Load
    ApiResponse<StreamProcessEndResponse> response = apiInstance.DataloadEndWithHttpInfo(applicationName, databaseName, streamId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StreamingDataLoadApi.DataloadEndWithHttpInfo: " + e.Message);
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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Data load ended successfully; includes status.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to end data load. The stream ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dataloadstart"></a>
# **DataloadStart**
> StreamProcessStartResponse DataloadStart (string applicationName, string databaseName, DataLoadStartPayload body = null)

Start Data Load

<p>Starts streaming data load.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DataloadStartExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new StreamingDataLoadApi(config);
            var applicationName = "applicationName_example";  // string | Application name
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new DataLoadStartPayload(); // DataLoadStartPayload | <p>Data load options such as rule file name and delimiter.</p> (optional) 

            try
            {
                // Start Data Load
                StreamProcessStartResponse result = apiInstance.DataloadStart(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StreamingDataLoadApi.DataloadStart: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DataloadStartWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start Data Load
    ApiResponse<StreamProcessStartResponse> response = apiInstance.DataloadStartWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StreamingDataLoadApi.DataloadStartWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | Application name |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**DataLoadStartPayload**](DataLoadStartPayload.md) | &lt;p&gt;Data load options such as rule file name and delimiter.&lt;/p&gt; | [optional]  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Data load started successfully; includes unique stream id which can be passed to subsequent requests. If &lt;code&gt;links&#x3D;true&lt;/code&gt; parameter is passed, also includes links to push more data and end the data load.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dataloadstreamdata"></a>
# **DataloadStreamData**
> StreamProcessStartResponse DataloadStreamData (string applicationName, string databaseName, string streamId, string body = null)

Push Data

<p>Pushes data for data load. Data can be pushed in chunks in CSV format by repeating this request multiple times.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DataloadStreamDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new StreamingDataLoadApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var streamId = "streamId_example";  // string | <p>Stream ID.</p>
            var body = "body_example";  // string | <p>CSV data.</p> (optional) 

            try
            {
                // Push Data
                StreamProcessStartResponse result = apiInstance.DataloadStreamData(applicationName, databaseName, streamId, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling StreamingDataLoadApi.DataloadStreamData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DataloadStreamDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Push Data
    ApiResponse<StreamProcessStartResponse> response = apiInstance.DataloadStreamDataWithHttpInfo(applicationName, databaseName, streamId, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling StreamingDataLoadApi.DataloadStreamDataWithHttpInfo: " + e.Message);
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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Data pushed successfully; includes links to push more data and end data load if  &lt;code&gt;links&#x3D;true&lt;/code&gt; parameter is passed.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to push data. The stream ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

