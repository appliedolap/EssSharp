# Oracle's Essbase 26 document is OpenAPI 3, while earlier archived specifications
# are Swagger 2. Keep the corrections here equivalent to the Swagger 2 corrections
# in process.sh/process.cmd, expressed using OpenAPI 3 request/response shapes.

def schema_ref($name):
  {"$ref": ("#/components/schemas/" + $name)};

def set_response_schema($path; $method; $status; $schema):
  .paths[$path][$method].responses[$status] |=
    (.content =
      (if (((.content // {}) | length) == 0)
       then {"application/json": {"schema": $schema}}
       else (.content | with_entries(.value.schema = $schema))
       end));

def use_json_request($path; $method):
  .paths[$path][$method].requestBody |=
    (.content as $content
     | .content = {
         "application/json":
           ($content["application/json"] // $content["*/*"] // ($content | to_entries[0].value))
       });

def use_json_response($path; $method; $status):
  .paths[$path][$method].responses[$status] |=
    (.content as $content
     | .content = {
         "application/json":
           ($content["application/json"] // $content["*/*"] // ($content | to_entries[0].value))
       });

# Oracle's document currently contains both `openapi` and `swagger`. The latter is
# invalid in an OpenAPI 3 document and causes OpenAPI Generator validation to fail.
del(.swagger)

# Swagger 2 body parameters in all earlier Essbase documents were named `body`.
# Preserve those generated C# signatures when consuming OpenAPI 3 requestBody
# objects. The file upload operation remains the one established `stream` case.
| .paths |= with_entries(
    .value |= with_entries(
      if ((.value | type) == "object" and .value.requestBody? != null)
      then .value.requestBody["x-codegen-request-body-name"] = "body"
      else .
      end
    )
  )

# Path and operation corrections.
| set_response_schema("/about/instance"; "get"; "200"; schema_ref("AboutInstance"))
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/dtreports/list"; "post"; "200"; {"type": "array", "items": {"type": "string"}})
| use_json_request("/applications"; "post")
| use_json_request("/applications/{application}/databases/{database}/mdx"; "post")
| .paths["/applications/{application}/databases/{database}/mdx"].post.responses["200"].content = {
    "application/octet-stream": {"schema": schema_ref("InputStream")},
    "text/html": {"schema": schema_ref("InputStream")}
  }
| .paths["/applications/{application}/databases/{database}/mdx/{name}"].get.responses["200"].content = {
    "application/octet-stream": {"schema": schema_ref("InputStream")}
  }
| use_json_request("/applications/{applicationName}/databases/{databaseName}/grid/layout"; "post")
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/grid/layout"; "post"; "200"; schema_ref("GridLayout"))
| set_response_schema("/applications/{applicationName}/configurations"; "get"; "200"; schema_ref("ApplicationConfigList"))
| set_response_schema("/outline/{app}/{cube}"; "get"; "200"; schema_ref("MembersList"))
| set_response_schema("/outline/{app}/{cube}/ancestors/{memberUniqueName}"; "get"; "200"; schema_ref("AncestorsList"))
| set_response_schema("/groups/{id}/members/users"; "post"; "200"; schema_ref("Users"))
| set_response_schema("/groups/{id}/members/groups"; "post"; "200"; schema_ref("Groups"))
| .paths["/applications/{applicationName}/databases/{databaseName}/aliases/setActiveAlias"].put.summary = "Set Active Alias"
| .paths["/applications/{applicationName}/databases/{databaseName}/aliases/setActiveAlias"].put.description = "Sets the active alias table associated with the specified application and database."
| .paths["/applications/{applicationName}/databases/{databaseName}/aliases/setActiveAlias"].put.operationId = "setActiveAlias"
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/reports"; "get"; "200"; schema_ref("ReportList"))
| .paths["/applications/{applicationName}/databases/{databaseName}/reports"].get.operationId = "DrillThroughReports.getReports"
| .paths["/applications/{applicationName}/databases/{databaseName}/dtreports/list"].post.operationId = "DrillThroughReports.getReportsForIntersections"
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/dimensions"; "get"; "200"; schema_ref("DimensionList"))
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/generations"; "get"; "200"; schema_ref("GenerationLevelList"))
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/dimensions/{dimensionName}/levels"; "get"; "200"; schema_ref("GenerationLevelList"))
| use_json_request("/applications/{applicationName}/databases/{databaseName}/grid"; "post")
| use_json_request("/applications/{applicationName}/databases/{databaseName}/grid/mdx"; "post")
| use_json_response("/applications/{applicationName}/databases/{databaseName}/scripts"; "post"; "200")
| use_json_response("/applications/{applicationName}/databases/{databaseName}/scripts/{scriptName}"; "put"; "200")
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/scripts/{scriptName}/content"; "get"; "200"; schema_ref("ScriptContent"))
| use_json_response("/applications/{applicationName}/databases/{databaseName}/locks/objects/lock"; "post"; "200")
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/locks/objects"; "get"; "200"; schema_ref("LockObjectList"))
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/scripts"; "get"; "200"; schema_ref("ScriptList"))
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/variables"; "get"; "200"; schema_ref("VariableList"))
| .paths["/applications/{applicationName}/databases/{databaseName}/settings/compressioninfo"].get.operationId = "DatabaseSettingsStatistics.getCompressionInfoSettings"
| .paths["/applications/{applicationName}/databases/{databaseName}/settings/compression"].get.operationId = "DatabaseSettingsStatistics.getCompressSettings"
| use_json_request("/applications/{applicationName}/datasources/query/stream"; "post")
| .paths["/applications/{applicationName}/logs/all"].get.responses["200"].content = {
    "application/zip": {"schema": {"type": "string", "format": "binary"}},
    "application/octet-stream": {"schema": {"type": "string", "format": "binary"}}
  }
| .paths["/applications/{applicationName}/logs/latest"].get.responses["200"].content = {
    "application/octet-stream": {"schema": {"type": "string", "format": "binary"}}
  }
| set_response_schema("/applications/{applicationName}/variables"; "get"; "200"; schema_ref("VariableList"))
| .paths["/datasources/customdelimited/query/stream"].post.operationId = "GlobalDatasources.getDelimitedDataStream"
| .paths["/datasources/query/stream"].post.operationId = "GlobalDatasources.getDataStream"
| use_json_request("/datasources/query"; "post")
| use_json_request("/datasources/query/stream"; "post")
| set_response_schema("/files"; "get"; "200"; schema_ref("FileCollectionResponse"))
| set_response_schema("/files/{path}"; "get"; "200"; schema_ref("FileCollectionResponse"))
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/locks/blocks"; "get"; "200"; schema_ref("LockBlockList"))
| .paths["/files/{path}"].put.requestBody = {
    "description": "Applicable only for adding a file. Provides the stream to upload.",
    "required": true,
    "x-codegen-request-body-name": "stream",
    "content": {
      "application/octet-stream": {
        "schema": {"type": "string", "format": "binary"}
      }
    }
  }
| .paths["/outline/{app}/{cube}/xml"].post.parameters = [
    {"name": "app", "in": "path", "description": "Application name.", "required": true, "schema": {"type": "string"}},
    {"name": "cube", "in": "path", "description": "Database name.", "required": true, "schema": {"type": "string"}},
    {"name": "connection", "in": "query", "description": "Essbase connection name.", "required": false, "schema": {"type": "string"}},
    {"name": "applicationNameForConnection", "in": "query", "description": "Application name for connection.", "required": false, "schema": {"type": "string"}}
  ]
| .paths["/outline/{app}/{cube}/xml"].post.requestBody = {
    "required": false,
    "content": {"application/json": {"schema": schema_ref("ExportOptions")}}
  }
| use_json_request("/preferences/grid"; "put")
| set_response_schema("/sessions"; "get"; "200"; {"type": "array", "items": schema_ref("SessionAttributes")})
| set_response_schema("/urls"; "get"; "200"; schema_ref("EssbaseURLList"))
| .paths["/utilities/{id}"].get.responses["200"].content = {
    "application/zip": {"schema": {"type": "string", "format": "binary"}},
    "application/octet-stream": {"schema": {"type": "string", "format": "binary"}}
  }
| set_response_schema("/variables"; "get"; "200"; schema_ref("VariableList"))

# Correct the typo in the Essbase 26 operation ID so regeneration preserves the
# method name already published by EssSharp's 2026.02.24 client.
| .paths["/ai/dbconnection/{dbConnectionName}/chat/credential/signingkey/{credentialName}"].post.operationId = "AI.createOCIChatCredentialUsingSigningKey"

# Schema corrections and supplementary response models.
| .components.schemas.AboutInstance = {
    "type": "object",
    "properties": {
      "provisioningSupported": {"type": "boolean"},
      "resetPasswordSupported": {"type": "boolean"},
      "easInstalled": {"type": "boolean"}
    },
    "xml": {"name": "aboutInstance"}
  }
| .components.schemas.InputStream = {"type": "object"}
| .components.schemas |= with_entries(if .key == "DataSource" then .key = "RuleDataSource" else . end)
| .components.schemas.Datasource.required = ["connection", "type"]
| .components.schemas.EssbaseURL = {
    "type": "object",
    "properties": {
      "application": {"type": "string"},
      "url": {"type": "string"}
    }
  }
| .components.schemas.EssbaseURLList = {
    "type": "object",
    "properties": {
      "items": {"type": "array", "items": schema_ref("EssbaseURL")}
    }
  }
| .components.schemas.FileBean = {
    "type": "object",
    "properties": {
      "name": {"type": "string"},
      "fullPath": {"type": "string"},
      "type": {"type": "string"},
      "permissions": {
        "type": "object",
        "properties": {
          "addFolder": {"type": "boolean"},
          "addFile": {"type": "boolean"}
        }
      },
      "links": {"type": "array", "items": schema_ref("Link")}
    },
    "xml": {"name": "File"}
  }
| .components.schemas.ScriptContent = {
    "type": "object",
    "properties": {"content": {"type": "string"}}
  }
| .components.schemas.FileCollectionResponse = {
    "type": "object",
    "properties": {
      "count": {"type": "integer", "format": "int64"},
      "items": {"type": "array", "items": schema_ref("FileBean")},
      "totalResults": {"type": "integer", "format": "int64"},
      "hasMore": {"type": "boolean"},
      "limit": {"type": "integer", "format": "int64"},
      "properties": {"type": "object", "additionalProperties": {"type": "string"}},
      "offset": {"type": "integer", "format": "int64"}
    }
  }
| .components.schemas.GridOperation.properties.action.enum = ["zoomin", "zoomout", "keeponly", "removeonly", "refresh", "pivot", "pivotToPOV", "submit"]
| .components.schemas.Slice.properties = {
    "rows": .components.schemas.Slice.properties.rows,
    "dirtyCells": .components.schemas.Slice.properties.dirtyCells,
    "dirtyTexts": .components.schemas.Slice.properties.dirtyTexts,
    "columns": .components.schemas.Slice.properties.columns,
    "data": .components.schemas.Slice.properties.data
  }
| .components.schemas.JobsInputBean.properties.jobtype.enum = ["dataload", "dimbuild", "calc", "clear", "importExcel", "exportExcel", "lcmExport", "lcmImport", "clearAggregation", "buildAggregation", "asoBufferDataLoad", "asoBufferCommit", "exportData", "mdxScript", "executeReport", "maxl", "groovy"]
| .components.schemas.DimensionMember = {
    "type": "object",
    "properties": {
      "name": {"type": "string"},
      "numberOfChildren": {"type": "integer"},
      "levelNumber": {"type": "integer"},
      "aliases": {"type": "object", "additionalProperties": {"type": "string"}},
      "uniqueName": {"type": "string"},
      "memberId": {"type": "string"},
      "previousSiblingsCount": {"type": "integer"},
      "memberSolveOrder": {"type": "integer"},
      "descendantsCount": {"type": "integer"},
      "dimension": {"type": "boolean"},
      "links": {"type": "array", "items": schema_ref("Link")},
      "dimSolveOrder": {"type": "integer"},
      "dimensionType": {"type": "string"},
      "dataStorageType": {"type": "string"},
      "formatString": {"type": "string"},
      "dimStorageType": {"type": "string"},
      "currencyConversionCategory": {"type": "string"}
    }
  }
| .components.schemas.MembersList = {
    "type": "object",
    "properties": {
      "items": {"type": "array", "items": schema_ref("MemberBean")}
    }
  }
| .components.schemas.AncestorsList = {
    "type": "array",
    "items": schema_ref("MemberBean")
  }
| .components.schemas.MemberBean.properties += {
    "dimSolveOrder": {"type": "integer"},
    "dimensionType": {"type": "string", "enum": ["TIME", "ACCOUNTS", "REGULAR", "ATTRIBUTE", "ATTRIBUTECALC"]},
    "formatString": {"type": "string"},
    "dimStorageType": {"type": "string", "enum": ["DENSE", "SPARSE"]},
    "currencyConversionCategory": {"type": "string"},
    "uda": {"type": "array", "items": {"type": "string"}},
    "dataStorageType": {"type": "string"},
    "parentName": {"type": "string"}
  }
| .components.schemas.GridLayout = {
    "type": "object",
    "properties": {
      "data": schema_ref("LayoutData"),
      "alias": {"type": "string"},
      "dimensions": {"type": "array", "items": schema_ref("GridDimension")}
    }
  }
| .components.schemas.LayoutData = {
    "type": "object",
    "properties": {
      "statuses": {"type": "array", "items": {"type": "array", "items": {"type": "string"}}},
      "texts": {"type": "array", "items": {"type": "array", "items": {"type": "string"}}},
      "enumIds": {"type": "array", "items": {"type": "array", "items": {"type": "string"}}},
      "dataFormats": {"type": "array", "items": {"type": "array", "items": {"type": "string"}}},
      "types": {"type": "array", "items": {"type": "array", "items": {"type": "string"}}},
      "filters": {"type": "array", "items": {"type": "array", "items": {"type": "string"}}},
      "values": {"type": "array", "items": {"type": "array", "items": {"type": "string"}}}
    }
  }
| .components.schemas.ParametersBean.properties.buildMethod.enum = ["PARENT-CHILD", "GENERATION"]
| .components.schemas.ParametersBean.properties.buildOption.enum = ["NONE", "RETAIN_ALL_DATA", "RETAIN_INPUT_DATA", "RETAIN_LEAF_DATA", "REMOVE_ALL_DATA"]
| .components.schemas.ParametersBean.properties.dataLevel.enum = ["ALL_DATA", "UPPER_LEVEL_BLOCKS", "NON_INPUT_BLOCKS", "LEVEL_ZERO_BLOCKS", "INPUT_LEVEL_DATA_BLOCKS"]
| .components.schemas.ParametersBean.properties.isScriptContent.type = "string"
| .components.schemas.ParametersBean.properties.lockForUpdate.type = "string"
| .components.schemas.ParametersBean.properties.useCatalogPath.type = "string"
| .components.schemas.ParametersBean.properties.exportdata.type = "string"
| .components.schemas.ParametersBean.properties.exportpartitions.type = "string"
| .components.schemas.ParametersBean.properties.exportfilters.type = "string"
| .components.schemas.ParametersBean.properties.physical.type = "string"
| .components.schemas.Rules.properties.dataSource = schema_ref("RuleDataSource")
| .components.schemas.ZoomIn.properties.ancestor.enum = ["top", "bottom"]
| .components.schemas.ZoomIn.properties.mode.enum = ["children", "descendents", "base"]
| del(.components.schemas.Preferences.properties.rowSuppression)
| .components.schemas.Preferences.properties.LatestMemberName = {"type": "object"}

# Add the authentication schemes expected by EssSharp's custom generated client.
| .components.securitySchemes = {
    "basicAuth": {"type": "http", "scheme": "basic"},
    "OAuth2": {
      "type": "oauth2",
      "flows": {
        "authorizationCode": {
          "authorizationUrl": "/oauth2/authorize",
          "tokenUrl": "/oauth2/token",
          "scopes": {}
        }
      }
    }
  }
| .security = [{"basicAuth": [], "OAuth2": []}]
