# EssSharp.Model.NamedQueriesPreferences

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Dataless** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to omit data values from the output set. Default is &lt;b&gt;false&lt;/b&gt;.&lt;/p&gt; | [optional]
**HideRestrictedData** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to hide the restricted cell data in the output.&lt;/p&gt; | [optional]
**CellAttributes** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to enable query result to return a set of metadata attributes besides the actual data value.&lt;/p&gt; | [optional]
**FormatString** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to return the formatted values for cells of type text or date, or cells associated with a format string. Default is &lt;b&gt;true&lt;/b&gt;.&lt;/p&gt; | [optional]
**FormatValues** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to return the formatted values for cells.&lt;/p&gt; | [optional]
**MeaninglessCells** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to suppress the meaningless cell data in the output. For example, missing or structurally irrelevant cells.&lt;/p&gt; | [optional]
**TextList** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to return a comma-separated list of all text values associated with the cell in the output.&lt;/p&gt; | [optional]
**UrlDrillThrough** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to add URLs to each applicable cell so users can click through to the source data. This works only if the Essbase drill-through links are configured.&lt;/p&gt; | [optional]
**MemberIdentifierType** | **string** | &lt;p&gt;Specify whether metadata in the output should refer to member names, member aliases, or unique member names (in case of duplicate member enabled outlines).&lt;/p&gt; | [optional]
**AliasTableName** | **string** | &lt;p&gt;When the &lt;b&gt;memberIdentifierType: ALIAS&lt;/b&gt; and the &lt;b&gt;aliasTable&lt;/b&gt; are set to a valid alias table name, the resulting mdx grid contains member names from the specified table. If the value of this property is not set then names are picked from the &lt;b&gt;Default&lt;/b&gt; alias table.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

