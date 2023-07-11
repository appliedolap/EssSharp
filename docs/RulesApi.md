# EssSharp.Api.RulesApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**RulesGet**](RulesApi.md#rulesget) | **GET** /utils/rules | Get Rules |
| [**RulesGetPreviewData**](RulesApi.md#rulesgetpreviewdata) | **POST** /utils/rules/preview | Preview Rules Data |
| [**RulesImportRule**](RulesApi.md#rulesimportrule) | **POST** /utils/rules/actions/import | Import Rules |
| [**RulesSave**](RulesApi.md#rulessave) | **POST** /utils/rules | Create Rules |
| [**RulesVerify**](RulesApi.md#rulesverify) | **POST** /utils/rules/verify | Verify Rules (Deprecated) |
| [**RulesVerifyRule**](RulesApi.md#rulesverifyrule) | **POST** /utils/rules/verifyRule | Verify Rules |

<a id="rulesget"></a>
# **RulesGet**
> Rules RulesGet (string path = null)

Get Rules

<p>Gets rules file from file catalog path.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class RulesGetExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new RulesApi(config);
            var path = "path_example";  // string | <p>Catalog path.</p> (optional) 

            try
            {
                // Get Rules
                Rules result = apiInstance.RulesGet(path);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.RulesGet: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RulesGetWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Rules
    ApiResponse<Rules> response = apiInstance.RulesGetWithHttpInfo(path);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling RulesApi.RulesGetWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **path** | **string** | &lt;p&gt;Catalog path.&lt;/p&gt; | [optional]  |

### Return type

[**Rules**](Rules.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Successfully returned rules file.  Response type can be either JSON, XML, or JSON stream, depending on the Accept header. If &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;, the rules are returned in the response body. If &lt;code&gt;Accept&#x3D;&#39;application/octet-stream&#39;&lt;/code&gt;, the rules are returned as a JSON stream.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to import rules file. The catalog path information may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="rulesgetpreviewdata"></a>
# **RulesGetPreviewData**
> RulePreviewOutput RulesGetPreviewData (RulePreviewInput body = null)

Preview Rules Data

<p>Applies a rules file to a two-dimensional array of data, and returns a previewed data load as two-dimensional array.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class RulesGetPreviewDataExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new RulesApi(config);
            var body = new RulePreviewInput(); // RulePreviewInput | <p>Rules file details and input two dimensional array.</p> (optional) 

            try
            {
                // Preview Rules Data
                RulePreviewOutput result = apiInstance.RulesGetPreviewData(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.RulesGetPreviewData: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RulesGetPreviewDataWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Preview Rules Data
    ApiResponse<RulePreviewOutput> response = apiInstance.RulesGetPreviewDataWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling RulesApi.RulesGetPreviewDataWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**RulePreviewInput**](RulePreviewInput.md) | &lt;p&gt;Rules file details and input two dimensional array.&lt;/p&gt; | [optional]  |

### Return type

[**RulePreviewOutput**](RulePreviewOutput.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Data through rules file was previewed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to preview this data through this rules file.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="rulesimportrule"></a>
# **RulesImportRule**
> void RulesImportRule (bool? overwrite = null, FilePathDetail body = null)

Import Rules

<p>Imports a rules file.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class RulesImportRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new RulesApi(config);
            var overwrite = false;  // bool? | <p>Optional overwrite specification. Default value is false, meaning if the rules file already exists, an error is returned. When set to true, if the rule file already exists, it will be overwritten.</p> (optional)  (default to false)
            var body = new FilePathDetail(); // FilePathDetail | <p>File path information: source and destination catalog paths.</p> (optional) 

            try
            {
                // Import Rules
                apiInstance.RulesImportRule(overwrite, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.RulesImportRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RulesImportRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Import Rules
    apiInstance.RulesImportRuleWithHttpInfo(overwrite, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling RulesApi.RulesImportRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **overwrite** | **bool?** | &lt;p&gt;Optional overwrite specification. Default value is false, meaning if the rules file already exists, an error is returned. When set to true, if the rule file already exists, it will be overwritten.&lt;/p&gt; | [optional] [default to false] |
| **body** | [**FilePathDetail**](FilePathDetail.md) | &lt;p&gt;File path information: source and destination catalog paths.&lt;/p&gt; | [optional]  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Rules file imported successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to import rules file. The file path information may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="rulessave"></a>
# **RulesSave**
> void RulesSave (string path = null, bool? overwrite = null, Rules body = null)

Create Rules

<p>Creates a rules file.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class RulesSaveExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new RulesApi(config);
            var path = "path_example";  // string | <p>Optional file catalog path.</p> (optional) 
            var overwrite = false;  // bool? | <p>Optional overwrite specification. Default value is false, meaning if the rules file already exists, an error is returned. When set to true, if the rule file already exists, it will be overwritten.</p> (optional)  (default to false)
            var body = new Rules(); // Rules | <p>Rules file details.</p> (optional) 

            try
            {
                // Create Rules
                apiInstance.RulesSave(path, overwrite, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.RulesSave: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RulesSaveWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Rules
    apiInstance.RulesSaveWithHttpInfo(path, overwrite, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling RulesApi.RulesSaveWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **path** | **string** | &lt;p&gt;Optional file catalog path.&lt;/p&gt; | [optional]  |
| **overwrite** | **bool?** | &lt;p&gt;Optional overwrite specification. Default value is false, meaning if the rules file already exists, an error is returned. When set to true, if the rule file already exists, it will be overwritten.&lt;/p&gt; | [optional] [default to false] |
| **body** | [**Rules**](Rules.md) | &lt;p&gt;Rules file details.&lt;/p&gt; | [optional]  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Rules file created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create rules file. The catalog path information may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="rulesverify"></a>
# **RulesVerify**
> void RulesVerify (Rules body = null)

Verify Rules (Deprecated)

<p>Verifies a rules file.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class RulesVerifyExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new RulesApi(config);
            var body = new Rules(); // Rules | <p>Rules file details.</p> (optional) 

            try
            {
                // Verify Rules (Deprecated)
                apiInstance.RulesVerify(body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.RulesVerify: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RulesVerifyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Verify Rules (Deprecated)
    apiInstance.RulesVerifyWithHttpInfo(body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling RulesApi.RulesVerifyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**Rules**](Rules.md) | &lt;p&gt;Rules file details.&lt;/p&gt; | [optional]  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Rules file verified successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Rules verification failed. Response contains a list of verification errors.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="rulesverifyrule"></a>
# **RulesVerifyRule**
> void RulesVerifyRule (string application = null, string database = null, Rules body = null)

Verify Rules

<p>Verifies a rules file.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class RulesVerifyRuleExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new RulesApi(config);
            var application = "application_example";  // string | <p>Application name.</p> (optional) 
            var database = "database_example";  // string | <p>Database name.</p> (optional) 
            var body = new Rules(); // Rules | <p>Rules file details.</p> (optional) 

            try
            {
                // Verify Rules
                apiInstance.RulesVerifyRule(application, database, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling RulesApi.RulesVerifyRule: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the RulesVerifyRuleWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Verify Rules
    apiInstance.RulesVerifyRuleWithHttpInfo(application, database, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling RulesApi.RulesVerifyRuleWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | &lt;p&gt;Application name.&lt;/p&gt; | [optional]  |
| **database** | **string** | &lt;p&gt;Database name.&lt;/p&gt; | [optional]  |
| **body** | [**Rules**](Rules.md) | &lt;p&gt;Rules file details.&lt;/p&gt; | [optional]  |

### Return type

void (empty response body)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Rules file verified successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Rules verification failed. Response contains a list of verification errors.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

