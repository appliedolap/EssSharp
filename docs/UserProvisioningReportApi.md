# EssSharp.Api.UserProvisioningReportApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**UserProvisioningReportGetFilters**](UserProvisioningReportApi.md#userprovisioningreportgetfilters) | **GET** /users/{userId}/provisionReport/applications/{application}/filters | Get Filters Provisioning Report |
| [**UserProvisioningReportGetFullReport**](UserProvisioningReportApi.md#userprovisioningreportgetfullreport) | **GET** /users/{userId}/provisionReport | Get Full Provisioning Report |
| [**UserProvisioningReportGetReportForAllApplications**](UserProvisioningReportApi.md#userprovisioningreportgetreportforallapplications) | **GET** /users/{userId}/provisionReport/applications | Get Applications Provisioning Reports |
| [**UserProvisioningReportGetReportForApplication**](UserProvisioningReportApi.md#userprovisioningreportgetreportforapplication) | **GET** /users/{userId}/provisionReport/applications/{application} | Get Application Provisioning Report |
| [**UserProvisioningReportGetRoles**](UserProvisioningReportApi.md#userprovisioningreportgetroles) | **GET** /users/{userId}/provisionReport/applications/{application}/roles | Get Application Roles Provisioning Report |
| [**UserProvisioningReportGetScripts**](UserProvisioningReportApi.md#userprovisioningreportgetscripts) | **GET** /users/{userId}/provisionReport/applications/{application}/scripts | Get Scripts Provisioning Report |

<a id="userprovisioningreportgetfilters"></a>
# **UserProvisioningReportGetFilters**
> DatabaseProvisionReportItemList UserProvisioningReportGetFilters (string application, string userId)

Get Filters Provisioning Report

<p>Gets a filters provisioning report for the specified application. The logged in user must have at least Database Manager role for the application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UserProvisioningReportGetFiltersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UserProvisioningReportApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var userId = "userId_example";  // string | <p>User ID.</p>

            try
            {
                // Get Filters Provisioning Report
                DatabaseProvisionReportItemList result = apiInstance.UserProvisioningReportGetFilters(application, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetFilters: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UserProvisioningReportGetFiltersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Filters Provisioning Report
    ApiResponse<DatabaseProvisionReportItemList> response = apiInstance.UserProvisioningReportGetFiltersWithHttpInfo(application, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetFiltersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **userId** | **string** | &lt;p&gt;User ID.&lt;/p&gt; |  |

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
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User not found.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="userprovisioningreportgetfullreport"></a>
# **UserProvisioningReportGetFullReport**
> MainProvisionReport UserProvisioningReportGetFullReport (string userId, string expand = null)

Get Full Provisioning Report

<p>Gets a full provisioning report. Service roles are included in the response only if the logged in user has Service Administrator role. The logged in user must have at least Database Manager role for an application to get its provisioning report. Application roles are included in the report only if the logged in user has at least Application Manager role for the application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UserProvisioningReportGetFullReportExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UserProvisioningReportApi(config);
            var userId = "userId_example";  // string | <p>User ID.</p>
            var expand = "expand_example";  // string | <p>Value can be <code>all</code> or <code>none</code>. Default value is <code>none</code>, meaning only links to applications are returned. If <code>all</code> is specified, provisioning information for all applications is returned.</p> (optional) 

            try
            {
                // Get Full Provisioning Report
                MainProvisionReport result = apiInstance.UserProvisioningReportGetFullReport(userId, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetFullReport: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UserProvisioningReportGetFullReportWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Full Provisioning Report
    ApiResponse<MainProvisionReport> response = apiInstance.UserProvisioningReportGetFullReportWithHttpInfo(userId, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetFullReportWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | &lt;p&gt;User ID.&lt;/p&gt; |  |
| **expand** | **string** | &lt;p&gt;Value can be &lt;code&gt;all&lt;/code&gt; or &lt;code&gt;none&lt;/code&gt;. Default value is &lt;code&gt;none&lt;/code&gt;, meaning only links to applications are returned. If &lt;code&gt;all&lt;/code&gt; is specified, provisioning information for all applications is returned.&lt;/p&gt; | [optional]  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Full provisioning report including service and all the applications.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User not found.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="userprovisioningreportgetreportforallapplications"></a>
# **UserProvisioningReportGetReportForAllApplications**
> ApplicationProvisionReportItemList UserProvisioningReportGetReportForAllApplications (string userId, string expand = null)

Get Applications Provisioning Reports

<p>Gets a provisioning report for all the applications. The logged in user must have at least Database Manager role for the application. Application roles are included in the report only if the logged in user has at least Application Manager role for the application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UserProvisioningReportGetReportForAllApplicationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UserProvisioningReportApi(config);
            var userId = "userId_example";  // string | <p>User ID.</p>
            var expand = "expand_example";  // string | <p>Value can be <code>all</code> or <code>none</code>. Default value is <code>none</code>, meaning only links to applications are returned. If <code>all</code> is specified, provisioning information for all applications is returned.</p> (optional) 

            try
            {
                // Get Applications Provisioning Reports
                ApplicationProvisionReportItemList result = apiInstance.UserProvisioningReportGetReportForAllApplications(userId, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetReportForAllApplications: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UserProvisioningReportGetReportForAllApplicationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Applications Provisioning Reports
    ApiResponse<ApplicationProvisionReportItemList> response = apiInstance.UserProvisioningReportGetReportForAllApplicationsWithHttpInfo(userId, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetReportForAllApplicationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | &lt;p&gt;User ID.&lt;/p&gt; |  |
| **expand** | **string** | &lt;p&gt;Value can be &lt;code&gt;all&lt;/code&gt; or &lt;code&gt;none&lt;/code&gt;. Default value is &lt;code&gt;none&lt;/code&gt;, meaning only links to applications are returned. If &lt;code&gt;all&lt;/code&gt; is specified, provisioning information for all applications is returned.&lt;/p&gt; | [optional]  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioning report returned successfully.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User not found.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="userprovisioningreportgetreportforapplication"></a>
# **UserProvisioningReportGetReportForApplication**
> ApplicationProvisionReportItem UserProvisioningReportGetReportForApplication (string application, string userId, string expand = null)

Get Application Provisioning Report

<p>Gets a provisioning report for the specified application. The logged in user must have at least Database Manager role for the application. Application roles are included in the report only if the logged in user has at least Application Manager role for the application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UserProvisioningReportGetReportForApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UserProvisioningReportApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var userId = "userId_example";  // string | <p>User ID.</p>
            var expand = "expand_example";  // string | <p>Value can be <code>all</code> or <code>none</code>. Default value is <code>none</code>, meaning only links to roles, filters, and scripts are returned. If <code>all</code> is specified, provisioning information for all roles, filters, and scripts in the application is returned.</p> (optional) 

            try
            {
                // Get Application Provisioning Report
                ApplicationProvisionReportItem result = apiInstance.UserProvisioningReportGetReportForApplication(application, userId, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetReportForApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UserProvisioningReportGetReportForApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Provisioning Report
    ApiResponse<ApplicationProvisionReportItem> response = apiInstance.UserProvisioningReportGetReportForApplicationWithHttpInfo(application, userId, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetReportForApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **userId** | **string** | &lt;p&gt;User ID.&lt;/p&gt; |  |
| **expand** | **string** | &lt;p&gt;Value can be &lt;code&gt;all&lt;/code&gt; or &lt;code&gt;none&lt;/code&gt;. Default value is &lt;code&gt;none&lt;/code&gt;, meaning only links to roles, filters, and scripts are returned. If &lt;code&gt;all&lt;/code&gt; is specified, provisioning information for all roles, filters, and scripts in the application is returned.&lt;/p&gt; | [optional]  |

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
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User not found.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="userprovisioningreportgetroles"></a>
# **UserProvisioningReportGetRoles**
> ProvisionReportItemList UserProvisioningReportGetRoles (string application, string userId)

Get Application Roles Provisioning Report

<p>Get a roles provisioning report for the specified application. The logged in user must have at least Application Manager role for the application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UserProvisioningReportGetRolesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UserProvisioningReportApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var userId = "userId_example";  // string | <p>User ID.</p>

            try
            {
                // Get Application Roles Provisioning Report
                ProvisionReportItemList result = apiInstance.UserProvisioningReportGetRoles(application, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetRoles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UserProvisioningReportGetRolesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Roles Provisioning Report
    ApiResponse<ProvisionReportItemList> response = apiInstance.UserProvisioningReportGetRolesWithHttpInfo(application, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetRolesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **userId** | **string** | &lt;p&gt;User ID.&lt;/p&gt; |  |

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
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the appropriate application role.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User not found.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="userprovisioningreportgetscripts"></a>
# **UserProvisioningReportGetScripts**
> DatabaseProvisionReportItemList UserProvisioningReportGetScripts (string application, string userId)

Get Scripts Provisioning Report

<p>Get a scripts provisioning report for the specified application. The logged in user must have at least Database Manager role for the application to get a provisioning report.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class UserProvisioningReportGetScriptsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new UserProvisioningReportApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var userId = "userId_example";  // string | <p>User ID.</p>

            try
            {
                // Get Scripts Provisioning Report
                DatabaseProvisionReportItemList result = apiInstance.UserProvisioningReportGetScripts(application, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetScripts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the UserProvisioningReportGetScriptsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Scripts Provisioning Report
    ApiResponse<DatabaseProvisionReportItemList> response = apiInstance.UserProvisioningReportGetScriptsWithHttpInfo(application, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling UserProvisioningReportApi.UserProvisioningReportGetScriptsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **userId** | **string** | &lt;p&gt;User ID.&lt;/p&gt; |  |

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
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The logged in user may not have the application service role.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User not found.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

