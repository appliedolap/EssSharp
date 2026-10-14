# EssSharp.Model.CreateApplication

## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**ApplicationName** | **string** | &lt;p&gt;The application name must not exceed 30 characters. Avoid using spaces. Application names are not case-sensitive.&lt;/p&gt; | [optional] 
**DatabaseName** | **string** | &lt;p&gt;The database (cube) name. The name is not case sensitive. Do not use spaces in the name. Only the following special characters are allowed in the name: &lt;code&gt;%$-{}()!~&#x60;#&amp;@^&lt;/code&gt;.&lt;/p&gt; | [optional] 
**AllowDuplicates** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if duplicate member names are permitted. When you enable duplicate member names in an Essbase outline, then member names do not have to be unique. You can have multiple members using the same name in the outline.&lt;/p&gt; | [optional] 
**EnableScenario** | **bool** | &lt;p&gt;&lt;b&gt;true&lt;/b&gt; if cubes can be enabled for scenario management. Note that scenario management is supported only for block storage cubes. For aggregate storage cubes, the only supported value for this parameter is &lt;b&gt;false&lt;/b&gt;.&lt;/p&gt; | [optional] 
**MemberCount** | **int** |  | [optional] 
**DatabaseType** | **string** | &lt;p&gt;The type of database (cube) to create. Values: &lt;b&gt;BSO&lt;/b&gt; for block storage cube, or &lt;b&gt;ASO&lt;/b&gt; for aggregate storage cube.&lt;/p&gt; | [optional] 
**MemberPrefix** | **string** |  | [optional] 
**AppType** | **string** |  | [optional] 
**DbType** | **string** |  | [optional] 

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)

