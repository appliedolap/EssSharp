# EssSharp.Api.GridApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GridExecute**](GridApi.md#gridexecute) | **POST** /applications/{applicationName}/databases/{databaseName}/grid | Execute Grid Operation |
| [**GridExecuteLayout**](GridApi.md#gridexecutelayout) | **GET** /applications/{applicationName}/databases/{databaseName}/grid/layout/{layout} | Execute Grid Layout |
| [**GridExecuteMDX**](GridApi.md#gridexecutemdx) | **POST** /applications/{applicationName}/databases/{databaseName}/grid/mdx | Execute MDX Report |
| [**GridGetDefault**](GridApi.md#gridgetdefault) | **GET** /applications/{applicationName}/databases/{databaseName}/grid | Get Default Grid |
| [**GridGetLayoutGrid**](GridApi.md#gridgetlayoutgrid) | **POST** /applications/{applicationName}/databases/{databaseName}/grid/layout | Get Layout Grid |

<a name="gridexecute"></a>
# **GridExecute**
> Grid GridExecute (string applicationName, string databaseName, GridOperation body = null)

Execute Grid Operation

<p>Returns the grid for specified operation. Supported grid operations are Zoom In (zoomin), Zoom Out (zoomout), Refresh (refresh), Keep Only (keeponly), Remove Only (removeonly), Submit (submit), Pivot (pivot), and Pivot To POV (pivotToPOV).</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GridExecuteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GridApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name for grid operation.</p>
            var databaseName = "databaseName_example";  // string | <p>Database/Cube name for grid operation.</p>
            var body = new GridOperation(); // GridOperation | <p>Grid Operation to be performed.</p> (optional) 

            try
            {
                // Execute Grid Operation
                Grid result = apiInstance.GridExecute(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GridApi.GridExecute: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GridExecuteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Execute Grid Operation
    ApiResponse<Grid> response = apiInstance.GridExecuteWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GridApi.GridExecuteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name for grid operation.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database/Cube name for grid operation.&lt;/p&gt; |  |
| **body** | [**GridOperation**](GridOperation.md) | &lt;p&gt;Grid Operation to be performed.&lt;/p&gt; | [optional]  |

### Return type

[**Grid**](Grid.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Grid operation completed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to execute grid operation. The application name or database name may be incorrect or missing.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gridexecutelayout"></a>
# **GridExecuteLayout**
> Grid GridExecuteLayout (string applicationName, string databaseName, string layout, string user = null)

Execute Grid Layout

<p>Renders the grid for the specified layout.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GridExecuteLayoutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GridApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name for grid layout.</p>
            var databaseName = "databaseName_example";  // string | <p>Database/Cube name for grid layout.</p>
            var layout = "layout_example";  // string | <p>Layout name to be executed.</p>
            var user = "user_example";  // string | <p>Owner of the layout.</p> (optional) 

            try
            {
                // Execute Grid Layout
                Grid result = apiInstance.GridExecuteLayout(applicationName, databaseName, layout, user);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GridApi.GridExecuteLayout: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GridExecuteLayoutWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Execute Grid Layout
    ApiResponse<Grid> response = apiInstance.GridExecuteLayoutWithHttpInfo(applicationName, databaseName, layout, user);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GridApi.GridExecuteLayoutWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name for grid layout.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database/Cube name for grid layout.&lt;/p&gt; |  |
| **layout** | **string** | &lt;p&gt;Layout name to be executed.&lt;/p&gt; |  |
| **user** | **string** | &lt;p&gt;Owner of the layout.&lt;/p&gt; | [optional]  |

### Return type

[**Grid**](Grid.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Grid layout rendered successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Validation failed. The application, database, or layout name may be missing or  incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gridexecutemdx"></a>
# **GridExecuteMDX**
> Grid GridExecuteMDX (string applicationName, string databaseName, MDXOperation body)

Execute MDX Report

<p>Returns an output grid from the specified MDX report.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GridExecuteMDXExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GridApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name for grid.</p>
            var databaseName = "databaseName_example";  // string | <p>Database/Cube name for grid.</p>
            var body = new MDXOperation(); // MDXOperation | <p>MDX report for grid.</p>

            try
            {
                // Execute MDX Report
                Grid result = apiInstance.GridExecuteMDX(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GridApi.GridExecuteMDX: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GridExecuteMDXWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Execute MDX Report
    ApiResponse<Grid> response = apiInstance.GridExecuteMDXWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GridApi.GridExecuteMDXWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name for grid.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database/Cube name for grid.&lt;/p&gt; |  |
| **body** | [**MDXOperation**](MDXOperation.md) | &lt;p&gt;MDX report for grid.&lt;/p&gt; |  |

### Return type

[**Grid**](Grid.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Grid returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Validation failed. The application, database, or MDX report name may be missing or incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gridgetdefault"></a>
# **GridGetDefault**
> Grid GridGetDefault (string applicationName, string databaseName, bool? reset = null)

Get Default Grid

<p>Returns the default grid layout for the specified cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GridGetDefaultExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GridApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name for default grid.</p>
            var databaseName = "databaseName_example";  // string | <p>Database/Cube name for default grid.</p>
            var reset = false;  // bool? | <p>Reset flag to avoid saved grid layout.</p> (optional)  (default to false)

            try
            {
                // Get Default Grid
                Grid result = apiInstance.GridGetDefault(applicationName, databaseName, reset);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GridApi.GridGetDefault: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GridGetDefaultWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Default Grid
    ApiResponse<Grid> response = apiInstance.GridGetDefaultWithHttpInfo(applicationName, databaseName, reset);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GridApi.GridGetDefaultWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name for default grid.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database/Cube name for default grid.&lt;/p&gt; |  |
| **reset** | **bool?** | &lt;p&gt;Reset flag to avoid saved grid layout.&lt;/p&gt; | [optional] [default to false] |

### Return type

[**Grid**](Grid.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Grid returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Validation failed. The application or database name may be missing or incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gridgetlayoutgrid"></a>
# **GridGetLayoutGrid**
> Grid GridGetLayoutGrid (string applicationName, string databaseName, Grid body)

Get Layout Grid

<p>Returns the layout grid for the current grid to be saved as layout for the specified cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GridGetLayoutGridExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GridApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name for default grid.</p>
            var databaseName = "databaseName_example";  // string | <p>Database/Cube name for default grid.</p>
            var body = new Grid(); // Grid | <p>The current grid displayed.</p>

            try
            {
                // Get Layout Grid
                Grid result = apiInstance.GridGetLayoutGrid(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GridApi.GridGetLayoutGrid: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GridGetLayoutGridWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Layout Grid
    ApiResponse<Grid> response = apiInstance.GridGetLayoutGridWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GridApi.GridGetLayoutGridWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name for default grid.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database/Cube name for default grid.&lt;/p&gt; |  |
| **body** | [**Grid**](Grid.md) | &lt;p&gt;The current grid displayed.&lt;/p&gt; |  |

### Return type

[**Grid**](Grid.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Layout grid returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Validation failed. The application or database name may be missing or  incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

