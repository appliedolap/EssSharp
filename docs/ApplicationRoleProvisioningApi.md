# EssSharp.Api.ApplicationRoleProvisioningApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ApplicationRoleProvisioningDeprovision**](ApplicationRoleProvisioningApi.md#applicationroleprovisioningdeprovision) | **DELETE** /applications/{app}/permissions/{id} | Deprovision User or Group |
| [**ApplicationRoleProvisioningGetProvision**](ApplicationRoleProvisioningApi.md#applicationroleprovisioninggetprovision) | **GET** /applications/{app}/permissions/{id} | Get Provision |
| [**ApplicationRoleProvisioningImportProvision**](ApplicationRoleProvisioningApi.md#applicationroleprovisioningimportprovision) | **POST** /applications/{app}/permissions | Import Provision |
| [**ApplicationRoleProvisioningProvision**](ApplicationRoleProvisioningApi.md#applicationroleprovisioningprovision) | **PUT** /applications/{app}/permissions/{id} | Provision User or Group |
| [**ApplicationRoleProvisioningSearchProvision**](ApplicationRoleProvisioningApi.md#applicationroleprovisioningsearchprovision) | **GET** /applications/{app}/permissions | Search Application Provisioning |

<a id="applicationroleprovisioningdeprovision"></a>
# **ApplicationRoleProvisioningDeprovision**
> void ApplicationRoleProvisioningDeprovision (string app, string id, bool? group = null)

Deprovision User or Group

<p>Deprovisions a single user or group on the specified application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationRoleProvisioningDeprovisionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationRoleProvisioningApi(config);
            var app = "app_example";  // string | <p>Application name.</p>
            var id = "id_example";  // string | <p>User or group ID.</p>
            var group = false;  // bool? | <p>If true, ID is for a group. If false, ID is for a user. Default is false (ID is considered to be for a user.)</p> (optional)  (default to false)

            try
            {
                // Deprovision User or Group
                apiInstance.ApplicationRoleProvisioningDeprovision(app, id, group);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationRoleProvisioningApi.ApplicationRoleProvisioningDeprovision: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationRoleProvisioningDeprovisionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Deprovision User or Group
    apiInstance.ApplicationRoleProvisioningDeprovisionWithHttpInfo(app, id, group);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationRoleProvisioningApi.ApplicationRoleProvisioningDeprovisionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **id** | **string** | &lt;p&gt;User or group ID.&lt;/p&gt; |  |
| **group** | **bool?** | &lt;p&gt;If true, ID is for a group. If false, ID is for a user. Default is false (ID is considered to be for a user.)&lt;/p&gt; | [optional] [default to false] |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Deprovisioned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="applicationroleprovisioninggetprovision"></a>
# **ApplicationRoleProvisioningGetProvision**
> UserGroupProvisionInfo ApplicationRoleProvisioningGetProvision (string app, string id, bool? group = null, bool? inherited = null)

Get Provision

<p>Gets provisioning information on the specified application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationRoleProvisioningGetProvisionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationRoleProvisioningApi(config);
            var app = "app_example";  // string | <p>Application name.</p>
            var id = "id_example";  // string | <p>User or group ID.</p>
            var group = false;  // bool? | <p>If true, ID is for a group. If false, ID is for a user. Default is false (ID is considered to be for a user.)</p> (optional)  (default to false)
            var inherited = false;  // bool? | <p>If true, consider roles derived through parent groups. Default is false.</p> (optional)  (default to false)

            try
            {
                // Get Provision
                UserGroupProvisionInfo result = apiInstance.ApplicationRoleProvisioningGetProvision(app, id, group, inherited);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationRoleProvisioningApi.ApplicationRoleProvisioningGetProvision: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationRoleProvisioningGetProvisionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Provision
    ApiResponse<UserGroupProvisionInfo> response = apiInstance.ApplicationRoleProvisioningGetProvisionWithHttpInfo(app, id, group, inherited);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationRoleProvisioningApi.ApplicationRoleProvisioningGetProvisionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **id** | **string** | &lt;p&gt;User or group ID.&lt;/p&gt; |  |
| **group** | **bool?** | &lt;p&gt;If true, ID is for a group. If false, ID is for a user. Default is false (ID is considered to be for a user.)&lt;/p&gt; | [optional] [default to false] |
| **inherited** | **bool?** | &lt;p&gt;If true, consider roles derived through parent groups. Default is false.&lt;/p&gt; | [optional] [default to false] |

### Return type

[**UserGroupProvisionInfo**](UserGroupProvisionInfo.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioning information returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="applicationroleprovisioningimportprovision"></a>
# **ApplicationRoleProvisioningImportProvision**
> void ApplicationRoleProvisioningImportProvision (string app)

Import Provision

<p>Imports provisioning information for multiple users or groups on the specified application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationRoleProvisioningImportProvisionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationRoleProvisioningApi(config);
            var app = "app_example";  // string | <p>Application name.</p>

            try
            {
                // Import Provision
                apiInstance.ApplicationRoleProvisioningImportProvision(app);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationRoleProvisioningApi.ApplicationRoleProvisioningImportProvision: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationRoleProvisioningImportProvisionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Import Provision
    apiInstance.ApplicationRoleProvisioningImportProvisionWithHttpInfo(app);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationRoleProvisioningApi.ApplicationRoleProvisioningImportProvisionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioning imported successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="applicationroleprovisioningprovision"></a>
# **ApplicationRoleProvisioningProvision**
> void ApplicationRoleProvisioningProvision (string app, string id, UserGroupProvisionInfo body = null)

Provision User or Group

<p>Provisions a single user or group on the specified application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationRoleProvisioningProvisionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationRoleProvisioningApi(config);
            var app = "app_example";  // string | <p>Application name.</p>
            var id = "id_example";  // string | <p>User or group ID.</p>
            var body = new UserGroupProvisionInfo(); // UserGroupProvisionInfo | <p>User or group provisioning information.</p> (optional) 

            try
            {
                // Provision User or Group
                apiInstance.ApplicationRoleProvisioningProvision(app, id, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationRoleProvisioningApi.ApplicationRoleProvisioningProvision: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationRoleProvisioningProvisionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Provision User or Group
    apiInstance.ApplicationRoleProvisioningProvisionWithHttpInfo(app, id, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationRoleProvisioningApi.ApplicationRoleProvisioningProvisionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **id** | **string** | &lt;p&gt;User or group ID.&lt;/p&gt; |  |
| **body** | [**UserGroupProvisionInfo**](UserGroupProvisionInfo.md) | &lt;p&gt;User or group provisioning information.&lt;/p&gt; | [optional]  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **409** | &lt;p&gt;&lt;strong&gt;Conflict&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The user/group being provisioned is stale and it has been removed from identity store&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="applicationroleprovisioningsearchprovision"></a>
# **ApplicationRoleProvisioningSearchProvision**
> UserGroupProvisionInfoList ApplicationRoleProvisioningSearchProvision (string app, string id = null, string role = null, string filter = null, bool? inherited = null)

Search Application Provisioning

<p>Search for provisioning information on the specified application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationRoleProvisioningSearchProvisionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationRoleProvisioningApi(config);
            var app = "app_example";  // string | <p>Application name.</p>
            var id = "\"*\"";  // string | <p>User or group ID wildcard pattern. if specified, returns users and groups matching the pattern. If not specified, returns all users and groups having some role. Users or groups having no role are not returned.</p> (optional)  (default to "*")
            var role = "\"all\"";  // string | <p>Input may include <code>all</code>, <code>none</code>, or a comma-separated list of roles (for example, <code>app_manager</code>, <code>db_manager</code>, <code>db_update</code>,or <code>db_access</code>). Default value is <code>all</code>, so if this query parameter is not specified, all users and groups having some role are returned. If <code>none</code> is specified, only users and groups having no role will be returned. If named roles are specified, then only users and groups having any of the named roles are returned.</p> (optional)  (default to "all")
            var filter = "\"all\"";  // string | <p>Input may include <code>all</code>, <code>group</code>, or <code>user</code>. Default value is <code>all</code>, so if this query parameter is not specified, all users and groups are returned.</p> (optional)  (default to "all")
            var inherited = false;  // bool? | <p>If true, consider roles derived through parent groups. Default is false.</p> (optional)  (default to false)

            try
            {
                // Search Application Provisioning
                UserGroupProvisionInfoList result = apiInstance.ApplicationRoleProvisioningSearchProvision(app, id, role, filter, inherited);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationRoleProvisioningApi.ApplicationRoleProvisioningSearchProvision: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationRoleProvisioningSearchProvisionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Search Application Provisioning
    ApiResponse<UserGroupProvisionInfoList> response = apiInstance.ApplicationRoleProvisioningSearchProvisionWithHttpInfo(app, id, role, filter, inherited);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationRoleProvisioningApi.ApplicationRoleProvisioningSearchProvisionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **id** | **string** | &lt;p&gt;User or group ID wildcard pattern. if specified, returns users and groups matching the pattern. If not specified, returns all users and groups having some role. Users or groups having no role are not returned.&lt;/p&gt; | [optional] [default to &quot;*&quot;] |
| **role** | **string** | &lt;p&gt;Input may include &lt;code&gt;all&lt;/code&gt;, &lt;code&gt;none&lt;/code&gt;, or a comma-separated list of roles (for example, &lt;code&gt;app_manager&lt;/code&gt;, &lt;code&gt;db_manager&lt;/code&gt;, &lt;code&gt;db_update&lt;/code&gt;,or &lt;code&gt;db_access&lt;/code&gt;). Default value is &lt;code&gt;all&lt;/code&gt;, so if this query parameter is not specified, all users and groups having some role are returned. If &lt;code&gt;none&lt;/code&gt; is specified, only users and groups having no role will be returned. If named roles are specified, then only users and groups having any of the named roles are returned.&lt;/p&gt; | [optional] [default to &quot;all&quot;] |
| **filter** | **string** | &lt;p&gt;Input may include &lt;code&gt;all&lt;/code&gt;, &lt;code&gt;group&lt;/code&gt;, or &lt;code&gt;user&lt;/code&gt;. Default value is &lt;code&gt;all&lt;/code&gt;, so if this query parameter is not specified, all users and groups are returned.&lt;/p&gt; | [optional] [default to &quot;all&quot;] |
| **inherited** | **bool?** | &lt;p&gt;If true, consider roles derived through parent groups. Default is false.&lt;/p&gt; | [optional] [default to false] |

### Return type

[**UserGroupProvisionInfoList**](UserGroupProvisionInfoList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Successfully returned provisioning information for users or groups matching search criteria.  Response type can be either JSON, XML, or CSV stream, depending on the Accept header. If &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;, the search result will be returned in the response body. If &lt;code&gt;Accept&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt;, the search result will be returned as a stream.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

