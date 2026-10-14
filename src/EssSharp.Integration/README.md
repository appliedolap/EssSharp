# EssSharp.Integration

Integration tests that run the library against a real Essbase server. By default the harness starts (or reuses) an
Essbase container and its repository database with Docker, so Docker must be running.

```
dotnet test --solution EssSharp.slnf --max-parallel-test-modules 1
```

The collections run in a fixed order: the available-server checks, then `CleanServerTests`, which **deletes every
application, cube, user and group on the server**, then the create, get and perform tests. Do not run the clean
collection against a server you want to keep (`--filter-not-trait "type=clean"` skips it).

## Settings

Settings are read from `appsettings.json`, and from `appsettings.local.json` next to it if one exists (it is not
committed). Each top-level section of the local file **replaces** the committed section of the same name; it is not
merged. A local `Images`, `ImageSettings`, `Connections` or `AiQuery` section therefore has to be complete.

- `Images`: the Essbase image to use. The first entry is used, and an already running container named after it (for
  example `essbase-26-1`) is reused instead of starting a new one.
- `Connections`: the servers and accounts the tests use.
- `ImageSettings`: per-image (or per version prefix) container profiles.

## AI query tests

`AiQueryTests` exercise the natural-language query APIs (Essbase 26ai). `AiQuery:Mode` decides how they get an
Essbase AI connection:

| Mode | What happens |
|---|---|
| `Disabled` (the committed default) | The AI query tests are skipped, and the harness does none of the AI setup. |
| `Managed` | The harness builds the environment: an Oracle AI Database container and a fake OCI Generative AI service on a private network, with the Essbase registry and AI connection configured to use them. It reuses a running pair (`<essbase container>-ai-service` and `-ai-database`), and removes only what it created when the run ends, unless `KeepEnvironment` is set. It pulls large images and takes several minutes on a cold start. |
| `External` | The Essbase server already has an AI connection for the application (for example one backed by a real OCI tenancy); the harness leaves it alone. |

To run them locally, add a complete `AiQuery` section to `appsettings.local.json`:

```json
{
  "Settings": {
    "AiQuery": {
      "Mode": "Managed",
      "ApplicationName": "Sample",
      "CubeName": "Basic",
      "NaturalLanguageQuery": "Show actual sales by year.",
      "ProfileName": ""
    }
  }
}
```

The AI tests are off by default so that CI does not have to build the AI environment. Against an Essbase 21.x server
(older than 21.8) the tests assert that the AI endpoints are refused.
