#!/usr/bin/env sh

# Capture the location of the current script.
scriptdir=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd -P)

# Remove tests, which will not be overwritten by design.
# https://github.com/OpenAPITools/openapi-generator/issues/4075
(cd "$scriptdir" && rm -rf "./src/EssSharp.Test" >/dev/null 2>&1)

# Generate the c# code from the processed.json file.
(cd "$scriptdir" && java -jar openapi-generator-cli-6.2.1.jar generate \
  --input-spec ./processed.json \
  --generator-name csharp-netcore \
  --template-dir ./templates \
  --additional-properties=packageName=EssSharp,netCoreProjectFile=true,optionalAssemblyInfo=false)

# When it's more mature, let's switch to the httpclient library.
#  --library httpclient \
