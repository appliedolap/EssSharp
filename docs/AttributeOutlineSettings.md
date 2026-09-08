# EssSharp.Model.AttributeOutlineSettings
<p>Specifications for handling member names in attribute dimensions.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**PrefixSuffixValue** | **string** | A prefix or suffix value to use for attribute member names as needed to support member name uniqueness. &lt;p&gt;Values: &lt;code&gt;NONE&lt;/code&gt;, &lt;code&gt;PARENT&lt;/code&gt;, &lt;code&gt;GRANDANDPARENT&lt;/code&gt;, &lt;code&gt;ALLANCESTORS&lt;/code&gt;, &lt;code&gt;DIMENSION&lt;/code&gt;&lt;/p&gt; | [optional] 
**PrefixSuffixSeparator** | **string** | &lt;p&gt;Separator character between the prefix or suffix and the attribute member name. Values: &lt;code&gt;UNDERSCORE&lt;/code&gt;, &lt;code&gt;PIPE&lt;/code&gt;, &lt;code&gt;CARET&lt;/code&gt;&lt;/p&gt; | [optional] 
**PrefixSuffixFormat** | **string** | &lt;p&gt;If unique names are required for member names in Boolean, date, and numeric attribute dimensions in the outline, set &lt;i&gt;prefixSuffixValue&lt;/i&gt; to something other than NONE and set this value to select a prefix or a suffix. Values: &lt;code&gt;PREFIX&lt;/code&gt; or &lt;code&gt;SUFFIX&lt;/code&gt;.&lt;/p&gt; | [optional] 
**TrueMemberName** | **string** | &lt;p&gt;Name for True members in Boolean attribute dimensions.&lt;/p&gt; | [optional] 
**FalseMemberName** | **string** | &lt;p&gt;Name for False members in Boolean attribute dimensions.&lt;/p&gt; | [optional] 
**DateMemberNames** | **string** | &lt;p&gt;Date member format in attribute dimensions. Values: &lt;code&gt;MMDDYYYY&lt;/code&gt; for Month first or &lt;code&gt;DDMMYYYY&lt;/code&gt; for day first.&lt;/p&gt; | [optional] 
**NumericRangesRepresent** | **string** | &lt;p&gt;Date range setting for numeric attribute dimensions. Values: &lt;code&gt;UPPER_BOUND_INCLUSIVE&lt;/code&gt;, &lt;code&gt;LOWER_BOUND_INCLUSIVE&lt;/code&gt;, &lt;code&gt;UPPER_BOUND_NON_INCLUSIVE&lt;/code&gt;, &lt;code&gt;LOWER_BOUND_NON_INCLUSIVE&lt;/code&gt; &lt;/p&gt; | [optional] 
**CalcDimensionName** | **string** | &lt;p&gt;The name of the attribute calculations dimension.&lt;/p&gt; | [optional] 
**CalcSumMember** | **string** | &lt;p&gt;In an attribute calculations dimension, the name to use when requesting sum data.&lt;/p&gt; | [optional] 
**CalcCountMember** | **string** | &lt;p&gt;In an attribute calculations dimension, the name to use when requesting count data.&lt;/p&gt; | [optional] 
**CalcMinimumMember** | **string** | &lt;p&gt;In an attribute calculations dimension, the name to use when requesting minimum data.&lt;/p&gt; | [optional] 
**CalcMaximumMember** | **string** | &lt;p&gt;In an attribute calculations dimension, the name to use when requesting maximum data.&lt;/p&gt; | [optional] 
**CalcAverageMember** | **string** | &lt;p&gt;In an attribute calculations dimension, the name to use when requesting average data.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

