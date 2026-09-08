# EssSharp.Model.GridDimension
<p>Dimension object array describing the position of a dimension. Includes name, column, row, and pov. If column = 0, the dimension is located at 0th column in grid. If row = 1, it is located at first row in the grid. If the dimension contains a POV, then that dimension is marked with the pov filter. In the following example, Product (100-10), Market (New York), and Scenario (Actual) are in the POV, Measures (Sales) is on columns, and Year (Jan) is on rows. <table><tr><th>&nbsp;</th><th>100-10</th><th>New York</th><th>Actual</th></tr><tr><td>&nbsp;</td><td><strong>Sales</strong></td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><strong>Jan</strong></td><td>1052</td><td>&nbsp;</td><td>&nbsp;</td></tr></table></p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Pov** | **string** | &lt;p&gt;If dimension is in the POV, the member name. POV is point of view: the starting context or scope for the grid. &lt;/p&gt; | [optional]
**Expanded** | **bool** |  | [optional]
**Row** | **int** | &lt;p&gt;Row location of the dimension in the grid.&lt;/p&gt; | [optional]
**Column** | **int** | &lt;p&gt;Column location of the dimension in the grid.&lt;/p&gt; | [optional]
**DisplayName** | **string** |  | [optional]
**Hidden** | **bool** |  | [optional]
**Name** | **string** | &lt;p&gt;Dimension name.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

