# EssSharp.Api.ApplicationsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ApplicationsCopyApplication**](ApplicationsApi.md#applicationscopyapplication) | **POST** /applications/actions/copy | Copy Application |
| [**ApplicationsCopyDatabase**](ApplicationsApi.md#applicationscopydatabase) | **POST** /applications/{applicationName}/databases/actions/copy | Copy Database |
| [**ApplicationsCreateApplications**](ApplicationsApi.md#applicationscreateapplications) | **POST** /applications | Create Application |
| [**ApplicationsCreateShadowApplication**](ApplicationsApi.md#applicationscreateshadowapplication) | **POST** /applications/actions/shadowCopy | Create Shadow Application |
| [**ApplicationsDeleteApplication**](ApplicationsApi.md#applicationsdeleteapplication) | **DELETE** /applications/{applicationName} | Delete Application |
| [**ApplicationsDeleteDatabase**](ApplicationsApi.md#applicationsdeletedatabase) | **DELETE** /applications/{applicationName}/databases/{databaseName} | Delete Database |
| [**ApplicationsDropShadowApplication**](ApplicationsApi.md#applicationsdropshadowapplication) | **DELETE** /applications/actions/shadowDelete/{shadowAppName} | Delete Shadow Application |
| [**ApplicationsExecuteReportScript**](ApplicationsApi.md#applicationsexecutereportscript) | **GET** /applications/{applicationName}/databases/{databaseName}/executeReport | Execute Report Script |
| [**ApplicationsGetActiveAlias**](ApplicationsApi.md#applicationsgetactivealias) | **GET** /applications/{applicationName}/databases/{databaseName}/aliases/getActiveAlias | List Active Aliases |
| [**ApplicationsGetAliases**](ApplicationsApi.md#applicationsgetaliases) | **GET** /applications/{applicationName}/databases/{databaseName}/aliases | List Aliases |
| [**ApplicationsGetApplication**](ApplicationsApi.md#applicationsgetapplication) | **GET** /applications/{applicationName} | Get Application |
| [**ApplicationsGetApplicationNames**](ApplicationsApi.md#applicationsgetapplicationnames) | **GET** /applications/actions/name/{appVisiblity} | List Application Names |
| [**ApplicationsGetApplicationProvisionReport**](ApplicationsApi.md#applicationsgetapplicationprovisionreport) | **GET** /applications/{applicationName}/provisionReport | Get Application Provisioning Report |
| [**ApplicationsGetApplications**](ApplicationsApi.md#applicationsgetapplications) | **GET** /applications | List Applications |
| [**ApplicationsGetApplicationsTree**](ApplicationsApi.md#applicationsgetapplicationstree) | **GET** /applications/actions/tree | Get Application Tree View |
| [**ApplicationsGetCube**](ApplicationsApi.md#applicationsgetcube) | **GET** /applications/{applicationName}/databases/{databaseName} | Get Database |
| [**ApplicationsGetCubes**](ApplicationsApi.md#applicationsgetcubes) | **GET** /applications/{applicationName}/databases | List Databases |
| [**ApplicationsPerformDbOperation**](ApplicationsApi.md#applicationsperformdboperation) | **PUT** /applications/{applicationName}/databases/{databaseName} | Start or Stop Database |
| [**ApplicationsPerformOperation**](ApplicationsApi.md#applicationsperformoperation) | **PUT** /applications/{applicationName} | Start or Stop Application |
| [**ApplicationsPromoteShadowApplication**](ApplicationsApi.md#applicationspromoteshadowapplication) | **POST** /applications/actions/shadowPromote | Promote Shadow Application |
| [**ApplicationsRenameApplication**](ApplicationsApi.md#applicationsrenameapplication) | **POST** /applications/actions/rename | Rename Application |
| [**ApplicationsRenameDatabase**](ApplicationsApi.md#applicationsrenamedatabase) | **POST** /applications/{applicationName}/databases/actions/rename | Rename Database |
| [**DatabasesFormulaFunctions**](ApplicationsApi.md#databasesformulafunctions) | **GET** /applications/{applicationName}/databases/{databaseName}/formulaFunctions | Get Formula Functions |
| [**DatabasesGetCalculationFunctions**](ApplicationsApi.md#databasesgetcalculationfunctions) | **GET** /applications/{applicationName}/databases/{databaseName}/calculationFunctions | Get Calculation Functions |
| [**DatabasesGetCurrencySettings**](ApplicationsApi.md#databasesgetcurrencysettings) | **GET** /applications/{applicationName}/databases/{databaseName}/currencySettings | Get Currency Settings |
| [**DatabasesGetMdxFunctions**](ApplicationsApi.md#databasesgetmdxfunctions) | **GET** /applications/{applicationName}/databases/{databaseName}/mdxFunctions | Get MDX Functions |
| [**DatabasesSetCurrencySettings**](ApplicationsApi.md#databasessetcurrencysettings) | **POST** /applications/{applicationName}/databases/{databaseName}/currencySettings | Set Currency Settings |
| [**SetActiveAlias**](ApplicationsApi.md#setactivealias) | **PUT** /applications/{applicationName}/databases/{databaseName}/aliases/setActiveAlias | Set Active Alias |

<a name="applicationscopyapplication"></a>
# **ApplicationsCopyApplication**
> void ApplicationsCopyApplication (CopyRenameBean body)

Copy Application

<p>Copies an application. You must provide the source and destination application names.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsCopyApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var body = new CopyRenameBean(); // CopyRenameBean | <p>Source and destination application information.</p>

            try
            {
                // Copy Application
                apiInstance.ApplicationsCopyApplication(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsCopyApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsCopyApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Copy Application
    apiInstance.ApplicationsCopyApplicationWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsCopyApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**CopyRenameBean**](CopyRenameBean.md) | &lt;p&gt;Source and destination application information.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Application copied successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to copy application.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationscopydatabase"></a>
# **ApplicationsCopyDatabase**
> void ApplicationsCopyDatabase (string applicationName, CubeCopy body)

Copy Database

<p>Copies a database. You must provide the source and destination application and database names.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsCopyDatabaseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Source application name.</p>
            var body = new CubeCopy(); // CubeCopy | <p>Source and destination database information.</p>

            try
            {
                // Copy Database
                apiInstance.ApplicationsCopyDatabase(applicationName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsCopyDatabase: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsCopyDatabaseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Copy Database
    apiInstance.ApplicationsCopyDatabaseWithHttpInfo(applicationName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsCopyDatabaseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Source application name.&lt;/p&gt; |  |
| **body** | [**CubeCopy**](CubeCopy.md) | &lt;p&gt;Source and destination database information.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Database copied successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to copy database.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationscreateapplications"></a>
# **ApplicationsCreateApplications**
> void ApplicationsCreateApplications (CreateApplication body)

Create Application

<p>Creates an application with the specified details.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsCreateApplicationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var body = new CreateApplication(); // CreateApplication | <p>Application details.</p>

            try
            {
                // Create Application
                apiInstance.ApplicationsCreateApplications(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsCreateApplications: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsCreateApplicationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Application
    apiInstance.ApplicationsCreateApplicationsWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsCreateApplicationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**CreateApplication**](CreateApplication.md) | &lt;p&gt;Application details.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Application created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create application.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationscreateshadowapplication"></a>
# **ApplicationsCreateShadowApplication**
> void ApplicationsCreateShadowApplication (ShadowCopyBean body)

Create Shadow Application

<p>Puts the specified primary application in read-only mode and creates a shadow application (a complete copy) out of the primary application.</p> <p>A shadow application enables you to perform cube modifications and outline restructure on the shadow application, while the primary application serves read-only operations such as queries.</p> <p>Permission required: power user.</p> <p>Shadow applications are useful because an outline restructure can take a very long time, depending on the size of the application.</p> <p>Instead of reporting users being blocked by the downtime due to restructure, a shadow solution helps them continue their queries against the primary application, while the restructure is occurring on the shadow application.</p> <p>Note that a shadow application can be made as hidden copy of the primary application.</p> <p>This means if you invoke <a href='./op-applications-get.html'>List Applications</a>, you will not see the shadow application in that list.</p> <p>The parameter <i>waitForOngoingUpdatesInSecs</i> allows you to control how long the copying process can wait, if there are any ongoing write-operations on the cubes(s) of this application at the time you are attempting to make a shadow copy.</p> <p>For example, if there is a data load in progress, the cloning process fails.</p> <p>If you specified waitForOngoingUpdatesInSecs as 60, Essbase waits up to one minute for the data load to complete before initiating a cloning process.</p> <p>If the data load doesn't complete within this specified wait-interval, Essbase does not create the copy, the cloning process fails with an error, and the data load continues.</p><p><b>See Also</b></p><ul><li><a href='./op-applications-actions-name-appvisiblity-get.html'>List Application Names</a></li><li><a href='./op-applications-actions-shadowpromote-post.html'>Promote Shadow Application</a></li><li><a href='./op-applications-actions-shadowdelete-shadowappname-delete.html'>Delete Shadow Application</a></li></ul>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsCreateShadowApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var body = new ShadowCopyBean(); // ShadowCopyBean | <p>primaryAppName: Source application name which you intend to clone.</p> <p>shadowAppName : Unique Destination or secondary application name which will be a copy of the source.</p> <p>hideShadow: Specify <b>true</b> to hide the application; otherwise, specify <b>false</b>.</p> <p>waitForOngoingUpdatesInSecs: Waiting period (in seconds) for any active write-operations to complete.</p> <p>runInBackground: Specify <b>true</b> to schedule 'Shadow Copy' as a Job; otherwise, specify <b>false</b>.</p> 

            try
            {
                // Create Shadow Application
                apiInstance.ApplicationsCreateShadowApplication(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsCreateShadowApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsCreateShadowApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Shadow Application
    apiInstance.ApplicationsCreateShadowApplicationWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsCreateShadowApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**ShadowCopyBean**](ShadowCopyBean.md) | &lt;p&gt;primaryAppName: Source application name which you intend to clone.&lt;/p&gt; &lt;p&gt;shadowAppName : Unique Destination or secondary application name which will be a copy of the source.&lt;/p&gt; &lt;p&gt;hideShadow: Specify &lt;b&gt;true&lt;/b&gt; to hide the application; otherwise, specify &lt;b&gt;false&lt;/b&gt;.&lt;/p&gt; &lt;p&gt;waitForOngoingUpdatesInSecs: Waiting period (in seconds) for any active write-operations to complete.&lt;/p&gt; &lt;p&gt;runInBackground: Specify &lt;b&gt;true&lt;/b&gt; to schedule &#39;Shadow Copy&#39; as a Job; otherwise, specify &lt;b&gt;false&lt;/b&gt;.&lt;/p&gt;  |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Shadow application created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create shadow application.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |
| **503** | &lt;p&gt;&lt;strong&gt;Service Unavailable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Naming exception or server exception.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsdeleteapplication"></a>
# **ApplicationsDeleteApplication**
> void ApplicationsDeleteApplication (string applicationName)

Delete Application

<p>Deletes specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsDeleteApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>

            try
            {
                // Delete Application
                apiInstance.ApplicationsDeleteApplication(applicationName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsDeleteApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsDeleteApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Application
    apiInstance.ApplicationsDeleteApplicationWithHttpInfo(applicationName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsDeleteApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Application deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete application.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsdeletedatabase"></a>
# **ApplicationsDeleteDatabase**
> void ApplicationsDeleteDatabase (string applicationName, string databaseName)

Delete Database

<p>Deletes specified database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsDeleteDatabaseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Delete Database
                apiInstance.ApplicationsDeleteDatabase(applicationName, databaseName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsDeleteDatabase: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsDeleteDatabaseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Database
    apiInstance.ApplicationsDeleteDatabaseWithHttpInfo(applicationName, databaseName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsDeleteDatabaseWithHttpInfo: " + e.Message);
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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Database deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete database.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsdropshadowapplication"></a>
# **ApplicationsDropShadowApplication**
> void ApplicationsDropShadowApplication (string shadowAppName)

Delete Shadow Application

<p>Forcefully deletes the specified shadow application.</p> <p>Although shadow applications can also be deleted using the regular <a href='./op-applications-applicationname-delete.html'>Delete Application</a>, if the shadow application is corrupted or has any locks, then it the regular delete application fails.</p> <p>Therefore, this API guarantees a forceful deletion of the shadow application.</p> <p>This API is similar to the MaxL statement <b>drop application <i>APP-NAME</i> cascade force</b></p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsDropShadowApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var shadowAppName = "shadowAppName_example";  // string | <p>shadowAppName: Name of the shadow application to remove.</p>

            try
            {
                // Delete Shadow Application
                apiInstance.ApplicationsDropShadowApplication(shadowAppName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsDropShadowApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsDropShadowApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Shadow Application
    apiInstance.ApplicationsDropShadowApplicationWithHttpInfo(shadowAppName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsDropShadowApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **shadowAppName** | **string** | &lt;p&gt;shadowAppName: Name of the shadow application to remove.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Shadow application deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete shadow application.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |
| **503** | &lt;p&gt;&lt;strong&gt;Service Unavailable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Naming exception or server exception.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsexecutereportscript"></a>
# **ApplicationsExecuteReportScript**
> void ApplicationsExecuteReportScript (string applicationName, string databaseName, string filename, string lockForUpdate = null)

Execute Report Script

<p>Returns output generated by  executing a report script specification file.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsExecuteReportScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var filename = "filename_example";  // string | <p>Report script file name.</p>
            var lockForUpdate = "\"false\"";  // string | <p>All blocks which are accessed by the report specification are locked.</p> (optional)  (default to "false")

            try
            {
                // Execute Report Script
                apiInstance.ApplicationsExecuteReportScript(applicationName, databaseName, filename, lockForUpdate);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsExecuteReportScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsExecuteReportScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Execute Report Script
    apiInstance.ApplicationsExecuteReportScriptWithHttpInfo(applicationName, databaseName, filename, lockForUpdate);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsExecuteReportScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **filename** | **string** | &lt;p&gt;Report script file name.&lt;/p&gt; |  |
| **lockForUpdate** | **string** | &lt;p&gt;All blocks which are accessed by the report specification are locked.&lt;/p&gt; | [optional] [default to &quot;false&quot;] |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Report generated successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsgetactivealias"></a>
# **ApplicationsGetActiveAlias**
> string ApplicationsGetActiveAlias (string applicationName, string databaseName)

List Active Aliases

<p>Returns alias tables associated with the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsGetActiveAliasExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // List Active Aliases
                string result = apiInstance.ApplicationsGetActiveAlias(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetActiveAlias: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsGetActiveAliasWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Active Aliases
    ApiResponse<string> response = apiInstance.ApplicationsGetActiveAliasWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetActiveAliasWithHttpInfo: " + e.Message);
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

**string**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Alias tables returned successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsgetaliases"></a>
# **ApplicationsGetAliases**
> StringCollectionResponse ApplicationsGetAliases (string applicationName, string databaseName)

List Aliases

<p>Returns alias tables associated with the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsGetAliasesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // List Aliases
                StringCollectionResponse result = apiInstance.ApplicationsGetAliases(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetAliases: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsGetAliasesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Aliases
    ApiResponse<StringCollectionResponse> response = apiInstance.ApplicationsGetAliasesWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetAliasesWithHttpInfo: " + e.Message);
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

[**StringCollectionResponse**](StringCollectionResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Alias tables returned successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsgetapplication"></a>
# **ApplicationsGetApplication**
> Application ApplicationsGetApplication (string applicationName, bool? role = null)

Get Application

<p>Returns details of application with specified name.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsGetApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var role = false;  // bool? | <p>Role.</p> (optional)  (default to false)

            try
            {
                // Get Application
                Application result = apiInstance.ApplicationsGetApplication(applicationName, role);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsGetApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application
    ApiResponse<Application> response = apiInstance.ApplicationsGetApplicationWithHttpInfo(applicationName, role);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **role** | **bool?** | &lt;p&gt;Role.&lt;/p&gt; | [optional] [default to false] |

### Return type

[**Application**](Application.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Application details returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get application.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsgetapplicationnames"></a>
# **ApplicationsGetApplicationNames**
> Object ApplicationsGetApplicationNames (string appVisiblity)

List Application Names

<p>Returns the list of application names, based on the type of visibility requested. For example, you can fetch the list of hidden (shadow) applications only, or you can fetch the visible applications list only, or both sets of applications.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsGetApplicationNamesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var appVisiblity = "ALL";  // string | <p>appVisiblity : Visibility level of application names. Valid options: ALL, HIDDEN, REGULAR</p>

            try
            {
                // List Application Names
                Object result = apiInstance.ApplicationsGetApplicationNames(appVisiblity);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetApplicationNames: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsGetApplicationNamesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Application Names
    ApiResponse<Object> response = apiInstance.ApplicationsGetApplicationNamesWithHttpInfo(appVisiblity);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetApplicationNamesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **appVisiblity** | **string** | &lt;p&gt;appVisiblity : Visibility level of application names. Valid options: ALL, HIDDEN, REGULAR&lt;/p&gt; |  |

### Return type

**Object**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Application names returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get application names.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |
| **503** | &lt;p&gt;&lt;strong&gt;Service Unavailable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Naming exception or server exception.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsgetapplicationprovisionreport"></a>
# **ApplicationsGetApplicationProvisionReport**
> void ApplicationsGetApplicationProvisionReport (string applicationName)

Get Application Provisioning Report

<p>Gets provisioning report of specified application for all the users and groups having at least one application role (directly or inherited through groups). To invoke this API, logged in user should have at least <b>Database Manager</b> role for the specified application. Application roles are included only when logged in user has <b>Application Manager</b> role for the specified application.</p> <p>If you are using EPM Shared Services security mode, this operation is not available. Instead, manage users, groups, and permissions in the Shared Services Console.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsGetApplicationProvisionReportExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | Application name

            try
            {
                // Get Application Provisioning Report
                apiInstance.ApplicationsGetApplicationProvisionReport(applicationName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetApplicationProvisionReport: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsGetApplicationProvisionReportWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Provisioning Report
    apiInstance.ApplicationsGetApplicationProvisionReportWithHttpInfo(applicationName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetApplicationProvisionReportWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | Application name |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Provisioning report returned successfully, as Excel stream.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate application role.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsgetapplications"></a>
# **ApplicationsGetApplications**
> ApplicationList ApplicationsGetApplications (string filter = null, int? offset = null, int? limit = null, string connectionName = null, string applicationNameForConnection = null, string fields = null)

List Applications

<p>Returns the list of Essbase applications. Connection name and application name for connection are optional parameters.</p> <p>If you provide only a connection name with no application name, this API fetches all applications using that named connection.</p> <p>If you provide a connection name and application name, this API fetches the specified applications using the specified connection.</p> <p>Use the <code>fields</code> parameter to return only required fields.</p> <p>Limitation: If the application status is required in response, the limit must be less than or equal to 100.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsGetApplicationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var filter = "\"*\"";  // string |  (optional)  (default to "*")
            var offset = 0;  // int? | <p>Number of applications to omit from the start of the result set. Default value is 0.</p> (optional)  (default to 0)
            var limit = 50;  // int? | <p>Maximum number of applications to return. Default is 50.</p> (optional)  (default to 50)
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p> (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string | <p>Application name for connection.</p> (optional) 
            var fields = "fields_example";  // string | <p>Comma-separated list of fields to be returned in response fields. If omitted, all fields are returned.</p> (optional) 

            try
            {
                // List Applications
                ApplicationList result = apiInstance.ApplicationsGetApplications(filter, offset, limit, connectionName, applicationNameForConnection, fields);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetApplications: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsGetApplicationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Applications
    ApiResponse<ApplicationList> response = apiInstance.ApplicationsGetApplicationsWithHttpInfo(filter, offset, limit, connectionName, applicationNameForConnection, fields);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetApplicationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **filter** | **string** |  | [optional] [default to &quot;*&quot;] |
| **offset** | **int?** | &lt;p&gt;Number of applications to omit from the start of the result set. Default value is 0.&lt;/p&gt; | [optional] [default to 0] |
| **limit** | **int?** | &lt;p&gt;Maximum number of applications to return. Default is 50.&lt;/p&gt; | [optional] [default to 50] |
| **connectionName** | **string** | &lt;p&gt;Connection name.&lt;/p&gt; | [optional]  |
| **applicationNameForConnection** | **string** | &lt;p&gt;Application name for connection.&lt;/p&gt; | [optional]  |
| **fields** | **string** | &lt;p&gt;Comma-separated list of fields to be returned in response fields. If omitted, all fields are returned.&lt;/p&gt; | [optional]  |

### Return type

[**ApplicationList**](ApplicationList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Application list returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get applications.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsgetapplicationstree"></a>
# **ApplicationsGetApplicationsTree**
> string ApplicationsGetApplicationsTree ()

Get Application Tree View

<p>Gets the list of applications and databases as a tree view.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsGetApplicationsTreeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);

            try
            {
                // Get Application Tree View
                string result = apiInstance.ApplicationsGetApplicationsTree();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetApplicationsTree: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsGetApplicationsTreeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Tree View
    ApiResponse<string> response = apiInstance.ApplicationsGetApplicationsTreeWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetApplicationsTreeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

**string**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Tree view listing of applications and databases returned successfully.&lt;/p&gt; |  -  |
| **500** | Internal Server Error |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsgetcube"></a>
# **ApplicationsGetCube**
> Cube ApplicationsGetCube (string applicationName, string databaseName)

Get Database

<p>Returns details of database with specified database name and application name.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsGetCubeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Database
                Cube result = apiInstance.ApplicationsGetCube(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetCube: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsGetCubeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Database
    ApiResponse<Cube> response = apiInstance.ApplicationsGetCubeWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetCubeWithHttpInfo: " + e.Message);
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

[**Cube**](Cube.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Database details returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get database.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsgetcubes"></a>
# **ApplicationsGetCubes**
> CubeList ApplicationsGetCubes (string applicationName, string connectionName = null, string applicationNameForConnection = null)

List Databases

<p>Returns list of databases. Providing the connection name for which to list databases is optional. If a connection name is provided, connections created at the specified application is used to fetch the database list.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsGetCubesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p> (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string | <p>Application name from which to list databases.</p> (optional) 

            try
            {
                // List Databases
                CubeList result = apiInstance.ApplicationsGetCubes(applicationName, connectionName, applicationNameForConnection);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetCubes: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsGetCubesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Databases
    ApiResponse<CubeList> response = apiInstance.ApplicationsGetCubesWithHttpInfo(applicationName, connectionName, applicationNameForConnection);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsGetCubesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **connectionName** | **string** | &lt;p&gt;Connection name.&lt;/p&gt; | [optional]  |
| **applicationNameForConnection** | **string** | &lt;p&gt;Application name from which to list databases.&lt;/p&gt; | [optional]  |

### Return type

[**CubeList**](CubeList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Database list returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get databases.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsperformdboperation"></a>
# **ApplicationsPerformDbOperation**
> void ApplicationsPerformDbOperation (string applicationName, string databaseName, string action)

Start or Stop Database

<p>Performs specified action on the application and database. Valid actions are <b>Start</b> and <b>Stop</b>.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsPerformDbOperationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var action = "action_example";  // string | <p>Action - start or stop.</p>

            try
            {
                // Start or Stop Database
                apiInstance.ApplicationsPerformDbOperation(applicationName, databaseName, action);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsPerformDbOperation: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsPerformDbOperationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start or Stop Database
    apiInstance.ApplicationsPerformDbOperationWithHttpInfo(applicationName, databaseName, action);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsPerformDbOperationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **action** | **string** | &lt;p&gt;Action - start or stop.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Action performed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to perform action.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsperformoperation"></a>
# **ApplicationsPerformOperation**
> void ApplicationsPerformOperation (string applicationName, string action)

Start or Stop Application

<p>Performs specified action on the application. Valid actions are <b>Start</b> and <b>Stop</b>.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsPerformOperationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var action = "action_example";  // string | Action

            try
            {
                // Start or Stop Application
                apiInstance.ApplicationsPerformOperation(applicationName, action);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsPerformOperation: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsPerformOperationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Start or Stop Application
    apiInstance.ApplicationsPerformOperationWithHttpInfo(applicationName, action);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsPerformOperationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **action** | **string** | Action |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Action performed successfully.&lt;/p&gt;. |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to perform action.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationspromoteshadowapplication"></a>
# **ApplicationsPromoteShadowApplication**
> void ApplicationsPromoteShadowApplication (ShadowPromoteBean body)

Promote Shadow Application

<p>Promotes the shadow application as the base application. Conceptually, the promote operation is equivalent to moving the Essbase application directory from a source to destination location, at the file system level.</p> <p>Essbase must stop (unload) both applications, if they are running, before promoting. At the time of unloading, if the destination application is serving any ongoing operations, such as queries, Essbase terminates those operations and attempts to unload the application.</p> <p>If a graceful unload process fails or takes longer than permitted by the input argument <i>timeoutToForceUnloadApp</i> (unit=seconds), Essbase forcefully terminates the application.</p> <p>Example: if you specify 60 seconds for the timeout, but the termination of ongoing requests and graceful unloading of the application does not complete within one minute, Essbase triggers a forceful termination.  After termination, Essbase promotes the shadow application.</p> <p>The promote operation is supported on all applications, including aggregate storage, block storage, and Hybrid mode.</p> <p>Note: when moving an existing application, only the application and cube artifacts (such as metadata and data) are replaced from the source to destination.</p> <p>During a promotion, all security layer associations on the destination application, such as  users, groups, and security filters, are retained, while that of shadow/source are lost. The same rule applies for partition definitions.</p> <p>Example: If users X and Y had read-access to App1, and an admin promotes a shadow App2 to replace App1, both X and Y will be able to access App1.</p> <p>If user Z had access to App2, then after promotion, Z is not be able to access App1.</p> <p>Promotion from shadowed application to base is honored only if there are no changes to the number of cubes and cube names. In other words, if a cube gets renamed or if there is any addition or deletion of an application after it was shadowed, then promotion of such an application fails with an error, leaving both applications as they were.</p> <p>Example:  ASOAppNew.cubeNew <i>cannot</i> be replaced as ASO.cube. ASOAppNew.cube <i>can</i> be replaced as ASO.cube.</p> <p>Tips: You need not unload or stop the application prior to calling this promotion API. Essbase loads the application to gather information, and unloads it prior to moving the applications.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsPromoteShadowApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var body = new ShadowPromoteBean(); // ShadowPromoteBean | <p>shadowAppName: Name of the hidden shadow application that needs to be promoted to base.</p> <p>primaryAppName: Name of the primary application.</p> <p>timeoutToForceUnloadApp: Time interval (in seconds) to force unload in the event of applications performing an ongoing requests even after time interval.</p> <p>runInBackground: Specify <b>true</b> to schedule 'Shadow Promote' as a Job; otherwise, specify <b>false</b>.</p> 

            try
            {
                // Promote Shadow Application
                apiInstance.ApplicationsPromoteShadowApplication(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsPromoteShadowApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsPromoteShadowApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Promote Shadow Application
    apiInstance.ApplicationsPromoteShadowApplicationWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsPromoteShadowApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**ShadowPromoteBean**](ShadowPromoteBean.md) | &lt;p&gt;shadowAppName: Name of the hidden shadow application that needs to be promoted to base.&lt;/p&gt; &lt;p&gt;primaryAppName: Name of the primary application.&lt;/p&gt; &lt;p&gt;timeoutToForceUnloadApp: Time interval (in seconds) to force unload in the event of applications performing an ongoing requests even after time interval.&lt;/p&gt; &lt;p&gt;runInBackground: Specify &lt;b&gt;true&lt;/b&gt; to schedule &#39;Shadow Promote&#39; as a Job; otherwise, specify &lt;b&gt;false&lt;/b&gt;.&lt;/p&gt;  |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Promote shadow application completed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to promote shadow application.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |
| **503** | &lt;p&gt;&lt;strong&gt;Service Unavailable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Naming exception or server exception.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsrenameapplication"></a>
# **ApplicationsRenameApplication**
> void ApplicationsRenameApplication (CopyRenameBean body)

Rename Application

<p>Renames an application. You must provide the source and destination application names.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsRenameApplicationExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var body = new CopyRenameBean(); // CopyRenameBean | <p>Source and destination application information.</p>

            try
            {
                // Rename Application
                apiInstance.ApplicationsRenameApplication(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsRenameApplication: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsRenameApplicationWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Rename Application
    apiInstance.ApplicationsRenameApplicationWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsRenameApplicationWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**CopyRenameBean**](CopyRenameBean.md) | &lt;p&gt;Source and destination application information.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Application renamed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to rename application.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationsrenamedatabase"></a>
# **ApplicationsRenameDatabase**
> void ApplicationsRenameDatabase (string applicationName, CopyRenameBean body)

Rename Database

<p>Renames a database. You must provide the source application name, and the source and destination database names. Destination application name is not required.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationsRenameDatabaseExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Source application name.</p>
            var body = new CopyRenameBean(); // CopyRenameBean | <p>Source and destination database information.</p>

            try
            {
                // Rename Database
                apiInstance.ApplicationsRenameDatabase(applicationName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.ApplicationsRenameDatabase: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationsRenameDatabaseWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Rename Database
    apiInstance.ApplicationsRenameDatabaseWithHttpInfo(applicationName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.ApplicationsRenameDatabaseWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Source application name.&lt;/p&gt; |  |
| **body** | [**CopyRenameBean**](CopyRenameBean.md) | &lt;p&gt;Source and destination database information.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Database renamed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to rename database.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="databasesformulafunctions"></a>
# **DatabasesFormulaFunctions**
> string DatabasesFormulaFunctions (string applicationName, string databaseName)

Get Formula Functions

<p>Returns a list of functions for defining formulas. For an aggregate storage cube, the list contains MDX functions. For a block storage cube, the list contains Essbase calculation functions.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabasesFormulaFunctionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Formula Functions
                string result = apiInstance.DatabasesFormulaFunctions(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.DatabasesFormulaFunctions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabasesFormulaFunctionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Formula Functions
    ApiResponse<string> response = apiInstance.DatabasesFormulaFunctionsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.DatabasesFormulaFunctionsWithHttpInfo: " + e.Message);
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

**string**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;List of functions, as XML or JSON string, returned successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="databasesgetcalculationfunctions"></a>
# **DatabasesGetCalculationFunctions**
> string DatabasesGetCalculationFunctions (string applicationName, string databaseName)

Get Calculation Functions

<p>Returns list of common and database-specific calculation functions.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabasesGetCalculationFunctionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Calculation Functions
                string result = apiInstance.DatabasesGetCalculationFunctions(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.DatabasesGetCalculationFunctions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabasesGetCalculationFunctionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Calculation Functions
    ApiResponse<string> response = apiInstance.DatabasesGetCalculationFunctionsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.DatabasesGetCalculationFunctionsWithHttpInfo: " + e.Message);
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

**string**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;XML or JSON string containing list of calculation functions returned successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="databasesgetcurrencysettings"></a>
# **DatabasesGetCurrencySettings**
> string DatabasesGetCurrencySettings (string applicationName, string databaseName)

Get Currency Settings

<p>Returns the currency settings for the database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabasesGetCurrencySettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Currency Settings
                string result = apiInstance.DatabasesGetCurrencySettings(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.DatabasesGetCurrencySettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabasesGetCurrencySettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Currency Settings
    ApiResponse<string> response = apiInstance.DatabasesGetCurrencySettingsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.DatabasesGetCurrencySettingsWithHttpInfo: " + e.Message);
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

**string**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Currency settings returned successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="databasesgetmdxfunctions"></a>
# **DatabasesGetMdxFunctions**
> string DatabasesGetMdxFunctions (string applicationName, string databaseName)

Get MDX Functions

Returns list of MDX functions

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabasesGetMdxFunctionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Application name.</p>

            try
            {
                // Get MDX Functions
                string result = apiInstance.DatabasesGetMdxFunctions(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.DatabasesGetMdxFunctions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabasesGetMdxFunctionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get MDX Functions
    ApiResponse<string> response = apiInstance.DatabasesGetMdxFunctionsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.DatabasesGetMdxFunctionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

### Return type

**string**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;XML or JSON string containing list of MDX functions returned successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="databasessetcurrencysettings"></a>
# **DatabasesSetCurrencySettings**
> string DatabasesSetCurrencySettings (string applicationName, string databaseName, CurrencySettings body)

Set Currency Settings

<p>Updates the currency settings for the database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabasesSetCurrencySettingsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new CurrencySettings(); // CurrencySettings | <p>Currency settings.</p>

            try
            {
                // Set Currency Settings
                string result = apiInstance.DatabasesSetCurrencySettings(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.DatabasesSetCurrencySettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabasesSetCurrencySettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Set Currency Settings
    ApiResponse<string> response = apiInstance.DatabasesSetCurrencySettingsWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.DatabasesSetCurrencySettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**CurrencySettings**](CurrencySettings.md) | &lt;p&gt;Currency settings.&lt;/p&gt; |  |

### Return type

**string**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Currency settings updated successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="setactivealias"></a>
# **SetActiveAlias**
> StringCollectionResponse SetActiveAlias (string applicationName, string databaseName, string aliasTableName = null)

Set Active Alias

Sets the active alias table associated with the specified application and database.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class SetActiveAliasExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var aliasTableName = "aliasTableName_example";  // string | <p>Alias table name.</p> (optional) 

            try
            {
                // Set Active Alias
                StringCollectionResponse result = apiInstance.SetActiveAlias(applicationName, databaseName, aliasTableName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationsApi.SetActiveAlias: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SetActiveAliasWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Set Active Alias
    ApiResponse<StringCollectionResponse> response = apiInstance.SetActiveAliasWithHttpInfo(applicationName, databaseName, aliasTableName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationsApi.SetActiveAliasWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **aliasTableName** | **string** | &lt;p&gt;Alias table name.&lt;/p&gt; | [optional]  |

### Return type

[**StringCollectionResponse**](StringCollectionResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Alias tables returned successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

