# EssSharp.Model.PartitionBean

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Id** | **string** | &lt;p&gt;Partition ID.&lt;/p&gt; | [optional]
**Type** | **string** | &lt;p&gt;Partition type. TRANSPARENT, REPLICATED, or FEDERATED.&lt;/p&gt; | [optional]
**Updatable** | **bool** | &lt;p&gt;Whether the partition definition can be updated.&lt;/p&gt; | [optional]
**IsNew** | **bool** | &lt;p&gt;Whether the partition definition is new.&lt;/p&gt; | [optional]
**Locked** | **bool** | &lt;p&gt;Whether the partition object is locked.&lt;/p&gt; | [optional]
**FederatedTypeCR** | **bool** | &lt;p&gt;Obsolete.&lt;/p&gt; | [optional]
**FederatedTypeAV** | **bool** | &lt;p&gt;Whether federated partition to Autonomous Database is enabled. If true, the cube data is stored in Autonomous Data Warehouse, and Analytic View objects are used for Essbase metadata.&lt;/p&gt; | [optional]
**SourceInfo** | [**ConnectionInfoBean**](ConnectionInfoBean.md) |  | [optional]
**TargetInfo** | [**ConnectionInfoBean**](ConnectionInfoBean.md) |  | [optional]
**Areas** | [**List&lt;AreaBean&gt;**](AreaBean.md) |  | [optional]
**Mappings** | [**List&lt;MemberMappingBean&gt;**](MemberMappingBean.md) |  | [optional]
**ErrorMessage** | **string** | &lt;p&gt;An error message associated with partition validation.&lt;/p&gt; | [optional]
**Links** | [**List&lt;Link&gt;**](Link.md) |  | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

