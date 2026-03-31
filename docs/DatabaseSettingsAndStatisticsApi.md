# EssSharp.Api.DatabaseSettingsAndStatisticsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DatabaseSettingsStatisticsExportQueryData**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsexportquerydata) | **POST** /applications/{applicationName}/databases/{databaseName}/settings/querytracking/export | Export Query Tracking |
| [**DatabaseSettingsStatisticsGetBuffersSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetbufferssettings) | **GET** /applications/{applicationName}/databases/{databaseName}/settings/buffers | Get Buffer Settings |
| [**DatabaseSettingsStatisticsGetCachesSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetcachessettings) | **GET** /applications/{applicationName}/databases/{databaseName}/settings/caches | Get Cache Settings |
| [**DatabaseSettingsStatisticsGetCalculationSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetcalculationsettings) | **GET** /applications/{applicationName}/databases/{databaseName}/settings/calculation | Get Calculation Settings |
| [**DatabaseSettingsStatisticsGetCompressSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetcompresssettings) | **GET** /applications/{applicationName}/databases/{databaseName}/settings/compression | Get Compression Settings |
| [**DatabaseSettingsStatisticsGetCompressionInfoSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetcompressioninfosettings) | **GET** /applications/{applicationName}/databases/{databaseName}/settings/compressioninfo | Get ASO Compression Info |
| [**DatabaseSettingsStatisticsGetOutlineAttributesSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetoutlineattributessettings) | **GET** /applications/{applicationName}/databases/{databaseName}/settings/outline/attributes | Get Attribute Settings |
| [**DatabaseSettingsStatisticsGetOutlineSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetoutlinesettings) | **GET** /applications/{applicationName}/databases/{databaseName}/settings/outline | Get Outline Settings |
| [**DatabaseSettingsStatisticsGetOutlineSettingsDateFormats**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetoutlinesettingsdateformats) | **GET** /applications/{applicationName}/databases/{databaseName}/settings/outline/dateformats | Get Date Formats |
| [**DatabaseSettingsStatisticsGetRuntimeStats**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetruntimestats) | **GET** /applications/{applicationName}/databases/{databaseName}/statistics/runtime | Get Runtime Statistics |
| [**DatabaseSettingsStatisticsGetSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetsettings) | **GET** /applications/{applicationName}/databases/{databaseName}/settings | Get General Settings |
| [**DatabaseSettingsStatisticsGetStartupSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetstartupsettings) | **GET** /applications/{applicationName}/databases/{databaseName}/settings/startup | Get Startup Settings |
| [**DatabaseSettingsStatisticsGetStatistics**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetstatistics) | **GET** /applications/{applicationName}/databases/{databaseName}/statistics | Get General Statistics |
| [**DatabaseSettingsStatisticsGetStorageStats**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgetstoragestats) | **GET** /applications/{applicationName}/databases/{databaseName}/statistics/storage | Get Storage Statistics |
| [**DatabaseSettingsStatisticsGetTransSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsgettranssettings) | **GET** /applications/{applicationName}/databases/{databaseName}/settings/transactions | Get Transaction Settings |
| [**DatabaseSettingsStatisticsImportQueryData**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsimportquerydata) | **POST** /applications/{applicationName}/databases/{databaseName}/settings/querytracking/import | Import Query Tracking |
| [**DatabaseSettingsStatisticsUpdateOutlineSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsupdateoutlinesettings) | **PATCH** /applications/{applicationName}/databases/{databaseName}/settings/outline | Update Outline Settings |
| [**DatabaseSettingsStatisticsUpdateSettings**](DatabaseSettingsAndStatisticsApi.md#databasesettingsstatisticsupdatesettings) | **PATCH** /applications/{applicationName}/databases/{databaseName}/settings | Update Settings |

<a id="databasesettingsstatisticsexportquerydata"></a>
# **DatabaseSettingsStatisticsExportQueryData**
> void DatabaseSettingsStatisticsExportQueryData (string applicationName, string databaseName, QueryTrackingInputs body)

Export Query Tracking

<p>Export query data from an aggregate storage (ASO) database to a text file. Essbase tracks query data to optimize aggregate views based on usage.</p><p>When an ASO cube is refreshed or restarted, query data is not persisted. As an optimization technique, before refreshing or restarting, you can export query tracking data to a text file. To rebuild aggregate views after a refresh or restart, import the query tracking data from the text file. Essbase uses the query data to select the most appropriate set of aggregate views to materialize.</p> <p>To perform this operation, query tracking must be enabled for the current ASO cube. Query tracking is enabled by default.  To ensure that query tracking is enabled, use <a href=\"./op-applications-applicationname-databases-databasename-settings-get.html\">Get General Settings</a> and ensure that <code>queryTracking</code> is <b>true</b>.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsExportQueryDataExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new QueryTrackingInputs(); // QueryTrackingInputs | <p>File name.</p>

            try
            {
                // Export Query Tracking
                apiInstance.DatabaseSettingsStatisticsExportQueryData(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsExportQueryData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsExportQueryDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Export Query Tracking
    apiInstance.DatabaseSettingsStatisticsExportQueryDataWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsExportQueryDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**QueryTrackingInputs**](QueryTrackingInputs.md) | &lt;p&gt;File name.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Query data  exported successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to export query data.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetbufferssettings"></a>
# **DatabaseSettingsStatisticsGetBuffersSettings**
> BufferSettings DatabaseSettingsStatisticsGetBuffersSettings (string applicationName, string databaseName)

Get Buffer Settings

<p>Returns buffer settings of the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetBuffersSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Buffer Settings
                BufferSettings result = apiInstance.DatabaseSettingsStatisticsGetBuffersSettings(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetBuffersSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetBuffersSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Buffer Settings
    ApiResponse<BufferSettings> response = apiInstance.DatabaseSettingsStatisticsGetBuffersSettingsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetBuffersSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**BufferSettings**](BufferSettings.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Buffer settings retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the buffer settings. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetcachessettings"></a>
# **DatabaseSettingsStatisticsGetCachesSettings**
> CacheSettings DatabaseSettingsStatisticsGetCachesSettings (string applicationName, string databaseName)

Get Cache Settings

<p>Returns cache settings of the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetCachesSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Cache Settings
                CacheSettings result = apiInstance.DatabaseSettingsStatisticsGetCachesSettings(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetCachesSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetCachesSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Cache Settings
    ApiResponse<CacheSettings> response = apiInstance.DatabaseSettingsStatisticsGetCachesSettingsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetCachesSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**CacheSettings**](CacheSettings.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Cache settings returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the cache settings. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetcalculationsettings"></a>
# **DatabaseSettingsStatisticsGetCalculationSettings**
> CalculationSettings DatabaseSettingsStatisticsGetCalculationSettings (string applicationName, string databaseName)

Get Calculation Settings

<p>Returns calculation settings of the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetCalculationSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Calculation Settings
                CalculationSettings result = apiInstance.DatabaseSettingsStatisticsGetCalculationSettings(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetCalculationSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetCalculationSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Calculation Settings
    ApiResponse<CalculationSettings> response = apiInstance.DatabaseSettingsStatisticsGetCalculationSettingsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetCalculationSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**CalculationSettings**](CalculationSettings.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Calculation settings retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the calculation settings. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetcompresssettings"></a>
# **DatabaseSettingsStatisticsGetCompressSettings**
> CompressionSettings DatabaseSettingsStatisticsGetCompressSettings (string applicationName, string databaseName)

Get Compression Settings

<p>Returns compression settings of the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetCompressSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Compression Settings
                CompressionSettings result = apiInstance.DatabaseSettingsStatisticsGetCompressSettings(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetCompressSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetCompressSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Compression Settings
    ApiResponse<CompressionSettings> response = apiInstance.DatabaseSettingsStatisticsGetCompressSettingsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetCompressSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**CompressionSettings**](CompressionSettings.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Compression settings retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the compression settings. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetcompressioninfosettings"></a>
# **DatabaseSettingsStatisticsGetCompressionInfoSettings**
> CompressionInfoOutput DatabaseSettingsStatisticsGetCompressionInfoSettings (string applicationName, string databaseName, bool? fetch = null)

Get ASO Compression Info

<p>Returns estimated compression information for the ASO cube when different dimensions are hypothetically used as the compression dimension. These estimates can help you choose the best dimension to use as the compression dimension.</p><p>If you include query parameter <code>fetch=true</code>, this API initiates an internal system job which evaluates the whole cube in terms of ASO compression. To monitor the job progress, you can use <a href=\"./op-jobs-get.html\">Get Job List</a>. The job type is listed as <b>ASO Compression information</b>, and the job stores the compression information. This job is not available to rerun. When any user calls this API without the <code>fetch=true</code> parameter, the result is retrieved from the stored system job. To re-evaluate the cube for ASO compression information, call the API again with <code>fetch=true</code>.</p><br><table><tr><th>Column Name</th><th>Description</th></tr><tr><td><strong>dimensionName</strong></td><td>Each dimension name in the cube, hypothetically considered to be the compression dimension.</td></tr><tr><td><strong>isCompression</strong></td><td>Indicates whether the dimension is the ASO compression dimension. There can be only one compression dimension in an ASO cube.</td></tr><tr><td><strong>storedLevel0Members</strong></td><td>The number of leaf-level members in the dimension. A large number of stored  level-0 members in a dimension indicates that it may not perform well as a compression dimension.</td></tr><tr><td><strong>averageBundleFill</strong></td><td>Estimated average number of values per compression dimension bundle. Choosing a  compression dimension that has a higher average bundle fill means that the cube compresses better.</td></tr><tr><td><strong>averageValueLength</strong></td><td>Estimated average number of bytes required to store a value. Dimensions with a  smaller average value length compress the cube better.</td></tr><tr><td><strong>level0MB</strong></td><td>Estimated size of the compressed cube, in megabytes. A smaller expected level-0 size indicates that choosing this dimension enables better compression.<br><br>Except for the scenario in which there is no compression dimension (<i>No Compression Dimension</i>), all estimates assume that all pages are compressed. As compressed pages require additional overhead that uncompressed pages do not, the estimated level-0 cube size for some dimensions may be larger than the value for <i>No Compression Dimension</i>.</td></tr></table>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetCompressionInfoSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database (cube) name.</p>
            var fetch = false;  // bool? | <p>Set fetch parameter to true if you need to re-evaluate compression info. The default value is false.</p> (optional)  (default to false)

            try
            {
                // Get ASO Compression Info
                CompressionInfoOutput result = apiInstance.DatabaseSettingsStatisticsGetCompressionInfoSettings(applicationName, databaseName, fetch);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetCompressionInfoSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetCompressionInfoSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get ASO Compression Info
    ApiResponse<CompressionInfoOutput> response = apiInstance.DatabaseSettingsStatisticsGetCompressionInfoSettingsWithHttpInfo(applicationName, databaseName, fetch);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetCompressionInfoSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database (cube) name.&lt;/p&gt; |  |
| **fetch** | **bool?** | &lt;p&gt;Set fetch parameter to true if you need to re-evaluate compression info. The default value is false.&lt;/p&gt; | [optional] [default to false] |

### Return type

[**CompressionInfoOutput**](CompressionInfoOutput.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Compression info retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get compression info. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetoutlineattributessettings"></a>
# **DatabaseSettingsStatisticsGetOutlineAttributesSettings**
> AttributeOutlineSettings DatabaseSettingsStatisticsGetOutlineAttributesSettings (string applicationName, string databaseName, string connectionName = null, string applicationNameForConnection = null)

Get Attribute Settings

<p>Returns attribute settings of the outline from the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetOutlineAttributesSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var connectionName = "connectionName_example";  // string | <p>Essbase connection name.</p> (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string | <p>Application name for connection.</p> (optional) 

            try
            {
                // Get Attribute Settings
                AttributeOutlineSettings result = apiInstance.DatabaseSettingsStatisticsGetOutlineAttributesSettings(applicationName, databaseName, connectionName, applicationNameForConnection);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetOutlineAttributesSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetOutlineAttributesSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Attribute Settings
    ApiResponse<AttributeOutlineSettings> response = apiInstance.DatabaseSettingsStatisticsGetOutlineAttributesSettingsWithHttpInfo(applicationName, databaseName, connectionName, applicationNameForConnection);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetOutlineAttributesSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **connectionName** | **string** | &lt;p&gt;Essbase connection name.&lt;/p&gt; | [optional]  |
| **applicationNameForConnection** | **string** | &lt;p&gt;Application name for connection.&lt;/p&gt; | [optional]  |

### Return type

[**AttributeOutlineSettings**](AttributeOutlineSettings.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Attribute settings returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the attribute settings. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetoutlinesettings"></a>
# **DatabaseSettingsStatisticsGetOutlineSettings**
> OutlineSettingsList DatabaseSettingsStatisticsGetOutlineSettings (string applicationName, string databaseName, string connectionName = null, string applicationNameForConnection = null, string expand = null)

Get Outline Settings

<p>Returns outline settings of the specified database. Additional settings can be retrieved using the <code>expand</code> parameter.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetOutlineSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var connectionName = "connectionName_example";  // string | <p>Essbase connection name.</p> (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string | <p>Application name for connection.</p> (optional) 
            var expand = "none";  // string | <p>Use <code>none</code> to show only general outline settings (this is the default). Other options available are <code>attribute</code> and <code>all</code>. (optional)  (default to none)

            try
            {
                // Get Outline Settings
                OutlineSettingsList result = apiInstance.DatabaseSettingsStatisticsGetOutlineSettings(applicationName, databaseName, connectionName, applicationNameForConnection, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetOutlineSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetOutlineSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Outline Settings
    ApiResponse<OutlineSettingsList> response = apiInstance.DatabaseSettingsStatisticsGetOutlineSettingsWithHttpInfo(applicationName, databaseName, connectionName, applicationNameForConnection, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetOutlineSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **connectionName** | **string** | &lt;p&gt;Essbase connection name.&lt;/p&gt; | [optional]  |
| **applicationNameForConnection** | **string** | &lt;p&gt;Application name for connection.&lt;/p&gt; | [optional]  |
| **expand** | **string** | &lt;p&gt;Use &lt;code&gt;none&lt;/code&gt; to show only general outline settings (this is the default). Other options available are &lt;code&gt;attribute&lt;/code&gt; and &lt;code&gt;all&lt;/code&gt;. | [optional] [default to none] |

### Return type

[**OutlineSettingsList**](OutlineSettingsList.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Database outline settings returned successfully, with links to get expanded settings.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the settings. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetoutlinesettingsdateformats"></a>
# **DatabaseSettingsStatisticsGetOutlineSettingsDateFormats**
> OutlineDateFormat DatabaseSettingsStatisticsGetOutlineSettingsDateFormats (string applicationName, string databaseName)

Get Date Formats

<p>Returns date formats of the outline from the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetOutlineSettingsDateFormatsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Date Formats
                OutlineDateFormat result = apiInstance.DatabaseSettingsStatisticsGetOutlineSettingsDateFormats(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetOutlineSettingsDateFormats: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetOutlineSettingsDateFormatsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Date Formats
    ApiResponse<OutlineDateFormat> response = apiInstance.DatabaseSettingsStatisticsGetOutlineSettingsDateFormatsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetOutlineSettingsDateFormatsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**OutlineDateFormat**](OutlineDateFormat.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Date formats retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the date formats. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetruntimestats"></a>
# **DatabaseSettingsStatisticsGetRuntimeStats**
> RuntimeStatistics DatabaseSettingsStatisticsGetRuntimeStats (string applicationName, string databaseName)

Get Runtime Statistics

<p>Returns runtime statistics of the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetRuntimeStatsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Runtime Statistics
                RuntimeStatistics result = apiInstance.DatabaseSettingsStatisticsGetRuntimeStats(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetRuntimeStats: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetRuntimeStatsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Runtime Statistics
    ApiResponse<RuntimeStatistics> response = apiInstance.DatabaseSettingsStatisticsGetRuntimeStatsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetRuntimeStatsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**RuntimeStatistics**](RuntimeStatistics.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Database runtime statistics returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get database runtime statistics. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetsettings"></a>
# **DatabaseSettingsStatisticsGetSettings**
> SettingsList DatabaseSettingsStatisticsGetSettings (string applicationName, string databaseName, string expand = null)

Get General Settings

<p>Returns general settings of the specified database. Additional groups of settings can be expanded using the <code>expand</code> parameter. If <code>expand</code> is none, links are returned for the additional groups of settings.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var expand = "none";  // string | <p>Use <code>none</code> to show only general settings, with links for other grouped settings. Other expansion options available: <ul><li><code>startup</code> (block storage cubes only)</li><li><code>calculation</code> (block storage only)</li><li><code>buffers</code></li><li><code>caches</code> (block storage only)</li><li><code>transactions</code> (block storage only)</li><li><code>compression</code> (aggregate storage only)</li><li><code>all</code></li></ul></p> (optional)  (default to none)

            try
            {
                // Get General Settings
                SettingsList result = apiInstance.DatabaseSettingsStatisticsGetSettings(applicationName, databaseName, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get General Settings
    ApiResponse<SettingsList> response = apiInstance.DatabaseSettingsStatisticsGetSettingsWithHttpInfo(applicationName, databaseName, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **expand** | **string** | &lt;p&gt;Use &lt;code&gt;none&lt;/code&gt; to show only general settings, with links for other grouped settings. Other expansion options available: &lt;ul&gt;&lt;li&gt;&lt;code&gt;startup&lt;/code&gt; (block storage cubes only)&lt;/li&gt;&lt;li&gt;&lt;code&gt;calculation&lt;/code&gt; (block storage only)&lt;/li&gt;&lt;li&gt;&lt;code&gt;buffers&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;caches&lt;/code&gt; (block storage only)&lt;/li&gt;&lt;li&gt;&lt;code&gt;transactions&lt;/code&gt; (block storage only)&lt;/li&gt;&lt;li&gt;&lt;code&gt;compression&lt;/code&gt; (aggregate storage only)&lt;/li&gt;&lt;li&gt;&lt;code&gt;all&lt;/code&gt;&lt;/li&gt;&lt;/ul&gt;&lt;/p&gt; | [optional] [default to none] |

### Return type

[**SettingsList**](SettingsList.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;General database settings returned successfully, with links to get expanded settings.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get database settings. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetstartupsettings"></a>
# **DatabaseSettingsStatisticsGetStartupSettings**
> StartupSettings DatabaseSettingsStatisticsGetStartupSettings (string applicationName, string databaseName)

Get Startup Settings

<p>Returns startup settings of the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetStartupSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Startup Settings
                StartupSettings result = apiInstance.DatabaseSettingsStatisticsGetStartupSettings(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetStartupSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetStartupSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Startup Settings
    ApiResponse<StartupSettings> response = apiInstance.DatabaseSettingsStatisticsGetStartupSettingsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetStartupSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**StartupSettings**](StartupSettings.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Startup settings returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;p&gt;Failed to return the startup settings. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetstatistics"></a>
# **DatabaseSettingsStatisticsGetStatistics**
> StatisticsList DatabaseSettingsStatisticsGetStatistics (string applicationName, string databaseName, string expand = null)

Get General Statistics

<p>Returns general statistics of the specified database. Additional statistics can be retrieved using the <code>expand</code> parameter.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetStatisticsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var expand = "none";  // string | <p>Use <code>none</code> to show only general statistics (this is the default). Other options available: <code>storage</code>, <code>runtime</code>, and <code>all</code>.</p> (optional)  (default to none)

            try
            {
                // Get General Statistics
                StatisticsList result = apiInstance.DatabaseSettingsStatisticsGetStatistics(applicationName, databaseName, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetStatistics: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetStatisticsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get General Statistics
    ApiResponse<StatisticsList> response = apiInstance.DatabaseSettingsStatisticsGetStatisticsWithHttpInfo(applicationName, databaseName, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetStatisticsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **expand** | **string** | &lt;p&gt;Use &lt;code&gt;none&lt;/code&gt; to show only general statistics (this is the default). Other options available: &lt;code&gt;storage&lt;/code&gt;, &lt;code&gt;runtime&lt;/code&gt;, and &lt;code&gt;all&lt;/code&gt;.&lt;/p&gt; | [optional] [default to none] |

### Return type

[**StatisticsList**](StatisticsList.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;General database statistics returned successfully, with links to get expanded statistics.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the statistics. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgetstoragestats"></a>
# **DatabaseSettingsStatisticsGetStorageStats**
> StorageStatistics DatabaseSettingsStatisticsGetStorageStats (string applicationName, string databaseName)

Get Storage Statistics

<p>Returns storage statistics of the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetStorageStatsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Storage Statistics
                StorageStatistics result = apiInstance.DatabaseSettingsStatisticsGetStorageStats(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetStorageStats: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetStorageStatsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Storage Statistics
    ApiResponse<StorageStatistics> response = apiInstance.DatabaseSettingsStatisticsGetStorageStatsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetStorageStatsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**StorageStatistics**](StorageStatistics.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Database storage statistics returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get storage statistics. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsgettranssettings"></a>
# **DatabaseSettingsStatisticsGetTransSettings**
> TransactionSettings DatabaseSettingsStatisticsGetTransSettings (string applicationName, string databaseName)

Get Transaction Settings

<p>Returns transaction settings of the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsGetTransSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Transaction Settings
                TransactionSettings result = apiInstance.DatabaseSettingsStatisticsGetTransSettings(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetTransSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsGetTransSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Transaction Settings
    ApiResponse<TransactionSettings> response = apiInstance.DatabaseSettingsStatisticsGetTransSettingsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsGetTransSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**TransactionSettings**](TransactionSettings.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Transaction settings returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the transaction settings. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsimportquerydata"></a>
# **DatabaseSettingsStatisticsImportQueryData**
> void DatabaseSettingsStatisticsImportQueryData (string applicationName, string databaseName, QueryTrackingInputs body)

Import Query Tracking

<p>Import query tracking data from a text file to an Essbase aggregate storage (ASO) database. Essbase tracks query data to optimize aggregate views based on usage.</p><p>When an ASO cube is refreshed or restarted, query data is not persisted. As an optimization technique, before refreshing or restarting, you can export query tracking data to a text file. To rebuild aggregate views after a refresh or restart, import the query tracking data from the text file. Essbase uses the query data to select the most appropriate set of aggregate views to materialize.</p> <p>To perform this operation, query tracking must be enabled for the current ASO cube. Query tracking is enabled by default.  To ensure that query tracking is enabled, use <a href=\"./op-applications-applicationname-databases-databasename-settings-get.html\">Get General Settings</a> and ensure that <code>queryTracking</code> is <b>true</b>.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsImportQueryDataExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new QueryTrackingInputs(); // QueryTrackingInputs | <p>File name.</p>

            try
            {
                // Import Query Tracking
                apiInstance.DatabaseSettingsStatisticsImportQueryData(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsImportQueryData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsImportQueryDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Import Query Tracking
    apiInstance.DatabaseSettingsStatisticsImportQueryDataWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsImportQueryDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**QueryTrackingInputs**](QueryTrackingInputs.md) | &lt;p&gt;File name.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Query data imported successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to import query data.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsupdateoutlinesettings"></a>
# **DatabaseSettingsStatisticsUpdateOutlineSettings**
> void DatabaseSettingsStatisticsUpdateOutlineSettings (string applicationName, string databaseName, List<PatchElement> body)

Update Outline Settings

<p>Updates the outline settings of the specified database. This operation has limited capacity. For more outline update options, refer to the <b>otlUpdate</b> action in <a href=\"./op-applications-application-databases-database-boe-post.html\">Run Batch Outline Edit</a> API.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsUpdateOutlineSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new List<PatchElement>(); // List<PatchElement> | <p>Outline settings patch list.</p>

            try
            {
                // Update Outline Settings
                apiInstance.DatabaseSettingsStatisticsUpdateOutlineSettings(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsUpdateOutlineSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsUpdateOutlineSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Outline Settings
    apiInstance.DatabaseSettingsStatisticsUpdateOutlineSettingsWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsUpdateOutlineSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**List&lt;PatchElement&gt;**](PatchElement.md) | &lt;p&gt;Outline settings patch list.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Outline settings updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the settings. The application or database name may be incorrect, or the JSON for the settings may be incorrect.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="databasesettingsstatisticsupdatesettings"></a>
# **DatabaseSettingsStatisticsUpdateSettings**
> void DatabaseSettingsStatisticsUpdateSettings (string applicationName, string databaseName, List<PatchElement> body)

Update Settings

<p>Updates the settings of the specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSettingsStatisticsUpdateSettingsExample
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

            var apiInstance = new DatabaseSettingsAndStatisticsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new List<PatchElement>(); // List<PatchElement> | <p>Database settings patch list.</p>

            try
            {
                // Update Settings
                apiInstance.DatabaseSettingsStatisticsUpdateSettings(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsUpdateSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSettingsStatisticsUpdateSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Settings
    apiInstance.DatabaseSettingsStatisticsUpdateSettingsWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSettingsAndStatisticsApi.DatabaseSettingsStatisticsUpdateSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**List&lt;PatchElement&gt;**](PatchElement.md) | &lt;p&gt;Database settings patch list.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Settings updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the settings. The application or database name may be incorrect, or the JSON for the settings may be incorrect.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

