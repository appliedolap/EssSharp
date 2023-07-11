# EssSharp.Api.ScenariosApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ScenariosAddApprover**](ScenariosApi.md#scenariosaddapprover) | **POST** /scenarios/{scenarioId}/approvers | Add Approver |
| [**ScenariosAddComment**](ScenariosApi.md#scenariosaddcomment) | **POST** /scenarios/{scenarioId}/comments | Add Comment |
| [**ScenariosAddParticipant**](ScenariosApi.md#scenariosaddparticipant) | **POST** /scenarios/{scenarioId}/participants | Add Participant |
| [**ScenariosCreate**](ScenariosApi.md#scenarioscreate) | **POST** /scenarios | Create Scenario |
| [**ScenariosDelete**](ScenariosApi.md#scenariosdelete) | **DELETE** /scenarios/{id} | &lt;p&gt;Deletes a scenario by ID.&lt;/p&gt; |
| [**ScenariosDeleteApprover**](ScenariosApi.md#scenariosdeleteapprover) | **DELETE** /scenarios/{scenarioId}/approvers/{userId} | Delete Approver |
| [**ScenariosDeleteComment**](ScenariosApi.md#scenariosdeletecomment) | **DELETE** /scenarios/{scenarioId}/comments/{commentId} | Delete Comment |
| [**ScenariosDeleteParticipant**](ScenariosApi.md#scenariosdeleteparticipant) | **DELETE** /scenarios/{scenarioId}/participants/{userId} | Delete Participant |
| [**ScenariosGet**](ScenariosApi.md#scenariosget) | **GET** /scenarios/{id} | Get Scenario |
| [**ScenariosGetApprovers**](ScenariosApi.md#scenariosgetapprovers) | **GET** /scenarios/{scenarioId}/approvers | Get Approvers |
| [**ScenariosGetChangeData**](ScenariosApi.md#scenariosgetchangedata) | **GET** /scenarios/{id}/changes | Get Changes |
| [**ScenariosGetComment**](ScenariosApi.md#scenariosgetcomment) | **GET** /scenarios/{scenarioId}/comments/{commentId} | Get Comment |
| [**ScenariosGetComments**](ScenariosApi.md#scenariosgetcomments) | **GET** /scenarios/{scenarioId}/comments | Get Comments |
| [**ScenariosGetParticipants**](ScenariosApi.md#scenariosgetparticipants) | **GET** /scenarios/{scenarioId}/participants | Get Participants |
| [**ScenariosGetRegisteredCubes**](ScenariosApi.md#scenariosgetregisteredcubes) | **GET** /scenarios/databases | Get Scenario-Enabled Cubes |
| [**ScenariosGetScenarios**](ScenariosApi.md#scenariosgetscenarios) | **GET** /scenarios | Get Scenarios |
| [**ScenariosGetScripts**](ScenariosApi.md#scenariosgetscripts) | **GET** /scenarios/{scenarioId}/scripts | Get Scenario Scripts |
| [**ScenariosPatch**](ScenariosApi.md#scenariospatch) | **PATCH** /scenarios/{id} | Update Scenario Partially |
| [**ScenariosPerformAction**](ScenariosApi.md#scenariosperformaction) | **POST** /scenarios/{id} | Perform Scenario Action |
| [**ScenariosUpdate**](ScenariosApi.md#scenariosupdate) | **PUT** /scenarios/{id} | Update Scenario Fully |
| [**ScenariosUpdateComment**](ScenariosApi.md#scenariosupdatecomment) | **PUT** /scenarios/{scenarioId}/comments/{commentId} | Update Comment |
| [**ScenariosUpdateScript**](ScenariosApi.md#scenariosupdatescript) | **PUT** /scenarios/{scenarioId}/scripts/{scriptType} | Update Scenario with Script |

<a id="scenariosaddapprover"></a>
# **ScenariosAddApprover**
> void ScenariosAddApprover (long scenarioId, ApproverBean body = null)

Add Approver

<p>Adds a scenario approver. Approvers are optional, and must have Database Access or higher role. They monitor and approve or reject scenarios. If a scenario has multiple approvers, each one must approve before it can be submitted.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosAddApproverExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>
            var body = new ApproverBean(); // ApproverBean | <p>Approver details.</p> (optional) 

            try
            {
                // Add Approver
                apiInstance.ScenariosAddApprover(scenarioId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosAddApprover: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosAddApproverWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Approver
    apiInstance.ScenariosAddApproverWithHttpInfo(scenarioId, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosAddApproverWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **body** | [**ApproverBean**](ApproverBean.md) | &lt;p&gt;Approver details.&lt;/p&gt; | [optional]  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario approver added successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to add approver. The scenario ID may be invalid, or the scenario may already have been submitted for approval.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosaddcomment"></a>
# **ScenariosAddComment**
> CommentBean ScenariosAddComment (long scenarioId, CommentBean body = null)

Add Comment

<p>Adds a comment to the specified scenario ID.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosAddCommentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>
            var body = new CommentBean(); // CommentBean | <p>Comment details.</p> (optional) 

            try
            {
                // Add Comment
                CommentBean result = apiInstance.ScenariosAddComment(scenarioId, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosAddComment: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosAddCommentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Comment
    ApiResponse<CommentBean> response = apiInstance.ScenariosAddCommentWithHttpInfo(scenarioId, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosAddCommentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **body** | [**CommentBean**](CommentBean.md) | &lt;p&gt;Comment details.&lt;/p&gt; | [optional]  |

### Return type

[**CommentBean**](CommentBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario comment added successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to add a comment. The scenario ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosaddparticipant"></a>
# **ScenariosAddParticipant**
> void ScenariosAddParticipant (long scenarioId, ParticipantBean body = null)

Add Participant

<p>Adds a scenario participant to the specified scenario ID.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosAddParticipantExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>
            var body = new ParticipantBean(); // ParticipantBean | <p>Participant details.</p> (optional) 

            try
            {
                // Add Participant
                apiInstance.ScenariosAddParticipant(scenarioId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosAddParticipant: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosAddParticipantWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Participant
    apiInstance.ScenariosAddParticipantWithHttpInfo(scenarioId, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosAddParticipantWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **body** | [**ParticipantBean**](ParticipantBean.md) | &lt;p&gt;Participant details.&lt;/p&gt; | [optional]  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario participant added successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to add participant. The scenario ID may be invalid, or the scenario may already have been submitted for approval.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenarioscreate"></a>
# **ScenariosCreate**
> ScenarioBean ScenariosCreate (ScenarioBean body = null)

Create Scenario

<p>Creates a new scenario. A scenario is a private work area in which you can model different assumptions within the data without affecting the existing data.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosCreateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var body = new ScenarioBean(); // ScenarioBean | <p>Scenario details.</p> (optional) 

            try
            {
                // Create Scenario
                ScenarioBean result = apiInstance.ScenariosCreate(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosCreate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosCreateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Scenario
    ApiResponse<ScenarioBean> response = apiInstance.ScenariosCreateWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosCreateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**ScenarioBean**](ScenarioBean.md) | &lt;p&gt;Scenario details.&lt;/p&gt; | [optional]  |

### Return type

[**ScenarioBean**](ScenarioBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create the scenario. Required fields such as scenario name or due date may be empty.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosdelete"></a>
# **ScenariosDelete**
> void ScenariosDelete (long id)

<p>Deletes a scenario by ID.</p>

<p>Deletes a scenario by ID.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosDeleteExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var id = 789L;  // long | <p>Scenario ID.</p>

            try
            {
                // <p>Deletes a scenario by ID.</p>
                apiInstance.ScenariosDelete(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosDelete: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosDeleteWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // <p>Deletes a scenario by ID.</p>
    apiInstance.ScenariosDeleteWithHttpInfo(id);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosDeleteWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete scenario. The scenario ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosdeleteapprover"></a>
# **ScenariosDeleteApprover**
> void ScenariosDeleteApprover (long scenarioId, string userId)

Delete Approver

<p>Deletes a scenario approver.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosDeleteApproverExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>
            var userId = "userId_example";  // string | <p>Approver ID.</p>

            try
            {
                // Delete Approver
                apiInstance.ScenariosDeleteApprover(scenarioId, userId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosDeleteApprover: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosDeleteApproverWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Approver
    apiInstance.ScenariosDeleteApproverWithHttpInfo(scenarioId, userId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosDeleteApproverWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **userId** | **string** | &lt;p&gt;Approver ID.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario approver deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete scenario approver. The scenario ID may be invalid, or the scenario may already have been submitted for approval.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosdeletecomment"></a>
# **ScenariosDeleteComment**
> void ScenariosDeleteComment (long scenarioId, long commentId)

Delete Comment

<p>Deletes a scenario comment by ID.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosDeleteCommentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>
            var commentId = 789L;  // long | <p>Comment ID.</p>

            try
            {
                // Delete Comment
                apiInstance.ScenariosDeleteComment(scenarioId, commentId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosDeleteComment: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosDeleteCommentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Comment
    apiInstance.ScenariosDeleteCommentWithHttpInfo(scenarioId, commentId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosDeleteCommentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **commentId** | **long** | &lt;p&gt;Comment ID.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario comment deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete scenario comment. The scenario ID or comment ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosdeleteparticipant"></a>
# **ScenariosDeleteParticipant**
> void ScenariosDeleteParticipant (long scenarioId, string userId)

Delete Participant

<p>Deletes a scenario participant.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosDeleteParticipantExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>
            var userId = "userId_example";  // string | <p>Participant ID.</p>

            try
            {
                // Delete Participant
                apiInstance.ScenariosDeleteParticipant(scenarioId, userId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosDeleteParticipant: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosDeleteParticipantWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Participant
    apiInstance.ScenariosDeleteParticipantWithHttpInfo(scenarioId, userId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosDeleteParticipantWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **userId** | **string** | &lt;p&gt;Participant ID.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario participant deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete scenario participant. The scenario ID may be invalid, or the scenario may already have been submitted for approval.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosget"></a>
# **ScenariosGet**
> ScenarioBean ScenariosGet (long id, string expand = null)

Get Scenario

<p>Gets a scenario by ID.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var id = 789L;  // long | <p>Scenario ID.</p>
            var expand = "expand_example";  // string | <p>Value can be <code>all</code> or <code>none</code>. Default value is <code>none</code>, meaning only links are returned. If <code>all</code> is specified, then approvers, participants and scripts are included in the response.</p> (optional) 

            try
            {
                // Get Scenario
                ScenarioBean result = apiInstance.ScenariosGet(id, expand);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Scenario
    ApiResponse<ScenarioBean> response = apiInstance.ScenariosGetWithHttpInfo(id, expand);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosGetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **expand** | **string** | &lt;p&gt;Value can be &lt;code&gt;all&lt;/code&gt; or &lt;code&gt;none&lt;/code&gt;. Default value is &lt;code&gt;none&lt;/code&gt;, meaning only links are returned. If &lt;code&gt;all&lt;/code&gt; is specified, then approvers, participants and scripts are included in the response.&lt;/p&gt; | [optional]  |

### Return type

[**ScenarioBean**](ScenarioBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario details returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get scenario details. The scenario ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosgetapprovers"></a>
# **ScenariosGetApprovers**
> ApproverListResponse ScenariosGetApprovers (long scenarioId)

Get Approvers

<p>Gets scenario approvers.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosGetApproversExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>

            try
            {
                // Get Approvers
                ApproverListResponse result = apiInstance.ScenariosGetApprovers(scenarioId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosGetApprovers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosGetApproversWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Approvers
    ApiResponse<ApproverListResponse> response = apiInstance.ScenariosGetApproversWithHttpInfo(scenarioId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosGetApproversWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |

### Return type

[**ApproverListResponse**](ApproverListResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;List of scenario approvers returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get scenario approvers. The scenario ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosgetchangedata"></a>
# **ScenariosGetChangeData**
> ScenarioChangesListResponse ScenariosGetChangeData (long id)

Get Changes

<p>Gets scenario data changes. If you are the owner, approver, or participant for a given scenario, you can compare scenario and base models.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosGetChangeDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var id = 789L;  // long | <p>Scenario ID.</p>

            try
            {
                // Get Changes
                ScenarioChangesListResponse result = apiInstance.ScenariosGetChangeData(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosGetChangeData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosGetChangeDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Changes
    ApiResponse<ScenarioChangesListResponse> response = apiInstance.ScenariosGetChangeDataWithHttpInfo(id);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosGetChangeDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |

### Return type

[**ScenarioChangesListResponse**](ScenarioChangesListResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario changes returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get scenario changes. The scenario ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosgetcomment"></a>
# **ScenariosGetComment**
> CommentBean ScenariosGetComment (long scenarioId, long commentId)

Get Comment

<p>Gets a scenario comment by ID.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosGetCommentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>
            var commentId = 789L;  // long | <p>Comment ID.</p>

            try
            {
                // Get Comment
                CommentBean result = apiInstance.ScenariosGetComment(scenarioId, commentId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosGetComment: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosGetCommentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Comment
    ApiResponse<CommentBean> response = apiInstance.ScenariosGetCommentWithHttpInfo(scenarioId, commentId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosGetCommentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **commentId** | **long** | &lt;p&gt;Comment ID.&lt;/p&gt; |  |

### Return type

[**CommentBean**](CommentBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario comment returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get scenario comment. The scenario ID or comment ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosgetcomments"></a>
# **ScenariosGetComments**
> CommentListResponse ScenariosGetComments (long scenarioId)

Get Comments

<p>Gets comments for the specified scenario ID.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosGetCommentsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>

            try
            {
                // Get Comments
                CommentListResponse result = apiInstance.ScenariosGetComments(scenarioId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosGetComments: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosGetCommentsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Comments
    ApiResponse<CommentListResponse> response = apiInstance.ScenariosGetCommentsWithHttpInfo(scenarioId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosGetCommentsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |

### Return type

[**CommentListResponse**](CommentListResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario comments returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get scenario comments. The scenario ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosgetparticipants"></a>
# **ScenariosGetParticipants**
> ParticipantListResponse ScenariosGetParticipants (long scenarioId)

Get Participants

<p>Gets scenario participants by scenario ID.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosGetParticipantsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>

            try
            {
                // Get Participants
                ParticipantListResponse result = apiInstance.ScenariosGetParticipants(scenarioId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosGetParticipants: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosGetParticipantsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Participants
    ApiResponse<ParticipantListResponse> response = apiInstance.ScenariosGetParticipantsWithHttpInfo(scenarioId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosGetParticipantsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |

### Return type

[**ParticipantListResponse**](ParticipantListResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario participants returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get scenario participants. The scenario ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosgetregisteredcubes"></a>
# **ScenariosGetRegisteredCubes**
> ScenarioCubesList ScenariosGetRegisteredCubes ()

Get Scenario-Enabled Cubes

<p>Gets information about all the databases registered for scenario management.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosGetRegisteredCubesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);

            try
            {
                // Get Scenario-Enabled Cubes
                ScenarioCubesList result = apiInstance.ScenariosGetRegisteredCubes();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosGetRegisteredCubes: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosGetRegisteredCubesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Scenario-Enabled Cubes
    ApiResponse<ScenarioCubesList> response = apiInstance.ScenariosGetRegisteredCubesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosGetRegisteredCubesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ScenarioCubesList**](ScenarioCubesList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario-enabled cubes returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get scenario-enabled cubes.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosgetscenarios"></a>
# **ScenariosGetScenarios**
> ScenarioCollectionResponse ScenariosGetScenarios (string filter = null, string role = null, bool? overdue = null, string state = null, string application = null, string database = null, bool? approvalPending = null, long? offset = null, long? limit = null, string orderBy = null, bool? count = null)

Get Scenarios

<p>Get scenarios matching search criteria. Response includes links to all basic scenario attributes, excluding approvers, participants, comments, and scripts.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosGetScenariosExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var filter = "filter_example";  // string | <p>Scenario name wildcard pattern.</p> (optional) 
            var role = "role_example";  // string | <p>Optional user role by which to filter the scenario list. Value can be <code>owner</code>, <code>participant</code>, or <code>approver</code>. If role is specified as <code>approver</code>, this API returns all scenarios for which the logged in user is the approver. By default, all scenarios are returned for which the logged in user is either an owner, a participant, or an approver.</p> (optional) 
            var overdue = true;  // bool? | <p>If true, returns all scenarios past the due date. Default is false, meaning all scenarios are returned.</p> (optional) 
            var state = "state_example";  // string | <p>Scenario state.</p> (optional) 
            var application = "application_example";  // string | <p>Application name.</p> (optional) 
            var database = "database_example";  // string | <p>Database name.</p> (optional) 
            var approvalPending = false;  // bool? | <p>Scenario is in submitted state, and approval is pending from logged in user.</p> (optional)  (default to false)
            var offset = 0L;  // long? | <p>Number of scenarios to omit from the start of the result set. Default is 0.</p> (optional)  (default to 0)
            var limit = 50L;  // long? | <p>Maximum number of scenarios to return. Default is 50.</p> (optional)  (default to 50)
            var orderBy = "\"createdTime:desc\"";  // string | <p>Order-by field and order for the result set. The value of this parameter must follow the format of <code><i>fieldName</i>:asc|desc</code>. For example: <code>name:asc</code>. Default value is <code>createdTime:desc</code>, meaning that scenarios are listed with the most recently created scenarios first.</p> (optional)  (default to "createdTime:desc")
            var count = false;  // bool? | <p>If <code>true</code>, response contains only the count of scenarios, and not actual scenarios.</p> (optional)  (default to false)

            try
            {
                // Get Scenarios
                ScenarioCollectionResponse result = apiInstance.ScenariosGetScenarios(filter, role, overdue, state, application, database, approvalPending, offset, limit, orderBy, count);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosGetScenarios: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosGetScenariosWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Scenarios
    ApiResponse<ScenarioCollectionResponse> response = apiInstance.ScenariosGetScenariosWithHttpInfo(filter, role, overdue, state, application, database, approvalPending, offset, limit, orderBy, count);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosGetScenariosWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **filter** | **string** | &lt;p&gt;Scenario name wildcard pattern.&lt;/p&gt; | [optional]  |
| **role** | **string** | &lt;p&gt;Optional user role by which to filter the scenario list. Value can be &lt;code&gt;owner&lt;/code&gt;, &lt;code&gt;participant&lt;/code&gt;, or &lt;code&gt;approver&lt;/code&gt;. If role is specified as &lt;code&gt;approver&lt;/code&gt;, this API returns all scenarios for which the logged in user is the approver. By default, all scenarios are returned for which the logged in user is either an owner, a participant, or an approver.&lt;/p&gt; | [optional]  |
| **overdue** | **bool?** | &lt;p&gt;If true, returns all scenarios past the due date. Default is false, meaning all scenarios are returned.&lt;/p&gt; | [optional]  |
| **state** | **string** | &lt;p&gt;Scenario state.&lt;/p&gt; | [optional]  |
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; | [optional]  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; | [optional]  |
| **approvalPending** | **bool?** | &lt;p&gt;Scenario is in submitted state, and approval is pending from logged in user.&lt;/p&gt; | [optional] [default to false] |
| **offset** | **long?** | &lt;p&gt;Number of scenarios to omit from the start of the result set. Default is 0.&lt;/p&gt; | [optional] [default to 0] |
| **limit** | **long?** | &lt;p&gt;Maximum number of scenarios to return. Default is 50.&lt;/p&gt; | [optional] [default to 50] |
| **orderBy** | **string** | &lt;p&gt;Order-by field and order for the result set. The value of this parameter must follow the format of &lt;code&gt;&lt;i&gt;fieldName&lt;/i&gt;:asc|desc&lt;/code&gt;. For example: &lt;code&gt;name:asc&lt;/code&gt;. Default value is &lt;code&gt;createdTime:desc&lt;/code&gt;, meaning that scenarios are listed with the most recently created scenarios first.&lt;/p&gt; | [optional] [default to &quot;createdTime:desc&quot;] |
| **count** | **bool?** | &lt;p&gt;If &lt;code&gt;true&lt;/code&gt;, response contains only the count of scenarios, and not actual scenarios.&lt;/p&gt; | [optional] [default to false] |

### Return type

[**ScenarioCollectionResponse**](ScenarioCollectionResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario list and details returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get scenarios.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosgetscripts"></a>
# **ScenariosGetScripts**
> ScriptListResponse ScenariosGetScripts (long scenarioId)

Get Scenario Scripts

<p>Gets scenario scripts.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosGetScriptsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>

            try
            {
                // Get Scenario Scripts
                ScriptListResponse result = apiInstance.ScenariosGetScripts(scenarioId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosGetScripts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosGetScriptsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Scenario Scripts
    ApiResponse<ScriptListResponse> response = apiInstance.ScenariosGetScriptsWithHttpInfo(scenarioId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosGetScriptsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |

### Return type

[**ScriptListResponse**](ScriptListResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario scripts returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get scenario scripts. The scenario ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariospatch"></a>
# **ScenariosPatch**
> ScenarioBean ScenariosPatch (long id, ScenarioEditBean body = null)

Update Scenario Partially

<p>Updates basic information about a scenario, including description, due date, priority, and owner.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosPatchExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var id = 789L;  // long | <p>Scenario ID.</p>
            var body = new ScenarioEditBean(); // ScenarioEditBean | <p>Scenario details. Set only those fields which need to be updated.</p> (optional) 

            try
            {
                // Update Scenario Partially
                ScenarioBean result = apiInstance.ScenariosPatch(id, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosPatch: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosPatchWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Scenario Partially
    ApiResponse<ScenarioBean> response = apiInstance.ScenariosPatchWithHttpInfo(id, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosPatchWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **body** | [**ScenarioEditBean**](ScenarioEditBean.md) | &lt;p&gt;Scenario details. Set only those fields which need to be updated.&lt;/p&gt; | [optional]  |

### Return type

[**ScenarioBean**](ScenarioBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the scenario. Required fields such as scenario name or due date may be empty.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosperformaction"></a>
# **ScenariosPerformAction**
> ScenarioBean ScenariosPerformAction (long id, string action = null, bool? overwrite = null, ScenarioActionPayload body = null)

Perform Scenario Action

<p>Performs the specified scenario workflow action. Common actions are <code>submit</code> to submit a scenario for approval, <code>approve</code> to approve a scenario, <code>reject</code> to reject it, and <code>apply</code> to overwrite the base data with the scenario data. Additional actions include <code>copy</code>, <code>refresh</code>, and <code>clear</code>.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosPerformActionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var id = 789L;  // long | <p>Scenario ID.</p>
            var action = "action_example";  // string | <p>Valid values are <code>submit</code>, <code>approve</code>, <code>reject</code>, <code>apply</code>, <code>copy</code>, <code>refresh</code>, and <code>clear</code>.</p> (optional) 
            var overwrite = false;  // bool? | <p>Optional overwrite parameter, applicable only when <i>action</i> is <code>refresh</code>. If <code>true</code>, when the base and scenario data have different values, the base value overwrites the scenario changes. Default is <code>false</code>.</p> (optional)  (default to false)
            var body = new ScenarioActionPayload(); // ScenarioActionPayload | <p>Action parameters. Copy options are applicable only in case of copy. Comment is applicable only for workflow actions.</p> (optional) 

            try
            {
                // Perform Scenario Action
                ScenarioBean result = apiInstance.ScenariosPerformAction(id, action, overwrite, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosPerformAction: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosPerformActionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Perform Scenario Action
    ApiResponse<ScenarioBean> response = apiInstance.ScenariosPerformActionWithHttpInfo(id, action, overwrite, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosPerformActionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **action** | **string** | &lt;p&gt;Valid values are &lt;code&gt;submit&lt;/code&gt;, &lt;code&gt;approve&lt;/code&gt;, &lt;code&gt;reject&lt;/code&gt;, &lt;code&gt;apply&lt;/code&gt;, &lt;code&gt;copy&lt;/code&gt;, &lt;code&gt;refresh&lt;/code&gt;, and &lt;code&gt;clear&lt;/code&gt;.&lt;/p&gt; | [optional]  |
| **overwrite** | **bool?** | &lt;p&gt;Optional overwrite parameter, applicable only when &lt;i&gt;action&lt;/i&gt; is &lt;code&gt;refresh&lt;/code&gt;. If &lt;code&gt;true&lt;/code&gt;, when the base and scenario data have different values, the base value overwrites the scenario changes. Default is &lt;code&gt;false&lt;/code&gt;.&lt;/p&gt; | [optional] [default to false] |
| **body** | [**ScenarioActionPayload**](ScenarioActionPayload.md) | &lt;p&gt;Action parameters. Copy options are applicable only in case of copy. Comment is applicable only for workflow actions.&lt;/p&gt; | [optional]  |

### Return type

[**ScenarioBean**](ScenarioBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario action performed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to perform scenario action. The scenario ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosupdate"></a>
# **ScenariosUpdate**
> ScenarioBean ScenariosUpdate (long id, ScenarioBean body = null)

Update Scenario Fully

<p>Updates full details about a scenario.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosUpdateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var id = 789L;  // long | <p>Scenario ID.</p>
            var body = new ScenarioBean(); // ScenarioBean | <p>Scenario details.</p> (optional) 

            try
            {
                // Update Scenario Fully
                ScenarioBean result = apiInstance.ScenariosUpdate(id, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosUpdate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosUpdateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Scenario Fully
    ApiResponse<ScenarioBean> response = apiInstance.ScenariosUpdateWithHttpInfo(id, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosUpdateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **id** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **body** | [**ScenarioBean**](ScenarioBean.md) | &lt;p&gt;Scenario details.&lt;/p&gt; | [optional]  |

### Return type

[**ScenarioBean**](ScenarioBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the scenario. Required fields such as scenario name or due date may be empty, or the scenario may already have been submitted for approval.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosupdatecomment"></a>
# **ScenariosUpdateComment**
> void ScenariosUpdateComment (long scenarioId, long commentId, CommentBean body = null)

Update Comment

<p>Updates a scenario comment by ID.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosUpdateCommentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>
            var commentId = 789L;  // long | <p>Comment ID.</p>
            var body = new CommentBean(); // CommentBean | <p>Comment details.</p> (optional) 

            try
            {
                // Update Comment
                apiInstance.ScenariosUpdateComment(scenarioId, commentId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosUpdateComment: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosUpdateCommentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Comment
    apiInstance.ScenariosUpdateCommentWithHttpInfo(scenarioId, commentId, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosUpdateCommentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **commentId** | **long** | &lt;p&gt;Comment ID.&lt;/p&gt; |  |
| **body** | [**CommentBean**](CommentBean.md) | &lt;p&gt;Comment details.&lt;/p&gt; | [optional]  |

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
| **204** | &lt;p&gt;Scenario comment successfully updated.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update scenario comment. The scenario ID or comment ID may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="scenariosupdatescript"></a>
# **ScenariosUpdateScript**
> void ScenariosUpdateScript (long scenarioId, string scriptType, ScriptBean body = null)

Update Scenario with Script

<p>Updates scenario using script of specified type.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScenariosUpdateScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScenariosApi(config);
            var scenarioId = 789L;  // long | <p>Scenario ID.</p>
            var scriptType = "scriptType_example";  // string | <p>Script type. Valid values are: <code>refresh</code> to revert to base, <code>apply</code> to commit to base, and <code>clear</code> to set to #Missing.</p>
            var body = new ScriptBean(); // ScriptBean | <p>Script details.</p> (optional) 

            try
            {
                // Update Scenario with Script
                apiInstance.ScenariosUpdateScript(scenarioId, scriptType, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScenariosApi.ScenariosUpdateScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScenariosUpdateScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Scenario with Script
    apiInstance.ScenariosUpdateScriptWithHttpInfo(scenarioId, scriptType, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScenariosApi.ScenariosUpdateScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **scenarioId** | **long** | &lt;p&gt;Scenario ID.&lt;/p&gt; |  |
| **scriptType** | **string** | &lt;p&gt;Script type. Valid values are: &lt;code&gt;refresh&lt;/code&gt; to revert to base, &lt;code&gt;apply&lt;/code&gt; to commit to base, and &lt;code&gt;clear&lt;/code&gt; to set to #Missing.&lt;/p&gt; |  |
| **body** | [**ScriptBean**](ScriptBean.md) | &lt;p&gt;Script details.&lt;/p&gt; | [optional]  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scenario updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update scenario with script. The scenario ID or script type may be invalid.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

