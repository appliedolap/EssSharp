# Essbase 26.1 REST API specification

`openapi.json` is the Essbase 26ai (26.1) REST API document exactly as a running Essbase 26.1 server serves
it at `/essbase/rest/v1/openapi.json`. EssSharp is generated from the document the server serves, not from
the one Oracle publishes with its documentation, because the server is the authority on what the client can
call. Do not edit it; every correction EssSharp needs is applied later by `process.jq`, the generator options
in `gen.sh`/`gen.cmd`, and the templates.

| | |
|---|---|
| Essbase release | 26.1.0.0.0, build 285 (`appliedolap/essbase:26.1-latest`) |
| Source | `GET /essbase/rest/v1/openapi.json` on the running server |
| Retrieved | 2026-10-14 |
| Embedded `info.version` | `V1` |
| OpenAPI version | `3.0.1` |
| SHA-256 | `3fc870cbde7bccbbfa59855222fb097fdcbc3581f5effc3f8391e8792759267d` |
| Size | 593,896 bytes, 60 tags, 348 paths, 494 operations (92 of them untagged), 256 schemas |

## Compared with Oracle's published document

`openapi-published.json` is the document Oracle publishes for the same release, kept for reference only:

| | |
|---|---|
| Source | https://docs.oracle.com/en/database/other-databases/essbase/26/essrt/swagger.json |
| Retrieved | 2026-09-08 |
| Embedded `info.version` | `2026.08.11` |
| SHA-256 | `4becd31b214cdc54978fcf12b3268c402071125922a7de50de67000766aabae9` |
| Size | 1,431,675 bytes, 61 tags, 290 paths, 420 operations, 262 schemas |

- The 402 operations in both documents are structurally identical, and carry the same tags. Oracle's
  document adds HTML descriptions and its own `x-internal-id` and `x-filename-id` keys.
- The 256 schemas the server serves are structurally identical in Oracle's document except one value: the
  published `ZoomIn.mode` enumeration says `descendants`, while the server serves, and expects, `descendents`.
- Oracle's document adds 18 operations that build 285 does not serve (the 17 `/cubedesigner/*` operations
  and `POST /connections/clone`; each answers 404 or 405), and 6 request schemas for them.
- The server serves 92 operations that Oracle does not publish (for example `/backup/*`, outline `edit` and
  `qedit`, `/amw`, `/odbc`). They are untagged, so `process.jq` tags them by path and names their operations
  after those tags, as Oracle names the rest.

## Adding a new version

1. Save the document the newest server serves, unmodified, as `versions/<ver>/openapi.json`. The client is
   generated from the newest version only.
2. Record the server version and build, retrieval date, `info.version` and SHA-256 here.
3. Run `gen.sh` (or `gen.cmd`), which applies `process.jq` and generates the client. `process.jq` stops on any
   correction whose target has changed, and on any untagged operation it has no tag for.
