# EssSharp.Api.SandboxDimensionApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**SandboxDimensionAddMembers**](SandboxDimensionApi.md#sandboxdimensionaddmembers) | **POST** /applications/{applicationName}/databases/{databaseName}/sandbox/members | Add Sandbox Members |
| [**SandboxDimensionCreate**](SandboxDimensionApi.md#sandboxdimensioncreate) | **POST** /applications/{applicationName}/databases/{databaseName}/sandbox | Create Sandbox |
| [**SandboxDimensionDelete**](SandboxDimensionApi.md#sandboxdimensiondelete) | **DELETE** /applications/{applicationName}/databases/{databaseName}/sandbox | Delete Sandbox |
| [**SandboxDimensionGet**](SandboxDimensionApi.md#sandboxdimensionget) | **GET** /applications/{applicationName}/databases/{databaseName}/sandbox | Get Sandbox Details |

<a name="sandboxdimensionaddmembers"></a>
# **SandboxDimensionAddMembers**
> SandboxDetail SandboxDimensionAddMembers (string applicationName, string databaseName, SandboxRequestPayload body = null)

Add Sandbox Members

<p>Adds members to an existing sandbox dimension.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class SandboxDimensionAddMembersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new SandboxDimensionApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new SandboxRequestPayload(); // SandboxRequestPayload | <p>Size of sandbox dimension members. Default is 100 if body is empty.</p> (optional) 

            try
            {
                // Add Sandbox Members
                SandboxDetail result = apiInstance.SandboxDimensionAddMembers(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SandboxDimensionApi.SandboxDimensionAddMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SandboxDimensionAddMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Sandbox Members
    ApiResponse<SandboxDetail> response = apiInstance.SandboxDimensionAddMembersWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SandboxDimensionApi.SandboxDimensionAddMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**SandboxRequestPayload**](SandboxRequestPayload.md) | &lt;p&gt;Size of sandbox dimension members. Default is 100 if body is empty.&lt;/p&gt; | [optional]  |

### Return type

[**SandboxDetail**](SandboxDetail.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Sandbox members added successfully; includes links to sandbox details.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to add sandbox members. The application or database name may be incorrect, or scenarios may not be enabled.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="sandboxdimensioncreate"></a>
# **SandboxDimensionCreate**
> SandboxDetail SandboxDimensionCreate (string applicationName, string databaseName, SandboxRequestPayload body = null)

Create Sandbox

<p>Creates Sandbox and CellProperties dimensions, while at the same time enabling scenario management for this cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class SandboxDimensionCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new SandboxDimensionApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new SandboxRequestPayload(); // SandboxRequestPayload | <p>Maximum count of sandbox dimension members. Default is 100 if body is empty.</p> (optional) 

            try
            {
                // Create Sandbox
                SandboxDetail result = apiInstance.SandboxDimensionCreate(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SandboxDimensionApi.SandboxDimensionCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SandboxDimensionCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Sandbox
    ApiResponse<SandboxDetail> response = apiInstance.SandboxDimensionCreateWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SandboxDimensionApi.SandboxDimensionCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**SandboxRequestPayload**](SandboxRequestPayload.md) | &lt;p&gt;Maximum count of sandbox dimension members. Default is 100 if body is empty.&lt;/p&gt; | [optional]  |

### Return type

[**SandboxDetail**](SandboxDetail.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Sandbox created successfully; includes links to sandbox details.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed create sandbox. The application or database name may be incorrect, or the logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="sandboxdimensiondelete"></a>
# **SandboxDimensionDelete**
> void SandboxDimensionDelete (string applicationName, string databaseName)

Delete Sandbox

<p>Deletes Sandbox and CellProperties dimensions from this cube. This action disables scenario management for the cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class SandboxDimensionDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new SandboxDimensionApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Delete Sandbox
                apiInstance.SandboxDimensionDelete(applicationName, databaseName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SandboxDimensionApi.SandboxDimensionDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SandboxDimensionDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Sandbox
    apiInstance.SandboxDimensionDeleteWithHttpInfo(applicationName, databaseName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SandboxDimensionApi.SandboxDimensionDeleteWithHttpInfo: " + e.Message);
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

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Sandbox deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete sandbox. The application or database name may be incorrect, or the logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="sandboxdimensionget"></a>
# **SandboxDimensionGet**
> SandboxDetail SandboxDimensionGet (string applicationName, string databaseName)

Get Sandbox Details

<p>Returns details about sandbox members.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class SandboxDimensionGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new SandboxDimensionApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Sandbox Details
                SandboxDetail result = apiInstance.SandboxDimensionGet(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SandboxDimensionApi.SandboxDimensionGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SandboxDimensionGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Sandbox Details
    ApiResponse<SandboxDetail> response = apiInstance.SandboxDimensionGetWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SandboxDimensionApi.SandboxDimensionGetWithHttpInfo: " + e.Message);
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

[**SandboxDetail**](SandboxDetail.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Sandbox details returned successfully; includes total sandbox members, available members, and assigned member.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get sandbox details. The application or database name may be incorrect, or the logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

