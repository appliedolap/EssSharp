# EssSharp.Api.DimensionsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DimensionsEditDimGenerations**](DimensionsApi.md#dimensionseditdimgenerations) | **PUT** /applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/generations/{generationNumber} | Update Generation |
| [**DimensionsEditDimLevels**](DimensionsApi.md#dimensionseditdimlevels) | **PUT** /applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/levels/{levelNumber} | Update Level |
| [**DimensionsGetDimGenerations**](DimensionsApi.md#dimensionsgetdimgenerations) | **GET** /applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/generations/{generationNumber} | Get Generation |
| [**DimensionsGetDimLevels**](DimensionsApi.md#dimensionsgetdimlevels) | **GET** /applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/levels/{levelNumber} | Get Level |
| [**DimensionsListDimGenerations**](DimensionsApi.md#dimensionslistdimgenerations) | **GET** /applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/generations | List Generations |
| [**DimensionsListDimLevels**](DimensionsApi.md#dimensionslistdimlevels) | **GET** /applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/levels | List Levels |
| [**DimensionsListDimensions**](DimensionsApi.md#dimensionslistdimensions) | **GET** /applications/{applicationName}/databases/{databaseName}/dimensions | List Dimensions |

<a id="dimensionseditdimgenerations"></a>
# **DimensionsEditDimGenerations**
> GenerationLevel DimensionsEditDimGenerations (string applicationName, string databaseName, string dimensionName, int generationNumber, GenerationLevel body)

Update Generation

<p>Updates and returns generation details of a dimension, based on the specified generation number, application, and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionsEditDimGenerationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DimensionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var dimensionName = "dimensionName_example";  // string | <p>Dimension name.</p>
            var generationNumber = 56;  // int | <p>Generation number.</p>
            var body = new GenerationLevel(); // GenerationLevel | <p>Generation details.</p>

            try
            {
                // Update Generation
                GenerationLevel result = apiInstance.DimensionsEditDimGenerations(applicationName, databaseName, dimensionName, generationNumber, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DimensionsApi.DimensionsEditDimGenerations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionsEditDimGenerationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Generation
    ApiResponse<GenerationLevel> response = apiInstance.DimensionsEditDimGenerationsWithHttpInfo(applicationName, databaseName, dimensionName, generationNumber, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DimensionsApi.DimensionsEditDimGenerationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **dimensionName** | **string** | &lt;p&gt;Dimension name.&lt;/p&gt; |  |
| **generationNumber** | **int** | &lt;p&gt;Generation number.&lt;/p&gt; |  |
| **body** | [**GenerationLevel**](GenerationLevel.md) | &lt;p&gt;Generation details.&lt;/p&gt; |  |

### Return type

[**GenerationLevel**](GenerationLevel.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Generation details updated successfully, including links to get or edit the generation.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the generation. The application name, database name, dimension name, or generation number may be incorrect.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dimensionseditdimlevels"></a>
# **DimensionsEditDimLevels**
> GenerationLevel DimensionsEditDimLevels (string applicationName, string databaseName, string dimensionName, int levelNumber, GenerationLevel body)

Update Level

<p>Updates and returns level details of a dimension, based on the specified level number, application, and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionsEditDimLevelsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DimensionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var dimensionName = "dimensionName_example";  // string | <p>Dimension name.</p>
            var levelNumber = 56;  // int | <p>Level number.</p>
            var body = new GenerationLevel(); // GenerationLevel | <p>Level details.</p>

            try
            {
                // Update Level
                GenerationLevel result = apiInstance.DimensionsEditDimLevels(applicationName, databaseName, dimensionName, levelNumber, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DimensionsApi.DimensionsEditDimLevels: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionsEditDimLevelsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Level
    ApiResponse<GenerationLevel> response = apiInstance.DimensionsEditDimLevelsWithHttpInfo(applicationName, databaseName, dimensionName, levelNumber, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DimensionsApi.DimensionsEditDimLevelsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **dimensionName** | **string** | &lt;p&gt;Dimension name.&lt;/p&gt; |  |
| **levelNumber** | **int** | &lt;p&gt;Level number.&lt;/p&gt; |  |
| **body** | [**GenerationLevel**](GenerationLevel.md) | &lt;p&gt;Level details.&lt;/p&gt; |  |

### Return type

[**GenerationLevel**](GenerationLevel.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Level details updated successfully, including links to get or edit the level.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the level. The application name, database name, dimension name, or level  number may be incorrect.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dimensionsgetdimgenerations"></a>
# **DimensionsGetDimGenerations**
> GenerationLevel DimensionsGetDimGenerations (string applicationName, string databaseName, string dimensionName, int generationNumber)

Get Generation

<p>Returns generation details of a dimension, based on the specified generation number, application, and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionsGetDimGenerationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DimensionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var dimensionName = "dimensionName_example";  // string | <p>Dimension name.</p>
            var generationNumber = 56;  // int | <p>Generation number.</p>

            try
            {
                // Get Generation
                GenerationLevel result = apiInstance.DimensionsGetDimGenerations(applicationName, databaseName, dimensionName, generationNumber);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DimensionsApi.DimensionsGetDimGenerations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionsGetDimGenerationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Generation
    ApiResponse<GenerationLevel> response = apiInstance.DimensionsGetDimGenerationsWithHttpInfo(applicationName, databaseName, dimensionName, generationNumber);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DimensionsApi.DimensionsGetDimGenerationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **dimensionName** | **string** | &lt;p&gt;Dimension name.&lt;/p&gt; |  |
| **generationNumber** | **int** | &lt;p&gt;Generation number.&lt;/p&gt; |  |

### Return type

[**GenerationLevel**](GenerationLevel.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Generation details returned successfully, including links to get or edit the generation.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get generation. The application name, database name, dimension name, or generation number may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dimensionsgetdimlevels"></a>
# **DimensionsGetDimLevels**
> GenerationLevel DimensionsGetDimLevels (string applicationName, string databaseName, string dimensionName, int levelNumber)

Get Level

<p>Returns level details of a dimension, based on the specified level number, application, and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionsGetDimLevelsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DimensionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var dimensionName = "dimensionName_example";  // string | <p>Dimension name.</p>
            var levelNumber = 56;  // int | <p>Level number.</p>

            try
            {
                // Get Level
                GenerationLevel result = apiInstance.DimensionsGetDimLevels(applicationName, databaseName, dimensionName, levelNumber);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DimensionsApi.DimensionsGetDimLevels: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionsGetDimLevelsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Level
    ApiResponse<GenerationLevel> response = apiInstance.DimensionsGetDimLevelsWithHttpInfo(applicationName, databaseName, dimensionName, levelNumber);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DimensionsApi.DimensionsGetDimLevelsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **dimensionName** | **string** | &lt;p&gt;Dimension name.&lt;/p&gt; |  |
| **levelNumber** | **int** | &lt;p&gt;Level number.&lt;/p&gt; |  |

### Return type

[**GenerationLevel**](GenerationLevel.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Level details returned successfully, including links to get or edit the level.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get level. The application name, database name, dimension name, or level number may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dimensionslistdimgenerations"></a>
# **DimensionsListDimGenerations**
> GenerationLevelList DimensionsListDimGenerations (string applicationName, string databaseName, string dimensionName)

List Generations

<p>Returns all the generations of a dimension from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionsListDimGenerationsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DimensionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var dimensionName = "dimensionName_example";  // string | <p>Dimension name.</p>

            try
            {
                // List Generations
                GenerationLevelList result = apiInstance.DimensionsListDimGenerations(applicationName, databaseName, dimensionName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DimensionsApi.DimensionsListDimGenerations: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionsListDimGenerationsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Generations
    ApiResponse<GenerationLevelList> response = apiInstance.DimensionsListDimGenerationsWithHttpInfo(applicationName, databaseName, dimensionName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DimensionsApi.DimensionsListDimGenerationsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **dimensionName** | **string** | &lt;p&gt;Dimension name.&lt;/p&gt; |  |

### Return type

[**GenerationLevelList**](GenerationLevelList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Generations retrieved successfully, including links to get or edit each generation.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the generations. The application, database, or dimension name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dimensionslistdimlevels"></a>
# **DimensionsListDimLevels**
> GenerationLevelList DimensionsListDimLevels (string applicationName, string databaseName, string dimensionName)

List Levels

<p>Returns all the levels of a dimension from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionsListDimLevelsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DimensionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var dimensionName = "dimensionName_example";  // string | <p>Dimension name.</p>

            try
            {
                // List Levels
                GenerationLevelList result = apiInstance.DimensionsListDimLevels(applicationName, databaseName, dimensionName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DimensionsApi.DimensionsListDimLevels: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionsListDimLevelsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Levels
    ApiResponse<GenerationLevelList> response = apiInstance.DimensionsListDimLevelsWithHttpInfo(applicationName, databaseName, dimensionName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DimensionsApi.DimensionsListDimLevelsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **dimensionName** | **string** | &lt;p&gt;Dimension name.&lt;/p&gt; |  |

### Return type

[**GenerationLevelList**](GenerationLevelList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Levels retrieved successfully, including links to get or edit each level.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the levels. The application, database, or dimension name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="dimensionslistdimensions"></a>
# **DimensionsListDimensions**
> DimensionList DimensionsListDimensions (string applicationName, string databaseName)

List Dimensions

<p>Returns all the dimensions from the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DimensionsListDimensionsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DimensionsApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // List Dimensions
                DimensionList result = apiInstance.DimensionsListDimensions(applicationName, databaseName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DimensionsApi.DimensionsListDimensions: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DimensionsListDimensionsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Dimensions
    ApiResponse<DimensionList> response = apiInstance.DimensionsListDimensionsWithHttpInfo(applicationName, databaseName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DimensionsApi.DimensionsListDimensionsWithHttpInfo: " + e.Message);
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

[**DimensionList**](DimensionList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Dimensions retrieved successfully, including details about the dimensions, and links to get the generations and levels of each dimension.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the dimensions. The application or database name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

