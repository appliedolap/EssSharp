# EssSharp.Api.FilesApi

All URIs are relative to */essbase/rest/v1*

| Method | HTTP request | Description |
|--------|--------------|-------------|
| [**FilesAbortUpload**](FilesApi.md#filesabortupload) | **DELETE** /files/abort/{path} | Abort File Upload |
| [**FilesAddFile**](FilesApi.md#filesaddfile) | **PUT** /files/{path} | Upload File or Create Folder |
| [**FilesCopyResource**](FilesApi.md#filescopyresource) | **POST** /files/actions/copy | Copy File |
| [**FilesCreateUpload**](FilesApi.md#filescreateupload) | **POST** /files/upload-create/{path} | Create Upload |
| [**FilesDeleteFile**](FilesApi.md#filesdeletefile) | **DELETE** /files/{path} | Delete File or Folder |
| [**FilesExtract**](FilesApi.md#filesextract) | **POST** /files/actions/extract | Extract Zip File |
| [**FilesGetSharedPath**](FilesApi.md#filesgetsharedpath) | **GET** /files/sharedpath | Get Shared Path |
| [**FilesGetUserHomePath**](FilesApi.md#filesgetuserhomepath) | **GET** /files/homepath | Get Home Path |
| [**FilesListFiles**](FilesApi.md#fileslistfiles) | **GET** /files/{path} | List or Download Files |
| [**FilesListRootFolders**](FilesApi.md#fileslistrootfolders) | **GET** /files | List Root Folders |
| [**FilesMoveResource**](FilesApi.md#filesmoveresource) | **POST** /files/actions/move | Move or Rename File |
| [**FilesUploadCommit**](FilesApi.md#filesuploadcommit) | **POST** /files/upload-commit/{path} | Upload Commit |
| [**FilesUploadPart**](FilesApi.md#filesuploadpart) | **PUT** /files/upload-part/{path} | Upload the part |
| [**GetUploadConfig**](FilesApi.md#getuploadconfig) | **GET** /files/uploadconfig |  |

<a name="filesabortupload"></a>
# **FilesAbortUpload**
> void FilesAbortUpload (string path, string uploadId)

Abort File Upload

<p>Abort file upload api , abort the upload operation of a file and delete all the uploaded parts.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesAbortUploadExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var path = "path_example";  // string | <p>File Path to abort</p>
            var uploadId = "uploadId_example";  // string | <p>Upload Id of file to abort</p>

            try
            {
                // Abort File Upload
                apiInstance.FilesAbortUpload(path, uploadId);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesAbortUpload: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesAbortUploadWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Abort File Upload
    apiInstance.FilesAbortUploadWithHttpInfo(path, uploadId);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesAbortUploadWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **path** | **string** | &lt;p&gt;File Path to abort&lt;/p&gt; |  |
| **uploadId** | **string** | &lt;p&gt;Upload Id of file to abort&lt;/p&gt; |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The file upload operation abort successfully.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filesaddfile"></a>
# **FilesAddFile**
> GenericEntity FilesAddFile (string path, bool overwrite, System.IO.Stream stream, bool? append = null)

Upload File or Create Folder

<p>Uploads a file to Essbase.</p><p>Supported file types include text files, rules files, calculation script files, and MaxL script files.</p> <p>If there is no content type, and a folder name is specified in the URL, a folder is created.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesAddFileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var path = "path_example";  // string | <p>Catalog path. If <code>Content-Type=application/octet-stream</code>, this is a file name. Otherwise, it is a folder name.</p>
            var overwrite = false;  // bool | <p>Applicable only for adding a file. Overwriting folders is not supported.</p> (default to false)
            var stream = new System.IO.MemoryStream(System.IO.File.ReadAllBytes("/path/to/file.txt"));  // System.IO.Stream | <p>Applicable only for adding a file. Provides the stream to upload.</p>
            var append = false;  // bool? | append (optional)  (default to false)

            try
            {
                // Upload File or Create Folder
                GenericEntity result = apiInstance.FilesAddFile(path, overwrite, stream, append);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesAddFile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesAddFileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Upload File or Create Folder
    ApiResponse<GenericEntity> response = apiInstance.FilesAddFileWithHttpInfo(path, overwrite, stream, append);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesAddFileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **path** | **string** | &lt;p&gt;Catalog path. If &lt;code&gt;Content-Type&#x3D;application/octet-stream&lt;/code&gt;, this is a file name. Otherwise, it is a folder name.&lt;/p&gt; |  |
| **overwrite** | **bool** | &lt;p&gt;Applicable only for adding a file. Overwriting folders is not supported.&lt;/p&gt; | [default to false] |
| **stream** | **System.IO.Stream****System.IO.Stream** | &lt;p&gt;Applicable only for adding a file. Provides the stream to upload.&lt;/p&gt; |  |
| **append** | **bool?** | append | [optional] [default to false] |

### Return type

[**GenericEntity**](GenericEntity.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/octet-stream
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The file uploaded successfully (if Content-Type is &lt;code&gt;application/octet-stream&lt;/code&gt;), or the folder was created successfully (if there is no Content-Type). |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions, or the file or folder may already exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filescopyresource"></a>
# **FilesCopyResource**
> void FilesCopyResource (FilePathDetail body, bool? overwrite = null)

Copy File

Copy a file from source to destination.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesCopyResourceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var body = new FilePathDetail(); // FilePathDetail | <p>File path details.</p>
            var overwrite = false;  // bool? | <p>Overwrite existing file.</p> (optional)  (default to false)

            try
            {
                // Copy File
                apiInstance.FilesCopyResource(body, overwrite);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesCopyResource: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesCopyResourceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Copy File
    apiInstance.FilesCopyResourceWithHttpInfo(body, overwrite);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesCopyResourceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**FilePathDetail**](FilePathDetail.md) | &lt;p&gt;File path details.&lt;/p&gt; |  |
| **overwrite** | **bool?** | &lt;p&gt;Overwrite existing file.&lt;/p&gt; | [optional] [default to false] |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;File copied successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filescreateupload"></a>
# **FilesCreateUpload**
> CreateFilePartUploadResponse FilesCreateUpload (string path, bool overwrite, bool append)

Create Upload

Initialize file upload in parts by registering the file, it returns unique upload id , which must be included in any request related to this file part upload.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesCreateUploadExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var path = "path_example";  // string | <p>Catalog path</p>
            var overwrite = false;  // bool | <p>Overwrite the file</p> (default to false)
            var append = false;  // bool | <p>Append</p> (default to false)

            try
            {
                // Create Upload
                CreateFilePartUploadResponse result = apiInstance.FilesCreateUpload(path, overwrite, append);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesCreateUpload: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesCreateUploadWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Create Upload
    ApiResponse<CreateFilePartUploadResponse> response = apiInstance.FilesCreateUploadWithHttpInfo(path, overwrite, append);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesCreateUploadWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **path** | **string** | &lt;p&gt;Catalog path&lt;/p&gt; |  |
| **overwrite** | **bool** | &lt;p&gt;Overwrite the file&lt;/p&gt; | [default to false] |
| **append** | **bool** | &lt;p&gt;Append&lt;/p&gt; | [default to false] |

### Return type

[**CreateFilePartUploadResponse**](CreateFilePartUploadResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Registered file successfully. Returns unique id.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions, or the file may already exist.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filesdeletefile"></a>
# **FilesDeleteFile**
> void FilesDeleteFile (string path)

Delete File or Folder

<p>Delete the file or folder specified in the path.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesDeleteFileExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var path = "path_example";  // string | Path of file/folder to delete

            try
            {
                // Delete File or Folder
                apiInstance.FilesDeleteFile(path);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesDeleteFile: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesDeleteFileWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Delete File or Folder
    apiInstance.FilesDeleteFileWithHttpInfo(path);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesDeleteFileWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **path** | **string** | Path of file/folder to delete |  |

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
| **204** | &lt;p&gt;&lt;strong&gt;No Content&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The file or folder was removed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filesextract"></a>
# **FilesExtract**
> void FilesExtract (ZipFileDetails body, bool? overwrite = null)

Extract Zip File

<p>Extract a zip file on same location. Supported for applications, users and shared folders.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesExtractExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var body = new ZipFileDetails(); // ZipFileDetails | <p>Zip file path details.</p>
            var overwrite = false;  // bool? | <p>Overwrite existing file. Not applicable for folder.</p> (optional)  (default to false)

            try
            {
                // Extract Zip File
                apiInstance.FilesExtract(body, overwrite);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesExtract: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesExtractWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Extract Zip File
    apiInstance.FilesExtractWithHttpInfo(body, overwrite);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesExtractWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**ZipFileDetails**](ZipFileDetails.md) | &lt;p&gt;Zip file path details.&lt;/p&gt; |  |
| **overwrite** | **bool?** | &lt;p&gt;Overwrite existing file. Not applicable for folder.&lt;/p&gt; | [optional] [default to false] |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The file operation completed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Contains an invalid special character.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filesgetsharedpath"></a>
# **FilesGetSharedPath**
> string FilesGetSharedPath ()

Get Shared Path

<p>Get user shared path.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesGetSharedPathExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);

            try
            {
                // Get Shared Path
                string result = apiInstance.FilesGetSharedPath();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesGetSharedPath: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesGetSharedPathWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Shared Path
    ApiResponse<string> response = apiInstance.FilesGetSharedPathWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesGetSharedPathWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

**string**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The shared path was returned successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filesgetuserhomepath"></a>
# **FilesGetUserHomePath**
> string FilesGetUserHomePath ()

Get Home Path

<p>Get user home path.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesGetUserHomePathExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);

            try
            {
                // Get Home Path
                string result = apiInstance.FilesGetUserHomePath();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesGetUserHomePath: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesGetUserHomePathWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Get Home Path
    ApiResponse<string> response = apiInstance.FilesGetUserHomePathWithHttpInfo();
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesGetUserHomePathWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters
This endpoint does not need any parameter.
### Return type

**string**

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;User home path.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="fileslistfiles"></a>
# **FilesListFiles**
> FileCollectionResponse FilesListFiles (string path, int? offset = null, int? limit = null, string type = null, bool? overwrite = null, string action = null, long? fileSize = null, string filter = null, bool? recursive = null)

List or Download Files

<p>Returns a list of files, or downloads the specified file. To list files, use <code>Accept='application/json'</code> for the Accept header. To download, use <code>Accept='application/octet-stream'</code> for the Accept header.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesListFilesExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var path = "path_example";  // string | <p>Catalog path, starting with <code>applications</code>, <code>shared</code>, or <code>users</code>. If listing files, this is the folder path. If downloading files, this is the file path.</p><p>Examples:</p><ul><li><code>applications/sample</code></li><li><code>shared</code></li><li><code>users/ksmith</code></li></ul>
            var offset = 56;  // int? | <p>Number of items to omit from the start of the result set. Default value is 0. Applicable only for listing files.</p> (optional) 
            var limit = 56;  // int? | <p>Maximum number of files to return. Applicable only for listing files.</p> (optional) 
            var type = "type_example";  // string | <p>List files by type. If type is not specified, returns all files. Applicable only for listing files.</p> (optional) 
            var overwrite = false;  // bool? | <p>If true, overwrite files. If false, any existing file is validated but not overwritten. Applicable only with query parameters  <code>action=validateUpload</code> and <code>Accept='application/json'</code> or <code>Accept='application/xml'</code> . Default value is false.</p> (optional)  (default to false)
            var action = "action_example";  // string | <p>Validates the upload. Supported action values are <code>validateUpload</code> and <code>'Accept=application/json'</code> or <code>'Accept=application/xml'</code>.</p> (optional) 
            var fileSize = 789L;  // long? | <p>Validates whether enough free space is available. Applicable only with query parameters <code>action='validateUpload'</code> and <code>Accept='application/json'</code> or <code>Accept='application/xml'</code>.</p> (optional) 
            var filter = "filter_example";  // string | <p>Filter the list of files.</p> (optional) 
            var recursive = false;  // bool? | <p>Recursive param to get search result as recursive.</p> (optional)  (default to false)

            try
            {
                // List or Download Files
                FileCollectionResponse result = apiInstance.FilesListFiles(path, offset, limit, type, overwrite, action, fileSize, filter, recursive);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesListFiles: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesListFilesWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List or Download Files
    ApiResponse<FileCollectionResponse> response = apiInstance.FilesListFilesWithHttpInfo(path, offset, limit, type, overwrite, action, fileSize, filter, recursive);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesListFilesWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **path** | **string** | &lt;p&gt;Catalog path, starting with &lt;code&gt;applications&lt;/code&gt;, &lt;code&gt;shared&lt;/code&gt;, or &lt;code&gt;users&lt;/code&gt;. If listing files, this is the folder path. If downloading files, this is the file path.&lt;/p&gt;&lt;p&gt;Examples:&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;applications/sample&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;shared&lt;/code&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;users/ksmith&lt;/code&gt;&lt;/li&gt;&lt;/ul&gt; |  |
| **offset** | **int?** | &lt;p&gt;Number of items to omit from the start of the result set. Default value is 0. Applicable only for listing files.&lt;/p&gt; | [optional]  |
| **limit** | **int?** | &lt;p&gt;Maximum number of files to return. Applicable only for listing files.&lt;/p&gt; | [optional]  |
| **type** | **string** | &lt;p&gt;List files by type. If type is not specified, returns all files. Applicable only for listing files.&lt;/p&gt; | [optional]  |
| **overwrite** | **bool?** | &lt;p&gt;If true, overwrite files. If false, any existing file is validated but not overwritten. Applicable only with query parameters  &lt;code&gt;action&#x3D;validateUpload&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt; . Default value is false.&lt;/p&gt; | [optional] [default to false] |
| **action** | **string** | &lt;p&gt;Validates the upload. Supported action values are &lt;code&gt;validateUpload&lt;/code&gt; and &lt;code&gt;&#39;Accept&#x3D;application/json&#39;&lt;/code&gt; or &lt;code&gt;&#39;Accept&#x3D;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; | [optional]  |
| **fileSize** | **long?** | &lt;p&gt;Validates whether enough free space is available. Applicable only with query parameters &lt;code&gt;action&#x3D;&#39;validateUpload&#39;&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; | [optional]  |
| **filter** | **string** | &lt;p&gt;Filter the list of files.&lt;/p&gt; | [optional]  |
| **recursive** | **bool?** | &lt;p&gt;Recursive param to get search result as recursive.&lt;/p&gt; | [optional] [default to false] |

### Return type

[**FileCollectionResponse**](FileCollectionResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt; Response type can be either JSON/XML or stream, depending on the Accept header. If &lt;code&gt;Accept&#x3D;application/json&lt;/code&gt; or &lt;code&gt;Accept&#x3D;application/xml&lt;/code&gt;, the response lists files and current folder details. If &lt;code&gt;Accept&#x3D;application/octet-stream&lt;/code&gt;, the response is returned as a stream. If query parameters include &lt;code&gt;action&#x3D;validateUpload&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;, the response is empty.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions, or the file may already exist when query parameters include &lt;code&gt;action&#x3D;validateUpload&lt;/code&gt; and &lt;code&gt;Accept&#x3D;&#39;application/json&#39;&lt;/code&gt; or &lt;code&gt;Accept&#x3D;&#39;application/xml&#39;&lt;/code&gt;.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="fileslistrootfolders"></a>
# **FilesListRootFolders**
> FileCollectionResponse FilesListRootFolders (string filter = null, bool? recursive = null)

List Root Folders

<p>List catalog root folders.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesListRootFoldersExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var filter = "filter_example";  // string | <p>Filter the list of files.</p> (optional) 
            var recursive = false;  // bool? | <p>Return search results recursively.</p> (optional)  (default to false)

            try
            {
                // List Root Folders
                FileCollectionResponse result = apiInstance.FilesListRootFolders(filter, recursive);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesListRootFolders: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesListRootFoldersWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // List Root Folders
    ApiResponse<FileCollectionResponse> response = apiInstance.FilesListRootFoldersWithHttpInfo(filter, recursive);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesListRootFoldersWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **filter** | **string** | &lt;p&gt;Filter the list of files.&lt;/p&gt; | [optional]  |
| **recursive** | **bool?** | &lt;p&gt;Return search results recursively.&lt;/p&gt; | [optional] [default to false] |

### Return type

[**FileCollectionResponse**](FileCollectionResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Folder list.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Invalid path.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filesmoveresource"></a>
# **FilesMoveResource**
> void FilesMoveResource (FilePathDetail body, bool? overwrite = null)

Move or Rename File

<p>Either moves a file from source to destination, or renames a file or folder. Moving a folder is not supported. Renaming a folder is supported only if the folder is not in the applications directory.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesMoveResourceExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var body = new FilePathDetail(); // FilePathDetail | <p>File path details.</p>
            var overwrite = false;  // bool? | <p>Overwrite existing file. Only applicable for moving or renaming a file.</p> (optional)  (default to false)

            try
            {
                // Move or Rename File
                apiInstance.FilesMoveResource(body, overwrite);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesMoveResource: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesMoveResourceWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Move or Rename File
    apiInstance.FilesMoveResourceWithHttpInfo(body, overwrite);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesMoveResourceWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **body** | [**FilePathDetail**](FilePathDetail.md) | &lt;p&gt;File path details.&lt;/p&gt; |  |
| **overwrite** | **bool?** | &lt;p&gt;Overwrite existing file. Only applicable for moving or renaming a file.&lt;/p&gt; | [optional] [default to false] |

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
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;The file operation completed successfully.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Logged in user may not have appropriate permissions.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filesuploadcommit"></a>
# **FilesUploadCommit**
> CommitFilePartUploadResponse FilesUploadCommit (string path, string uploadId = null, Dictionary<string, string> body = null)

Upload Commit

<p>Commit the upload. Include the part number and corresponding ETag value for each part.</p>

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesUploadCommitExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var path = "path_example";  // string | 
            var uploadId = "uploadId_example";  // string |  (optional) 
            var body = new Dictionary<string, string>(); // Dictionary<string, string> |  (optional) 

            try
            {
                // Upload Commit
                CommitFilePartUploadResponse result = apiInstance.FilesUploadCommit(path, uploadId, body);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesUploadCommit: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesUploadCommitWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Upload Commit
    ApiResponse<CommitFilePartUploadResponse> response = apiInstance.FilesUploadCommitWithHttpInfo(path, uploadId, body);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesUploadCommitWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **path** | **string** |  |  |
| **uploadId** | **string** |  | [optional]  |
| **body** | [**Dictionary&lt;string, string&gt;**](string.md) |  | [optional]  |

### Return type

[**CommitFilePartUploadResponse**](CommitFilePartUploadResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: application/json, application/xml
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Returns ETag for the file.&lt;/p&gt;  |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Error occurred while merging all the parts.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="filesuploadpart"></a>
# **FilesUploadPart**
> UploadFilePartResponse FilesUploadPart (string path, int partNum, string uploadId)

Upload the part

Upload Part request for each object part upload. It should contain upload ID.

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class FilesUploadPartExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);
            var path = "path_example";  // string | <p>Catalog Path </p>
            var partNum = 56;  // int | <p>Part Number</p>
            var uploadId = "uploadId_example";  // string | <p>Upload Id</p>

            try
            {
                // Upload the part
                UploadFilePartResponse result = apiInstance.FilesUploadPart(path, partNum, uploadId);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.FilesUploadPart: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the FilesUploadPartWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    // Upload the part
    ApiResponse<UploadFilePartResponse> response = apiInstance.FilesUploadPartWithHttpInfo(path, partNum, uploadId);
    Debug.Write("Status Code: " + response.StatusCode);
    Debug.Write("Response Headers: " + response.Headers);
    Debug.Write("Response Body: " + response.Data);
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.FilesUploadPartWithHttpInfo: " + e.Message);
    Debug.Print("Status Code: " + e.ErrorCode);
    Debug.Print(e.StackTrace);
}
```

### Parameters

| Name | Type | Description | Notes |
|------|------|-------------|-------|
| **path** | **string** | &lt;p&gt;Catalog Path &lt;/p&gt; |  |
| **partNum** | **int** | &lt;p&gt;Part Number&lt;/p&gt; |  |
| **uploadId** | **string** | &lt;p&gt;Upload Id&lt;/p&gt; |  |

### Return type

[**UploadFilePartResponse**](UploadFilePartResponse.md)

### Authorization

[basicAuth](../README.md#basicAuth)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/xml


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | &lt;p&gt;&lt;strong&gt;OK&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;Returns a unique ETag(entity tag). Both the part number and corresponding ETag value for each part when commit the uploaded.&lt;/p&gt; |  -  |
| **400** | &lt;p&gt;&lt;strong&gt;Bad Request&lt;/strong&gt;&lt;/p&gt;&lt;p&gt;If any issue while uploading parts, it returns error and all parts get clean.&lt;/p&gt; |  -  |
| **500** | &lt;p&gt;Internal Server Error.&lt;/p&gt; |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getuploadconfig"></a>
# **GetUploadConfig**
> void GetUploadConfig ()



### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using EssSharp.Api;
using EssSharp.Client;
using EssSharp.Model;

namespace Example
{
    public class GetUploadConfigExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "/essbase/rest/v1";
            // Configure HTTP basic authorization: basicAuth
            config.Username = "YOUR_USERNAME";
            config.Password = "YOUR_PASSWORD";

            var apiInstance = new FilesApi(config);

            try
            {
                apiInstance.GetUploadConfig();
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling FilesApi.GetUploadConfig: " + e.Message);
                Debug.Print("Status Code: " + e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

#### Using the GetUploadConfigWithHttpInfo variant
This returns an ApiResponse object which contains the response data, status code and headers.

```csharp
try
{
    apiInstance.GetUploadConfigWithHttpInfo();
}
catch (ApiException e)
{
    Debug.Print("Exception when calling FilesApi.GetUploadConfigWithHttpInfo: " + e.Message);
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
| **0** | successful operation |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

