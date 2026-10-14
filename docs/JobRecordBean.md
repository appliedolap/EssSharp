# EssSharp.Model.JobRecordBean

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**JobID** | **long** |  | [optional] 
**AppName** | **string** |  | [optional] 
**DbName** | **string** |  | [optional] 
**JobType** | **string** | &lt;p&gt;The type of job. Examples: &lt;code&gt;dataload&lt;/code&gt;, &lt;code&gt;dimbuild&lt;/code&gt;, &lt;code&gt;calc&lt;/code&gt;, &lt;code&gt;clear&lt;/code&gt;, &lt;code&gt;importExcel&lt;/code&gt;, &lt;code&gt;exportExcel&lt;/code&gt;, &lt;code&gt;lcmExport&lt;/code&gt;, &lt;code&gt;lcmImport&lt;/code&gt;,  &lt;code&gt;clearAggregation&lt;/code&gt;, &lt;code&gt;buildAggregation&lt;/code&gt;, &lt;code&gt;asoBufferDataLoad&lt;/code&gt;, &lt;code&gt;asoBufferCommit&lt;/code&gt;, &lt;code&gt;exportData&lt;/code&gt;, &lt;code&gt;mdxScript&lt;/code&gt;.&lt;/p&gt; | [optional] 
**JobfileName** | **string** | &lt;p&gt;The script file used for the job.&lt;/p&gt; | [optional] 
**UserName** | **string** | &lt;p&gt;User who ran the job. Users have access to job listings based on their assigned user role. For example, if you have the Service Administrator role, you can see all jobs; if you have the User role, you can see only the jobs you ran.&lt;/p&gt; | [optional] 
**StartTime** | **long** | &lt;p&gt;Start time of the job.&lt;/p&gt; | [optional] 
**EndTime** | **long** | &lt;p&gt;End time of the job.&lt;/p&gt; | [optional] 
**StatusCode** | **int** | &lt;p&gt;Job status code indicating progress. Each code has a corresponding &lt;i&gt;statusMessage&lt;/i&gt;.&lt;/p&gt;&lt;br&gt;&lt;table&gt;&lt;tr&gt;&lt;th&gt;statusCode&lt;/th&gt;&lt;th&gt;statusMessage&lt;/th&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;100&lt;/td&gt;&lt;td&gt;IN_PROGRESS&lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;200&lt;/td&gt;&lt;td&gt;COMPLETED&lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;300&lt;/td&gt;    &lt;td&gt;COMPLETED_WITH_WARNINGS&lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;400&lt;/td&gt;&lt;td&gt;FAILED&lt;/td&gt;&lt;/tr&gt;&lt;/table&gt; | [optional] 
**StatusMessage** | **string** | &lt;p&gt;Job status message string indicating progress. Each string has a corresponding &lt;i&gt;statusCode&lt;/i&gt;.&lt;/p&gt;&lt;br&gt;&lt;table&gt;&lt;tr&gt;&lt;th&gt;statusCode&lt;/th&gt;&lt;th&gt;statusMessage&lt;/th&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;100&lt;/td&gt;&lt;td&gt;IN_PROGRESS&lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;200&lt;/td&gt;&lt;td&gt;COMPLETED&lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;300&lt;/td&gt;    &lt;td&gt;COMPLETED_WITH_WARNINGS&lt;/td&gt;&lt;/tr&gt;&lt;tr&gt;&lt;td&gt;400&lt;/td&gt;&lt;td&gt;FAILED&lt;/td&gt;&lt;/tr&gt;&lt;/table&gt; | [optional] 
**JobInputInfo** | **Dictionary&lt;string, Object&gt;** |  | [optional] 
**JobOutputInfo** | **Dictionary&lt;string, Object&gt;** |  | [optional] 
**Links** | [**List&lt;Link&gt;**](Link.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

