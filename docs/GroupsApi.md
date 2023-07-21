# EssSharp.Api.GroupsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GroupsAdd**](GroupsApi.md#groupsadd) | **POST** /groups | Add or Import Group(s) |
| [**GroupsAddGroupMembersToGroup**](GroupsApi.md#groupsaddgroupmemberstogroup) | **POST** /groups/{id}/members/groups | Add Groups to Group |
| [**GroupsAddUserMembersToGroup**](GroupsApi.md#groupsaddusermemberstogroup) | **POST** /groups/{id}/members/users | Add Group Users |
| [**GroupsDelete**](GroupsApi.md#groupsdelete) | **DELETE** /groups/{id} | Delete Group |
| [**GroupsEdit**](GroupsApi.md#groupsedit) | **PUT** /groups/{id} | Update Group |
| [**GroupsGet**](GroupsApi.md#groupsget) | **GET** /groups/{id} | Get Group |
| [**GroupsGetGroupMembersOfGroup**](GroupsApi.md#groupsgetgroupmembersofgroup) | **GET** /groups/{id}/members/groups | Get Groups in Group |
| [**GroupsGetMembers**](GroupsApi.md#groupsgetmembers) | **GET** /groups/{id}/members | Get Group Members |
| [**GroupsGetUserMembersOfGroup**](GroupsApi.md#groupsgetusermembersofgroup) | **GET** /groups/{id}/members/users | Get Group Users |
| [**GroupsRemoveGroupMembersFromGroup**](GroupsApi.md#groupsremovegroupmembersfromgroup) | **DELETE** /groups/{id}/members/groups | Remove Groups From Group |
| [**GroupsRemoveUserMembersFromGroup**](GroupsApi.md#groupsremoveusermembersfromgroup) | **DELETE** /groups/{id}/members/users | Remove Group Users |
| [**GroupsSearch**](GroupsApi.md#groupssearch) | **GET** /groups | Search or Export Groups |
| [**UsersDeleteGroups**](GroupsApi.md#usersdeletegroups) | **POST** /groups/actions/delete | Delete Groups in File |

<a id="groupsadd"></a>
# **GroupsAdd**
> GroupBean GroupsAdd (GroupBean body)

Add or Import Group(s)

<p>Adds or imports one or more groups.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsAddExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var body = new GroupBean(); // GroupBean | <p>Group details to add group if header <code>Content-Type='application/json'</code> or <code>'application/xml'</code>;  otherwise, if header <code>Content-Type='application/octet-stream'</code>, CSV file to import group(s).</p>

            try
            {
                // Add or Import Group(s)
                GroupBean result = apiInstance.GroupsAdd(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsAdd: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsAddWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add or Import Group(s)
    ApiResponse<GroupBean> response = apiInstance.GroupsAddWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsAddWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**GroupBean**](GroupBean.md) | &lt;p&gt;Group details to add group if header &lt;code&gt;Content-Type&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;&#39;application/xml&#39;&lt;/code&gt;;  otherwise, if header &lt;code&gt;Content-Type&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt;, CSV file to import group(s).&lt;/p&gt; |  |

### Return type

[**GroupBean**](GroupBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Group added successfully. For &lt;code&gt;Content-Type&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;&#39;application/xml&#39;&lt;/code&gt;, added group is returned; otherwise, for &lt;code&gt;Content-Type&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt;, the group was imported successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsaddgroupmemberstogroup"></a>
# **GroupsAddGroupMembersToGroup**
> Groups GroupsAddGroupMembersToGroup (string id, List<string> body)

Add Groups to Group

<p>Adds multiple group members to a group.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsAddGroupMembersToGroupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var id = "id_example";  // string | <p>ID of group.</p>
            var body = new List<string>(); // List<string> | <p>Array of group IDs.</p>

            try
            {
                // Add Groups to Group
                Groups result = apiInstance.GroupsAddGroupMembersToGroup(id, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsAddGroupMembersToGroup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsAddGroupMembersToGroupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Groups to Group
    ApiResponse<Groups> response = apiInstance.GroupsAddGroupMembersToGroupWithHttpInfo(id, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsAddGroupMembersToGroupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of group.&lt;/p&gt; |  |
| **body** | [**List&lt;string&gt;**](string.md) | &lt;p&gt;Array of group IDs.&lt;/p&gt; |  |

### Return type

[**Groups**](Groups.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Group updated successfully. Returns link to get group members of a group.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsaddusermemberstogroup"></a>
# **GroupsAddUserMembersToGroup**
> Users GroupsAddUserMembersToGroup (string id, List<string> body)

Add Group Users

<p>Adds multiple user members to a group.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsAddUserMembersToGroupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var id = "id_example";  // string | <p>ID of group.</p>
            var body = new List<string>(); // List<string> | <p>Array of user IDs.</p>

            try
            {
                // Add Group Users
                Users result = apiInstance.GroupsAddUserMembersToGroup(id, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsAddUserMembersToGroup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsAddUserMembersToGroupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Group Users
    ApiResponse<Users> response = apiInstance.GroupsAddUserMembersToGroupWithHttpInfo(id, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsAddUserMembersToGroupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of group.&lt;/p&gt; |  |
| **body** | [**List&lt;string&gt;**](string.md) | &lt;p&gt;Array of user IDs.&lt;/p&gt; |  |

### Return type

[**Users**](Users.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Group updated successfully. Returns link to get user members of a group.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsdelete"></a>
# **GroupsDelete**
> void GroupsDelete (string id)

Delete Group

<p>Deletes a group.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var id = "id_example";  // string | <p>ID of group.</p>

            try
            {
                // Delete Group
                apiInstance.GroupsDelete(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Group
    apiInstance.GroupsDeleteWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsDeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of group.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Group deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsedit"></a>
# **GroupsEdit**
> GroupBean GroupsEdit (string id, GroupBean body)

Update Group

<p>Updates a group.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsEditExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var id = "id_example";  // string | <p>ID of group.</p>
            var body = new GroupBean(); // GroupBean | <p>Group details to update.</p>

            try
            {
                // Update Group
                GroupBean result = apiInstance.GroupsEdit(id, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsEdit: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsEditWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Group
    ApiResponse<GroupBean> response = apiInstance.GroupsEditWithHttpInfo(id, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsEditWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of group.&lt;/p&gt; |  |
| **body** | [**GroupBean**](GroupBean.md) | &lt;p&gt;Group details to update.&lt;/p&gt; |  |

### Return type

[**GroupBean**](GroupBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Group updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsget"></a>
# **GroupsGet**
> GroupBean GroupsGet (string id)

Get Group

<p>Gets group details.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var id = "id_example";  // string | <p>Group ID.</p>

            try
            {
                // Get Group
                GroupBean result = apiInstance.GroupsGet(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Group
    ApiResponse<GroupBean> response = apiInstance.GroupsGetWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsGetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;Group ID.&lt;/p&gt; |  |

### Return type

[**GroupBean**](GroupBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;Group details&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The group with that ID does not exist.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsgetgroupmembersofgroup"></a>
# **GroupsGetGroupMembersOfGroup**
> Groups GroupsGetGroupMembersOfGroup (string id)

Get Groups in Group

<p>Gets group members of a group.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsGetGroupMembersOfGroupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var id = "id_example";  // string | <p>ID of group.</p>

            try
            {
                // Get Groups in Group
                Groups result = apiInstance.GroupsGetGroupMembersOfGroup(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsGetGroupMembersOfGroup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsGetGroupMembersOfGroupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Groups in Group
    ApiResponse<Groups> response = apiInstance.GroupsGetGroupMembersOfGroupWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsGetGroupMembersOfGroupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of group.&lt;/p&gt; |  |

### Return type

[**Groups**](Groups.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Returns list of group members of a group.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsgetmembers"></a>
# **GroupsGetMembers**
> UserBean GroupsGetMembers (string id)

Get Group Members

<p>Returns links to get user and group members of a group.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsGetMembersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var id = "id_example";  // string | <p>ID of group.</p>

            try
            {
                // Get Group Members
                UserBean result = apiInstance.GroupsGetMembers(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsGetMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsGetMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Group Members
    ApiResponse<UserBean> response = apiInstance.GroupsGetMembersWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsGetMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of group.&lt;/p&gt; |  |

### Return type

[**UserBean**](UserBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User and group members of a group returned successfully.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsgetusermembersofgroup"></a>
# **GroupsGetUserMembersOfGroup**
> Users GroupsGetUserMembersOfGroup (string id)

Get Group Users

<p>Gets user members of a group.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsGetUserMembersOfGroupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var id = "id_example";  // string | <p>ID of group.</p>

            try
            {
                // Get Group Users
                Users result = apiInstance.GroupsGetUserMembersOfGroup(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsGetUserMembersOfGroup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsGetUserMembersOfGroupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Group Users
    ApiResponse<Users> response = apiInstance.GroupsGetUserMembersOfGroupWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsGetUserMembersOfGroupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of group.&lt;/p&gt; |  |

### Return type

[**Users**](Users.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Group member list returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsremovegroupmembersfromgroup"></a>
# **GroupsRemoveGroupMembersFromGroup**
> UserBean GroupsRemoveGroupMembersFromGroup (string id, List<string> body)

Remove Groups From Group

<p>Removes multiple group members from a group.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsRemoveGroupMembersFromGroupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var id = "id_example";  // string | <p>ID of group.</p>
            var body = new List<string>(); // List<string> | <p>Array of group IDs.</p>

            try
            {
                // Remove Groups From Group
                UserBean result = apiInstance.GroupsRemoveGroupMembersFromGroup(id, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsRemoveGroupMembersFromGroup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsRemoveGroupMembersFromGroupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove Groups From Group
    ApiResponse<UserBean> response = apiInstance.GroupsRemoveGroupMembersFromGroupWithHttpInfo(id, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsRemoveGroupMembersFromGroupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of group.&lt;/p&gt; |  |
| **body** | [**List&lt;string&gt;**](string.md) | &lt;p&gt;Array of group IDs.&lt;/p&gt; |  |

### Return type

[**UserBean**](UserBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Group updated successfully. Returns link to get group members of a group.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupsremoveusermembersfromgroup"></a>
# **GroupsRemoveUserMembersFromGroup**
> UserBean GroupsRemoveUserMembersFromGroup (string id, List<string> body)

Remove Group Users

Remove multiple user members from a group

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsRemoveUserMembersFromGroupExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var id = "id_example";  // string | <p>ID of group.</p>
            var body = new List<string>(); // List<string> | <p>Array of user IDs.</p>

            try
            {
                // Remove Group Users
                UserBean result = apiInstance.GroupsRemoveUserMembersFromGroup(id, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsRemoveUserMembersFromGroup: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsRemoveUserMembersFromGroupWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove Group Users
    ApiResponse<UserBean> response = apiInstance.GroupsRemoveUserMembersFromGroupWithHttpInfo(id, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsRemoveUserMembersFromGroupWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of group.&lt;/p&gt; |  |
| **body** | [**List&lt;string&gt;**](string.md) | &lt;p&gt;Array of user IDs.&lt;/p&gt; |  |

### Return type

[**UserBean**](UserBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Group updated successfully. Returns link to get user members of a group.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupssearch"></a>
# **GroupsSearch**
> Groups GroupsSearch (string filter = null, int? limit = null, string expand = null)

Search or Export Groups

<p>Gets a list of groups based on search results, or exports groups as CSV file.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupsSearchExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);
            var filter = "filter_example";  // string | <p>Group ID wildcard pattern. Filter by name of groups if header <code>Accept='application/json'</code> or <code>Accept='application/xml'</code>.</p> (optional) 
            var limit = -1;  // int? | <p>Maximum number of groups to return, if header <code>Accept='application/json'</code> or <code>Accept='application/xml'</code>.</p> (optional)  (default to -1)
            var expand = "\"all\"";  // string | <p>Value can be <code>all</code> or <code>none</code>. Default value is <code>all</code>, meaning service role and parent groups are returned for each group. If <code>none</code> is specified, service role and parent groups are not returned.</p> (optional)  (default to "all")

            try
            {
                // Search or Export Groups
                Groups result = apiInstance.GroupsSearch(filter, limit, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.GroupsSearch: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupsSearchWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Search or Export Groups
    ApiResponse<Groups> response = apiInstance.GroupsSearchWithHttpInfo(filter, limit, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.GroupsSearchWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **filter** | **string** | &lt;p&gt;Group ID wildcard pattern. Filter by name of groups if header &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; | [optional]  |
| **limit** | **int?** | &lt;p&gt;Maximum number of groups to return, if header &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; | [optional] [default to -1] |
| **expand** | **string** | &lt;p&gt;Value can be &lt;code&gt;all&lt;/code&gt; or &lt;code&gt;none&lt;/code&gt;. Default value is &lt;code&gt;all&lt;/code&gt;, meaning service role and parent groups are returned for each group. If &lt;code&gt;none&lt;/code&gt; is specified, service role and parent groups are not returned.&lt;/p&gt; | [optional] [default to &quot;all&quot;] |

### Return type

[**Groups**](Groups.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;List of groups matching search criteria. Response type can be either JSON, XML, or CSV stream, depending on the Accept header. If &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;, the search result is returned in the response. If &lt;code&gt;Accept&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt;, search result is returned as stream (not shown due to a Swagger limitation).&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="usersdeletegroups"></a>
# **UsersDeleteGroups**
> Object UsersDeleteGroups ()

Delete Groups in File

<p>Deletes the groups specified in the text file.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UsersDeleteGroupsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupsApi(config);

            try
            {
                // Delete Groups in File
                Object result = apiInstance.UsersDeleteGroups();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupsApi.UsersDeleteGroups: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UsersDeleteGroupsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Groups in File
    ApiResponse<Object> response = apiInstance.UsersDeleteGroupsWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupsApi.UsersDeleteGroupsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

**Object**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | OK |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

