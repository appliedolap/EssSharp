#!/usr/bin/env sh

# Capture the location of the current script.
scriptdir=$(CDPATH= cd -- "$(dirname -- "$0")" && pwd -P)

# Remove tests, which will not be overwritten by design.
# https://github.com/OpenAPITools/openapi-generator/issues/4075
(cd "$scriptdir" && rm -rf ./src/EssSharp.Test/Api && rm -rf ./src/EssSharp.Test/Model >/dev/null 2>&1)

# Generate the c# code from the processed.json file.
(cd "$scriptdir" && java -jar openapi-generator-cli-7.13.0.jar generate \
  --input-spec ./processed.json \
  --generator-name csharp \
  --template-dir ./templates \
  --additional-properties=library=restsharp,packageName=EssSharp,netCoreProjectFile=true,optionalAssemblyInfo=false,targetFramework=netstandard2.0)

# When it's more mature, let's switch to the httpclient library.
#  --library httpclient \
