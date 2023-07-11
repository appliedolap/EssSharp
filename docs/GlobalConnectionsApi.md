# EssSharp.Api.GlobalConnectionsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**GlobalConnectionsCreateConnection**](GlobalConnectionsApi.md#globalconnectionscreateconnection) | **POST** /connections | Create Connection |
| [**GlobalConnectionsDeleteConnection**](GlobalConnectionsApi.md#globalconnectionsdeleteconnection) | **DELETE** /connections/{connectionName} | Delete Connection |
| [**GlobalConnectionsGetConnectionDetails**](GlobalConnectionsApi.md#globalconnectionsgetconnectiondetails) | **GET** /connections/{connectionName} | Get Connection |
| [**GlobalConnectionsGetConnections**](GlobalConnectionsApi.md#globalconnectionsgetconnections) | **GET** /connections | List Connections |
| [**GlobalConnectionsTestConnection**](GlobalConnectionsApi.md#globalconnectionstestconnection) | **POST** /connections/actions/test | Test New Connection |
| [**GlobalConnectionsTestConnectionExisting**](GlobalConnectionsApi.md#globalconnectionstestconnectionexisting) | **POST** /connections/{connectionName}/actions/test | Test Saved Connection |
| [**GlobalConnectionsUpdateConnection**](GlobalConnectionsApi.md#globalconnectionsupdateconnection) | **PUT** /connections/{connectionName} | Update Connection |
| [**GlobalConnectionsWallets**](GlobalConnectionsApi.md#globalconnectionswallets) | **PUT** /connections/{connectionName}/wallet | Upload Connection Wallet File |

<a id="globalconnectionscreateconnection"></a>
# **GlobalConnectionsCreateConnection**
> void GlobalConnectionsCreateConnection (Connection body)

Create Connection

<p>Creates a connection based on specified inputs. <code>name</code> and <code>type</code> are required inputs for all types of connections. Other required inputs differ based on the type of connection.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalConnectionsCreateConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalConnectionsApi(config);
            var body = new Connection(); // Connection | <p>Connection details.</p>

            try
            {
                // Create Connection
                apiInstance.GlobalConnectionsCreateConnection(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsCreateConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalConnectionsCreateConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Connection
    apiInstance.GlobalConnectionsCreateConnectionWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsCreateConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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

<a id="globalconnectionsdeleteconnection"></a>
# **GlobalConnectionsDeleteConnection**
> void GlobalConnectionsDeleteConnection (string connectionName)

Delete Connection

<p>Deletes a named global connection.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalConnectionsDeleteConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalConnectionsApi(config);
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p>

            try
            {
                // Delete Connection
                apiInstance.GlobalConnectionsDeleteConnection(connectionName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsDeleteConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalConnectionsDeleteConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Connection
    apiInstance.GlobalConnectionsDeleteConnectionWithHttpInfo(connectionName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsDeleteConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete the connection.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="globalconnectionsgetconnectiondetails"></a>
# **GlobalConnectionsGetConnectionDetails**
> Connection GlobalConnectionsGetConnectionDetails (string connectionName, bool? password = null)

Get Connection

<p>Returns details about the specified global connection.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalConnectionsGetConnectionDetailsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalConnectionsApi(config);
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p>
            var password = true;  // bool? | <p>If set to true, the encrypted password is returned in the result.</p> (optional) 

            try
            {
                // Get Connection
                Connection result = apiInstance.GlobalConnectionsGetConnectionDetails(connectionName, password);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsGetConnectionDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalConnectionsGetConnectionDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Connection
    ApiResponse<Connection> response = apiInstance.GlobalConnectionsGetConnectionDetailsWithHttpInfo(connectionName, password);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsGetConnectionDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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

<a id="globalconnectionsgetconnections"></a>
# **GlobalConnectionsGetConnections**
> ConnectionsList GlobalConnectionsGetConnections (int? offset = null, int? limit = null, string connType = null, bool? repoConn = null, bool? walletConn = null)

List Connections

<p>Returns global connections list, including details such as name, description, and type.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalConnectionsGetConnectionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalConnectionsApi(config);
            var offset = 0;  // int? | <p>Number of connections to omit from the start of the result set.</p> (optional)  (default to 0)
            var limit = 50;  // int? | <p>Maximum number of connections to return. Default is 50.</p> (optional)  (default to 50)
            var connType = "connType_example";  // string | <p>Type of connections to return, if provided</p> (optional) 
            var repoConn = false;  // bool? | <p>Used in conjunction with connType param. If set to true, returns repository-based Autonomous Data Warehouse connections. Default is false</p> (optional)  (default to false)
            var walletConn = false;  // bool? | <p>Used in conjunction with connType param. If set to true, returns wallet-based Autonomous Data Warehouse connections. Default is false</p> (optional)  (default to false)

            try
            {
                // List Connections
                ConnectionsList result = apiInstance.GlobalConnectionsGetConnections(offset, limit, connType, repoConn, walletConn);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsGetConnections: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalConnectionsGetConnectionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Connections
    ApiResponse<ConnectionsList> response = apiInstance.GlobalConnectionsGetConnectionsWithHttpInfo(offset, limit, connType, repoConn, walletConn);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsGetConnectionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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

<a id="globalconnectionstestconnection"></a>
# **GlobalConnectionsTestConnection**
> void GlobalConnectionsTestConnection (Connection body)

Test New Connection

<p>Tests a new or updated global connection, using specified inputs, without saving it.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalConnectionsTestConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalConnectionsApi(config);
            var body = new Connection(); // Connection | <p>Connection details.</p>

            try
            {
                // Test New Connection
                apiInstance.GlobalConnectionsTestConnection(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsTestConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalConnectionsTestConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Test New Connection
    apiInstance.GlobalConnectionsTestConnectionWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsTestConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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

<a id="globalconnectionstestconnectionexisting"></a>
# **GlobalConnectionsTestConnectionExisting**
> void GlobalConnectionsTestConnectionExisting (string connectionName)

Test Saved Connection

<p>Tests a saved global connection by name.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalConnectionsTestConnectionExistingExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalConnectionsApi(config);
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p>

            try
            {
                // Test Saved Connection
                apiInstance.GlobalConnectionsTestConnectionExisting(connectionName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsTestConnectionExisting: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalConnectionsTestConnectionExistingWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Test Saved Connection
    apiInstance.GlobalConnectionsTestConnectionExistingWithHttpInfo(connectionName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsTestConnectionExistingWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The connection tested successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Error occurred while testing connection.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="globalconnectionsupdateconnection"></a>
# **GlobalConnectionsUpdateConnection**
> Connection GlobalConnectionsUpdateConnection (string connectionName, Connection body)

Update Connection

<p>Update the named global connection. If the update is successful, returns details about the updated connection. <code>type</code> is a required input for all types of connections. Other required inputs differ based on the type of the connection.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalConnectionsUpdateConnectionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalConnectionsApi(config);
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p>
            var body = new Connection(); // Connection | <p>Connection details.</p>

            try
            {
                // Update Connection
                Connection result = apiInstance.GlobalConnectionsUpdateConnection(connectionName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsUpdateConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalConnectionsUpdateConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Connection
    ApiResponse<Connection> response = apiInstance.GlobalConnectionsUpdateConnectionWithHttpInfo(connectionName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsUpdateConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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

<a id="globalconnectionswallets"></a>
# **GlobalConnectionsWallets**
> WalletLocation GlobalConnectionsWallets (string connectionName)

Upload Connection Wallet File

<p>Upload a connection wallet file for a global connection. Oracle client credentials (wallet files) are downloaded from Autonomous Data Warehouse by a service administrator. If you are not an Autonomous Data Warehouse administrator, your administrator should provide you with the client credentials.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GlobalConnectionsWalletsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new GlobalConnectionsApi(config);
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p>

            try
            {
                // Upload Connection Wallet File
                WalletLocation result = apiInstance.GlobalConnectionsWallets(connectionName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsWallets: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GlobalConnectionsWalletsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Upload Connection Wallet File
    ApiResponse<WalletLocation> response = apiInstance.GlobalConnectionsWalletsWithHttpInfo(connectionName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling GlobalConnectionsApi.GlobalConnectionsWalletsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
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

