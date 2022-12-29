# EssSharp.Api.RolesApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**RolesGetRoles**](RolesApi.md#rolesgetroles) | **GET** /roles | Get Essbase Roles |

<a name="rolesgetroles"></a>
# **RolesGetRoles**
> Roles RolesGetRoles (string type = null)

Get Essbase Roles

<p>Returns the roles available in Essbase. Valid type values are <code>application</code> and <code>server</code>. If type is empty, then both <code>application</code> and <code>server</code> roles are returned.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class RolesGetRolesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new RolesApi(config);
            var type = "type_example";  // string | <p>Valid type values are <code>application</code> and <code>server</code>.</p> (optional) 

            try
            {
                // Get Essbase Roles
                Roles result = apiInstance.RolesGetRoles(type);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RolesApi.RolesGetRoles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RolesGetRolesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Essbase Roles
    ApiResponse<Roles> response = apiInstance.RolesGetRolesWithHttpInfo(type);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling RolesApi.RolesGetRolesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **type** | **string** | &lt;p&gt;Valid type values are &lt;code&gt;application&lt;/code&gt; and &lt;code&gt;server&lt;/code&gt;.&lt;/p&gt; | [optional]  |

### Return type

[**Roles**](Roles.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;Strong&gt;OK&lt;/strong&gt;&lt;p&gt;Returns roles for the specified type.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

