# EssSharp.Model.Preferences

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**CellText** | **bool** |  | [optional]
**ColumnSupression** | [**ColumnSuppression**](ColumnSuppression.md) |  | [optional]
**ZoomIn** | [**ZoomIn**](ZoomIn.md) |  | [optional]
**Navigate** | **bool** |  | [optional]
**IncludeSelection** | **bool** | &lt;p&gt;Display the selected member and the members retrieved as a result of the operation.&lt;/p&gt; | [optional]
**RepeatMemberLabels** | **bool** | &lt;p&gt;Whether to repeat member labels in each row or column cell that represents a data point. Setting to true can help with legibility in large grids that require scrolling.&lt;/p&gt; | [optional]
**WithinSelectedGroup** | **bool** | &lt;p&gt;Perform ad hoc operations only on the selected group of cells, leaving unselected cells as is. This setting is meaningful only when there are two or more dimensions down the grid as rows or across the grid as columns. For Zoom, Keep Only, and Remove Only.&lt;/p&gt; | [optional]
**RemoveUnSelectedGroup** | **bool** | &lt;p&gt;For Zoom In or Zoom Out, remove all dimensions and members except the selected member and the members retrieved as a result of zooming.&lt;/p&gt; | [optional]
**IncludeDescriptionLabel** | **bool** |  | [optional]
**MissingText** | **string** | &lt;p&gt;The string displayed for cells that have no data value. Default is #Missing.&lt;/p&gt; | [optional]
**NoAccessText** | **string** | &lt;p&gt;The string displayed when you do not have the proper security access to view a data value. Default is #NoAccess.&lt;/p&gt; | [optional]
**FormulaRetention** | [**FormulaRetention**](FormulaRetention.md) |  | [optional]
**MaxColumns** | **long** |  | [optional]
**Indentation** | **string** | &lt;p&gt;How hierarchy levels are indented. NONE: No indentation. SUBITEMS: Indent descendants. Ancestors are left-justified in the column. TOTALS: Indent ancestors. Descendants are left-justified in the column.&lt;/p&gt; | [optional]
**RowSupression** | [**RowSuppression**](RowSuppression.md) |  | [optional]
**MaxRows** | **long** |  | [optional]
**LatestMemberName** | **Object** |  | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

