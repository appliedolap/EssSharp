# EssSharp.Api.SessionsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**SessionsDeleteAllActiveSessions**](SessionsApi.md#sessionsdeleteallactivesessions) | **DELETE** /sessions | Delete All Sessions |
| [**SessionsDeleteSessionWithId**](SessionsApi.md#sessionsdeletesessionwithid) | **DELETE** /sessions/{sessionId} | Delete Session By ID |
| [**SessionsGetAllActiveSessions**](SessionsApi.md#sessionsgetallactivesessions) | **GET** /sessions | List Sessions |

<a id="sessionsdeleteallactivesessions"></a>
# **SessionsDeleteAllActiveSessions**
> void SessionsDeleteAllActiveSessions (string application = null, string database = null, string userId = null, bool? disconnect = null)

Delete All Sessions

<p>Deletes all the sessions currently active, or kills all the requests currently processing.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class SessionsDeleteAllActiveSessionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new SessionsApi(config);
            var application = "application_example";  // string | <p>Application name.</p> (optional) 
            var database = "database_example";  // string | <p>Database name.</p> (optional) 
            var userId = "userId_example";  // string | <p>User ID.</p> (optional) 
            var disconnect = false;  // bool? | <p>Disconnect value (Boolean). If false, the request is killed. Otherwise, the session is logged off.</p> (optional)  (default to false)

            try
            {
                // Delete All Sessions
                apiInstance.SessionsDeleteAllActiveSessions(application, database, userId, disconnect);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SessionsApi.SessionsDeleteAllActiveSessions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SessionsDeleteAllActiveSessionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete All Sessions
    apiInstance.SessionsDeleteAllActiveSessionsWithHttpInfo(application, database, userId, disconnect);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SessionsApi.SessionsDeleteAllActiveSessionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; | [optional]  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; | [optional]  |
| **userId** | **string** | &lt;p&gt;User ID.&lt;/p&gt; | [optional]  |
| **disconnect** | **bool?** | &lt;p&gt;Disconnect value (Boolean). If false, the request is killed. Otherwise, the session is logged off.&lt;/p&gt; | [optional] [default to false] |

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
| **200** | &lt;p&gt;1. Deletes all the sessions for the parameter &#39;application&#39;, &#39;database&#39; and &#39;userid&#39; provided. If no parameters are specified, deletes all active sessions.&lt;/p&gt;&lt;p&gt;2. Cannot disconnect user. Essbase Error(1051041): Insufficient privilege for this operation.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Essbase or platform security exception.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="sessionsdeletesessionwithid"></a>
# **SessionsDeleteSessionWithId**
> void SessionsDeleteSessionWithId (long sessionId, bool? disconnect = null)

Delete Session By ID

<p>Deletes a particular session or kills a particular request using the session id.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class SessionsDeleteSessionWithIdExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new SessionsApi(config);
            var sessionId = 789L;  // long | <p>sessionId of the session to be disconnected or request killed.</p>
            var disconnect = false;  // bool? | <p>Disconnection value. Default is false, meaning kill the request. If true, disconnect the user session.</p> (optional)  (default to false)

            try
            {
                // Delete Session By ID
                apiInstance.SessionsDeleteSessionWithId(sessionId, disconnect);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SessionsApi.SessionsDeleteSessionWithId: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SessionsDeleteSessionWithIdWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Session By ID
    apiInstance.SessionsDeleteSessionWithIdWithHttpInfo(sessionId, disconnect);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SessionsApi.SessionsDeleteSessionWithIdWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **long** | &lt;p&gt;sessionId of the session to be disconnected or request killed.&lt;/p&gt; |  |
| **disconnect** | **bool?** | &lt;p&gt;Disconnection value. Default is false, meaning kill the request. If true, disconnect the user session.&lt;/p&gt; | [optional] [default to false] |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Session or request terminated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;ol&gt;&lt;li&gt;Essbase or platform security exception.&lt;/li&gt;&lt;li&gt;If the sessionId is incorrect, &lt;code&gt;Error: No session with specified login id.&lt;/code&gt;&lt;/li&gt;&lt;li&gt;Cannot disconnect user. Essbase Error(1051041): Insufficient privilege for this operation&lt;/li&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="sessionsgetallactivesessions"></a>
# **SessionsGetAllActiveSessions**
> List&lt;SessionAttributes&gt; SessionsGetAllActiveSessions (string application = null, string database = null, string userId = null)

List Sessions

<p>Returns a list of session currently active for a user or request.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class SessionsGetAllActiveSessionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new SessionsApi(config);
            var application = "application_example";  // string | <p>Application name.</p> (optional) 
            var database = "database_example";  // string | <p>Database name.</p> (optional) 
            var userId = "userId_example";  // string | <p>User ID for whom to return the active sessions. If not provided, all the sesions are retrieved.</p> (optional) 

            try
            {
                // List Sessions
                List<SessionAttributes> result = apiInstance.SessionsGetAllActiveSessions(application, database, userId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling SessionsApi.SessionsGetAllActiveSessions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the SessionsGetAllActiveSessionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Sessions
    ApiResponse<List<SessionAttributes>> response = apiInstance.SessionsGetAllActiveSessionsWithHttpInfo(application, database, userId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling SessionsApi.SessionsGetAllActiveSessionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; | [optional]  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; | [optional]  |
| **userId** | **string** | &lt;p&gt;User ID for whom to return the active sessions. If not provided, all the sesions are retrieved.&lt;/p&gt; | [optional]  |

### Return type

[**List&lt;SessionAttributes&gt;**](SessionAttributes.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Session details returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Essbase or platform security exception.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

