# EssSharp.Api.ApplicationLogsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ApplicationLogsDownloadAllLogFiles**](ApplicationLogsApi.md#applicationlogsdownloadalllogfiles) | **GET** /applications/{applicationName}/logs/all | Download All Logs |
| [**ApplicationLogsDownloadAppLogFiles**](ApplicationLogsApi.md#applicationlogsdownloadapplogfiles) | **GET** /applications/{applicationName}/logs | Download Logs |
| [**ApplicationLogsDownloadLatestLogFile**](ApplicationLogsApi.md#applicationlogsdownloadlatestlogfile) | **GET** /applications/{applicationName}/logs/latest | Download Latest Log |

<a name="applicationlogsdownloadalllogfiles"></a>
# **ApplicationLogsDownloadAllLogFiles**
> void ApplicationLogsDownloadAllLogFiles (string applicationName)

Download All Logs

Returns or download zip file which contains all log files for the specific application

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationLogsDownloadAllLogFilesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationLogsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>

            try
            {
                // Download All Logs
                apiInstance.ApplicationLogsDownloadAllLogFiles(applicationName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationLogsApi.ApplicationLogsDownloadAllLogFiles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationLogsDownloadAllLogFilesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Download All Logs
    apiInstance.ApplicationLogsDownloadAllLogFilesWithHttpInfo(applicationName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationLogsApi.ApplicationLogsDownloadAllLogFilesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

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

<a name="applicationlogsdownloadapplogfiles"></a>
# **ApplicationLogsDownloadAppLogFiles**
> Link ApplicationLogsDownloadAppLogFiles (string applicationName)

Download Logs

<p>Returns links to download all log files as a zip file, and to download the latest log file.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationLogsDownloadAppLogFilesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationLogsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>

            try
            {
                // Download Logs
                Link result = apiInstance.ApplicationLogsDownloadAppLogFiles(applicationName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationLogsApi.ApplicationLogsDownloadAppLogFiles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationLogsDownloadAppLogFilesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Download Logs
    ApiResponse<Link> response = apiInstance.ApplicationLogsDownloadAppLogFilesWithHttpInfo(applicationName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationLogsApi.ApplicationLogsDownloadAppLogFilesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

### Return type

[**Link**](Link.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;List of URI links returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to return downloadable log files.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationlogsdownloadlatestlogfile"></a>
# **ApplicationLogsDownloadLatestLogFile**
> void ApplicationLogsDownloadLatestLogFile (string applicationName)

Download Latest Log

<p>Downloads the latest application log file as a text file.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationLogsDownloadLatestLogFileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationLogsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>

            try
            {
                // Download Latest Log
                apiInstance.ApplicationLogsDownloadLatestLogFile(applicationName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationLogsApi.ApplicationLogsDownloadLatestLogFile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationLogsDownloadLatestLogFileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Download Latest Log
    apiInstance.ApplicationLogsDownloadLatestLogFileWithHttpInfo(applicationName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationLogsApi.ApplicationLogsDownloadLatestLogFileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Log file returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to return log file.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

