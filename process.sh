#!/usr/bin/env bash

# verify that jq is available on the path...
command -v jq >/dev/null 2>&1 || { echo "The required 'jq' command is not present on the path, exiting."; exit 1; }

# Use the script directory as the working directory.
pushd "$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)" >/dev/null 2>&1

# if a first argument is given, assign its value to ESSBASE_SWAGGER_VERSION, which is used to locate a swagger.json file.
# otherwise, if the ESSBASE_SWAGGER_VERSION is unset or empty, use the highest version in the versions directory.
if [ ! -z "$1" ]; then
  ESSBASE_SWAGGER_VERSION=$1
elif [ -z "${ESSBASE_SWAGGER_VERSION}" ]; then
  ESSBASE_SWAGGER_VERSION=$(find ./versions -mindepth 1 -maxdepth 1 -name '*' -type d -exec basename {} \; | sort | tail -1)
fi

# if the resolved path to the swagger.json does not exist, bail.
[ -f "./versions/$ESSBASE_SWAGGER_VERSION/swagger.json" ] || { echo "versions/$ESSBASE_SWAGGER_VERSION/swagger.json does not exist, exiting."; exit 1; }

# format the located swagger.json file and save it as formatted.json.
jq . "./versions/$ESSBASE_SWAGGER_VERSION/swagger.json" > formatted.json || { echo "Unable to save formatted.json"; exit 1; }

# remove any prior temp.json/json.tmp files.
[ -e temp.json ] && ( rm temp.json >/dev/null 2>&1 || { echo "Unable to delete temp.json, exiting."; exit 1; } )
[ -e json.tmp  ] && ( rm json.tmp  >/dev/null 2>&1 || { echo "Unable to delete json.tmp, exiting." ; exit 1; } )

# copy the formatted.json to temp.json
cp formatted.json temp.json >/dev/null 2>&1 || { echo "Unable to copy formatted.json to temp.json, exiting."; exit 1; }

##### paths ####

cat temp.json | jq '.paths."/about/instance".get.responses = {"200": {"description": "successful operation", "schema": { "$ref": "#/definitions/AboutInstance" }}}' > json.tmp && mv json.tmp temp.json 

# Fix the consumes for the execute mdx endpoint
cat temp.json | jq '.paths."/applications/{application}/databases/{database}/mdx".post.consumes = ["application/json"]' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.paths."/applications/{applicationName}/configurations".get.responses."200".schema = {"$ref": "#/definitions/ApplicationConfigList"}' > json.tmp && mv json.tmp temp.json

# Replace duplicated fields from "List Aliases" with new ones for "Set Active Alias".
cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/aliases/setActiveAlias".put.summary = "Set Active Alias"' > json.tmp && mv json.tmp temp.json
cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/aliases/setActiveAlias".put.description = "Sets the active alias table associated with the specified application and database."' > json.tmp && mv json.tmp temp.json
cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/aliases/setActiveAlias".put.operationId = "setActiveAlias"' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/reports".get.responses."200".schema = {"$ref": "#/definitions/ReportList"}' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/dimensions".get.responses."200".schema = {"$ref": "#/definitions/DimensionList"}' > json.tmp && mv json.tmp temp.json

# Return types for generations are not a List<GenerationLevelList>, they are a GenerationLevelList
cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/generations".get.responses."200".schema = {"$ref": "#/definitions/GenerationLevelList"}' > json.tmp && mv json.tmp temp.json
cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/levels".get.responses."200".schema = {"$ref": "#/definitions/GenerationLevelList"}' > json.tmp && mv json.tmp temp.json

# Fix the consumes for the grid execute endpoint
cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/grid".post.consumes = ["application/json"]' > json.tmp && mv json.tmp temp.json

# Return types for locked objects are not a List<LockObjectList>, they are a LockObjectList
cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/locks/objects".get.responses."200".schema = {"$ref": "#/definitions/LockObjectList"}' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/scripts".get.responses."200".schema = {"$ref": "#/definitions/ScriptList"}' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/scripts".get.responses."200".schema = {"$ref": "#/definitions/ScriptList"}' > json.tmp && mv json.tmp temp.json

# Return types for variables are not a List<VariableList>, they are a VariableList
cat temp.json | jq '.paths."/applications/{applicationName}/databases/{databaseName}/variables".get.responses."200".schema = {"$ref": "#/definitions/VariableList"}' > json.tmp && mv json.tmp temp.json

# Fix the consumes for the application datasource stream endpoint
cat temp.json | jq '.paths."/applications/{applicationName}/datasources/query/stream".post.consumes = ["application/json"]' > json.tmp && mv json.tmp temp.json

# Fix the response schema for the application all logs download endpoint.
cat temp.json | jq '.paths."/applications/{applicationName}/logs/all".get.responses."200".schema = {"type":"string","format":"binary"}' > json.tmp && mv json.tmp temp.json

# Fix the response schema for the application latest logs download endpoint.
cat temp.json | jq '.paths."/applications/{applicationName}/logs/latest".get.responses."200".schema = {"type":"string","format":"binary"}' > json.tmp && mv json.tmp temp.json

# Return types for variables are not a List<VariableList>, they are a VariableList
cat temp.json | jq '.paths."/applications/{applicationName}/variables".get.responses."200".schema = {"$ref": "#/definitions/VariableList"}' > json.tmp && mv json.tmp temp.json

# Fix a duplicate operationId for the datasources customdelimited query stream endpoint
cat temp.json | jq '.paths."/datasources/customdelimited/query/stream".post.operationId = "GlobalDatasources.getDelimitedDataStream"' > json.tmp && mv json.tmp temp.json

# Fix the consumes for the datasources query endpoint
cat temp.json | jq '.paths."/datasources/query".post.consumes = ["application/json"]' > json.tmp && mv json.tmp temp.json

# Fix the consumes for the datasources query stream endpoint
cat temp.json | jq '.paths."/datasources/query/stream".post.consumes = ["application/json"]' > json.tmp && mv json.tmp temp.json

# Add a modeled response type for the files get endpoint.
cat temp.json | jq '.paths."/files".get.responses."200".schema = { "$ref": "#/definitions/FileCollectionResponse" }' > json.tmp && mv json.tmp temp.json
cat temp.json | jq '.paths."/files/{path}".get.responses."200".schema = { "$ref": "#/definitions/FileCollectionResponse" }' > json.tmp && mv json.tmp temp.json

# Add a stream request parameter to the files put endpoint.
cat temp.json | jq '.paths."/files/{path}".put.consumes = ["application/octet-stream"]' > json.tmp && mv json.tmp temp.json
cat temp.json | jq '.paths."/files/{path}".put.parameters += [
  {
    "name": "stream",
    "in": "body",
    "description": "<p>Applicable only for adding a file. Provides the stream to upload.</p>",
    "required": true,
    "schema": {
      "type": "string",
      "format": "binary"
    }
  }
]' > json.tmp && mv json.tmp temp.json

# Fix the post parameters for the export outline to xml endpoint. 
cat temp.json | jq '.paths."/outline/{app}/{cube}/xml".post.parameters = [
  {
    "name": "app",
    "in": "path",
    "description": "Application name.",
    "required": true,
    "type": "string"
  },
  {
    "name": "cube",
    "in": "path",
    "description": "Database name.",
    "required": true,
    "type": "string"
  },
  {
    "name": "connection",
    "in": "query",
    "description": "Essbase connection name.",
    "required": false,
    "type": "string"
  },
  {
    "name": "applicationNameForConnection",
    "in": "query",
    "description": "Application name for connection.",
    "required": false,
    "type": "string"
  },
  {
    "in": "body",
    "name": "body",
    "required": false,
    "schema": {
      "$ref": "#/definitions/ExportOptions"
    }
  }
]' > json.tmp && mv json.tmp temp.json

# Fix the consumes for the grid preferences endpoint
cat temp.json | jq '.paths."/preferences/grid".put.consumes = ["application/json"]' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.paths."/sessions".get.responses."200".schema = {"type": "array","items": {"$ref": "#/definitions/SessionAttributes"}}' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.paths."/urls".get.responses."200".schema = { "$ref": "#/definitions/EssbaseURLList" }' > json.tmp && mv json.tmp temp.json

# Fix the produces and response schema for the download utility endpoint
cat temp.json | jq '.paths."/utilities/{id}".get.produces = ["application/zip","application/octet-stream","application/json","application/xml"]' > json.tmp && mv json.tmp temp.json
cat temp.json | jq '.paths."/utilities/{id}".get.responses."200".schema = {"type":"string","format":"binary"}' > json.tmp && mv json.tmp temp.json

# Return types for variables are not a List<VariableList>, they are a VariableList
cat temp.json | jq '.paths."/variables".get.responses."200".schema = {"$ref": "#/definitions/VariableList"}' > json.tmp && mv json.tmp temp.json

#### definitions ####

# The about instance default definition doesn't model a response at all, so we create and stick one in here
cat temp.json | jq '.definitions."AboutInstance" = {
  "type": "object",
  "properties": {
    "provisioningSupported": {
      "type": "boolean"
    },
    "resetPasswordSupported": {
      "type": "boolean"
    },
    "easInstalled": {
      "type": "boolean"
    }
  },
  "xml": {
    "name": "aboutInstance"
  }
}' > json.tmp && mv json.tmp temp.json


# There are duplicate "Datasource" definitions, and jq discards all but the last duplicate field of an object.
# Were this an array, it would be easy enough to handle with unique/unique_by or even just simple indexing,
# but for an object it is very difficult without leaning on the streaming parser. A simple fix is to just 
# rewrite the remaining "Datasource" definition to the one that we actually need.
cat temp.json | jq '.definitions.Datasource = {
    "type": "object",
    "required": [
        "application",
        "columns",
        "connection",
        "cube",
        "description",
        "query",
        "sheet",
        "startRow",
        "type",
        "widths"
    ],
    "properties": {
        "type": {
            "type": "string",
            "enum": [
                "TEMPLATE",
                "EXCELFILE",
                "DB",
                "DELIMITEDFILE",
                "FIXEDWIDTHFILE",
                "BI",
                "ESSBASE",
                "JDBC",
                "SPARK",
                "MS_SQL",
                "MYSQL",
                "DB2",
                "ORACLE",
                "FILE"
            ]
        },
        "connection": {
            "type": "string"
        },
        "description": {
            "type": "string"
        },
        "columns": {
            "$ref": "#/definitions/ColumnsType"
        },
        "name": {
            "type": "string"
        },
        "ignoreErrorRecords": {
            "type": "boolean"
        },
        "delimeter": {
            "type": "string",
            "xml": {
                "name": "delimiter"
            }
        },
        "customDelimiter": {
            "type": "string"
        },
        "query": {
            "type": "string"
        },
        "application": {
            "type": "string"
        },
        "cube": {
            "type": "string"
        },
        "startRow": {
            "type": "integer",
            "format": "int64"
        },
        "endRow": {
            "type": "integer",
            "format": "int64"
        },
        "headerRow": {
            "type": "integer",
            "format": "int64"
        },
        "sheet": {
            "type": "string"
        },
        "skipHiddenRows": {
            "type": "boolean"
        },
        "widths": {
            "type": "array",
            "items": {
                "type": "integer",
                "format": "int64",
                "xml": {
                    "name": "widths"
                }
            }
        },
        "queryParameters": {
            "type": "array",
            "items": {
                "xml": {
                    "name": "queryParameters"
                },
                "$ref": "#/definitions/QueryParamsInfo"
            }
        },
        "headers": {
            "type": "array",
            "items": {
                "xml": {
                    "name": "headers"
                },
                "$ref": "#/definitions/HeaderType"
            }
        },
        "links": {
            "type": "array",
            "items": {
                "$ref": "#/definitions/Link"
            }
        }
    },
    "xml": {
        "name": "datasource"
    }
}' > json.tmp && mv json.tmp temp.json

# The required properties for the Datasource definition are incorrect. Adjust the list of required properties.
cat temp.json | jq '.definitions.Datasource.required = ["columns", "connection", "type"]' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.definitions."EssbaseURL" = {
  "type": "object",
  "properties": {
    "application": {
      "type": "string"
    },
    "url": {
      "type": "string"
    }
  }
}' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.definitions."EssbaseURLList" = {
  "type": "object",
  "properties": {
    "items": {
      "type": "array",
      "items": {
        "$ref": "#/definitions/EssbaseURL"
      }
    }
  }
}' > json.tmp && mv json.tmp temp.json

# The files path doesn't model a strongly typed responses, so create new object type and add it to the definitions list.
cat temp.json | jq '.definitions."FileBean" = {
  "type": "object",
  "properties": {
    "name": {
      "type": "string"
    },
    "fullPath": {
      "type": "string"
    },
    "type": {
      "type": "string"
    },
    "permissions": {
      "type": "object",
      "properties": {
          "addFolder": {
            "type": "boolean"
          },
          "addFile": {
            "type": "boolean"
          }
      }
    },
    "links": {
      "type": "array",
      "items": {
        "$ref": "#/definitions/Link"
      }
    }
  },
  "xml": {
    "name": "File"
  }
}' > json.tmp && mv json.tmp temp.json

# The files path doesn't model a strongly typed response, so create new collection type and add it to the definitions list.
cat temp.json | jq '.definitions."FileCollectionResponse" = {
  "type": "object",
  "properties": {
    "count": {
      "type": "integer",
      "format": "int64"
    },
    "items": {
      "type": "array",
      "items": {
        "$ref": "#/definitions/FileBean"
      }
    },
    "totalResults": {
      "type": "integer",
      "format": "int64"
    },
    "hasMore": {
      "type": "boolean"
    },
    "limit": {
      "type": "integer",
      "format": "int64"
    },
    "properties": {
      "type": "object",
      "additionalProperties": {
        "type": "string"
      }
    },
    "offset": {
      "type": "integer",
      "format": "int64"
    }
  }
}' > json.tmp && mv json.tmp temp.json

# Essbase REST API seems to be case-sensitive. An example error message when using the default upper-case enums is:
#
#   Cannot deserialize value of type `oracle.essbase.restws.services.main.grid.Action` from String \"ZOOMIN\":
#   value not one of declared Enum instance names: [removeonly, keeponly, pivot, submit, pivotToPOV, refresh, zoomin, zoomout]
cat temp.json | jq '.definitions.GridOperation.properties.action.enum = ["zoomin", "zoomout", "keeponly", "removeonly", "refresh", "pivot", "pivotToPOV", "submit"]' > json.tmp && mv json.tmp temp.json

# The properties for the MemberBean definition are incomplete. Add the following properties.
cat temp.json | jq '.definitions.MemberBean.properties += {
  "uda": {
    "type": "array",
    "items": {
      "type": "string"
    }
  },
  "dataStorageType": {
    "type": "string"
  },
  "parentName": {
    "type": "string"
  }
}' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.definitions.ZoomIn.properties.ancestor.enum = ["top", "bottom"]' > json.tmp && mv json.tmp temp.json

cat temp.json | jq '.definitions.ZoomIn.properties.mode.enum = ["children", "descendents", "base"]' > json.tmp && mv json.tmp temp.json

#### securityDefinitions ####

# Add securityDefinitions and security for basic auth by default.
cat temp.json | jq '. += ({securityDefinitions: {basicAuth: {type: "basic"}}, security: [{"basicAuth": []}]})' > json.tmp && mv json.tmp temp.json

# save the processed json
cp temp.json processed.json
