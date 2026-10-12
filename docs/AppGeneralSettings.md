# EssSharp.Model.AppGeneralSettings
<p>General application settings, including options for startup, expiration of locks on data, EAS-managed applications, and maximum LRO file sizes.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Description** | **string** | &lt;p&gt;Description of the application.&lt;/p&gt; | [optional] 
**LogLevelAsString** | **string** | &lt;p&gt;Log level. INFO, WARN, ERROR, FATAL, or DEBUG.&lt;/p&gt; | [optional] 
**EasManagedApp** | **bool** | &lt;p&gt;If &lt;b&gt;true&lt;/b&gt;, the application is managed in Essbase Administration Services (EAS) Lite instead of the Essbase web interface. Before you can connect to an application in EAS Lite, you must set it as an EAS managed application. The Essbase web interface is the modern administration interface that supports all current platform features, but EAS Lite is available as a limited-option alternative available only for Essbase 21c independent deployment.&lt;/p&gt; | [optional] 
**TimeoutOnDataBlockLocks** | **long** | &lt;p&gt;Maximum time interval that locks on data blocks can be held by Smart View (or other grid client) users. When a client data-block lock is held for more than the time out interval, Essbase removes the lock and the transaction is rolled back. The default interval is 60 minutes. This setting affects all databases in the application.&lt;/p&gt; | [optional] 
**MaxAttachmentFileSizeInKbs** | **long** | &lt;p&gt;Maximum file size for Linked Reporting Object(LRO) attachments. There is no default. There is no minimum or maximum value, excepting limitations imposed by your system resources.&lt;/p&gt; | [optional] 
**PendingCacheSizeLimitInMbs** | **long** | &lt;p&gt;Maximum size to which the aggregate storage cache may grow. The aggregate storage cache grows dynamically until it reaches this limit. This setting takes effect after you restart the application. This setting is ignored if ASODEFAULTCACHESIZE configuration property is in use.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

