# EssSharp.Api.DatabaseSizeStatisticsApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**DatabaseSizeStatisticsGetDBSizes**](DatabaseSizeStatisticsApi.md#databasesizestatisticsgetdbsizes) | **GET** /databasesizestatistics | Get Database Size Statistics |

<a id="databasesizestatisticsgetdbsizes"></a>
# **DatabaseSizeStatisticsGetDBSizes**
> DBSizeList DatabaseSizeStatisticsGetDBSizes ()

Get Database Size Statistics

<p>Returns a list of databases and their page and index sizes.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class DatabaseSizeStatisticsGetDBSizesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new DatabaseSizeStatisticsApi(config);

            try
            {
                // Get Database Size Statistics
                DBSizeList result = apiInstance.DatabaseSizeStatisticsGetDBSizes();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling DatabaseSizeStatisticsApi.DatabaseSizeStatisticsGetDBSizes: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the DatabaseSizeStatisticsGetDBSizesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Database Size Statistics
    ApiResponse<DBSizeList> response = apiInstance.DatabaseSizeStatisticsGetDBSizesWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling DatabaseSizeStatisticsApi.DatabaseSizeStatisticsGetDBSizesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**DBSizeList**](DBSizeList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Database size statistics returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get database size statistics.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

