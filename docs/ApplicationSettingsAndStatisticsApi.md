# EssSharp.Api.ApplicationSettingsAndStatisticsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ApplicationSettingsStatisticsGetSecuritySettings**](ApplicationSettingsAndStatisticsApi.md#applicationsettingsstatisticsgetsecuritysettings) | **GET** /applications/{applicationName}/settings/security | Get Application Security Settings |
| [**ApplicationSettingsStatisticsGetSettings**](ApplicationSettingsAndStatisticsApi.md#applicationsettingsstatisticsgetsettings) | **GET** /applications/{applicationName}/settings | Get Application General Settings |
| [**ApplicationSettingsStatisticsGetStartupSettings**](ApplicationSettingsAndStatisticsApi.md#applicationsettingsstatisticsgetstartupsettings) | **GET** /applications/{applicationName}/settings/startup | Get Application Startup Settings |
| [**ApplicationSettingsStatisticsGetStatistics**](ApplicationSettingsAndStatisticsApi.md#applicationsettingsstatisticsgetstatistics) | **GET** /applications/{applicationName}/statistics | Get Application Statistics |
| [**ApplicationSettingsStatisticsUpdateSettings**](ApplicationSettingsAndStatisticsApi.md#applicationsettingsstatisticsupdatesettings) | **PATCH** /applications/{applicationName}/settings | Update Application Settings |

<a id="applicationsettingsstatisticsgetsecuritysettings"></a>
# **ApplicationSettingsStatisticsGetSecuritySettings**
> AppSecuritySettings ApplicationSettingsStatisticsGetSecuritySettings (string applicationName)

Get Application Security Settings

<p>Returns the security settings of the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationSettingsStatisticsGetSecuritySettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>

            try
            {
                // Get Application Security Settings
                AppSecuritySettings result = apiInstance.ApplicationSettingsStatisticsGetSecuritySettings(applicationName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationSettingsAndStatisticsApi.ApplicationSettingsStatisticsGetSecuritySettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationSettingsStatisticsGetSecuritySettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Security Settings
    ApiResponse<AppSecuritySettings> response = apiInstance.ApplicationSettingsStatisticsGetSecuritySettingsWithHttpInfo(applicationName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationSettingsAndStatisticsApi.ApplicationSettingsStatisticsGetSecuritySettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

### Return type

[**AppSecuritySettings**](AppSecuritySettings.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Security settings returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the security settings. The application name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="applicationsettingsstatisticsgetsettings"></a>
# **ApplicationSettingsStatisticsGetSettings**
> AppSettingsList ApplicationSettingsStatisticsGetSettings (string applicationName, string expand = null)

Get Application General Settings

<p>Returns general settings of the specified application. Additional settings can be retrieved using the <code>expand</code> parameter.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationSettingsStatisticsGetSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var expand = "none";  // string | <p>Use <code>none</code> to show only general settings (this is the default). Other options available: <code>startup</code>, <code>security</code>, and <code>all</code>.</p> (optional)  (default to none)

            try
            {
                // Get Application General Settings
                AppSettingsList result = apiInstance.ApplicationSettingsStatisticsGetSettings(applicationName, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationSettingsAndStatisticsApi.ApplicationSettingsStatisticsGetSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationSettingsStatisticsGetSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application General Settings
    ApiResponse<AppSettingsList> response = apiInstance.ApplicationSettingsStatisticsGetSettingsWithHttpInfo(applicationName, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationSettingsAndStatisticsApi.ApplicationSettingsStatisticsGetSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **expand** | **string** | &lt;p&gt;Use &lt;code&gt;none&lt;/code&gt; to show only general settings (this is the default). Other options available: &lt;code&gt;startup&lt;/code&gt;, &lt;code&gt;security&lt;/code&gt;, and &lt;code&gt;all&lt;/code&gt;.&lt;/p&gt; | [optional] [default to none] |

### Return type

[**AppSettingsList**](AppSettingsList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;General application settings returned successfully, with links to get expanded settings and to edit settings.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;Failed to get the settings. The application name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="applicationsettingsstatisticsgetstartupsettings"></a>
# **ApplicationSettingsStatisticsGetStartupSettings**
> AppStartupSettings ApplicationSettingsStatisticsGetStartupSettings (string applicationName)

Get Application Startup Settings

<p>Returns the startup settings of the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationSettingsStatisticsGetStartupSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>

            try
            {
                // Get Application Startup Settings
                AppStartupSettings result = apiInstance.ApplicationSettingsStatisticsGetStartupSettings(applicationName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationSettingsAndStatisticsApi.ApplicationSettingsStatisticsGetStartupSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationSettingsStatisticsGetStartupSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Startup Settings
    ApiResponse<AppStartupSettings> response = apiInstance.ApplicationSettingsStatisticsGetStartupSettingsWithHttpInfo(applicationName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationSettingsAndStatisticsApi.ApplicationSettingsStatisticsGetStartupSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

### Return type

[**AppStartupSettings**](AppStartupSettings.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Startup settings retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the startup settings. The application name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="applicationsettingsstatisticsgetstatistics"></a>
# **ApplicationSettingsStatisticsGetStatistics**
> ApplicationStatistics ApplicationSettingsStatisticsGetStatistics (string applicationName)

Get Application Statistics

<p>Returns the statistics of the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationSettingsStatisticsGetStatisticsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>

            try
            {
                // Get Application Statistics
                ApplicationStatistics result = apiInstance.ApplicationSettingsStatisticsGetStatistics(applicationName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationSettingsAndStatisticsApi.ApplicationSettingsStatisticsGetStatistics: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationSettingsStatisticsGetStatisticsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Statistics
    ApiResponse<ApplicationStatistics> response = apiInstance.ApplicationSettingsStatisticsGetStatisticsWithHttpInfo(applicationName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationSettingsAndStatisticsApi.ApplicationSettingsStatisticsGetStatisticsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

### Return type

[**ApplicationStatistics**](ApplicationStatistics.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Statistics retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the statistics. The application name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="applicationsettingsstatisticsupdatesettings"></a>
# **ApplicationSettingsStatisticsUpdateSettings**
> void ApplicationSettingsStatisticsUpdateSettings (string applicationName, List<PatchElement> body)

Update Application Settings

<p>Updates the settings of the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationSettingsStatisticsUpdateSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var body = new List<PatchElement>(); // List<PatchElement> | <p>Application settings patch list.</p>

            try
            {
                // Update Application Settings
                apiInstance.ApplicationSettingsStatisticsUpdateSettings(applicationName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationSettingsAndStatisticsApi.ApplicationSettingsStatisticsUpdateSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationSettingsStatisticsUpdateSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Application Settings
    apiInstance.ApplicationSettingsStatisticsUpdateSettingsWithHttpInfo(applicationName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationSettingsAndStatisticsApi.ApplicationSettingsStatisticsUpdateSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **body** | [**List&lt;PatchElement&gt;**](PatchElement.md) | &lt;p&gt;Application settings patch list.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Settings updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the settings. The application name may be incorrect, or the JSON for the settings may be incorrect.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

