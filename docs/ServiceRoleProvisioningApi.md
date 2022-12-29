# EssSharp.Api.ServiceRoleProvisioningApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ServiceRoleProvisioningDeprovision**](ServiceRoleProvisioningApi.md#serviceroleprovisioningdeprovision) | **DELETE** /permissions/{id} | Deprovision |
| [**ServiceRoleProvisioningGetProvision**](ServiceRoleProvisioningApi.md#serviceroleprovisioninggetprovision) | **GET** /permissions/{id} | Get Provision |
| [**ServiceRoleProvisioningProvision**](ServiceRoleProvisioningApi.md#serviceroleprovisioningprovision) | **PUT** /permissions/{id} | Provision User or Group |
| [**ServiceRoleProvisioningSearchProvision**](ServiceRoleProvisioningApi.md#serviceroleprovisioningsearchprovision) | **GET** /permissions | Search Provision |

<a name="serviceroleprovisioningdeprovision"></a>
# **ServiceRoleProvisioningDeprovision**
> void ServiceRoleProvisioningDeprovision (string id, bool? group = null)

Deprovision

<p>Deprovisions a single user or group from a service role.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ServiceRoleProvisioningDeprovisionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServiceRoleProvisioningApi(config);
            var id = "id_example";  // string | <p>User or group ID.</p>
            var group = false;  // bool? | <p>If true, ID is for a group. If false, ID is for a user. Default is false (ID is considered to be for a user.)</p> (optional)  (default to false)

            try
            {
                // Deprovision
                apiInstance.ServiceRoleProvisioningDeprovision(id, group);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServiceRoleProvisioningApi.ServiceRoleProvisioningDeprovision: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServiceRoleProvisioningDeprovisionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Deprovision
    apiInstance.ServiceRoleProvisioningDeprovisionWithHttpInfo(id, group);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServiceRoleProvisioningApi.ServiceRoleProvisioningDeprovisionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User or group deprovisioned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the required service administrator role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="serviceroleprovisioninggetprovision"></a>
# **ServiceRoleProvisioningGetProvision**
> UserGroupProvisionInfo ServiceRoleProvisioningGetProvision (string id, bool? group = null)

Get Provision

<p>Gets service role provisioning information.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ServiceRoleProvisioningGetProvisionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServiceRoleProvisioningApi(config);
            var id = "id_example";  // string | <p>User or group ID.</p>
            var group = false;  // bool? | <p>If true, ID is for a group. If false, ID is for a user. Default is false (ID is considered to be for a user.)</p> (optional)  (default to false)

            try
            {
                // Get Provision
                UserGroupProvisionInfo result = apiInstance.ServiceRoleProvisioningGetProvision(id, group);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServiceRoleProvisioningApi.ServiceRoleProvisioningGetProvision: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServiceRoleProvisioningGetProvisionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Provision
    ApiResponse<UserGroupProvisionInfo> response = apiInstance.ServiceRoleProvisioningGetProvisionWithHttpInfo(id, group);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServiceRoleProvisioningApi.ServiceRoleProvisioningGetProvisionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;User or group ID.&lt;/p&gt; |  |
| **group** | **bool?** | &lt;p&gt;If true, ID is for a group. If false, ID is for a user. Default is false (ID is considered to be for a user.)&lt;/p&gt; | [optional] [default to false] |

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
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the required service administrator role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="serviceroleprovisioningprovision"></a>
# **ServiceRoleProvisioningProvision**
> void ServiceRoleProvisioningProvision (string id, UserGroupProvisionInfo body = null)

Provision User or Group

<p>Provisions a single user or group for a service role.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ServiceRoleProvisioningProvisionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServiceRoleProvisioningApi(config);
            var id = "id_example";  // string | <p>User or group ID.</p>
            var body = new UserGroupProvisionInfo(); // UserGroupProvisionInfo | <p>User or group provisioning information.</p> (optional) 

            try
            {
                // Provision User or Group
                apiInstance.ServiceRoleProvisioningProvision(id, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServiceRoleProvisioningApi.ServiceRoleProvisioningProvision: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServiceRoleProvisioningProvisionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Provision User or Group
    apiInstance.ServiceRoleProvisioningProvisionWithHttpInfo(id, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServiceRoleProvisioningApi.ServiceRoleProvisioningProvisionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User or group provisioned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the required service administrator role.&lt;/p&gt; |  -  |
| **409** | &lt;p&gt;&lt;strong&gt;Conflict&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The user/group being provisioned is stale and it has been removed from identity store&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="serviceroleprovisioningsearchprovision"></a>
# **ServiceRoleProvisioningSearchProvision**
> UserGroupProvisionInfoList ServiceRoleProvisioningSearchProvision (string id = null, string role = null, string filter = null, int? page = null)

Search Provision

<p>Search for provisioning information.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ServiceRoleProvisioningSearchProvisionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServiceRoleProvisioningApi(config);
            var id = "\"*\"";  // string | <p>User or group ID wildcard pattern. if specified, returns users and groups matching the pattern, if not specified, returns all the users and groups having some role. Users or groups having no role are not returned.</p> (optional)  (default to "*")
            var role = "\"all\"";  // string | <p>Input may include <code>all</code>, <code>none</code>, or a comma-separated list of roles (for example, <code>service_administrator</code>, <code>power_user</code>, or <code>user</code>). Default value is <code>all</code>, so if this query parameter is not specified, all users and groups having some role are returned. If <code>none</code> is specified, only users and groups having no role will be returned. If named roles are specified, then only users and groups having any of the named roles are returned.</p> (optional)  (default to "all")
            var filter = "\"all\"";  // string | <p>Search filter. Values available: <code>all/group/user</code>. Default value is <code>all</code>, so if this query parameter is not specified, then all users and groups having some role will be returned.</p> (optional)  (default to "all")
            var page = -1;  // int? | <p>This is used to get the list of users/groups having no service roles in paginated manner. Page number starts with 0</p> (optional)  (default to -1)

            try
            {
                // Search Provision
                UserGroupProvisionInfoList result = apiInstance.ServiceRoleProvisioningSearchProvision(id, role, filter, page);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServiceRoleProvisioningApi.ServiceRoleProvisioningSearchProvision: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ServiceRoleProvisioningSearchProvisionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Search Provision
    ApiResponse<UserGroupProvisionInfoList> response = apiInstance.ServiceRoleProvisioningSearchProvisionWithHttpInfo(id, role, filter, page);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServiceRoleProvisioningApi.ServiceRoleProvisioningSearchProvisionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;User or group ID wildcard pattern. if specified, returns users and groups matching the pattern, if not specified, returns all the users and groups having some role. Users or groups having no role are not returned.&lt;/p&gt; | [optional] [default to &quot;*&quot;] |
| **role** | **string** | &lt;p&gt;Input may include &lt;code&gt;all&lt;/code&gt;, &lt;code&gt;none&lt;/code&gt;, or a comma-separated list of roles (for example, &lt;code&gt;service_administrator&lt;/code&gt;, &lt;code&gt;power_user&lt;/code&gt;, or &lt;code&gt;user&lt;/code&gt;). Default value is &lt;code&gt;all&lt;/code&gt;, so if this query parameter is not specified, all users and groups having some role are returned. If &lt;code&gt;none&lt;/code&gt; is specified, only users and groups having no role will be returned. If named roles are specified, then only users and groups having any of the named roles are returned.&lt;/p&gt; | [optional] [default to &quot;all&quot;] |
| **filter** | **string** | &lt;p&gt;Search filter. Values available: &lt;code&gt;all/group/user&lt;/code&gt;. Default value is &lt;code&gt;all&lt;/code&gt;, so if this query parameter is not specified, then all users and groups having some role will be returned.&lt;/p&gt; | [optional] [default to &quot;all&quot;] |
| **page** | **int?** | &lt;p&gt;This is used to get the list of users/groups having no service roles in paginated manner. Page number starts with 0&lt;/p&gt; | [optional] [default to -1] |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User and group provisioning results returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the required service administrator role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

