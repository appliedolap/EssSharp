@echo off

:: Create a local environment scope for this script.
setlocal

:: Change directory to the location of this script.
cd "%~dp0"

:: Verify that jq is available on the path.
where jq.exe >nul 2>&1 || ( echo The required 'jq' command is not present on the path, exiting. & exit /B 1 )

:: The client is always generated from the newest specification in the versions directory. The older
:: specifications are kept as a historical record only. Folder names are compared as version numbers.
set "ESSBASE_VERSION="
for /F "usebackq delims=" %%v in (`powershell -NoProfile -Command "(Get-ChildItem versions -Directory | Sort-Object { [version]$_.Name } | Select-Object -Last 1).Name"`) do set "ESSBASE_VERSION=%%v"
if not exist "versions\%ESSBASE_VERSION%\openapi.json" ( echo versions\%ESSBASE_VERSION%\openapi.json does not exist, exiting. & exit /B 1 )

:: Format the specification as formatted.json, then apply the corrections in process.jq to produce processed.json.
jq . "versions\%ESSBASE_VERSION%\openapi.json" > formatted.json || ( echo Unable to save formatted.json, exiting. & exit /B 1 )
jq -f process.jq formatted.json > processed.json.tmp || ( echo Unable to process the specification, exiting. & del /f processed.json.tmp >nul 2>&1 & exit /B 1 )
move /Y processed.json.tmp processed.json >nul || ( echo Unable to save processed.json, exiting. & exit /B 1 )

:: Remove tests, which will not be overwritten by design.
:: https://github.com/OpenAPITools/openapi-generator/issues/4075
rmdir /S /Q .\src\EssSharp.Test\Api >nul 2>&1
rmdir /S /Q .\src\EssSharp.Test\Model >nul 2>&1

:: Generate the c# code from the processed.json file.
::
:: The operationId mappings keep the method names that earlier Essbase specifications produced where Oracle's 26
:: document renamed or suffixed them. The generator uses a mapped name as the method name exactly as written,
:: so each value is the final PascalCase name, without the dot of a 'Tag.method' operationId.
java -jar openapi-generator-cli-7.14.0.jar generate ^
  --input-spec .\processed.json ^
  --generator-name csharp ^
  --template-dir .\templates ^
  --operation-id-name-mappings DrillThroughReports.getReports_1=DrillThroughReportsGetReports,DrillThroughReports.getReports=DrillThroughReportsGetReportsForIntersections,Applications.getAliases_1=SetActiveAlias,DatabaseSettingsStatistics.getCompressSettings=DatabaseSettingsStatisticsGetCompressionInfoSettings,DatabaseSettingsStatistics.getCompressSettings_1=DatabaseSettingsStatisticsGetCompressSettings,GlobalDatasources.getDataStream=GlobalDatasourcesGetDelimitedDataStream,GlobalDatasources.getDataStream_1=GlobalDatasourcesGetDataStream ^
  --additional-properties=library=restsharp,packageName=EssSharp,netCoreProjectFile=true,optionalAssemblyInfo=false,targetFramework=netstandard2.0

:: When it's more mature, let's switch to the httpclient library.
::  --library httpclient ^
