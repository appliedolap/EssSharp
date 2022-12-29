# EssSharp.Api.NamedQueriesApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**NamedqueriesDeleteNamedQuery**](NamedQueriesApi.md#namedqueriesdeletenamedquery) | **DELETE** /applications/{application}/databases/{database}/queries/{query} | Delete MDX Report |
| [**NamedqueriesEditNamedQuery**](NamedQueriesApi.md#namedquerieseditnamedquery) | **PUT** /applications/{application}/databases/{database}/queries/{query} | Update MDX Report |
| [**NamedqueriesGetNamedQueries**](NamedQueriesApi.md#namedqueriesgetnamedqueries) | **GET** /applications/{application}/databases/{database}/queries | List MDX Reports |
| [**NamedqueriesGetNamedQueryDetails**](NamedQueriesApi.md#namedqueriesgetnamedquerydetails) | **GET** /applications/{application}/databases/{database}/queries/{query} | Get MDX Report Details |
| [**NamedqueriesSaveNamedQuery**](NamedQueriesApi.md#namedqueriessavenamedquery) | **POST** /applications/{application}/databases/{database}/queries | Save MDX Report |

<a name="namedqueriesdeletenamedquery"></a>
# **NamedqueriesDeleteNamedQuery**
> void NamedqueriesDeleteNamedQuery (string application, string database, string query)

Delete MDX Report

<p>Delete the MDX report for specified cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class NamedqueriesDeleteNamedQueryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new NamedQueriesApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var query = "query_example";  // string | <p>MDX report name.</p>

            try
            {
                // Delete MDX Report
                apiInstance.NamedqueriesDeleteNamedQuery(application, database, query);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling NamedQueriesApi.NamedqueriesDeleteNamedQuery: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NamedqueriesDeleteNamedQueryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete MDX Report
    apiInstance.NamedqueriesDeleteNamedQueryWithHttpInfo(application, database, query);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling NamedQueriesApi.NamedqueriesDeleteNamedQueryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **query** | **string** | &lt;p&gt;MDX report name.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;MDX report deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete MDX report.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="namedquerieseditnamedquery"></a>
# **NamedqueriesEditNamedQuery**
> NamedQuery NamedqueriesEditNamedQuery (string application, string database, string query, Query body = null)

Update MDX Report

<p>Update the MDX report for the specified cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class NamedqueriesEditNamedQueryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new NamedQueriesApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var query = "query_example";  // string | <p>MDX report name.</p>
            var body = new Query(); // Query | <p>Details of MDX report to be updated.</p> (optional) 

            try
            {
                // Update MDX Report
                NamedQuery result = apiInstance.NamedqueriesEditNamedQuery(application, database, query, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling NamedQueriesApi.NamedqueriesEditNamedQuery: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NamedqueriesEditNamedQueryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update MDX Report
    ApiResponse<NamedQuery> response = apiInstance.NamedqueriesEditNamedQueryWithHttpInfo(application, database, query, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling NamedQueriesApi.NamedqueriesEditNamedQueryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **query** | **string** | &lt;p&gt;MDX report name.&lt;/p&gt; |  |
| **body** | [**Query**](Query.md) | &lt;p&gt;Details of MDX report to be updated.&lt;/p&gt; | [optional]  |

### Return type

[**NamedQuery**](NamedQuery.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;MDX report updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update MDX report.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="namedqueriesgetnamedqueries"></a>
# **NamedqueriesGetNamedQueries**
> Queries NamedqueriesGetNamedQueries (string application, string database)

List MDX Reports

<p>List all the saved MDX reports for the specified cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class NamedqueriesGetNamedQueriesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new NamedQueriesApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>

            try
            {
                // List MDX Reports
                Queries result = apiInstance.NamedqueriesGetNamedQueries(application, database);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling NamedQueriesApi.NamedqueriesGetNamedQueries: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NamedqueriesGetNamedQueriesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List MDX Reports
    ApiResponse<Queries> response = apiInstance.NamedqueriesGetNamedQueriesWithHttpInfo(application, database);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling NamedQueriesApi.NamedqueriesGetNamedQueriesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |

### Return type

[**Queries**](Queries.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;List of MDX reports returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get MDX reports.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="namedqueriesgetnamedquerydetails"></a>
# **NamedqueriesGetNamedQueryDetails**
> NamedQuery NamedqueriesGetNamedQueryDetails (string application, string database, string query)

Get MDX Report Details

<p>Get the details for the specified MDX report.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class NamedqueriesGetNamedQueryDetailsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new NamedQueriesApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var query = "query_example";  // string | <p>MDX report name.</p>

            try
            {
                // Get MDX Report Details
                NamedQuery result = apiInstance.NamedqueriesGetNamedQueryDetails(application, database, query);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling NamedQueriesApi.NamedqueriesGetNamedQueryDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NamedqueriesGetNamedQueryDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get MDX Report Details
    ApiResponse<NamedQuery> response = apiInstance.NamedqueriesGetNamedQueryDetailsWithHttpInfo(application, database, query);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling NamedQueriesApi.NamedqueriesGetNamedQueryDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **query** | **string** | &lt;p&gt;MDX report name.&lt;/p&gt; |  |

### Return type

[**NamedQuery**](NamedQuery.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;MDX report details returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get MDX report details.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="namedqueriessavenamedquery"></a>
# **NamedqueriesSaveNamedQuery**
> NamedQuery NamedqueriesSaveNamedQuery (string application, string database, NamedQuery body = null)

Save MDX Report

<p>Saves an MDX report for the specified cube.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class NamedqueriesSaveNamedQueryExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new NamedQueriesApi(config);
            var application = "application_example";  // string | <p>Application name.</p>
            var database = "database_example";  // string | <p>Database name.</p>
            var body = new NamedQuery(); // NamedQuery | <p>Details of query to be saved.</p> (optional) 

            try
            {
                // Save MDX Report
                NamedQuery result = apiInstance.NamedqueriesSaveNamedQuery(application, database, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling NamedQueriesApi.NamedqueriesSaveNamedQuery: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the NamedqueriesSaveNamedQueryWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Save MDX Report
    ApiResponse<NamedQuery> response = apiInstance.NamedqueriesSaveNamedQueryWithHttpInfo(application, database, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling NamedQueriesApi.NamedqueriesSaveNamedQueryWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **body** | [**NamedQuery**](NamedQuery.md) | &lt;p&gt;Details of query to be saved.&lt;/p&gt; | [optional]  |

### Return type

[**NamedQuery**](NamedQuery.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;MDX report saved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to save MDX report.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

