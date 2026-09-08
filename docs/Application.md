# EssSharp.Model.Application

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Name** | **string** |  | [optional]
**Owner** | **string** |  | [optional]
**CreationTime** | **long** |  | [optional]
**ModifiedBy** | **string** |  | [optional]
**ModifiedTime** | **long** |  | [optional]
**Status** | **string** |  | [optional]
**Description** | **string** |  | [optional]
**Type** | **string** |  | [optional]
**StartTime** | **long** |  | [optional]
**ConnectedUsersCount** | **int** | &lt;p&gt;The number of users currently connected to the application.&lt;/p&gt; | [optional]
**Role** | **string** |  | [optional]
**Links** | [**List&lt;Link&gt;**](Link.md) |  | [optional]
**EasManagedApp** | **bool** | &lt;p&gt;If &lt;b&gt;true&lt;/b&gt;, the application is managed in Essbase Administration Services (EAS) Lite instead of the Essbase web interface. Before you can connect to an application in EAS Lite, you must set it as an EAS managed application. The Essbase web interface is the modern administration interface that supports all current platform features, but EAS Lite is available as a limited-option alternative available only for Essbase 21c independent deployment.&lt;/p&gt; | [optional]
**StartStopAppAllowed** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if users who have at least read permission can start the application.&lt;/p&gt; | [optional]
**InspectAppAllowed** | **bool** |  | [optional]
**AppVariablesSetting** | [**VariablesSetting**](VariablesSetting.md) |  | [optional]
**Encrypted** | **bool** |  | [optional]
**AiConnection** | **string** |  | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

