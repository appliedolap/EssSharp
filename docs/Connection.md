# EssSharp.Model.Connection

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**Description** | **string** | &lt;p&gt;A descriptive string for this connection.&lt;/p&gt; | [optional]
**Name** | **string** | &lt;p&gt;A name for this connection.&lt;/p&gt; |
**Type** | **string** | &lt;p&gt;Required. Type of connection. &lt;code&gt;FILE&lt;/code&gt; to connect to a file on the server, &lt;code&gt;DB&lt;/code&gt; to connect to an external source system, or &lt;code&gt;ESSBASE&lt;/code&gt; to connect to another cube.&lt;/p&gt; |
**Path** | **string** | &lt;p&gt;Required for file type connections. Catalog path to the file source of data. Example: /gallery/Technical/Drill Through/DrillthroughDS.csv&lt;/p&gt; | [optional]
**Catalog** | **bool** |  | [optional]
**Host** | **string** | &lt;p&gt;Host server name or IP. Required for Oracle Database connections. Required for Essbase connections, unless &lt;i&gt;dbURL&lt;/i&gt; is used instead.&lt;/p&gt; | [optional]
**Port** | **int** | &lt;p&gt;Port number on the remote host. Required for connections when &lt;i&gt;host&lt;/i&gt; is given.&lt;/p&gt; | [optional]
**User** | **string** | &lt;p&gt;Required. User ID with authorization to access the remote source for this connection.&lt;/p&gt; | [optional]
**Password** | **string** | &lt;p&gt;Required. Password of the user ID with authorization to access the remote source for this connection.&lt;/p&gt; | [optional]
**Encrypted** | **bool** |  | [optional]
**Token** | **string** |  | [optional]
**Sid** | **string** | &lt;p&gt;The Oracle System ID (SID) that uniquely identifies an Oracle Database. Required for Oracle Database connections unless &lt;i&gt;service&lt;/i&gt; is used instead.&lt;/p&gt; | [optional]
**Service** | **string** | &lt;p&gt;Service name, if you are defining an Oracle Database connection.&lt;/p&gt; | [optional]
**Schema** | **string** |  | [optional]
**DbURL** | **string** | &lt;p&gt;URL to an external RDBMS database, or, discovery URL to an Essbase instance. For connections to other Essbase instances, this parameter is an alternative to providing the &lt;i&gt;host&lt;/i&gt; and &lt;i&gt;port&lt;/i&gt;. Example of Essbase discovery URL &lt;code&gt;https://192.0.2.1:443/essbase/agent&lt;/code&gt;. For examples of other uses, see documentation for global Get Connection endpoint.&lt;/p&gt; | [optional]
**DbDriver** | **string** | &lt;p&gt;Optional. If &lt;i&gt;type&lt;/i&gt; is &lt;code&gt;DB&lt;/code&gt; and you are configuring Essbase to use a generic JDBC driver, provide the fully qualified class name of the JDBC driver. For example, &lt;code&gt;oracle.jdbc.driver.OracleDriver&lt;/code&gt;.&lt;/p&gt; | [optional]
**Datasource** | **string** |  | [optional]
**Subtype** | **string** | &lt;p&gt;The type of external source. Supported sources and versions are listed in the Database section of the certification matrix (Platform SQL table).&lt;/p&gt; | [optional]
**WalletPath** | **string** | &lt;p&gt;Path to a wallet file, if required for your connection to Autonomous Data Warehouse (if &lt;i&gt;repoWallet&lt;/i&gt; &#x3D; false). Example: &lt;code&gt;/system/wallets/EssbaseADWS&lt;/code&gt;. Obtain a wallet file by selecting Download Client Credentials (Wallet) from your Autonomous Data Warehouse Administration page in Oracle Cloud Infrastructure. If you are using a connection which is already available (a repository connection), you do not need to upload a wallet.&lt;/p&gt; | [optional]
**RepoWallet** | **bool** | &lt;p&gt;Set to &lt;b&gt;true&lt;/b&gt; if you are using an Autonomous Data Warehouse connection which is already available (a repository connection). In this case, you do not need to upload a wallet.&lt;/p&gt; | [optional]
**MinPoolSize** | **int** | &lt;p&gt;Minimum connection pool size. Default is 5. If you get connection errors you may need to adjust minimum and maximum connection pool sizes. See &lt;b&gt;About Controlling the Pool Size in UCP&lt;/b&gt; in &lt;i&gt;Universal Connection Pool Developer&#39;s Guide&lt;/i&gt;.&lt;/p&gt; | [optional]
**MaxPoolSize** | **int** | &lt;p&gt;Maximum connection pool size. Default is 50. If you get connection errors you may need to adjust minimum and maximum connection pool sizes. See &lt;b&gt;About Controlling the Pool Size in UCP&lt;/b&gt; in &lt;i&gt;Universal Connection Pool Developer&#39;s Guide&lt;/i&gt;.&lt;/p&gt; | [optional]
**Hidden** | **bool** |  | [optional]
**Availability** | **string** |  | [optional]
**AiConnection** | **string** |  | [optional]
**ChatCredential** | **string** |  | [optional]
**VectorCredential** | **string** |  | [optional]
**ChatModel** | **string** |  | [optional]
**OciCompartmentId** | **string** |  | [optional]
**Region** | **string** |  | [optional]
**Url** | **string** |  | [optional]
**EmbedModel** | **string** |  | [optional]
**TransferTimeout** | **string** |  | [optional]
**OciAPIFormat** | **string** |  |
**NarrateIndexName** | **string** |  | [optional]
**NarrateProfileName** | **string** |  | [optional]
**NarrateDocDirectory** | **string** |  | [optional]
**Links** | [**List&lt;Link&gt;**](Link.md) |  | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

