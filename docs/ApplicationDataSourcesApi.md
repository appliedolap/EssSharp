# EssSharp.Api.ApplicationDatasourcesApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ApplicationDatasourcesCreateDatasource**](ApplicationDatasourcesApi.md#applicationdatasourcescreatedatasource) | **POST** /applications/{applicationName}/datasources | Create Application Datasource |
| [**ApplicationDatasourcesDeleteDatasource**](ApplicationDatasourcesApi.md#applicationdatasourcesdeletedatasource) | **DELETE** /applications/{applicationName}/datasources/{datasourceName} | Delete Application Datasource |
| [**ApplicationDatasourcesGetDataStream**](ApplicationDatasourcesApi.md#applicationdatasourcesgetdatastream) | **POST** /applications/{applicationName}/datasources/query/stream | Get Streamed Datasource Results |
| [**ApplicationDatasourcesGetDatasourceDetails**](ApplicationDatasourcesApi.md#applicationdatasourcesgetdatasourcedetails) | **GET** /applications/{applicationName}/datasources/{datasouceName} | Get Application Datasource |
| [**ApplicationDatasourcesGetDatasources**](ApplicationDatasourcesApi.md#applicationdatasourcesgetdatasources) | **GET** /applications/{applicationName}/datasources | Get Application Datasources |
| [**ApplicationDatasourcesUpdateDatasource**](ApplicationDatasourcesApi.md#applicationdatasourcesupdatedatasource) | **PUT** /applications/{applicationName}/datasources/{datasouceName} | Update Application Datasource |

<a name="applicationdatasourcescreatedatasource"></a>
# **ApplicationDatasourcesCreateDatasource**
> void ApplicationDatasourcesCreateDatasource (string applicationName, Datasource body = null)

Create Application Datasource

<p>Creates an application-level Datasource based on specified inputs. <code>name</code>, <code>connection</code>, and <code>type</code> are required inputs for all types of Datasource. Other required inputs differ based on the type of Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationDatasourcesCreateDatasourceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationDatasourcesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var body = new Datasource(); // Datasource | <p>Datasource details.</p> (optional) 

            try
            {
                // Create Application Datasource
                apiInstance.ApplicationDatasourcesCreateDatasource(applicationName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesCreateDatasource: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationDatasourcesCreateDatasourceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Application Datasource
    apiInstance.ApplicationDatasourcesCreateDatasourceWithHttpInfo(applicationName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesCreateDatasourceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **body** | [**Datasource**](Datasource.md) | &lt;p&gt;Datasource details.&lt;/p&gt; | [optional]  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Datasource created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create Datasource.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationdatasourcesdeletedatasource"></a>
# **ApplicationDatasourcesDeleteDatasource**
> void ApplicationDatasourcesDeleteDatasource (string applicationName, string datasourceName)

Delete Application Datasource

<p>Deletes the named application-level Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationDatasourcesDeleteDatasourceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationDatasourcesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var datasourceName = "datasourceName_example";  // string | <p>Datasource name.</p>

            try
            {
                // Delete Application Datasource
                apiInstance.ApplicationDatasourcesDeleteDatasource(applicationName, datasourceName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesDeleteDatasource: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationDatasourcesDeleteDatasourceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Application Datasource
    apiInstance.ApplicationDatasourcesDeleteDatasourceWithHttpInfo(applicationName, datasourceName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesDeleteDatasourceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **datasourceName** | **string** | &lt;p&gt;Datasource name.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Datasource was deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete Datasource.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationdatasourcesgetdatastream"></a>
# **ApplicationDatasourcesGetDataStream**
> void ApplicationDatasourcesGetDataStream (string applicationName, bool? includeHeaders = null, bool? metaDataOnly = null, DatasourceQueryInfo body = null)

Get Streamed Datasource Results

<p>Returns results in stream from an application-level Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationDatasourcesGetDataStreamExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationDatasourcesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var includeHeaders = false;  // bool? | <p>Include headers.</p> (optional)  (default to false)
            var metaDataOnly = false;  // bool? | <p>Metadata Only.</p> (optional)  (default to false)
            var body = new DatasourceQueryInfo(); // DatasourceQueryInfo | <p>Query information.</p> (optional) 

            try
            {
                // Get Streamed Datasource Results
                apiInstance.ApplicationDatasourcesGetDataStream(applicationName, includeHeaders, metaDataOnly, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesGetDataStream: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationDatasourcesGetDataStreamWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Streamed Datasource Results
    apiInstance.ApplicationDatasourcesGetDataStreamWithHttpInfo(applicationName, includeHeaders, metaDataOnly, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesGetDataStreamWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **includeHeaders** | **bool?** | &lt;p&gt;Include headers.&lt;/p&gt; | [optional] [default to false] |
| **metaDataOnly** | **bool?** | &lt;p&gt;Metadata Only.&lt;/p&gt; | [optional] [default to false] |
| **body** | [**DatasourceQueryInfo**](DatasourceQueryInfo.md) | &lt;p&gt;Query information.&lt;/p&gt; | [optional]  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Results fetched successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;&lt;/p&gt;&lt;p&gt;Failed to stream results.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationdatasourcesgetdatasourcedetails"></a>
# **ApplicationDatasourcesGetDatasourceDetails**
> Datasource ApplicationDatasourcesGetDatasourceDetails (string applicationName, string datasouceName)

Get Application Datasource

<p>Returns details about the specified application-level Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationDatasourcesGetDatasourceDetailsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationDatasourcesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var datasouceName = "datasouceName_example";  // string | <p>Datasource name</p>

            try
            {
                // Get Application Datasource
                Datasource result = apiInstance.ApplicationDatasourcesGetDatasourceDetails(applicationName, datasouceName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesGetDatasourceDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationDatasourcesGetDatasourceDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Datasource
    ApiResponse<Datasource> response = apiInstance.ApplicationDatasourcesGetDatasourceDetailsWithHttpInfo(applicationName, datasouceName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesGetDatasourceDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **datasouceName** | **string** | &lt;p&gt;Datasource name&lt;/p&gt; |  |

### Return type

[**Datasource**](Datasource.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Datasource details returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get Datasource details.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationdatasourcesgetdatasources"></a>
# **ApplicationDatasourcesGetDatasources**
> DatasourcesList ApplicationDatasourcesGetDatasources (string applicationName, int? offset = null, int? limit = null)

Get Application Datasources

<p>Returns a list of application-level Datasources, including details such as name, description, connection, and type.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationDatasourcesGetDatasourcesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationDatasourcesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var offset = 0;  // int? | <p>Number of Datasources to omit from the start of the result set.</p> (optional)  (default to 0)
            var limit = 50;  // int? | <p>Maximum number of Datasources to return. Default is 50.</p> (optional)  (default to 50)

            try
            {
                // Get Application Datasources
                DatasourcesList result = apiInstance.ApplicationDatasourcesGetDatasources(applicationName, offset, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesGetDatasources: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationDatasourcesGetDatasourcesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Datasources
    ApiResponse<DatasourcesList> response = apiInstance.ApplicationDatasourcesGetDatasourcesWithHttpInfo(applicationName, offset, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesGetDatasourcesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **offset** | **int?** | &lt;p&gt;Number of Datasources to omit from the start of the result set.&lt;/p&gt; | [optional] [default to 0] |
| **limit** | **int?** | &lt;p&gt;Maximum number of Datasources to return. Default is 50.&lt;/p&gt; | [optional] [default to 50] |

### Return type

[**DatasourcesList**](DatasourcesList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;List of Datasources returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get Datasources.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationdatasourcesupdatedatasource"></a>
# **ApplicationDatasourcesUpdateDatasource**
> Datasource ApplicationDatasourcesUpdateDatasource (string applicationName, string datasouceName, Datasource body = null)

Update Application Datasource

<p>Update the named application-level Datasource. If the update is successful, returns details about the updated Datasource. <code>type</code> and <code>connection</code> are required inputs for all types of Datasource. Other required inputs differ based on the type of the Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationDatasourcesUpdateDatasourceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationDatasourcesApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var datasouceName = "datasouceName_example";  // string | <p>Datasource name.</p>
            var body = new Datasource(); // Datasource | <p>Updated Datasource details.</p> (optional) 

            try
            {
                // Update Application Datasource
                Datasource result = apiInstance.ApplicationDatasourcesUpdateDatasource(applicationName, datasouceName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesUpdateDatasource: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationDatasourcesUpdateDatasourceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Application Datasource
    ApiResponse<Datasource> response = apiInstance.ApplicationDatasourcesUpdateDatasourceWithHttpInfo(applicationName, datasouceName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationDatasourcesApi.ApplicationDatasourcesUpdateDatasourceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **datasouceName** | **string** | &lt;p&gt;Datasource name.&lt;/p&gt; |  |
| **body** | [**Datasource**](Datasource.md) | &lt;p&gt;Updated Datasource details.&lt;/p&gt; | [optional]  |

### Return type

[**Datasource**](Datasource.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Datasource was updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the Datasource.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

