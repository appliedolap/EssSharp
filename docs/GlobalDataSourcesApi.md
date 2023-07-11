# EssSharp.Api.GlobalDatasourcesApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GlobalDatasourcesCreateDatasource**](GlobalDatasourcesApi.md#globaldatasourcescreatedatasource) | **POST** /datasources | Create Global Datasource |
| [**GlobalDatasourcesDeleteDatasource**](GlobalDatasourcesApi.md#globaldatasourcesdeletedatasource) | **DELETE** /datasources/{datasourceName} | Delete Global Datasource |
| [**GlobalDatasourcesGetData**](GlobalDatasourcesApi.md#globaldatasourcesgetdata) | **GET** /datasources/query/data/{streamId} | Get Streamed Datasource Results by ID |
| [**GlobalDatasourcesGetDataStream**](GlobalDatasourcesApi.md#globaldatasourcesgetdatastream) | **POST** /datasources/query/stream | Get Streamed Datasource Results |
| [**GlobalDatasourcesGetDatasourceDetails**](GlobalDatasourcesApi.md#globaldatasourcesgetdatasourcedetails) | **GET** /datasources/{datasouceName} | Get Global Datasource |
| [**GlobalDatasourcesGetDatasources**](GlobalDatasourcesApi.md#globaldatasourcesgetdatasources) | **GET** /datasources | Get Global Datasource |
| [**GlobalDatasourcesGetDelimitedDataStream**](GlobalDatasourcesApi.md#globaldatasourcesgetdelimiteddatastream) | **POST** /datasources/customdelimited/query/stream | Get Streamed Datasource Results |
| [**GlobalDatasourcesGetResults**](GlobalDatasourcesApi.md#globaldatasourcesgetresults) | **POST** /datasources/query | Stream Datasource Results |
| [**GlobalDatasourcesUpdateDatasource**](GlobalDatasourcesApi.md#globaldatasourcesupdatedatasource) | **PUT** /datasources/{datasouceName} | Update Global Datasource |

<a id="globaldatasourcescreatedatasource"></a>
# **GlobalDatasourcesCreateDatasource**
> void GlobalDatasourcesCreateDatasource (Datasource body = null)

Create Global Datasource

<p>Creates a global-level Datasource based on specified inputs. <code>name</code>, <code>connection</code>, and <code>type</code> are required inputs for all types of Datasources. Other required inputs differ based on the type of Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalDatasourcesCreateDatasourceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalDatasourcesApi(config);
            var body = new Datasource(); // Datasource | <p>Datasource details.</p> (optional) 

            try
            {
                // Create Global Datasource
                apiInstance.GlobalDatasourcesCreateDatasource(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesCreateDatasource: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalDatasourcesCreateDatasourceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Global Datasource
    apiInstance.GlobalDatasourcesCreateDatasourceWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesCreateDatasourceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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

<a id="globaldatasourcesdeletedatasource"></a>
# **GlobalDatasourcesDeleteDatasource**
> void GlobalDatasourcesDeleteDatasource (string datasourceName)

Delete Global Datasource

<p>Deletes the named global-level Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalDatasourcesDeleteDatasourceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalDatasourcesApi(config);
            var datasourceName = "datasourceName_example";  // string | <p>Datasource name.</p>

            try
            {
                // Delete Global Datasource
                apiInstance.GlobalDatasourcesDeleteDatasource(datasourceName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesDeleteDatasource: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalDatasourcesDeleteDatasourceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Global Datasource
    apiInstance.GlobalDatasourcesDeleteDatasourceWithHttpInfo(datasourceName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesDeleteDatasourceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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

<a id="globaldatasourcesgetdata"></a>
# **GlobalDatasourcesGetData**
> void GlobalDatasourcesGetData (string streamId)

Get Streamed Datasource Results by ID

<p>Returns results from a global-level Datasource associated with the specified stream id.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalDatasourcesGetDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalDatasourcesApi(config);
            var streamId = "streamId_example";  // string | <p>Stream id.</p>

            try
            {
                // Get Streamed Datasource Results by ID
                apiInstance.GlobalDatasourcesGetData(streamId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalDatasourcesGetDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Streamed Datasource Results by ID
    apiInstance.GlobalDatasourcesGetDataWithHttpInfo(streamId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **streamId** | **string** | &lt;p&gt;Stream id.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Results fetched successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to stream results.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="globaldatasourcesgetdatastream"></a>
# **GlobalDatasourcesGetDataStream**
> void GlobalDatasourcesGetDataStream (bool? includeHeaders = null, bool? metaDataOnly = null, DatasourceQueryInfo body = null)

Get Streamed Datasource Results

<p>Returns results in stream from a global-level Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalDatasourcesGetDataStreamExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalDatasourcesApi(config);
            var includeHeaders = false;  // bool? | <p>Include headers.</p> (optional)  (default to false)
            var metaDataOnly = false;  // bool? | <p>Metadata Only.</p> (optional)  (default to false)
            var body = new DatasourceQueryInfo(); // DatasourceQueryInfo | <p>Query information.</p> (optional) 

            try
            {
                // Get Streamed Datasource Results
                apiInstance.GlobalDatasourcesGetDataStream(includeHeaders, metaDataOnly, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetDataStream: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalDatasourcesGetDataStreamWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Streamed Datasource Results
    apiInstance.GlobalDatasourcesGetDataStreamWithHttpInfo(includeHeaders, metaDataOnly, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetDataStreamWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to stream results.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="globaldatasourcesgetdatasourcedetails"></a>
# **GlobalDatasourcesGetDatasourceDetails**
> Datasource GlobalDatasourcesGetDatasourceDetails (string datasouceName)

Get Global Datasource

<p>Returns details about the specified global Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalDatasourcesGetDatasourceDetailsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalDatasourcesApi(config);
            var datasouceName = "datasouceName_example";  // string | <p>Datasource name.</p>

            try
            {
                // Get Global Datasource
                Datasource result = apiInstance.GlobalDatasourcesGetDatasourceDetails(datasouceName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetDatasourceDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalDatasourcesGetDatasourceDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Global Datasource
    ApiResponse<Datasource> response = apiInstance.GlobalDatasourcesGetDatasourceDetailsWithHttpInfo(datasouceName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetDatasourceDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **datasouceName** | **string** | &lt;p&gt;Datasource name.&lt;/p&gt; |  |

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

<a id="globaldatasourcesgetdatasources"></a>
# **GlobalDatasourcesGetDatasources**
> DatasourcesList GlobalDatasourcesGetDatasources (int? offset = null, int? limit = null)

Get Global Datasource

<p>Returns a list of global-level Datasources, including details such as name, description, connection, and type.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalDatasourcesGetDatasourcesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalDatasourcesApi(config);
            var offset = 0;  // int? | <p>Number of Datasources to omit from the start of the result set.</p> (optional)  (default to 0)
            var limit = 50;  // int? | <p>Maximum number of Datasources to return. Default is 50.</p> (optional)  (default to 50)

            try
            {
                // Get Global Datasource
                DatasourcesList result = apiInstance.GlobalDatasourcesGetDatasources(offset, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetDatasources: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalDatasourcesGetDatasourcesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Global Datasource
    ApiResponse<DatasourcesList> response = apiInstance.GlobalDatasourcesGetDatasourcesWithHttpInfo(offset, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetDatasourcesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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

<a id="globaldatasourcesgetdelimiteddatastream"></a>
# **GlobalDatasourcesGetDelimitedDataStream**
> void GlobalDatasourcesGetDelimitedDataStream (bool? includeHeaders = null, bool? metaDataOnly = null, DatasourceQueryInfo body = null)

Get Streamed Datasource Results

<p>Returns results in stream from a global-level Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalDatasourcesGetDelimitedDataStreamExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalDatasourcesApi(config);
            var includeHeaders = false;  // bool? | <p>Include headers.</p> (optional)  (default to false)
            var metaDataOnly = false;  // bool? | <p>Metadata Only.</p> (optional)  (default to false)
            var body = new DatasourceQueryInfo(); // DatasourceQueryInfo | <p>Query information.</p> (optional) 

            try
            {
                // Get Streamed Datasource Results
                apiInstance.GlobalDatasourcesGetDelimitedDataStream(includeHeaders, metaDataOnly, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetDelimitedDataStream: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalDatasourcesGetDelimitedDataStreamWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Streamed Datasource Results
    apiInstance.GlobalDatasourcesGetDelimitedDataStreamWithHttpInfo(includeHeaders, metaDataOnly, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetDelimitedDataStreamWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **includeHeaders** | **bool?** | &lt;p&gt;Include headers.&lt;/p&gt; | [optional] [default to false] |
| **metaDataOnly** | **bool?** | &lt;p&gt;Metadata Only.&lt;/p&gt; | [optional] [default to false] |
| **body** | [**DatasourceQueryInfo**](DatasourceQueryInfo.md) | &lt;p&gt;Query information.&lt;/p&gt; | [optional]  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Results fetched successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to stream results.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="globaldatasourcesgetresults"></a>
# **GlobalDatasourcesGetResults**
> ResultBean GlobalDatasourcesGetResults (int? pageSize = null, DatasourceQueryInfo body = null)

Stream Datasource Results

<p>Returns column headers of the Datasource, and a link to fetch the streamed results in CSV (comma-separated) or TSV (tab-separated) formats.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalDatasourcesGetResultsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalDatasourcesApi(config);
            var pageSize = -1;  // int? | <p>Number of records to return. If not passed, all records are returned.</p> (optional)  (default to -1)
            var body = new DatasourceQueryInfo(); // DatasourceQueryInfo | <p>Query information.</p> (optional) 

            try
            {
                // Stream Datasource Results
                ResultBean result = apiInstance.GlobalDatasourcesGetResults(pageSize, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetResults: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalDatasourcesGetResultsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Stream Datasource Results
    ApiResponse<ResultBean> response = apiInstance.GlobalDatasourcesGetResultsWithHttpInfo(pageSize, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesGetResultsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pageSize** | **int?** | &lt;p&gt;Number of records to return. If not passed, all records are returned.&lt;/p&gt; | [optional] [default to -1] |
| **body** | [**DatasourceQueryInfo**](DatasourceQueryInfo.md) | &lt;p&gt;Query information.&lt;/p&gt; | [optional]  |

### Return type

[**ResultBean**](ResultBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Streaming information returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get streaming information.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="globaldatasourcesupdatedatasource"></a>
# **GlobalDatasourcesUpdateDatasource**
> Datasource GlobalDatasourcesUpdateDatasource (string datasouceName, Datasource body = null)

Update Global Datasource

<p>Update the named global-level Datasource. If the update is successful, returns details about the updated Datasource. <code>type</code> and <code>connection</code> are required inputs for all types of Datasources. Other required inputs differ based on the type of the Datasource.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalDatasourcesUpdateDatasourceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalDatasourcesApi(config);
            var datasouceName = "datasouceName_example";  // string | <p>Datasource name.</p>
            var body = new Datasource(); // Datasource | <p>Updated Datasource details.</p> (optional) 

            try
            {
                // Update Global Datasource
                Datasource result = apiInstance.GlobalDatasourcesUpdateDatasource(datasouceName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesUpdateDatasource: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalDatasourcesUpdateDatasourceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Global Datasource
    ApiResponse<Datasource> response = apiInstance.GlobalDatasourcesUpdateDatasourceWithHttpInfo(datasouceName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalDatasourcesApi.GlobalDatasourcesUpdateDatasourceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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

