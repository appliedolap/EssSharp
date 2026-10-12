# Essbase 26.1 REST API specification

`openapi.json` is the Essbase 26ai (26.1) REST API document exactly as Oracle publishes it. It is an
OpenAPI 3.0.1 document, unlike the Swagger 2.0 `swagger.json` files of Essbase 21.x. Do not edit it; every
correction EssSharp needs is applied later by `process.sh` (through `process-openapi3.jq`), the generator
options in `gen.sh`/`gen.cmd`, and the templates.

| | |
|---|---|
| Essbase release | 26.1.0.0.0 |
| Source | https://docs.oracle.com/en/database/other-databases/essbase/26/essrt/swagger.json |
| Retrieved | 2026-09-08 |
| Embedded `info.version` | `2026.08.11` |
| OpenAPI version | `3.0.1` (the document also carries a stray `"swagger": "2.0"` key) |
| SHA-256 | `4becd31b214cdc54978fcf12b3268c402071125922a7de50de67000766aabae9` |
| Size | 1,431,675 bytes, 61 tags, 290 paths, 420 operations, 262 schemas |

The retrieval was recorded when the file was first archived; the checksum above was re-verified against the
committed bytes when it was added to this branch.

## Cross-check against a running server

The document was compared with the spec served by an Essbase 26.1.0.0.0 (build 285) test container at
`/essbase/rest/v1/openapi.json` (OpenAPI 3.0.1, `info.version` `V1`, SHA-256 `809b8618...` when compared).

- The 396 operations present in both are structurally identical: same parameters, request body content
  types, and response codes.
- The 256 schemas present in both are structurally identical (descriptions aside) except one value: the
  published `ZoomIn.mode` enumeration says `descendants`, while the server serves, and expects, `descendents`.
  `process-openapi3.jq` restores the server's spelling.
- The published document adds tags and the HTML descriptions, 18 operations the compared server does not
  serve (for example `/cubedesigner/*`), and 6 request schemas for them.
- The server serves 92 operations that Oracle does not publish (for example `/backup/*`, outline `edit` and
  `qedit`, `/ess-mcp`, `/amw`, `/odbc`). They are untagged, so a generator would put them in `DefaultApi`.
  EssSharp deliberately does not wrap them.

Because the published document is tagged and has no duplicate operationIds, it generates the same API class
structure EssSharp already has. Capture the served document again (and compare) when moving to a newer build.

## Adding a new version

1. Save the document unmodified in `versions/<ver>/` (`openapi.json` for OpenAPI 3, `swagger.json` for Swagger 2).
2. Record the source URL, retrieval date, server version/build, `info.version` and SHA-256 here.
3. Run `process.sh <ver>`, then `gen.sh`.
