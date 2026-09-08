# EssSharp.Model.TransactionSettings

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**CommittedAccess** | **bool** | &lt;p&gt;If &lt;b&gt;true&lt;/b&gt;, sets the isolation level to committed access, meaning that only one transaction at a time can update data blocks, and Essbase holds read/write locks on all data blocks until the transaction and the commit operations are performed. If concurrencyPreImageAccess is enabled, users (or transactions) can still have read-only access to data at its last commit point. The default is &lt;b&gt;false&lt;/b&gt;.&lt;/p&gt; | [optional] 
**ConcurrencyWaitSeconds** | **int** | &lt;p&gt;Lock timeout interval; number of seconds to wait for blocks to be unlocked when the database is in committed mode. If a transaction request is made that cannot be granted in the allotted time, the transaction is rolled back until a lock can be granted. Applicable only when committedAccess is &lt;b&gt;true&lt;/b&gt;.&lt;/p&gt; | [optional] 
**ConcurrencyPreImageAccess** | **bool** | &lt;p&gt;If &lt;b&gt;true&lt;/b&gt;, allow users (or other transactions) read-only access to data at its last commit point  when the cube is in committed mode (meaning that data blocks may be locked for the duration of a concurrent transaction). The default is &lt;b&gt;true&lt;/b&gt;, when committedAccess is enabled.&lt;/p&gt; | [optional] 
**CommitBlocks** | **long** | &lt;p&gt;The number of data blocks updated before an explicit commit is performed (during calculation and grid updates). Applicable when committedAccess is &lt;b&gt;false&lt;/b&gt;.&lt;/p&gt; | [optional] 
**CommitRows** | **long** | &lt;p&gt;The number of rows of the input file processed before an explicit commit is performed (during data load). Applicable when committedAccess is &lt;b&gt;false&lt;/b&gt;.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

