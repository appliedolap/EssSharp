# EssSharp.Model.SQLProperties
<p>SQL Properties enable connectivity to an external system such as an RDBMS to extract data from a relational system using a query.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Server** | **string** | &lt;p&gt;Connection string or Datasource info to establish SQL-based connectivity. Example for Datasource: &lt;code&gt;REST;URL&#x3D;LOCAL;DS&#x3D;Orcl_DS&lt;/code&gt;. For Oracle Database with SID: &lt;code&gt;oracle://somedb99:1521/orcl&lt;/code&gt;. For Oracle Database with service name: &lt;code&gt;ORACLESERVICE:oracle://somedb99:1234/esscs.host1.oraclecloud.com&lt;/code&gt;. For Microsoft SQL Server: &lt;code&gt;sqlserver://myMSSQLHost:1433:myDbName&lt;/code&gt;. For MySQL: &lt;code&gt;mysql://HostName:3306:DBName&lt;/code&gt;. For Teradata: &lt;code&gt;teradata://192.0.2.110:1025/myDBName&lt;/code&gt;. For IBM DB2:  &lt;code&gt;db2://myDB2Host:1234:myDbName&lt;/code&gt;.&lt;/p&gt; | [optional] 
**Application** | **string** | &lt;p&gt;Essbase application name, if connectivity is to another Essbase cube.&lt;/p&gt; | [optional] 
**Database** | **string** | &lt;p&gt;Essbase database name, if connectivity is to another Essbase cube.&lt;/p&gt; | [optional] 
**Dictionary** | **string** |  | [optional] 
**Select** | **string** | &lt;p&gt;The SELECT clause of the query, excluding SELECT.&lt;/p&gt; | [optional] 
**From** | **string** | &lt;p&gt;The FROM clause of the query, excluding FROM.&lt;/p&gt; | [optional] 
**Where** | **string** | &lt;p&gt;The WHERE clause of the query, excluding WHERE.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

