# EssSharp.Model.CurrencySettings

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**CurrencyDatabase** | **string** | &lt;p&gt;The currency cube. By assigning currency tags to members in the main cube outline, you enable Essbase to generate the currency cube automatically. In the Sample_Currency application, the currency cube is Xchgrate.&lt;/p&gt; | [optional]
**ConversionType** | **string** |  | [optional]
**ConversionTypeMemberName** | **string** | &lt;p&gt;In a currency cube, the optional currency type dimension that enables different scenarios for currency conversion. Typically, a cube has different exchange rates for different scenarios, such as actual, budget, and forecast. To convert data between scenarios, you select which type of rate to use.&lt;/p&gt; | [optional]
**CountryMemberName** | **string** | &lt;p&gt;For currency databases, the country dimension.&lt;/p&gt; | [optional]
**TimeMemberName** | **string** | &lt;p&gt;For currency databases, the time dimension.&lt;/p&gt; | [optional]
**CategoryMemberName** | **string** | &lt;p&gt;For currency databases, the accounts dimension where currency categories are defined.&lt;/p&gt; | [optional]
**PartitionMemberName** | **string** | &lt;p&gt;Optional. The name of dimension designated as the currency partition. This dimension contains members for both local and base values, and holds the data that users input in their own currencies. The local data is converted to the base data using currency conversion calculation scripts.&lt;/p&gt; | [optional]

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

