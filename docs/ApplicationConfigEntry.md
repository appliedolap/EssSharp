# EssSharp.Model.ApplicationConfigEntry

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Key** | **string** | &lt;p&gt;Filter key to refine the configuration property results. For example if &lt;code&gt;key&#x3D;A*&lt;/code&gt;, all configuration properties beginning with the letter A are returned. Default is *&lt;/p&gt; | [optional]
**Description** | **string** | &lt;p&gt;A description of the configuration property.&lt;/p&gt; | [optional]
**Syntax** | **string** | &lt;p&gt;The syntax for the configuration property. Example: &lt;code&gt;DATACACHESIZE n&lt;/code&gt;&lt;/p&gt; | [optional]
**Example** | **string** | &lt;p&gt;Usage example for the configuration property. Example: &lt;code&gt;DATACACHESIZE 90M&lt;/code&gt;&lt;/p&gt; | [optional]
**Value** | **string** | &lt;p&gt;Value for the configuration property. Example: &lt;code&gt;1024&lt;/code&gt;&lt;/p&gt; | [optional]
**Configured** | **bool** | the configuration property is enabled. | [optional]
**Links** | [**List&lt;Link&gt;**](Link.md) |  | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

