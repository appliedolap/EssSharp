# EssSharp.Model.Rules

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Dimensions** | [**List&lt;Dimension&gt;**](Dimension.md) | &lt;p&gt;Properties defined per dimension, to set how the rule behaves for a dimension.&lt;/p&gt; | [optional]
**Fields** | [**List&lt;Field&gt;**](Field.md) | &lt;p&gt;Array of field information, including general properties for all fields, as well as information about specific fields.&lt;/p&gt; | [optional]
**TimeStamp** | **long** |  | [optional]
**DataSource** | [**RuleDataSource**](RuleDataSource.md) |  | [optional]
**DimensionBuildOptions** | [**DimBuildOptions**](DimBuildOptions.md) |  | [optional]
**DataLoadOptions** | [**DataLoadOptions**](DataLoadOptions.md) |  | [optional]
**EditorOptions** | [**EditorOptions**](EditorOptions.md) |  | [optional]
**Encoding** | **string** | &lt;p&gt;The encoding type of the rule file.&lt;/p&gt; | [optional]
**Name** | **string** |  | [optional]
**Locale** | **string** |  | [optional]
**Studio** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if the rule is Index-based, and &lt;b&gt;false&lt;/b&gt; if the rule is Regular. Index-based rules (sometimes called BPM rules) have different structure than Regular rules. Fields can be in any order; for example, Generation 2 can come before Generation 1, and a property of Generation 1 can come before the actual Generation 1 column. An index-based rule does not offer column operations (join, split, and move), but you can accomplish the same using field expressions.&lt;/p&gt; | [optional]
**Bibpm** | **bool** |  | [optional]
**Xolap** | **bool** |  | [optional]
**FlatFileBased** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if the rule is associated with a flat file.&lt;/p&gt; | [optional]
**EssbaseInfo** | [**EssbaseInfo**](EssbaseInfo.md) |  | [optional]
**ColumnOperations** | [**List&lt;ColumnOperation&gt;**](ColumnOperation.md) | &lt;p&gt;Rule operations available to perform at the field level. For example, you can move a field to a new position in the record.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

