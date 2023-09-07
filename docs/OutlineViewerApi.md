# EssSharp.Api.OutlineViewerApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**OutlineGetAllSmartList**](OutlineViewerApi.md#outlinegetallsmartlist) | **GET** /outline/{app}/{cube}/settings/smartlist | Get Text Lists |
| [**OutlineGetAncestorsMemberInfo**](OutlineViewerApi.md#outlinegetancestorsmemberinfo) | **GET** /outline/{app}/{cube}/ancestors/{memberUniqueName} | Get Member Ancestors |
| [**OutlineGetDescendantsCount**](OutlineViewerApi.md#outlinegetdescendantscount) | **GET** /outline/{app}/{cube}/descendantsCount/{memberUniqueName} | Get Descendants Count |
| [**OutlineGetMemberInfo**](OutlineViewerApi.md#outlinegetmemberinfo) | **GET** /outline/{app}/{cube}/{memberUniqueName} | Get Member Info |
| [**OutlineGetMembers**](OutlineViewerApi.md#outlinegetmembers) | **GET** /outline/{app}/{cube} | Get Dimensions, Children, or Search |
| [**OutlineGetOutlineXML**](OutlineViewerApi.md#outlinegetoutlinexml) | **POST** /outline/{app}/{cube}/xml | Export Outline to XML |

<a id="outlinegetallsmartlist"></a>
# **OutlineGetAllSmartList**
> void OutlineGetAllSmartList (string app, string cube, string connection = null, string applicationNameForConnection = null, string accept = null)

Get Text Lists

<p>Returns all text lists associated with the database outline.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineGetAllSmartListExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new OutlineViewerApi(config);
            var app = "app_example";  // string | 
            var cube = "cube_example";  // string | 
            var connection = "connection_example";  // string |  (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string |  (optional) 
            var accept = "\"application/json\"";  // string |  (optional)  (default to "application/json")

            try
            {
                // Get Text Lists
                apiInstance.OutlineGetAllSmartList(app, cube, connection, applicationNameForConnection, accept);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineViewerApi.OutlineGetAllSmartList: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineGetAllSmartListWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Text Lists
    apiInstance.OutlineGetAllSmartListWithHttpInfo(app, cube, connection, applicationNameForConnection, accept);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineViewerApi.OutlineGetAllSmartListWithHttpInfo: " + e.Message);
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
| **0** | successful operation |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlinegetancestorsmemberinfo"></a>
# **OutlineGetAncestorsMemberInfo**
> MemberBean OutlineGetAncestorsMemberInfo (string app, string cube, string memberUniqueName, string connection = null, string applicationNameForConnection = null, string fields = null)

Get Member Ancestors

<p>Returns all ancestors of the requested member.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineGetAncestorsMemberInfoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new OutlineViewerApi(config);
            var app = "app_example";  // string | <p>Application name.</p>
            var cube = "cube_example";  // string | <p>Database name.</p>
            var memberUniqueName = "memberUniqueName_example";  // string | <p>Unique member name (fully qualified name).</p>
            var connection = "connection_example";  // string | <p>Essbase connection name.</p> (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string | <p>Application name for connection.</p> (optional) 
            var fields = "fields_example";  // string | <p>Comma-separated list of member properties to fetch.</p> (optional) 

            try
            {
                // Get Member Ancestors
                MemberBean result = apiInstance.OutlineGetAncestorsMemberInfo(app, cube, memberUniqueName, connection, applicationNameForConnection, fields);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineViewerApi.OutlineGetAncestorsMemberInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineGetAncestorsMemberInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Member Ancestors
    ApiResponse<MemberBean> response = apiInstance.OutlineGetAncestorsMemberInfoWithHttpInfo(app, cube, memberUniqueName, connection, applicationNameForConnection, fields);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineViewerApi.OutlineGetAncestorsMemberInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **cube** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **memberUniqueName** | **string** | &lt;p&gt;Unique member name (fully qualified name).&lt;/p&gt; |  |
| **connection** | **string** | &lt;p&gt;Essbase connection name.&lt;/p&gt; | [optional]  |
| **applicationNameForConnection** | **string** | &lt;p&gt;Application name for connection.&lt;/p&gt; | [optional]  |
| **fields** | **string** | &lt;p&gt;Comma-separated list of member properties to fetch.&lt;/p&gt; | [optional]  |

### Return type

[**MemberBean**](MemberBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;List of ancestors returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get ancestors.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlinegetdescendantscount"></a>
# **OutlineGetDescendantsCount**
> int OutlineGetDescendantsCount (string app, string cube, string memberUniqueName, string connection = null, string applicationNameForConnection = null)

Get Descendants Count

<p>Returns descendants count for the requested member.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineGetDescendantsCountExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new OutlineViewerApi(config);
            var app = "app_example";  // string | <p>Application name.</p>
            var cube = "cube_example";  // string | <p>Database name.</p>
            var memberUniqueName = "memberUniqueName_example";  // string | <p>Unique member name (fully qualified name).</p>
            var connection = "connection_example";  // string | <p>Essbase connection name.</p> (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string | <p>Application name for connection.</p> (optional) 

            try
            {
                // Get Descendants Count
                int result = apiInstance.OutlineGetDescendantsCount(app, cube, memberUniqueName, connection, applicationNameForConnection);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineViewerApi.OutlineGetDescendantsCount: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineGetDescendantsCountWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Descendants Count
    ApiResponse<int> response = apiInstance.OutlineGetDescendantsCountWithHttpInfo(app, cube, memberUniqueName, connection, applicationNameForConnection);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineViewerApi.OutlineGetDescendantsCountWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **cube** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **memberUniqueName** | **string** | &lt;p&gt;Unique member name (fully qualified name).&lt;/p&gt; |  |
| **connection** | **string** | &lt;p&gt;Essbase connection name.&lt;/p&gt; | [optional]  |
| **applicationNameForConnection** | **string** | &lt;p&gt;Application name for connection.&lt;/p&gt; | [optional]  |

### Return type

**int**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Descendants count returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get descendants count.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlinegetmemberinfo"></a>
# **OutlineGetMemberInfo**
> MemberBean OutlineGetMemberInfo (string app, string cube, string memberUniqueName, string connection = null, string applicationNameForConnection = null, string fields = null)

Get Member Info

<p>Returns either all member properties, or requested member properties.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineGetMemberInfoExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new OutlineViewerApi(config);
            var app = "app_example";  // string | <p>Application name.</p>
            var cube = "cube_example";  // string | <p>Database name.</p>
            var memberUniqueName = "memberUniqueName_example";  // string | <p>Unique member name (fully qualified name). Can be a member name, a member ID, or an alias. If the member name is non unique (in a duplicate member enabled outline), use a fully qualified member name or use the member ID.</p>
            var connection = "connection_example";  // string | <p>Essbase connection name.</p> (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string | <p>Application name for connection.</p> (optional) 
            var fields = "fields_example";  // string | <p>Comma-separated list of member properties to fetch.</p> (optional) 

            try
            {
                // Get Member Info
                MemberBean result = apiInstance.OutlineGetMemberInfo(app, cube, memberUniqueName, connection, applicationNameForConnection, fields);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineViewerApi.OutlineGetMemberInfo: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineGetMemberInfoWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Member Info
    ApiResponse<MemberBean> response = apiInstance.OutlineGetMemberInfoWithHttpInfo(app, cube, memberUniqueName, connection, applicationNameForConnection, fields);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineViewerApi.OutlineGetMemberInfoWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **cube** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **memberUniqueName** | **string** | &lt;p&gt;Unique member name (fully qualified name). Can be a member name, a member ID, or an alias. If the member name is non unique (in a duplicate member enabled outline), use a fully qualified member name or use the member ID.&lt;/p&gt; |  |
| **connection** | **string** | &lt;p&gt;Essbase connection name.&lt;/p&gt; | [optional]  |
| **applicationNameForConnection** | **string** | &lt;p&gt;Application name for connection.&lt;/p&gt; | [optional]  |
| **fields** | **string** | &lt;p&gt;Comma-separated list of member properties to fetch.&lt;/p&gt; | [optional]  |

### Return type

[**MemberBean**](MemberBean.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Member information retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get member information.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlinegetmembers"></a>
# **OutlineGetMembers**
> DimensionMemberList OutlineGetMembers (string app, string cube, string connection = null, string applicationNameForConnection = null, string keyword = null, bool? matchWholeWord = null, string parent = null, string parentUniqueName = null, string isMbrId = null, string fields = null, int? offset = null, int? limit = null)

Get Dimensions, Children, or Search

<p>Returns a list of dimensions when no parameters are provided. Returns a list of child members when <i>parent</i> or <i>parentUniqueName</i> parameters are provided. Returns search results when a search keyword parameter is used.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineGetMembersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new OutlineViewerApi(config);
            var app = "app_example";  // string | <p>Application name.</p>
            var cube = "cube_example";  // string | <p>Database name.</p>
            var connection = "connection_example";  // string | <p>Essbase connection name.</p> (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string | <p>Application name for connection.</p> (optional) 
            var keyword = "keyword_example";  // string | <p>Keyword to search for member.</p> (optional) 
            var matchWholeWord = false;  // bool? | <p>Match member name with keyword.</p> (optional)  (default to false)
            var parent = "parent_example";  // string | <p>Parent name.</p> (optional) 
            var parentUniqueName = "parentUniqueName_example";  // string | <p>Parent unique name (fully qualified).</p> (optional) 
            var isMbrId = "isMbrId_example";  // string | <p>Parent ID.</p> (optional) 
            var fields = "fields_example";  // string | <p>Comma-separated list of member properties to fetch.</p> (optional) 
            var offset = 0;  // int? | <p>Number of members to omit from the start of the result set.</p> (optional)  (default to 0)
            var limit = 50;  // int? | <p>Maximum number of members to return.</p> (optional)  (default to 50)

            try
            {
                // Get Dimensions, Children, or Search
                DimensionMemberList result = apiInstance.OutlineGetMembers(app, cube, connection, applicationNameForConnection, keyword, matchWholeWord, parent, parentUniqueName, isMbrId, fields, offset, limit);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineViewerApi.OutlineGetMembers: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineGetMembersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Dimensions, Children, or Search
    ApiResponse<DimensionMemberList> response = apiInstance.OutlineGetMembersWithHttpInfo(app, cube, connection, applicationNameForConnection, keyword, matchWholeWord, parent, parentUniqueName, isMbrId, fields, offset, limit);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineViewerApi.OutlineGetMembersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **cube** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **connection** | **string** | &lt;p&gt;Essbase connection name.&lt;/p&gt; | [optional]  |
| **applicationNameForConnection** | **string** | &lt;p&gt;Application name for connection.&lt;/p&gt; | [optional]  |
| **keyword** | **string** | &lt;p&gt;Keyword to search for member.&lt;/p&gt; | [optional]  |
| **matchWholeWord** | **bool?** | &lt;p&gt;Match member name with keyword.&lt;/p&gt; | [optional] [default to false] |
| **parent** | **string** | &lt;p&gt;Parent name.&lt;/p&gt; | [optional]  |
| **parentUniqueName** | **string** | &lt;p&gt;Parent unique name (fully qualified).&lt;/p&gt; | [optional]  |
| **isMbrId** | **string** | &lt;p&gt;Parent ID.&lt;/p&gt; | [optional]  |
| **fields** | **string** | &lt;p&gt;Comma-separated list of member properties to fetch.&lt;/p&gt; | [optional]  |
| **offset** | **int?** | &lt;p&gt;Number of members to omit from the start of the result set.&lt;/p&gt; | [optional] [default to 0] |
| **limit** | **int?** | &lt;p&gt;Maximum number of members to return.&lt;/p&gt; | [optional] [default to 50] |

### Return type

[**DimensionMemberList**](DimensionMemberList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Dimensions, children, or search results returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get members or search results.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="outlinegetoutlinexml"></a>
# **OutlineGetOutlineXML**
> void OutlineGetOutlineXML (string app, string cube, string connection = null, string applicationNameForConnection = null, ExportOptions body = null)

Export Outline to XML

<p>Exports the outline to XML. If tree is true, then aliasTable will be ignored. Tree mode exports only member names. If aliasTable is provided, then only alias values of the members for the specified alias table will be exported.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class OutlineGetOutlineXMLExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new OutlineViewerApi(config);
            var app = "app_example";  // string | Application name.
            var cube = "cube_example";  // string | Database name.
            var connection = "connection_example";  // string | Essbase connection name. (optional) 
            var applicationNameForConnection = "applicationNameForConnection_example";  // string | Application name for connection. (optional) 
            var body = new ExportOptions(); // ExportOptions |  (optional) 

            try
            {
                // Export Outline to XML
                apiInstance.OutlineGetOutlineXML(app, cube, connection, applicationNameForConnection, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling OutlineViewerApi.OutlineGetOutlineXML: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the OutlineGetOutlineXMLWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Export Outline to XML
    apiInstance.OutlineGetOutlineXMLWithHttpInfo(app, cube, connection, applicationNameForConnection, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling OutlineViewerApi.OutlineGetOutlineXMLWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **app** | **string** | Application name. |  |
| **cube** | **string** | Database name. |  |
| **connection** | **string** | Essbase connection name. | [optional]  |
| **applicationNameForConnection** | **string** | Application name for connection. | [optional]  |
| **body** | [**ExportOptions**](ExportOptions.md) |  | [optional]  |

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
| **0** | successful operation |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

