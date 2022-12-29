# EssSharp.Api.BatchOutlineEditingApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**BatchOutlineEditingExecute**](BatchOutlineEditingApi.md#batchoutlineeditingexecute) | **POST** /applications/{application}/databases/{database}/boe | Run Batch Outline Edit |

<a name="batchoutlineeditingexecute"></a>
# **BatchOutlineEditingExecute**
> BOEOutput BatchOutlineEditingExecute (string application, string database, OtlEditMain body)

Run Batch Outline Edit

<p>Executes batch outline editing process. Based on the XML or JSON body, adds or removes members from the outline in the active cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BatchOutlineEditingExecuteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new BatchOutlineEditingApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var body = new OtlEditMain(); // OtlEditMain | Batch outline JSON/XML

            try
            {
                // Run Batch Outline Edit
                BOEOutput result = apiInstance.BatchOutlineEditingExecute(application, database, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BatchOutlineEditingApi.BatchOutlineEditingExecute: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BatchOutlineEditingExecuteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Run Batch Outline Edit
    ApiResponse<BOEOutput> response = apiInstance.BatchOutlineEditingExecuteWithHttpInfo(application, database, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BatchOutlineEditingApi.BatchOutlineEditingExecuteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**OtlEditMain**](OtlEditMain.md) | Batch outline JSON/XML |  |

### Return type

[**BOEOutput**](BOEOutput.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The batch outline edit completed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to complete batch outline editing. The output may be invalid, the sequence of metadata operations may be incorrect, or saving the outline may have failed.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

