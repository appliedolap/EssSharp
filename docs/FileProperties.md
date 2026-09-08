# EssSharp.Model.FileProperties

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Delimiter** | **string** | &lt;p&gt;If the rule is associated with a delimited file, the delimiter.&lt;/p&gt; | [optional]
**Width** | **int** | &lt;p&gt;If the rule is associated with a fixed-width flat file, the width in bytes.&lt;/p&gt; | [optional]
**DataloadRecordNumber** | **int** | &lt;p&gt;The number of the record to start with when performing a data load.&lt;/p&gt; | [optional]
**DimensionBuildRecordNumber** | **int** | &lt;p&gt;The number of the record to start with when performing a dimension build.&lt;/p&gt; | [optional]
**HeaderRecordNumber** | **int** | &lt;p&gt;The number of header rows to skip when performing a data load or dimension build. For example, 1 if the first record is a header. 0 if there is no header.&lt;/p&gt; | [optional]
**LineSkipCount** | **int** | &lt;p&gt;Number of lines at the top of the data source to skip.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

