# Corrections applied to the Essbase OpenAPI document before code generation. gen.sh and gen.cmd run this on
# the newest specification in versions/ and generate the client from the result.
#
# Keep this file small. Before adding anything here, check whether the correction can be made another way:
#   * a template override in templates/ (for example the "*/*" request content type in ApiClient.mustache),
#   * a generator option in gen.sh/gen.cmd (model and operationId name mappings), or
#   * a hand-written partial class in src/EssSharp/Extensions/Partials.
# What remains here is the response shapes, response models, and enum/property fixes that the generator
# and templates cannot know about for the operations EssSharp wraps.

# jq silently creates any path an assignment names, so a correction aimed at an operation or schema that
# Oracle renamed or removed would quietly add junk instead of failing. Likewise, a correction that replaces
# something Oracle later starts to define would quietly discard Oracle's version. These helpers fail instead.
def require($path):
  if getpath($path) == null
  then error("process.jq: cannot patch missing OpenAPI path: " + ($path | map(tostring) | join(" > ")))
  else .
  end;

def require_absent($path):
  if getpath($path) != null
  then error("process.jq: the document now defines OpenAPI path: " + ($path | map(tostring) | join(" > ")) + ", review the correction that replaces it")
  else .
  end;

def schema_ref($name):
  {"$ref": ("#/components/schemas/" + $name)};

# Replaces the schema of every media type of an operation's response, adding application/json when the
# response documents no content at all.
def set_response_schema($path; $method; $status; $schema):
  require(["paths", $path, $method, "responses", $status])
  | .paths[$path][$method].responses[$status] |=
      (.content =
        (if (((.content // {}) | length) == 0)
         then {"application/json": {"schema": $schema}}
         else (.content | with_entries(.value.schema = $schema))
         end));

# The document gives some responses only the wildcard "*/*". Essbase 21 answers a wildcard Accept header in a form
# the client cannot read, so declare the JSON these operations return.
def use_json_response($path; $method; $status):
  require(["paths", $path, $method, "responses", $status, "content"])
  | .paths[$path][$method].responses[$status].content |=
      {"application/json": (.["application/json"] // .["*/*"] // (to_entries[0].value))};

# Declares the media types of a response that the document leaves without any content.
def set_response_content($path; $method; $status; $content):
  require(["paths", $path, $method, "responses", $status])
  | .paths[$path][$method].responses[$status].content = $content;

# Defines a model that the document does not define.
def define_schema($name; $schema):
  require_absent(["components", "schemas", $name])
  | .components.schemas[$name] = $schema;

# Adds properties that the document does not declare to an existing schema.
def add_schema_properties($schema; $properties):
  require(["components", "schemas", $schema, "properties"])
  | reduce ($properties | keys[]) as $property
      (.; require_absent(["components", "schemas", $schema, "properties", $property]))
  | .components.schemas[$schema].properties += $properties;

# Sets a property of an existing schema, for example its type.
def set_schema_property($schema; $property; $key; $value):
  require(["components", "schemas", $schema, "properties", $property])
  | .components.schemas[$schema].properties[$property][$key] = $value;

# Declares the allowed values of a property that the document leaves as a free string.
def set_schema_enum($schema; $property; $values):
  require(["components", "schemas", $schema, "properties", $property])
  | require_absent(["components", "schemas", $schema, "properties", $property, "enum"])
  | .components.schemas[$schema].properties[$property].enum = $values;

# Tags each untagged operation with the tag of the longest path prefix in $tags that it falls under, and names
# it after that tag as Oracle names its tagged operations ("Tag.operation"), dropping a numeric suffix such as
# _1 where the name is unique within the tag. An untagged operation under none of the prefixes fails, so a new
# one is given a tag deliberately instead of landing in a generated DefaultApi.
def tag_untagged_operations($tags):
  def untagged: type == "object" and has("operationId") and ((.tags // []) | length) == 0;
  def base_name: .operationId | sub("_[0-9]+$"; "");
  .paths |= with_entries(
      .key as $path
      | .value |= with_entries(
          if (.value | untagged)
          then ([$tags | to_entries[] | select(.key as $prefix | $path == $prefix or ($path | startswith($prefix + "/")))]
                | max_by(.key | length)) as $match
            | if $match == null
              then error("process.jq: no tag for the untagged operation " + (.key | ascii_upcase) + " " + $path + ", add one to the tags")
              else .value.tags = [$match.value] | .value["x-process-tagged"] = true
              end
          else .
          end))
  | ([.paths[][] | objects | select(.["x-process-tagged"]) | [.tags[0], base_name]]
     | group_by(.) | map(select(length > 1) | .[0])) as $ambiguous
  | .paths |= map_values(map_values(
      if type == "object" and .["x-process-tagged"]
      then [.tags[0], base_name] as $key
           | .operationId = (.tags[0] | gsub(" "; "")) + "."
                            + (if ($ambiguous | index([$key])) then .operationId else base_name end)
           | del(.["x-process-tagged"])
      else .
      end));

# Oracle documents several list endpoints as an array of a paged collection (for example an array of
# DimensionList) when the server returns the collection object itself. A schema is a paged collection when it
# has both the items and totalResults properties. Use the collection as the response schema.
def unwrap_collection_arrays:
  .components.schemas as $schemas
  | def collection($ref):
      ($ref | startswith("#/components/schemas/"))
      and (($schemas[$ref | ltrimstr("#/components/schemas/")].properties // {}) | has("items") and has("totalResults"));
    def unwrap:
      if .schema.type? == "array" and ((.schema.items["$ref"]? // "") as $ref | $ref != "" and collection($ref))
      then .schema = .schema.items
      else .
      end;
    .paths |= map_values(map_values(
      if type == "object" and has("responses")
      then .responses |= with_entries(
             if (.key | startswith("2")) and (.value | has("content"))
             then .value.content |= map_values(unwrap)
             else .
             end)
      else .
      end));

# The server serves operations that Oracle does not publish, without tags. Tag them by path, so that each group
# is generated into its own API class.
tag_untagged_operations({
    "/amw": "AMW",
    "/backup": "Backup",
    "/catalog_old": "Legacy Catalog",
    "/ess-mcp": "Essbase MCP",
    "/event": "Events",
    "/fastwriteback": "Fast Writeback",
    "/federatedoverdatasource": "Federated Over Datasource",
    "/federatedsample": "Federated Sample",
    "/odbc": "ODBC",
    "/outline/{app}/{cube}/edit": "Outline Editor",
    "/outline/{app}/{cube}/qedit": "Outline Quick Editor",
    "/security": "Security",
    "/service": "Service",
    "/version": "Version"
  })

# OpenAPI 3 request bodies are unnamed, so the generator would name the C# parameter after the body's schema.
# Swagger 2 body parameters were named "body" and the wrapper code relies on that name.
| .paths |= map_values(map_values(
    if type == "object" and has("requestBody")
    then .requestBody["x-codegen-request-body-name"] = "body"
    else .
    end))

# ---- Response shapes ----

| unwrap_collection_arrays
| set_response_schema("/about/instance"; "get"; "200"; schema_ref("AboutInstance"))
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/dtreports/list"; "post"; "200"; {"type": "array", "items": {"type": "string"}})
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/grid/layout"; "post"; "200"; schema_ref("GridLayout"))
| set_response_schema("/applications/{applicationName}/databases/{databaseName}/scripts/{scriptName}/content"; "get"; "200"; schema_ref("ScriptContent"))
| set_response_schema("/files"; "get"; "200"; schema_ref("FileCollectionResponse"))
| set_response_schema("/files/{path}"; "get"; "200"; schema_ref("FileCollectionResponse"))
| set_response_schema("/groups/{id}/members/users"; "post"; "200"; schema_ref("Users"))
| set_response_schema("/groups/{id}/members/groups"; "post"; "200"; schema_ref("Groups"))
| set_response_schema("/outline/{app}/{cube}"; "get"; "200"; schema_ref("MembersList"))
| set_response_schema("/outline/{app}/{cube}/ancestors/{memberUniqueName}"; "get"; "200"; schema_ref("AncestorsList"))
| set_response_schema("/sessions"; "get"; "200"; {"type": "array", "items": schema_ref("SessionAttributes")})
| set_response_schema("/urls"; "get"; "200"; schema_ref("EssbaseURLList"))
| use_json_response("/applications/{applicationName}/databases/{databaseName}/scripts"; "post"; "200")
| use_json_response("/applications/{applicationName}/databases/{databaseName}/scripts/{scriptName}"; "put"; "200")
| use_json_response("/applications/{applicationName}/databases/{databaseName}/locks/objects/lock"; "post"; "200")

# Execute MDX returns JSON, but the document gives its response no content, so the generator would return nothing
# and send no Accept header. Essbase 26 rejects every proper subset of the media types this endpoint produces,
# so all three must reach the Accept header. The JSON type carries its charset parameter because
# ClientUtils.SelectHeaderAccept collapses the header to "application/json" alone, a proper subset, whenever that
# exact string appears in the list.
| set_response_content("/applications/{application}/databases/{database}/mdx"; "post"; "200"; {
    "application/octet-stream": {"schema": {"type": "object"}},
    "text/html": {"schema": {"type": "object"}},
    "application/json;charset=UTF-8": {"schema": {"type": "object"}}
  })

# Downloads that the document leaves without any content.
| set_response_content("/applications/{applicationName}/logs/all"; "get"; "200"; {
    "application/zip": {"schema": {"type": "string", "format": "binary"}},
    "application/octet-stream": {"schema": {"type": "string", "format": "binary"}}
  })
| set_response_content("/applications/{applicationName}/logs/latest"; "get"; "200"; {
    "application/octet-stream": {"schema": {"type": "string", "format": "binary"}}
  })
| set_response_content("/utilities/{id}"; "get"; "200"; {
    "application/zip": {"schema": {"type": "string", "format": "binary"}},
    "application/octet-stream": {"schema": {"type": "string", "format": "binary"}}
  })

# The document declares the export outline to XML response as octet-stream without a schema, so the client would
# discard the XML.
| set_response_content("/outline/{app}/{cube}/xml"; "post"; "default"; {
    "application/octet-stream": {"schema": {"type": "string", "format": "binary"}}
  })

# The document names two query parameters of the export outline to XML operation with their HTML descriptions
# instead of their names.
| require(["paths", "/outline/{app}/{cube}/xml", "post", "parameters"])
| .paths["/outline/{app}/{cube}/xml"].post.parameters = [
    {"name": "app", "in": "path", "description": "Application name.", "required": true, "schema": {"type": "string"}},
    {"name": "cube", "in": "path", "description": "Database name.", "required": true, "schema": {"type": "string"}},
    {"name": "connection", "in": "query", "description": "Essbase connection name.", "required": false, "schema": {"type": "string"}},
    {"name": "applicationNameForConnection", "in": "query", "description": "Application name for connection.", "required": false, "schema": {"type": "string"}}
  ]

# The document gives the file upload operation no request body, but the stream is the body.
| require(["paths", "/files/{path}", "put"])
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

# ---- Response models the document does not define ----

| define_schema("AboutInstance"; {
    "type": "object",
    "properties": {
      "provisioningSupported": {"type": "boolean"},
      "serviceRoleProvisioningSupported": {"type": "boolean"},
      "appRoleProvisioningSupported": {"type": "boolean"},
      "resetPasswordSupported": {"type": "boolean"},
      "easInstalled": {"type": "boolean"},
      "aiEnabled": {"type": "boolean"},
      "aiMdxEnabled": {"type": "boolean"},
      "aiAskEssbaseEnabled": {"type": "boolean"},
      "aiCalcEnabled": {"type": "boolean"},
      "aiSemanticSearchEnabled": {"type": "boolean"}
    },
    "xml": {"name": "aboutInstance"}
  })
| define_schema("EssbaseURL"; {
    "type": "object",
    "properties": {
      "application": {"type": "string"},
      "url": {"type": "string"}
    }
  })
| define_schema("EssbaseURLList"; {
    "type": "object",
    "properties": {
      "items": {"type": "array", "items": schema_ref("EssbaseURL")}
    }
  })
| define_schema("FileBean"; {
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
  })
| define_schema("FileCollectionResponse"; {
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
  })
| define_schema("ScriptContent"; {
    "type": "object",
    "properties": {"content": {"type": "string"}}
  })
| define_schema("MembersList"; {
    "type": "object",
    "properties": {
      "items": {"type": "array", "items": schema_ref("MemberBean")}
    }
  })
| define_schema("AncestorsList"; {
    "type": "array",
    "items": schema_ref("MemberBean")
  })
| define_schema("GridLayout"; {
    "type": "object",
    "properties": {
      "data": schema_ref("LayoutData"),
      "alias": {"type": "string"},
      "dimensions": {"type": "array", "items": schema_ref("GridDimension")}
    }
  })

# ---- Schema corrections ----

# The document defines both 'Datasource' and 'DataSource'. Names that differ only by case cannot be generated
# reliably (the generated file names depend on the operating system's case sensitivity), so DataSource, the
# data source of a rule, becomes RuleDataSource. Rules is its only reference.
| require(["components", "schemas", "DataSource"])
| .components.schemas |= with_entries(if .key == "DataSource" then .key = "RuleDataSource" else . end)
| require(["components", "schemas", "Rules", "properties", "dataSource", "$ref"])
| .components.schemas.Rules.properties.dataSource["$ref"] = "#/components/schemas/RuleDataSource"

# Only the connection and type properties of a datasource are required.
| require(["components", "schemas", "Datasource"])
| .components.schemas.Datasource.required = ["connection", "type"]

# The document leaves the properties of these job parameters untyped or too loosely typed.
| set_schema_enum("JobsInputBean"; "jobtype"; ["dataload", "dimbuild", "calc", "clear", "importExcel", "exportExcel", "lcmExport", "lcmImport", "clearAggregation", "buildAggregation", "asoBufferDataLoad", "asoBufferCommit", "exportData", "mdxScript", "executeReport", "maxl", "groovy"])
| set_schema_enum("ParametersBean"; "buildMethod"; ["PARENT-CHILD", "GENERATION"])
| set_schema_enum("ParametersBean"; "buildOption"; ["NONE", "RETAIN_ALL_DATA", "RETAIN_INPUT_DATA", "RETAIN_LEAF_DATA", "REMOVE_ALL_DATA"])
| set_schema_enum("ParametersBean"; "dataLevel"; ["ALL_DATA", "UPPER_LEVEL_BLOCKS", "NON_INPUT_BLOCKS", "LEVEL_ZERO_BLOCKS", "INPUT_LEVEL_DATA_BLOCKS"])

# Essbase expects these job parameters as strings, not booleans.
| reduce ("isScriptContent", "lockForUpdate", "useCatalogPath", "exportdata", "exportpartitions", "exportfilters", "physical") as $property
    (.; set_schema_property("ParametersBean"; $property; "type"; "string"))

# The properties of the MemberBean definition are incomplete.
| add_schema_properties("MemberBean"; {
    "dimSolveOrder": {"type": "integer"},
    "dimensionType": {"type": "string", "enum": ["TIME", "ACCOUNTS", "REGULAR", "ATTRIBUTE", "ATTRIBUTECALC"]},
    "formatString": {"type": "string"},
    "dimStorageType": {"type": "string", "enum": ["DENSE", "SPARSE"]},
    "currencyConversionCategory": {"type": "string"},
    "uda": {"type": "array", "items": {"type": "string"}},
    "dataStorageType": {"type": "string"},
    "parentName": {"type": "string"}
  })

# The generated model's JSON member order follows the schema, and the Essbase grid endpoint rejects grids
# posted back when rows appears after the range data.
| if (.components.schemas.Slice.properties // {} | keys) != ["columns", "data", "dirtyCells", "dirtyTexts", "rows"]
  then error("process.jq: Slice properties changed, review the property order correction")
  else .
  end
| .components.schemas.Slice.properties = {
    "rows": .components.schemas.Slice.properties.rows,
    "dirtyCells": .components.schemas.Slice.properties.dirtyCells,
    "dirtyTexts": .components.schemas.Slice.properties.dirtyTexts,
    "columns": .components.schemas.Slice.properties.columns,
    "data": .components.schemas.Slice.properties.data
  }

# Preferences declares both "rowSupression" and "rowSuppression". The hand-written Preferences partial
# already defines RowSuppression to serialize the corrected spelling, so the generator must not.
| require(["components", "schemas", "Preferences", "properties", "rowSuppression"])
| del(.components.schemas.Preferences.properties.rowSuppression)

# The server sends and accepts LatestMemberName, which the document does not declare.
| add_schema_properties("Preferences"; {"LatestMemberName": {"type": "object"}})

# ---- Security ----

# Add the authentication schemes that EssSharp's customised generated client expects. The document declares none.
| require_absent(["components", "securitySchemes"])
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
