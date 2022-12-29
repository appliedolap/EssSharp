# EssSharp.Api.AuditTrailApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AuditTrailGetDataAudit**](AuditTrailApi.md#audittrailgetdataaudit) | **GET** /applications/{application}/databases/{database}/audittrail/data | Get Audit Data |
| [**AuditTrailPurge**](AuditTrailApi.md#audittrailpurge) | **DELETE** /applications/{application}/databases/{database}/audittrail/data | Delete Audit Data |

<a name="audittrailgetdataaudit"></a>
# **AuditTrailGetDataAudit**
> string AuditTrailGetDataAudit (string application, string database, long? fetchCount = null)

Get Audit Data

<p>Returns audit trail data in CSV string format if <code>Accept='text/csv'</code> or <code>Accept='text/plain'</code>. If <code>Accept='application/octet-stream'</code>, returns audit data as a CSV stream to download. If <code>Accept='application/json'</code>, returns the audit data list in JSON format.<p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AuditTrailGetDataAuditExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new AuditTrailApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var fetchCount = 789L;  // long? | <p>Number of records to be fetched.</p> (optional) 

            try
            {
                // Get Audit Data
                string result = apiInstance.AuditTrailGetDataAudit(application, database, fetchCount);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AuditTrailApi.AuditTrailGetDataAudit: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AuditTrailGetDataAuditWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Audit Data
    ApiResponse<string> response = apiInstance.AuditTrailGetDataAuditWithHttpInfo(application, database, fetchCount);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AuditTrailApi.AuditTrailGetDataAuditWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **fetchCount** | **long?** | &lt;p&gt;Number of records to be fetched.&lt;/p&gt; | [optional]  |

### Return type

**string**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, text/csv


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Audit trail data returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to return audit data.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="audittrailpurge"></a>
# **AuditTrailPurge**
> void AuditTrailPurge (string application, string database, long? olderthan = null)

Delete Audit Data

<p>Deletes audit trail data older than the specified time.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AuditTrailPurgeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new AuditTrailApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var olderthan = 789L;  // long? | <p>Time in milliseconds.</p> (optional) 

            try
            {
                // Delete Audit Data
                apiInstance.AuditTrailPurge(application, database, olderthan);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AuditTrailApi.AuditTrailPurge: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AuditTrailPurgeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Audit Data
    apiInstance.AuditTrailPurgeWithHttpInfo(application, database, olderthan);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AuditTrailApi.AuditTrailPurgeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **olderthan** | **long?** | &lt;p&gt;Time in milliseconds.&lt;/p&gt; | [optional]  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Audit data deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete audit data.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

