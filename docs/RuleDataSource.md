# EssSharp.Model.RuleDataSource
<p>General Source Properties of a dimension build or data load rule. Use to broadly set how the rule behaves with respect to the source data.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Header** | **string** | &lt;p&gt;Header string used in the source data, if known. This will be skipped during the dimension build or data load.&lt;/p&gt; | [optional] 
**Name** | **string** |  | [optional] 
**Tokens** | **List&lt;string&gt;** | &lt;p&gt;Ignore source data records during the dimension build or data load if the record contains the specified token(s). A token is one delimited string in the source data. To specify multiple tokens, be sure to delimit them and specify a &lt;i&gt;tokensCombineOption&lt;/i&gt;. Example of tokens delimited by space:&lt;code&gt; \&quot;tokens\&quot; : [ \&quot;&amp;&amp;    UNDEFINED\&quot; ]&lt;/code&gt;&lt;/p&gt; | [optional] 
**TokensCombineOption** | **string** | &lt;p&gt;If you listed multiple tokens to ignore, specify AND if Essbase should ignore only records that contain all of the tokens. Specify OR if Essbase should ignore records that contain any of the tokens.&lt;/p&gt; | [optional] 
**SqlProperties** | [**SQLProperties**](SQLProperties.md) |  | [optional] 
**FileProperties** | [**FileProperties**](FileProperties.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

