# EssSharp.Model.Datasource

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Type** | **string** |  | 
**Connection** | **string** | &lt;p&gt;Connection used for this Datasource.&lt;/p&gt; | 
**Description** | **string** | &lt;p&gt;Optional description of this Datasource.&lt;/p&gt; | [optional] 
**Columns** | [**ColumnsType**](ColumnsType.md) |  | [optional] 
**Name** | **string** | &lt;p&gt;Datasource name. Must begin with a letter. Can contain only letters, underscore character, and digits.&lt;/p&gt; | [optional] 
**IgnoreErrorRecords** | **bool** |  | [optional] 
**Delimeter** | **string** | &lt;p&gt;Delimiter of the data records, if Datasource &lt;i&gt;type&lt;/i&gt; is &lt;code&gt;DELIMITEDFILE&lt;/code&gt;. Delimiter can be &lt;code&gt;Comma&lt;/code&gt; for CSV format or &lt;code&gt;Tab&lt;/code&gt; for tab-separated format. To use a custom delimiter, use value &lt;code&gt;Custom&lt;/code&gt;, and provide the delimiter as a value to &lt;i&gt;customDelimiter&lt;/i&gt;.&lt;/p&gt; | [optional] 
**CustomDelimiter** | **string** | &lt;p&gt;Custom delimiter of the data records, if the value provided for &lt;i&gt;delimiter&lt;/i&gt; is &lt;code&gt;Custom&lt;/code&gt;.&lt;/p&gt; | [optional] 
**Query** | **string** | &lt;p&gt;Query associated with the Datasource. For example, a SQL query for an external database, or an MDX query for another Essbase cube. The query selects which data you want to make available in this Datasource.&lt;/p&gt; | [optional] 
**Application** | **string** | &lt;p&gt;Applicable if the &lt;i&gt;type&lt;/i&gt; of Datasource is ESSBASE. The Essbase application name.&lt;/p&gt; | [optional] 
**Cube** | **string** | &lt;p&gt;Applicable if the &lt;i&gt;type&lt;/i&gt; of Datasource is ESSBASE. The Essbase database name.&lt;/p&gt; | [optional] 
**StartRow** | **long** | &lt;p&gt;Optional (default is 1 if not given). For a Datasource that is an Excel or text file, the starting data row number, excluding headerRow if one exists. For example, if headerRow is specified as 1 and startRow is specified as 10, the actual starting data row will be 11.&lt;/p&gt; | [optional] 
**EndRow** | **long** | &lt;p&gt;For a Datasource that is an Excel or text file, the ending row number.&lt;/p&gt; | [optional] 
**HeaderRow** | **long** | &lt;p&gt;For a Datasource that is an Excel or text file, the header row number. 0 if there is no header.&lt;/p&gt; | [optional] 
**Sheet** | **string** | &lt;p&gt;For a Datasource that is an Excel file, the worksheet name.&lt;/p&gt; | [optional] 
**SkipHiddenRows** | **bool** |  | [optional] 
**Widths** | **List&lt;long&gt;** |  | [optional] 
**QueryParameters** | [**List&lt;QueryParamsInfo&gt;**](QueryParamsInfo.md) | &lt;p&gt;Parameter implementation details, if the Datasource query is parameterized. For example, if the query includes a &lt;code&gt;?&lt;/code&gt; placeholder for passing a parameter, as in the following query: &lt;code&gt;select * from profit_data where year&#x3D;?&lt;/code&gt;, then you need define the implementation details.&lt;/p&gt; | [optional] 
**Headers** | [**List&lt;HeaderType&gt;**](HeaderType.md) |  | [optional] 
**Links** | [**List&lt;Link&gt;**](Link.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

