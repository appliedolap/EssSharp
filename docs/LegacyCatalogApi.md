# EssSharp.Api.LegacyCatalogApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**LegacyCatalogGetFileContent**](LegacyCatalogApi.md#legacycataloggetfilecontent) | **GET** /catalog_old/{fileName} |  |

<a id="legacycataloggetfilecontent"></a>
# **LegacyCatalogGetFileContent**
> void LegacyCatalogGetFileContent (string fileName)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LegacyCatalogGetFileContentExample
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

            var apiInstance = new LegacyCatalogApi(config);
            var fileName = "fileName_example";  // string | 

            try
            {
                apiInstance.LegacyCatalogGetFileContent(fileName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LegacyCatalogApi.LegacyCatalogGetFileContent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LegacyCatalogGetFileContentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.LegacyCatalogGetFileContentWithHttpInfo(fileName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LegacyCatalogApi.LegacyCatalogGetFileContentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **fileName** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

