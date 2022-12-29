# EssSharp.Api.LayoutsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**LayoutsDeleteLayout**](LayoutsApi.md#layoutsdeletelayout) | **DELETE** /applications/{application}/databases/{database}/layouts/{layout} | Delete Layout |
| [**LayoutsEditLayout**](LayoutsApi.md#layoutseditlayout) | **PUT** /applications/{application}/databases/{database}/layouts/{layout} | Update Layout |
| [**LayoutsGetLayoutDetails**](LayoutsApi.md#layoutsgetlayoutdetails) | **GET** /applications/{application}/databases/{database}/layouts/{layout} | Get Layout Details |
| [**LayoutsGetLayouts**](LayoutsApi.md#layoutsgetlayouts) | **GET** /applications/{application}/databases/{database}/layouts | List Layouts |
| [**LayoutsMarkDefaultLayout**](LayoutsApi.md#layoutsmarkdefaultlayout) | **POST** /applications/{application}/databases/{database}/layouts/{layout}/actions/markDefault | Mark Layout as Default |
| [**LayoutsSaveLayout**](LayoutsApi.md#layoutssavelayout) | **POST** /applications/{application}/databases/{database}/layouts | Save Layout |

<a name="layoutsdeletelayout"></a>
# **LayoutsDeleteLayout**
> void LayoutsDeleteLayout (string application, string database, string layout, string user = null)

Delete Layout

<p>Deletes the layout in the specified cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LayoutsDeleteLayoutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LayoutsApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var layout = "layout_example";  // string | <p>Layout name.</p>
            var user = "user_example";  // string | <p>User name.</p> (optional) 

            try
            {
                // Delete Layout
                apiInstance.LayoutsDeleteLayout(application, database, layout, user);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LayoutsApi.LayoutsDeleteLayout: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LayoutsDeleteLayoutWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Layout
    apiInstance.LayoutsDeleteLayoutWithHttpInfo(application, database, layout, user);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LayoutsApi.LayoutsDeleteLayoutWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **layout** | **string** | &lt;p&gt;Layout name.&lt;/p&gt; |  |
| **user** | **string** | &lt;p&gt;User name.&lt;/p&gt; | [optional]  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Layout deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete layout.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="layoutseditlayout"></a>
# **LayoutsEditLayout**
> Layout LayoutsEditLayout (string application, string database, string layout, string user = null, Layout body = null)

Update Layout

<p>Updates the layout with the provided details in the specified cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LayoutsEditLayoutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LayoutsApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var layout = "layout_example";  // string | <p>Layout name.</p>
            var user = "user_example";  // string | <p>User name.</p> (optional) 
            var body = new Layout(); // Layout | <p>Layout details to be updated.</p> (optional) 

            try
            {
                // Update Layout
                Layout result = apiInstance.LayoutsEditLayout(application, database, layout, user, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LayoutsApi.LayoutsEditLayout: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LayoutsEditLayoutWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Layout
    ApiResponse<Layout> response = apiInstance.LayoutsEditLayoutWithHttpInfo(application, database, layout, user, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LayoutsApi.LayoutsEditLayoutWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **layout** | **string** | &lt;p&gt;Layout name.&lt;/p&gt; |  |
| **user** | **string** | &lt;p&gt;User name.&lt;/p&gt; | [optional]  |
| **body** | [**Layout**](Layout.md) | &lt;p&gt;Layout details to be updated.&lt;/p&gt; | [optional]  |

### Return type

[**Layout**](Layout.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Layout updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update layout.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="layoutsgetlayoutdetails"></a>
# **LayoutsGetLayoutDetails**
> Layout LayoutsGetLayoutDetails (string application, string database, string layout, string user = null)

Get Layout Details

<p>Gets the details for the specified layout.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LayoutsGetLayoutDetailsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LayoutsApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var layout = "layout_example";  // string | <p>Layout name.</p>
            var user = "user_example";  // string | <p>User name.</p> (optional) 

            try
            {
                // Get Layout Details
                Layout result = apiInstance.LayoutsGetLayoutDetails(application, database, layout, user);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LayoutsApi.LayoutsGetLayoutDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LayoutsGetLayoutDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Layout Details
    ApiResponse<Layout> response = apiInstance.LayoutsGetLayoutDetailsWithHttpInfo(application, database, layout, user);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LayoutsApi.LayoutsGetLayoutDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **layout** | **string** | &lt;p&gt;Layout name.&lt;/p&gt; |  |
| **user** | **string** | &lt;p&gt;User name.&lt;/p&gt; | [optional]  |

### Return type

[**Layout**](Layout.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Layout details returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get layout details.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="layoutsgetlayouts"></a>
# **LayoutsGetLayouts**
> Layouts LayoutsGetLayouts (string application, string database)

List Layouts

<p>Lists the available saved grid layouts. If you are a service administrator, all saved layouts are listed, including those created by other users.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LayoutsGetLayoutsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LayoutsApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>

            try
            {
                // List Layouts
                Layouts result = apiInstance.LayoutsGetLayouts(application, database);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LayoutsApi.LayoutsGetLayouts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LayoutsGetLayoutsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Layouts
    ApiResponse<Layouts> response = apiInstance.LayoutsGetLayoutsWithHttpInfo(application, database);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LayoutsApi.LayoutsGetLayoutsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**Layouts**](Layouts.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;List of layouts returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get layouts.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="layoutsmarkdefaultlayout"></a>
# **LayoutsMarkDefaultLayout**
> Layout LayoutsMarkDefaultLayout (string application, string database, string layout, DefaultLayoutBean body = null)

Mark Layout as Default

<p>Marks a saved grid layout as the default grid view for this user or this cube. Requires Database Access permission to set the user default layout, and Database Manager permission to set the cube default layout.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LayoutsMarkDefaultLayoutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LayoutsApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var layout = "layout_example";  // string | <p>Saved grid layout name.</p>
            var body = new DefaultLayoutBean(); // DefaultLayoutBean | <p>User default or database default details.</p> (optional) 

            try
            {
                // Mark Layout as Default
                Layout result = apiInstance.LayoutsMarkDefaultLayout(application, database, layout, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LayoutsApi.LayoutsMarkDefaultLayout: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LayoutsMarkDefaultLayoutWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Mark Layout as Default
    ApiResponse<Layout> response = apiInstance.LayoutsMarkDefaultLayoutWithHttpInfo(application, database, layout, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LayoutsApi.LayoutsMarkDefaultLayoutWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **layout** | **string** | &lt;p&gt;Saved grid layout name.&lt;/p&gt; |  |
| **body** | [**DefaultLayoutBean**](DefaultLayoutBean.md) | &lt;p&gt;User default or database default details.&lt;/p&gt; | [optional]  |

### Return type

[**Layout**](Layout.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Layout successfully marked as default.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to mark layout as user default or database default.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="layoutssavelayout"></a>
# **LayoutsSaveLayout**
> Layout LayoutsSaveLayout (string application, string database, Layout body = null)

Save Layout

<p>Saves a grid layout for the specified cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LayoutsSaveLayoutExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LayoutsApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var body = new Layout(); // Layout | <p>Grid to be saved as a layout.</p> (optional) 

            try
            {
                // Save Layout
                Layout result = apiInstance.LayoutsSaveLayout(application, database, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LayoutsApi.LayoutsSaveLayout: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LayoutsSaveLayoutWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Save Layout
    ApiResponse<Layout> response = apiInstance.LayoutsSaveLayoutWithHttpInfo(application, database, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LayoutsApi.LayoutsSaveLayoutWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**Layout**](Layout.md) | &lt;p&gt;Grid to be saved as a layout.&lt;/p&gt; | [optional]  |

### Return type

[**Layout**](Layout.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Layout saved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to save layout.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

