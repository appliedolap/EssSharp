# EssSharp.Model.OtlEditMain

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**EditActions** | [**List&lt;OtlEditMainEditActionsInner&gt;**](OtlEditMainEditActionsInner.md) | &lt;p&gt;The outline editing action to perform. See example for details on each.&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;b&gt;otlUpdate&lt;/b&gt; - change outline properties&lt;/li&gt;&lt;li&gt;&lt;b&gt;mbrUpdate&lt;/b&gt; - change member properties&lt;/li&gt;&lt;li&gt;&lt;b&gt;mbrAdd&lt;/b&gt; - add a member&lt;/li&gt;&lt;li&gt;&lt;b&gt;mbrDelete&lt;/b&gt; - delete a member&lt;/li&gt;&lt;li&gt;&lt;b&gt;mbrRename&lt;/b&gt; - rename a member&lt;/li&gt;&lt;li&gt;&lt;b&gt;mbrMove&lt;/b&gt; - move a member&lt;/li&gt;&lt;li&gt;&lt;b&gt;mbrAssoc&lt;/b&gt; - make or remove member attribute associations&lt;/li&gt;&lt;li&gt;&lt;b&gt;dimAdd&lt;/b&gt; - add a dimension&lt;/li&gt;&lt;li&gt;&lt;b&gt;smartListAddOrUpdate&lt;/b&gt; - add or update a text list&lt;/li&gt;&lt;li&gt;&lt;b&gt;dimUpdate&lt;/b&gt; - change dimension name or properties&lt;/li&gt;&lt;li&gt;&lt;b&gt;dimAssoc&lt;/b&gt; - make or remove dimension attribute associations &lt;/li&gt;&lt;li&gt;&lt;b&gt;markForDelete&lt;/b&gt; - mark a dimension to be deleted&lt;/li&gt;&lt;li&gt;&lt;b&gt;deleteMarked&lt;/b&gt; - delete marked dimension&lt;/li&gt;&lt;li&gt;&lt;b&gt;sortChildren&lt;/b&gt; - sort a hierarchy&lt;/li&gt;&lt;/ul&gt; | [optional] 
**OtlVersion** | **int** |  | [optional] 
**Validate** | **bool** | &lt;p&gt;Whether to validate the outline: &lt;code&gt;true&lt;/code&gt; or &lt;code&gt;false&lt;/code&gt;&lt;/p&gt; | [optional] 
**ValidateFormulas** | **bool** | &lt;p&gt;Whether to validate formulas: &lt;code&gt;true&lt;/code&gt; or &lt;code&gt;false&lt;/code&gt;&lt;/p&gt; | [optional] 
**KeepTransaction** | **bool** | &lt;p&gt;Whether to keep transactions: &lt;code&gt;true&lt;/code&gt; or &lt;code&gt;false&lt;/code&gt;&lt;/p&gt; | [optional] 
**RestructOption** | **string** | &lt;p&gt;Restructure option. &lt;code&gt;ALL_DATA&lt;/code&gt; to preserve all existing data (this is the default), &lt;code&gt;NO_DATA&lt;/code&gt; to discard all existing data, &lt;code&gt;LOW_DATA&lt;/code&gt; to preserve existing level 0 blocks (applicable to block storage only), &lt;code&gt;IN_DATA&lt;/code&gt; to preserve existing input-level blocks (applicable to block storage only).&lt;/p&gt; | [optional] 
**JsonformatLog** | **bool** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

