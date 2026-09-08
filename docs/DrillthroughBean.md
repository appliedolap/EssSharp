# EssSharp.Model.DrillthroughBean
<p>Drill through report definition. Determines the access users should have to external information. Drill through report definitions are associated with a cube, and include a column mapping (required), a drillable region (required), and a mapping for runtime parameters (optional - can be used if the Datasource query is parameterized).</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Links** | [**List&lt;Link&gt;**](Link.md) |  | [optional]
**ParameterMapping** | [**Dictionary&lt;string, RunTimeParametersInfo&gt;**](RunTimeParametersInfo.md) | &lt;p&gt;Optional specification of dimension, generation, or level mapping for runtime parameters (if implemented in a Datasource), enabling further customization of drill through report results based on the variable context.&lt;/p&gt; | [optional]
**DrillableRegions** | **List&lt;string&gt;** | &lt;p&gt;Specification of which cell intersections offer access to drill through reports (or a URL). Specify using Essbase member names and (optionally) member-set calculation functions. Example: Market,Year,Scenario,Sales,@LEVMBRS(Product,1)&lt;/p&gt; | [optional]
**UseTempTables** | **bool** | &lt;p&gt;Whether Essbase should create temporary tables for drill through performance improvement for queries that have a large number of values in the SQL ???IN??? clause.&lt;/p&gt; | [optional]
**ColumnMapping** | [**Dictionary&lt;string, ColumnMappingInfo&gt;**](ColumnMappingInfo.md) | &lt;p&gt;Specification of which external source columns should be included in the drill through report, which Essbase dimensions those columns map to, and (optionally) a generation/level filter condition indicating how much depth of access to provide.&lt;/p&gt; | [optional]
**DataSourceName** | **string** | &lt;p&gt;Name of the Datasource used for this drill through report definition.&lt;/p&gt; | [optional]
**Url** | **string** | &lt;p&gt;Static or dynamic URL string. Use if you are defining drill through to external data using URLs.&lt;/p&gt; | [optional]
**Columns** | **List&lt;string&gt;** | &lt;p&gt;List of columns from the external Datasource that you want included in the drill through report.&lt;/p&gt; | [optional]
**Type** | **string** | &lt;p&gt;Type of drill through report definition: &lt;code&gt;DATASOURCE&lt;/code&gt; to drill through to an external source system or &lt;code&gt;URL&lt;/code&gt; to drill through to a web URL.&lt;/p&gt; | [optional]
**Name** | **string** | &lt;p&gt;Name of the drill through report definition.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

