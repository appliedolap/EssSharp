# EssSharp.Api.ServerLogsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ServerLogsDownloadAllLogFiles**](ServerLogsApi.md#serverlogsdownloadalllogfiles) | **GET** /logs/{serverType}/all | Download All Logs |
| [**ServerLogsDownloadLatestLogFile**](ServerLogsApi.md#serverlogsdownloadlatestlogfile) | **GET** /logs/{serverType}/latest | Download Latest Log |
| [**ServerLogsDownloadServerLogFiles**](ServerLogsApi.md#serverlogsdownloadserverlogfiles) | **GET** /logs/{serverType} | Download Logs for Server Type |
| [**ServerLogsGetServerLogLinks**](ServerLogsApi.md#serverlogsgetserverloglinks) | **GET** /logs | Get All Server Types |
| [**ServerLogsStream**](ServerLogsApi.md#serverlogsstream) | **GET** /logs/stream | Return Filtered Logs |

<a id="serverlogsdownloadalllogfiles"></a>
# **ServerLogsDownloadAllLogFiles**
> void ServerLogsDownloadAllLogFiles (string serverType)

Download All Logs

<p>Returns stream or downloadable zip file containing all log files for the specified server type.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ServerLogsDownloadAllLogFilesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServerLogsApi(config);
            var serverType = "serverType_example";  // string | Server type

            try
            {
                // Download All Logs
                apiInstance.ServerLogsDownloadAllLogFiles(serverType);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServerLogsApi.ServerLogsDownloadAllLogFiles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServerLogsDownloadAllLogFilesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Download All Logs
    apiInstance.ServerLogsDownloadAllLogFilesWithHttpInfo(serverType);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServerLogsApi.ServerLogsDownloadAllLogFilesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **serverType** | **string** | Server type |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logs returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to return logs.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="serverlogsdownloadlatestlogfile"></a>
# **ServerLogsDownloadLatestLogFile**
> void ServerLogsDownloadLatestLogFile (string serverType)

Download Latest Log

<p>Download the latest generated application log file as text file.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ServerLogsDownloadLatestLogFileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServerLogsApi(config);
            var serverType = "serverType_example";  // string | Server type

            try
            {
                // Download Latest Log
                apiInstance.ServerLogsDownloadLatestLogFile(serverType);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServerLogsApi.ServerLogsDownloadLatestLogFile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServerLogsDownloadLatestLogFileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Download Latest Log
    apiInstance.ServerLogsDownloadLatestLogFileWithHttpInfo(serverType);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServerLogsApi.ServerLogsDownloadLatestLogFileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **serverType** | **string** | Server type |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Log returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to return log.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="serverlogsdownloadserverlogfiles"></a>
# **ServerLogsDownloadServerLogFiles**
> Link ServerLogsDownloadServerLogFiles (string serverType)

Download Logs for Server Type

<p>Returns links to download all log files as zip file, and to download the latest log file for specific server type.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ServerLogsDownloadServerLogFilesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServerLogsApi(config);
            var serverType = "serverType_example";  // string | <p>Server type.</p>

            try
            {
                // Download Logs for Server Type
                Link result = apiInstance.ServerLogsDownloadServerLogFiles(serverType);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServerLogsApi.ServerLogsDownloadServerLogFiles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServerLogsDownloadServerLogFilesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Download Logs for Server Type
    ApiResponse<Link> response = apiInstance.ServerLogsDownloadServerLogFilesWithHttpInfo(serverType);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServerLogsApi.ServerLogsDownloadServerLogFilesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **serverType** | **string** | &lt;p&gt;Server type.&lt;/p&gt; |  |

### Return type

[**Link**](Link.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Return list of URI links.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Platform security exception.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="serverlogsgetserverloglinks"></a>
# **ServerLogsGetServerLogLinks**
> Link ServerLogsGetServerLogLinks ()

Get All Server Types

<p>Returns links listing all server types.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ServerLogsGetServerLogLinksExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServerLogsApi(config);

            try
            {
                // Get All Server Types
                Link result = apiInstance.ServerLogsGetServerLogLinks();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServerLogsApi.ServerLogsGetServerLogLinks: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServerLogsGetServerLogLinksWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get All Server Types
    ApiResponse<Link> response = apiInstance.ServerLogsGetServerLogLinksWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServerLogsApi.ServerLogsGetServerLogLinksWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**Link**](Link.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Successfully returned URI links listing all server types.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to return server types.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="serverlogsstream"></a>
# **ServerLogsStream**
> void ServerLogsStream (List<string> msgtype = null, string user = null, long? after = null, long? before = null, int? tail = null, List<string> filetype = null, List<string> application = null, string contains = null, bool? details = null)

Return Filtered Logs

<p>Returns the logs based on specified filters. Filters can be applied for user, message text, time, and other criteria.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ServerLogsStreamExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServerLogsApi(config);
            var msgtype = new List<string>(); // List<string> | One or more Log message types e.g. ERROR (optional) 
            var user = "user_example";  // string | <p>User ID.</p> (optional) 
            var after = 789L;  // long? | <p>After time (in milliseconds).</p> (optional) 
            var before = 789L;  // long? | <p>Before time (in milliseconds).</p> (optional) 
            var tail = 56;  // int? | <p>Last <i>n</i> records - maximum of 1000.</p> (optional) 
            var filetype = new List<string>(); // List<string> | <p>One or more file types.</p> (optional) 
            var application = new List<string>(); // List<string> | <p>Application name.</p> (optional) 
            var contains = "contains_example";  // string | <p>Check if message contains this value.</p> (optional) 
            var details = true;  // bool? | <p>Include supporting details in the response.</p> (optional) 

            try
            {
                // Return Filtered Logs
                apiInstance.ServerLogsStream(msgtype, user, after, before, tail, filetype, application, contains, details);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServerLogsApi.ServerLogsStream: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServerLogsStreamWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Return Filtered Logs
    apiInstance.ServerLogsStreamWithHttpInfo(msgtype, user, after, before, tail, filetype, application, contains, details);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServerLogsApi.ServerLogsStreamWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **msgtype** | [**List&lt;string&gt;**](string.md) | One or more Log message types e.g. ERROR | [optional]  |
| **user** | **string** | &lt;p&gt;User ID.&lt;/p&gt; | [optional]  |
| **after** | **long?** | &lt;p&gt;After time (in milliseconds).&lt;/p&gt; | [optional]  |
| **before** | **long?** | &lt;p&gt;Before time (in milliseconds).&lt;/p&gt; | [optional]  |
| **tail** | **int?** | &lt;p&gt;Last &lt;i&gt;n&lt;/i&gt; records - maximum of 1000.&lt;/p&gt; | [optional]  |
| **filetype** | [**List&lt;string&gt;**](string.md) | &lt;p&gt;One or more file types.&lt;/p&gt; | [optional]  |
| **application** | [**List&lt;string&gt;**](string.md) | &lt;p&gt;Application name.&lt;/p&gt; | [optional]  |
| **contains** | **string** | &lt;p&gt;Check if message contains this value.&lt;/p&gt; | [optional]  |
| **details** | **bool?** | &lt;p&gt;Include supporting details in the response.&lt;/p&gt; | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logs returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to return logs.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

