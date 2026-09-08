# EssSharp.Model.CompressionSettings
<p>Estimated compression statistics for an aggregate storage cube, with different dimensions hypothetically used as the compression dimension. These estimates can help you choose the best compression dimension.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**IsCompression** | **bool** | &lt;p&gt;Whether this is an aggregate storage compression dimension. By default, the compression dimension is the Accounts dimension. There can be only one compression dimension in an aggregate storage cube.&lt;/p&gt; | [optional] 
**StoredLevel0Members** | **double** | &lt;p&gt;Number of stored level 0 members. Aggregate storage compression dimensions with a large number of stored level 0 members do not perform optimally. As with any dynamically calculated dimension, upper-level retrievals from compression dimensions are slower.&lt;/p&gt; | [optional] 
**AverageBundleFill** | **double** | &lt;p&gt;Applies to aggregate storage cubes only. Estimated average number of values per compression dimension bundle. Choosing a compression dimension that has a higher average bundle fill means that the cube compresses better.&lt;/p&gt; | [optional] 
**AverageValueLength** | **double** | &lt;p&gt;Estimated average number of bytes required to store a value. Dimensions with a smaller average value length compress the cube better.&lt;/p&gt; | [optional] 
**Level0MB** | **double** | &lt;p&gt;Estimated size of the compressed cube, in megabytes. A smaller expected level-0 size indicates that choosing this dimension enables better compression. Except for the scenario in which there is no compression dimension (None), all estimates assume that all pages are compressed. Since compressed pages require additional overhead that uncompressed pages do not, the estimated level-0 database size for some dimensions may be larger than the value for None.&lt;/p&gt; | [optional] 
**DimensionName** | **string** | &lt;p&gt;Dimension name hypothetically considered to be the aggregate storage compression dimension, for purposes of estimating compression statistics.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

