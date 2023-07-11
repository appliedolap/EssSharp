# EssSharp.Api.ExecuteMDXApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**MDXExecuteMDX**](ExecuteMDXApi.md#mdxexecutemdx) | **POST** /applications/{application}/databases/{database}/mdx | Run MDX Query |
| [**MDXExecutenq**](ExecuteMDXApi.md#mdxexecutenq) | **GET** /applications/{application}/databases/{database}/mdx/{name} | Run MDX Report |

<a id="mdxexecutemdx"></a>
# **MDXExecuteMDX**
> Object MDXExecuteMDX (string application, string database, string format = null, MDXInput body = null)

Run MDX Query

<p>Runs an MDX query, returning the results in the selected format (JSON, HTML, Excel, or CSV).</p> <p>Results are an MDX output set in the requested format (the default is JSON). The output set contains metadata (including page, column, and row tuples) followed by data (a tuple for each row).</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class MDXExecuteMDXExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ExecuteMDXApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var format = "XLSX";  // string | <p>Result format.</p> (optional)  (default to JSON)
            var body = new MDXInput(); // MDXInput | <p>MDX query and preferences.</p> (optional) 

            try
            {
                // Run MDX Query
                Object result = apiInstance.MDXExecuteMDX(application, database, format, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ExecuteMDXApi.MDXExecuteMDX: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MDXExecuteMDXWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Run MDX Query
    ApiResponse<Object> response = apiInstance.MDXExecuteMDXWithHttpInfo(application, database, format, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ExecuteMDXApi.MDXExecuteMDXWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **format** | **string** | &lt;p&gt;Result format.&lt;/p&gt; | [optional] [default to JSON] |
| **body** | [**MDXInput**](MDXInput.md) | &lt;p&gt;MDX query and preferences.&lt;/p&gt; | [optional]  |

### Return type

**Object**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/octet-stream, text/html


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;Mostly OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;As this is a streaming API, it can fail even with status 200. Check for an &lt;code&gt;errorMessage&lt;/code&gt; tag in the response to identify any errors.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the data in the required format.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="mdxexecutenq"></a>
# **MDXExecutenq**
> Object MDXExecutenq (string application, string database, string name, string format = null)

Run MDX Report

<p>Runs an MDX report, returning the results in the selected format (JSON, HTML, Excel, or CSV). An MDX report is saved in the cube context.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class MDXExecutenqExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ExecuteMDXApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var name = "name_example";  // string | <p>MDX report name.</p>
            var format = "XLSX";  // string | <p>Result format.</p> (optional)  (default to JSON)

            try
            {
                // Run MDX Report
                Object result = apiInstance.MDXExecutenq(application, database, name, format);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ExecuteMDXApi.MDXExecutenq: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the MDXExecutenqWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Run MDX Report
    ApiResponse<Object> response = apiInstance.MDXExecutenqWithHttpInfo(application, database, name, format);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ExecuteMDXApi.MDXExecutenqWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **name** | **string** | &lt;p&gt;MDX report name.&lt;/p&gt; |  |
| **format** | **string** | &lt;p&gt;Result format.&lt;/p&gt; | [optional] [default to JSON] |

### Return type

**Object**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;Mostly OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;As this is a streaming API, it can fail even with status 200. Check for an &lt;code&gt;errorMessage&lt;/code&gt; tag in the response to identify any errors.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the data in the required format.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

