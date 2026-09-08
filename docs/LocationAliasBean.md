# EssSharp.Model.LocationAliasBean

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**AliasName** | **string** | &lt;p&gt;Name of the location alias.&lt;/p&gt; | [optional] 
**ConnectionName** | **string** | &lt;p&gt;If the location alias is based on a saved connection, the name of the connection.&lt;/p&gt; | [optional] 
**ServerName** | **string** | &lt;p&gt;If the location alias is not based on a saved connection, the Essbase Server host name of the database/cube to which the location alias refers.&lt;/p&gt; | [optional] 
**UserName** | **string** | &lt;p&gt;If the location alias is not based on a saved connection, the name of a user who is authorized to log in to &lt;i&gt;serverName&lt;/i&gt;. Optional if both cubes are on the same Essbase Server.&lt;/p&gt; | [optional] 
**ApplicationName** | **string** | &lt;p&gt;Application name for the database/cube to which the location alias refers.&lt;/p&gt; | [optional] 
**DatabaseName** | **string** | &lt;p&gt;Name of the database to which the location alias refers.&lt;/p&gt; | [optional] 
**ApplicationLevelConnection** | **bool** | &lt;p&gt;If the location alias is based on a saved connection, this parameter should be &lt;b&gt;true&lt;/b&gt; if the connection is application-level, or &lt;b&gt;false&lt;/b&gt; if the connection is globally defined.&lt;/p&gt; | [optional] 
**Links** | [**List&lt;Link&gt;**](Link.md) |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

