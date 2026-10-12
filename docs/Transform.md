# EssSharp.Model.Transform
<p>Transformation options for data processing that you can use in an index-based dimension build rule. Types can be COLUMN, SUBSTR for substring function, CONCAT for joins, STATICSTR for static strings, or IGNORE. The following index-based rule transformation spec is the same as the Essbase Web interface using a field expression of <code>join(column0,column1)</code>. <code>\"transform\" : {\"type\": \"CONCAT\", \"nodes\": [ {\"type\": \"COLUMN\", \"index\" : 0}, {\"type\": \"COLUMN\", \"index\": 1}]}}</code>.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Nodes** | [**List&lt;Transform&gt;**](Transform.md) |  | [optional] 
**Type** | **string** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

