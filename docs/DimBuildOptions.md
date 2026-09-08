# EssSharp.Model.DimBuildOptions
<p>Global properties affecting all dimensions included in a dimension build rule.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AutoConfig** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; let Essbase automatically assign dimensions as dense or sparse. By default, density/sparsity settings are kept as either the existing setting or the setting specified in the dimension build rule. Applicable to block storage cubes only.&lt;/p&gt; | [optional] 
**ArrangeDimensions** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to arrange dimensions in hourglass order for calculation performance. Applicable to block storage cubes only. The order is: 1- densest dimensions (accounts and time), 2- remaining dense dimensions (largest to smallest), 3- sparse dimensions (smallest to largest), 4- attribute dimensions.&lt;/p&gt; | [optional] 
**AliasTable** | **string** | &lt;p&gt;Select which alias table to update with new aliases from the source data. If unspecified, dimension build updates the default alias table.&lt;/p&gt; | [optional] 
**SmartLists** | [**List&lt;SmartList&gt;**](SmartList.md) | &lt;p&gt;Array to add or update a text list object, also known as a Smart List. Text list objects are a way to store metrics as textual values when your accounts dimension is designed to work with text measures, and your outline is enabled for typed measures. To see a sample cube that uses a textual measure dimension, import the sample application Facility Rating, available in the gallery section of the Files catalog.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

