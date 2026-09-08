# EssSharp.Model.MeasureOptions
<p>Load rule definition properties relating primarily to the Accounts dimension.</p>

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**CurrencyCategory** | **string** | &lt;p&gt;If the dimension is associated with a currency conversion application, the currency category. This is an Accounts member from the currency cube. Example: P&amp;L.&lt;/p&gt; | [optional] 
**CurrencyName** | **string** | &lt;p&gt;If the dimension is associated with a currency conversion application, the currency name. This is a Country dimension type member from the currency cube. Examples: USD, CND, GPB, EUR&lt;/p&gt; | [optional] 
**CurrencyConversion** | **string** | &lt;p&gt;Currency conversion action to take during the dimension build. Mark any members that should not be currency-converted as NO_CONVERSION. NONE does not indicate no conversion; rather, it indicates that the conversion category is unspecified (thus inherited).&lt;/p&gt; | [optional] 
**Skip** | **string** | &lt;p&gt;If you set the &lt;i&gt;timeBalanceOption&lt;/i&gt; as FIRST, LAST, or AVERAGE, then use the skip property to indicate how to calculate the parent value when missing- or zero-values  are encountered. NONE: Does not skip data when calculating parent value. MISSING: Skips #MISSING data. ZERO: Skips data that equals zero. MISSING_ZERO: Skips #MISSING data and data that equals zero.&lt;/p&gt; | [optional] 
**TimeBalanceOption** | **string** | &lt;p&gt;The time balance property, if used. By default, a parent in the time dimension is calculated based on the consolidation and formulas of its children. For example, in the Sample.Basic database, the Qtr1 member is the sum of its children (Jan, Feb, and Mar). However, setting a time balance property causes parents, for example Qtr1, to roll up differently.&lt;/p&gt; | [optional] 
**VarianceReporting** | **string** | &lt;p&gt;Whether to treat accounts members as expense items. EXISTING to keep the current setting.&lt;/p&gt; | [optional] 
**TwoPass** | **bool** | &lt;p&gt;True to mark the dimension as using two-pass calculation.&lt;/p&gt; | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

