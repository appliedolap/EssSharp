@echo off

:: Create a local environment scope for this script.
setlocal

:: Change directory to the location of this script.
cd "%~dp0"

:: Remove tests, which will not be overwritten by design.
:: https://github.com/OpenAPITools/openapi-generator/issues/4075
rmdir /S /Q .\src\EssSharp.Test >nul 2>&1

:: Generate the c# code from the processed.json file.
java -jar openapi-generator-cli-6.2.1.jar generate ^
  --input-spec .\processed.json ^
  --generator-name csharp-netcore ^
  --template-dir .\templates ^
  --additional-properties=packageName=EssSharp,netCoreProjectFile=true,optionalAssemblyInfo=false

:: When it's more mature, let's switch to the httpclient library.
::  --library httpclient ^
