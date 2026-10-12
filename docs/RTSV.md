# EssSharp.Model.RTSV
<p>Details about the runtime substitution variable.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | &lt;p&gt;Name of the runtime substitution variable.&lt;/p&gt; | [optional] 
**Description** | **string** | &lt;p&gt;&lt;/p&gt; | [optional] 
**AllowMissing** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to allow data cells for which no data exists, or &lt;b&gt;false&lt;/b&gt; to suppress them.&lt;/p&gt; | [optional] 
**Dimension** | **string** | &lt;p&gt;The name of the dimension from which this variable pulls information. Supported only if &lt;i&gt;type&lt;/i&gt; &#x3D; MEMBER.&lt;/p&gt; | [optional] 
**SingleChoice** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; if only one contextual member selection may be passed to the runtime substitution variable. If there is a single member on the grid or POV, that member is used. If a dimension is on the POV, the active member is used. If a dimension is on the POV and there are multiple members, an error occurs. &lt;/p&gt;&lt;p&gt;Set to &lt;b&gt;false&lt;/b&gt; if all dimension members on the grid or the POV are included.&lt;/p&gt; | [optional] 
**Type** | **string** | &lt;p&gt;Specification of whether the variable is for a member, string, or number.&lt;/p&gt; | [optional] 
**Value** | **Object** | &lt;p&gt;Default value of the runtime substitution variable. RTSV values can be strings,  constants, member names, or member combinations.&lt;/p&gt;&lt;p&gt;If the RTSV is designed for calcs executed in Smart View, its value must be set to the constant &lt;code&gt;POV&lt;/code&gt;, to indicate that only the current data slice present in the spreadsheet grid should be calculated.&lt;/p&gt; | [optional] 
**Limit** | **string** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

