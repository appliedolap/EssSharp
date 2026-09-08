# EssSharp.Model.ConnectionInfoBean

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ConnectionName** | **string** | &lt;p&gt;Connection name used for partition connectivity. Required for partitions accessing data outside of a single Essbase instance.&lt;/p&gt; | [optional] 
**ServerName** | **string** | &lt;p&gt;URL of the Essbase server, ending in &lt;code&gt;essbase/agent&lt;/code&gt;.&lt;/p&gt; | [optional] 
**UserName** | **string** | &lt;p&gt;Partition username, if authentication info is embedded in the partition definition.&lt;/p&gt; | [optional] 
**Password** | **string** | &lt;p&gt;Partition user password, if authentication info is embedded in the partition definition.&lt;/p&gt; | [optional] 
**Description** | **string** |  | [optional] 
**ApplicationName** | **string** | &lt;p&gt;Name of application.&lt;/p&gt; | [optional] 
**DatabaseName** | **string** | &lt;p&gt;Name of Essbase database/cube.&lt;/p&gt; | [optional] 
**DatasourceName** | **string** | &lt;p&gt;Datasource name used for partition connectivity.&lt;/p&gt; | [optional] 
**MeasuresDimensionName** | **string** | &lt;p&gt;Pivot dimension name defined in federated partition. Can be Measures dimension but does not have to be.&lt;/p&gt; | [optional] 
**SchemaName** | **string** | &lt;p&gt;The name of the database schema (used as the repository database for federated partition).&lt;/p&gt; | [optional] 
**FactTableName** | **string** | &lt;p&gt;The name of the fact table in Autonomous Database that stores numeric values and keys.&lt;/p&gt; | [optional] 
**IsFactManagedByFederatedCube** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if the federated partition fact table is managed by Essbase, instead of by the Autonomous Data Warehouse schema administrator.&lt;/p&gt; | [optional] 
**EssbaseToColumnMap** | [**EsbToColMap**](EsbToColMap.md) |  | [optional] 
**EssbaseToDataSourceMap** | [**EssToDsMapDTO**](EssToDsMapDTO.md) |  | [optional] 
**AlterCredentials** | **bool** |  | [optional] 
**ApplicationLevelConnection** | **bool** | &lt;p&gt;Must be specified as &lt;b&gt;true&lt;/b&gt; if the connection is defined at the application level rather than globally.&lt;/p&gt; | [optional] 
**ApplicationLevelDatasource** | **bool** | &lt;p&gt;Must be specified as &lt;b&gt;true&lt;/b&gt; if the Datasource is defined at the application level rather than globally.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

