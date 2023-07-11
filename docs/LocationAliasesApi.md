# EssSharp.Api.LocationAliasesApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**LocationAliasesCreate**](LocationAliasesApi.md#locationaliasescreate) | **POST** /applications/{applicationName}/databases/{databaseName}/locationaliases | Create Location Alias |
| [**LocationAliasesDelete**](LocationAliasesApi.md#locationaliasesdelete) | **DELETE** /applications/{applicationName}/databases/{databaseName}/locationaliases/{aliasName} | Delete Location Alias |
| [**LocationAliasesGetLocationAlias**](LocationAliasesApi.md#locationaliasesgetlocationalias) | **GET** /applications/{applicationName}/databases/{databaseName}/locationaliases/{aliasName} | Get Location Alias |
| [**LocationAliasesGetLocationAliases**](LocationAliasesApi.md#locationaliasesgetlocationaliases) | **GET** /applications/{applicationName}/databases/{databaseName}/locationaliases | Get Location Alias |
| [**LocationAliasesUpdate**](LocationAliasesApi.md#locationaliasesupdate) | **PATCH** /applications/{applicationName}/databases/{databaseName}/locationaliases/{aliasName} | Update Location Alias |

<a id="locationaliasescreate"></a>
# **LocationAliasesCreate**
> void LocationAliasesCreate (string applicationName, string databaseName, LocationAliasBean body)

Create Location Alias

<p>Creates a new location alias in the given application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocationAliasesCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocationAliasesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new LocationAliasBean(); // LocationAliasBean | <p>Location alias details.</p>

            try
            {
                // Create Location Alias
                apiInstance.LocationAliasesCreate(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocationAliasesApi.LocationAliasesCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocationAliasesCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Location Alias
    apiInstance.LocationAliasesCreateWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocationAliasesApi.LocationAliasesCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**LocationAliasBean**](LocationAliasBean.md) | &lt;p&gt;Location alias details.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Location alias created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create location alias.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="locationaliasesdelete"></a>
# **LocationAliasesDelete**
> void LocationAliasesDelete (string applicationName, string databaseName, string aliasName)

Delete Location Alias

<p>Deletes the specified location alias from the application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocationAliasesDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocationAliasesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var aliasName = "aliasName_example";  // string | <p>Location alias name.</p>

            try
            {
                // Delete Location Alias
                apiInstance.LocationAliasesDelete(applicationName, databaseName, aliasName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocationAliasesApi.LocationAliasesDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocationAliasesDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Location Alias
    apiInstance.LocationAliasesDeleteWithHttpInfo(applicationName, databaseName, aliasName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocationAliasesApi.LocationAliasesDeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **aliasName** | **string** | &lt;p&gt;Location alias name.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Location alias deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete location alias.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="locationaliasesgetlocationalias"></a>
# **LocationAliasesGetLocationAlias**
> LocationAliasBean LocationAliasesGetLocationAlias (string applicationName, string databaseName, string aliasName)

Get Location Alias

<p>Returns details of the specified location alias.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocationAliasesGetLocationAliasExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocationAliasesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var aliasName = "aliasName_example";  // string | <p>Location alias name.</p>

            try
            {
                // Get Location Alias
                LocationAliasBean result = apiInstance.LocationAliasesGetLocationAlias(applicationName, databaseName, aliasName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocationAliasesApi.LocationAliasesGetLocationAlias: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocationAliasesGetLocationAliasWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Location Alias
    ApiResponse<LocationAliasBean> response = apiInstance.LocationAliasesGetLocationAliasWithHttpInfo(applicationName, databaseName, aliasName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocationAliasesApi.LocationAliasesGetLocationAliasWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **aliasName** | **string** | &lt;p&gt;Location alias name.&lt;/p&gt; |  |

### Return type

[**LocationAliasBean**](LocationAliasBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Location alias returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get location alias details.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="locationaliasesgetlocationaliases"></a>
# **LocationAliasesGetLocationAliases**
> LocationAliasList LocationAliasesGetLocationAliases (string applicationName, string databaseName, int? offset = null, int? limit = null, string serverName = null, string applicationName2 = null, string databaseName2 = null)

Get Location Alias

Get Location Alias

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocationAliasesGetLocationAliasesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocationAliasesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var offset = 56;  // int? | <p>Number of items to omit from the start of the result set.</p> (optional) 
            var limit = 56;  // int? | <p>Maximum number of location aliases to return.</p> (optional) 
            var serverName = "serverName_example";  // string | <p>Location alias target server name.</p> (optional) 
            var applicationName2 = "applicationName_example";  // string | <p>Location alias target application name.</p> (optional) 
            var databaseName2 = "databaseName_example";  // string | <p>Location alias target database name.</p> (optional) 

            try
            {
                // Get Location Alias
                LocationAliasList result = apiInstance.LocationAliasesGetLocationAliases(applicationName, databaseName, offset, limit, serverName, applicationName2, databaseName2);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocationAliasesApi.LocationAliasesGetLocationAliases: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocationAliasesGetLocationAliasesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Location Alias
    ApiResponse<LocationAliasList> response = apiInstance.LocationAliasesGetLocationAliasesWithHttpInfo(applicationName, databaseName, offset, limit, serverName, applicationName2, databaseName2);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocationAliasesApi.LocationAliasesGetLocationAliasesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **offset** | **int?** | &lt;p&gt;Number of items to omit from the start of the result set.&lt;/p&gt; | [optional]  |
| **limit** | **int?** | &lt;p&gt;Maximum number of location aliases to return.&lt;/p&gt; | [optional]  |
| **serverName** | **string** | &lt;p&gt;Location alias target server name.&lt;/p&gt; | [optional]  |
| **applicationName2** | **string** | &lt;p&gt;Location alias target application name.&lt;/p&gt; | [optional]  |
| **databaseName2** | **string** | &lt;p&gt;Location alias target database name.&lt;/p&gt; | [optional]  |

### Return type

[**LocationAliasList**](LocationAliasList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Location alias returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get location alias details.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="locationaliasesupdate"></a>
# **LocationAliasesUpdate**
> void LocationAliasesUpdate (string applicationName, string databaseName, string aliasName, LocationAliasBean body)

Update Location Alias

<p>Updates location alias with new application and database. Not supported when location alias is defined across Essbase instances.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocationAliasesUpdateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocationAliasesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var aliasName = "aliasName_example";  // string | <p>Location alias name.</p>
            var body = new LocationAliasBean(); // LocationAliasBean | <p>Location alias details.</p>

            try
            {
                // Update Location Alias
                apiInstance.LocationAliasesUpdate(applicationName, databaseName, aliasName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocationAliasesApi.LocationAliasesUpdate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocationAliasesUpdateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Location Alias
    apiInstance.LocationAliasesUpdateWithHttpInfo(applicationName, databaseName, aliasName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocationAliasesApi.LocationAliasesUpdateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **aliasName** | **string** | &lt;p&gt;Location alias name.&lt;/p&gt; |  |
| **body** | [**LocationAliasBean**](LocationAliasBean.md) | &lt;p&gt;Location alias details.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Location alias updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update location alias.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

