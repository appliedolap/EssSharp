# EssSharp.Api.ApplicationConfigurationApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ApplicationConfigurationAddConfiguration**](ApplicationConfigurationApi.md#applicationconfigurationaddconfiguration) | **POST** /applications/{applicationName}/configurations | Add Application Configuration |
| [**ApplicationConfigurationDeleteConfiguration**](ApplicationConfigurationApi.md#applicationconfigurationdeleteconfiguration) | **DELETE** /applications/{applicationName}/configurations/{configId} | Delete Application Configuration |
| [**ApplicationConfigurationGetConfiguration**](ApplicationConfigurationApi.md#applicationconfigurationgetconfiguration) | **GET** /applications/{applicationName}/configurations/{configId} | Get Application Configuration Property |
| [**ApplicationConfigurationGetConfigurationKeys**](ApplicationConfigurationApi.md#applicationconfigurationgetconfigurationkeys) | **GET** /applications/{applicationName}/configurationkeys | Get Application Configuration (Filtered) |
| [**ApplicationConfigurationGetConfigurations**](ApplicationConfigurationApi.md#applicationconfigurationgetconfigurations) | **GET** /applications/{applicationName}/configurations | Get Application Configuration |
| [**ApplicationConfigurationSetConfiguration**](ApplicationConfigurationApi.md#applicationconfigurationsetconfiguration) | **PUT** /applications/{applicationName}/configurations/{configId} | Update Application Configuration |
| [**ApplicationConfigurationSetConfigurations**](ApplicationConfigurationApi.md#applicationconfigurationsetconfigurations) | **PUT** /applications/{applicationName}/configurations | Updates Application Configurations |

<a name="applicationconfigurationaddconfiguration"></a>
# **ApplicationConfigurationAddConfiguration**
> ApplicationConfigEntry ApplicationConfigurationAddConfiguration (string applicationName, ApplicationConfigEntry body)

Add Application Configuration

<p>Adds the configuration property to the application and returns the added configuration property name.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConfigurationAddConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConfigurationApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var body = new ApplicationConfigEntry(); // ApplicationConfigEntry | <p>Configuration property entry.</p>

            try
            {
                // Add Application Configuration
                ApplicationConfigEntry result = apiInstance.ApplicationConfigurationAddConfiguration(applicationName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationAddConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConfigurationAddConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Application Configuration
    ApiResponse<ApplicationConfigEntry> response = apiInstance.ApplicationConfigurationAddConfigurationWithHttpInfo(applicationName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationAddConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **body** | [**ApplicationConfigEntry**](ApplicationConfigEntry.md) | &lt;p&gt;Configuration property entry.&lt;/p&gt; |  |

### Return type

[**ApplicationConfigEntry**](ApplicationConfigEntry.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Configuration property added successfully. Returns the configuration details and the links to get, edit, or delete the configuration.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to add the configuration property. The application name or the configuration property JSON could be incorrect, or the configuration property might already have been added to the application.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconfigurationdeleteconfiguration"></a>
# **ApplicationConfigurationDeleteConfiguration**
> void ApplicationConfigurationDeleteConfiguration (string applicationName, string configId)

Delete Application Configuration

<p>Deletes the specified configuration property from the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConfigurationDeleteConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConfigurationApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var configId = "configId_example";  // string | <p>Configuration property name.</p>

            try
            {
                // Delete Application Configuration
                apiInstance.ApplicationConfigurationDeleteConfiguration(applicationName, configId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationDeleteConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConfigurationDeleteConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Application Configuration
    apiInstance.ApplicationConfigurationDeleteConfigurationWithHttpInfo(applicationName, configId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationDeleteConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **configId** | **string** | &lt;p&gt;Configuration property name.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The configuration was deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete the configuration. The application name or configuration property is invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconfigurationgetconfiguration"></a>
# **ApplicationConfigurationGetConfiguration**
> ApplicationConfigEntry ApplicationConfigurationGetConfiguration (string applicationName, string configId)

Get Application Configuration Property

<p>Returns configuration (based on configuration property name) from the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConfigurationGetConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConfigurationApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var configId = "configId_example";  // string | <p>Configuration property name.</p>

            try
            {
                // Get Application Configuration Property
                ApplicationConfigEntry result = apiInstance.ApplicationConfigurationGetConfiguration(applicationName, configId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationGetConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConfigurationGetConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Configuration Property
    ApiResponse<ApplicationConfigEntry> response = apiInstance.ApplicationConfigurationGetConfigurationWithHttpInfo(applicationName, configId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationGetConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **configId** | **string** | &lt;p&gt;Configuration property name.&lt;/p&gt; |  |

### Return type

[**ApplicationConfigEntry**](ApplicationConfigEntry.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The configuration was retrieved successfully. Returns the configuration details and the links to get, edit, or delete the configuration.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the configuration information. The application name or configuration property is invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconfigurationgetconfigurationkeys"></a>
# **ApplicationConfigurationGetConfigurationKeys**
> List&lt;ApplicationConfigList&gt; ApplicationConfigurationGetConfigurationKeys (string applicationName, string key = null, string configured = null)

Get Application Configuration (Filtered)

<p>Returns all the configuration properties currently set for the specified application, with option to filter by configured value.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConfigurationGetConfigurationKeysExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConfigurationApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var key = "\"*\"";  // string | <p>Filter key to refine the configuration property results.</p> (optional)  (default to "*")
            var configured = "all";  // string | <p>Specify <code>all</code> to return all configured properties. Specify <code>true</code> to return enabled configuration properties. Specify <code>false</code> to return configuration properties that are turned off.</p> (optional)  (default to all)

            try
            {
                // Get Application Configuration (Filtered)
                List<ApplicationConfigList> result = apiInstance.ApplicationConfigurationGetConfigurationKeys(applicationName, key, configured);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationGetConfigurationKeys: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConfigurationGetConfigurationKeysWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Configuration (Filtered)
    ApiResponse<List<ApplicationConfigList>> response = apiInstance.ApplicationConfigurationGetConfigurationKeysWithHttpInfo(applicationName, key, configured);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationGetConfigurationKeysWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **key** | **string** | &lt;p&gt;Filter key to refine the configuration property results.&lt;/p&gt; | [optional] [default to &quot;*&quot;] |
| **configured** | **string** | &lt;p&gt;Specify &lt;code&gt;all&lt;/code&gt; to return all configured properties. Specify &lt;code&gt;true&lt;/code&gt; to return enabled configuration properties. Specify &lt;code&gt;false&lt;/code&gt; to return configuration properties that are turned off.&lt;/p&gt; | [optional] [default to all] |

### Return type

[**List&lt;ApplicationConfigList&gt;**](ApplicationConfigList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The configuration properties were retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the configuration properties. The application name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconfigurationgetconfigurations"></a>
# **ApplicationConfigurationGetConfigurations**
> ApplicationConfigList ApplicationConfigurationGetConfigurations (string applicationName)

Get Application Configuration

<p>Returns all the configuration properties currently set for the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConfigurationGetConfigurationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConfigurationApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>

            try
            {
                // Get Application Configuration
                ApplicationConfigList result = apiInstance.ApplicationConfigurationGetConfigurations(applicationName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationGetConfigurations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConfigurationGetConfigurationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Configuration
    ApiResponse<ApplicationConfigList> response = apiInstance.ApplicationConfigurationGetConfigurationsWithHttpInfo(applicationName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationGetConfigurationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

### Return type

[**ApplicationConfigList**](ApplicationConfigList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Configuration properties retrieved successfully. Returns all the configuration properties which are set for the application, and the links to get, edit, or delete each property.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the configuration properties. The application name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconfigurationsetconfiguration"></a>
# **ApplicationConfigurationSetConfiguration**
> ApplicationConfigEntry ApplicationConfigurationSetConfiguration (string applicationName, string configId, ApplicationConfigEntry body)

Update Application Configuration

<p>Updates the application configuration and returns the updated configuration details.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConfigurationSetConfigurationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConfigurationApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var configId = "configId_example";  // string | <p>Configuration property name.</p>
            var body = new ApplicationConfigEntry(); // ApplicationConfigEntry | <p>Configuration property value entry.</p>

            try
            {
                // Update Application Configuration
                ApplicationConfigEntry result = apiInstance.ApplicationConfigurationSetConfiguration(applicationName, configId, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationSetConfiguration: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConfigurationSetConfigurationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Application Configuration
    ApiResponse<ApplicationConfigEntry> response = apiInstance.ApplicationConfigurationSetConfigurationWithHttpInfo(applicationName, configId, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationSetConfigurationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **configId** | **string** | &lt;p&gt;Configuration property name.&lt;/p&gt; |  |
| **body** | [**ApplicationConfigEntry**](ApplicationConfigEntry.md) | &lt;p&gt;Configuration property value entry.&lt;/p&gt; |  |

### Return type

[**ApplicationConfigEntry**](ApplicationConfigEntry.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The configuration was updated successfully. Returns the configuration details and the links to get, edit, or delete the configuration.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the configuration. The application name or configuration property is invalid, or the specified configuration property has not been added to the application.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconfigurationsetconfigurations"></a>
# **ApplicationConfigurationSetConfigurations**
> ApplicationConfigList ApplicationConfigurationSetConfigurations (string applicationName, List<ApplicationConfigEntry> body)

Updates Application Configurations

<p>Updates the application configurations and returns the updated configuration details.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConfigurationSetConfigurationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConfigurationApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var body = new List<ApplicationConfigEntry>(); // List<ApplicationConfigEntry> | <p>Configuration property value entries.</p>

            try
            {
                // Updates Application Configurations
                ApplicationConfigList result = apiInstance.ApplicationConfigurationSetConfigurations(applicationName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationSetConfigurations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConfigurationSetConfigurationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Updates Application Configurations
    ApiResponse<ApplicationConfigList> response = apiInstance.ApplicationConfigurationSetConfigurationsWithHttpInfo(applicationName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConfigurationApi.ApplicationConfigurationSetConfigurationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **body** | [**List&lt;ApplicationConfigEntry&gt;**](ApplicationConfigEntry.md) | &lt;p&gt;Configuration property value entries.&lt;/p&gt; |  |

### Return type

[**ApplicationConfigList**](ApplicationConfigList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Configurations were updated successfully. Returns the configuration details and the links to get, edit, or delete the configurations.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the configurations. The application name or configuration entries is invalid, or the given configuration property has not been added to the application.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

