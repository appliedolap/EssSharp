# EssSharp.Model.DataLoadBuffer

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**BufferId** | **long** | &lt;p&gt;Unique ID of a single aggregate storage data load buffer. Must be a number between 1 and 4294967296.&lt;/p&gt; | [optional]
**DuplicateAggregationMethod** | **string** | &lt;p&gt;Select an option to resolve cell conflicts for duplicate cells in the aggregate storage data load buffer.&lt;/p&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;ADD&lt;/code&gt;: (Default) Add values when the buffer contains multiple values for the same cell.&lt;/li&gt;&lt;li&gt;&lt;code&gt;ASSUME_EQUAL&lt;/code&gt;: Treat duplicate values as equal. &lt;/li&gt;&lt;li&gt;&lt;code&gt;USE_LAST&lt;/code&gt;: Combine duplicate cells by using the value of the cell that was loaded last into the data load buffer.&lt;/li&gt;&lt;/ul&gt; | [optional]
**LoadBufferOptions** | **string** | &lt;p&gt;Select an option to determine how missing and zero values in the aggregate storage data load buffer should be handled.&lt;/p&gt; &lt;ul&gt;&lt;li&gt;&lt;code&gt;IGNORE_NONE(0)&lt;/code&gt;: Do not ignore any values in the incoming data stream&lt;/li&gt;&lt;li&gt;&lt;code&gt;IGNORE_MISSING_VALUES(1)&lt;/code&gt;: Ignore #MI values in the incoming data stream&lt;/li&gt;&lt;li&gt;&lt;code&gt;IGNORE_ZERO_VALUES(2)&lt;/code&gt;: Ignore zero values in the incoming data stream&lt;/li&gt;&lt;li&gt;&lt;code&gt;IGNORE_MISSING_AND_ZERO_VALUES(3)&lt;/code&gt;: Ignore #MI and zero values in the incoming data stream&lt;/li&gt;&lt;/ul&gt; | [optional]
**ResourceUsage** | **long** | Percentage of the total load buffer resources that the load buffer will be allowed to use; must be within [0, 100], and the value of 0 is interpreted as default, which is currently 100. | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

