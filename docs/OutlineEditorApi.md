# EssSharp.Api.OutlineEditorApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**OutlineEditorCopyMembers**](OutlineEditorApi.md#outlineeditorcopymembers) | **POST** /outline/{app}/{cube}/edit/copy |  |
| [**OutlineEditorCreateCurrencyDB**](OutlineEditorApi.md#outlineeditorcreatecurrencydb) | **POST** /outline/{app}/{cube}/edit/currencyDatabase |  |
| [**OutlineEditorDeleteMember**](OutlineEditorApi.md#outlineeditordeletemember) | **DELETE** /outline/{app}/{cube}/edit |  |
| [**OutlineEditorExportAliasTable**](OutlineEditorApi.md#outlineeditorexportaliastable) | **POST** /outline/{app}/{cube}/edit/exportaliastable |  |
| [**OutlineEditorGetAliasTableNames**](OutlineEditorApi.md#outlineeditorgetaliastablenames) | **GET** /outline/{app}/{cube}/edit/settings/aliases |  |
| [**OutlineEditorGetAllSmartList**](OutlineEditorApi.md#outlineeditorgetallsmartlist) | **GET** /outline/{app}/{cube}/edit/settings/smartlist |  |
| [**OutlineEditorGetAncestorsMemberInfo**](OutlineEditorApi.md#outlineeditorgetancestorsmemberinfo) | **GET** /outline/{app}/{cube}/edit/ancestors/{memberUniqueName} |  |
| [**OutlineEditorGetCopyMembersStatus**](OutlineEditorApi.md#outlineeditorgetcopymembersstatus) | **GET** /outline/{app}/{cube}/edit/copy/status |  |
| [**OutlineEditorGetDescendantsCount**](OutlineEditorApi.md#outlineeditorgetdescendantscount) | **GET** /outline/{app}/{cube}/edit/descendantsCount/{memberUniqueName} |  |
| [**OutlineEditorGetHierarchyDetails**](OutlineEditorApi.md#outlineeditorgethierarchydetails) | **GET** /outline/{app}/{cube}/edit/settings/hierarchyDetails |  |
| [**OutlineEditorGetMemberInfo**](OutlineEditorApi.md#outlineeditorgetmemberinfo) | **GET** /outline/{app}/{cube}/edit/{memberUniqueName} |  |
| [**OutlineEditorGetMembers**](OutlineEditorApi.md#outlineeditorgetmembers) | **POST** /outline/{app}/{cube}/edit/members |  |
| [**OutlineEditorGetMembers1**](OutlineEditorApi.md#outlineeditorgetmembers1) | **GET** /outline/{app}/{cube}/edit |  |
| [**OutlineEditorGetOutlineSettings**](OutlineEditorApi.md#outlineeditorgetoutlinesettings) | **GET** /outline/{app}/{cube}/edit/settings/outline |  |
| [**OutlineEditorGetRelatedInfo**](OutlineEditorApi.md#outlineeditorgetrelatedinfo) | **GET** /outline/{app}/{cube}/edit/getRelatedInfo |  |
| [**OutlineEditorGetSharedMembers**](OutlineEditorApi.md#outlineeditorgetsharedmembers) | **GET** /outline/{app}/{cube}/edit/shared |  |
| [**OutlineEditorImportAliasTable**](OutlineEditorApi.md#outlineeditorimportaliastable) | **POST** /outline/{app}/{cube}/edit/importaliastable |  |
| [**OutlineEditorMemberSelection**](OutlineEditorApi.md#outlineeditormemberselection) | **GET** /outline/{app}/{cube}/edit/memberSelection |  |
| [**OutlineEditorOutlineAction**](OutlineEditorApi.md#outlineeditoroutlineaction) | **POST** /outline/{app}/{cube}/edit |  |
| [**OutlineEditorOutlineEdit**](OutlineEditorApi.md#outlineeditoroutlineedit) | **POST** /outline/{app}/{cube}/edit/direct |  |
| [**OutlineEditorSave**](OutlineEditorApi.md#outlineeditorsave) | **PUT** /outline/{app}/{cube}/edit |  |
| [**OutlineEditorSetMOTF**](OutlineEditorApi.md#outlineeditorsetmotf) | **POST** /outline/{app}/{cube}/edit/motf |  |
| [**OutlineEditorUpdateMember**](OutlineEditorApi.md#outlineeditorupdatemember) | **PATCH** /outline/{app}/{cube}/edit |  |
| [**OutlineEditorValidate**](OutlineEditorApi.md#outlineeditorvalidate) | **GET** /outline/{app}/{cube}/edit/validate |  |

<a id="outlineeditorcopymembers"></a>
# **OutlineEditorCopyMembers**
> void OutlineEditorCopyMembers (string app, string cube, string connection = null, string applicationNameForConnection = null, string accept = null, string sourceApp = null, string sourceCube = null, string members = null, string targetParent = null, string targetSibling = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorCopyMembersExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var connection = "connection_example";  // string |  (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string |  (optional) 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")
            var sourceApp = "sourceApp_example";  // string |  (optional) 
            var sourceCube = "sourceCube_example";  // string |  (optional) 
            var members = "members_example";  // string |  (optional) 
            var targetParent = "targetParent_example";  // string |  (optional) 
            var targetSibling = "targetSibling_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorCopyMembers(app, cube, connection, applicationNameForConnection, accept, sourceApp, sourceCube, members, targetParent, targetSibling);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorCopyMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorCopyMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorCopyMembersWithHttpInfo(app, cube, connection, applicationNameForConnection, accept, sourceApp, sourceCube, members, targetParent, targetSibling);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorCopyMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **connection** | **string** |  | [optional]  |
| **applicationNameForConnection** | **string** |  | [optional]  |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |
| **sourceApp** | **string** |  | [optional]  |
| **sourceCube** | **string** |  | [optional]  |
| **members** | **string** |  | [optional]  |
| **targetParent** | **string** |  | [optional]  |
| **targetSibling** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/x-www-form-urlencoded
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorcreatecurrencydb"></a>
# **OutlineEditorCreateCurrencyDB**
> void OutlineEditorCreateCurrencyDB (string app, string cube, string dbname = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorCreateCurrencyDBExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var dbname = "dbname_example";  // string |  (optional) 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineEditorCreateCurrencyDB(app, cube, dbname, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorCreateCurrencyDB: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorCreateCurrencyDBWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorCreateCurrencyDBWithHttpInfo(app, cube, dbname, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorCreateCurrencyDBWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **dbname** | **string** |  | [optional]  |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditordeletemember"></a>
# **OutlineEditorDeleteMember**
> void OutlineEditorDeleteMember (string app, string cube, string accept = null, string members = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorDeleteMemberExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "accept_example";  // string |  (optional) 
            var members = "members_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorDeleteMember(app, cube, accept, members);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorDeleteMember: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorDeleteMemberWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorDeleteMemberWithHttpInfo(app, cube, accept, members);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorDeleteMemberWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **accept** | **string** |  | [optional]  |
| **members** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/x-www-form-urlencoded
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorexportaliastable"></a>
# **OutlineEditorExportAliasTable**
> void OutlineEditorExportAliasTable (string app, string cube, string accept = null, string aliasname = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorExportAliasTableExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")
            var aliasname = "aliasname_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorExportAliasTable(app, cube, accept, aliasname);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorExportAliasTable: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorExportAliasTableWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorExportAliasTableWithHttpInfo(app, cube, accept, aliasname);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorExportAliasTableWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |
| **aliasname** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/x-www-form-urlencoded
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorgetaliastablenames"></a>
# **OutlineEditorGetAliasTableNames**
> void OutlineEditorGetAliasTableNames (string app, string cube, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetAliasTableNamesExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineEditorGetAliasTableNames(app, cube, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetAliasTableNames: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetAliasTableNamesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetAliasTableNamesWithHttpInfo(app, cube, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetAliasTableNamesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorgetallsmartlist"></a>
# **OutlineEditorGetAllSmartList**
> void OutlineEditorGetAllSmartList (string app, string cube, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetAllSmartListExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineEditorGetAllSmartList(app, cube, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetAllSmartList: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetAllSmartListWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetAllSmartListWithHttpInfo(app, cube, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetAllSmartListWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorgetancestorsmemberinfo"></a>
# **OutlineEditorGetAncestorsMemberInfo**
> void OutlineEditorGetAncestorsMemberInfo (string app, string cube, string memberUniqueName, string fields = null, bool? isMbrId = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetAncestorsMemberInfoExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var memberUniqueName = "memberUniqueName_example";  // string | 
            var fields = "fields_example";  // string |  (optional) 
            var isMbrId = false;  // bool? |  (optional)  (default to false)
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorGetAncestorsMemberInfo(app, cube, memberUniqueName, fields, isMbrId, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetAncestorsMemberInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetAncestorsMemberInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetAncestorsMemberInfoWithHttpInfo(app, cube, memberUniqueName, fields, isMbrId, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetAncestorsMemberInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **memberUniqueName** | **string** |  |  |
| **fields** | **string** |  | [optional]  |
| **isMbrId** | **bool?** |  | [optional] [default to false] |
| **accept** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorgetcopymembersstatus"></a>
# **OutlineEditorGetCopyMembersStatus**
> void OutlineEditorGetCopyMembersStatus (string app, string cube, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetCopyMembersStatusExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineEditorGetCopyMembersStatus(app, cube, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetCopyMembersStatus: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetCopyMembersStatusWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetCopyMembersStatusWithHttpInfo(app, cube, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetCopyMembersStatusWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorgetdescendantscount"></a>
# **OutlineEditorGetDescendantsCount**
> void OutlineEditorGetDescendantsCount (string app, string cube, string memberUniqueName, bool? isMbrId = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetDescendantsCountExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var memberUniqueName = "memberUniqueName_example";  // string | 
            var isMbrId = false;  // bool? |  (optional)  (default to false)
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorGetDescendantsCount(app, cube, memberUniqueName, isMbrId, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetDescendantsCount: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetDescendantsCountWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetDescendantsCountWithHttpInfo(app, cube, memberUniqueName, isMbrId, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetDescendantsCountWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **memberUniqueName** | **string** |  |  |
| **isMbrId** | **bool?** |  | [optional] [default to false] |
| **accept** | **string** |  | [optional]  |

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

<a id="outlineeditorgethierarchydetails"></a>
# **OutlineEditorGetHierarchyDetails**
> void OutlineEditorGetHierarchyDetails (string app, string cube)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetHierarchyDetailsExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 

            try
            {
                apiInstance.OutlineEditorGetHierarchyDetails(app, cube);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetHierarchyDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetHierarchyDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetHierarchyDetailsWithHttpInfo(app, cube);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetHierarchyDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorgetmemberinfo"></a>
# **OutlineEditorGetMemberInfo**
> void OutlineEditorGetMemberInfo (string app, string cube, string memberUniqueName, bool? isMbrId = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetMemberInfoExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var memberUniqueName = "memberUniqueName_example";  // string | 
            var isMbrId = false;  // bool? |  (optional)  (default to false)
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorGetMemberInfo(app, cube, memberUniqueName, isMbrId, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetMemberInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetMemberInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetMemberInfoWithHttpInfo(app, cube, memberUniqueName, isMbrId, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetMemberInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **memberUniqueName** | **string** |  |  |
| **isMbrId** | **bool?** |  | [optional] [default to false] |
| **accept** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorgetmembers"></a>
# **OutlineEditorGetMembers**
> void OutlineEditorGetMembers (string app, string cube, string accept = null, OutlineEditorDeleteMemberRequest body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetMembersExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "accept_example";  // string |  (optional) 
            var body = new OutlineEditorDeleteMemberRequest(); // OutlineEditorDeleteMemberRequest |  (optional) 

            try
            {
                apiInstance.OutlineEditorGetMembers(app, cube, accept, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetMembersWithHttpInfo(app, cube, accept, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **accept** | **string** |  | [optional]  |
| **body** | [**OutlineEditorDeleteMemberRequest**](OutlineEditorDeleteMemberRequest.md) |  | [optional]  |

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

<a id="outlineeditorgetmembers1"></a>
# **OutlineEditorGetMembers1**
> void OutlineEditorGetMembers1 (string app, string cube, string keyword = null, string parent = null, string parentUniqueName = null, bool? matchWholeWord = null, string isMbrId = null, int? offset = null, int? limit = null, bool? hideAttrCalc = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetMembers1Example
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var keyword = "keyword_example";  // string |  (optional) 
            var parent = "parent_example";  // string |  (optional) 
            var parentUniqueName = "parentUniqueName_example";  // string |  (optional) 
            var matchWholeWord = false;  // bool? |  (optional)  (default to false)
            var isMbrId = "\"\"";  // string |  (optional)  (default to "")
            var offset = 0;  // int? |  (optional)  (default to 0)
            var limit = 50;  // int? |  (optional)  (default to 50)
            var hideAttrCalc = true;  // bool? |  (optional)  (default to true)
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorGetMembers1(app, cube, keyword, parent, parentUniqueName, matchWholeWord, isMbrId, offset, limit, hideAttrCalc, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetMembers1: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetMembers1WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetMembers1WithHttpInfo(app, cube, keyword, parent, parentUniqueName, matchWholeWord, isMbrId, offset, limit, hideAttrCalc, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetMembers1WithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **keyword** | **string** |  | [optional]  |
| **parent** | **string** |  | [optional]  |
| **parentUniqueName** | **string** |  | [optional]  |
| **matchWholeWord** | **bool?** |  | [optional] [default to false] |
| **isMbrId** | **string** |  | [optional] [default to &quot;&quot;] |
| **offset** | **int?** |  | [optional] [default to 0] |
| **limit** | **int?** |  | [optional] [default to 50] |
| **hideAttrCalc** | **bool?** |  | [optional] [default to true] |
| **accept** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorgetoutlinesettings"></a>
# **OutlineEditorGetOutlineSettings**
> void OutlineEditorGetOutlineSettings (string app, string cube, string expand = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetOutlineSettingsExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var expand = "\"none\"";  // string |  (optional)  (default to "none")
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineEditorGetOutlineSettings(app, cube, expand, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetOutlineSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetOutlineSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetOutlineSettingsWithHttpInfo(app, cube, expand, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetOutlineSettingsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **expand** | **string** |  | [optional] [default to &quot;none&quot;] |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorgetrelatedinfo"></a>
# **OutlineEditorGetRelatedInfo**
> void OutlineEditorGetRelatedInfo (string app, string cube, string memberId = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetRelatedInfoExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var memberId = "memberId_example";  // string |  (optional) 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineEditorGetRelatedInfo(app, cube, memberId, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetRelatedInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetRelatedInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetRelatedInfoWithHttpInfo(app, cube, memberId, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetRelatedInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **memberId** | **string** |  | [optional]  |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorgetsharedmembers"></a>
# **OutlineEditorGetSharedMembers**
> void OutlineEditorGetSharedMembers (string app, string cube, string name = null, string fields = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorGetSharedMembersExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var name = "name_example";  // string |  (optional) 
            var fields = "fields_example";  // string |  (optional) 
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorGetSharedMembers(app, cube, name, fields, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetSharedMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorGetSharedMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorGetSharedMembersWithHttpInfo(app, cube, name, fields, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorGetSharedMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **name** | **string** |  | [optional]  |
| **fields** | **string** |  | [optional]  |
| **accept** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorimportaliastable"></a>
# **OutlineEditorImportAliasTable**
> void OutlineEditorImportAliasTable (string app, string cube, string accept = null, string aliasname = null, string aliasfilename = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorImportAliasTableExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")
            var aliasname = "aliasname_example";  // string |  (optional) 
            var aliasfilename = "aliasfilename_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorImportAliasTable(app, cube, accept, aliasname, aliasfilename);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorImportAliasTable: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorImportAliasTableWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorImportAliasTableWithHttpInfo(app, cube, accept, aliasname, aliasfilename);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorImportAliasTableWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |
| **aliasname** | **string** |  | [optional]  |
| **aliasfilename** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/x-www-form-urlencoded
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditormemberselection"></a>
# **OutlineEditorMemberSelection**
> void OutlineEditorMemberSelection (string app, string cube, string queryType = null, string queryOptions = null, string dimName = null, string memberName = null, string aliasName = null, string namedGen = null, string namedLevel = null, string search = null, string uda = null, string fields = null, int? offset = null, int? limit = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorMemberSelectionExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var queryType = "queryType_example";  // string |  (optional) 
            var queryOptions = "queryOptions_example";  // string |  (optional) 
            var dimName = "dimName_example";  // string |  (optional) 
            var memberName = "memberName_example";  // string |  (optional) 
            var aliasName = "aliasName_example";  // string |  (optional) 
            var namedGen = "namedGen_example";  // string |  (optional) 
            var namedLevel = "namedLevel_example";  // string |  (optional) 
            var search = "search_example";  // string |  (optional) 
            var uda = "uda_example";  // string |  (optional) 
            var fields = "fields_example";  // string |  (optional) 
            var offset = 0;  // int? |  (optional)  (default to 0)
            var limit = 25;  // int? |  (optional)  (default to 25)
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineEditorMemberSelection(app, cube, queryType, queryOptions, dimName, memberName, aliasName, namedGen, namedLevel, search, uda, fields, offset, limit, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorMemberSelection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorMemberSelectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorMemberSelectionWithHttpInfo(app, cube, queryType, queryOptions, dimName, memberName, aliasName, namedGen, namedLevel, search, uda, fields, offset, limit, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorMemberSelectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **queryType** | **string** |  | [optional]  |
| **queryOptions** | **string** |  | [optional]  |
| **dimName** | **string** |  | [optional]  |
| **memberName** | **string** |  | [optional]  |
| **aliasName** | **string** |  | [optional]  |
| **namedGen** | **string** |  | [optional]  |
| **namedLevel** | **string** |  | [optional]  |
| **search** | **string** |  | [optional]  |
| **uda** | **string** |  | [optional]  |
| **fields** | **string** |  | [optional]  |
| **offset** | **int?** |  | [optional] [default to 0] |
| **limit** | **int?** |  | [optional] [default to 25] |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditoroutlineaction"></a>
# **OutlineEditorOutlineAction**
> void OutlineEditorOutlineAction (string app, string cube, string action = null, string accept = null, string info = null, string members = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorOutlineActionExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var action = "\"add\"";  // string |  (optional)  (default to "add")
            var accept = "accept_example";  // string |  (optional) 
            var info = "info_example";  // string |  (optional) 
            var members = "members_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorOutlineAction(app, cube, action, accept, info, members);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorOutlineAction: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorOutlineActionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorOutlineActionWithHttpInfo(app, cube, action, accept, info, members);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorOutlineActionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **action** | **string** |  | [optional] [default to &quot;add&quot;] |
| **accept** | **string** |  | [optional]  |
| **info** | **string** |  | [optional]  |
| **members** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/x-www-form-urlencoded
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditoroutlineedit"></a>
# **OutlineEditorOutlineEdit**
> void OutlineEditorOutlineEdit (string app, string cube, OutlineEditorOutlineEditRequest body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorOutlineEditExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var body = new OutlineEditorOutlineEditRequest(); // OutlineEditorOutlineEditRequest |  (optional) 

            try
            {
                apiInstance.OutlineEditorOutlineEdit(app, cube, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorOutlineEdit: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorOutlineEditWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorOutlineEditWithHttpInfo(app, cube, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorOutlineEditWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **body** | [**OutlineEditorOutlineEditRequest**](OutlineEditorOutlineEditRequest.md) |  | [optional]  |

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

<a id="outlineeditorsave"></a>
# **OutlineEditorSave**
> void OutlineEditorSave (string app, string cube, string action = null, string restructure = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorSaveExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var action = "\"save\"";  // string |  (optional)  (default to "save")
            var restructure = "\"0\"";  // string |  (optional)  (default to "0")
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorSave(app, cube, action, restructure, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorSave: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorSaveWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorSaveWithHttpInfo(app, cube, action, restructure, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorSaveWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **action** | **string** |  | [optional] [default to &quot;save&quot;] |
| **restructure** | **string** |  | [optional] [default to &quot;0&quot;] |
| **accept** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorsetmotf"></a>
# **OutlineEditorSetMOTF**
> void OutlineEditorSetMOTF (string app, string cube, string accept = null, OutlineEditorDeleteMemberRequest body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorSetMOTFExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")
            var body = new OutlineEditorDeleteMemberRequest(); // OutlineEditorDeleteMemberRequest |  (optional) 

            try
            {
                apiInstance.OutlineEditorSetMOTF(app, cube, accept, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorSetMOTF: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorSetMOTFWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorSetMOTFWithHttpInfo(app, cube, accept, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorSetMOTFWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |
| **body** | [**OutlineEditorDeleteMemberRequest**](OutlineEditorDeleteMemberRequest.md) |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorupdatemember"></a>
# **OutlineEditorUpdateMember**
> void OutlineEditorUpdateMember (string app, string cube, string accept = null, string members = null, string changes = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorUpdateMemberExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "accept_example";  // string |  (optional) 
            var members = "members_example";  // string |  (optional) 
            var changes = "changes_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineEditorUpdateMember(app, cube, accept, members, changes);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorUpdateMember: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorUpdateMemberWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorUpdateMemberWithHttpInfo(app, cube, accept, members, changes);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorUpdateMemberWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **accept** | **string** |  | [optional]  |
| **members** | **string** |  | [optional]  |
| **changes** | **string** |  | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/x-www-form-urlencoded
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlineeditorvalidate"></a>
# **OutlineEditorValidate**
> void OutlineEditorValidate (string app, string cube, bool? fetchStatistics = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineEditorValidateExample
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

            var apiInstance = new OutlineEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var fetchStatistics = false;  // bool? |  (optional)  (default to false)
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineEditorValidate(app, cube, fetchStatistics, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorValidate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineEditorValidateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineEditorValidateWithHttpInfo(app, cube, fetchStatistics, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineEditorApi.OutlineEditorValidateWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **fetchStatistics** | **bool?** |  | [optional] [default to false] |
| **accept** | **string** |  | [optional] [default to &quot;application/json&quot;] |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **0** | default response |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

