# EssSharp.Api.GroupProvisioningReportApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GroupProvisioningReportGetFilters**](GroupProvisioningReportApi.md#groupprovisioningreportgetfilters) | **GET** /groups/{groupId}/provisionReport/applications/{application}/filters | Get Filters Provisioning Report |
| [**GroupProvisioningReportGetFullReport**](GroupProvisioningReportApi.md#groupprovisioningreportgetfullreport) | **GET** /groups/{groupId}/provisionReport | Get Full Provisioning Report |
| [**GroupProvisioningReportGetReportForAllApplications**](GroupProvisioningReportApi.md#groupprovisioningreportgetreportforallapplications) | **GET** /groups/{groupId}/provisionReport/applications | Get All Applications Provisioning Report |
| [**GroupProvisioningReportGetReportForApplication**](GroupProvisioningReportApi.md#groupprovisioningreportgetreportforapplication) | **GET** /groups/{groupId}/provisionReport/applications/{application} | Get Application Provisioning Report |
| [**GroupProvisioningReportGetRoles**](GroupProvisioningReportApi.md#groupprovisioningreportgetroles) | **GET** /groups/{groupId}/provisionReport/applications/{application}/roles | Get Application Roles Provisioning Report |
| [**GroupProvisioningReportGetScripts**](GroupProvisioningReportApi.md#groupprovisioningreportgetscripts) | **GET** /groups/{groupId}/provisionReport/applications/{application}/scripts | Get Scripts Provisioning Report |

<a id="groupprovisioningreportgetfilters"></a>
# **GroupProvisioningReportGetFilters**
> DatabaseProvisionReportItemList GroupProvisioningReportGetFilters (string application, string groupId)

Get Filters Provisioning Report

<p>Gets a provisioning report about filters for the specified application. The logged in user must have at least Database Manager role for the application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupProvisioningReportGetFiltersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupProvisioningReportApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var groupId = "groupId_example";  // string | <p>Group ID.</p>

            try
            {
                // Get Filters Provisioning Report
                DatabaseProvisionReportItemList result = apiInstance.GroupProvisioningReportGetFilters(application, groupId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetFilters: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupProvisioningReportGetFiltersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Filters Provisioning Report
    ApiResponse<DatabaseProvisionReportItemList> response = apiInstance.GroupProvisioningReportGetFiltersWithHttpInfo(application, groupId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetFiltersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **groupId** | **string** | &lt;p&gt;Group ID.&lt;/p&gt; |  |

### Return type

[**DatabaseProvisionReportItemList**](DatabaseProvisionReportItemList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioning report returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get provisioning report. The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The group with that ID does not exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupprovisioningreportgetfullreport"></a>
# **GroupProvisioningReportGetFullReport**
> MainProvisionReport GroupProvisioningReportGetFullReport (string groupId, string expand = null)

Get Full Provisioning Report

<p>Gets a full provisioning report for the service and all applications. Service roles are included in the response only if the logged in user has Service Administrator role. The logged in user must have at least Database Manager role for applications to get application provisioning reports. Application roles are included in the report only if the logged in user has at least Application Manager role for the application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupProvisioningReportGetFullReportExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupProvisioningReportApi(config);
            var groupId = "groupId_example";  // string | <p>Group ID.</p>
            var expand = "expand_example";  // string | <p>Use <code>all</code> to get provisioning information for all applications.</p> (optional) 

            try
            {
                // Get Full Provisioning Report
                MainProvisionReport result = apiInstance.GroupProvisioningReportGetFullReport(groupId, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetFullReport: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupProvisioningReportGetFullReportWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Full Provisioning Report
    ApiResponse<MainProvisionReport> response = apiInstance.GroupProvisioningReportGetFullReportWithHttpInfo(groupId, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetFullReportWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **groupId** | **string** | &lt;p&gt;Group ID.&lt;/p&gt; |  |
| **expand** | **string** | &lt;p&gt;Use &lt;code&gt;all&lt;/code&gt; to get provisioning information for all applications.&lt;/p&gt; | [optional]  |

### Return type

[**MainProvisionReport**](MainProvisionReport.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioning report returned successfully.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The group with that ID does not exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupprovisioningreportgetreportforallapplications"></a>
# **GroupProvisioningReportGetReportForAllApplications**
> ApplicationProvisionReportItemList GroupProvisioningReportGetReportForAllApplications (string groupId, string expand = null)

Get All Applications Provisioning Report

<p>Gets a provisioning report for all applications. The logged in user must have at least Database Manager role for the application. Application roles are included in the report only if the logged in user has at least Application Manager role for the application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupProvisioningReportGetReportForAllApplicationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupProvisioningReportApi(config);
            var groupId = "groupId_example";  // string | <p>Group ID.</p>
            var expand = "expand_example";  // string | <p>Use <code>all</code> to get provisioning information for all applications.</p> (optional) 

            try
            {
                // Get All Applications Provisioning Report
                ApplicationProvisionReportItemList result = apiInstance.GroupProvisioningReportGetReportForAllApplications(groupId, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetReportForAllApplications: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupProvisioningReportGetReportForAllApplicationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get All Applications Provisioning Report
    ApiResponse<ApplicationProvisionReportItemList> response = apiInstance.GroupProvisioningReportGetReportForAllApplicationsWithHttpInfo(groupId, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetReportForAllApplicationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **groupId** | **string** | &lt;p&gt;Group ID.&lt;/p&gt; |  |
| **expand** | **string** | &lt;p&gt;Use &lt;code&gt;all&lt;/code&gt; to get provisioning information for all applications.&lt;/p&gt; | [optional]  |

### Return type

[**ApplicationProvisionReportItemList**](ApplicationProvisionReportItemList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioning report for applications returned successfully.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The group with that ID does not exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupprovisioningreportgetreportforapplication"></a>
# **GroupProvisioningReportGetReportForApplication**
> ApplicationProvisionReportItem GroupProvisioningReportGetReportForApplication (string application, string groupId, string expand = null)

Get Application Provisioning Report

<p>Gets provisioning report for the specified application. The logged in user must have at least Database Manager role for the application. Application roles are included in the report only if the logged in user has at least Application Manager role for the application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupProvisioningReportGetReportForApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupProvisioningReportApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var groupId = "groupId_example";  // string | <p>Group ID.</p>
            var expand = "expand_example";  // string | <p>Value can be <code>all</code> or <code>none</code>. Default value is <code>none</code>. When value is <code>none</code>, only links to roles, filters and scripts will be returned. When value is <code>all</code>, provisioning information for roles, filters, and scripts are returned.</p> (optional) 

            try
            {
                // Get Application Provisioning Report
                ApplicationProvisionReportItem result = apiInstance.GroupProvisioningReportGetReportForApplication(application, groupId, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetReportForApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupProvisioningReportGetReportForApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Provisioning Report
    ApiResponse<ApplicationProvisionReportItem> response = apiInstance.GroupProvisioningReportGetReportForApplicationWithHttpInfo(application, groupId, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetReportForApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **groupId** | **string** | &lt;p&gt;Group ID.&lt;/p&gt; |  |
| **expand** | **string** | &lt;p&gt;Value can be &lt;code&gt;all&lt;/code&gt; or &lt;code&gt;none&lt;/code&gt;. Default value is &lt;code&gt;none&lt;/code&gt;. When value is &lt;code&gt;none&lt;/code&gt;, only links to roles, filters and scripts will be returned. When value is &lt;code&gt;all&lt;/code&gt;, provisioning information for roles, filters, and scripts are returned.&lt;/p&gt; | [optional]  |

### Return type

[**ApplicationProvisionReportItem**](ApplicationProvisionReportItem.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioning report returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The group with that ID does not exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupprovisioningreportgetroles"></a>
# **GroupProvisioningReportGetRoles**
> ProvisionReportItemList GroupProvisioningReportGetRoles (string application, string groupId)

Get Application Roles Provisioning Report

<p>Gets a roles provisioning report for the specified application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupProvisioningReportGetRolesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupProvisioningReportApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var groupId = "groupId_example";  // string | <p>Group ID.</p>

            try
            {
                // Get Application Roles Provisioning Report
                ProvisionReportItemList result = apiInstance.GroupProvisioningReportGetRoles(application, groupId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetRoles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupProvisioningReportGetRolesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Roles Provisioning Report
    ApiResponse<ProvisionReportItemList> response = apiInstance.GroupProvisioningReportGetRolesWithHttpInfo(application, groupId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetRolesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **groupId** | **string** | &lt;p&gt;Group ID.&lt;/p&gt; |  |

### Return type

[**ProvisionReportItemList**](ProvisionReportItemList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioning report returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get provisioning report. The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The group with that ID does not exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="groupprovisioningreportgetscripts"></a>
# **GroupProvisioningReportGetScripts**
> DatabaseProvisionReportItemList GroupProvisioningReportGetScripts (string application, string groupId)

Get Scripts Provisioning Report

<p>Gets a provisioning report about scripts for the specified application. The logged in user must have at least Database Manager role for the application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GroupProvisioningReportGetScriptsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GroupProvisioningReportApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var groupId = "groupId_example";  // string | <p>Group ID.</p>

            try
            {
                // Get Scripts Provisioning Report
                DatabaseProvisionReportItemList result = apiInstance.GroupProvisioningReportGetScripts(application, groupId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetScripts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GroupProvisioningReportGetScriptsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Scripts Provisioning Report
    ApiResponse<DatabaseProvisionReportItemList> response = apiInstance.GroupProvisioningReportGetScriptsWithHttpInfo(application, groupId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GroupProvisioningReportApi.GroupProvisioningReportGetScriptsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **groupId** | **string** | &lt;p&gt;Group ID.&lt;/p&gt; |  |

### Return type

[**DatabaseProvisionReportItemList**](DatabaseProvisionReportItemList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioning report returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get provisioning report. The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The group with that ID does not exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

