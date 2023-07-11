# EssSharp.Api.PlatformServiceSettingsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**Getodllogsettings**](PlatformServiceSettingsApi.md#getodllogsettings) | **GET** /settings/odlLogSettings | Get Diagnostic Log Settings |
| [**PSMSettingsGetAll**](PlatformServiceSettingsApi.md#psmsettingsgetall) | **GET** /settings | Get Available Platform Service Settings |
| [**PSMSettingsGetDatabaseSettings**](PlatformServiceSettingsApi.md#psmsettingsgetdatabasesettings) | **GET** /settings/database | Get Database Settings |
| [**PSMSettingsGetSystemMaintenanceLimits**](PlatformServiceSettingsApi.md#psmsettingsgetsystemmaintenancelimits) | **GET** /settings/maintenance | Get Maintenance Settings |
| [**PSMSettingsSetDatabaseSettings**](PlatformServiceSettingsApi.md#psmsettingssetdatabasesettings) | **PUT** /settings/database | Store Database Settings |
| [**Setodllogsettings**](PlatformServiceSettingsApi.md#setodllogsettings) | **PUT** /settings/odlLogSettings | Set Diagnostic Log Settings |

<a id="getodllogsettings"></a>
# **Getodllogsettings**
> void Getodllogsettings ()

Get Diagnostic Log Settings

<p>Get the Oracle Diagnostic Log (ODL) settings.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GetodllogsettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PlatformServiceSettingsApi(config);

            try
            {
                // Get Diagnostic Log Settings
                apiInstance.Getodllogsettings();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlatformServiceSettingsApi.Getodllogsettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetodllogsettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Diagnostic Log Settings
    apiInstance.GetodllogsettingsWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PlatformServiceSettingsApi.GetodllogsettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Successful operation.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="psmsettingsgetall"></a>
# **PSMSettingsGetAll**
> Settings PSMSettingsGetAll ()

Get Available Platform Service Settings

<p>Returns the platform service settings. This API returns the links to various settings available in this release.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PSMSettingsGetAllExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PlatformServiceSettingsApi(config);

            try
            {
                // Get Available Platform Service Settings
                Settings result = apiInstance.PSMSettingsGetAll();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlatformServiceSettingsApi.PSMSettingsGetAll: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PSMSettingsGetAllWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Available Platform Service Settings
    ApiResponse<Settings> response = apiInstance.PSMSettingsGetAllWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PlatformServiceSettingsApi.PSMSettingsGetAllWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**Settings**](Settings.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | successful operation |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="psmsettingsgetdatabasesettings"></a>
# **PSMSettingsGetDatabaseSettings**
> Limits PSMSettingsGetDatabaseSettings ()

Get Database Settings

<p>Gets the platform service database settings.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PSMSettingsGetDatabaseSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PlatformServiceSettingsApi(config);

            try
            {
                // Get Database Settings
                Limits result = apiInstance.PSMSettingsGetDatabaseSettings();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlatformServiceSettingsApi.PSMSettingsGetDatabaseSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PSMSettingsGetDatabaseSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Database Settings
    ApiResponse<Limits> response = apiInstance.PSMSettingsGetDatabaseSettingsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PlatformServiceSettingsApi.PSMSettingsGetDatabaseSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**Limits**](Limits.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Platform service database settings returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get resource settings.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="psmsettingsgetsystemmaintenancelimits"></a>
# **PSMSettingsGetSystemMaintenanceLimits**
> Limits PSMSettingsGetSystemMaintenanceLimits ()

Get Maintenance Settings

<p>Gets the platform service maintenance settings, such as disk space and memory.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PSMSettingsGetSystemMaintenanceLimitsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PlatformServiceSettingsApi(config);

            try
            {
                // Get Maintenance Settings
                Limits result = apiInstance.PSMSettingsGetSystemMaintenanceLimits();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlatformServiceSettingsApi.PSMSettingsGetSystemMaintenanceLimits: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PSMSettingsGetSystemMaintenanceLimitsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Maintenance Settings
    ApiResponse<Limits> response = apiInstance.PSMSettingsGetSystemMaintenanceLimitsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PlatformServiceSettingsApi.PSMSettingsGetSystemMaintenanceLimitsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**Limits**](Limits.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Saved platform service resource settings returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get resource settings.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="psmsettingssetdatabasesettings"></a>
# **PSMSettingsSetDatabaseSettings**
> Limits PSMSettingsSetDatabaseSettings (DatabaseSettings body = null)

Store Database Settings

<p>Saves the platform service database settings.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PSMSettingsSetDatabaseSettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PlatformServiceSettingsApi(config);
            var body = new DatabaseSettings(); // DatabaseSettings |  (optional) 

            try
            {
                // Store Database Settings
                Limits result = apiInstance.PSMSettingsSetDatabaseSettings(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlatformServiceSettingsApi.PSMSettingsSetDatabaseSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PSMSettingsSetDatabaseSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Store Database Settings
    ApiResponse<Limits> response = apiInstance.PSMSettingsSetDatabaseSettingsWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PlatformServiceSettingsApi.PSMSettingsSetDatabaseSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**DatabaseSettings**](DatabaseSettings.md) |  | [optional]  |

### Return type

[**Limits**](Limits.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to save settings.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to save settings.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="setodllogsettings"></a>
# **Setodllogsettings**
> void Setodllogsettings (List<ODLLogHandlerSetting> body = null)

Set Diagnostic Log Settings

<p>Set the Oracle Diagnostic Log (ODL) settings.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class SetodllogsettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PlatformServiceSettingsApi(config);
            var body = new List<ODLLogHandlerSetting>(); // List<ODLLogHandlerSetting> |  (optional) 

            try
            {
                // Set Diagnostic Log Settings
                apiInstance.Setodllogsettings(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PlatformServiceSettingsApi.Setodllogsettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SetodllogsettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Set Diagnostic Log Settings
    apiInstance.SetodllogsettingsWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PlatformServiceSettingsApi.SetodllogsettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**List&lt;ODLLogHandlerSetting&gt;**](ODLLogHandlerSetting.md) |  | [optional]  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Successful operation.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

