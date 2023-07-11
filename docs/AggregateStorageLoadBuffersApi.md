# EssSharp.Api.AggregateStorageLoadBuffersApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ASOLoadBuffersCreateBuffer**](AggregateStorageLoadBuffersApi.md#asoloadbufferscreatebuffer) | **POST** /applications/{applicationName}/databases/{databaseName}/asodataload/buffers | Create Buffer |
| [**ASOLoadBuffersDestroyBuffers**](AggregateStorageLoadBuffersApi.md#asoloadbuffersdestroybuffers) | **DELETE** /applications/{applicationName}/databases/{databaseName}/asodataload/buffers | Destroy Dataload Buffer |
| [**ASOLoadBuffersListBuffers**](AggregateStorageLoadBuffersApi.md#asoloadbufferslistbuffers) | **GET** /applications/{applicationName}/databases/{databaseName}/asodataload/buffers | List Buffers |
| [**ASOLoadBuffersMerge**](AggregateStorageLoadBuffersApi.md#asoloadbuffersmerge) | **POST** /applications/{applicationName}/databases/{databaseName}/asodataload/actions/merge | Merge Data |

<a id="asoloadbufferscreatebuffer"></a>
# **ASOLoadBuffersCreateBuffer**
> void ASOLoadBuffersCreateBuffer (string applicationName, string databaseName, DataLoadBuffer body = null)

Create Buffer

<p>Creates an aggregate storage data load buffer with the specified options.</p><p>In REST API, the <i>jobType</i> to load data without use of buffers is <b>dataload</b>. The jobTypes you need to load data incrementally to aggregate storage cubes (using buffers) are <b>asoBufferDataLoad</b> and <b>asoBufferCommit</b>.</p><p>The workflow in REST API to load data to aggregate storage cubes using buffers is:</p><ol><li>Create (initialize) the buffer(s)</li><li>(Optional) List existing buffers</li><li>Run <i>jobType</i> <b>asoBufferDataLoad</b> to load data into the buffer(s)</li><li>Run <i>jobType</i> <b>asoBufferCommit</b> to commit data from the buffer(s) to the aggregate storage cube</li></ol>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ASOLoadBuffersCreateBufferExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new AggregateStorageLoadBuffersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new DataLoadBuffer(); // DataLoadBuffer |  (optional) 

            try
            {
                // Create Buffer
                apiInstance.ASOLoadBuffersCreateBuffer(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AggregateStorageLoadBuffersApi.ASOLoadBuffersCreateBuffer: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ASOLoadBuffersCreateBufferWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Buffer
    apiInstance.ASOLoadBuffersCreateBufferWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AggregateStorageLoadBuffersApi.ASOLoadBuffersCreateBufferWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**DataLoadBuffer**](DataLoadBuffer.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Load buffer created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create load buffer.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="asoloadbuffersdestroybuffers"></a>
# **ASOLoadBuffersDestroyBuffers**
> void ASOLoadBuffersDestroyBuffers (string applicationName, string databaseName, DestroyBuffer body = null)

Destroy Dataload Buffer

<p>Destroys data load buffer with the specified ids.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ASOLoadBuffersDestroyBuffersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new AggregateStorageLoadBuffersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new DestroyBuffer(); // DestroyBuffer | Buffer Ids (optional) 

            try
            {
                // Destroy Dataload Buffer
                apiInstance.ASOLoadBuffersDestroyBuffers(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AggregateStorageLoadBuffersApi.ASOLoadBuffersDestroyBuffers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ASOLoadBuffersDestroyBuffersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Destroy Dataload Buffer
    apiInstance.ASOLoadBuffersDestroyBuffersWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AggregateStorageLoadBuffersApi.ASOLoadBuffersDestroyBuffersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**DestroyBuffer**](DestroyBuffer.md) | Buffer Ids | [optional]  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Load buffers destroyed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to destroy load buffers.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="asoloadbufferslistbuffers"></a>
# **ASOLoadBuffersListBuffers**
> LoadBuffersList ASOLoadBuffersListBuffers (string applicationName, string databaseName)

List Buffers

<p>Lists existing aggregate storage data load buffers. An error is returned if called on a block storage database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ASOLoadBuffersListBuffersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new AggregateStorageLoadBuffersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // List Buffers
                LoadBuffersList result = apiInstance.ASOLoadBuffersListBuffers(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AggregateStorageLoadBuffersApi.ASOLoadBuffersListBuffers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ASOLoadBuffersListBuffersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Buffers
    ApiResponse<LoadBuffersList> response = apiInstance.ASOLoadBuffersListBuffersWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AggregateStorageLoadBuffersApi.ASOLoadBuffersListBuffersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**LoadBuffersList**](LoadBuffersList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Load buffers list returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get load buffers.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="asoloadbuffersmerge"></a>
# **ASOLoadBuffersMerge**
> void ASOLoadBuffersMerge (string applicationName, string databaseName, MergeSilceOption body = null)

Merge Data

<p>Merges aggregate storage incremental data slices.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ASOLoadBuffersMergeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new AggregateStorageLoadBuffersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new MergeSilceOption(); // MergeSilceOption |  (optional) 

            try
            {
                // Merge Data
                apiInstance.ASOLoadBuffersMerge(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AggregateStorageLoadBuffersApi.ASOLoadBuffersMerge: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ASOLoadBuffersMergeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Merge Data
    apiInstance.ASOLoadBuffersMergeWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AggregateStorageLoadBuffersApi.ASOLoadBuffersMergeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**MergeSilceOption**](MergeSilceOption.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Data merged successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to merge data.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

