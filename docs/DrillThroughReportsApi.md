# EssSharp.Api.DrillThroughReportsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DrillThroughReportsCreate**](DrillThroughReportsApi.md#drillthroughreportscreate) | **POST** /applications/{applicationName}/databases/{databaseName}/reports | Create Drill Through Report |
| [**DrillThroughReportsDelete**](DrillThroughReportsApi.md#drillthroughreportsdelete) | **DELETE** /applications/{applicationName}/databases/{databaseName}/reports/{name} | Delete Drill Through Report |
| [**DrillThroughReportsExecute**](DrillThroughReportsApi.md#drillthroughreportsexecute) | **POST** /applications/{applicationName}/databases/{databaseName}/reports/{name} | Execute Drill Through Report |
| [**DrillThroughReportsGetReport**](DrillThroughReportsApi.md#drillthroughreportsgetreport) | **GET** /applications/{applicationName}/databases/{databaseName}/reports/{name} | Get Drill Through Report |
| [**DrillThroughReportsGetReports**](DrillThroughReportsApi.md#drillthroughreportsgetreports) | **GET** /applications/{applicationName}/databases/{databaseName}/reports | Get Drill Through Reports |
| [**DrillThroughReportsUpdateReport**](DrillThroughReportsApi.md#drillthroughreportsupdatereport) | **PUT** /applications/{applicationName}/databases/{databaseName}/reports/{name} | Update Drill Through Reoprt |

<a name="drillthroughreportscreate"></a>
# **DrillThroughReportsCreate**
> DrillthroughBean DrillThroughReportsCreate (string applicationName, string databaseName, DrillthroughBean body)

Create Drill Through Report

<p>Creates a drill through report in the specified application and database, and returns details about the report.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DrillThroughReportsCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DrillThroughReportsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new DrillthroughBean(); // DrillthroughBean | <p>Drill through report details.</p>

            try
            {
                // Create Drill Through Report
                DrillthroughBean result = apiInstance.DrillThroughReportsCreate(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DrillThroughReportsCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Drill Through Report
    ApiResponse<DrillthroughBean> response = apiInstance.DrillThroughReportsCreateWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**DrillthroughBean**](DrillthroughBean.md) | &lt;p&gt;Drill through report details.&lt;/p&gt; |  |

### Return type

[**DrillthroughBean**](DrillthroughBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The drill through report was created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create the drill through report. The application or database name may be incorrect, or the report may already exist.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="drillthroughreportsdelete"></a>
# **DrillThroughReportsDelete**
> void DrillThroughReportsDelete (string applicationName, string databaseName, string name)

Delete Drill Through Report

<p>Deletes the specified drill through report from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DrillThroughReportsDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DrillThroughReportsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var name = "name_example";  // string | <p>Drill through report name.</p>

            try
            {
                // Delete Drill Through Report
                apiInstance.DrillThroughReportsDelete(applicationName, databaseName, name);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DrillThroughReportsDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Drill Through Report
    apiInstance.DrillThroughReportsDeleteWithHttpInfo(applicationName, databaseName, name);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsDeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **name** | **string** | &lt;p&gt;Drill through report name.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The drill through report was deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete the drill through report. The report name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="drillthroughreportsexecute"></a>
# **DrillThroughReportsExecute**
> void DrillThroughReportsExecute (string applicationName, string databaseName, string name, DrillthroughMetadataBean body)

Execute Drill Through Report

<p>Executes a drill through report in the specified application and database, and returns records.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DrillThroughReportsExecuteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DrillThroughReportsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var name = "name_example";  // string | <p>Drillthrough report name.</p>
            var body = new DrillthroughMetadataBean(); // DrillthroughMetadataBean | <p>Drillthrough metadata. In example request body, dtrContext corresponds to the intersection of cells in smartview.</p>

            try
            {
                // Execute Drill Through Report
                apiInstance.DrillThroughReportsExecute(applicationName, databaseName, name, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsExecute: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DrillThroughReportsExecuteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Execute Drill Through Report
    apiInstance.DrillThroughReportsExecuteWithHttpInfo(applicationName, databaseName, name, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsExecuteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **name** | **string** | &lt;p&gt;Drillthrough report name.&lt;/p&gt; |  |
| **body** | [**DrillthroughMetadataBean**](DrillthroughMetadataBean.md) | &lt;p&gt;Drillthrough metadata. In example request body, dtrContext corresponds to the intersection of cells in smartview.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The drillthrough report was executed successfully. Result is a json array where first node is the datatype of each column, second node is the column names and rest are the data nodes corresponding to each record in the report.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to execute the drillthrough report.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="drillthroughreportsgetreport"></a>
# **DrillThroughReportsGetReport**
> DrillthroughBean DrillThroughReportsGetReport (string applicationName, string databaseName, string name)

Get Drill Through Report

<p>Returns the specified drill through report from the specified application and database.<p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DrillThroughReportsGetReportExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DrillThroughReportsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var name = "name_example";  // string | <p>Drill through report name.</p>

            try
            {
                // Get Drill Through Report
                DrillthroughBean result = apiInstance.DrillThroughReportsGetReport(applicationName, databaseName, name);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsGetReport: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DrillThroughReportsGetReportWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Drill Through Report
    ApiResponse<DrillthroughBean> response = apiInstance.DrillThroughReportsGetReportWithHttpInfo(applicationName, databaseName, name);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsGetReportWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **name** | **string** | &lt;p&gt;Drill through report name.&lt;/p&gt; |  |

### Return type

[**DrillthroughBean**](DrillthroughBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The drill through report was retrieved successfully. Returns the links to get, edit, or delete the report.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the report. The application name, database name, or report name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="drillthroughreportsgetreports"></a>
# **DrillThroughReportsGetReports**
> ReportList DrillThroughReportsGetReports (string applicationName, string databaseName)

Get Drill Through Reports

<p>Returns all the drill through reports from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DrillThroughReportsGetReportsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DrillThroughReportsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Drill Through Reports
                ReportList result = apiInstance.DrillThroughReportsGetReports(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsGetReports: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DrillThroughReportsGetReportsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Drill Through Reports
    ApiResponse<ReportList> response = apiInstance.DrillThroughReportsGetReportsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsGetReportsWithHttpInfo: " + e.Message);
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

[**ReportList**](ReportList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The drill through reports were retrieved successfully. Returns the links to get, edit, or delete the reports.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the drill through reports. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="drillthroughreportsupdatereport"></a>
# **DrillThroughReportsUpdateReport**
> DrillthroughBean DrillThroughReportsUpdateReport (string applicationName, string databaseName, string name, DrillthroughBean body)

Update Drill Through Reoprt

<p>Updates the drill through report in the specified application and database, and returns details of the updated report.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DrillThroughReportsUpdateReportExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DrillThroughReportsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var name = "name_example";  // string | <p>Drill through report name.</p>
            var body = new DrillthroughBean(); // DrillthroughBean | <p>Drill through report details.</p>

            try
            {
                // Update Drill Through Reoprt
                DrillthroughBean result = apiInstance.DrillThroughReportsUpdateReport(applicationName, databaseName, name, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsUpdateReport: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DrillThroughReportsUpdateReportWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Drill Through Reoprt
    ApiResponse<DrillthroughBean> response = apiInstance.DrillThroughReportsUpdateReportWithHttpInfo(applicationName, databaseName, name, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DrillThroughReportsApi.DrillThroughReportsUpdateReportWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **name** | **string** | &lt;p&gt;Drill through report name.&lt;/p&gt; |  |
| **body** | [**DrillthroughBean**](DrillthroughBean.md) | &lt;p&gt;Drill through report details.&lt;/p&gt; |  |

### Return type

[**DrillthroughBean**](DrillthroughBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The drill through report was updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the report. The application name, database name, or report name may be incorrect, or the specified report name may not exist.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

