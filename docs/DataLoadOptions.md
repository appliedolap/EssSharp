# EssSharp.Model.DataLoadOptions
<p>Rule operations available to perform on the data in a field; for example, changing how data affects existing values, clearing values, or flipping signs. Not applicable for dimension build rules.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ClearCombinations** | **List&lt;string&gt;** | &lt;p&gt;Array of text values to clear while loading the data.&lt;/p&gt; | [optional]
**Option** | **string** | &lt;p&gt;A data load option to specify how newly loaded data values affect existing data values. By default, Essbase overwrites the existing values of the cube with the values of the source data. For example, if you load weekly values, you can specify the ADD option to create monthly values in the cube.&lt;/p&gt; | [optional]
**RemoveAll** | **int** |  | [optional]
**SignFlipDimension** | **string** | &lt;p&gt;Dimension name in which to flip signs. You can reverse, or flip, the value of a data field by flipping its sign. Sign flips are based on the UDA (user defined attribute) that you specify in &lt;i&gt;signFlipUDA&lt;/i&gt;. When loading data into the accounts dimension, for example, you can specify that any record whose accounts member has a UDA of Expense change from a plus sign to a minus sign.&lt;/p&gt; | [optional]
**SignFlipUDA** | **string** | &lt;p&gt;UDA (user defined attribute) for which to flip signs. You can reverse, or flip, the value of a data field by flipping its sign. Also specify the &lt;i&gt;signFlipDimension&lt;/i&gt;.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

