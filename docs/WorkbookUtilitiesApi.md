# EssSharp.Api.WorkbookUtilitiesApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**ExcelUtilitiesAnalyzeDataFormat**](WorkbookUtilitiesApi.md#excelutilitiesanalyzedataformat) | **GET** /excel/utils/dataformat | Get Workbook Format |

<a id="excelutilitiesanalyzedataformat"></a>
# **ExcelUtilitiesAnalyzeDataFormat**
> void ExcelUtilitiesAnalyzeDataFormat (string path)

Get Workbook Format

<p>Returns details about the application workbook: application name, cube name, and whether the workbook's format is structured or unstructured (tabular).</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class ExcelUtilitiesAnalyzeDataFormatExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new WorkbookUtilitiesApi(config);
            var path = "path_example";  // string | <p>Catalog path of the workbook file.</p>

            try
            {
                // Get Workbook Format
                apiInstance.ExcelUtilitiesAnalyzeDataFormat(path);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WorkbookUtilitiesApi.ExcelUtilitiesAnalyzeDataFormat: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the ExcelUtilitiesAnalyzeDataFormatWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Workbook Format
    apiInstance.ExcelUtilitiesAnalyzeDataFormatWithHttpInfo(path);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling WorkbookUtilitiesApi.ExcelUtilitiesAnalyzeDataFormatWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **path** | **string** | &lt;p&gt;Catalog path of the workbook file.&lt;/p&gt; |  |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The workbook information was retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the workbook details. The file path may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

