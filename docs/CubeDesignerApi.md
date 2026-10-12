# EssSharp.Api.CubeDesignerApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AicdAnalyzeschema**](CubeDesignerApi.md#aicdanalyzeschema) | **POST** /cubedesigner/analyze | Analyze Schema |
| [**AicdCleanupsessions**](CubeDesignerApi.md#aicdcleanupsessions) | **POST** /cubedesigner/sessions/cleanup | Cleanup Sessions |
| [**AicdCreatesession**](CubeDesignerApi.md#aicdcreatesession) | **POST** /cubedesigner/session | Create Session |
| [**AicdDeletesession**](CubeDesignerApi.md#aicddeletesession) | **DELETE** /cubedesigner/session | Delete Session |
| [**AicdDeploycube**](CubeDesignerApi.md#aicddeploycube) | **POST** /cubedesigner/deploy | Deploy Cube |
| [**AicdDiscover**](CubeDesignerApi.md#aicddiscover) | **POST** /cubedesigner/discover | Discover Relations Between Two Or More Tables |
| [**AicdExportositemplate**](CubeDesignerApi.md#aicdexportositemplate) | **POST** /cubedesigner/export-osi | Export Cube Creator Template |
| [**AicdGetanalyzelogs**](CubeDesignerApi.md#aicdgetanalyzelogs) | **GET** /cubedesigner/analyze-logs | Get Analyze Logs |
| [**AicdGetlogs**](CubeDesignerApi.md#aicdgetlogs) | **GET** /cubedesigner/logs | Get Pipeline Logs |
| [**AicdGetpersonas**](CubeDesignerApi.md#aicdgetpersonas) | **GET** /cubedesigner/personas | List Personas |
| [**AicdGetresult**](CubeDesignerApi.md#aicdgetresult) | **GET** /cubedesigner/result | Get Pipeline Result |
| [**AicdGetscenarios**](CubeDesignerApi.md#aicdgetscenarios) | **POST** /cubedesigner/recommend | List Scenarios |
| [**AicdGetscenarios1**](CubeDesignerApi.md#aicdgetscenarios1) | **GET** /cubedesigner/scenarios | List Scenarios |
| [**AicdImportosischema**](CubeDesignerApi.md#aicdimportosischema) | **POST** /cubedesigner/import-osi | Import OSI Schema |
| [**AicdListsessions**](CubeDesignerApi.md#aicdlistsessions) | **GET** /cubedesigner/sessions | List Sessions |
| [**AicdValidateconnection**](CubeDesignerApi.md#aicdvalidateconnection) | **POST** /cubedesigner/validate | Validate Connection |
| [**AicdValidateosischema**](CubeDesignerApi.md#aicdvalidateosischema) | **POST** /cubedesigner/validate-osi-schema | Validate OSI Schema |

<a id="aicdanalyzeschema"></a>
# **AicdAnalyzeschema**
> void AicdAnalyzeschema (string sessionId = null, AnalyzeRequest body = null)

Analyze Schema

Analyzes the given fact table(s) to find column -> dimension/measure mapping.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdAnalyzeschemaExample
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

            var apiInstance = new CubeDesignerApi(config);
            var sessionId = "sessionId_example";  // string |  (optional) 
            var body = new AnalyzeRequest(); // AnalyzeRequest |  (optional) 

            try
            {
                // Analyze Schema
                apiInstance.AicdAnalyzeschema(sessionId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdAnalyzeschema: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdAnalyzeschemaWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Analyze Schema
    apiInstance.AicdAnalyzeschemaWithHttpInfo(sessionId, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdAnalyzeschemaWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** |  | [optional]  |
| **body** | [**AnalyzeRequest**](AnalyzeRequest.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicdcleanupsessions"></a>
# **AicdCleanupsessions**
> void AicdCleanupsessions (int? idleMinutes = null)

Cleanup Sessions

Deletes idle Cube designer sessions that have not been active for the requested number of minutes.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdCleanupsessionsExample
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

            var apiInstance = new CubeDesignerApi(config);
            var idleMinutes = 56;  // int? |  (optional) 

            try
            {
                // Cleanup Sessions
                apiInstance.AicdCleanupsessions(idleMinutes);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdCleanupsessions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdCleanupsessionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Cleanup Sessions
    apiInstance.AicdCleanupsessionsWithHttpInfo(idleMinutes);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdCleanupsessionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **idleMinutes** | **int?** |  | [optional]  |

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

<a id="aicdcreatesession"></a>
# **AicdCreatesession**
> void AicdCreatesession ()

Create Session

Creates a new Cube designer session

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdCreatesessionExample
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

            var apiInstance = new CubeDesignerApi(config);

            try
            {
                // Create Session
                apiInstance.AicdCreatesession();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdCreatesession: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdCreatesessionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Session
    apiInstance.AicdCreatesessionWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdCreatesessionWithHttpInfo: " + e.Message);
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

<a id="aicddeletesession"></a>
# **AicdDeletesession**
> void AicdDeletesession (string sessionId = null)

Delete Session

Deletes a Cube designer session

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdDeletesessionExample
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

            var apiInstance = new CubeDesignerApi(config);
            var sessionId = "sessionId_example";  // string |  (optional) 

            try
            {
                // Delete Session
                apiInstance.AicdDeletesession(sessionId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdDeletesession: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdDeletesessionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Session
    apiInstance.AicdDeletesessionWithHttpInfo(sessionId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdDeletesessionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** |  | [optional]  |

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

<a id="aicddeploycube"></a>
# **AicdDeploycube**
> void AicdDeploycube (string sessionId = null, ExecuteRequest body = null)

Deploy Cube

From given cube structure, creates outline rule files. and load data into the cube.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdDeploycubeExample
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

            var apiInstance = new CubeDesignerApi(config);
            var sessionId = "sessionId_example";  // string |  (optional) 
            var body = new ExecuteRequest(); // ExecuteRequest |  (optional) 

            try
            {
                // Deploy Cube
                apiInstance.AicdDeploycube(sessionId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdDeploycube: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdDeploycubeWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Deploy Cube
    apiInstance.AicdDeploycubeWithHttpInfo(sessionId, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdDeploycubeWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** |  | [optional]  |
| **body** | [**ExecuteRequest**](ExecuteRequest.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicddiscover"></a>
# **AicdDiscover**
> void AicdDiscover (string sessionId = null, DiscoverRequest body = null)

Discover Relations Between Two Or More Tables

From a list of tables or full schema discovers relationships that can be used to construct dimensions

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdDiscoverExample
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

            var apiInstance = new CubeDesignerApi(config);
            var sessionId = "sessionId_example";  // string |  (optional) 
            var body = new DiscoverRequest(); // DiscoverRequest |  (optional) 

            try
            {
                // Discover Relations Between Two Or More Tables
                apiInstance.AicdDiscover(sessionId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdDiscover: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdDiscoverWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Discover Relations Between Two Or More Tables
    apiInstance.AicdDiscoverWithHttpInfo(sessionId, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdDiscoverWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** |  | [optional]  |
| **body** | [**DiscoverRequest**](DiscoverRequest.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicdexportositemplate"></a>
# **AicdExportositemplate**
> void AicdExportositemplate (string sessionId = null, ExecuteRequest body = null)

Export Cube Creator Template

Generates an Open Semantic Interchange YAML template from the reviewed cube mapping without deploying the cube.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdExportositemplateExample
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

            var apiInstance = new CubeDesignerApi(config);
            var sessionId = "sessionId_example";  // string |  (optional) 
            var body = new ExecuteRequest(); // ExecuteRequest |  (optional) 

            try
            {
                // Export Cube Creator Template
                apiInstance.AicdExportositemplate(sessionId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdExportositemplate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdExportositemplateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Export Cube Creator Template
    apiInstance.AicdExportositemplateWithHttpInfo(sessionId, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdExportositemplateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** |  | [optional]  |
| **body** | [**ExecuteRequest**](ExecuteRequest.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicdgetanalyzelogs"></a>
# **AicdGetanalyzelogs**
> void AicdGetanalyzelogs (string sessionId = null)

Get Analyze Logs

Streams live analyze logs for the current cube designer session.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdGetanalyzelogsExample
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

            var apiInstance = new CubeDesignerApi(config);
            var sessionId = "sessionId_example";  // string |  (optional) 

            try
            {
                // Get Analyze Logs
                apiInstance.AicdGetanalyzelogs(sessionId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdGetanalyzelogs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdGetanalyzelogsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Analyze Logs
    apiInstance.AicdGetanalyzelogsWithHttpInfo(sessionId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdGetanalyzelogsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/event-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicdgetlogs"></a>
# **AicdGetlogs**
> void AicdGetlogs (string pipelineid = null)

Get Pipeline Logs

From a pipeline id, gets live logs.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdGetlogsExample
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

            var apiInstance = new CubeDesignerApi(config);
            var pipelineid = "pipelineid_example";  // string |  (optional) 

            try
            {
                // Get Pipeline Logs
                apiInstance.AicdGetlogs(pipelineid);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdGetlogs: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdGetlogsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Pipeline Logs
    apiInstance.AicdGetlogsWithHttpInfo(pipelineid);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdGetlogsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **pipelineid** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/event-stream


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicdgetpersonas"></a>
# **AicdGetpersonas**
> void AicdGetpersonas ()

List Personas

Gets a list of personas available to the cube designer

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdGetpersonasExample
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

            var apiInstance = new CubeDesignerApi(config);

            try
            {
                // List Personas
                apiInstance.AicdGetpersonas();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdGetpersonas: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdGetpersonasWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Personas
    apiInstance.AicdGetpersonasWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdGetpersonasWithHttpInfo: " + e.Message);
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

<a id="aicdgetresult"></a>
# **AicdGetresult**
> void AicdGetresult (string sessionId = null)

Get Pipeline Result

Gets the latest Cube Creator pipeline status and result for a session, including YAML after completion.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdGetresultExample
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

            var apiInstance = new CubeDesignerApi(config);
            var sessionId = "sessionId_example";  // string |  (optional) 

            try
            {
                // Get Pipeline Result
                apiInstance.AicdGetresult(sessionId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdGetresult: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdGetresultWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Pipeline Result
    apiInstance.AicdGetresultWithHttpInfo(sessionId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdGetresultWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** |  | [optional]  |

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

<a id="aicdgetscenarios"></a>
# **AicdGetscenarios**
> void AicdGetscenarios (RecommendRequest body = null)

List Scenarios

Gets a list of scenarios available to the persona/user role

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdGetscenariosExample
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

            var apiInstance = new CubeDesignerApi(config);
            var body = new RecommendRequest(); // RecommendRequest |  (optional) 

            try
            {
                // List Scenarios
                apiInstance.AicdGetscenarios(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdGetscenarios: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdGetscenariosWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Scenarios
    apiInstance.AicdGetscenariosWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdGetscenariosWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**RecommendRequest**](RecommendRequest.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicdgetscenarios1"></a>
# **AicdGetscenarios1**
> void AicdGetscenarios1 (string persona = null)

List Scenarios

Gets a list of scenarios available to the persona/user role

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdGetscenarios1Example
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

            var apiInstance = new CubeDesignerApi(config);
            var persona = "persona_example";  // string |  (optional) 

            try
            {
                // List Scenarios
                apiInstance.AicdGetscenarios1(persona);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdGetscenarios1: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdGetscenarios1WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Scenarios
    apiInstance.AicdGetscenarios1WithHttpInfo(persona);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdGetscenarios1WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **persona** | **string** |  | [optional]  |

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

<a id="aicdimportosischema"></a>
# **AicdImportosischema**
> void AicdImportosischema (string sessionId = null, string body = null)

Import OSI Schema

Imports an Open Semantic Interchange YAML document into the current cube designer session and return the inferred preview.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdImportosischemaExample
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

            var apiInstance = new CubeDesignerApi(config);
            var sessionId = "sessionId_example";  // string |  (optional) 
            var body = "body_example";  // string |  (optional) 

            try
            {
                // Import OSI Schema
                apiInstance.AicdImportosischema(sessionId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdImportosischema: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdImportosischemaWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Import OSI Schema
    apiInstance.AicdImportosischemaWithHttpInfo(sessionId, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdImportosischemaWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** |  | [optional]  |
| **body** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: text/plain
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicdlistsessions"></a>
# **AicdListsessions**
> void AicdListsessions ()

List Sessions

Lists active cube designer sessions.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdListsessionsExample
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

            var apiInstance = new CubeDesignerApi(config);

            try
            {
                // List Sessions
                apiInstance.AicdListsessions();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdListsessions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdListsessionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Sessions
    apiInstance.AicdListsessionsWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdListsessionsWithHttpInfo: " + e.Message);
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

<a id="aicdvalidateconnection"></a>
# **AicdValidateconnection**
> void AicdValidateconnection (string sessionId = null, ConnectRequest body = null)

Validate Connection

Validates connection for select AI capability and if successful return list of tables.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdValidateconnectionExample
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

            var apiInstance = new CubeDesignerApi(config);
            var sessionId = "sessionId_example";  // string |  (optional) 
            var body = new ConnectRequest(); // ConnectRequest |  (optional) 

            try
            {
                // Validate Connection
                apiInstance.AicdValidateconnection(sessionId, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdValidateconnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdValidateconnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Validate Connection
    apiInstance.AicdValidateconnectionWithHttpInfo(sessionId, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdValidateconnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **sessionId** | **string** |  | [optional]  |
| **body** | [**ConnectRequest**](ConnectRequest.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicdvalidateosischema"></a>
# **AicdValidateosischema**
> void AicdValidateosischema (string body = null)

Validate OSI Schema

Validates an Open Semantic Interchange YAML document against the bundled OSI schema.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AicdValidateosischemaExample
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

            var apiInstance = new CubeDesignerApi(config);
            var body = "body_example";  // string |  (optional) 

            try
            {
                // Validate OSI Schema
                apiInstance.AicdValidateosischema(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling CubeDesignerApi.AicdValidateosischema: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AicdValidateosischemaWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Validate OSI Schema
    apiInstance.AicdValidateosischemaWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling CubeDesignerApi.AicdValidateosischemaWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: text/plain
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

