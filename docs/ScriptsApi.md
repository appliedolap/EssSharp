# EssSharp.Api.ScriptsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ScriptsAddScriptPermission**](ScriptsApi.md#scriptsaddscriptpermission) | **POST** /applications/{applicationName}/databases/{databaseName}/scripts/{scriptName}/permissions | Add Essbase Script Permissions |
| [**ScriptsCopyScript**](ScriptsApi.md#scriptscopyscript) | **POST** /applications/{applicationName}/databases/{databaseName}/scripts/scriptops/copy | Copy Essbase Script |
| [**ScriptsCreateScript**](ScriptsApi.md#scriptscreatescript) | **POST** /applications/{applicationName}/databases/{databaseName}/scripts | Create Essbase Script |
| [**ScriptsDeleteScript**](ScriptsApi.md#scriptsdeletescript) | **DELETE** /applications/{applicationName}/databases/{databaseName}/scripts/{scriptName} | Delete Essbase Script |
| [**ScriptsEditScript**](ScriptsApi.md#scriptseditscript) | **PUT** /applications/{applicationName}/databases/{databaseName}/scripts/{scriptName} | Update Essbase Script |
| [**ScriptsGetRTSVsForScripts**](ScriptsApi.md#scriptsgetrtsvsforscripts) | **GET** /applications/{applicationName}/databases/{databaseName}/scripts/{scriptName}/rtsv | Get Essbase Script RTSVs |
| [**ScriptsGetScript**](ScriptsApi.md#scriptsgetscript) | **GET** /applications/{applicationName}/databases/{databaseName}/scripts/{scriptName} | Get Essbase Script |
| [**ScriptsGetScriptContent**](ScriptsApi.md#scriptsgetscriptcontent) | **GET** /applications/{applicationName}/databases/{databaseName}/scripts/{scriptName}/content | Get Essbase Script Contents |
| [**ScriptsGetScriptPermissions**](ScriptsApi.md#scriptsgetscriptpermissions) | **GET** /applications/{applicationName}/databases/{databaseName}/scripts/{scriptName}/permissions | Get Essbase Script Permissions |
| [**ScriptsListScripts**](ScriptsApi.md#scriptslistscripts) | **GET** /applications/{applicationName}/databases/{databaseName}/scripts | List Essbase Scripts |
| [**ScriptsRemoveScriptPermission**](ScriptsApi.md#scriptsremovescriptpermission) | **DELETE** /applications/{applicationName}/databases/{databaseName}/scripts/{scriptName}/permissions/{userGroupId} | Remove Essbase Script Permissions |
| [**ScriptsRenameScript**](ScriptsApi.md#scriptsrenamescript) | **POST** /applications/{applicationName}/databases/{databaseName}/scripts/scriptops/rename | Rename Essbase Script |
| [**ScriptsValidateScript**](ScriptsApi.md#scriptsvalidatescript) | **POST** /applications/{applicationName}/databases/{databaseName}/scripts/scriptops/validate | Validate Essbase Script |

<a name="scriptsaddscriptpermission"></a>
# **ScriptsAddScriptPermission**
> UserGroupProvisionInfo ScriptsAddScriptPermission (string applicationName, string databaseName, string scriptName, UserGroupProvisionInfo body)

Add Essbase Script Permissions

<p>Adds permissions to the specified script for the specified user or group. Applicable only for calculation scripts.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsAddScriptPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var scriptName = "scriptName_example";  // string | <p>Script name.</p>
            var body = new UserGroupProvisionInfo(); // UserGroupProvisionInfo | <p>User or group details.</p>

            try
            {
                // Add Essbase Script Permissions
                UserGroupProvisionInfo result = apiInstance.ScriptsAddScriptPermission(applicationName, databaseName, scriptName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsAddScriptPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsAddScriptPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add Essbase Script Permissions
    ApiResponse<UserGroupProvisionInfo> response = apiInstance.ScriptsAddScriptPermissionWithHttpInfo(applicationName, databaseName, scriptName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsAddScriptPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **scriptName** | **string** | &lt;p&gt;Script name.&lt;/p&gt; |  |
| **body** | [**UserGroupProvisionInfo**](UserGroupProvisionInfo.md) | &lt;p&gt;User or group details.&lt;/p&gt; |  |

### Return type

[**UserGroupProvisionInfo**](UserGroupProvisionInfo.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script permission added successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to add permissions for the script. The application, database, or script name may be incorrect; the user or group ID may be incorrect; or the specified user or group may not have sufficient privileges.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptscopyscript"></a>
# **ScriptsCopyScript**
> Script ScriptsCopyScript (string applicationName, string databaseName, ScriptCopy body)

Copy Essbase Script

<p>Copies the script in the specified application and database and returns the created script. Applicable only for calculation scripts.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsCopyScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new ScriptCopy(); // ScriptCopy | <p>Script copy details.</p>

            try
            {
                // Copy Essbase Script
                Script result = apiInstance.ScriptsCopyScript(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsCopyScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsCopyScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Copy Essbase Script
    ApiResponse<Script> response = apiInstance.ScriptsCopyScriptWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsCopyScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**ScriptCopy**](ScriptCopy.md) | &lt;p&gt;Script copy details.&lt;/p&gt; |  |

### Return type

[**Script**](Script.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script copied successfully, including script details and links to get, edit, or delete the script and to get its contents.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to copy the script. The application, database, or script name may be incorrect, or the specified script name may already exist.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptscreatescript"></a>
# **ScriptsCreateScript**
> Script ScriptsCreateScript (string applicationName, string databaseName, Script body, string file = null)

Create Essbase Script

<p>Creates the script in the specified application and database and returns the created script.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsCreateScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new Script(); // Script | <p>Script details.</p>
            var file = "\"calc\"";  // string | <p>Type of script file.</p> (optional)  (default to "calc")

            try
            {
                // Create Essbase Script
                Script result = apiInstance.ScriptsCreateScript(applicationName, databaseName, body, file);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsCreateScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsCreateScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Essbase Script
    ApiResponse<Script> response = apiInstance.ScriptsCreateScriptWithHttpInfo(applicationName, databaseName, body, file);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsCreateScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**Script**](Script.md) | &lt;p&gt;Script details.&lt;/p&gt; |  |
| **file** | **string** | &lt;p&gt;Type of script file.&lt;/p&gt; | [optional] [default to &quot;calc&quot;] |

### Return type

[**Script**](Script.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script created successfully, including script details and links to get, edit, or delete the script and to get its contents.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create the script. The application or database name may be incorrect, the JSON for the script may be incorrect, or the specified script name may already exist.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptsdeletescript"></a>
# **ScriptsDeleteScript**
> void ScriptsDeleteScript (string applicationName, string databaseName, string scriptName, string file = null)

Delete Essbase Script

<p>Deletes the specified script in the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsDeleteScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var scriptName = "scriptName_example";  // string | <p>Script name.</p>
            var file = "\"calc\"";  // string | <p>Type of script file.</p> (optional)  (default to "calc")

            try
            {
                // Delete Essbase Script
                apiInstance.ScriptsDeleteScript(applicationName, databaseName, scriptName, file);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsDeleteScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsDeleteScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Essbase Script
    apiInstance.ScriptsDeleteScriptWithHttpInfo(applicationName, databaseName, scriptName, file);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsDeleteScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **scriptName** | **string** | &lt;p&gt;Script name.&lt;/p&gt; |  |
| **file** | **string** | &lt;p&gt;Type of script file.&lt;/p&gt; | [optional] [default to &quot;calc&quot;] |

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
| **204** | &lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete the script. The application, database, or script name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptseditscript"></a>
# **ScriptsEditScript**
> Script ScriptsEditScript (string applicationName, string databaseName, string scriptName, Script body, string file = null)

Update Essbase Script

<p>Updates the specified script in the specified application and database and returns the updated script.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsEditScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var scriptName = "scriptName_example";  // string | <p>Script name.</p>
            var body = new Script(); // Script | <p>Script details.</p>
            var file = "\"calc\"";  // string | <p>Type of script file.</p> (optional)  (default to "calc")

            try
            {
                // Update Essbase Script
                Script result = apiInstance.ScriptsEditScript(applicationName, databaseName, scriptName, body, file);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsEditScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsEditScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Essbase Script
    ApiResponse<Script> response = apiInstance.ScriptsEditScriptWithHttpInfo(applicationName, databaseName, scriptName, body, file);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsEditScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **scriptName** | **string** | &lt;p&gt;Script name.&lt;/p&gt; |  |
| **body** | [**Script**](Script.md) | &lt;p&gt;Script details.&lt;/p&gt; |  |
| **file** | **string** | &lt;p&gt;Type of script file.&lt;/p&gt; | [optional] [default to &quot;calc&quot;] |

### Return type

[**Script**](Script.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: */*


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script updated successfully. Includes script details and links get, edit, or delete the script and to get its contents.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the script. The application or database name may be incorrect, the JSON for the script may be incorrect, or the specified script name may already exist.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptsgetrtsvsforscripts"></a>
# **ScriptsGetRTSVsForScripts**
> List&lt;RTSVList&gt; ScriptsGetRTSVsForScripts (string applicationName, string databaseName, string scriptName)

Get Essbase Script RTSVs

<p>Returns the runtime substitution variables used in the specified script name.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsGetRTSVsForScriptsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var scriptName = "scriptName_example";  // string | <p>Script name.</p>

            try
            {
                // Get Essbase Script RTSVs
                List<RTSVList> result = apiInstance.ScriptsGetRTSVsForScripts(applicationName, databaseName, scriptName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsGetRTSVsForScripts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsGetRTSVsForScriptsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Essbase Script RTSVs
    ApiResponse<List<RTSVList>> response = apiInstance.ScriptsGetRTSVsForScriptsWithHttpInfo(applicationName, databaseName, scriptName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsGetRTSVsForScriptsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **scriptName** | **string** | &lt;p&gt;Script name.&lt;/p&gt; |  |

### Return type

[**List&lt;RTSVList&gt;**](RTSVList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;List of runtime substitution variables returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get variables. The application, database, or script name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptsgetscript"></a>
# **ScriptsGetScript**
> Script ScriptsGetScript (string applicationName, string databaseName, string scriptName, string file = null)

Get Essbase Script

<p>Returns the named script from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsGetScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var scriptName = "scriptName_example";  // string | <p>Script name.</p>
            var file = "\"calc\"";  // string | <p>Type of script file.</p> (optional)  (default to "calc")

            try
            {
                // Get Essbase Script
                Script result = apiInstance.ScriptsGetScript(applicationName, databaseName, scriptName, file);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsGetScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsGetScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Essbase Script
    ApiResponse<Script> response = apiInstance.ScriptsGetScriptWithHttpInfo(applicationName, databaseName, scriptName, file);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsGetScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **scriptName** | **string** | &lt;p&gt;Script name.&lt;/p&gt; |  |
| **file** | **string** | &lt;p&gt;Type of script file.&lt;/p&gt; | [optional] [default to &quot;calc&quot;] |

### Return type

[**Script**](Script.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script returned successfully, including links get, edit, or delete the script and to get its contents.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the script. The application, database, or script name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptsgetscriptcontent"></a>
# **ScriptsGetScriptContent**
> ScriptContent ScriptsGetScriptContent (string applicationName, string databaseName, string scriptName, string file = null)

Get Essbase Script Contents

<p>Returns the contents of the specified script from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsGetScriptContentExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var scriptName = "scriptName_example";  // string | <p>Script name.</p>
            var file = "\"calc\"";  // string | <p>Type of script file.</p> (optional)  (default to "calc")

            try
            {
                // Get Essbase Script Contents
                ScriptContent result = apiInstance.ScriptsGetScriptContent(applicationName, databaseName, scriptName, file);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsGetScriptContent: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsGetScriptContentWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Essbase Script Contents
    ApiResponse<ScriptContent> response = apiInstance.ScriptsGetScriptContentWithHttpInfo(applicationName, databaseName, scriptName, file);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsGetScriptContentWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **scriptName** | **string** | &lt;p&gt;Script name.&lt;/p&gt; |  |
| **file** | **string** | &lt;p&gt;Type of script file.&lt;/p&gt; | [optional] [default to &quot;calc&quot;] |

### Return type

[**ScriptContent**](ScriptContent.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script content retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get script contents. The application, database, or script name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptsgetscriptpermissions"></a>
# **ScriptsGetScriptPermissions**
> List&lt;UserGroupProvisionInfoList&gt; ScriptsGetScriptPermissions (string applicationName, string databaseName, string scriptName)

Get Essbase Script Permissions

<p>Retrieves permissions for the specified script. Applicable only for calculation scripts.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsGetScriptPermissionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var scriptName = "scriptName_example";  // string | <p>Script name.</p>

            try
            {
                // Get Essbase Script Permissions
                List<UserGroupProvisionInfoList> result = apiInstance.ScriptsGetScriptPermissions(applicationName, databaseName, scriptName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsGetScriptPermissions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsGetScriptPermissionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Essbase Script Permissions
    ApiResponse<List<UserGroupProvisionInfoList>> response = apiInstance.ScriptsGetScriptPermissionsWithHttpInfo(applicationName, databaseName, scriptName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsGetScriptPermissionsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **scriptName** | **string** | &lt;p&gt;Script name.&lt;/p&gt; |  |

### Return type

[**List&lt;UserGroupProvisionInfoList&gt;**](UserGroupProvisionInfoList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script permissions retrieved successfully. |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get permissions for the script. The application, database, or script name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptslistscripts"></a>
# **ScriptsListScripts**
> ScriptList ScriptsListScripts (string applicationName, string databaseName, string file = null)

List Essbase Scripts

<p>Returns all the scripts from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsListScriptsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var file = "\"calc\"";  // string | <p>Type of script file.</p> (optional)  (default to "calc")

            try
            {
                // List Essbase Scripts
                ScriptList result = apiInstance.ScriptsListScripts(applicationName, databaseName, file);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsListScripts: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsListScriptsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Essbase Scripts
    ApiResponse<ScriptList> response = apiInstance.ScriptsListScriptsWithHttpInfo(applicationName, databaseName, file);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsListScriptsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **file** | **string** | &lt;p&gt;Type of script file.&lt;/p&gt; | [optional] [default to &quot;calc&quot;] |

### Return type

[**ScriptList**](ScriptList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Scripts retrieved successfully, including script details and links to get, edit, or delete the script and to get its contents.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the scripts. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptsremovescriptpermission"></a>
# **ScriptsRemoveScriptPermission**
> void ScriptsRemoveScriptPermission (string applicationName, string databaseName, string scriptName, string userGroupId, bool group)

Remove Essbase Script Permissions

<p>Removes permissions from the specified script for the specified user or group. Applicable only for calculation scripts.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsRemoveScriptPermissionExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var scriptName = "scriptName_example";  // string | <p>Script name.</p>
            var userGroupId = "userGroupId_example";  // string | <p>Id of the user or group.</p>
            var group = false;  // bool | <p>True if the userGroupId refers to a group.</p> (default to false)

            try
            {
                // Remove Essbase Script Permissions
                apiInstance.ScriptsRemoveScriptPermission(applicationName, databaseName, scriptName, userGroupId, group);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsRemoveScriptPermission: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsRemoveScriptPermissionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Remove Essbase Script Permissions
    apiInstance.ScriptsRemoveScriptPermissionWithHttpInfo(applicationName, databaseName, scriptName, userGroupId, group);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsRemoveScriptPermissionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **scriptName** | **string** | &lt;p&gt;Script name.&lt;/p&gt; |  |
| **userGroupId** | **string** | &lt;p&gt;Id of the user or group.&lt;/p&gt; |  |
| **group** | **bool** | &lt;p&gt;True if the userGroupId refers to a group.&lt;/p&gt; | [default to false] |

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
| **204** | &lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script permission removed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete permissions for the script. The application, database, or script name may be incorrect, or the user or group ID may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptsrenamescript"></a>
# **ScriptsRenameScript**
> Script ScriptsRenameScript (string applicationName, string databaseName, ScriptCopy body)

Rename Essbase Script

<p>Renames the script in the specified application and database and returns the created script. Applicable only for calculation scripts.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsRenameScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new ScriptCopy(); // ScriptCopy | <p>Script rename details.</p>

            try
            {
                // Rename Essbase Script
                Script result = apiInstance.ScriptsRenameScript(applicationName, databaseName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsRenameScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsRenameScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Rename Essbase Script
    ApiResponse<Script> response = apiInstance.ScriptsRenameScriptWithHttpInfo(applicationName, databaseName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsRenameScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**ScriptCopy**](ScriptCopy.md) | &lt;p&gt;Script rename details.&lt;/p&gt; |  |

### Return type

[**Script**](Script.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script renamed successfully. Includes script details and links to get, edit, or delete the script and to get its contents.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to rename the script. The application, database, or script name may be incorrect, or the specified script name may already exist.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="scriptsvalidatescript"></a>
# **ScriptsValidateScript**
> void ScriptsValidateScript (string applicationName, string databaseName, Script body, string file = null)

Validate Essbase Script

<p>Validates the specified script. Applicable only for calculation scripts.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ScriptsValidateScriptExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ScriptsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var body = new Script(); // Script | <p>Script details.</p>
            var file = "\"calc\"";  // string | <p>File.</p> (optional)  (default to "calc")

            try
            {
                // Validate Essbase Script
                apiInstance.ScriptsValidateScript(applicationName, databaseName, body, file);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ScriptsApi.ScriptsValidateScript: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ScriptsValidateScriptWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Validate Essbase Script
    apiInstance.ScriptsValidateScriptWithHttpInfo(applicationName, databaseName, body, file);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ScriptsApi.ScriptsValidateScriptWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**Script**](Script.md) | &lt;p&gt;Script details.&lt;/p&gt; |  |
| **file** | **string** | &lt;p&gt;File.&lt;/p&gt; | [optional] [default to &quot;calc&quot;] |

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
| **200** | &lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Script validated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to validate the script. The application or database name may be incorrect, or the contents may be incomplete for the specified script name.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

