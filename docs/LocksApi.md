# EssSharp.Api.LocksApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**LocksGetLockedBlocks**](LocksApi.md#locksgetlockedblocks) | **GET** /applications/{applicationName}/databases/{databaseName}/locks/blocks | List Locked Blocks |
| [**LocksGetLockedObjects**](LocksApi.md#locksgetlockedobjects) | **GET** /applications/{applicationName}/databases/{databaseName}/locks/objects | List Locked Objects |
| [**LocksGetLocks**](LocksApi.md#locksgetlocks) | **GET** /applications/{applicationName}/databases/{databaseName}/locks | List Locks |
| [**LocksLockObject**](LocksApi.md#lockslockobject) | **POST** /applications/{applicationName}/databases/{databaseName}/locks/objects/lock | Lock Object |
| [**LocksUnLockBlock**](LocksApi.md#locksunlockblock) | **POST** /applications/{applicationName}/databases/{databaseName}/locks/blocks/unlock | Unlock Block |
| [**LocksUnLockObject**](LocksApi.md#locksunlockobject) | **POST** /applications/{applicationName}/databases/{databaseName}/locks/objects/unlock | Unlock Object |

<a name="locksgetlockedblocks"></a>
# **LocksGetLockedBlocks**
> LockBlockList LocksGetLockedBlocks (string applicationName, string databaseName, int? offset = null, int? limit = null)

List Locked Blocks

<p>Returns all the locked blocks from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocksGetLockedBlocksExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocksApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var offset = 0;  // int? | <p>Number of items to omit from the start of the result set.</p> (optional)  (default to 0)
            var limit = 50;  // int? | Maximum number of blocks to return. Default is 50. (optional)  (default to 50)

            try
            {
                // List Locked Blocks
                LockBlockList result = apiInstance.LocksGetLockedBlocks(applicationName, databaseName, offset, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocksApi.LocksGetLockedBlocks: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocksGetLockedBlocksWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Locked Blocks
    ApiResponse<LockBlockList> response = apiInstance.LocksGetLockedBlocksWithHttpInfo(applicationName, databaseName, offset, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocksApi.LocksGetLockedBlocksWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **offset** | **int?** | &lt;p&gt;Number of items to omit from the start of the result set.&lt;/p&gt; | [optional] [default to 0] |
| **limit** | **int?** | Maximum number of blocks to return. Default is 50. | [optional] [default to 50] |

### Return type

[**LockBlockList**](LockBlockList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Locked blocks returned successfully, including details of locked blocks and links to unlock the objects.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the locked blocks. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="locksgetlockedobjects"></a>
# **LocksGetLockedObjects**
> LockObjectList LocksGetLockedObjects (string applicationName, string databaseName, int? offset = null, int? limit = null)

List Locked Objects

<p>Returns all the locked objects from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocksGetLockedObjectsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocksApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var offset = 0;  // int? | <p>Number of items to omit from the start of the result set. Default value is 0.</p> (optional)  (default to 0)
            var limit = 50;  // int? | <p>Maximum number of objects to return. Default is 50.</p> (optional)  (default to 50)

            try
            {
                // List Locked Objects
                LockObjectList result = apiInstance.LocksGetLockedObjects(applicationName, databaseName, offset, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocksApi.LocksGetLockedObjects: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocksGetLockedObjectsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Locked Objects
    ApiResponse<LockObjectList> response = apiInstance.LocksGetLockedObjectsWithHttpInfo(applicationName, databaseName, offset, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocksApi.LocksGetLockedObjectsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **offset** | **int?** | &lt;p&gt;Number of items to omit from the start of the result set. Default value is 0.&lt;/p&gt; | [optional] [default to 0] |
| **limit** | **int?** | &lt;p&gt;Maximum number of objects to return. Default is 50.&lt;/p&gt; | [optional] [default to 50] |

### Return type

[**LockObjectList**](LockObjectList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Locked objects retrieved successfully. Gives the details of locked objects along with the links to lock/unlock the object.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the locked objects. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="locksgetlocks"></a>
# **LocksGetLocks**
> LockObject LocksGetLocks (string applicationName, string databaseName)

List Locks

<p>Returns links for locked objects and locked blocks from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocksGetLocksExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocksApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // List Locks
                LockObject result = apiInstance.LocksGetLocks(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocksApi.LocksGetLocks: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocksGetLocksWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Locks
    ApiResponse<LockObject> response = apiInstance.LocksGetLocksWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocksApi.LocksGetLocksWithHttpInfo: " + e.Message);
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

[**LockObject**](LockObject.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Locks listed successfully, including links to unlock.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="lockslockobject"></a>
# **LocksLockObject**
> LockObject LocksLockObject (string applicationName, string databaseName, LockObject body)

Lock Object

<p>Locks the object in the specified application and database and returns the details of the locked object.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocksLockObjectExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocksApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new LockObject(); // LockObject | <p>Details of object to be locked.</p>

            try
            {
                // Lock Object
                LockObject result = apiInstance.LocksLockObject(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocksApi.LocksLockObject: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocksLockObjectWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Lock Object
    ApiResponse<LockObject> response = apiInstance.LocksLockObjectWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocksApi.LocksLockObjectWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**LockObject**](LockObject.md) | &lt;p&gt;Details of object to be locked.&lt;/p&gt; |  |

### Return type

[**LockObject**](LockObject.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Object locked successfully. Gives the details of locked object along with the links to lock/unlock the object.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to lock the object. The application, database, or object name may be incorrect, or the object type may be incorrect.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="locksunlockblock"></a>
# **LocksUnLockBlock**
> void LocksUnLockBlock (string applicationName, string databaseName, LockBlock body)

Unlock Block

<p>Unlocks the locked block in the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocksUnLockBlockExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocksApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new LockBlock(); // LockBlock | <p>Block details to be unlocked.</p>

            try
            {
                // Unlock Block
                apiInstance.LocksUnLockBlock(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocksApi.LocksUnLockBlock: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocksUnLockBlockWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Unlock Block
    apiInstance.LocksUnLockBlockWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocksApi.LocksUnLockBlockWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**LockBlock**](LockBlock.md) | &lt;p&gt;Block details to be unlocked.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Block unlocked successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the settings. The application or database name may be incorrect, or the JSON for the username in the block details may be incorrect.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="locksunlockobject"></a>
# **LocksUnLockObject**
> void LocksUnLockObject (string applicationName, string databaseName, LockObject body)

Unlock Object

<p>Unlocks the object in the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class LocksUnLockObjectExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new LocksApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new LockObject(); // LockObject | <p>Details about object to be unlocked.</p>

            try
            {
                // Unlock Object
                apiInstance.LocksUnLockObject(applicationName, databaseName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling LocksApi.LocksUnLockObject: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the LocksUnLockObjectWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Unlock Object
    apiInstance.LocksUnLockObjectWithHttpInfo(applicationName, databaseName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling LocksApi.LocksUnLockObjectWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**LockObject**](LockObject.md) | &lt;p&gt;Details about object to be unlocked.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Object unlocked successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to unlock the object. The application, database, or object name may be incorrect, or the object type may be incorrect.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

