# EssSharp.Model.AreaBean

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**SourceArea** | **string** | &lt;p&gt;Partition source area/region, with optional list of slices containing specific member mappings.&lt;/p&gt; | [optional] 
**TargetArea** | **string** | &lt;p&gt;Partition target area/region, with optional list of slices containing specific member mappings.&lt;/p&gt; | [optional] 
**SourceCellCount** | **long** | &lt;p&gt;Cell count of the partition source area. The source and target areas of a transparent or replicated partition should contain the same number of cells. Cell count does not include the cells of attribute dimensions.&lt;/p&gt; | [optional] 
**TargetCellCount** | **long** | &lt;p&gt;Cell count of the partition target area. The source and target areas of a transparent or replicated partition should contain the same number of cells. Cell count does not include the cells of attribute dimensions.&lt;/p&gt; | [optional] 
**Slices** | [**List&lt;MemberMappingBean&gt;**](MemberMappingBean.md) | &lt;p&gt;Optional slice definition defining source and target member mappings. Essbase uses this information to determine how to put data into the target if the  target and the source use different names for some members and dimensions.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

