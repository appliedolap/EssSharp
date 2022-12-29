# EssSharp.Api.ApplicationConnectionsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ApplicationConnectionsCreateConnection**](ApplicationConnectionsApi.md#applicationconnectionscreateconnection) | **POST** /applications/{applicationName}/connections | Create Application Connection |
| [**ApplicationConnectionsDeleteConnection**](ApplicationConnectionsApi.md#applicationconnectionsdeleteconnection) | **DELETE** /applications/{applicationName}/connections/{connectionName} | Delete Application Connection |
| [**ApplicationConnectionsGetConnectionDetails**](ApplicationConnectionsApi.md#applicationconnectionsgetconnectiondetails) | **GET** /applications/{applicationName}/connections/{connectionName} | Get Application Connection |
| [**ApplicationConnectionsGetConnections**](ApplicationConnectionsApi.md#applicationconnectionsgetconnections) | **GET** /applications/{applicationName}/connections | List Application Connections |
| [**ApplicationConnectionsTestConnection**](ApplicationConnectionsApi.md#applicationconnectionstestconnection) | **POST** /applications/{applicationName}/connections/actions/test | Test New Application Connection |
| [**ApplicationConnectionsTestConnectionExisting**](ApplicationConnectionsApi.md#applicationconnectionstestconnectionexisting) | **POST** /applications/{applicationName}/connections/{connectionName}/actions/test | Test Saved Application Connection |
| [**ApplicationConnectionsUpdateConnection**](ApplicationConnectionsApi.md#applicationconnectionsupdateconnection) | **PUT** /applications/{applicationName}/connections/{connectionName} | Update Application Connection |
| [**ApplicationConnectionsWallets**](ApplicationConnectionsApi.md#applicationconnectionswallets) | **PUT** /applications/{applicationName}/connections/{connectionName}/wallet | Upload a connection wallet file |

<a name="applicationconnectionscreateconnection"></a>
# **ApplicationConnectionsCreateConnection**
> void ApplicationConnectionsCreateConnection (string applicationName, Connection body)

Create Application Connection

<p>Creates an application-level connection based on specified inputs. <code>name</code> and <code>type</code> are required inputs for all types of connections. Other required inputs differ based on the type of the connection.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConnectionsCreateConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConnectionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var body = new Connection(); // Connection | <p>Connection details.</p>

            try
            {
                // Create Application Connection
                apiInstance.ApplicationConnectionsCreateConnection(applicationName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsCreateConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConnectionsCreateConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Application Connection
    apiInstance.ApplicationConnectionsCreateConnectionWithHttpInfo(applicationName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsCreateConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **body** | [**Connection**](Connection.md) | &lt;p&gt;Connection details.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Connection created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create connection.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconnectionsdeleteconnection"></a>
# **ApplicationConnectionsDeleteConnection**
> void ApplicationConnectionsDeleteConnection (string applicationName, string connectionName)

Delete Application Connection

<p>Delete a saved application connection by name.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConnectionsDeleteConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConnectionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p>

            try
            {
                // Delete Application Connection
                apiInstance.ApplicationConnectionsDeleteConnection(applicationName, connectionName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsDeleteConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConnectionsDeleteConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Application Connection
    apiInstance.ApplicationConnectionsDeleteConnectionWithHttpInfo(applicationName, connectionName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsDeleteConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **connectionName** | **string** | &lt;p&gt;Connection name.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The connection was deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete connection.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconnectionsgetconnectiondetails"></a>
# **ApplicationConnectionsGetConnectionDetails**
> Connection ApplicationConnectionsGetConnectionDetails (string applicationName, string connectionName, bool? password = null)

Get Application Connection

<p>Returns details about the specified application-level connection.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConnectionsGetConnectionDetailsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConnectionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p>
            var password = true;  // bool? | <p>If set to true, the encrypted password is returned in the result.</p> (optional) 

            try
            {
                // Get Application Connection
                Connection result = apiInstance.ApplicationConnectionsGetConnectionDetails(applicationName, connectionName, password);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsGetConnectionDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConnectionsGetConnectionDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Application Connection
    ApiResponse<Connection> response = apiInstance.ApplicationConnectionsGetConnectionDetailsWithHttpInfo(applicationName, connectionName, password);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsGetConnectionDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **connectionName** | **string** | &lt;p&gt;Connection name.&lt;/p&gt; |  |
| **password** | **bool?** | &lt;p&gt;If set to true, the encrypted password is returned in the result.&lt;/p&gt; | [optional]  |

### Return type

[**Connection**](Connection.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Connection details returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get connection details.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconnectionsgetconnections"></a>
# **ApplicationConnectionsGetConnections**
> ConnectionsList ApplicationConnectionsGetConnections (string applicationName, int? offset = null, int? limit = null, string connType = null, bool? repoConn = null, bool? walletConn = null)

List Application Connections

<p>Returns a list of connections for the application, including details such as name, description, and type.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConnectionsGetConnectionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConnectionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var offset = 0;  // int? | <p>Number of connections to omit from the start of the result set.</p> (optional)  (default to 0)
            var limit = 50;  // int? | <p>Maximum number of connections to return. Default is 50.</p> (optional)  (default to 50)
            var connType = "connType_example";  // string | <p>Type of connections to return, if provided</p> (optional) 
            var repoConn = false;  // bool? | <p>Used in conjunction with connType param. If set to true, returns repository-based Autonomous Data Warehouse connections. Default is false</p> (optional)  (default to false)
            var walletConn = false;  // bool? | <p>Used in conjunction with connType param. If set to true, returns wallet-based Autonomous Data Warehouse connections. Default is false</p> (optional)  (default to false)

            try
            {
                // List Application Connections
                ConnectionsList result = apiInstance.ApplicationConnectionsGetConnections(applicationName, offset, limit, connType, repoConn, walletConn);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsGetConnections: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConnectionsGetConnectionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Application Connections
    ApiResponse<ConnectionsList> response = apiInstance.ApplicationConnectionsGetConnectionsWithHttpInfo(applicationName, offset, limit, connType, repoConn, walletConn);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsGetConnectionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **offset** | **int?** | &lt;p&gt;Number of connections to omit from the start of the result set.&lt;/p&gt; | [optional] [default to 0] |
| **limit** | **int?** | &lt;p&gt;Maximum number of connections to return. Default is 50.&lt;/p&gt; | [optional] [default to 50] |
| **connType** | **string** | &lt;p&gt;Type of connections to return, if provided&lt;/p&gt; | [optional]  |
| **repoConn** | **bool?** | &lt;p&gt;Used in conjunction with connType param. If set to true, returns repository-based Autonomous Data Warehouse connections. Default is false&lt;/p&gt; | [optional] [default to false] |
| **walletConn** | **bool?** | &lt;p&gt;Used in conjunction with connType param. If set to true, returns wallet-based Autonomous Data Warehouse connections. Default is false&lt;/p&gt; | [optional] [default to false] |

### Return type

[**ConnectionsList**](ConnectionsList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;List of connections returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to list connections.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconnectionstestconnection"></a>
# **ApplicationConnectionsTestConnection**
> void ApplicationConnectionsTestConnection (string applicationName, Connection body)

Test New Application Connection

<p>Tests a new or updated application connection, using specified inputs, without saving it.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConnectionsTestConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConnectionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var body = new Connection(); // Connection | <p>Connection details.</p>

            try
            {
                // Test New Application Connection
                apiInstance.ApplicationConnectionsTestConnection(applicationName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsTestConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConnectionsTestConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Test New Application Connection
    apiInstance.ApplicationConnectionsTestConnectionWithHttpInfo(applicationName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsTestConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **body** | [**Connection**](Connection.md) | &lt;p&gt;Connection details.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The connection tested successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Error occurred while testing connection.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconnectionstestconnectionexisting"></a>
# **ApplicationConnectionsTestConnectionExisting**
> void ApplicationConnectionsTestConnectionExisting (string applicationName, string connectionName)

Test Saved Application Connection

<p>Tests the saved application-level connection with the specified name.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConnectionsTestConnectionExistingExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConnectionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var connectionName = "connectionName_example";  // string | <p>Saved connection name.</p>

            try
            {
                // Test Saved Application Connection
                apiInstance.ApplicationConnectionsTestConnectionExisting(applicationName, connectionName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsTestConnectionExisting: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConnectionsTestConnectionExistingWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Test Saved Application Connection
    apiInstance.ApplicationConnectionsTestConnectionExistingWithHttpInfo(applicationName, connectionName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsTestConnectionExistingWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **connectionName** | **string** | &lt;p&gt;Saved connection name.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The connection tested successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Error occurred while testing connection.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconnectionsupdateconnection"></a>
# **ApplicationConnectionsUpdateConnection**
> Connection ApplicationConnectionsUpdateConnection (string applicationName, string connectionName, Connection body)

Update Application Connection

<p>Update the named application connection. If successful, returns details of the updated connection. <code>type</code> is a required input for all connections. Other required inputs differ, depending on the type of the connection.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConnectionsUpdateConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConnectionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p>
            var body = new Connection(); // Connection | <p>Connection details.</p>

            try
            {
                // Update Application Connection
                Connection result = apiInstance.ApplicationConnectionsUpdateConnection(applicationName, connectionName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsUpdateConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConnectionsUpdateConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Application Connection
    ApiResponse<Connection> response = apiInstance.ApplicationConnectionsUpdateConnectionWithHttpInfo(applicationName, connectionName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsUpdateConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **connectionName** | **string** | &lt;p&gt;Connection name.&lt;/p&gt; |  |
| **body** | [**Connection**](Connection.md) | &lt;p&gt;Connection details.&lt;/p&gt; |  |

### Return type

[**Connection**](Connection.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Connection updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update connection.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="applicationconnectionswallets"></a>
# **ApplicationConnectionsWallets**
> WalletLocation ApplicationConnectionsWallets (string applicationName, string connectionName)

Upload a connection wallet file

Upload a connection wallet file.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ApplicationConnectionsWalletsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ApplicationConnectionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p>

            try
            {
                // Upload a connection wallet file
                WalletLocation result = apiInstance.ApplicationConnectionsWallets(applicationName, connectionName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsWallets: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ApplicationConnectionsWalletsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Upload a connection wallet file
    ApiResponse<WalletLocation> response = apiInstance.ApplicationConnectionsWalletsWithHttpInfo(applicationName, connectionName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ApplicationConnectionsApi.ApplicationConnectionsWalletsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **connectionName** | **string** | &lt;p&gt;Connection name.&lt;/p&gt; |  |

### Return type

[**WalletLocation**](WalletLocation.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Return the wallet file location in catalog.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Unable to process wallet file.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

