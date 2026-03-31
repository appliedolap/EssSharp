# EssSharp.Api.AIApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**AIAssociateConnection**](AIApi.md#aiassociateconnection) | **POST** /ai/applications/{applicationName}/connection/{connectionName} | Associate AI connection |
| [**AICreateOCIChatCredentialUsingSingingKey**](AIApi.md#aicreateocichatcredentialusingsingingkey) | **POST** /ai/dbconnection/{dbConnectionName}/chat/credential/signingkey/{credentialName} | Create OCI chat credential using singing key |
| [**AICreateOCIChatProfile**](AIApi.md#aicreateocichatprofile) | **POST** /ai/aiconnection/{aiConnectionName}/chat/profile/{profileName} | Create OCI chat profile |
| [**AICreateOCIVectorCredential**](AIApi.md#aicreateocivectorcredential) | **POST** /ai/dbconnection/{dbConnectionName}/vector/credential/{credentialName} | Create OCI vector credential |
| [**AICreateVectorIndexJob**](AIApi.md#aicreatevectorindexjob) | **POST** /ai/aiconnection/{aiConnectionName}/job/vectorindex | Create a job to build vector index |
| [**AIDissociateConnection**](AIApi.md#aidissociateconnection) | **DELETE** /ai/applications/{applicationName}/connection | Dissociate AI connection |
| [**AIDropOCIChatCredential**](AIApi.md#aidropocichatcredential) | **DELETE** /ai/dbconnection/{dbConnectionName}/chat/credential/{credentialName} | Drop OCI chat credential |
| [**AIDropOCIChatProfile**](AIApi.md#aidropocichatprofile) | **DELETE** /ai/aiconnection/{aiConnectionName}/chat/profile/{profileName} | Drop OCI chat profile |
| [**AIDropOCIVectorCredential**](AIApi.md#aidropocivectorcredential) | **DELETE** /ai/dbconnection/{dbConnectionName}/vector/credential/{credentialName} | Drop OCI vector credential |
| [**AIDropVectorIndexJob**](AIApi.md#aidropvectorindexjob) | **DELETE** /ai/aiconnection/{aiConnectionName}/job/vectorindex | Create a job to drop the vector index |
| [**AIGetConnection**](AIApi.md#aigetconnection) | **GET** /ai/connection | Get AI connection |
| [**AIGetVectorIndex**](AIApi.md#aigetvectorindex) | **GET** /ai/vectorindex | Get vector index |
| [**AIListSampleQueries**](AIApi.md#ailistsamplequeries) | **POST** /ai/applications/{applicationName}/databases/{databaseName}/listSampleQueries | List Sample Queries |
| [**AIMDXGenerator**](AIApi.md#aimdxgenerator) | **POST** /ai/applications/{applicationName}/databases/{databaseName}/mdxgenerator | MDX Generator |
| [**AINNearestNeighbourSearch**](AIApi.md#ainnearestneighboursearch) | **POST** /ai/applications/{applicationName}/databases/{databaseName}/nnearestneighboursearch | N-Nearest Neighbour Search |
| [**AINarrateVectorIndex**](AIApi.md#ainarratevectorindex) | **GET** /ai/aiconnection/{aiConnectionName}/vectorindex/{vectorIndexName}/narrate/{profileName} | AI Narrate |
| [**AIPassThrough**](AIApi.md#aipassthrough) | **POST** /ai/applications/{applicationName}/chat/passThrough | AI pass through |
| [**AISemanticSearch**](AIApi.md#aisemanticsearch) | **POST** /ai/applications/{applicationName}/databases/{databaseName}/semanticsearch | Semantic Search |
| [**AIVectorizationDate**](AIApi.md#aivectorizationdate) | **POST** /ai/applications/{applicationName}/databases/{databaseName}/vectorizationDate | Vectorization Date |
| [**AIVectorizeOutlineJob**](AIApi.md#aivectorizeoutlinejob) | **POST** /ai/applications/{applicationName}/job/vectorize/databases/{databaseName} | Vectorize outline job |

<a id="aiassociateconnection"></a>
# **AIAssociateConnection**
> void AIAssociateConnection (string applicationName, string connectionName)

Associate AI connection

<p>Associate AI connection for the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIAssociateConnectionExample
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

            var apiInstance = new AIApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var connectionName = "connectionName_example";  // string | <p>Connection name.</p>

            try
            {
                // Associate AI connection
                apiInstance.AIAssociateConnection(applicationName, connectionName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIAssociateConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIAssociateConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Associate AI connection
    apiInstance.AIAssociateConnectionWithHttpInfo(applicationName, connectionName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIAssociateConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **connectionName** | **string** | &lt;p&gt;Connection name.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;AI connection associated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to associate AI connection.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicreateocichatcredentialusingsingingkey"></a>
# **AICreateOCIChatCredentialUsingSingingKey**
> void AICreateOCIChatCredentialUsingSingingKey (string dbConnectionName, string credentialName, OCIChatCredentialSigningKeyDTO body)

Create OCI chat credential using singing key

<p>Create OCI chat credential against the specified DB connection using singing key.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AICreateOCIChatCredentialUsingSingingKeyExample
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

            var apiInstance = new AIApi(config);
            var dbConnectionName = "dbConnectionName_example";  // string | <p>DB connection name.</p>
            var credentialName = "credentialName_example";  // string | <p>Credential name.</p>
            var body = new OCIChatCredentialSigningKeyDTO(); // OCIChatCredentialSigningKeyDTO | <p>OCI credential details: singing key.</p>

            try
            {
                // Create OCI chat credential using singing key
                apiInstance.AICreateOCIChatCredentialUsingSingingKey(dbConnectionName, credentialName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AICreateOCIChatCredentialUsingSingingKey: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AICreateOCIChatCredentialUsingSingingKeyWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create OCI chat credential using singing key
    apiInstance.AICreateOCIChatCredentialUsingSingingKeyWithHttpInfo(dbConnectionName, credentialName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AICreateOCIChatCredentialUsingSingingKeyWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dbConnectionName** | **string** | &lt;p&gt;DB connection name.&lt;/p&gt; |  |
| **credentialName** | **string** | &lt;p&gt;Credential name.&lt;/p&gt; |  |
| **body** | [**OCIChatCredentialSigningKeyDTO**](OCIChatCredentialSigningKeyDTO.md) | &lt;p&gt;OCI credential details: singing key.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;OCI chat credential using singing key created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create OCI chat credential using singing key.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicreateocichatprofile"></a>
# **AICreateOCIChatProfile**
> void AICreateOCIChatProfile (string aiConnectionName, string profileName)

Create OCI chat profile

<p>Create OCI chat profile for the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AICreateOCIChatProfileExample
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

            var apiInstance = new AIApi(config);
            var aiConnectionName = "aiConnectionName_example";  // string | <p>AI Connection Name.</p>
            var profileName = "profileName_example";  // string | <p>OCI chat profile name.</p>

            try
            {
                // Create OCI chat profile
                apiInstance.AICreateOCIChatProfile(aiConnectionName, profileName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AICreateOCIChatProfile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AICreateOCIChatProfileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create OCI chat profile
    apiInstance.AICreateOCIChatProfileWithHttpInfo(aiConnectionName, profileName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AICreateOCIChatProfileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **aiConnectionName** | **string** | &lt;p&gt;AI Connection Name.&lt;/p&gt; |  |
| **profileName** | **string** | &lt;p&gt;OCI chat profile name.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;OCI chat profile created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create OCI chat profile.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicreateocivectorcredential"></a>
# **AICreateOCIVectorCredential**
> void AICreateOCIVectorCredential (string dbConnectionName, string credentialName, OCIVectorCredentialDTO body)

Create OCI vector credential

<p>Create OCI vector credential.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AICreateOCIVectorCredentialExample
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

            var apiInstance = new AIApi(config);
            var dbConnectionName = "dbConnectionName_example";  // string | <p>DB Connection name.</p>
            var credentialName = "credentialName_example";  // string | <p>Credential name.</p>
            var body = new OCIVectorCredentialDTO(); // OCIVectorCredentialDTO | <p>OCI vector credential details.</p>

            try
            {
                // Create OCI vector credential
                apiInstance.AICreateOCIVectorCredential(dbConnectionName, credentialName, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AICreateOCIVectorCredential: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AICreateOCIVectorCredentialWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create OCI vector credential
    apiInstance.AICreateOCIVectorCredentialWithHttpInfo(dbConnectionName, credentialName, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AICreateOCIVectorCredentialWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dbConnectionName** | **string** | &lt;p&gt;DB Connection name.&lt;/p&gt; |  |
| **credentialName** | **string** | &lt;p&gt;Credential name.&lt;/p&gt; |  |
| **body** | [**OCIVectorCredentialDTO**](OCIVectorCredentialDTO.md) | &lt;p&gt;OCI vector credential details.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;OCI vector credential created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create OCI vector credential.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aicreatevectorindexjob"></a>
# **AICreateVectorIndexJob**
> void AICreateVectorIndexJob (string aiConnectionName)

Create a job to build vector index

<p>Create a job to build vector index.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AICreateVectorIndexJobExample
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

            var apiInstance = new AIApi(config);
            var aiConnectionName = "aiConnectionName_example";  // string | <p>AI connection name.</p>

            try
            {
                // Create a job to build vector index
                apiInstance.AICreateVectorIndexJob(aiConnectionName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AICreateVectorIndexJob: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AICreateVectorIndexJobWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a job to build vector index
    apiInstance.AICreateVectorIndexJobWithHttpInfo(aiConnectionName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AICreateVectorIndexJobWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **aiConnectionName** | **string** | &lt;p&gt;AI connection name.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Job to build vector index was created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create a job to build vector index.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aidissociateconnection"></a>
# **AIDissociateConnection**
> void AIDissociateConnection (string applicationName)

Dissociate AI connection

<p>Dissociate AI connection for the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIDissociateConnectionExample
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

            var apiInstance = new AIApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>

            try
            {
                // Dissociate AI connection
                apiInstance.AIDissociateConnection(applicationName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIDissociateConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIDissociateConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Dissociate AI connection
    apiInstance.AIDissociateConnectionWithHttpInfo(applicationName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIDissociateConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;AI connection dissociated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to dissociate AI connection.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aidropocichatcredential"></a>
# **AIDropOCIChatCredential**
> void AIDropOCIChatCredential (string dbConnectionName, string credentialName)

Drop OCI chat credential

<p>Drop OCI chat credential for the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIDropOCIChatCredentialExample
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

            var apiInstance = new AIApi(config);
            var dbConnectionName = "dbConnectionName_example";  // string | <p>DB Connection Name.</p>
            var credentialName = "credentialName_example";  // string | <p>Credential Name.</p>

            try
            {
                // Drop OCI chat credential
                apiInstance.AIDropOCIChatCredential(dbConnectionName, credentialName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIDropOCIChatCredential: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIDropOCIChatCredentialWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Drop OCI chat credential
    apiInstance.AIDropOCIChatCredentialWithHttpInfo(dbConnectionName, credentialName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIDropOCIChatCredentialWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dbConnectionName** | **string** | &lt;p&gt;DB Connection Name.&lt;/p&gt; |  |
| **credentialName** | **string** | &lt;p&gt;Credential Name.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;OCI chat credential dropped successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to drop OCI chat credential.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aidropocichatprofile"></a>
# **AIDropOCIChatProfile**
> void AIDropOCIChatProfile (string aiConnectionName, string profileName)

Drop OCI chat profile

<p>Drop OCI chat profile.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIDropOCIChatProfileExample
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

            var apiInstance = new AIApi(config);
            var aiConnectionName = "aiConnectionName_example";  // string | <p>AI Connection Name.</p>
            var profileName = "profileName_example";  // string | <p>OCI chat profile name.</p>

            try
            {
                // Drop OCI chat profile
                apiInstance.AIDropOCIChatProfile(aiConnectionName, profileName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIDropOCIChatProfile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIDropOCIChatProfileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Drop OCI chat profile
    apiInstance.AIDropOCIChatProfileWithHttpInfo(aiConnectionName, profileName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIDropOCIChatProfileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **aiConnectionName** | **string** | &lt;p&gt;AI Connection Name.&lt;/p&gt; |  |
| **profileName** | **string** | &lt;p&gt;OCI chat profile name.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;OCI chat profile dropped successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to drop OCI chat profile.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aidropocivectorcredential"></a>
# **AIDropOCIVectorCredential**
> void AIDropOCIVectorCredential (string dbConnectionName, string credentialName)

Drop OCI vector credential

<p>Drop OCI vector credential for the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIDropOCIVectorCredentialExample
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

            var apiInstance = new AIApi(config);
            var dbConnectionName = "dbConnectionName_example";  // string | <p>DB Connection Name.</p>
            var credentialName = "credentialName_example";  // string | <p>Credential Name.</p>

            try
            {
                // Drop OCI vector credential
                apiInstance.AIDropOCIVectorCredential(dbConnectionName, credentialName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIDropOCIVectorCredential: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIDropOCIVectorCredentialWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Drop OCI vector credential
    apiInstance.AIDropOCIVectorCredentialWithHttpInfo(dbConnectionName, credentialName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIDropOCIVectorCredentialWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **dbConnectionName** | **string** | &lt;p&gt;DB Connection Name.&lt;/p&gt; |  |
| **credentialName** | **string** | &lt;p&gt;Credential Name.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;OCI vector credential dropped successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to drop OCI vector credential.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aidropvectorindexjob"></a>
# **AIDropVectorIndexJob**
> void AIDropVectorIndexJob (string aiConnectionName)

Create a job to drop the vector index

<p>Create a job to drop the vector index.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIDropVectorIndexJobExample
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

            var apiInstance = new AIApi(config);
            var aiConnectionName = "aiConnectionName_example";  // string | <p>AI connection name.</p>

            try
            {
                // Create a job to drop the vector index
                apiInstance.AIDropVectorIndexJob(aiConnectionName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIDropVectorIndexJob: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIDropVectorIndexJobWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create a job to drop the vector index
    apiInstance.AIDropVectorIndexJobWithHttpInfo(aiConnectionName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIDropVectorIndexJobWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **aiConnectionName** | **string** | &lt;p&gt;AI connection name.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Job to drop the vector index was created successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to create a job to drop the vector index.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aigetconnection"></a>
# **AIGetConnection**
> void AIGetConnection (string application = null)

Get AI connection

<p>Get AI connection.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIGetConnectionExample
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

            var apiInstance = new AIApi(config);
            var application = "application_example";  // string | application name (optional) 

            try
            {
                // Get AI connection
                apiInstance.AIGetConnection(application);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIGetConnection: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIGetConnectionWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get AI connection
    apiInstance.AIGetConnectionWithHttpInfo(application);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIGetConnectionWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **application** | **string** | application name | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Retrieved AI connection successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to retrieve AI connection.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aigetvectorindex"></a>
# **AIGetVectorIndex**
> void AIGetVectorIndex ()

Get vector index

<p>Get vector index.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIGetVectorIndexExample
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

            var apiInstance = new AIApi(config);

            try
            {
                // Get vector index
                apiInstance.AIGetVectorIndex();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIGetVectorIndex: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIGetVectorIndexWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get vector index
    apiInstance.AIGetVectorIndexWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIGetVectorIndexWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Retrieved vector index successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to retrieve vector index.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="ailistsamplequeries"></a>
# **AIListSampleQueries**
> void AIListSampleQueries (string applicationName, string databaseName)

List Sample Queries

Returns samples queries for an outline

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIListSampleQueriesExample
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

            var apiInstance = new AIApi(config);
            var applicationName = "applicationName_example";  // string | 
            var databaseName = "databaseName_example";  // string | 

            try
            {
                // List Sample Queries
                apiInstance.AIListSampleQueries(applicationName, databaseName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIListSampleQueries: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIListSampleQueriesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Sample Queries
    apiInstance.AIListSampleQueriesWithHttpInfo(applicationName, databaseName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIListSampleQueriesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** |  |  |
| **databaseName** | **string** |  |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Successfully retrieved sample queries. |  -  |
| **400** | Bad request. Required parameters may be missing or invalid. |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aimdxgenerator"></a>
# **AIMDXGenerator**
> void AIMDXGenerator (string applicationName, string databaseName, string profileName, string nlq, bool includeAttributesInNlq, bool isConvStart, string prompt = null)

MDX Generator

<p>MDX Generator.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIMDXGeneratorExample
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

            var apiInstance = new AIApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var profileName = "profileName_example";  // string | <p>Profile name.</p>
            var nlq = "nlq_example";  // string | <p>NLQ.</p>
            var includeAttributesInNlq = true;  // bool | <p>Include Attributes in NLQ.</p>
            var isConvStart = true;  // bool | <p>Is Conversation Start.</p>
            var prompt = "prompt_example";  // string | <p>Prompt.</p> (optional) 

            try
            {
                // MDX Generator
                apiInstance.AIMDXGenerator(applicationName, databaseName, profileName, nlq, includeAttributesInNlq, isConvStart, prompt);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIMDXGenerator: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIMDXGeneratorWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // MDX Generator
    apiInstance.AIMDXGeneratorWithHttpInfo(applicationName, databaseName, profileName, nlq, includeAttributesInNlq, isConvStart, prompt);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIMDXGeneratorWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **profileName** | **string** | &lt;p&gt;Profile name.&lt;/p&gt; |  |
| **nlq** | **string** | &lt;p&gt;NLQ.&lt;/p&gt; |  |
| **includeAttributesInNlq** | **bool** | &lt;p&gt;Include Attributes in NLQ.&lt;/p&gt; |  |
| **isConvStart** | **bool** | &lt;p&gt;Is Conversation Start.&lt;/p&gt; |  |
| **prompt** | **string** | &lt;p&gt;Prompt.&lt;/p&gt; | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;MDX Generated successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to generate MDX.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="ainnearestneighboursearch"></a>
# **AINNearestNeighbourSearch**
> void AINNearestNeighbourSearch (string applicationName, string databaseName, string responseParam, int topMatches, NNearestNeighbourDTO body, string aliasType = null, double? threshold = null)

N-Nearest Neighbour Search

<p>N-Nearest Neighbour Search.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AINNearestNeighbourSearchExample
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

            var apiInstance = new AIApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var responseParam = "responseParam_example";  // string | <p>Response paramter.</p>
            var topMatches = 1;  // int | <p>Top matches.</p> (default to 1)
            var body = new NNearestNeighbourDTO(); // NNearestNeighbourDTO | <p>N-Nearest neighbour body data.</p>
            var aliasType = "aliasType_example";  // string | <p>Alias Table Name.</p> (optional) 
            var threshold = 1.0D;  // double? | <p>Threshold.</p> (optional)  (default to 1.0D)

            try
            {
                // N-Nearest Neighbour Search
                apiInstance.AINNearestNeighbourSearch(applicationName, databaseName, responseParam, topMatches, body, aliasType, threshold);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AINNearestNeighbourSearch: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AINNearestNeighbourSearchWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // N-Nearest Neighbour Search
    apiInstance.AINNearestNeighbourSearchWithHttpInfo(applicationName, databaseName, responseParam, topMatches, body, aliasType, threshold);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AINNearestNeighbourSearchWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **responseParam** | **string** | &lt;p&gt;Response paramter.&lt;/p&gt; |  |
| **topMatches** | **int** | &lt;p&gt;Top matches.&lt;/p&gt; | [default to 1] |
| **body** | [**NNearestNeighbourDTO**](NNearestNeighbourDTO.md) | &lt;p&gt;N-Nearest neighbour body data.&lt;/p&gt; |  |
| **aliasType** | **string** | &lt;p&gt;Alias Table Name.&lt;/p&gt; | [optional]  |
| **threshold** | **double?** | &lt;p&gt;Threshold.&lt;/p&gt; | [optional] [default to 1.0D] |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;N-Nearest neighbour retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to retrieve n-nearest neighbour.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="ainarratevectorindex"></a>
# **AINarrateVectorIndex**
> void AINarrateVectorIndex (string aiConnectionName, string vectorIndexName, string profileName, string docDirName, string prompt, bool isConvStart)

AI Narrate

<p>AI Narrate.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AINarrateVectorIndexExample
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

            var apiInstance = new AIApi(config);
            var aiConnectionName = "aiConnectionName_example";  // string | <p>AI connection name.</p>
            var vectorIndexName = "vectorIndexName_example";  // string | <p>Vector index name.</p>
            var profileName = "profileName_example";  // string | <p>Profile name.</p>
            var docDirName = "docDirName_example";  // string | <p>Name of directory where all the doc files are kept.</p>
            var prompt = "prompt_example";  // string | <p>Prompt.</p>
            var isConvStart = true;  // bool | <p>Is Conversation Start.</p>

            try
            {
                // AI Narrate
                apiInstance.AINarrateVectorIndex(aiConnectionName, vectorIndexName, profileName, docDirName, prompt, isConvStart);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AINarrateVectorIndex: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AINarrateVectorIndexWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // AI Narrate
    apiInstance.AINarrateVectorIndexWithHttpInfo(aiConnectionName, vectorIndexName, profileName, docDirName, prompt, isConvStart);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AINarrateVectorIndexWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **aiConnectionName** | **string** | &lt;p&gt;AI connection name.&lt;/p&gt; |  |
| **vectorIndexName** | **string** | &lt;p&gt;Vector index name.&lt;/p&gt; |  |
| **profileName** | **string** | &lt;p&gt;Profile name.&lt;/p&gt; |  |
| **docDirName** | **string** | &lt;p&gt;Name of directory where all the doc files are kept.&lt;/p&gt; |  |
| **prompt** | **string** | &lt;p&gt;Prompt.&lt;/p&gt; |  |
| **isConvStart** | **bool** | &lt;p&gt;Is Conversation Start.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;AI Narrate successful.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to perform AI Narrate.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aipassthrough"></a>
# **AIPassThrough**
> void AIPassThrough (string applicationName, string profileName, bool isConvStart, PassThroughDTO body)

AI pass through

<p>AI pass through for the specified application.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIPassThroughExample
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

            var apiInstance = new AIApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var profileName = "profileName_example";  // string | <p>Profile name.</p>
            var isConvStart = true;  // bool | <p>Is Conversation Start.</p>
            var body = new PassThroughDTO(); // PassThroughDTO | <p>Pass through prompt details.</p>

            try
            {
                // AI pass through
                apiInstance.AIPassThrough(applicationName, profileName, isConvStart, body);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIPassThrough: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIPassThroughWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // AI pass through
    apiInstance.AIPassThroughWithHttpInfo(applicationName, profileName, isConvStart, body);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIPassThroughWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **profileName** | **string** | &lt;p&gt;Profile name.&lt;/p&gt; |  |
| **isConvStart** | **bool** | &lt;p&gt;Is Conversation Start.&lt;/p&gt; |  |
| **body** | [**PassThroughDTO**](PassThroughDTO.md) | &lt;p&gt;Pass through prompt details.&lt;/p&gt; |  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Pass through was successful.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to perform pass through.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aisemanticsearch"></a>
# **AISemanticSearch**
> void AISemanticSearch (string applicationName, string databaseName, string responseParam, string nlq, string profileName, bool isConvStart, string aliasType = null)

Semantic Search

<p>Semantic Search.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AISemanticSearchExample
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

            var apiInstance = new AIApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>
            var responseParam = "responseParam_example";  // string | <p>Response Parameter.</p>
            var nlq = "nlq_example";  // string | <p>NLQ.</p>
            var profileName = "profileName_example";  // string | <p>Profile Name.</p>
            var isConvStart = true;  // bool | <p>Is Conversation Start.</p>
            var aliasType = "aliasType_example";  // string | <p>Alias Table Name.</p> (optional) 

            try
            {
                // Semantic Search
                apiInstance.AISemanticSearch(applicationName, databaseName, responseParam, nlq, profileName, isConvStart, aliasType);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AISemanticSearch: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AISemanticSearchWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Semantic Search
    apiInstance.AISemanticSearchWithHttpInfo(applicationName, databaseName, responseParam, nlq, profileName, isConvStart, aliasType);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AISemanticSearchWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **applicationName** | **string** | &lt;p&gt;Application name.&lt;/p&gt; |  |
| **databaseName** | **string** | &lt;p&gt;Database name.&lt;/p&gt; |  |
| **responseParam** | **string** | &lt;p&gt;Response Parameter.&lt;/p&gt; |  |
| **nlq** | **string** | &lt;p&gt;NLQ.&lt;/p&gt; |  |
| **profileName** | **string** | &lt;p&gt;Profile Name.&lt;/p&gt; |  |
| **isConvStart** | **bool** | &lt;p&gt;Is Conversation Start.&lt;/p&gt; |  |
| **aliasType** | **string** | &lt;p&gt;Alias Table Name.&lt;/p&gt; | [optional]  |

### Return type

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Semantic search retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to retrieve semantic search.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aivectorizationdate"></a>
# **AIVectorizationDate**
> void AIVectorizationDate (string applicationName, string databaseName)

Vectorization Date

<p>Retrive Vectorization Date for the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIVectorizationDateExample
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

            var apiInstance = new AIApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Vectorization Date
                apiInstance.AIVectorizationDate(applicationName, databaseName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIVectorizationDate: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIVectorizationDateWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Vectorization Date
    apiInstance.AIVectorizationDateWithHttpInfo(applicationName, databaseName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIVectorizationDateWithHttpInfo: " + e.Message);
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

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Vectorization Date retrieved successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to retrieve Vectorization date.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a id="aivectorizeoutlinejob"></a>
# **AIVectorizeOutlineJob**
> void AIVectorizeOutlineJob (string applicationName, string databaseName)

Vectorize outline job

<p>Vectorize outline job for the specified application and database.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class AIVectorizeOutlineJobExample
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

            var apiInstance = new AIApi(config);
            var applicationName = "applicationName_example";  // string | <p>Application name.</p>
            var databaseName = "databaseName_example";  // string | <p>Database name.</p>

            try
            {
                // Vectorize outline job
                apiInstance.AIVectorizeOutlineJob(applicationName, databaseName);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AIApi.AIVectorizeOutlineJob: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the AIVectorizeOutlineJobWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Vectorize outline job
    apiInstance.AIVectorizeOutlineJobWithHttpInfo(applicationName, databaseName);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling AIApi.AIVectorizeOutlineJobWithHttpInfo: " + e.Message);
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

void (empty response body)

### Authorization

[OAuth2](../README.md#OAuth2), [basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Vectorized outline successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Failed to vectorize outline.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

