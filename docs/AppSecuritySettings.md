# EssSharp.Model.AppSecuritySettings
<p>Application security settings.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AllowCommands** | **bool** | &lt;p&gt;If &lt;b&gt;true&lt;/b&gt;, all users with sufficient permissions can make requests to the database (cube) in the application. By default, commands are enabled. If set to &lt;b&gt;false&lt;/b&gt;, no requests are permitted by any user including administrators. The &lt;b&gt;false&lt;/b&gt; setting remains in effect only for the duration of the issuer&#39;s session. The &lt;b&gt;false&lt;/b&gt; setting takes effect immediately, and affects users who are currently logged in, as well as users who log in later during the issuer&#39;s session.&lt;/p&gt; | [optional]
**AllowConnects** | **bool** | &lt;p&gt;If &lt;b&gt;true&lt;/b&gt;, all users with sufficient permissions can make connections to the database (cube) in the application. By default, connections are enabled. If &lt;b&gt;false&lt;/b&gt;, no user with a permission lower than Application Manager can start the cube nor make connections that require the cube to be started.&lt;/p&gt; | [optional]
**AllowUpdates** | **bool** | &lt;p&gt;If &lt;b&gt;true&lt;/b&gt;, all users with sufficient permissions can make requests to the databases (cube) in the application. By default, updates are enabled. If set to &lt;b&gt;false&lt;/b&gt;, updates are not permitted, lasting for the duration of the issuer&#39;s session.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

