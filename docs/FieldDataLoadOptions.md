# EssSharp.Model.FieldDataLoadOptions
<p>Field-level options you can set for data-load rules. For example, Essbase can ignore, rescale, and perform extractions on fields from the source data.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ReferDimension** | **string** | &lt;p&gt;For duplicate member outlines, specify the dimension and the &lt;i&gt;referOption&lt;/i&gt; that indicates the build method (level reference or generation reference) that Essbase uses to map the field. &lt;/p&gt; | [optional] 
**ReferNumber** | **int** | &lt;p&gt;For duplicate member outlines, use with &lt;i&gt;referOption&lt;/i&gt; to specify the generation or level number expected for the source data field.&lt;/p&gt; | [optional] 
**ReferOption** | **int** | &lt;p&gt;For duplicate member outlines, specify &lt;i&gt;referOption&lt;/i&gt; that indicates the build method (level reference or generation reference) that Essbase uses to map the field. Use level reference when fields are organized bottom-up in the source data. Use generation reference when fields are organized top down in the source data.&lt;/p&gt; | [optional] 
**Ignore** | **bool** |  | [optional] 
**Scale** | **bool** | &lt;p&gt;Available only for data fields. Set to true if you want to scale the values of the source data to match the scale of values stored in the cube. Must be used with &lt;b&gt;scalingfactor&lt;/b&gt;.&lt;/p&gt; | [optional] 
**UseReference** | **bool** |  | [optional] 
**Data** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to indicate that the field is a data (non-metadata) field.&lt;/p&gt; | [optional] 
**Scalingfactor** | **double** | &lt;p&gt;Available only for data fields and when &lt;b&gt;scale&lt;/b&gt; is set to true. A scaling factor, if the values of the source data are not in the same scale as the values of the cube. For example, assume the real value of sales is $5,460. If the Sales source data tracks the values in hundreds, the value is 54.6. If the Essbase cube tracks the real value, you must multiply the value coming in from the Sales source data (54.6) by 100 to have the value display correctly in the Essbase cube (as 5460).&lt;/p&gt; | [optional] 
**StoreType** | **string** | &lt;p&gt;Column-level options to extract the source data in a specific way. Available only for data fields. MIN stores the minimum value of the incoming data, including a comparison with existing cube data. MAX stores the maximum value. SUM behaves the same as the ADD global option, adding the incoming data to existing cube data. COUNT stores the count of values present in the incoming data.&lt;/p&gt; | [optional] 
**StoreTypeCountMissing** | **bool** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

