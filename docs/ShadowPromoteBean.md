# EssSharp.Model.ShadowPromoteBean

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ShadowAppName** | **string** | &lt;p&gt;Unique shadow application name which is a copy of the source.&lt;/p&gt; | 
**PrimaryAppName** | **string** | &lt;p&gt;Name of the primary, non-shadow application.&lt;/p&gt; | 
**TimeoutToForceUnloadApp** | **int** | &lt;p&gt;Time interval (in seconds) to wait before forcefully unloading/stopping an application, if it is performing ongoing requests. If a graceful unload process fails or takes longer than permitted by this timeout, Essbase forcefully terminates the application.&lt;/p&gt; | 
**RunInBackground** | **bool** | &lt;p&gt;Specify &lt;b&gt;true&lt;/b&gt; to schedule &#39;Shadow Promote&#39; as a Job; otherwise, specify &lt;b&gt;false&lt;/b&gt;.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

