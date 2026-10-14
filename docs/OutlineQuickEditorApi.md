# EssSharp.Api.OutlineQuickEditorApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**OutlineQuickEditorCreateCurrencyDB**](OutlineQuickEditorApi.md#outlinequickeditorcreatecurrencydb) | **POST** /outline/{app}/{cube}/qedit/currencyDatabase |  |
| [**OutlineQuickEditorDeleteMember**](OutlineQuickEditorApi.md#outlinequickeditordeletemember) | **DELETE** /outline/{app}/{cube}/qedit |  |
| [**OutlineQuickEditorGetAliasTableNames**](OutlineQuickEditorApi.md#outlinequickeditorgetaliastablenames) | **GET** /outline/{app}/{cube}/qedit/settings/aliases |  |
| [**OutlineQuickEditorGetAllSmartList**](OutlineQuickEditorApi.md#outlinequickeditorgetallsmartlist) | **GET** /outline/{app}/{cube}/qedit/settings/smartlist |  |
| [**OutlineQuickEditorGetAncestorsMemberInfo**](OutlineQuickEditorApi.md#outlinequickeditorgetancestorsmemberinfo) | **GET** /outline/{app}/{cube}/qedit/ancestors/{memberUniqueName} |  |
| [**OutlineQuickEditorGetDescendantsCount**](OutlineQuickEditorApi.md#outlinequickeditorgetdescendantscount) | **GET** /outline/{app}/{cube}/qedit/descendantsCount/{memberUniqueName} |  |
| [**OutlineQuickEditorGetMemberInfo**](OutlineQuickEditorApi.md#outlinequickeditorgetmemberinfo) | **GET** /outline/{app}/{cube}/qedit/{memberUniqueName} |  |
| [**OutlineQuickEditorGetMembers2**](OutlineQuickEditorApi.md#outlinequickeditorgetmembers2) | **POST** /outline/{app}/{cube}/qedit/members |  |
| [**OutlineQuickEditorGetMembers3**](OutlineQuickEditorApi.md#outlinequickeditorgetmembers3) | **GET** /outline/{app}/{cube}/qedit |  |
| [**OutlineQuickEditorGetOutlineSettings**](OutlineQuickEditorApi.md#outlinequickeditorgetoutlinesettings) | **GET** /outline/{app}/{cube}/qedit/settings/outline |  |
| [**OutlineQuickEditorGetSharedMembers**](OutlineQuickEditorApi.md#outlinequickeditorgetsharedmembers) | **GET** /outline/{app}/{cube}/qedit/shared |  |
| [**OutlineQuickEditorMemberSelection**](OutlineQuickEditorApi.md#outlinequickeditormemberselection) | **GET** /outline/{app}/{cube}/qedit/memberSelection |  |
| [**OutlineQuickEditorOutlineAction**](OutlineQuickEditorApi.md#outlinequickeditoroutlineaction) | **POST** /outline/{app}/{cube}/qedit |  |
| [**OutlineQuickEditorOutlineEdit**](OutlineQuickEditorApi.md#outlinequickeditoroutlineedit) | **POST** /outline/{app}/{cube}/qedit/direct |  |
| [**OutlineQuickEditorSave**](OutlineQuickEditorApi.md#outlinequickeditorsave) | **PUT** /outline/{app}/{cube}/qedit |  |
| [**OutlineQuickEditorSetMOTF**](OutlineQuickEditorApi.md#outlinequickeditorsetmotf) | **POST** /outline/{app}/{cube}/qedit/motf |  |
| [**OutlineQuickEditorUpdateMember**](OutlineQuickEditorApi.md#outlinequickeditorupdatemember) | **PATCH** /outline/{app}/{cube}/qedit |  |
| [**OutlineQuickEditorValidate**](OutlineQuickEditorApi.md#outlinequickeditorvalidate) | **GET** /outline/{app}/{cube}/qedit/validate |  |

<a id="outlinequickeditorcreatecurrencydb"></a>
# **OutlineQuickEditorCreateCurrencyDB**
> void OutlineQuickEditorCreateCurrencyDB (string app, string cube, string dbname = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorCreateCurrencyDBExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var dbname = "dbname_example";  // string |  (optional) 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineQuickEditorCreateCurrencyDB(app, cube, dbname, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorCreateCurrencyDB: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorCreateCurrencyDBWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorCreateCurrencyDBWithHttpInfo(app, cube, dbname, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorCreateCurrencyDBWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditordeletemember"></a>
# **OutlineQuickEditorDeleteMember**
> void OutlineQuickEditorDeleteMember (string app, string cube, string accept = null, string members = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorDeleteMemberExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "accept_example";  // string |  (optional) 
            var members = "members_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorDeleteMember(app, cube, accept, members);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorDeleteMember: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorDeleteMemberWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorDeleteMemberWithHttpInfo(app, cube, accept, members);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorDeleteMemberWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorgetaliastablenames"></a>
# **OutlineQuickEditorGetAliasTableNames**
> void OutlineQuickEditorGetAliasTableNames (string app, string cube, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorGetAliasTableNamesExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineQuickEditorGetAliasTableNames(app, cube, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetAliasTableNames: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorGetAliasTableNamesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorGetAliasTableNamesWithHttpInfo(app, cube, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetAliasTableNamesWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorgetallsmartlist"></a>
# **OutlineQuickEditorGetAllSmartList**
> void OutlineQuickEditorGetAllSmartList (string app, string cube, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorGetAllSmartListExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineQuickEditorGetAllSmartList(app, cube, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetAllSmartList: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorGetAllSmartListWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorGetAllSmartListWithHttpInfo(app, cube, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetAllSmartListWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorgetancestorsmemberinfo"></a>
# **OutlineQuickEditorGetAncestorsMemberInfo**
> void OutlineQuickEditorGetAncestorsMemberInfo (string app, string cube, string memberUniqueName, string fields = null, bool? isMbrId = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorGetAncestorsMemberInfoExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var memberUniqueName = "memberUniqueName_example";  // string | 
            var fields = "fields_example";  // string |  (optional) 
            var isMbrId = false;  // bool? |  (optional)  (default to false)
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorGetAncestorsMemberInfo(app, cube, memberUniqueName, fields, isMbrId, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetAncestorsMemberInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorGetAncestorsMemberInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorGetAncestorsMemberInfoWithHttpInfo(app, cube, memberUniqueName, fields, isMbrId, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetAncestorsMemberInfoWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorgetdescendantscount"></a>
# **OutlineQuickEditorGetDescendantsCount**
> void OutlineQuickEditorGetDescendantsCount (string app, string cube, string memberUniqueName, bool? isMbrId = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorGetDescendantsCountExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var memberUniqueName = "memberUniqueName_example";  // string | 
            var isMbrId = false;  // bool? |  (optional)  (default to false)
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorGetDescendantsCount(app, cube, memberUniqueName, isMbrId, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetDescendantsCount: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorGetDescendantsCountWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorGetDescendantsCountWithHttpInfo(app, cube, memberUniqueName, isMbrId, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetDescendantsCountWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorgetmemberinfo"></a>
# **OutlineQuickEditorGetMemberInfo**
> void OutlineQuickEditorGetMemberInfo (string app, string cube, string memberUniqueName, bool? isMbrId = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorGetMemberInfoExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var memberUniqueName = "memberUniqueName_example";  // string | 
            var isMbrId = false;  // bool? |  (optional)  (default to false)
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorGetMemberInfo(app, cube, memberUniqueName, isMbrId, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetMemberInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorGetMemberInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorGetMemberInfoWithHttpInfo(app, cube, memberUniqueName, isMbrId, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetMemberInfoWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorgetmembers2"></a>
# **OutlineQuickEditorGetMembers2**
> void OutlineQuickEditorGetMembers2 (string app, string cube, string accept = null, OutlineEditorDeleteMemberRequest body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorGetMembers2Example
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "accept_example";  // string |  (optional) 
            var body = new OutlineEditorDeleteMemberRequest(); // OutlineEditorDeleteMemberRequest |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorGetMembers2(app, cube, accept, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetMembers2: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorGetMembers2WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorGetMembers2WithHttpInfo(app, cube, accept, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetMembers2WithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorgetmembers3"></a>
# **OutlineQuickEditorGetMembers3**
> void OutlineQuickEditorGetMembers3 (string app, string cube, string keyword = null, string parent = null, string parentUniqueName = null, string parentId = null, int? offset = null, int? limit = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorGetMembers3Example
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var keyword = "keyword_example";  // string |  (optional) 
            var parent = "parent_example";  // string |  (optional) 
            var parentUniqueName = "parentUniqueName_example";  // string |  (optional) 
            var parentId = "\"\"";  // string |  (optional)  (default to "")
            var offset = 0;  // int? |  (optional)  (default to 0)
            var limit = 50;  // int? |  (optional)  (default to 50)
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorGetMembers3(app, cube, keyword, parent, parentUniqueName, parentId, offset, limit, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetMembers3: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorGetMembers3WithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorGetMembers3WithHttpInfo(app, cube, keyword, parent, parentUniqueName, parentId, offset, limit, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetMembers3WithHttpInfo: " + e.Message);
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
| **parentId** | **string** |  | [optional] [default to &quot;&quot;] |
| **offset** | **int?** |  | [optional] [default to 0] |
| **limit** | **int?** |  | [optional] [default to 50] |
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

<a id="outlinequickeditorgetoutlinesettings"></a>
# **OutlineQuickEditorGetOutlineSettings**
> void OutlineQuickEditorGetOutlineSettings (string app, string cube, string expand = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorGetOutlineSettingsExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var expand = "\"none\"";  // string |  (optional)  (default to "none")
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineQuickEditorGetOutlineSettings(app, cube, expand, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetOutlineSettings: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorGetOutlineSettingsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorGetOutlineSettingsWithHttpInfo(app, cube, expand, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetOutlineSettingsWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorgetsharedmembers"></a>
# **OutlineQuickEditorGetSharedMembers**
> void OutlineQuickEditorGetSharedMembers (string app, string cube, string name = null, string fields = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorGetSharedMembersExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var name = "name_example";  // string |  (optional) 
            var fields = "fields_example";  // string |  (optional) 
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorGetSharedMembers(app, cube, name, fields, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetSharedMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorGetSharedMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorGetSharedMembersWithHttpInfo(app, cube, name, fields, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorGetSharedMembersWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditormemberselection"></a>
# **OutlineQuickEditorMemberSelection**
> void OutlineQuickEditorMemberSelection (string app, string cube, string queryType = null, string queryOptions = null, string dimName = null, string memberName = null, string aliasName = null, string namedGen = null, string namedLevel = null, string search = null, string uda = null, string fields = null, int? offset = null, int? limit = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorMemberSelectionExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
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
                apiInstance.OutlineQuickEditorMemberSelection(app, cube, queryType, queryOptions, dimName, memberName, aliasName, namedGen, namedLevel, search, uda, fields, offset, limit, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorMemberSelection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorMemberSelectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorMemberSelectionWithHttpInfo(app, cube, queryType, queryOptions, dimName, memberName, aliasName, namedGen, namedLevel, search, uda, fields, offset, limit, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorMemberSelectionWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditoroutlineaction"></a>
# **OutlineQuickEditorOutlineAction**
> void OutlineQuickEditorOutlineAction (string app, string cube, string action = null, string accept = null, string info = null, string members = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorOutlineActionExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var action = "\"add\"";  // string |  (optional)  (default to "add")
            var accept = "accept_example";  // string |  (optional) 
            var info = "info_example";  // string |  (optional) 
            var members = "members_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorOutlineAction(app, cube, action, accept, info, members);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorOutlineAction: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorOutlineActionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorOutlineActionWithHttpInfo(app, cube, action, accept, info, members);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorOutlineActionWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditoroutlineedit"></a>
# **OutlineQuickEditorOutlineEdit**
> void OutlineQuickEditorOutlineEdit (string app, string cube, OutlineQuickEditorOutlineEditRequest body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorOutlineEditExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var body = new OutlineQuickEditorOutlineEditRequest(); // OutlineQuickEditorOutlineEditRequest |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorOutlineEdit(app, cube, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorOutlineEdit: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorOutlineEditWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorOutlineEditWithHttpInfo(app, cube, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorOutlineEditWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** |  |  |
| **cube** | **string** |  |  |
| **body** | [**OutlineQuickEditorOutlineEditRequest**](OutlineQuickEditorOutlineEditRequest.md) |  | [optional]  |

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

<a id="outlinequickeditorsave"></a>
# **OutlineQuickEditorSave**
> void OutlineQuickEditorSave (string app, string cube, string action = null, string restructure = null, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorSaveExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var action = "\"save\"";  // string |  (optional)  (default to "save")
            var restructure = "\"0\"";  // string |  (optional)  (default to "0")
            var accept = "accept_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorSave(app, cube, action, restructure, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorSave: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorSaveWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorSaveWithHttpInfo(app, cube, action, restructure, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorSaveWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorsetmotf"></a>
# **OutlineQuickEditorSetMOTF**
> void OutlineQuickEditorSetMOTF (string app, string cube, string accept = null, OutlineEditorDeleteMemberRequest body = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorSetMOTFExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")
            var body = new OutlineEditorDeleteMemberRequest(); // OutlineEditorDeleteMemberRequest |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorSetMOTF(app, cube, accept, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorSetMOTF: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorSetMOTFWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorSetMOTFWithHttpInfo(app, cube, accept, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorSetMOTFWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorupdatemember"></a>
# **OutlineQuickEditorUpdateMember**
> void OutlineQuickEditorUpdateMember (string app, string cube, string accept = null, string members = null, string changes = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorUpdateMemberExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "accept_example";  // string |  (optional) 
            var members = "members_example";  // string |  (optional) 
            var changes = "changes_example";  // string |  (optional) 

            try
            {
                apiInstance.OutlineQuickEditorUpdateMember(app, cube, accept, members, changes);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorUpdateMember: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorUpdateMemberWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorUpdateMemberWithHttpInfo(app, cube, accept, members, changes);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorUpdateMemberWithHttpInfo: " + e.Message);
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

<a id="outlinequickeditorvalidate"></a>
# **OutlineQuickEditorValidate**
> void OutlineQuickEditorValidate (string app, string cube, string accept = null)



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineQuickEditorValidateExample
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

            var apiInstance = new OutlineQuickEditorApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                apiInstance.OutlineQuickEditorValidate(app, cube, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorValidate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineQuickEditorValidateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.OutlineQuickEditorValidateWithHttpInfo(app, cube, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineQuickEditorApi.OutlineQuickEditorValidateWithHttpInfo: " + e.Message);
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

