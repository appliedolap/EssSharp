# EssSharp.Model.MemberBean
<p>The member information output that Essbase displays is contextual. The following are examples of properties that may not be displayed when you request information for a member:<ul><li><i>consolidation</i>: not displayed if the member consolidation is the default of Add (+)</li><li><i>numberOfChildren</i>: not displayed for leaf-level members (members with no children)</li><li><i>activeAliasName</i>: not displayed unless a non-default alias table is being used in the session</li><li><i>attributeType</i>: only displayed for members that are attribute dimension names</li><li><i>shareMembers</i>: not displayed for dimension-name members nor attributes, as these cannot be the prototype member for a shared member.</li><li><i>memberHasUniqueName</i>: not displayed unless the outline is duplicate member enabled</li></ul></p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** | &lt;p&gt;Member name.&lt;/p&gt; | [optional] 
**DimensionName** | **string** | &lt;p&gt;Name of the dimension this member belongs to.&lt;/p&gt; | [optional] 
**NumberOfChildren** | **int** | &lt;p&gt;Number of children this member has.&lt;/p&gt; | [optional] 
**LevelNumber** | **int** | &lt;p&gt;The level number of this member. Leaf members are level 0, and the level number increases by 1 with each step closer to the dimension root member.&lt;/p&gt; | [optional] 
**GenerationNumber** | **int** | &lt;p&gt;The generation number of this member. Dimension names are generation 1 members, and the generation number increases by 1 with each step closer to the leaf members.&lt;/p&gt; | [optional] 
**Aliases** | **Dictionary&lt;string, string&gt;** |  | [optional] 
**ActiveAliasName** | **string** | &lt;p&gt;Currently active alias name of this member, if an alias table other than Default is being used in the current session.&lt;/p&gt; | [optional] 
**MemberHasUniqueName** | **bool** | &lt;p&gt;&lt;b&gt;false&lt;/b&gt; if this member name is a duplicate name within the outline, or &lt;b&gt;true&lt;/b&gt; if its name is unique. Applicable only for duplicate member enabled outlines.&lt;/p&gt; | [optional] 
**UniqueName** | **string** | &lt;p&gt;The unique name for this member in the outline. If the outline is duplicate-member enabled and the member name is not unique, then this unique name will be a qualified name that differentiates it from other members with the same name. For example: &lt;code&gt;[Market].[New York].[New York]&lt;/code&gt; is a unique name.&lt;/p&gt; | [optional] 
**MemberId** | **string** | &lt;p&gt;A permanent, unique identifier for a member, separate from its name. When Essbase auto generates member IDs, they follow an incremental naming pattern: id__0, id__1, id__2, etc.&lt;/p&gt; | [optional] 
**UniqueId** | **string** |  | [optional] 
**Type** | **string** | &lt;p&gt;Applicable only if the member is a measure. Which type is designated for the measure; for example, DATE for a measure classified using a date format, SMARTLIST for a text-based measure, NUMERIC.&lt;/p&gt; | [optional] 
**MemberSolveOrder** | **int** | &lt;p&gt;A solve order, if assigned for this member. If not assigned, members inherit the solve order of their dimension. Solve order can be 0-127. Lower solve-order members are calculated before higher.&lt;/p&gt; | [optional] 
**DescendantsCount** | **long** | &lt;p&gt;Number of descendants this member has.&lt;/p&gt; | [optional] 
**PreviousSiblingsCount** | **int** | &lt;p&gt;Number of siblings that precede this member in the outline.&lt;/p&gt; | [optional] 
**Dimension** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if this member is a top-level dimension member (the member name &#x3D; the dimension name).&lt;/p&gt; | [optional] 
**Attribute** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if this member is a member of an attribute dimension.&lt;/p&gt; | [optional] 
**Account** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if this member is a member of an Accounts dimension.&lt;/p&gt; | [optional] 
**Links** | [**List&lt;Link&gt;**](Link.md) |  | [optional] 
**DimSolveOrder** | **int** |  | [optional] 
**DimensionType** | **string** |  | [optional] 
**FormatString** | **string** |  | [optional] 
**DimStorageType** | **string** |  | [optional] 
**CurrencyConversionCategory** | **string** |  | [optional] 
**Uda** | **List&lt;string&gt;** |  | [optional] 
**DataStorageType** | **string** |  | [optional] 
**ParentName** | **string** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

