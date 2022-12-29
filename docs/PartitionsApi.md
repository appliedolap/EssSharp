# EssSharp.Api.PartitionsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**PartitionCreatePartition**](PartitionsApi.md#partitioncreatepartition) | **POST** /applications/{applicationName}/databases/{databaseName}/partitions | Create Partition |
| [**PartitionDeletePartition**](PartitionsApi.md#partitiondeletepartition) | **DELETE** /applications/{applicationName}/databases/{databaseName}/partitions | Delete Partition |
| [**PartitionGetPartitionCellCount**](PartitionsApi.md#partitiongetpartitioncellcount) | **POST** /applications/{applicationName}/databases/{databaseName}/partitions/cellcount | Get Partition Cell Count |
| [**PartitionGetPartitions**](PartitionsApi.md#partitiongetpartitions) | **GET** /applications/{applicationName}/databases/{databaseName}/partitions | Get Partitions |
| [**PartitionGetSupportedFederatedTypes**](PartitionsApi.md#partitiongetsupportedfederatedtypes) | **GET** /applications/{applicationName}/databases/{databaseName}/partitions/supportedfederatedtypes | Get Supported Partition Types |
| [**PartitionLockPartition**](PartitionsApi.md#partitionlockpartition) | **POST** /applications/{applicationName}/databases/{databaseName}/partitions/lock | Lock Partition |
| [**PartitionReplicateDataFromSource**](PartitionsApi.md#partitionreplicatedatafromsource) | **POST** /applications/{applicationName}/databases/{databaseName}/partitions/replicatedata | Replicate Data |
| [**PartitionUnlockPartition**](PartitionsApi.md#partitionunlockpartition) | **POST** /applications/{applicationName}/databases/{databaseName}/partitions/unlock | Unlock Partition |
| [**PartitionUpdatePartition**](PartitionsApi.md#partitionupdatepartition) | **PATCH** /applications/{applicationName}/databases/{databaseName}/partitions | Update Partition |
| [**PartitionValidatePartition**](PartitionsApi.md#partitionvalidatepartition) | **POST** /applications/{applicationName}/databases/{databaseName}/partitions/validate | Validate Partition |

<a name="partitioncreatepartition"></a>
# **PartitionCreatePartition**
> void PartitionCreatePartition (string applicationName, string databaseName, PartitionBean body, bool? executeInBackground = null)

Create Partition

<p>Creates a new partition. For partitions across instances, <i>connectionName</i> must be provided.</p><p>If the connection is defined at the application level, the property <i>applicationLevelConnection</i> must be specified as true.</p><p>If the Datasource is defined at the application level, the property <i>applicationLevelDatasource</i>  must be specified as true.</p><p>Examples (with minimum required properties)</p><p><b>TRANSPARENT/REPLICATED</b> (change type appropriately)</p><p><code>{'type':'TRANSPARENT','isNew':true,'sourceInfo':{'applicationName':'Sample_2','databaseName':'Basic'},'areas':[{'sourceArea':'Jan','targetArea':'Jan','slices':[]}],'mappings':[]}</code></p><p><b>FEDERATED</b> </p><p><code>{'type':'FEDERATED','isNew':true,'sourceInfo':{'datasourceName':'federatedDatasourceExcel1','measuresDimensionName':'Measures','essbaseToColumnMap':{'arr':[{'essbaseName':'Caffeinated','columnName':''},{'essbaseName':'Ounces','columnName':''},{'essbaseName':'Pkg Type','columnName':''},{'essbaseName':'Population','columnName':''},{'essbaseName':'Intro Date','columnName':''}]}},'areas':[{'sourceArea':'Jan','targetArea':'Jan','slices':[]}]}</code></p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PartitionCreatePartitionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PartitionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new PartitionBean(); // PartitionBean | <p>Partition information.</p>
            var executeInBackground = false;  // bool? | <p>Execute Analytic View federated partition in background</p> (optional)  (default to false)

            try
            {
                // Create Partition
                apiInstance.PartitionCreatePartition(applicationName, databaseName, body, executeInBackground);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PartitionsApi.PartitionCreatePartition: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PartitionCreatePartitionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Partition
    apiInstance.PartitionCreatePartitionWithHttpInfo(applicationName, databaseName, body, executeInBackground);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PartitionsApi.PartitionCreatePartitionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**PartitionBean**](PartitionBean.md) | &lt;p&gt;Partition information.&lt;/p&gt; |  |
| **executeInBackground** | **bool?** | &lt;p&gt;Execute Analytic View federated partition in background&lt;/p&gt; | [optional] [default to false] |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Partition created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create partition.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="partitiondeletepartition"></a>
# **PartitionDeletePartition**
> void PartitionDeletePartition (string applicationName, string databaseName, string type, string serverName = null, string applicationName2 = null, string databaseName2 = null, string datasourceName = null, string measuresDimensionName = null, bool? applicationLevelDatasource = null)

Delete Partition

<p>Deletes a partition based on the query parameters.</p> <ol><li>To delete a transparent or replicated partition, the partition type, source server, source application, and source database are required.</li><li>To delete a federated partition, the partition type, Datasource name, and measures dimension name are required.</li>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PartitionDeletePartitionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PartitionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var type = "TRANSPARENT";  // string | <p>Partition type.</p>
            var serverName = "serverName_example";  // string | <p>Partition source server name.</p> (optional) 
            var applicationName2 = "applicationName_example";  // string | <p>Partition source application name.</p> (optional) 
            var databaseName2 = "databaseName_example";  // string | <p>Partition source database name.</p> (optional) 
            var datasourceName = "datasourceName_example";  // string | <p>Datasource name.</p> (optional) 
            var measuresDimensionName = "measuresDimensionName_example";  // string | <p>Measures dimension name.</p> (optional) 
            var applicationLevelDatasource = true;  // bool? | <p>Specify whether the Datasource is defined at the application level.</p> (optional) 

            try
            {
                // Delete Partition
                apiInstance.PartitionDeletePartition(applicationName, databaseName, type, serverName, applicationName2, databaseName2, datasourceName, measuresDimensionName, applicationLevelDatasource);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PartitionsApi.PartitionDeletePartition: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PartitionDeletePartitionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Partition
    apiInstance.PartitionDeletePartitionWithHttpInfo(applicationName, databaseName, type, serverName, applicationName2, databaseName2, datasourceName, measuresDimensionName, applicationLevelDatasource);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PartitionsApi.PartitionDeletePartitionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **type** | **string** | &lt;p&gt;Partition type.&lt;/p&gt; |  |
| **serverName** | **string** | &lt;p&gt;Partition source server name.&lt;/p&gt; | [optional]  |
| **applicationName2** | **string** | &lt;p&gt;Partition source application name.&lt;/p&gt; | [optional]  |
| **databaseName2** | **string** | &lt;p&gt;Partition source database name.&lt;/p&gt; | [optional]  |
| **datasourceName** | **string** | &lt;p&gt;Datasource name.&lt;/p&gt; | [optional]  |
| **measuresDimensionName** | **string** | &lt;p&gt;Measures dimension name.&lt;/p&gt; | [optional]  |
| **applicationLevelDatasource** | **bool?** | &lt;p&gt;Specify whether the Datasource is defined at the application level.&lt;/p&gt; | [optional]  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Partition deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete partition.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="partitiongetpartitioncellcount"></a>
# **PartitionGetPartitionCellCount**
> PartitionBean PartitionGetPartitionCellCount (string applicationName, string databaseName, PartitionBean body)

Get Partition Cell Count

<p>Gets source and target cell counts for the partition area definitions. A partition area must have the same number of cells in the source and target.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PartitionGetPartitionCellCountExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PartitionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new PartitionBean(); // PartitionBean | <p>Partition information.</p>

            try
            {
                // Get Partition Cell Count
                PartitionBean result = apiInstance.PartitionGetPartitionCellCount(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PartitionsApi.PartitionGetPartitionCellCount: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PartitionGetPartitionCellCountWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Partition Cell Count
    ApiResponse<PartitionBean> response = apiInstance.PartitionGetPartitionCellCountWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PartitionsApi.PartitionGetPartitionCellCountWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**PartitionBean**](PartitionBean.md) | &lt;p&gt;Partition information.&lt;/p&gt; |  |

### Return type

[**PartitionBean**](PartitionBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Partition area cell count returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get partition area cell count.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="partitiongetpartitions"></a>
# **PartitionGetPartitions**
> PartitionList PartitionGetPartitions (string applicationName, string databaseName, List<string> type = null, int? offset = null, int? limit = null, string serverName = null, string applicationName2 = null, string databaseName2 = null, string datasourceName = null, string measuresDimensionName = null, bool? applicationLevelDatasource = null)

Get Partitions

<p>Returns a list of partitions defined on the database, or a specific partition.</p> <ul><li>With no parameters: Returns all available partitions.</li><li>With <i>type</i> parameter: Returns filtered partitions list.</li><li>With partition type, source server, source application, and source database parameters: Returns the specified partition details.</li></ul>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PartitionGetPartitionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PartitionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var type = new List<string>(); // List<string> | <p>Partition type. Multiple types can be added with a comma separator.</p> (optional) 
            var offset = 56;  // int? | <p>Number of partitions to omit from the start of the result set.</p> (optional) 
            var limit = 56;  // int? | <p>Maximum number of partitions to return.</p> (optional) 
            var serverName = "serverName_example";  // string | <p>Partition source server name.</p> (optional) 
            var applicationName2 = "applicationName_example";  // string | <p>Partition source application name.</p> (optional) 
            var databaseName2 = "databaseName_example";  // string | <p>Partition source database name.</p> (optional) 
            var datasourceName = "datasourceName_example";  // string | <p>Datasource name.</p> (optional) 
            var measuresDimensionName = "measuresDimensionName_example";  // string | <p>Measures dimension name.</p> (optional) 
            var applicationLevelDatasource = true;  // bool? | <p>Specify whether the Datasource is defined at application level.</p> (optional) 

            try
            {
                // Get Partitions
                PartitionList result = apiInstance.PartitionGetPartitions(applicationName, databaseName, type, offset, limit, serverName, applicationName2, databaseName2, datasourceName, measuresDimensionName, applicationLevelDatasource);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PartitionsApi.PartitionGetPartitions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PartitionGetPartitionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Partitions
    ApiResponse<PartitionList> response = apiInstance.PartitionGetPartitionsWithHttpInfo(applicationName, databaseName, type, offset, limit, serverName, applicationName2, databaseName2, datasourceName, measuresDimensionName, applicationLevelDatasource);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PartitionsApi.PartitionGetPartitionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **type** | [**List&lt;string&gt;**](string.md) | &lt;p&gt;Partition type. Multiple types can be added with a comma separator.&lt;/p&gt; | [optional]  |
| **offset** | **int?** | &lt;p&gt;Number of partitions to omit from the start of the result set.&lt;/p&gt; | [optional]  |
| **limit** | **int?** | &lt;p&gt;Maximum number of partitions to return.&lt;/p&gt; | [optional]  |
| **serverName** | **string** | &lt;p&gt;Partition source server name.&lt;/p&gt; | [optional]  |
| **applicationName2** | **string** | &lt;p&gt;Partition source application name.&lt;/p&gt; | [optional]  |
| **databaseName2** | **string** | &lt;p&gt;Partition source database name.&lt;/p&gt; | [optional]  |
| **datasourceName** | **string** | &lt;p&gt;Datasource name.&lt;/p&gt; | [optional]  |
| **measuresDimensionName** | **string** | &lt;p&gt;Measures dimension name.&lt;/p&gt; | [optional]  |
| **applicationLevelDatasource** | **bool?** | &lt;p&gt;Specify whether the Datasource is defined at application level.&lt;/p&gt; | [optional]  |

### Return type

[**PartitionList**](PartitionList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Partition list or partition details returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get partition results.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="partitiongetsupportedfederatedtypes"></a>
# **PartitionGetSupportedFederatedTypes**
> void PartitionGetSupportedFederatedTypes (string applicationName, string databaseName)

Get Supported Partition Types

<p>Returns supported partition types.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PartitionGetSupportedFederatedTypesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PartitionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Get Supported Partition Types
                apiInstance.PartitionGetSupportedFederatedTypes(applicationName, databaseName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PartitionsApi.PartitionGetSupportedFederatedTypes: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PartitionGetSupportedFederatedTypesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Supported Partition Types
    apiInstance.PartitionGetSupportedFederatedTypesWithHttpInfo(applicationName, databaseName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PartitionsApi.PartitionGetSupportedFederatedTypesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Returned supported partition types successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get supported types.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="partitionlockpartition"></a>
# **PartitionLockPartition**
> void PartitionLockPartition (string applicationName, string databaseName, string type, string serverName = null, string applicationName2 = null, string databaseName2 = null, string datasourceName = null, string measuresDimensionName = null, bool? applicationLevelDatasource = null)

Lock Partition

<p>Locks partition object. For non-federated partitions, both the source and target partition objects are  locked.</p> <p>For federated partitions, the partition type, Datasource name, measures dimension name, and optional application level Datasource are required.</p><p>For transparent or replicated partitions, the source server, source application, and source database are required.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PartitionLockPartitionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PartitionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var type = "TRANSPARENT";  // string | <p>Partition type.</p>
            var serverName = "serverName_example";  // string | <p>Partition source server name.</p> (optional) 
            var applicationName2 = "applicationName_example";  // string | <p>Partition source application name.</p> (optional) 
            var databaseName2 = "databaseName_example";  // string | <p>Partition source database name.</p> (optional) 
            var datasourceName = "datasourceName_example";  // string | <p>Datasource name.</p> (optional) 
            var measuresDimensionName = "measuresDimensionName_example";  // string | <p>Measures dimension name.</p> (optional) 
            var applicationLevelDatasource = true;  // bool? | <p>Specify whether the Datasource is defined at the application level.</p> (optional) 

            try
            {
                // Lock Partition
                apiInstance.PartitionLockPartition(applicationName, databaseName, type, serverName, applicationName2, databaseName2, datasourceName, measuresDimensionName, applicationLevelDatasource);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PartitionsApi.PartitionLockPartition: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PartitionLockPartitionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Lock Partition
    apiInstance.PartitionLockPartitionWithHttpInfo(applicationName, databaseName, type, serverName, applicationName2, databaseName2, datasourceName, measuresDimensionName, applicationLevelDatasource);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PartitionsApi.PartitionLockPartitionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **type** | **string** | &lt;p&gt;Partition type.&lt;/p&gt; |  |
| **serverName** | **string** | &lt;p&gt;Partition source server name.&lt;/p&gt; | [optional]  |
| **applicationName2** | **string** | &lt;p&gt;Partition source application name.&lt;/p&gt; | [optional]  |
| **databaseName2** | **string** | &lt;p&gt;Partition source database name.&lt;/p&gt; | [optional]  |
| **datasourceName** | **string** | &lt;p&gt;Datasource name.&lt;/p&gt; | [optional]  |
| **measuresDimensionName** | **string** | &lt;p&gt;Measures dimension name.&lt;/p&gt; | [optional]  |
| **applicationLevelDatasource** | **bool?** | &lt;p&gt;Specify whether the Datasource is defined at the application level.&lt;/p&gt; | [optional]  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Partition locked successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to lock partition.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="partitionreplicatedatafromsource"></a>
# **PartitionReplicateDataFromSource**
> void PartitionReplicateDataFromSource (string applicationName, string databaseName, string applicationName2, string databaseName2, string serverName = null, string replicateOption = null)

Replicate Data

<p>Replicates data from source to target for a replicated partition.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PartitionReplicateDataFromSourceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PartitionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var applicationName2 = "applicationName_example";  // string | <p>Partition source application name.</p>
            var databaseName2 = "databaseName_example";  // string | <p>Partition source database name.</p>
            var serverName = "serverName_example";  // string | <p>Partition source server name.</p> (optional) 
            var replicateOption = "UPDATED_CELLS";  // string | <p>Replicate data options.</p> (optional)  (default to UPDATED_CELLS)

            try
            {
                // Replicate Data
                apiInstance.PartitionReplicateDataFromSource(applicationName, databaseName, applicationName2, databaseName2, serverName, replicateOption);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PartitionsApi.PartitionReplicateDataFromSource: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PartitionReplicateDataFromSourceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Replicate Data
    apiInstance.PartitionReplicateDataFromSourceWithHttpInfo(applicationName, databaseName, applicationName2, databaseName2, serverName, replicateOption);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PartitionsApi.PartitionReplicateDataFromSourceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **applicationName2** | **string** | &lt;p&gt;Partition source application name.&lt;/p&gt; |  |
| **databaseName2** | **string** | &lt;p&gt;Partition source database name.&lt;/p&gt; |  |
| **serverName** | **string** | &lt;p&gt;Partition source server name.&lt;/p&gt; | [optional]  |
| **replicateOption** | **string** | &lt;p&gt;Replicate data options.&lt;/p&gt; | [optional] [default to UPDATED_CELLS] |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Partition data replicated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to replicate partition data from the source. The partition type may be incorrect (must be a replicated partition).&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="partitionunlockpartition"></a>
# **PartitionUnlockPartition**
> void PartitionUnlockPartition (string applicationName, string databaseName, string type, string serverName = null, string applicationName2 = null, string databaseName2 = null, string datasourceName = null, string measuresDimensionName = null, bool? applicationLevelDatasource = null)

Unlock Partition

<p>Unlocks the partition object. For non-federated partitions, both the source and target partition objects are  unlocked.</p> <p>For federated partitions, the Datasource name, measures dimension name, and optional application level Datasource are required.</p><p>For transparent and replicated partitions, the source server, source application, and source database are required.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PartitionUnlockPartitionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PartitionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var type = "TRANSPARENT";  // string | <p>Partition type.</p>
            var serverName = "serverName_example";  // string | <p>Partition source server name.</p> (optional) 
            var applicationName2 = "applicationName_example";  // string | <p>Partition source application name.</p> (optional) 
            var databaseName2 = "databaseName_example";  // string | <p>Partition source database name.</p> (optional) 
            var datasourceName = "datasourceName_example";  // string | <p>Datasource name.</p> (optional) 
            var measuresDimensionName = "measuresDimensionName_example";  // string | <p>Measures dimension name.</p> (optional) 
            var applicationLevelDatasource = true;  // bool? | <p>Specify whether the Datasource is defined at the application level.</p> (optional) 

            try
            {
                // Unlock Partition
                apiInstance.PartitionUnlockPartition(applicationName, databaseName, type, serverName, applicationName2, databaseName2, datasourceName, measuresDimensionName, applicationLevelDatasource);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PartitionsApi.PartitionUnlockPartition: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PartitionUnlockPartitionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Unlock Partition
    apiInstance.PartitionUnlockPartitionWithHttpInfo(applicationName, databaseName, type, serverName, applicationName2, databaseName2, datasourceName, measuresDimensionName, applicationLevelDatasource);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PartitionsApi.PartitionUnlockPartitionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **type** | **string** | &lt;p&gt;Partition type.&lt;/p&gt; |  |
| **serverName** | **string** | &lt;p&gt;Partition source server name.&lt;/p&gt; | [optional]  |
| **applicationName2** | **string** | &lt;p&gt;Partition source application name.&lt;/p&gt; | [optional]  |
| **databaseName2** | **string** | &lt;p&gt;Partition source database name.&lt;/p&gt; | [optional]  |
| **datasourceName** | **string** | &lt;p&gt;Datasource name.&lt;/p&gt; | [optional]  |
| **measuresDimensionName** | **string** | &lt;p&gt;Measures dimension name.&lt;/p&gt; | [optional]  |
| **applicationLevelDatasource** | **bool?** | &lt;p&gt;Specify whether the Datasource is defined at the application level.&lt;/p&gt; | [optional]  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Partition unlocked successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to unlock partition.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="partitionupdatepartition"></a>
# **PartitionUpdatePartition**
> void PartitionUpdatePartition (string applicationName, string databaseName, PartitionBean body, bool? executeInBackground = null)

Update Partition

Updates an existing partition

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PartitionUpdatePartitionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PartitionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new PartitionBean(); // PartitionBean | <p>Partition information.</p>
            var executeInBackground = false;  // bool? | <p>Execute Analytic View federated partition in background</p> (optional)  (default to false)

            try
            {
                // Update Partition
                apiInstance.PartitionUpdatePartition(applicationName, databaseName, body, executeInBackground);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PartitionsApi.PartitionUpdatePartition: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PartitionUpdatePartitionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Partition
    apiInstance.PartitionUpdatePartitionWithHttpInfo(applicationName, databaseName, body, executeInBackground);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PartitionsApi.PartitionUpdatePartitionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**PartitionBean**](PartitionBean.md) | &lt;p&gt;Partition information.&lt;/p&gt; |  |
| **executeInBackground** | **bool?** | &lt;p&gt;Execute Analytic View federated partition in background&lt;/p&gt; | [optional] [default to false] |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Partition updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update partition.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="partitionvalidatepartition"></a>
# **PartitionValidatePartition**
> void PartitionValidatePartition (string applicationName, string databaseName, PartitionBean body)

Validate Partition

<p>Validates a new or existing partition.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class PartitionValidatePartitionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new PartitionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new PartitionBean(); // PartitionBean | <p>Partition information.</p>

            try
            {
                // Validate Partition
                apiInstance.PartitionValidatePartition(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling PartitionsApi.PartitionValidatePartition: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the PartitionValidatePartitionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Validate Partition
    apiInstance.PartitionValidatePartitionWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling PartitionsApi.PartitionValidatePartitionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**PartitionBean**](PartitionBean.md) | &lt;p&gt;Partition information.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Partition validated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to validate partition.&lt;/p&gt; |  -  |
| **422** | &lt;p&gt;&lt;strong&gt;Unprocessable Entity&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Partition validated with warnings.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

