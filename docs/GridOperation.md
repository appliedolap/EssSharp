# EssSharp.Model.GridOperation
<p>Grid operation to perform, specifying the grid, action, coordinates, and ranges.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Grid** | [**Grid**](Grid.md) |  | [optional]
**Action** | **string** |  | [optional]
**Alias** | **string** | &lt;p&gt;The active alias table for the grid.&lt;/p&gt; | [optional]
**Coordinates** | **List&lt;int&gt;** | &lt;p&gt;Coordinates array for grid operation. Specify using syntax &lt;code&gt;\&quot;coordinates\&quot;: [&lt;i&gt;index&lt;/i&gt;]&lt;/code&gt;, where &lt;i&gt;index&lt;/i&gt; describes a cell position, starting with 0 for the upper-left-most cell, and counting left to right, row by row.&lt;/p&gt; | [optional]
**Ranges** | **List&lt;List&lt;int&gt;&gt;** | &lt;p&gt;Range object used for zoomin, zoomout, keeponly, and removeonly grid operations. Specify one or more ranges as an array using the syntax: &lt;code&gt;\&quot;ranges\&quot;: [&lt;i&gt;rowNo&lt;/i&gt;,&lt;i&gt;colNo&lt;/i&gt;,&lt;i&gt;noOfRows&lt;/i&gt;,&lt;i&gt;noOfCols&lt;/i&gt;]&lt;/code&gt;, where the first argument is row number (start at 0), the second argument is column (start at 0), third argument is number of rows, and the fourth argument is number of columns.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

