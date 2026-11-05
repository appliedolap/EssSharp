# EssSharp.Integration

Integration tests that run the library against a real Essbase server. By default the harness starts (or reuses) an
Essbase container and its repository database with Docker, so Docker must be running.

```
dotnet test --solution EssSharp.slnf --max-parallel-test-modules 1
```

The collections run in a fixed order: the available-server checks, then `CleanServerTests`, which **deletes every
application, cube, user and group on the server**, then the create, get and perform tests. Do not run the clean
collection against a server you want to keep (`--filter-not-trait "type=clean"` skips it).

A container the harness starts runs `Scripts/Essbase/wrapper.sh`, as the scoop `essbase` stack does, before the image's
start command: it gives Essbase's managed server a 4 GB heap (`ESSBASE_MANAGED_SERVER_HEAP` in the profile's
environment sets another), since with the image's 1 GB Essbase refuses drill-through reports once the heap is busy. A
reused container keeps the heap it was started with.

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
| `Managed` | The harness builds the environment: an Oracle AI Database container beside the harness's OCI gateway, which answers in canned mode, with the Essbase registry forced and the gateway configuring the AI connection. It reuses a running `<essbase container>-ai-database` and `-oci-gateway` (the names the scoop `essbase` stack uses), and removes only what it created when the run ends, unless `KeepEnvironment` is set. It pulls large images and takes several minutes on a cold start. |
| `External` | The Essbase server already has an AI connection for the application (for example one backed by a real OCI tenancy); the harness leaves it alone. |

The gateway (`Scripts/Oci/gateway`) answers each question from `answers.json`, keyed by a phrase of the question, and
embeds text deterministically, so the tests can assert what Essbase makes of an answer. To give a new test a
deterministic answer, add an entry to `answers.json`. The gateway reads it on every request.

The same gateway runs in the scoop `essbase` stack, in model mode by default. To run these tests in `External` mode
against that stack, switch its gateway to canned answers first (`essbase ai canned`, and `essbase ai local` to switch
back).

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

The AI tests are off by default so that a local run does not have to build the AI environment. The Build / Test
workflow turns them on for every push, pull request and manual run (a manual run can clear the `ai_query_tests`
input); it does so by writing an `appsettings.local.json` like the one above, in `.github/workflows/dotnet.yml`.
Against an Essbase 21.x server (older than 21.8) the tests assert that the AI endpoints are refused.

## OAuth tests

`OAuthTests` send Essbase REST the bearer tokens of an identity service, which is how Dodeca and the library use OAuth
against Essbase: a token signs the library in as the token's user, a forged or expired token is refused, and the
service's users also sign in with their passwords. `OAuth:Mode` decides where the tokens come from:

| Mode | What happens |
|---|---|
| `Disabled` (the committed default) | The OAuth tests are skipped. |
| `Managed` | The harness provides the identity service: it starts its OCI gateway (`Scripts/Oci/gateway`, the same scripts the scoop `essbase` stack runs) before Essbase, with the identity tenant and the GenAI inference host as network aliases, and creates the Essbase container in Identity Cloud mode against it (`IDENTITY_PROVIDER=IDCS`, with `Scripts/Essbase/wrapper.sh` adding the gateway's certificate authority to the domain's trust). The tokens come from the gateway's identity port, published on a random host port. A running Essbase container is reused; if it was not created in Identity Cloud mode, the tests are skipped with that reason. |
| `External` | The Essbase server already accepts an identity service's tokens, and `OAuth:TokenUrl` names its token endpoint; for the scoop stack created with `essbase identity idcs`, `http://localhost:9005/oauth2/v1/token`. |

The tests take their tokens with the password grant, unattended; Essbase gets the same bearer token whichever grant
produced it. `OAuth:Users` names the identity users the tests sign in as and the Essbase roles the collection's fixture
gives them first (`alice` as a service administrator, `bob` as a user with read access to `Sample`, `carol` with no
role): the users of the harness's identity service (`Scripts/Oci/gateway/users.json`), which the scoop stack's
identity service also knows by default.

An Essbase server in Identity Cloud mode keeps no users of its own: it looks them up in the identity service and only
keeps the roles, and refuses to create, list or delete users and groups. So when `OAuth:Mode` is `Managed` or
`External`, the tests that create or remove users and every group test are skipped; application permissions still
work and are still tested. The `PowerUser` and `User` connections must then name users of the identity service: the
harness's (`Scripts/Oci/gateway/users.json`) has `poweruser` and `user` with the committed passwords, and gives them
their service roles.

The Build / Test workflow leaves the OAuth tests off on pushes and pull requests; a manual run with the `oauth_tests`
input runs the whole suite with `OAuth:Mode` set to `Managed`.

The collection depends only on the `Sample` application and the identity users, so it can be run on its own against a
prepared server: `dotnet test --project src/EssSharp.Integration/EssSharp.Integration.csproj --filter-class EssSharp.Integration.OAuthTests`.

To run them locally against the scoop stack, add a complete `OAuth` section to `appsettings.local.json`:

```json
{
  "Settings": {
    "OAuth": {
      "Mode": "External",
      "TokenUrl": "http://localhost:9005/oauth2/v1/token",
      "ClientId": "esssharp",
      "ClientSecret": "",
      "Scope": "openid",
      "Users": [
        { "Username": "alice", "Password": "Password1", "Role": "ServiceAdministrator" },
        { "Username": "bob", "Password": "Password1", "Role": "User", "Applications": { "Sample": "db_access" } },
        { "Username": "carol", "Password": "Password1", "Role": "Unkown" }
      ]
    }
  }
}
```
