# EssSharp.Api.CentralizedURLApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CentraliazedurlGetSingleURLList**](CentralizedURLApi.md#centraliazedurlgetsingleurllist) | **GET** /centralizedurl | Get Essbase Server list |
| [**CentralizedUrlAddHostSingleURL**](CentralizedURLApi.md#centralizedurladdhostsingleurl) | **POST** /centralizedurl | Add Essbase server to Centralized URL List  |
| [**CentralizedUrlDeletSingleURLProperties**](CentralizedURLApi.md#centralizedurldeletsingleurlproperties) | **DELETE** /centralizedurl | Delete Essbase server from Centralized URL  |
| [**CentralizedUrlUpdateSingleURLProperties**](CentralizedURLApi.md#centralizedurlupdatesingleurlproperties) | **PUT** /centralizedurl | Update Essbase URL of Centralized URL List |

<a id="centraliazedurlgetsingleurllist"></a>
# **CentraliazedurlGetSingleURLList**
> List&lt;CentralizedURLRes&gt; CentraliazedurlGetSingleURLList (string filter = null)

Get Essbase Server list

<p> Returns the list of Essbase servers configured to access through centralized URL.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class CentraliazedurlGetSingleURLListExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new CentralizedURLApi(config);
            var filter = "filter_example";  // string |  (optional) 

            try
            {
                // Get Essbase Server list
                List<CentralizedURLRes> result = apiInstance.CentraliazedurlGetSingleURLList(filter);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CentralizedURLApi.CentraliazedurlGetSingleURLList: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CentraliazedurlGetSingleURLListWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Essbase Server list
    ApiResponse<List<CentralizedURLRes>> response = apiInstance.CentraliazedurlGetSingleURLListWithHttpInfo(filter);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CentralizedURLApi.CentraliazedurlGetSingleURLListWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **filter** | **string** |  | [optional]  |

### Return type

[**List&lt;CentralizedURLRes&gt;**](CentralizedURLRes.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Centralized URL List retrived sucessfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to retrive the centralized url list.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="centralizedurladdhostsingleurl"></a>
# **CentralizedUrlAddHostSingleURL**
> List&lt;CentralizedURL&gt; CentralizedUrlAddHostSingleURL (CentralizedURL body)

Add Essbase server to Centralized URL List 

<p>Add new alias and agent-url pair to centralized url list

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class CentralizedUrlAddHostSingleURLExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new CentralizedURLApi(config);
            var body = new CentralizedURL(); // CentralizedURL | CentralizedURL entry

            try
            {
                // Add Essbase server to Centralized URL List 
                List<CentralizedURL> result = apiInstance.CentralizedUrlAddHostSingleURL(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CentralizedURLApi.CentralizedUrlAddHostSingleURL: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CentralizedUrlAddHostSingleURLWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Essbase server to Centralized URL List 
    ApiResponse<List<CentralizedURL>> response = apiInstance.CentralizedUrlAddHostSingleURLWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CentralizedURLApi.CentralizedUrlAddHostSingleURLWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**CentralizedURL**](CentralizedURL.md) | CentralizedURL entry |  |

### Return type

[**List&lt;CentralizedURL&gt;**](CentralizedURL.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Added alias and agent url pair sucessfully into the centralized url list.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to add new entry to Centralized URL list. The JSON may be incorrect, or the given alias name may already exist or url specified is not an agent url.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="centralizedurldeletsingleurlproperties"></a>
# **CentralizedUrlDeletSingleURLProperties**
> List&lt;CentralizedURL&gt; CentralizedUrlDeletSingleURLProperties (string aliasName = null)

Delete Essbase server from Centralized URL 

<p> Removes the single entry from Centralized URL List.Provide the URL encoded String value as input(aliasName)</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class CentralizedUrlDeletSingleURLPropertiesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new CentralizedURLApi(config);
            var aliasName = "aliasName_example";  // string |  (optional) 

            try
            {
                // Delete Essbase server from Centralized URL 
                List<CentralizedURL> result = apiInstance.CentralizedUrlDeletSingleURLProperties(aliasName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CentralizedURLApi.CentralizedUrlDeletSingleURLProperties: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CentralizedUrlDeletSingleURLPropertiesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Essbase server from Centralized URL 
    ApiResponse<List<CentralizedURL>> response = apiInstance.CentralizedUrlDeletSingleURLPropertiesWithHttpInfo(aliasName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CentralizedURLApi.CentralizedUrlDeletSingleURLPropertiesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **aliasName** | **string** |  | [optional]  |

### Return type

[**List&lt;CentralizedURL&gt;**](CentralizedURL.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Sucessfully removed the entry from the centralized URL list&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete entry from centralized url list. Name specified may be incorrect, or not exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="centralizedurlupdatesingleurlproperties"></a>
# **CentralizedUrlUpdateSingleURLProperties**
> CentralizedURL CentralizedUrlUpdateSingleURLProperties (List<CentralizedURL> body)

Update Essbase URL of Centralized URL List

<p>Updates the single/multiple values of Centralized URL list 

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class CentralizedUrlUpdateSingleURLPropertiesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new CentralizedURLApi(config);
            var body = new List<CentralizedURL>(); // List<CentralizedURL> | CentralizedURL entry

            try
            {
                // Update Essbase URL of Centralized URL List
                CentralizedURL result = apiInstance.CentralizedUrlUpdateSingleURLProperties(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CentralizedURLApi.CentralizedUrlUpdateSingleURLProperties: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CentralizedUrlUpdateSingleURLPropertiesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Essbase URL of Centralized URL List
    ApiResponse<CentralizedURL> response = apiInstance.CentralizedUrlUpdateSingleURLPropertiesWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CentralizedURLApi.CentralizedUrlUpdateSingleURLPropertiesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**List&lt;CentralizedURL&gt;**](CentralizedURL.md) | CentralizedURL entry |  |

### Return type

[**CentralizedURL**](CentralizedURL.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Updated the agent urls successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the entries.The JSON may be incorrect, or provided url are already exist in the list.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

