# EssSharp.Api.JobsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**JobsExecuteByJobId**](JobsApi.md#jobsexecutebyjobid) | **POST** /jobs/{id} | Rerun Job |
| [**JobsExecuteJob**](JobsApi.md#jobsexecutejob) | **POST** /jobs | Execute Job |
| [**JobsGetAllJobRecords**](JobsApi.md#jobsgetalljobrecords) | **GET** /jobs | Get Job List |
| [**JobsGetJobInfo**](JobsApi.md#jobsgetjobinfo) | **GET** /jobs/{id} | Get Job |
| [**JobsGetJobStatistics**](JobsApi.md#jobsgetjobstatistics) | **GET** /jobs/statistics/{userId} | Get Job Statistics |
| [**JobsPurge**](JobsApi.md#jobspurge) | **DELETE** /jobs/purge | Delete jobs |

<a id="jobsexecutebyjobid"></a>
# **JobsExecuteByJobId**
> JobRecordBean JobsExecuteByJobId (long id)

Rerun Job

<p>Reruns the job, returning job information and new job ID.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class JobsExecuteByJobIdExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new JobsApi(config);
            var id = 789L;  // long | <p>Job ID.</p>

            try
            {
                // Rerun Job
                JobRecordBean result = apiInstance.JobsExecuteByJobId(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling JobsApi.JobsExecuteByJobId: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the JobsExecuteByJobIdWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Rerun Job
    ApiResponse<JobRecordBean> response = apiInstance.JobsExecuteByJobIdWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling JobsApi.JobsExecuteByJobIdWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **long** | &lt;p&gt;Job ID.&lt;/p&gt; |  |

### Return type

[**JobRecordBean**](JobRecordBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Job information returned successfully for newly created job ID.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |
| **503** | &lt;p&gt;&lt;strong&gt;Service Unavailable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Naming exception or server exception. Job ID is incorrect or invalid.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="jobsexecutejob"></a>
# **JobsExecuteJob**
> JobRecordBean JobsExecuteJob (JobsInputBean body)

Execute Job

<p>Executes the job and returns the record containing job information, such as job ID, status, inputs, and output information for the current job.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class JobsExecuteJobExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new JobsApi(config);
            var body = new JobsInputBean(); // JobsInputBean | parameter provided as json string in the request body

            try
            {
                // Execute Job
                JobRecordBean result = apiInstance.JobsExecuteJob(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling JobsApi.JobsExecuteJob: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the JobsExecuteJobWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Execute Job
    ApiResponse<JobRecordBean> response = apiInstance.JobsExecuteJobWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling JobsApi.JobsExecuteJobWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**JobsInputBean**](JobsInputBean.md) | parameter provided as json string in the request body |  |

### Return type

[**JobRecordBean**](JobRecordBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Job started successfully. Job information returned in response.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Application may not exist, or application parameter may be incorrect. Or, database may not exist, or database parameter may be incorrect. Or, a null argument may have been passed.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |
| **503** | &lt;p&gt;&lt;strong&gt;Service Unavailable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Naming exception or server exception.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="jobsgetalljobrecords"></a>
# **JobsGetAllJobRecords**
> JobRecordPaginatedResultWrapper JobsGetAllJobRecords (string keyword = null, string fullAppName = null, string orderBy = null, long? offset = null, long? limit = null, bool? systemjobs = null)

Get Job List

<p>Returns job list for the given query parameters, including job status, type, and input and output information. If no query parameter is specified, returns a list of all the jobs.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class JobsGetAllJobRecordsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new JobsApi(config);
            var keyword = "keyword_example";  // string | <p>Filter the job records using a keyword that may be part of the job ID, application name, database name, job file name (script), or user name. If this parameter and fullAppName are both specified, fullAppName takes precedence.</p> (optional) 
            var fullAppName = "fullAppName_example";  // string | <p>Application name for which to retrieve job records.</p> (optional) 
            var orderBy = "\"job_ID:desc\"";  // string | <p>Order By specification. By default, jobs records are returned by job IDs in descending order.</p> (optional)  (default to "job_ID:desc")
            var offset = 0L;  // long? | <p>Number of jobs to omit from the start of the result set.</p> (optional)  (default to 0)
            var limit = 50L;  // long? | <p>Maximum number of jobs to fetch. </p> (optional)  (default to 50)
            var systemjobs = false;  // bool? | <p>Include backup jobs in jobs records.</p> (optional)  (default to false)

            try
            {
                // Get Job List
                JobRecordPaginatedResultWrapper result = apiInstance.JobsGetAllJobRecords(keyword, fullAppName, orderBy, offset, limit, systemjobs);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling JobsApi.JobsGetAllJobRecords: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the JobsGetAllJobRecordsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Job List
    ApiResponse<JobRecordPaginatedResultWrapper> response = apiInstance.JobsGetAllJobRecordsWithHttpInfo(keyword, fullAppName, orderBy, offset, limit, systemjobs);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling JobsApi.JobsGetAllJobRecordsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **keyword** | **string** | &lt;p&gt;Filter the job records using a keyword that may be part of the job ID, application name, database name, job file name (script), or user name. If this parameter and fullAppName are both specified, fullAppName takes precedence.&lt;/p&gt; | [optional]  |
| **fullAppName** | **string** | &lt;p&gt;Application name for which to retrieve job records.&lt;/p&gt; | [optional]  |
| **orderBy** | **string** | &lt;p&gt;Order By specification. By default, jobs records are returned by job IDs in descending order.&lt;/p&gt; | [optional] [default to &quot;job_ID:desc&quot;] |
| **offset** | **long?** | &lt;p&gt;Number of jobs to omit from the start of the result set.&lt;/p&gt; | [optional] [default to 0] |
| **limit** | **long?** | &lt;p&gt;Maximum number of jobs to fetch. &lt;/p&gt; | [optional] [default to 50] |
| **systemjobs** | **bool?** | &lt;p&gt;Include backup jobs in jobs records.&lt;/p&gt; | [optional] [default to false] |

### Return type

[**JobRecordPaginatedResultWrapper**](JobRecordPaginatedResultWrapper.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Job records returned successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |
| **503** | &lt;p&gt;&lt;strong&gt;Service Unavailable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Naming exception or server exception.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="jobsgetjobinfo"></a>
# **JobsGetJobInfo**
> JobRecordBean JobsGetJobInfo (string id)

Get Job

<p>Returns job information for given job ID, including job status, type, and input and output information.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class JobsGetJobInfoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new JobsApi(config);
            var id = "id_example";  // string | Job ID

            try
            {
                // Get Job
                JobRecordBean result = apiInstance.JobsGetJobInfo(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling JobsApi.JobsGetJobInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the JobsGetJobInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Job
    ApiResponse<JobRecordBean> response = apiInstance.JobsGetJobInfoWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling JobsApi.JobsGetJobInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **string** | Job ID |  |

### Return type

[**JobRecordBean**](JobRecordBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Job information returned successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |
| **503** | &lt;p&gt;&lt;strong&gt;Service Unavailable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Naming exception or server exception.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="jobsgetjobstatistics"></a>
# **JobsGetJobStatistics**
> JobStatisticsBean JobsGetJobStatistics (string userId)

Get Job Statistics

<p>Returns the job statistics for the currently logged in user, including number of jobs that are successful, number of jobs containing errors, number of jobs containing warnings, and the number of jobs running.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class JobsGetJobStatisticsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new JobsApi(config);
            var userId = "userId_example";  // string | User Id of the logged in user

            try
            {
                // Get Job Statistics
                JobStatisticsBean result = apiInstance.JobsGetJobStatistics(userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling JobsApi.JobsGetJobStatistics: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the JobsGetJobStatisticsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Job Statistics
    ApiResponse<JobStatisticsBean> response = apiInstance.JobsGetJobStatisticsWithHttpInfo(userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling JobsApi.JobsGetJobStatisticsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **userId** | **string** | User Id of the logged in user |  |

### Return type

[**JobStatisticsBean**](JobStatisticsBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Job statistics returned successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |
| **503** | &lt;p&gt;Naming Exception or Server Exception.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="jobspurge"></a>
# **JobsPurge**
> void JobsPurge (long? olderthan = null, long? rangeStartTime = null, long? rangeEndTime = null, string application = null, string database = null, string jobtype = null, int? jobstatus = null)

Delete jobs

<p>Deletes jobs data older than the specified time.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class JobsPurgeExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new JobsApi(config);
            var olderthan = 789L;  // long? | <p>Time in milliseconds.</p> (optional) 
            var rangeStartTime = 789L;  // long? | <p>Start Time range in milliseconds.</p> (optional) 
            var rangeEndTime = 789L;  // long? | <p>End Time range in milliseconds.</p> (optional) 
            var application = "application_example";  // string | <p>Application name.</p> (optional) 
            var database = "database_example";  // string | <p>Database name.</p> (optional) 
            var jobtype = "jobtype_example";  // string | <p>Job Type.</p> (optional) 
            var jobstatus = 56;  // int? | <p>Job Status.</p> (optional) 

            try
            {
                // Delete jobs
                apiInstance.JobsPurge(olderthan, rangeStartTime, rangeEndTime, application, database, jobtype, jobstatus);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling JobsApi.JobsPurge: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the JobsPurgeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete jobs
    apiInstance.JobsPurgeWithHttpInfo(olderthan, rangeStartTime, rangeEndTime, application, database, jobtype, jobstatus);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling JobsApi.JobsPurgeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **olderthan** | **long?** | &lt;p&gt;Time in milliseconds.&lt;/p&gt; | [optional]  |
| **rangeStartTime** | **long?** | &lt;p&gt;Start Time range in milliseconds.&lt;/p&gt; | [optional]  |
| **rangeEndTime** | **long?** | &lt;p&gt;End Time range in milliseconds.&lt;/p&gt; | [optional]  |
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; | [optional]  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; | [optional]  |
| **jobtype** | **string** | &lt;p&gt;Job Type.&lt;/p&gt; | [optional]  |
| **jobstatus** | **int?** | &lt;p&gt;Job Status.&lt;/p&gt; | [optional]  |

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
| **0** | successful operation |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

