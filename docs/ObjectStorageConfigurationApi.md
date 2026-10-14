# EssSharp.Api.ObjectStorageConfigurationApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**CloudStorageCreateOrUpdateConfig**](ObjectStorageConfigurationApi.md#cloudstoragecreateorupdateconfig) | **PUT** /cloudstorage/config | Create or Update Object Storage Configuration |
| [**CloudStorageGetConfig**](ObjectStorageConfigurationApi.md#cloudstoragegetconfig) | **GET** /cloudstorage/config | Get Object Storage Configuration |
| [**CloudStorageTestConfig**](ObjectStorageConfigurationApi.md#cloudstoragetestconfig) | **POST** /cloudstorage/config/test | Verify Object Storage Configuration |

<a id="cloudstoragecreateorupdateconfig"></a>
# **CloudStorageCreateOrUpdateConfig**
> void CloudStorageCreateOrUpdateConfig (CloudStorageDetails body)

Create or Update Object Storage Configuration

<p>Create or update object storage configuration. This is applicable only in ADBS deployment

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class CloudStorageCreateOrUpdateConfigExample
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

            var apiInstance = new ObjectStorageConfigurationApi(config);
            var body = new CloudStorageDetails(); // CloudStorageDetails | <p>Object storage configuration.</p>

            try
            {
                // Create or Update Object Storage Configuration
                apiInstance.CloudStorageCreateOrUpdateConfig(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ObjectStorageConfigurationApi.CloudStorageCreateOrUpdateConfig: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CloudStorageCreateOrUpdateConfigWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create or Update Object Storage Configuration
    apiInstance.CloudStorageCreateOrUpdateConfigWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ObjectStorageConfigurationApi.CloudStorageCreateOrUpdateConfigWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**CloudStorageDetails**](CloudStorageDetails.md) | &lt;p&gt;Object storage configuration.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Object storage configuration saved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to save object storage configuration.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="cloudstoragegetconfig"></a>
# **CloudStorageGetConfig**
> void CloudStorageGetConfig ()

Get Object Storage Configuration

<p>Get object storage configuration. This is applicable only in ADBS deployment

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class CloudStorageGetConfigExample
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

            var apiInstance = new ObjectStorageConfigurationApi(config);

            try
            {
                // Get Object Storage Configuration
                apiInstance.CloudStorageGetConfig();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ObjectStorageConfigurationApi.CloudStorageGetConfig: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CloudStorageGetConfigWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Object Storage Configuration
    apiInstance.CloudStorageGetConfigWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ObjectStorageConfigurationApi.CloudStorageGetConfigWithHttpInfo: " + e.Message);
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
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Object storage configuration retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to retrieve object storage configuration.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="cloudstoragetestconfig"></a>
# **CloudStorageTestConfig**
> void CloudStorageTestConfig (CloudStorageDetails body)

Verify Object Storage Configuration

<p>Verify provided object storage configuration details. This is applicable only in ADBS deployment

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class CloudStorageTestConfigExample
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

            var apiInstance = new ObjectStorageConfigurationApi(config);
            var body = new CloudStorageDetails(); // CloudStorageDetails | <p>Object storage configuration.</p>

            try
            {
                // Verify Object Storage Configuration
                apiInstance.CloudStorageTestConfig(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ObjectStorageConfigurationApi.CloudStorageTestConfig: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the CloudStorageTestConfigWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Verify Object Storage Configuration
    apiInstance.CloudStorageTestConfigWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ObjectStorageConfigurationApi.CloudStorageTestConfigWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**CloudStorageDetails**](CloudStorageDetails.md) | &lt;p&gt;Object storage configuration.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Verified object storage configuration details successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to verify object storage configuration details.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

