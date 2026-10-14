# EssSharp.Api.FederatedOverDatasourceApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**FederatedOverDatasourceAvParams**](FederatedOverDatasourceApi.md#federatedoverdatasourceavparams) | **GET** /federatedoverdatasource/av/avparams |  |
| [**FederatedOverDatasourceAvStatus**](FederatedOverDatasourceApi.md#federatedoverdatasourceavstatus) | **GET** /federatedoverdatasource/av/status |  |
| [**FederatedOverDatasourceCacheFormulas**](FederatedOverDatasourceApi.md#federatedoverdatasourcecacheformulas) | **POST** /federatedoverdatasource/saveformulas/cache |  |
| [**FederatedOverDatasourceCancelRequest**](FederatedOverDatasourceApi.md#federatedoverdatasourcecancelrequest) | **GET** /federatedoverdatasource/cancel |  |
| [**FederatedOverDatasourceClear**](FederatedOverDatasourceApi.md#federatedoverdatasourceclear) | **POST** /federatedoverdatasource/clearregions |  |
| [**FederatedOverDatasourceCreateAv**](FederatedOverDatasourceApi.md#federatedoverdatasourcecreateav) | **POST** /federatedoverdatasource/av/createAv |  |
| [**FederatedOverDatasourceCreateMappingCR**](FederatedOverDatasourceApi.md#federatedoverdatasourcecreatemappingcr) | **PUT** /federatedoverdatasource/mapping/cr |  |
| [**FederatedOverDatasourceCreateMappingDenormalized**](FederatedOverDatasourceApi.md#federatedoverdatasourcecreatemappingdenormalized) | **PUT** /federatedoverdatasource/mapping/denormalized |  |
| [**FederatedOverDatasourceCreateMappingDenormalizedLegacyTest**](FederatedOverDatasourceApi.md#federatedoverdatasourcecreatemappingdenormalizedlegacytest) | **PUT** /federatedoverdatasource/mapping/denormalized/legacytest |  |
| [**FederatedOverDatasourceCreateMappingLegacy**](FederatedOverDatasourceApi.md#federatedoverdatasourcecreatemappinglegacy) | **PUT** /federatedoverdatasource/mapping |  |
| [**FederatedOverDatasourceDeleteMapping**](FederatedOverDatasourceApi.md#federatedoverdatasourcedeletemapping) | **DELETE** /federatedoverdatasource/mapping |  |
| [**FederatedOverDatasourceFactManagedSettings**](FederatedOverDatasourceApi.md#federatedoverdatasourcefactmanagedsettings) | **GET** /federatedoverdatasource/factmanaged/settings |  |
| [**FederatedOverDatasourceGetFedVersion**](FederatedOverDatasourceApi.md#federatedoverdatasourcegetfedversion) | **GET** /federatedoverdatasource/lastcommit |  |
| [**FederatedOverDatasourceGetMapping**](FederatedOverDatasourceApi.md#federatedoverdatasourcegetmapping) | **GET** /federatedoverdatasource/mapping |  |
| [**FederatedOverDatasourceGetMappingCR**](FederatedOverDatasourceApi.md#federatedoverdatasourcegetmappingcr) | **GET** /federatedoverdatasource/mapping/cr |  |
| [**FederatedOverDatasourceGetMappingDenormalized**](FederatedOverDatasourceApi.md#federatedoverdatasourcegetmappingdenormalized) | **GET** /federatedoverdatasource/mapping/denormalized |  |
| [**FederatedOverDatasourceReport**](FederatedOverDatasourceApi.md#federatedoverdatasourcereport) | **POST** /federatedoverdatasource |  |
| [**FederatedOverDatasourceSaveFormulas**](FederatedOverDatasourceApi.md#federatedoverdatasourcesaveformulas) | **POST** /federatedoverdatasource/saveformulas |  |
| [**FederatedOverDatasourceValidate**](FederatedOverDatasourceApi.md#federatedoverdatasourcevalidate) | **GET** /federatedoverdatasource/av/validate |  |
| [**FederatedOverDatasourceWriteback**](FederatedOverDatasourceApi.md#federatedoverdatasourcewriteback) | **POST** /federatedoverdatasource/writeback |  |

<a id="federatedoverdatasourceavparams"></a>
# **FederatedOverDatasourceAvParams**
> void FederatedOverDatasourceAvParams (string connectionName = null, string applicationName = null, string dbName = null, string reqId = null, string debug = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceAvParamsExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var debug = "debug_example";  // string |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceAvParams(connectionName, applicationName, dbName, reqId, debug);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceAvParams: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceAvParamsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceAvParamsWithHttpInfo(connectionName, applicationName, dbName, reqId, debug);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceAvParamsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **reqId** | **string** |  | [optional]  |
| **debug** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourceavstatus"></a>
# **FederatedOverDatasourceAvStatus**
> void FederatedOverDatasourceAvStatus (string applicationName = null, string dbName = null, string connectionName = null, string debug = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceAvStatusExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var connectionName = "connectionName_example";  // string |  (optional) 
            var debug = "debug_example";  // string |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceAvStatus(applicationName, dbName, connectionName, debug);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceAvStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceAvStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceAvStatusWithHttpInfo(applicationName, dbName, connectionName, debug);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceAvStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **connectionName** | **string** |  | [optional]  |
| **debug** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcecacheformulas"></a>
# **FederatedOverDatasourceCacheFormulas**
> void FederatedOverDatasourceCacheFormulas (string connectionName = null, string jagentInstanceId = null, string applicationName = null, string dbName = null, string debug = null, string reqId = null, bool? bSQLSessionGovernor = null, string body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceCacheFormulasExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var jagentInstanceId = "jagentInstanceId_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var debug = "debug_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var bSQLSessionGovernor = true;  // bool? |  (optional) 
            var body = "body_example";  // string |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceCacheFormulas(connectionName, jagentInstanceId, applicationName, dbName, debug, reqId, bSQLSessionGovernor, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCacheFormulas: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceCacheFormulasWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceCacheFormulasWithHttpInfo(connectionName, jagentInstanceId, applicationName, dbName, debug, reqId, bSQLSessionGovernor, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCacheFormulasWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **jagentInstanceId** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **debug** | **string** |  | [optional]  |
| **reqId** | **string** |  | [optional]  |
| **bSQLSessionGovernor** | **bool?** |  | [optional]  |
| **body** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcecancelrequest"></a>
# **FederatedOverDatasourceCancelRequest**
> void FederatedOverDatasourceCancelRequest (string jagentInstanceId = null, string applicationName = null, string dbName = null, string reqId = null, bool? bSQLSessionGovernor = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceCancelRequestExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var jagentInstanceId = "jagentInstanceId_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var bSQLSessionGovernor = true;  // bool? |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceCancelRequest(jagentInstanceId, applicationName, dbName, reqId, bSQLSessionGovernor);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCancelRequest: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceCancelRequestWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceCancelRequestWithHttpInfo(jagentInstanceId, applicationName, dbName, reqId, bSQLSessionGovernor);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCancelRequestWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **jagentInstanceId** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **reqId** | **string** |  | [optional]  |
| **bSQLSessionGovernor** | **bool?** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourceclear"></a>
# **FederatedOverDatasourceClear**
> void FederatedOverDatasourceClear (string connectionName = null, string dbName = null, string applicationName = null, string reqId = null, bool? bLogical = null, long? otlId = null, string debug = null, FederatedDataDTO body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceClearExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var bLogical = true;  // bool? |  (optional) 
            var otlId = 789L;  // long? |  (optional) 
            var debug = "debug_example";  // string |  (optional) 
            var body = new FederatedDataDTO(); // FederatedDataDTO |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceClear(connectionName, dbName, applicationName, reqId, bLogical, otlId, debug, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceClear: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceClearWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceClearWithHttpInfo(connectionName, dbName, applicationName, reqId, bLogical, otlId, debug, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceClearWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **reqId** | **string** |  | [optional]  |
| **bLogical** | **bool?** |  | [optional]  |
| **otlId** | **long?** |  | [optional]  |
| **debug** | **string** |  | [optional]  |
| **body** | [**FederatedDataDTO**](FederatedDataDTO.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcecreateav"></a>
# **FederatedOverDatasourceCreateAv**
> void FederatedOverDatasourceCreateAv (string connectionName = null, string applicationName = null, string dbName = null, string reqId = null, bool? isInMemoryAllowed = null, string debug = null, AvDdlDTO body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceCreateAvExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var isInMemoryAllowed = true;  // bool? |  (optional) 
            var debug = "debug_example";  // string |  (optional) 
            var body = new AvDdlDTO(); // AvDdlDTO |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceCreateAv(connectionName, applicationName, dbName, reqId, isInMemoryAllowed, debug, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCreateAv: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceCreateAvWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceCreateAvWithHttpInfo(connectionName, applicationName, dbName, reqId, isInMemoryAllowed, debug, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCreateAvWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **reqId** | **string** |  | [optional]  |
| **isInMemoryAllowed** | **bool?** |  | [optional]  |
| **debug** | **string** |  | [optional]  |
| **body** | [**AvDdlDTO**](AvDdlDTO.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: text/plain


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcecreatemappingcr"></a>
# **FederatedOverDatasourceCreateMappingCR**
> void FederatedOverDatasourceCreateMappingCR (string applicationName = null, string dbName = null, CrPartitionSettings body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceCreateMappingCRExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var body = new CrPartitionSettings(); // CrPartitionSettings |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceCreateMappingCR(applicationName, dbName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCreateMappingCR: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceCreateMappingCRWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceCreateMappingCRWithHttpInfo(applicationName, dbName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCreateMappingCRWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **body** | [**CrPartitionSettings**](CrPartitionSettings.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcecreatemappingdenormalized"></a>
# **FederatedOverDatasourceCreateMappingDenormalized**
> void FederatedOverDatasourceCreateMappingDenormalized (string applicationName = null, string dbName = null, DenormalizedPartition body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceCreateMappingDenormalizedExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var body = new DenormalizedPartition(); // DenormalizedPartition |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceCreateMappingDenormalized(applicationName, dbName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCreateMappingDenormalized: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceCreateMappingDenormalizedWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceCreateMappingDenormalizedWithHttpInfo(applicationName, dbName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCreateMappingDenormalizedWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **body** | [**DenormalizedPartition**](DenormalizedPartition.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcecreatemappingdenormalizedlegacytest"></a>
# **FederatedOverDatasourceCreateMappingDenormalizedLegacyTest**
> void FederatedOverDatasourceCreateMappingDenormalizedLegacyTest (string connectionName = null, string applicationName = null, string dbName = null, string dsName = null, EssToDsMapDTO body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceCreateMappingDenormalizedLegacyTestExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var dsName = "dsName_example";  // string |  (optional) 
            var body = new EssToDsMapDTO(); // EssToDsMapDTO |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceCreateMappingDenormalizedLegacyTest(connectionName, applicationName, dbName, dsName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCreateMappingDenormalizedLegacyTest: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceCreateMappingDenormalizedLegacyTestWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceCreateMappingDenormalizedLegacyTestWithHttpInfo(connectionName, applicationName, dbName, dsName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCreateMappingDenormalizedLegacyTestWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **dsName** | **string** |  | [optional]  |
| **body** | [**EssToDsMapDTO**](EssToDsMapDTO.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcecreatemappinglegacy"></a>
# **FederatedOverDatasourceCreateMappingLegacy**
> void FederatedOverDatasourceCreateMappingLegacy (string connectionName = null, string applicationName = null, string dbName = null, string dsName = null, EssToDsMapDTO body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceCreateMappingLegacyExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var dsName = "dsName_example";  // string |  (optional) 
            var body = new EssToDsMapDTO(); // EssToDsMapDTO |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceCreateMappingLegacy(connectionName, applicationName, dbName, dsName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCreateMappingLegacy: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceCreateMappingLegacyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceCreateMappingLegacyWithHttpInfo(connectionName, applicationName, dbName, dsName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceCreateMappingLegacyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **dsName** | **string** |  | [optional]  |
| **body** | [**EssToDsMapDTO**](EssToDsMapDTO.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcedeletemapping"></a>
# **FederatedOverDatasourceDeleteMapping**
> void FederatedOverDatasourceDeleteMapping (string applicationName = null, string dbName = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceDeleteMappingExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceDeleteMapping(applicationName, dbName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceDeleteMapping: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceDeleteMappingWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceDeleteMappingWithHttpInfo(applicationName, dbName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceDeleteMappingWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcefactmanagedsettings"></a>
# **FederatedOverDatasourceFactManagedSettings**
> void FederatedOverDatasourceFactManagedSettings (string connectionName = null, string applicationName = null, string dbName = null, string reqId = null, string debug = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceFactManagedSettingsExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var debug = "debug_example";  // string |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceFactManagedSettings(connectionName, applicationName, dbName, reqId, debug);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceFactManagedSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceFactManagedSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceFactManagedSettingsWithHttpInfo(connectionName, applicationName, dbName, reqId, debug);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceFactManagedSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **reqId** | **string** |  | [optional]  |
| **debug** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcegetfedversion"></a>
# **FederatedOverDatasourceGetFedVersion**
> void FederatedOverDatasourceGetFedVersion ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceGetFedVersionExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);

            try
            {
                apiInstance.FederatedOverDatasourceGetFedVersion();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceGetFedVersion: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceGetFedVersionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceGetFedVersionWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceGetFedVersionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcegetmapping"></a>
# **FederatedOverDatasourceGetMapping**
> void FederatedOverDatasourceGetMapping (string connectionName = null, string applicationName = null, string dbName = null, string reqId = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceGetMappingExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceGetMapping(connectionName, applicationName, dbName, reqId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceGetMapping: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceGetMappingWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceGetMappingWithHttpInfo(connectionName, applicationName, dbName, reqId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceGetMappingWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **reqId** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcegetmappingcr"></a>
# **FederatedOverDatasourceGetMappingCR**
> void FederatedOverDatasourceGetMappingCR (string applicationName = null, string dbName = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceGetMappingCRExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceGetMappingCR(applicationName, dbName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceGetMappingCR: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceGetMappingCRWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceGetMappingCRWithHttpInfo(applicationName, dbName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceGetMappingCRWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcegetmappingdenormalized"></a>
# **FederatedOverDatasourceGetMappingDenormalized**
> void FederatedOverDatasourceGetMappingDenormalized (string applicationName = null, string dbName = null, string dsName = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceGetMappingDenormalizedExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var dsName = "dsName_example";  // string |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceGetMappingDenormalized(applicationName, dbName, dsName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceGetMappingDenormalized: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceGetMappingDenormalizedWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceGetMappingDenormalizedWithHttpInfo(applicationName, dbName, dsName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceGetMappingDenormalizedWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **dsName** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcereport"></a>
# **FederatedOverDatasourceReport**
> void FederatedOverDatasourceReport (string connectionName = null, string jagentInstanceId = null, string dbName = null, string applicationName = null, string reqId = null, string datasourceName = null, int? queryTimeout = null, bool? bSQLSessionGovernor = null, long? otlId = null, string debug = null, bool? cr = null, string measureDimensionName = null, string essbaseToColumnMap = null, FederatedDataDTO body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceReportExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var jagentInstanceId = "jagentInstanceId_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var datasourceName = "datasourceName_example";  // string |  (optional) 
            var queryTimeout = 56;  // int? |  (optional) 
            var bSQLSessionGovernor = true;  // bool? |  (optional) 
            var otlId = 789L;  // long? |  (optional) 
            var debug = "debug_example";  // string |  (optional) 
            var cr = true;  // bool? |  (optional) 
            var measureDimensionName = "measureDimensionName_example";  // string |  (optional) 
            var essbaseToColumnMap = "essbaseToColumnMap_example";  // string |  (optional) 
            var body = new FederatedDataDTO(); // FederatedDataDTO |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceReport(connectionName, jagentInstanceId, dbName, applicationName, reqId, datasourceName, queryTimeout, bSQLSessionGovernor, otlId, debug, cr, measureDimensionName, essbaseToColumnMap, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceReport: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceReportWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceReportWithHttpInfo(connectionName, jagentInstanceId, dbName, applicationName, reqId, datasourceName, queryTimeout, bSQLSessionGovernor, otlId, debug, cr, measureDimensionName, essbaseToColumnMap, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceReportWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **jagentInstanceId** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **reqId** | **string** |  | [optional]  |
| **datasourceName** | **string** |  | [optional]  |
| **queryTimeout** | **int?** |  | [optional]  |
| **bSQLSessionGovernor** | **bool?** |  | [optional]  |
| **otlId** | **long?** |  | [optional]  |
| **debug** | **string** |  | [optional]  |
| **cr** | **bool?** |  | [optional]  |
| **measureDimensionName** | **string** |  | [optional]  |
| **essbaseToColumnMap** | **string** |  | [optional]  |
| **body** | [**FederatedDataDTO**](FederatedDataDTO.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/octet-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcesaveformulas"></a>
# **FederatedOverDatasourceSaveFormulas**
> void FederatedOverDatasourceSaveFormulas (string connectionName = null, string jagentInstanceId = null, string applicationName = null, string dbName = null, string reqId = null, bool? bSQLSessionGovernor = null, bool? isAllDimensions = null, string debug = null, string body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceSaveFormulasExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var jagentInstanceId = "jagentInstanceId_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var bSQLSessionGovernor = true;  // bool? |  (optional) 
            var isAllDimensions = true;  // bool? |  (optional) 
            var debug = "debug_example";  // string |  (optional) 
            var body = "body_example";  // string |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceSaveFormulas(connectionName, jagentInstanceId, applicationName, dbName, reqId, bSQLSessionGovernor, isAllDimensions, debug, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceSaveFormulas: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceSaveFormulasWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceSaveFormulasWithHttpInfo(connectionName, jagentInstanceId, applicationName, dbName, reqId, bSQLSessionGovernor, isAllDimensions, debug, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceSaveFormulasWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **jagentInstanceId** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **reqId** | **string** |  | [optional]  |
| **bSQLSessionGovernor** | **bool?** |  | [optional]  |
| **isAllDimensions** | **bool?** |  | [optional]  |
| **debug** | **string** |  | [optional]  |
| **body** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcevalidate"></a>
# **FederatedOverDatasourceValidate**
> void FederatedOverDatasourceValidate (string applicationName = null, string dbName = null, string connectionName = null, string debug = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceValidateExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var connectionName = "connectionName_example";  // string |  (optional) 
            var debug = "debug_example";  // string |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceValidate(applicationName, dbName, connectionName, debug);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceValidate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceValidateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceValidateWithHttpInfo(applicationName, dbName, connectionName, debug);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceValidateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **connectionName** | **string** |  | [optional]  |
| **debug** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="federatedoverdatasourcewriteback"></a>
# **FederatedOverDatasourceWriteback**
> void FederatedOverDatasourceWriteback (string connectionName = null, string jagentInstanceId = null, string essbaseToColumnMap = null, string debug = null, string applicationName = null, string dbName = null, string reqId = null, bool? bSQLSessionGovernor = null, long? otlId = null, Object body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FederatedOverDatasourceWritebackExample
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

            var apiInstance = new FederatedOverDatasourceApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var jagentInstanceId = "jagentInstanceId_example";  // string |  (optional) 
            var essbaseToColumnMap = "essbaseToColumnMap_example";  // string |  (optional) 
            var debug = "debug_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var bSQLSessionGovernor = true;  // bool? |  (optional) 
            var otlId = 789L;  // long? |  (optional) 
            var body = null;  // Object |  (optional) 

            try
            {
                apiInstance.FederatedOverDatasourceWriteback(connectionName, jagentInstanceId, essbaseToColumnMap, debug, applicationName, dbName, reqId, bSQLSessionGovernor, otlId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceWriteback: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FederatedOverDatasourceWritebackWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FederatedOverDatasourceWritebackWithHttpInfo(connectionName, jagentInstanceId, essbaseToColumnMap, debug, applicationName, dbName, reqId, bSQLSessionGovernor, otlId, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FederatedOverDatasourceApi.FederatedOverDatasourceWritebackWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **connectionName** | **string** |  | [optional]  |
| **jagentInstanceId** | **string** |  | [optional]  |
| **essbaseToColumnMap** | **string** |  | [optional]  |
| **debug** | **string** |  | [optional]  |
| **applicationName** | **string** |  | [optional]  |
| **dbName** | **string** |  | [optional]  |
| **reqId** | **string** |  | [optional]  |
| **bSQLSessionGovernor** | **bool?** |  | [optional]  |
| **otlId** | **long?** |  | [optional]  |
| **body** | **Object** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/octet-stream
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

