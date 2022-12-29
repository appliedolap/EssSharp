# EssSharp.Api.UsersApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**UsersAdd**](UsersApi.md#usersadd) | **POST** /users | Add or Import User(s) |
| [**UsersDelete**](UsersApi.md#usersdelete) | **DELETE** /users/{id} | Delete User |
| [**UsersDeleteUsers**](UsersApi.md#usersdeleteusers) | **POST** /users/actions/delete | Delete Users |
| [**UsersGet**](UsersApi.md#usersget) | **GET** /users/{id} | Get User |
| [**UsersPUT**](UsersApi.md#usersput) | **PUT** /users/{id} | Update User |
| [**UsersSearch**](UsersApi.md#userssearch) | **GET** /users | Search or Export Users |

<a name="usersadd"></a>
# **UsersAdd**
> UserBean UsersAdd (UserBean body)

Add or Import User(s)

<p>Add or import user(s).</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UsersAddExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UsersApi(config);
            var body = new UserBean(); // UserBean | <p>Users details to create, if <code>Accept='application/json'</code> or <code>Accept='application/xml'</code>. Or, CSV file from which to import users, if <code>Accept='application/octet-stream'</code>.</p>

            try
            {
                // Add or Import User(s)
                UserBean result = apiInstance.UsersAdd(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.UsersAdd: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UsersAddWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add or Import User(s)
    ApiResponse<UserBean> response = apiInstance.UsersAddWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.UsersAddWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**UserBean**](UserBean.md) | &lt;p&gt;Users details to create, if &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;. Or, CSV file from which to import users, if &lt;code&gt;Accept&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt;.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User added successfully. If &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;, the users are returned in the response body. If &lt;code&gt;Accept&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt;, the added users are imported but not included in the response body.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate service role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="usersdelete"></a>
# **UsersDelete**
> void UsersDelete (string id)

Delete User

<p>Deletes a user.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UsersDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UsersApi(config);
            var id = "id_example";  // string | <p>ID of user.</p>

            try
            {
                // Delete User
                apiInstance.UsersDelete(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.UsersDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UsersDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete User
    apiInstance.UsersDeleteWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.UsersDeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of user.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate service role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="usersdeleteusers"></a>
# **UsersDeleteUsers**
> Object UsersDeleteUsers ()

Delete Users

<p>Deletes the users specified in the text file. Requires service administrator role.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UsersDeleteUsersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UsersApi(config);

            try
            {
                // Delete Users
                Object result = apiInstance.UsersDeleteUsers();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.UsersDeleteUsers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UsersDeleteUsersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Users
    ApiResponse<Object> response = apiInstance.UsersDeleteUsersWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.UsersDeleteUsersWithHttpInfo: " + e.Message);
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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Users deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Validation failed. The logged in user may not have the appropriate service role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="usersget"></a>
# **UsersGet**
> UserBean UsersGet (string id)

Get User

<p>Gets details for a specified user ID.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UsersGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UsersApi(config);
            var id = "id_example";  // string | <p>ID of user.</p>

            try
            {
                // Get User
                UserBean result = apiInstance.UsersGet(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.UsersGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UsersGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get User
    ApiResponse<UserBean> response = apiInstance.UsersGetWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.UsersGetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of user.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User details returned successfully.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User not found.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate service role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="usersput"></a>
# **UsersPUT**
> UserBean UsersPUT (string id, UserBean body)

Update User

<p>Updates a user.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UsersPUTExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UsersApi(config);
            var id = "id_example";  // string | <p>ID of user.</p>
            var body = new UserBean(); // UserBean | Updated user

            try
            {
                // Update User
                UserBean result = apiInstance.UsersPUT(id, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.UsersPUT: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UsersPUTWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update User
    ApiResponse<UserBean> response = apiInstance.UsersPUTWithHttpInfo(id, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.UsersPUTWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | &lt;p&gt;ID of user.&lt;/p&gt; |  |
| **body** | [**UserBean**](UserBean.md) | Updated user |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate service role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="userssearch"></a>
# **UsersSearch**
> Users UsersSearch (string filter = null, int? limit = null, string expand = null)

Search or Export Users

<p>Get a list of users based on search results, or export users as CSV file.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UsersSearchExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UsersApi(config);
            var filter = "filter_example";  // string | <p>User ID wildcard pattern. Filter by name of user if header <code>Accept='application/json'</code> or <code>Accept='application/xml'</code>.</p> (optional) 
            var limit = -1;  // int? | <p>Maximum number of users to return, if header <code>Accept='application/json'</code> or <code>Accept='application/xml'</code>. Default of <code>-1</code> = no maximum.</p> (optional)  (default to -1)
            var expand = "\"all\"";  // string | <p>Value can be <code>all</code> or <code>none</code>. Default value is <code>all</code>, meaning service role and groups are returned for each user. If <code>none</code> is specified, service role and groups are not returned.</p> (optional)  (default to "all")

            try
            {
                // Search or Export Users
                Users result = apiInstance.UsersSearch(filter, limit, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UsersApi.UsersSearch: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UsersSearchWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Search or Export Users
    ApiResponse<Users> response = apiInstance.UsersSearchWithHttpInfo(filter, limit, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UsersApi.UsersSearchWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **filter** | **string** | &lt;p&gt;User ID wildcard pattern. Filter by name of user if header &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; | [optional]  |
| **limit** | **int?** | &lt;p&gt;Maximum number of users to return, if header &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;. Default of &lt;code&gt;-1&lt;/code&gt; &#x3D; no maximum.&lt;/p&gt; | [optional] [default to -1] |
| **expand** | **string** | &lt;p&gt;Value can be &lt;code&gt;all&lt;/code&gt; or &lt;code&gt;none&lt;/code&gt;. Default value is &lt;code&gt;all&lt;/code&gt;, meaning service role and groups are returned for each user. If &lt;code&gt;none&lt;/code&gt; is specified, service role and groups are not returned.&lt;/p&gt; | [optional] [default to &quot;all&quot;] |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User results returned successfully. Response type can be either JSON, XML, or CSV stream, depending on the Accept header. If &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;, the users are returned in the response body. If &lt;code&gt;Accept&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt;, the users are returned as a JSON stream.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate service role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

