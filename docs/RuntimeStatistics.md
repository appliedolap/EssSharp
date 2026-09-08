# EssSharp.Model.RuntimeStatistics

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**CacheHitRatio** | **double** | &lt;p&gt;Ratio of the number of requests answered from aggregate storage cache as opposed to from the hard disk. May not be accurate when parallel data load or parallel calculation operations are in use.&lt;/p&gt; | [optional] 
**CurrentCacheSize** | **double** | &lt;p&gt;The current size of the aggregate storage cache. See also currentCacheSizeLimit.&lt;/p&gt; | [optional] 
**CurrentCacheSizeLimit** | **double** | &lt;p&gt;The maximum size (in kilobytes) to which the aggregate storage cache may grow.&lt;/p&gt; | [optional] 
**PageReadsSinceLastStartup** | **double** | &lt;p&gt;For an aggregate storage cube, number of data pages read from disk since the last time the application was started.&lt;/p&gt; | [optional] 
**PageWritesSinceLastStartup** | **double** | &lt;p&gt;For an aggregate storage cube, number of data pages written to disk since the last time the application was started.&lt;/p&gt; | [optional] 
**PageSize** | **double** | &lt;p&gt;For an aggregate storage cube, size of the data page in kilobytes.&lt;/p&gt; | [optional] 
**DiskSpaceAllocatedForData** | **double** | &lt;p&gt;For an aggregate storage cube, total space used by all disk files in the default tablespace.&lt;/p&gt; | [optional] 
**DiskSpaceUsedByData** | **double** | &lt;p&gt;For an aggregate storage cube, total space actually in use within the disk files in the default tablespace (some space within files may be free).&lt;/p&gt; | [optional] 
**TemporaryDiskSpaceAllocated** | **double** | &lt;p&gt;For an aggregate storage cube, total space used by all disk files in the temp tablespace.&lt;/p&gt; | [optional] 
**TemporaryDiskSpaceUsed** | **double** | &lt;p&gt;For an aggregate storage cube, total space actually in use within the disk files in the temp tablespace (some space within files may be free).&lt;/p&gt; | [optional] 
**HitRatioOnIndexCache** | **double** |  | [optional] 
**HitRatioOnDataCache** | **double** |  | [optional] 
**NumberOfIndexPageReads** | **double** |  | [optional] 
**NumberOfIndexPageWrites** | **double** |  | [optional] 
**NumberOfDataBlockReads** | **double** |  | [optional] 
**NumberOfDataBlockWrites** | **double** |  | [optional] 
**HitRatioOnDataFileCache** | **double** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

