# EssSharp.Model.EssbaseASODbCompression

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** |  | [optional]
**ArtifactType** | **int** |  | [optional]
**NodeName** | **string** |  | [optional]
**AppName** | **string** |  | [optional]
**DbName** | **string** |  | [optional]
**Locked** | **bool** |  | [optional]
**LockedByUser** | **string** |  | [optional]
**DimensionName** | **string** | &lt;p&gt;Each dimension name in the cube, hypothetically considered to be the compression dimension.&lt;/p&gt; | [optional]
**IsCompression** | **bool** | &lt;p&gt;Indicates whether the dimension is the ASO compression dimension. There can be only one compression dimension in an ASO cube.&lt;/p&gt; | [optional]
**StoredLevel0Members** | **double** | &lt;p&gt;The number of leaf-level members in the dimension. A large number of stored  level-0 members in a dimension indicates that it may not perform well as a compression dimension.&lt;/p&gt; | [optional]
**AverageBundleFill** | **double** | &lt;p&gt;Estimated average number of values per compression dimension bundle. Choosing a  compression dimension that has a higher average bundle fill means that the cube compresses  better.&lt;/p&gt; | [optional]
**AverageValueLength** | **double** | &lt;p&gt;Estimated average number of bytes required to store a value. Dimensions with a  smaller average value length compress the cube better.&lt;/p&gt; | [optional]
**Level0MB** | **double** | &lt;p&gt;Estimated size of the compressed cube, in megabytes. A smaller expected level-0 size indicates that choosing this dimension enables better compression.&lt;br&gt;&lt;br&gt;Except for the scenario in which there is no compression dimension (&lt;i&gt;None&lt;/i&gt;), all estimates assume that all pages are compressed. As compressed pages require additional overhead that uncompressed pages do not, the estimated level-0 cube size for some dimensions may be larger than the value for &lt;i&gt;None&lt;/i&gt;. &lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

