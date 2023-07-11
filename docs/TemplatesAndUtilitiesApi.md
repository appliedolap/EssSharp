# EssSharp.Api.TemplatesAndUtilitiesApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ResourcesDownloadUtility**](TemplatesAndUtilitiesApi.md#resourcesdownloadutility) | **GET** /utilities/{id} | Download Utility |
| [**ResourcesGetUtilities**](TemplatesAndUtilitiesApi.md#resourcesgetutilities) | **GET** /utilities | List Utilities |

<a id="resourcesdownloadutility"></a>
# **ResourcesDownloadUtility**
> System.IO.Stream ResourcesDownloadUtility (string id)

Download Utility

<p>Returns utility with specified ID as stream.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ResourcesDownloadUtilityExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new TemplatesAndUtilitiesApi(config);
            var id = "id_example";  // string | <p>ID of the utility.</p> <ul><li><code>exportutility</code>: Command-line cube export utility.</li><li><code>smartview</code>: Smart View for Essbase</li><li><code>lcm</code>: Life Cycle Management (LCM) utility for backup/restore/migration. </li><li><code>cli</code>: Essbase Command-Line Interface</li><li><code>migrationTool</code>: Command-line migration utility </li><li><code>EssbaseMaxlClient</code>: MaxL Client for Windows</li><li><code>EssbaseLinuxMaxlClient</code>:  MaxL Client for Linux</li><li><code>EssbaseClientLinux</code>: Essbase client libraries for C developers on Linux</li><li><code>EssbaseClientWindows</code>: Essbase client libraries for C developers on  Windows</li><li><code>EssbaseClientMacosx</code>: Essbase client libraries for C developers on Mac OS</li><li><code>japi</code>: Essbase tools and libraries for Java API</li><li><code>esscdext</code>: Cube Designer extension and add-in to Smart View</li></ul>

            try
            {
                // Download Utility
                System.IO.Stream result = apiInstance.ResourcesDownloadUtility(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling TemplatesAndUtilitiesApi.ResourcesDownloadUtility: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ResourcesDownloadUtilityWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Download Utility
    ApiResponse<System.IO.Stream> response = apiInstance.ResourcesDownloadUtilityWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling TemplatesAndUtilitiesApi.ResourcesDownloadUtilityWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of the utility.&lt;/p&gt; &lt;ul&gt;&lt;li&gt;&lt;code&gt;exportutility&lt;/code&gt;: Command-line cube export utility.&lt;/li&gt;&lt;li&gt;&lt;code&gt;smartview&lt;/code&gt;: Smart View for Essbase&lt;/li&gt;&lt;li&gt;&lt;code&gt;lcm&lt;/code&gt;: Life Cycle Management (LCM) utility for backup/restore/migration. &lt;/li&gt;&lt;li&gt;&lt;code&gt;cli&lt;/code&gt;: Essbase Command-Line Interface&lt;/li&gt;&lt;li&gt;&lt;code&gt;migrationTool&lt;/code&gt;: Command-line migration utility &lt;/li&gt;&lt;li&gt;&lt;code&gt;EssbaseMaxlClient&lt;/code&gt;: MaxL Client for Windows&lt;/li&gt;&lt;li&gt;&lt;code&gt;EssbaseLinuxMaxlClient&lt;/code&gt;:  MaxL Client for Linux&lt;/li&gt;&lt;li&gt;&lt;code&gt;EssbaseClientLinux&lt;/code&gt;: Essbase client libraries for C developers on Linux&lt;/li&gt;&lt;li&gt;&lt;code&gt;EssbaseClientWindows&lt;/code&gt;: Essbase client libraries for C developers on  Windows&lt;/li&gt;&lt;li&gt;&lt;code&gt;EssbaseClientMacosx&lt;/code&gt;: Essbase client libraries for C developers on Mac OS&lt;/li&gt;&lt;li&gt;&lt;code&gt;japi&lt;/code&gt;: Essbase tools and libraries for Java API&lt;/li&gt;&lt;li&gt;&lt;code&gt;esscdext&lt;/code&gt;: Cube Designer extension and add-in to Smart View&lt;/li&gt;&lt;/ul&gt; |  |

### Return type

**System.IO.Stream**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/zip, application/octet-stream, application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Utility with specified ID found.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to download utility with specified ID.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="resourcesgetutilities"></a>
# **ResourcesGetUtilities**
> ResourceList ResourcesGetUtilities ()

List Utilities

<p>Returns available utilities. For example: Export Utility, Smart View for Essbase.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ResourcesGetUtilitiesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new TemplatesAndUtilitiesApi(config);

            try
            {
                // List Utilities
                ResourceList result = apiInstance.ResourcesGetUtilities();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling TemplatesAndUtilitiesApi.ResourcesGetUtilities: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ResourcesGetUtilitiesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Utilities
    ApiResponse<ResourceList> response = apiInstance.ResourcesGetUtilitiesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling TemplatesAndUtilitiesApi.ResourcesGetUtilitiesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ResourceList**](ResourceList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Utilities returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get utilities.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

