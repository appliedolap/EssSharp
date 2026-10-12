@echo off

:: Create a local environment scope for this script.
setlocal

:: Change directory to the location of this script.
cd "%~dp0"

:: Remove tests, which will not be overwritten by design.
:: https://github.com/OpenAPITools/openapi-generator/issues/4075
rmdir /S /Q .\src\EssSharp.Test >nul 2>&1

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
