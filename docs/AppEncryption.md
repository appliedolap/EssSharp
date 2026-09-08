# EssSharp.Model.AppEncryption

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Type** | **string** | &lt;p&gt;The supported encryption type. The only supported value is &lt;code&gt;OCID&lt;/code&gt;, representing Oracle Vault key management.&lt;/p&gt; | 
**Key** | **byte[]** |  | [optional] 
**VaultId** | **string** | &lt;p&gt;Required for OCID encryption type. The OCID of the Oracle Vault in OCI.&lt;/p&gt; | [optional] 
**MasterKeyId** | **string** | &lt;p&gt;Required for OCID encryption type. The OCID of the Master Encryption Key (MEK) in the Oracle Vault in OCI.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

