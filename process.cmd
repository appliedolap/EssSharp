@echo off

:: verify that jq.exe is available on the path...
where jq.exe >nul 2>&1 || ( echo The required 'jq' command is not present on the path, exiting. & exit /B 1 )

:: Use the script directory as the working directory.
pushd "%~dp0"

:: set the ESSBASE_SWAGGER_VERSION variable as given in the first argument, 
:: or use the highest swagger version folder in the versions directory.
if not [%1]==[] (
  set "ESSBASE_SWAGGER_VERSION=%1"
) else (
  pushd versions
  for /d %%a in (*) do set "ESSBASE_SWAGGER_VERSION=%%a"
  popd versions
)

:: if the given or found swagger.json file does not exist, bail.
if not exist "versions\%ESSBASE_SWAGGER_VERSION%\swagger.json" echo %~dp0\versions\%ESSBASE_SWAGGER_VERSION%\swagger.json does not exist, exiting. & exit /B 1

:: format the given swagger.json file and save it as formatted.json.
jq . "versions\%ESSBASE_SWAGGER_VERSION%\swagger.json" > formatted.json || ( echo "Unable to save %~dp0\formatted.json" & exit /B 1 )

:: remove any prior temp.json/json.tmp files.
if exist temp.json del /f temp.json >nul 2>&1 || ( echo Unable to delete temp.json, exiting. & exit /B 1 )
if exist json.tmp  del /f json.tmp  >nul 2>&1 || ( echo Unable to delete json.tmp, exiting.  & exit /B 1 )

:: copy the formatted.json to temp.json
copy /Y formatted.json temp.json >nul 2>&1 || ( echo Unable to copy formatted.json to temp.json, exiting. & exit /B 1)

:::: paths ::::

type temp.json | jq ".paths.\"/about/instance\".get.responses = {\"200\": {\"description\": \"successful operation\", \"schema\": {\"$ref\": \"#/definitions/AboutInstance\" }}}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Fix the consumes for the execute mdx endpoint
type temp.json | jq ".paths.\"/applications/{application}/databases/{database}/mdx\".post.consumes = [\"application/json\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Replace duplicated fields from "List Aliases" with new ones for "Set Active Alias".
type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/aliases/setActiveAlias\".put.summary = \"Set Active Alias\"" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )
type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/aliases/setActiveAlias\".put.description = \"Sets the active alias table associated with the specified application and database.\"" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )
type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/aliases/setActiveAlias\".put.operationId = \"setActiveAlias\"" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".paths.\"/applications/{applicationName}/configurations\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/ApplicationConfigList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/reports\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/ReportList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/dimensions\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/DimensionList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Return types for generations are not a List<GenerationLevelList>, they are a GenerationLevelList
type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/generations\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/GenerationLevelList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )
type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/levels\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/GenerationLevelList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Fix the consumes for the grid execute and mdx endpoints
type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/grid\".post.consumes = [\"application/json\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )
type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/grid/mdx\".post.consumes = [\"application/json\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Return types for locked objects are not a List<LockObjectList>, they are a LockObjectList
type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/locks/objects\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/LockObjectList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/scripts\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/ScriptList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/scripts\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/ScriptList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Return types for variables are not a List<VariableList>, they are a VariableList
type temp.json | jq ".paths.\"/applications/{applicationName}/databases/{databaseName}/variables\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/VariableList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Fix the consumes for the application datasource stream endpoint
type temp.json | jq ".paths.\"/applications/{applicationName}/datasources/query/stream\".post.consumes = [\"application/json\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

# Fix the response schema for the application all logs download endpoint.
type temp.json | jq ".paths.\"/applications/{applicationName}/logs/all\".get.responses.\"200\".schema = {\"type\":\"string\",\"format\":\"binary\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

# Fix the response schema for the application latest logs download endpoint.
type temp.json | jq ".paths.\"/applications/{applicationName}/logs/latest\".get.responses.\"200\".schema = {\"type\":\"string\",\"format\":\"binary\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Return types for variables are not a List<VariableList>, they are a VariableList
type temp.json | jq ".paths.\"/applications/{applicationName}/variables\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/VariableList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Fix a duplicate operationId for the datasources customdelimited query stream endpoint
type temp.json | jq ".paths.\"/datasources/customdelimited/query/stream\".post.operationId = \"GlobalDatasources.getDelimitedDataStream\"" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Fix the consumes for the datasource query endpoint
type temp.json | jq ".paths.\"/datasources/query\".post.consumes = [\"application/json\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Fix the consumes for the datasource stream endpoint
type temp.json | jq ".paths.\"/datasources/query/stream\".post.consumes = [\"application/json\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Add a modeled response type for the files endpoint.
type temp.json | jq ".paths.\"/files/{path}\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/FileCollectionResponse\" }" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Fix the post parameters for the export outline to xml endpoint. 
type temp.json | jq ".paths.\"/outline/{app}/{cube}/xml\".post.parameters = [{\"name\": \"app\", \"in\": \"path\", \"description\": \"Application name.\", \"required\": true, \"type\": \"string\"}, {\"name\": \"cube\", \"in\": \"path\", \"description\": \"Database name.\", \"required\": true, \"type\": \"string\"}, {\"name\": \"connection\", \"in\": \"query\", \"description\": \"Essbase connection name.\", \"required\": false, \"type\": \"string\"}, {\"name\": \"applicationNameForConnection\", \"in\": \"query\", \"description\": \"Application name for connection.\", \"required\": false, \"type\": \"string\"}, {\"in\": \"body\", \"name\": \"body\", \"required\": false, \"schema\": {\"$ref\": \"#/definitions/ExportOptions\"}}]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Fix the consumes for the grid preferences endpoint
type temp.json | jq ".paths.\"/preferences/grid\".put.consumes = [\"application/json\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".paths.\"/sessions\".get.responses.\"200\".schema = {\"type\": \"array\",\"items\": {\"$ref\": \"#/definitions/SessionAttributes\"}}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".paths.\"/urls\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/EssbaseURLList\" }" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Fix the produces and response schema for the download utility endpoint
type temp.json | jq ".paths.\"/utilities/{id}\".get.produces = [\"application/zip\",\"application/octet-stream\",\"application/json\",\"application/xml\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )
type temp.json | jq ".paths.\"/utilities/{id}\".get.responses.\"200\".schema = {\"type\":\"string\",\"format\":\"binary\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Return types for variables are not a List<VariableList>, they are a VariableList
type temp.json | jq ".paths.\"/variables\".get.responses.\"200\".schema = {\"$ref\": \"#/definitions/VariableList\"}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:::: definitions ::::

:: The about instance default definition doesn't model a response at all, so we create and stick one in here
type temp.json | jq ".definitions.\"AboutInstance\" = {\"type\": \"object\", \"properties\": {\"provisioningSupported\": {\"type\": \"boolean\"}, \"resetPasswordSupported\": {\"type\": \"boolean\"}, \"easInstalled\": {\"type\": \"boolean\"}}, \"xml\": {\"name\": \"aboutInstance\"}}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: There are duplicate \"Datasource\" definitions, and jq discards all but the last duplicate field of an object.
:: Were this an array, it would be easy enough to handle with unique/unique_by or even just simple indexing,
:: but for an object it is very difficult without leaning on the streaming parser. A simple fix is to just 
:: rewrite the remaining \"Datasource\" definition to the one that we actually need.
type temp.json | jq ".definitions.Datasource = {\"type\": \"object\",\"required\": [ \"application\", \"columns\", \"connection\", \"cube\", \"description\", \"query\", \"sheet\", \"startRow\", \"type\", \"widths\"],\"properties\": {\"type\": {\"type\": \"string\", \"enum\": [ \"TEMPLATE\", \"EXCELFILE\", \"DB\", \"DELIMITEDFILE\", \"FIXEDWIDTHFILE\", \"BI\", \"ESSBASE\", \"JDBC\", \"SPARK\", \"MS_SQL\", \"MYSQL\", \"DB2\", \"ORACLE\", \"FILE\" ] }, \"connection\": {\"type\": \"string\" }, \"description\": {\"type\": \"string\" }, \"columns\": {\"$ref\": \"#/definitions/ColumnsType\" }, \"name\": {\"type\": \"string\" }, \"ignoreErrorRecords\": {\"type\": \"boolean\" }, \"delimeter\": {\"type\": \"string\", \"xml\": {\"name\": \"delimiter\" } }, \"customDelimiter\": {\"type\": \"string\" }, \"query\": {\"type\": \"string\" }, \"application\": {\"type\": \"string\" }, \"cube\": {\"type\": \"string\" }, \"startRow\": {\"type\": \"integer\", \"format\": \"int64\" }, \"endRow\": {\"type\": \"integer\", \"format\": \"int64\" }, \"headerRow\": {\"type\": \"integer\", \"format\": \"int64\" }, \"sheet\": {\"type\": \"string\" }, \"skipHiddenRows\": {\"type\": \"boolean\" }, \"widths\": {\"type\": \"array\", \"items\": {\"type\": \"integer\", \"format\": \"int64\", \"xml\": {\"name\": \"widths\" } } }, \"queryParameters\": {\"type\": \"array\", \"items\": {\"xml\": {\"name\": \"queryParameters\" }, \"$ref\": \"#/definitions/QueryParamsInfo\" } }, \"headers\": {\"type\": \"array\", \"items\": {\"xml\": {\"name\": \"headers\" }, \"$ref\": \"#/definitions/HeaderType\" } }, \"links\": {\"type\": \"array\", \"items\": {\"$ref\": \"#/definitions/Link\" } }},\"xml\": {\"name\": \"datasource\"}}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: The required properties for the Datasource definition are incorrect. Adjust the list of required properties.
type temp.json | jq ".definitions.Datasource.required = [\"columns\", \"connection\", \"type\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".definitions.\"EssbaseURL\" = {\"type\": \"object\", \"properties\": {\"application\": {\"type\": \"string\"}, \"url\": {\"type\": \"string\"}}}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".definitions.\"EssbaseURLList\" = {\"type\": \"object\", \"properties\": {\"items\": {\"type\": \"array\", \"items\": {\"$ref\": \"#/definitions/EssbaseURL\"}}}}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: The files path doesn't model a strongly typed responses, so create new object type and add it to the definitions list.
type temp.json | jq ".definitions.\"FileBean\" = {\"type\": \"object\",\"properties\": {\"name\": {\"type\": \"string\"},\"fullPath\": {\"type\": \"string\"},\"type\": {\"type\": \"string\"},\"permissions\": {\"type\": \"object\", \"properties\": {\"addFolder\": {\"type\": \"boolean\" }, \"addFile\": {\"type\": \"boolean\" } }},\"links\": {\"type\": \"array\", \"items\": {\"$ref\": \"#/definitions/Link\"}}}, \"xml\": {\"name\": \"File\"}}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: The files path doesn't model a strongly typed response, so create new collection type and add it to the definitions list.
type temp.json | jq ".definitions.\"FileCollectionResponse\" = {\"type\": \"object\", \"properties\": {\"count\": {\"type\": \"integer\", \"format\": \"int64\"},\"items\": {\"type\": \"array\", \"items\": {\"$ref\": \"#/definitions/FileBean\" }},\"totalResults\": {\"type\": \"integer\", \"format\": \"int64\"},\"hasMore\": {\"type\": \"boolean\"},\"limit\": {\"type\": \"integer\", \"format\": \"int64\"},\"properties\": {\"type\": \"object\", \"additionalProperties\": {\"type\": \"string\" }},\"offset\": {\"type\": \"integer\", \"format\": \"int64\"}}}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: Essbase REST API seems to be case-sensitive. An example error message when using the default upper-case enums is:
::
:: Cannot deserialize value of type `oracle.essbase.restws.services.main.grid.Action` from String \"ZOOMIN\":
:: value not one of declared Enum instance names: [removeonly, keeponly, pivot, submit, pivotToPOV, refresh, zoomin, zoomout]
type temp.json | jq ".definitions.GridOperation.properties.action.enum = [\"zoomin\", \"zoomout\", \"keeponly\", \"removeonly\", \"refresh\", \"pivot\", \"pivotToPOV\", \"submit\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: The properties for the MemberBean definition are incomplete. Add the following properties.
type temp.json | jq ".definitions.MemberBean.properties += {\"uda\": {\"type\": \"array\",\"items\": {\"type\": \"string\"}}, \"dataStorageType\": {\"type\": \"string\"}, \"parentName\": {\"type\": \"string\"}}" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".definitions.ZoomIn.properties.ancestor.enum = [\"top\", \"bottom\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

type temp.json | jq ".definitions.ZoomIn.properties.mode.enum = [\"children\", \"descendents\", \"base\"]" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:::: securityDefinitions ::::

:: Add securityDefinitions and security for basic auth by default.
type temp.json | jq ". += ({securityDefinitions: {basicAuth: {type: \"basic\"}}, security: [{\"basicAuth\": []}]})" > json.tmp && move /Y json.tmp temp.json >nul 2>&1 || ( echo "Unable to move json.tmp to temp.json, processing failed." & exit /B 1 )

:: save the processed json
copy /Y temp.json processed.json >nul 2>&1 || ( echo "Unable to copy temp.json to processed.json, processing failed." & exit /B 1 )