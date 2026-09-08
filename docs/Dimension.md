# EssSharp.Model.Dimension

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Generations** | [**List&lt;Level&gt;**](Level.md) |  | [optional]
**Levels** | [**List&lt;Level&gt;**](Level.md) |  | [optional]
**AllowassociationChanges** | **bool** |  | [optional]
**AllowFormulaChanges** | **bool** |  | [optional]
**AllowPropertyChanges** | **bool** |  | [optional]
**AllowUDAChanges** | **bool** |  | [optional]
**MeasureOptions** | [**MeasureOptions**](MeasureOptions.md) |  | [optional]
**AggregateLevelUsage** | **string** |  | [optional]
**AddMemberOption** | **string** | &lt;p&gt;The build method algorithm to add, change, or remove dimensions, members, and aliases in this dimension. Should be based on the properties of the source data; for example, if each record specifies a parent and later a child member, in that order, then PARENT_CHILD is a good method.&lt;/p&gt; | [optional]
**AttributeOptions** | [**AttributeOptions**](AttributeOptions.md) |  | [optional]
**ConfigOption** | **string** |  | [optional]
**Unique** | **string** |  | [optional]
**HierarchyType** | **string** | &lt;p&gt;MULTIPLE if multiple hierarchies should be enabled in this dimension (applicable to aggregate storage cube only). EXISTING to keep the setting unchanged. STORED for aggregate storage stored hierarchy type (no formulas). DYNAMIC for aggregate storage hierarchy type that aggregates using formulas (includes Accounts dimension).&lt;/p&gt; | [optional]
**SortOption** | **string** | &lt;p&gt;Whether and how to sort this dimension&#39;s children in hierarchies: None (do not sort), ascending, or descending.&lt;/p&gt; | [optional]
**StorageType** | **string** |  | [optional]
**Type** | **string** |  | [optional]
**UpdateOption** | **string** | &lt;p&gt;Incremental dimension build update option. Incremental dimension building is the process of updating existing dimensions by adding, moving, deleting, and reordering members. MERGE (the default) adds new members while retaining existing members. REMOVE_UNSPECIFIED removes members that are not specified in the source (available only when &lt;i&gt;addMemberOption&lt;/i&gt; &#x3D; &lt;code&gt;GENERATION&lt;/code&gt;, &lt;code&gt;LEVEL&lt;/code&gt;, or &lt;code&gt;PARENT_CHILD&lt;/code&gt;). Outlines are invalid if removing members results in level 0 Dynamic Calc members without formulas. RESET_DIMENSION is available only for Index-based rules (i.e. &lt;i&gt;studio&lt;/i&gt; &#x3D; &lt;code&gt;true&lt;/code&gt;). Reset Dimension rebuilds the entire dimension. This option enables insert, reorder, move, and delete operations during incremental dimension build, persisting member placement in the hierarchy instead of moving them to the end. To use Reset Dimension, &lt;i&gt;allowMoves&lt;/i&gt; should be &lt;code&gt;NOTOK&lt;/code&gt;; otherwise, you cannot build shared hierarchies.&lt;/p&gt; | [optional]
**AllowMoves** | **string** | &lt;p&gt;Set to &lt;b&gt;OK&lt;/b&gt; to allow moving of members and their children to new parents, allowing Essbase to recognize primary members and match them with the source data. Not available for duplicate member outlines. Leave as &lt;b&gt;NOTOK&lt;/b&gt; if &lt;i&gt;updateOption&lt;/i&gt; &#x3D; &lt;code&gt;RESET_DIMENSION&lt;/code&gt;.&lt;/p&gt; | [optional]
**SolveOrder** | **int** | &lt;p&gt;Priority in which the dimension or member is calculated (0 to 127). Higher solve order means the member is calculated later. Members not assigned a solve order inherit it from their dimension.&lt;/p&gt; | [optional]
**CreateAttributeMembers** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to allow Essbase to create attribute members. If &lt;b&gt;false&lt;/b&gt;, prevents Essbase from creating attribute members.&lt;/p&gt; | [optional]
**Share** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; if the dimension includes shared members.&lt;/p&gt; | [optional]
**IncrementalSort** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; to sort members after Essbase has processed and added all members from the source data.&lt;/p&gt; | [optional]
**AutoFixSharedMember** | **bool** |  | [optional]
**Flexible** | **bool** |  | [optional]
**MemberName** | **string** | &lt;p&gt;Parent member name under which to add children. Used with Add as child of build method.&lt;/p&gt; | [optional]
**Name** | **string** |  | [optional]
**DimensionSolveOrder** | **int** | &lt;p&gt;Solve order of the dimension.&lt;/p&gt; | [optional]
**Added** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; for a new dimension that does not yet exist in the outline. If &lt;b&gt;true&lt;/b&gt;, the new dimension will be created during the dimension build. If &lt;b&gt;false&lt;/b&gt;, dimension build will fail for a new dimension.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

