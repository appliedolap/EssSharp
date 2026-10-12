# EssSharp.Api.ROClusterApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ROClusterCreateROCluster**](ROClusterApi.md#roclustercreaterocluster) | **POST** /rocluster | Create ReadOnly Cluster |
| [**ROClusterDeleteROCluster**](ROClusterApi.md#roclusterdeleterocluster) | **DELETE** /rocluster | Delete ReadOnly Cluster |
| [**ROClusterEnableCluster**](ROClusterApi.md#roclusterenablecluster) | **POST** /rocluster/changestatus | Change Read-only Cluster Status |
| [**ROClusterGetAppAndCubeListByServerName**](ROClusterApi.md#roclustergetappandcubelistbyservername) | **GET** /rocluster/{svrName}/appcubelist | Get App Cube List |
| [**ROClusterGetClusterList**](ROClusterApi.md#roclustergetclusterlist) | **GET** /rocluster | Get Read-only Cluster List |
| [**ROClusterGetROCluster**](ROClusterApi.md#roclustergetrocluster) | **GET** /rocluster/findByName | Find ReadOnly Cluster by Name |
| [**ROClusterUpdateROCluster**](ROClusterApi.md#roclusterupdaterocluster) | **PUT** /rocluster | Update ReadOnly Cluster |

<a id="roclustercreaterocluster"></a>
# **ROClusterCreateROCluster**
> void ROClusterCreateROCluster (ROCluster body)

Create ReadOnly Cluster

<p>Adds new ReadOnly cluster with all selected nodes and description to the domain db of Essbase server.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ROClusterCreateROClusterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ROClusterApi(config);
            var body = new ROCluster(); // ROCluster | RoCluster

            try
            {
                // Create ReadOnly Cluster
                apiInstance.ROClusterCreateROCluster(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ROClusterApi.ROClusterCreateROCluster: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ROClusterCreateROClusterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create ReadOnly Cluster
    apiInstance.ROClusterCreateROClusterWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ROClusterApi.ROClusterCreateROClusterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**ROCluster**](ROCluster.md) | RoCluster |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Successfully created the ReadOnly cluster.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed the create ReadOnly cluster.JSON may be incorrect or name or node info is missing&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="roclusterdeleterocluster"></a>
# **ROClusterDeleteROCluster**
> void ROClusterDeleteROCluster (string clusterName = null)

Delete ReadOnly Cluster

<p>Deletes the existing ReadOnly cluster and all nodes under it from the domain db of Essbase server. Provide the URL encoded String value as input(clusterName)

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ROClusterDeleteROClusterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ROClusterApi(config);
            var clusterName = "clusterName_example";  // string |  (optional) 

            try
            {
                // Delete ReadOnly Cluster
                apiInstance.ROClusterDeleteROCluster(clusterName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ROClusterApi.ROClusterDeleteROCluster: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ROClusterDeleteROClusterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete ReadOnly Cluster
    apiInstance.ROClusterDeleteROClusterWithHttpInfo(clusterName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ROClusterApi.ROClusterDeleteROClusterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clusterName** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Successfully removed the ReadOnly cluster from the domain DB of the Essbase server.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete the ReadOnly cluster.Name specified may be incorrect or not exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="roclusterenablecluster"></a>
# **ROClusterEnableCluster**
> ROCluster ROClusterEnableCluster (ROCluster body)

Change Read-only Cluster Status

<p>Change Status allows to modify the state 1(enable)/2(disable) of read-only cluster nodes.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ROClusterEnableClusterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ROClusterApi(config);
            var body = new ROCluster(); // ROCluster | RoCluster

            try
            {
                // Change Read-only Cluster Status
                ROCluster result = apiInstance.ROClusterEnableCluster(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ROClusterApi.ROClusterEnableCluster: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ROClusterEnableClusterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Change Read-only Cluster Status
    ApiResponse<ROCluster> response = apiInstance.ROClusterEnableClusterWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ROClusterApi.ROClusterEnableClusterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**ROCluster**](ROCluster.md) | RoCluster |  |

### Return type

[**ROCluster**](ROCluster.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Successfully changed the states of ReadOnly cluster nodes. Returns updated ReadOnly cluster.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to Change the status of ReadOnly Cluster nodes. The JSON may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="roclustergetappandcubelistbyservername"></a>
# **ROClusterGetAppAndCubeListByServerName**
> string ROClusterGetAppAndCubeListByServerName (string svrName)

Get App Cube List

Returns the list of applications and databases under it of selected Essbase Server

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ROClusterGetAppAndCubeListByServerNameExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ROClusterApi(config);
            var svrName = "svrName_example";  // string | 

            try
            {
                // Get App Cube List
                string result = apiInstance.ROClusterGetAppAndCubeListByServerName(svrName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ROClusterApi.ROClusterGetAppAndCubeListByServerName: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ROClusterGetAppAndCubeListByServerNameWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get App Cube List
    ApiResponse<string> response = apiInstance.ROClusterGetAppAndCubeListByServerNameWithHttpInfo(svrName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ROClusterApi.ROClusterGetAppAndCubeListByServerNameWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **svrName** | **string** |  |  |

### Return type

**string**

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Successfully retrieved the apps and cubes list.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to retrieve the apps and cubes list. Incorrect server name.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="roclustergetclusterlist"></a>
# **ROClusterGetClusterList**
> List&lt;ROCluster&gt; ROClusterGetClusterList ()

Get Read-only Cluster List

Returns list of read-only cluster from Essbase domain db.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ROClusterGetClusterListExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ROClusterApi(config);

            try
            {
                // Get Read-only Cluster List
                List<ROCluster> result = apiInstance.ROClusterGetClusterList();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ROClusterApi.ROClusterGetClusterList: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ROClusterGetClusterListWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Read-only Cluster List
    ApiResponse<List<ROCluster>> response = apiInstance.ROClusterGetClusterListWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ROClusterApi.ROClusterGetClusterListWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**List&lt;ROCluster&gt;**](ROCluster.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;RO-Cluster list retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to retrieve the ReadOnly cluster list.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="roclustergetrocluster"></a>
# **ROClusterGetROCluster**
> ROCluster ROClusterGetROCluster (string clusterName = null)

Find ReadOnly Cluster by Name

<p>Returns the named ReadOnly cluster from Essbase domain db. Provide the URL encoded String value as input(clusterName)

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ROClusterGetROClusterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ROClusterApi(config);
            var clusterName = "clusterName_example";  // string |  (optional) 

            try
            {
                // Find ReadOnly Cluster by Name
                ROCluster result = apiInstance.ROClusterGetROCluster(clusterName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ROClusterApi.ROClusterGetROCluster: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ROClusterGetROClusterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Find ReadOnly Cluster by Name
    ApiResponse<ROCluster> response = apiInstance.ROClusterGetROClusterWithHttpInfo(clusterName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ROClusterApi.ROClusterGetROClusterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **clusterName** | **string** |  | [optional]  |

### Return type

[**ROCluster**](ROCluster.md)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;ReadOnly cluster retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to retrieve the ReadOnly cluster. Incorrect or missing cluster name.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="roclusterupdaterocluster"></a>
# **ROClusterUpdateROCluster**
> void ROClusterUpdateROCluster (ROCluster body)

Update ReadOnly Cluster

<p>Removes the existing ReadOnly cluster and create new ReadOnly cluster with latest nodes details.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ROClusterUpdateROClusterExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure OAuth2 access token for authorization: OAuth2
            config.AccessToken = "YOUR_ACCESS_TOKEN";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ROClusterApi(config);
            var body = new ROCluster(); // ROCluster | ROCluster

            try
            {
                // Update ReadOnly Cluster
                apiInstance.ROClusterUpdateROCluster(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ROClusterApi.ROClusterUpdateROCluster: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ROClusterUpdateROClusterWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update ReadOnly Cluster
    apiInstance.ROClusterUpdateROClusterWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ROClusterApi.ROClusterUpdateROClusterWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**ROCluster**](ROCluster.md) | ROCluster |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Successfully created the ReadOnly cluster.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed the delete/create ReadOnly cluster.JSON may be incorrect or name or node info is missing&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

