# EssSharp.Api.ServerVariablesApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**VariablesCreateServerVariable**](ServerVariablesApi.md#variablescreateservervariable) | **POST** /variables | Create Server Variable |
| [**VariablesDeleteServerVariable**](ServerVariablesApi.md#variablesdeleteservervariable) | **DELETE** /variables/{variableName} | Delete Server Variable |
| [**VariablesEditServerVariable**](ServerVariablesApi.md#variableseditservervariable) | **PUT** /variables/{variableName} | Update Server Variable |
| [**VariablesGetServerVariable**](ServerVariablesApi.md#variablesgetservervariable) | **GET** /variables/{variableName} | Get Server Variable |
| [**VariablesListServerVariables**](ServerVariablesApi.md#variableslistservervariables) | **GET** /variables | Get Server Variables |

<a name="variablescreateservervariable"></a>
# **VariablesCreateServerVariable**
> Variable VariablesCreateServerVariable (Variable body)

Create Server Variable

<p>Creates a server substitution variable and returns details about it.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class VariablesCreateServerVariableExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServerVariablesApi(config);
            var body = new Variable(); // Variable | <p>Variable details.</p>

            try
            {
                // Create Server Variable
                Variable result = apiInstance.VariablesCreateServerVariable(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServerVariablesApi.VariablesCreateServerVariable: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the VariablesCreateServerVariableWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Server Variable
    ApiResponse<Variable> response = apiInstance.VariablesCreateServerVariableWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServerVariablesApi.VariablesCreateServerVariableWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**Variable**](Variable.md) | &lt;p&gt;Variable details.&lt;/p&gt; |  |

### Return type

[**Variable**](Variable.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Variable created successfully. Includes variable details and links to get, edit, or delete the variable.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create variable. The JSON may be incorrect, or a  variable with that name may already exist.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="variablesdeleteservervariable"></a>
# **VariablesDeleteServerVariable**
> void VariablesDeleteServerVariable (string variableName)

Delete Server Variable

<p>Deletes the given substitution variable from the server.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class VariablesDeleteServerVariableExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServerVariablesApi(config);
            var variableName = "variableName_example";  // string | <p>Variable name.</p>

            try
            {
                // Delete Server Variable
                apiInstance.VariablesDeleteServerVariable(variableName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServerVariablesApi.VariablesDeleteServerVariable: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the VariablesDeleteServerVariableWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete Server Variable
    apiInstance.VariablesDeleteServerVariableWithHttpInfo(variableName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServerVariablesApi.VariablesDeleteServerVariableWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **variableName** | **string** | &lt;p&gt;Variable name.&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Variable deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to delete the variable. The variable name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="variableseditservervariable"></a>
# **VariablesEditServerVariable**
> Variable VariablesEditServerVariable (string variableName, Variable body)

Update Server Variable

<p>Updates a substitution variable on the server, and returns the updated variable.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class VariablesEditServerVariableExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServerVariablesApi(config);
            var variableName = "variableName_example";  // string | <p>Variable name.</p>
            var body = new Variable(); // Variable | <p>Variable details.</p>

            try
            {
                // Update Server Variable
                Variable result = apiInstance.VariablesEditServerVariable(variableName, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServerVariablesApi.VariablesEditServerVariable: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the VariablesEditServerVariableWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Update Server Variable
    ApiResponse<Variable> response = apiInstance.VariablesEditServerVariableWithHttpInfo(variableName, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServerVariablesApi.VariablesEditServerVariableWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **variableName** | **string** | &lt;p&gt;Variable name.&lt;/p&gt; |  |
| **body** | [**Variable**](Variable.md) | &lt;p&gt;Variable details.&lt;/p&gt; |  |

### Return type

[**Variable**](Variable.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Variable updated successfully. Includes variable details and links to get, edit, or delete the variable.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to update the variable. The variable name may be incorrect.&lt;/p&gt; |  -  |
| **415** | &lt;p&gt;&lt;strong&gt;Not Acceptable&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The media type isn&#39;t supported or wasn&#39;t specified.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="variablesgetservervariable"></a>
# **VariablesGetServerVariable**
> Variable VariablesGetServerVariable (string variableName)

Get Server Variable

<p>Returns substitution variable details from the server.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class VariablesGetServerVariableExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServerVariablesApi(config);
            var variableName = "variableName_example";  // string | <p>Variable name.</p>

            try
            {
                // Get Server Variable
                Variable result = apiInstance.VariablesGetServerVariable(variableName);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServerVariablesApi.VariablesGetServerVariable: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the VariablesGetServerVariableWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Server Variable
    ApiResponse<Variable> response = apiInstance.VariablesGetServerVariableWithHttpInfo(variableName);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServerVariablesApi.VariablesGetServerVariableWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **variableName** | **string** | &lt;p&gt;Variable name.&lt;/p&gt; |  |

### Return type

[**Variable**](Variable.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Variable retrieved successfully. Includes variable details and links to get, edit, or delete the variable.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get the variable. The variable name may be incorrect.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="variableslistservervariables"></a>
# **VariablesListServerVariables**
> VariableList VariablesListServerVariables (string all = null)

Get Server Variables

<p>Returns all the substitution variables from the server.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class VariablesListServerVariablesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new ServerVariablesApi(config);
            var all = "\"false\"";  // string |  (optional)  (default to "false")

            try
            {
                // Get Server Variables
                VariableList result = apiInstance.VariablesListServerVariables(all);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling ServerVariablesApi.VariablesListServerVariables: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the VariablesListServerVariablesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Server Variables
    ApiResponse<VariableList> response = apiInstance.VariablesListServerVariablesWithHttpInfo(all);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling ServerVariablesApi.VariablesListServerVariablesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **all** | **string** |  | [optional] [default to &quot;false&quot;] |

### Return type

[**VariableList**](VariableList.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Variables retrieved successfully. Includes variable details and links to get, edit, or delete the variables.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to get variables.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

