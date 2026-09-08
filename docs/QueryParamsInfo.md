# EssSharp.Model.QueryParamsInfo

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Index** | **int** | &lt;p&gt;Ordinal index of the Datasource query parameter. For example, 1 for the first parameter, 2 for the second parameter, etc.&lt;/p&gt; | [optional]
**Name** | **string** | &lt;p&gt;Optional name for the Datasource query parameter, meaningful for your use case. For example, instead of Param1 you can use param_G_month to indicate that the parameter uses a global variable for the current month, or you can rename it to param_appName_month to indicate that the parameter uses an application-level variable for the current month.&lt;/p&gt; | [optional]
**DefaultValue** | **string** | &lt;p&gt;A fixed, default parameter value that the Datasource should use as a fallback in case the parameter has an invalid context at runtime. Example: &lt;b&gt;Jan&lt;/b&gt;. Required only if the Datasource query is parameterized (it includes a &lt;code&gt;?&lt;/code&gt; placeholder for passing a parameter) AND the placeholder is not intended to reference a substitution variable nor a user-defined function developed in the external source.&lt;/p&gt; | [optional]
**Required** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if the Datasource query parameter is required, or &lt;b&gt;false&lt;/b&gt; otherwise.&lt;/p&gt; | [optional]
**UseSubVariable** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if the Datasource query parameter references an Essbase substitution variable, or &lt;b&gt;false&lt;/b&gt; otherwise.&lt;/p&gt; | [optional]
**SubVariableName** | **string** | &lt;p&gt;If &lt;i&gt;useSubVariable&lt;/i&gt; is &lt;b&gt;true&lt;/b&gt;, the name of an Essbase substitution variable.&lt;/p&gt; | [optional]
**Type** | **string** | &lt;p&gt;Datatype of the Datasource query parameter.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

