# EssSharp.Api.FastWritebackApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**FastWritebackCheckIfSQLLoad**](FastWritebackApi.md#fastwritebackcheckifsqlload) | **GET** /fastwriteback/checkSQLload |  |
| [**FastWritebackLoad**](FastWritebackApi.md#fastwritebackload) | **POST** /fastwriteback/adwload |  |

<a id="fastwritebackcheckifsqlload"></a>
# **FastWritebackCheckIfSQLLoad**
> void FastWritebackCheckIfSQLLoad (string applicationName = null, string dbName = null, string connectionName = null, string reqId = null, string ruleFilePath = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FastWritebackCheckIfSQLLoadExample
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

            var apiInstance = new FastWritebackApi(config);
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var connectionName = "connectionName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var ruleFilePath = "ruleFilePath_example";  // string |  (optional) 

            try
            {
                apiInstance.FastWritebackCheckIfSQLLoad(applicationName, dbName, connectionName, reqId, ruleFilePath);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FastWritebackApi.FastWritebackCheckIfSQLLoad: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FastWritebackCheckIfSQLLoadWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FastWritebackCheckIfSQLLoadWithHttpInfo(applicationName, dbName, connectionName, reqId, ruleFilePath);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FastWritebackApi.FastWritebackCheckIfSQLLoadWithHttpInfo: " + e.Message);
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
| **reqId** | **string** |  | [optional]  |
| **ruleFilePath** | **string** |  | [optional]  |

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

<a id="fastwritebackload"></a>
# **FastWritebackLoad**
> void FastWritebackLoad (string connectionName = null, string applicationName = null, string dbName = null, string reqId = null, string tableName = null, string filePath = null, string containsMissing = null, string specialRecordDelimiter = null, string useEscape = null, bool? fileExists = null, bool? bFactManagedByFederatedCube = null, bool? serverProcessing = null, bool? isAppASO = null, string jagentInstanceId = null, string fieldList = null, bool? lcmDataLoad = null, bool? bGetPartitionSettings = null, string rulOperation = null, string rFfileHdr = null, string rFConstHdr = null, string rFdelimiter = null, bool? bDBMSFormatMigrationCase = null, bool? bSQLSessionGovernor = null, bool? bstardataformat = null, string ruleFilePath = null, bool? bAvDbmsClassicPath = null, Object body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FastWritebackLoadExample
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

            var apiInstance = new FastWritebackApi(config);
            var connectionName = "connectionName_example";  // string |  (optional) 
            var applicationName = "applicationName_example";  // string |  (optional) 
            var dbName = "dbName_example";  // string |  (optional) 
            var reqId = "reqId_example";  // string |  (optional) 
            var tableName = "tableName_example";  // string |  (optional) 
            var filePath = "filePath_example";  // string |  (optional) 
            var containsMissing = "containsMissing_example";  // string |  (optional) 
            var specialRecordDelimiter = "specialRecordDelimiter_example";  // string |  (optional) 
            var useEscape = "useEscape_example";  // string |  (optional) 
            var fileExists = true;  // bool? |  (optional) 
            var bFactManagedByFederatedCube = true;  // bool? |  (optional) 
            var serverProcessing = true;  // bool? |  (optional) 
            var isAppASO = true;  // bool? |  (optional) 
            var jagentInstanceId = "jagentInstanceId_example";  // string |  (optional) 
            var fieldList = "fieldList_example";  // string |  (optional) 
            var lcmDataLoad = true;  // bool? |  (optional) 
            var bGetPartitionSettings = true;  // bool? |  (optional) 
            var rulOperation = "rulOperation_example";  // string |  (optional) 
            var rFfileHdr = "rFfileHdr_example";  // string |  (optional) 
            var rFConstHdr = "rFConstHdr_example";  // string |  (optional) 
            var rFdelimiter = "rFdelimiter_example";  // string |  (optional) 
            var bDBMSFormatMigrationCase = true;  // bool? |  (optional) 
            var bSQLSessionGovernor = true;  // bool? |  (optional) 
            var bstardataformat = true;  // bool? |  (optional) 
            var ruleFilePath = "ruleFilePath_example";  // string |  (optional) 
            var bAvDbmsClassicPath = true;  // bool? |  (optional) 
            var body = null;  // Object |  (optional) 

            try
            {
                apiInstance.FastWritebackLoad(connectionName, applicationName, dbName, reqId, tableName, filePath, containsMissing, specialRecordDelimiter, useEscape, fileExists, bFactManagedByFederatedCube, serverProcessing, isAppASO, jagentInstanceId, fieldList, lcmDataLoad, bGetPartitionSettings, rulOperation, rFfileHdr, rFConstHdr, rFdelimiter, bDBMSFormatMigrationCase, bSQLSessionGovernor, bstardataformat, ruleFilePath, bAvDbmsClassicPath, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FastWritebackApi.FastWritebackLoad: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FastWritebackLoadWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.FastWritebackLoadWithHttpInfo(connectionName, applicationName, dbName, reqId, tableName, filePath, containsMissing, specialRecordDelimiter, useEscape, fileExists, bFactManagedByFederatedCube, serverProcessing, isAppASO, jagentInstanceId, fieldList, lcmDataLoad, bGetPartitionSettings, rulOperation, rFfileHdr, rFConstHdr, rFdelimiter, bDBMSFormatMigrationCase, bSQLSessionGovernor, bstardataformat, ruleFilePath, bAvDbmsClassicPath, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FastWritebackApi.FastWritebackLoadWithHttpInfo: " + e.Message);
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
| **tableName** | **string** |  | [optional]  |
| **filePath** | **string** |  | [optional]  |
| **containsMissing** | **string** |  | [optional]  |
| **specialRecordDelimiter** | **string** |  | [optional]  |
| **useEscape** | **string** |  | [optional]  |
| **fileExists** | **bool?** |  | [optional]  |
| **bFactManagedByFederatedCube** | **bool?** |  | [optional]  |
| **serverProcessing** | **bool?** |  | [optional]  |
| **isAppASO** | **bool?** |  | [optional]  |
| **jagentInstanceId** | **string** |  | [optional]  |
| **fieldList** | **string** |  | [optional]  |
| **lcmDataLoad** | **bool?** |  | [optional]  |
| **bGetPartitionSettings** | **bool?** |  | [optional]  |
| **rulOperation** | **string** |  | [optional]  |
| **rFfileHdr** | **string** |  | [optional]  |
| **rFConstHdr** | **string** |  | [optional]  |
| **rFdelimiter** | **string** |  | [optional]  |
| **bDBMSFormatMigrationCase** | **bool?** |  | [optional]  |
| **bSQLSessionGovernor** | **bool?** |  | [optional]  |
| **bstardataformat** | **bool?** |  | [optional]  |
| **ruleFilePath** | **string** |  | [optional]  |
| **bAvDbmsClassicPath** | **bool?** |  | [optional]  |
| **body** | **Object** |  | [optional]  |

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

