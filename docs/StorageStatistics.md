# EssSharp.Model.StorageStatistics

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Dimensions** | [**List&lt;StatisticsDimensions&gt;**](StatisticsDimensions.md) |  | [optional] 
**MaxKeyLengthBits** | **int** | &lt;p&gt;For aggregate storage cubes, the sum of all the bits used by each dimension. For example, there are 20 bits in the key used for dimensions, and the first 4 are used by Year.&lt;/p&gt; | [optional] 
**MaxKeyLengthBytes** | **int** | &lt;p&gt;For aggregate storage cubes, how many bytes the key uses per cell&lt;/p&gt; | [optional] 
**NumberOfInputLevelCells** | **int** | &lt;p&gt;For aggregate storage cubes, the number of existing level-0 cells in the database, including incremental slices&lt;/p&gt; | [optional] 
**NumberOfIncrementalDataSlices** | **int** | &lt;p&gt;For aggregate storage cubes, the number of data slices resulting from incremental data loads&lt;/p&gt; | [optional] 
**NumberOfIncrementalInputCells** | **int** | &lt;p&gt;For aggregate storage cubes, the number of level-0 cells in the incremental data slices&lt;/p&gt; | [optional] 
**NumberOfAggregateViews** | **int** | &lt;p&gt;For aggregate storage cubes, the number of aggregate views, including those automatically built on incremental slices&lt;/p&gt; | [optional] 
**NumberOfAggregateCells** | **int** | &lt;p&gt;For aggregate storage cubes, the number of cells stored in aggregate views&lt;/p&gt; | [optional] 
**NumberOfIncrementalAggregateCells** | **int** | &lt;p&gt;For aggregate storage cubes, the number of cells stored in the incremental slices&#39; aggregate views&lt;/p&gt; | [optional] 
**CostOfQueryingIncrementalData** | **double** | &lt;p&gt;For aggregate storage cubes, the average percentage of query time spent processing incremental data slices. This is useful in deciding when slices should be merged to improve query performance.&lt;/p&gt; | [optional] 
**InputLevelDataSize** | **int** | &lt;p&gt;For aggregate storage cubes, the total disk space used by input-level data&lt;/p&gt; | [optional] 
**AggregateDataSize** | **int** | &lt;p&gt;For aggregate storage cubes, the total disk space occupied by aggregate cells&lt;/p&gt; | [optional] 
**NumberOfExistingBlocks** | **double** | &lt;p&gt;Total number of existing data blocks (not the maximum)&lt;/p&gt; | [optional] 
**BlockSize** | **int** |  | [optional] 
**PotentialNumberOfBlocks** | **double** |  | [optional] 
**ExistingLevelZeroBlocks** | **double** |  | [optional] 
**ExistingUpperLevelBlocks** | **double** |  | [optional] 
**BlockDensity** | **double** |  | [optional] 
**PercentageOfMaximumBlocksExisting** | **double** |  | [optional] 
**CompressionRatio** | **double** |  | [optional] 
**AverageClusteringRatio** | **double** |  | [optional] 
**PageFileSize** | **long** |  | [optional] 
**IndexFileSize** | **long** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

