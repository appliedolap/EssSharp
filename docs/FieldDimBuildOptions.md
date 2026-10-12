# EssSharp.Model.FieldDimBuildOptions
<p>Field-level options you can set for dimension-build rules. For example, Essbase can ignore, split, join, and reposition fields.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Parent** | **int** | &lt;p&gt;Parent field generation of this field. For example, a field of generation 3 has a generation-2 field as a parent.&lt;/p&gt; | [optional] 
**GenerationType** | **string** | &lt;p&gt;Type you assign to the source data field to help Essbase build the dimension correctly.&lt;/p&gt; | [optional] 
**Refer** | **int** | &lt;p&gt;Which field number this field refers to.&lt;/p&gt; | [optional] 
**ReferIndex** | **int** |  | [optional] 
**Shared** | **int** |  | [optional] 
**AttributeBuildProperties** | [**AttributeBuildProperties**](AttributeBuildProperties.md) |  | [optional] 
**Dimension** | **string** |  | [optional] 
**AttributeDimension** | **string** |  | [optional] 
**Alias** | **string** |  | [optional] 
**EndIndepColumns** | **List&lt;int&gt;** |  | [optional] 
**StartIndepColumns** | **List&lt;int&gt;** |  | [optional] 
**GenerationProperty** | **string** |  | [optional] 
**Generation** | **int** |  | [optional] 
**Ignore** | **bool** | &lt;p&gt; &lt;b&gt;true&lt;/b&gt; if record should be ignored during data loads and dimension builds.&lt;/p&gt; | [optional] 
**StaticField** | **bool** | &lt;p&gt;Whether the field value is static. If &lt;b&gt;true&lt;/b&gt;, then the name is used for loading.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

