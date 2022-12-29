# EssSharp.Api.FiltersApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**FiltersAddFilterPermission**](FiltersApi.md#filtersaddfilterpermission) | **POST** /applications/{applicationName}/databases/{databaseName}/filters/{filterName}/permissions | Add Permissions |
| [**FiltersCopyFilter**](FiltersApi.md#filterscopyfilter) | **POST** /applications/{applicationName}/databases/{databaseName}/filters/filterops/copy | Copy Filter |
| [**FiltersCreateFilter**](FiltersApi.md#filterscreatefilter) | **POST** /applications/{applicationName}/databases/{databaseName}/filters | Create Filter |
| [**FiltersDeleteFilter**](FiltersApi.md#filtersdeletefilter) | **DELETE** /applications/{applicationName}/databases/{databaseName}/filters/{filterName} | Delete Filter |
| [**FiltersEditFilter**](FiltersApi.md#filterseditfilter) | **PUT** /applications/{applicationName}/databases/{databaseName}/filters/{filterName} | Update Filter |
| [**FiltersGetFilter**](FiltersApi.md#filtersgetfilter) | **GET** /applications/{applicationName}/databases/{databaseName}/filters/{filterName} | Get Filter |
| [**FiltersGetFilterPermissions**](FiltersApi.md#filtersgetfilterpermissions) | **GET** /applications/{applicationName}/databases/{databaseName}/filters/{filterName}/permissions | Get Permissions |
| [**FiltersGetFilterRows**](FiltersApi.md#filtersgetfilterrows) | **GET** /applications/{applicationName}/databases/{databaseName}/filters/{filterName}/rows | Get Filter Rows |
| [**FiltersListFilters**](FiltersApi.md#filterslistfilters) | **GET** /applications/{applicationName}/databases/{databaseName}/filters | Get Filters |
| [**FiltersRemoveFilterPermission**](FiltersApi.md#filtersremovefilterpermission) | **DELETE** /applications/{applicationName}/databases/{databaseName}/filters/{filterName}/permissions/{userGroupId} | Delete Permission |
| [**FiltersRenameFilter**](FiltersApi.md#filtersrenamefilter) | **POST** /applications/{applicationName}/databases/{databaseName}/filters/filterops/rename | Rename Filter |
| [**FiltersReplaceFilterPermission**](FiltersApi.md#filtersreplacefilterpermission) | **PUT** /applications/{applicationName}/databases/{databaseName}/filters/{filterName}/permissions | Edit Permissions |
| [**FiltersValidateFilter**](FiltersApi.md#filtersvalidatefilter) | **POST** /applications/{applicationName}/databases/{databaseName}/filters/filterops/validate | Validate Filter |

<a name="filtersaddfilterpermission"></a>
# **FiltersAddFilterPermission**
> UserGroupProvisionInfo FiltersAddFilterPermission (string applicationName, string databaseName, string filterName, UserGroupProvisionInfo body)

Add Permissions

<p>Adds permissions, in the named filter, for the named user or group.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersAddFilterPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var filterName = "filterName_example";  // string | <p>Filter name.</p>
            var body = new UserGroupProvisionInfo(); // UserGroupProvisionInfo | <p>User or group details.</p>

            try
            {
                // Add Permissions
                UserGroupProvisionInfo result = apiInstance.FiltersAddFilterPermission(applicationName, databaseName, filterName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersAddFilterPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersAddFilterPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Permissions
    ApiResponse<UserGroupProvisionInfo> response = apiInstance.FiltersAddFilterPermissionWithHttpInfo(applicationName, databaseName, filterName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersAddFilterPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **filterName** | **string** | &lt;p&gt;Filter name.&lt;/p&gt; |  |
| **body** | [**UserGroupProvisionInfo**](UserGroupProvisionInfo.md) | &lt;p&gt;User or group details.&lt;/p&gt; |  |

### Return type

[**UserGroupProvisionInfo**](UserGroupProvisionInfo.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The filter permissions were added successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to add the filter permissions. The application name, database name, filter name, or user or group name may be incorrect, or the specified user or group may not have enough privileges.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filterscopyfilter"></a>
# **FiltersCopyFilter**
> FilterBean FiltersCopyFilter (string applicationName, string databaseName, FilterCopy body)

Copy Filter

<p>Copies a filter in the specified application and database and returns the new filter.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersCopyFilterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new FilterCopy(); // FilterCopy | Filter copy details

            try
            {
                // Copy Filter
                FilterBean result = apiInstance.FiltersCopyFilter(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersCopyFilter: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersCopyFilterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Copy Filter
    ApiResponse<FilterBean> response = apiInstance.FiltersCopyFilterWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersCopyFilterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**FilterCopy**](FilterCopy.md) | Filter copy details |  |

### Return type

[**FilterBean**](FilterBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The filter was copied successfully. Returns filter details and links to get, edit, or delete the filter, and to get the filter rows.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to copy the filter. The application name, database name, or filter name may be incorrect, or a filter with the specified name already exists.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filterscreatefilter"></a>
# **FiltersCreateFilter**
> FilterBean FiltersCreateFilter (string applicationName, string databaseName, FilterBean body)

Create Filter

<p>Creates the filter in the specified application and database and returns the created filter</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersCreateFilterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new FilterBean(); // FilterBean | <p>Filter details.</p>

            try
            {
                // Create Filter
                FilterBean result = apiInstance.FiltersCreateFilter(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersCreateFilter: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersCreateFilterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Filter
    ApiResponse<FilterBean> response = apiInstance.FiltersCreateFilterWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersCreateFilterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**FilterBean**](FilterBean.md) | &lt;p&gt;Filter details.&lt;/p&gt; |  |

### Return type

[**FilterBean**](FilterBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Filter was created successfully. Returns filter details and links to get, edit, or delete the filter, and to get the filter rows.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create filter. The application name or database name may be incorrect, or the filter may already exist.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filtersdeletefilter"></a>
# **FiltersDeleteFilter**
> void FiltersDeleteFilter (string applicationName, string databaseName, string filterName)

Delete Filter

<p>Deletes the named filter in the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersDeleteFilterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var filterName = "filterName_example";  // string | <p>Filter name.</p>

            try
            {
                // Delete Filter
                apiInstance.FiltersDeleteFilter(applicationName, databaseName, filterName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersDeleteFilter: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersDeleteFilterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Filter
    apiInstance.FiltersDeleteFilterWithHttpInfo(applicationName, databaseName, filterName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersDeleteFilterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **filterName** | **string** | &lt;p&gt;Filter name.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The filter was deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete the filter. The application name, database name, or filter name may be incorrect, or the named filter may not exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filterseditfilter"></a>
# **FiltersEditFilter**
> FilterBean FiltersEditFilter (string applicationName, string databaseName, string filterName, FilterBean body)

Update Filter

<p>Updates the named filter in the specified application and database, and returns the updated filter.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersEditFilterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var filterName = "filterName_example";  // string | <p>Filter name.</p>
            var body = new FilterBean(); // FilterBean | <p>Filter details.</p>

            try
            {
                // Update Filter
                FilterBean result = apiInstance.FiltersEditFilter(applicationName, databaseName, filterName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersEditFilter: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersEditFilterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Filter
    ApiResponse<FilterBean> response = apiInstance.FiltersEditFilterWithHttpInfo(applicationName, databaseName, filterName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersEditFilterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **filterName** | **string** | &lt;p&gt;Filter name.&lt;/p&gt; |  |
| **body** | [**FilterBean**](FilterBean.md) | &lt;p&gt;Filter details.&lt;/p&gt; |  |

### Return type

[**FilterBean**](FilterBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The filter was updated successfully, with links to get, edit, or delete the filter, and to get the filter rows.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the filter. The application name, database name, or filter name may be incorrect, or the named filter may not exist.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filtersgetfilter"></a>
# **FiltersGetFilter**
> FilterBean FiltersGetFilter (string applicationName, string databaseName, string filterName)

Get Filter

<p>Returns the named filter from the specified application and database.<p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersGetFilterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var filterName = "filterName_example";  // string | <p>Filter name.</p>

            try
            {
                // Get Filter
                FilterBean result = apiInstance.FiltersGetFilter(applicationName, databaseName, filterName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersGetFilter: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersGetFilterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Filter
    ApiResponse<FilterBean> response = apiInstance.FiltersGetFilterWithHttpInfo(applicationName, databaseName, filterName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersGetFilterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **filterName** | **string** | &lt;p&gt;Filter name.&lt;/p&gt; |  |

### Return type

[**FilterBean**](FilterBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The filter was retrieved successfully, with links to get, edit, or delete the filter, and to get the filter rows.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the filter. The application name, database name, or filter name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filtersgetfilterpermissions"></a>
# **FiltersGetFilterPermissions**
> List&lt;UserGroupProvisionInfoList&gt; FiltersGetFilterPermissions (string applicationName, string databaseName, string filterName)

Get Permissions

<p>Retrieves permissions associated with the named filter.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersGetFilterPermissionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var filterName = "filterName_example";  // string | <p>Filter name.</p>

            try
            {
                // Get Permissions
                List<UserGroupProvisionInfoList> result = apiInstance.FiltersGetFilterPermissions(applicationName, databaseName, filterName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersGetFilterPermissions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersGetFilterPermissionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Permissions
    ApiResponse<List<UserGroupProvisionInfoList>> response = apiInstance.FiltersGetFilterPermissionsWithHttpInfo(applicationName, databaseName, filterName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersGetFilterPermissionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **filterName** | **string** | &lt;p&gt;Filter name.&lt;/p&gt; |  |

### Return type

[**List&lt;UserGroupProvisionInfoList&gt;**](UserGroupProvisionInfoList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The filter permissions were retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the filter permissions. The application name, database name, or filter name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filtersgetfilterrows"></a>
# **FiltersGetFilterRows**
> FilterRowList FiltersGetFilterRows (string applicationName, string databaseName, string filterName)

Get Filter Rows

<p>Returns the access definition rows from the specified filter.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersGetFilterRowsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var filterName = "filterName_example";  // string | <p>Filter name.</p>

            try
            {
                // Get Filter Rows
                FilterRowList result = apiInstance.FiltersGetFilterRows(applicationName, databaseName, filterName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersGetFilterRows: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersGetFilterRowsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Filter Rows
    ApiResponse<FilterRowList> response = apiInstance.FiltersGetFilterRowsWithHttpInfo(applicationName, databaseName, filterName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersGetFilterRowsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **filterName** | **string** | &lt;p&gt;Filter name.&lt;/p&gt; |  |

### Return type

[**FilterRowList**](FilterRowList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Filter rows were retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get filter rows. The application name, database name, or filter name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filterslistfilters"></a>
# **FiltersListFilters**
> List&lt;FilterList&gt; FiltersListFilters (string applicationName, string databaseName)

Get Filters

<p>Returns all the filters from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersListFiltersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Filters
                List<FilterList> result = apiInstance.FiltersListFilters(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersListFilters: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersListFiltersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Filters
    ApiResponse<List<FilterList>> response = apiInstance.FiltersListFiltersWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersListFiltersWithHttpInfo: " + e.Message);
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

[**List&lt;FilterList&gt;**](FilterList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Filters were retrieved successfully. Returns filter details and links to get, edit, or delete the filters, and to get the filter rows.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get filters. The application name or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filtersremovefilterpermission"></a>
# **FiltersRemoveFilterPermission**
> void FiltersRemoveFilterPermission (string applicationName, string databaseName, string filterName, string userGroupId, bool group)

Delete Permission

<p>Removes permissions from a filter for the specified user or group.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersRemoveFilterPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var filterName = "filterName_example";  // string | <p>Filter name.</p>
            var userGroupId = "userGroupId_example";  // string | <p>User or group ID.</p>
            var group = false;  // bool | <p>True if the userGroupId refers to a group; false if it refers to a user.</p> (default to false)

            try
            {
                // Delete Permission
                apiInstance.FiltersRemoveFilterPermission(applicationName, databaseName, filterName, userGroupId, group);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersRemoveFilterPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersRemoveFilterPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Permission
    apiInstance.FiltersRemoveFilterPermissionWithHttpInfo(applicationName, databaseName, filterName, userGroupId, group);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersRemoveFilterPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **filterName** | **string** | &lt;p&gt;Filter name.&lt;/p&gt; |  |
| **userGroupId** | **string** | &lt;p&gt;User or group ID.&lt;/p&gt; |  |
| **group** | **bool** | &lt;p&gt;True if the userGroupId refers to a group; false if it refers to a user.&lt;/p&gt; | [default to false] |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The permission was removed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to remove permission from the filter. An input parameter may have been specified incorrectly.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filtersrenamefilter"></a>
# **FiltersRenameFilter**
> FilterBean FiltersRenameFilter (string applicationName, string databaseName, FilterCopy body)

Rename Filter

Renames the filter in the specified application and database and returns the created filter

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersRenameFilterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new FilterCopy(); // FilterCopy | <p>Filter rename details.</p>

            try
            {
                // Rename Filter
                FilterBean result = apiInstance.FiltersRenameFilter(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersRenameFilter: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersRenameFilterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Rename Filter
    ApiResponse<FilterBean> response = apiInstance.FiltersRenameFilterWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersRenameFilterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**FilterCopy**](FilterCopy.md) | &lt;p&gt;Filter rename details.&lt;/p&gt; |  |

### Return type

[**FilterBean**](FilterBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Filter was renamed successfully. Returns filter details and links to get, edit, or delete the filter, and to get the filter rows.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to rename filter. The application name or database name may be incorrect, or a filter with that name may already exist.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filtersreplacefilterpermission"></a>
# **FiltersReplaceFilterPermission**
> UserGroupProvisionInfo FiltersReplaceFilterPermission (string applicationName, string databaseName, string filterName, UserGroupProvisionInfoList body)

Edit Permissions

<p>Replaces all the users and groups assigned to the filter with a new list of users and groups.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersReplaceFilterPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var filterName = "filterName_example";  // string | <p>Filter name.</p>
            var body = new UserGroupProvisionInfoList(); // UserGroupProvisionInfoList | <p>User or group details.</p>

            try
            {
                // Edit Permissions
                UserGroupProvisionInfo result = apiInstance.FiltersReplaceFilterPermission(applicationName, databaseName, filterName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersReplaceFilterPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersReplaceFilterPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Edit Permissions
    ApiResponse<UserGroupProvisionInfo> response = apiInstance.FiltersReplaceFilterPermissionWithHttpInfo(applicationName, databaseName, filterName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersReplaceFilterPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **filterName** | **string** | &lt;p&gt;Filter name.&lt;/p&gt; |  |
| **body** | [**UserGroupProvisionInfoList**](UserGroupProvisionInfoList.md) | &lt;p&gt;User or group details.&lt;/p&gt; |  |

### Return type

[**UserGroupProvisionInfo**](UserGroupProvisionInfo.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The filter permissions were edited successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to modify the filter permissions. The application name, database name, filter name, or user or group name may be incorrect, or the specified user or group may not have enough privileges.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filtersvalidatefilter"></a>
# **FiltersValidateFilter**
> void FiltersValidateFilter (string applicationName, string databaseName, FilterBean body)

Validate Filter

<p>Validates the filter in the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FiltersValidateFilterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FiltersApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new FilterBean(); // FilterBean | <p>Filter details.</p>

            try
            {
                // Validate Filter
                apiInstance.FiltersValidateFilter(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FiltersApi.FiltersValidateFilter: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FiltersValidateFilterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Validate Filter
    apiInstance.FiltersValidateFilterWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FiltersApi.FiltersValidateFilterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**FilterBean**](FilterBean.md) | &lt;p&gt;Filter details.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The filter was validated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to validate the filter. The application name or database name may be incorrect, or the filter rows may be incomplete. |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

