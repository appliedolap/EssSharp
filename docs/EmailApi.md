# EssSharp.Api.EmailApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**EmailAddIMAPDetails**](EmailApi.md#emailaddimapdetails) | **PUT** /email/imap | Add IMAP Configuration |
| [**EmailAddSMTPDetails**](EmailApi.md#emailaddsmtpdetails) | **PUT** /email/smtp | Add SMTP Configuration |
| [**EmailDeleteIMAPDetails**](EmailApi.md#emaildeleteimapdetails) | **DELETE** /email/imap | Delete IMAP Configuration |
| [**EmailDeleteSMTPDetails**](EmailApi.md#emaildeletesmtpdetails) | **DELETE** /email/smtp | Delete SMTP Configuration |
| [**EmailGetEmailLinks**](EmailApi.md#emailgetemaillinks) | **GET** /email | Get E-mail Configuration |
| [**EmailGetIMAPServerDetail**](EmailApi.md#emailgetimapserverdetail) | **GET** /email/imap | Get IMAP Configuration |
| [**EmailGetSMTPServerDetail**](EmailApi.md#emailgetsmtpserverdetail) | **GET** /email/smtp | Get SMTP Configuration |

<a name="emailaddimapdetails"></a>
# **EmailAddIMAPDetails**
> ListLinkResponseObject EmailAddIMAPDetails (ServerConfiguration body = null)

Add IMAP Configuration

<p>Configure IMAP e-mail server information. IMAP is the protocol Essbase uses for sending e-mails related to scenario management workflow and approval.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class EmailAddIMAPDetailsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new EmailApi(config);
            var body = new ServerConfiguration(); // ServerConfiguration | <p>IMAP server configuration details. Provide the host name, port, user e-mail address, and encoded password.</p> (optional) 

            try
            {
                // Add IMAP Configuration
                ListLinkResponseObject result = apiInstance.EmailAddIMAPDetails(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EmailApi.EmailAddIMAPDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EmailAddIMAPDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add IMAP Configuration
    ApiResponse<ListLinkResponseObject> response = apiInstance.EmailAddIMAPDetailsWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EmailApi.EmailAddIMAPDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**ServerConfiguration**](ServerConfiguration.md) | &lt;p&gt;IMAP server configuration details. Provide the host name, port, user e-mail address, and encoded password.&lt;/p&gt; | [optional]  |

### Return type

[**ListLinkResponseObject**](ListLinkResponseObject.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The e-mail configuration was updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user must be a service administrator to execute this request.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="emailaddsmtpdetails"></a>
# **EmailAddSMTPDetails**
> ListLinkResponseObject EmailAddSMTPDetails (ServerConfiguration body = null)

Add SMTP Configuration

<p>Configure SMTP e-mail server information. IMAP is the protocol used for receiving e-mails related to scenario management workflow and approval.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class EmailAddSMTPDetailsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new EmailApi(config);
            var body = new ServerConfiguration(); // ServerConfiguration | <p>SMTP server configuration details. Provide the host name, port, user e-mail address, and encoded password.</p> (optional) 

            try
            {
                // Add SMTP Configuration
                ListLinkResponseObject result = apiInstance.EmailAddSMTPDetails(body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EmailApi.EmailAddSMTPDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EmailAddSMTPDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Add SMTP Configuration
    ApiResponse<ListLinkResponseObject> response = apiInstance.EmailAddSMTPDetailsWithHttpInfo(body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EmailApi.EmailAddSMTPDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**ServerConfiguration**](ServerConfiguration.md) | &lt;p&gt;SMTP server configuration details. Provide the host name, port, user e-mail address, and encoded password.&lt;/p&gt; | [optional]  |

### Return type

[**ListLinkResponseObject**](ListLinkResponseObject.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The e-mail configuration was updated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user must be a service administrator to execute this request.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="emaildeleteimapdetails"></a>
# **EmailDeleteIMAPDetails**
> void EmailDeleteIMAPDetails ()

Delete IMAP Configuration

<p>Deletes the IMAP server configuration details. IMAP is the protocol Essbase uses to send e-mails related to scenario management workflow and approval.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class EmailDeleteIMAPDetailsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new EmailApi(config);

            try
            {
                // Delete IMAP Configuration
                apiInstance.EmailDeleteIMAPDetails();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EmailApi.EmailDeleteIMAPDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EmailDeleteIMAPDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete IMAP Configuration
    apiInstance.EmailDeleteIMAPDetailsWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EmailApi.EmailDeleteIMAPDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The e-mail configuration was deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user must be a service administrator to execute this request.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="emaildeletesmtpdetails"></a>
# **EmailDeleteSMTPDetails**
> void EmailDeleteSMTPDetails ()

Delete SMTP Configuration

<p>Deletes the SMTP server configuration details. SMTP is the protocol Essbase uses for receiving e-mails related to scenario management workflow and approval.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class EmailDeleteSMTPDetailsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new EmailApi(config);

            try
            {
                // Delete SMTP Configuration
                apiInstance.EmailDeleteSMTPDetails();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EmailApi.EmailDeleteSMTPDetails: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EmailDeleteSMTPDetailsWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete SMTP Configuration
    apiInstance.EmailDeleteSMTPDetailsWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EmailApi.EmailDeleteSMTPDetailsWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The e-mail configuration was deleted successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user must be a service administrator to execute this request.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="emailgetemaillinks"></a>
# **EmailGetEmailLinks**
> ListLinkResponseObject EmailGetEmailLinks ()

Get E-mail Configuration

<p>Returns a URL to access the saved SMTP and IMAP server details. SMTP is the protocol Essbase uses for sending e-mails related to scenario management. IMAP is the protocol for receiving e-mails.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class EmailGetEmailLinksExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new EmailApi(config);

            try
            {
                // Get E-mail Configuration
                ListLinkResponseObject result = apiInstance.EmailGetEmailLinks();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EmailApi.EmailGetEmailLinks: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EmailGetEmailLinksWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get E-mail Configuration
    ApiResponse<ListLinkResponseObject> response = apiInstance.EmailGetEmailLinksWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EmailApi.EmailGetEmailLinksWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ListLinkResponseObject**](ListLinkResponseObject.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The e-mail configuration was retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user must be a service administrator to execute this request.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="emailgetimapserverdetail"></a>
# **EmailGetIMAPServerDetail**
> ServerConfiguration EmailGetIMAPServerDetail ()

Get IMAP Configuration

<p>Returns IMAP server configuration details. IMAP is the protocol Essbase uses for sending e-mails related to scenario management workflow and approval.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class EmailGetIMAPServerDetailExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new EmailApi(config);

            try
            {
                // Get IMAP Configuration
                ServerConfiguration result = apiInstance.EmailGetIMAPServerDetail();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EmailApi.EmailGetIMAPServerDetail: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EmailGetIMAPServerDetailWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get IMAP Configuration
    ApiResponse<ServerConfiguration> response = apiInstance.EmailGetIMAPServerDetailWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EmailApi.EmailGetIMAPServerDetailWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ServerConfiguration**](ServerConfiguration.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The e-mail configuration was retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user must be a service administrator to execute this request.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The e-mail configuration is not present.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="emailgetsmtpserverdetail"></a>
# **EmailGetSMTPServerDetail**
> ServerConfiguration EmailGetSMTPServerDetail ()

Get SMTP Configuration

<p>Returns SMTP server configuration details. SMTP is the protocol Essbase uses for receiving e-mails related to scenario management workflow and approval.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class EmailGetSMTPServerDetailExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new EmailApi(config);

            try
            {
                // Get SMTP Configuration
                ServerConfiguration result = apiInstance.EmailGetSMTPServerDetail();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling EmailApi.EmailGetSMTPServerDetail: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the EmailGetSMTPServerDetailWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get SMTP Configuration
    ApiResponse<ServerConfiguration> response = apiInstance.EmailGetSMTPServerDetailWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling EmailApi.EmailGetSMTPServerDetailWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

[**ServerConfiguration**](ServerConfiguration.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The e-mail configuration was retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user must be a service administrator to execute this request.&lt;/p&gt; |  -  |
| **404** | &lt;p&gt;&lt;strong&gt;Not Found&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The e-mail configuration is not present.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

