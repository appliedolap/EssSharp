# EssSharp.Api.BackupApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**BackupBackupApplications**](BackupApi.md#backupbackupapplications) | **POST** /backup/applications |  |
| [**BackupGetApplicationBackups**](BackupApi.md#backupgetapplicationbackups) | **GET** /backup/application/{applicationId} |  |
| [**BackupGetApplications**](BackupApi.md#backupgetapplications) | **GET** /backup/applications |  |
| [**BackupGetBackupInfo**](BackupApi.md#backupgetbackupinfo) | **GET** /backup/info |  |
| [**BackupGetQueueInfo**](BackupApi.md#backupgetqueueinfo) | **GET** /backup/queue |  |
| [**BackupGetSettings**](BackupApi.md#backupgetsettings) | **GET** /backup/settings |  |
| [**BackupGetSettingsValidator**](BackupApi.md#backupgetsettingsvalidator) | **GET** /backup/settings/validation |  |
| [**BackupGetSystemBackups**](BackupApi.md#backupgetsystembackups) | **GET** /backup/system |  |
| [**BackupIsRunning**](BackupApi.md#backupisrunning) | **GET** /backup/isrunning |  |
| [**BackupSaveSettings**](BackupApi.md#backupsavesettings) | **PUT** /backup/settings |  |
| [**BackupStartBackup**](BackupApi.md#backupstartbackup) | **POST** /backup/start |  |
| [**BackupStopBackup**](BackupApi.md#backupstopbackup) | **POST** /backup/stop |  |

<a id="backupbackupapplications"></a>
# **BackupBackupApplications**
> void BackupBackupApplications ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupBackupApplicationsExample
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

            var apiInstance = new BackupApi(config);

            try
            {
                apiInstance.BackupBackupApplications();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupBackupApplications: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupBackupApplicationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupBackupApplicationsWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupBackupApplicationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupgetapplicationbackups"></a>
# **BackupGetApplicationBackups**
> void BackupGetApplicationBackups (string applicationId)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupGetApplicationBackupsExample
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

            var apiInstance = new BackupApi(config);
            var applicationId = "applicationId_example";  // string | 

            try
            {
                apiInstance.BackupGetApplicationBackups(applicationId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupGetApplicationBackups: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupGetApplicationBackupsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupGetApplicationBackupsWithHttpInfo(applicationId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupGetApplicationBackupsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationId** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupgetapplications"></a>
# **BackupGetApplications**
> void BackupGetApplications ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupGetApplicationsExample
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

            var apiInstance = new BackupApi(config);

            try
            {
                apiInstance.BackupGetApplications();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupGetApplications: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupGetApplicationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupGetApplicationsWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupGetApplicationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupgetbackupinfo"></a>
# **BackupGetBackupInfo**
> void BackupGetBackupInfo ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupGetBackupInfoExample
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

            var apiInstance = new BackupApi(config);

            try
            {
                apiInstance.BackupGetBackupInfo();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupGetBackupInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupGetBackupInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupGetBackupInfoWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupGetBackupInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupgetqueueinfo"></a>
# **BackupGetQueueInfo**
> void BackupGetQueueInfo ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupGetQueueInfoExample
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

            var apiInstance = new BackupApi(config);

            try
            {
                apiInstance.BackupGetQueueInfo();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupGetQueueInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupGetQueueInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupGetQueueInfoWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupGetQueueInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupgetsettings"></a>
# **BackupGetSettings**
> void BackupGetSettings ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupGetSettingsExample
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

            var apiInstance = new BackupApi(config);

            try
            {
                apiInstance.BackupGetSettings();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupGetSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupGetSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupGetSettingsWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupGetSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupgetsettingsvalidator"></a>
# **BackupGetSettingsValidator**
> void BackupGetSettingsValidator ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupGetSettingsValidatorExample
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

            var apiInstance = new BackupApi(config);

            try
            {
                apiInstance.BackupGetSettingsValidator();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupGetSettingsValidator: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupGetSettingsValidatorWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupGetSettingsValidatorWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupGetSettingsValidatorWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupgetsystembackups"></a>
# **BackupGetSystemBackups**
> void BackupGetSystemBackups ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupGetSystemBackupsExample
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

            var apiInstance = new BackupApi(config);

            try
            {
                apiInstance.BackupGetSystemBackups();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupGetSystemBackups: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupGetSystemBackupsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupGetSystemBackupsWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupGetSystemBackupsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupisrunning"></a>
# **BackupIsRunning**
> void BackupIsRunning ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupIsRunningExample
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

            var apiInstance = new BackupApi(config);

            try
            {
                apiInstance.BackupIsRunning();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupIsRunning: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupIsRunningWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupIsRunningWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupIsRunningWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupsavesettings"></a>
# **BackupSaveSettings**
> void BackupSaveSettings (BackupConfigBean body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupSaveSettingsExample
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

            var apiInstance = new BackupApi(config);
            var body = new BackupConfigBean(); // BackupConfigBean |  (optional) 

            try
            {
                apiInstance.BackupSaveSettings(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupSaveSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupSaveSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupSaveSettingsWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupSaveSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**BackupConfigBean**](BackupConfigBean.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupstartbackup"></a>
# **BackupStartBackup**
> void BackupStartBackup ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupStartBackupExample
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

            var apiInstance = new BackupApi(config);

            try
            {
                apiInstance.BackupStartBackup();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupStartBackup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupStartBackupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupStartBackupWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupStartBackupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="backupstopbackup"></a>
# **BackupStopBackup**
> void BackupStopBackup ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class BackupStopBackupExample
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

            var apiInstance = new BackupApi(config);

            try
            {
                apiInstance.BackupStopBackup();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling BackupApi.BackupStopBackup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the BackupStopBackupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.BackupStopBackupWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling BackupApi.BackupStopBackupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

