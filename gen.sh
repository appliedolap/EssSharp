#!/usr/bin/env sh

# Use the location of the current script as the working directory.
cd "$(CDPATH= cd -- "$(dirname -- "$0")" && pwd -P)" || exit 1

# Verify that jq is available on the path.
command -v jq >/dev/null 2>&1 || { echo "The required 'jq' command is not present on the path, exiting."; exit 1; }

# The client is always generated from the newest specification in the versions directory. The older
# specifications are kept as a historical record only.
version=$(find ./versions -mindepth 1 -maxdepth 1 -type d -exec basename {} \; | sort -V | tail -1)
[ -f "./versions/$version/openapi.json" ] || { echo "versions/$version/openapi.json does not exist, exiting."; exit 1; }

# Format the specification as formatted.json, then apply the corrections in process.jq to produce processed.json.
jq . "./versions/$version/openapi.json" > formatted.json || { echo "Unable to save formatted.json, exiting."; exit 1; }
jq -f process.jq formatted.json > processed.json.tmp || { echo "Unable to process the specification, exiting."; rm -f processed.json.tmp; exit 1; }
mv processed.json.tmp processed.json || { echo "Unable to save processed.json, exiting."; exit 1; }

# Remove tests, which will not be overwritten by design.
# https://github.com/OpenAPITools/openapi-generator/issues/4075
rm -rf ./src/EssSharp.Test/Api ./src/EssSharp.Test/Model >/dev/null 2>&1

# Generate the c# code from the processed.json file.
#
# The operationId mappings keep the method names that earlier Essbase specifications produced where Oracle's 26
# document renamed or suffixed them. The generator uses a mapped name as the method name exactly as written,
# so each value is the final PascalCase name, without the dot of a 'Tag.method' operationId.
java -jar openapi-generator-cli-7.14.0.jar generate \
  --input-spec ./processed.json \
  --generator-name csharp \
  --template-dir ./templates \
  --operation-id-name-mappings DrillThroughReports.getReports_1=DrillThroughReportsGetReports,DrillThroughReports.getReports=DrillThroughReportsGetReportsForIntersections,Applications.getAliases_1=SetActiveAlias,DatabaseSettingsStatistics.getCompressSettings=DatabaseSettingsStatisticsGetCompressionInfoSettings,DatabaseSettingsStatistics.getCompressSettings_1=DatabaseSettingsStatisticsGetCompressSettings,GlobalDatasources.getDataStream=GlobalDatasourcesGetDelimitedDataStream,GlobalDatasources.getDataStream_1=GlobalDatasourcesGetDataStream \
  --additional-properties=library=restsharp,packageName=EssSharp,netCoreProjectFile=true,optionalAssemblyInfo=false,targetFramework=netstandard2.0

# When it's more mature, let's switch to the httpclient library.
#  --library httpclient \
