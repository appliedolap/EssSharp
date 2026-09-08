# EssSharp.Model.Field
<p>Field properties for performing load rule operations at the record level. For example, you can filter to select or reject certain records before they are loaded into the cube.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**RejectFilters** | [**List&lt;Filter&gt;**](Filter.md) | &lt;p&gt;Rejection filter criteria for omitting data load or dimension build records while loading to the Essbase cube. Filtration options include string or numeric matching of specific values, logical join options, and case sensitivity. &lt;/p&gt; | [optional] 
**SelectFilters** | [**List&lt;Filter&gt;**](Filter.md) | &lt;p&gt;Selection filter criteria for approval of data load or dimension build records while loading to the Essbase cube. Filtration options include string or numeric matching of specific values, logical join options, and case sensitivity.&lt;/p&gt; | [optional] 
**ReplaceInformation** | [**List&lt;ReplaceInfo&gt;**](ReplaceInfo.md) | &lt;p&gt;Replacement value specification, if the filter is designed for finding and replacing values in source records as you load them to Essbase.&lt;/p&gt; | [optional] 
**SelectFilterJoinOption** | **string** | &lt;p&gt;If you define more than one selection filter, choose a logical join operator, AND or OR. AND means that all the defined selection criteria must apply (if even one criterion is not met for any given record, then the filter does not apply to that record). OR means the opposite (if even one criterion is met for a given record, the filter applies).&lt;/p&gt; | [optional] 
**RejectFilterJoinOption** | **string** | &lt;p&gt;If you define more than one rejection filter, choose a logical join operator, AND or OR. AND means that all the defined rejection criteria must apply (if even one criterion is not met for any given record, then the filter does not apply to that record). OR means the opposite (if even one criterion is met for a given record, the filter applies).&lt;/p&gt; | [optional] 
**DateFormat** | **string** | &lt;p&gt;Date format for the field, if applicable.&lt;/p&gt; | [optional] 
**Name** | **string** |  | [optional] 
**Prefix** | **string** | &lt;p&gt;Prefix for the field, if applicable.&lt;/p&gt; | [optional] 
**Suffix** | **string** | &lt;p&gt;Suffix for the field, if applicable.&lt;/p&gt; | [optional] 
**Option** | **byte[]** |  | [optional] 
**ConvertSpaceToUnderScore** | **bool** | &lt;p&gt;Convert spaces in source data fields to underscores while loading to Essbase.&lt;/p&gt; | [optional] 
**Trim** | **bool** | &lt;p&gt;Trim leading or trailing spaces from around source data fields so that they map correctly to Essbase member names.&lt;/p&gt; | [optional] 
**Width** | **double** | &lt;p&gt;Width of the field, if applicable (if the source data fields are fixed width).&lt;/p&gt; | [optional] 
**SmartList** | **string** | &lt;p&gt;Text list object (smart list) associated with the field, if applicable.&lt;/p&gt; | [optional] 
**DimensionBuildOptions** | [**FieldDimBuildOptions**](FieldDimBuildOptions.md) |  | [optional] 
**DataloadOptions** | [**FieldDataLoadOptions**](FieldDataLoadOptions.md) |  | [optional] 
**Transform** | [**Transform**](Transform.md) |  | [optional] 
**Case** | **string** | &lt;p&gt;Select a character case-conversion operation to perform on incoming data while loading to Essbase. NOOP: perform no conversion. LOWER_CASE: convert upper case to lower case. UPPER_CASE: convert lower case to upper case. FIRST_CAPITAL_CASE: convert the first character to upper case and the remaining characters to lower case.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

