# EssSharp.Api.ODBCApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ODBCRead**](ODBCApi.md#odbcread) | **GET** /odbc |  |
| [**ODBCWrite**](ODBCApi.md#odbcwrite) | **PUT** /odbc |  |

<a id="odbcread"></a>
# **ODBCRead**
> void ODBCRead ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ODBCReadExample
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

            var apiInstance = new ODBCApi(config);

            try
            {
                apiInstance.ODBCRead();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ODBCApi.ODBCRead: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ODBCReadWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.ODBCReadWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ODBCApi.ODBCReadWithHttpInfo: " + e.Message);
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
 - **Accept**: text/plain


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="odbcwrite"></a>
# **ODBCWrite**
> void ODBCWrite (string body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ODBCWriteExample
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

            var apiInstance = new ODBCApi(config);
            var body = "body_example";  // string |  (optional) 

            try
            {
                apiInstance.ODBCWrite(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ODBCApi.ODBCWrite: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ODBCWriteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.ODBCWriteWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ODBCApi.ODBCWriteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: text/plain
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

