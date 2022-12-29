# EssSharp.Api.ASOLoadBuffersApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ASOLoadBuffersCreateBuffer**](ASOLoadBuffersApi.md#asoloadbufferscreatebuffer) | **POST** /applications/{applicationName}/databases/{databaseName}/asodataload/buffers | Create Buffer |
| [**ASOLoadBuffersDestroyBuffers**](ASOLoadBuffersApi.md#asoloadbuffersdestroybuffers) | **DELETE** /applications/{applicationName}/databases/{databaseName}/asodataload/buffers | Destroy Dataload Buffer |
| [**ASOLoadBuffersListBuffers**](ASOLoadBuffersApi.md#asoloadbufferslistbuffers) | **GET** /applications/{applicationName}/databases/{databaseName}/asodataload/buffers | List Buffers |
| [**ASOLoadBuffersMerge**](ASOLoadBuffersApi.md#asoloadbuffersmerge) | **POST** /applications/{applicationName}/databases/{databaseName}/asodataload/actions/merge | Merge Data |

<a name="asoloadbufferscreatebuffer"></a>
# **ASOLoadBuffersCreateBuffer**
> void ASOLoadBuffersCreateBuffer (string applicationName, string databaseName, DataLoadBuffer body = null)

Create Buffer

<p>Creates an aggregate storage data load buffer with the specified options.</p>

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

            var apiInstance = new ASOLoadBuffersApi(config);
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
                Debug.Print("Exception when calling ASOLoadBuffersApi.ASOLoadBuffersCreateBuffer: " + e.Message);
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
    Debug.Print("Exception when calling ASOLoadBuffersApi.ASOLoadBuffersCreateBufferWithHttpInfo: " + e.Message);
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

<a name="asoloadbuffersdestroybuffers"></a>
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

            var apiInstance = new ASOLoadBuffersApi(config);
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
                Debug.Print("Exception when calling ASOLoadBuffersApi.ASOLoadBuffersDestroyBuffers: " + e.Message);
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
    Debug.Print("Exception when calling ASOLoadBuffersApi.ASOLoadBuffersDestroyBuffersWithHttpInfo: " + e.Message);
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

<a name="asoloadbufferslistbuffers"></a>
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

            var apiInstance = new ASOLoadBuffersApi(config);
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
                Debug.Print("Exception when calling ASOLoadBuffersApi.ASOLoadBuffersListBuffers: " + e.Message);
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
    Debug.Print("Exception when calling ASOLoadBuffersApi.ASOLoadBuffersListBuffersWithHttpInfo: " + e.Message);
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

<a name="asoloadbuffersmerge"></a>
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

            var apiInstance = new ASOLoadBuffersApi(config);
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
                Debug.Print("Exception when calling ASOLoadBuffersApi.ASOLoadBuffersMerge: " + e.Message);
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
    Debug.Print("Exception when calling ASOLoadBuffersApi.ASOLoadBuffersMergeWithHttpInfo: " + e.Message);
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

