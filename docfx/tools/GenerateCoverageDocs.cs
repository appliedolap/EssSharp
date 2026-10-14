// Reports which endpoints of the newest Essbase specification the EssSharp contracts reach.
//
// Every endpoint of the newest versions/*/openapi.json is matched, by HTTP method and path, to the generated
// EssSharp.Api method that calls it (each generated method records its operation and path). An endpoint is exposed
// when a member of the EssSharp.Abstractions contracts reaches that method through the hand-written implementation
// in src/EssSharp, following calls from member to member. A generated method counts as called only when it is
// called on an API object (api.Method(...), jobsApi.Method(...)) or from a partial of its own API class, so a
// hand-written member that shares a generated method's name never counts as a call to it. An endpoint that the
// implementation calls but no contract member reaches is reported separately. Calls between hand-written members
// are followed by name, so the result can overstate coverage only where a contract member shares its name with an
// unrelated member. It measures the code rather than a kept list.
//
// Usage, from the repository root:
//   dotnet run docfx/tools/GenerateCoverageDocs.cs -- [repositoryRoot] [outputFile]

using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

var root       = args.ElementAtOrDefault(0) ?? ".";
var outputFile = args.ElementAtOrDefault(1) ?? Path.Combine(root, "docfx", "articles", "coverage.md");

var sourceDirectory   = Path.Combine(root, "src", "EssSharp");
var partialsDirectory = Path.Combine(sourceDirectory, "Extensions", "Partials");
var contractDirectory = Path.Combine(root, "src", "EssSharp.Abstractions");

// ---- The newest specification ----

var versionDirectory = Directory.GetDirectories(Path.Combine(root, "versions"))
    .Where(directory => Version.TryParse(Path.GetFileName(directory), out _))
    .OrderBy(directory => Version.Parse(Path.GetFileName(directory)))
    .Last();

var version = Path.GetFileName(versionDirectory);

using var specification = JsonDocument.Parse(File.ReadAllText(Path.Combine(versionDirectory, "openapi.json")));

var httpMethods = new[] { "get", "put", "post", "delete", "patch", "head", "options" };

var endpoints = specification.RootElement.GetProperty("paths").EnumerateObject()
    .SelectMany(path => path.Value.EnumerateObject()
        .Where(operation => httpMethods.Contains(operation.Name))
        .Select(operation => new Endpoint(operation.Name.ToUpperInvariant(), path.Name, GetTag(operation.Value))))
    .ToList();

// ---- Generated methods, keyed by HTTP method and path ----

var operationPattern = new Regex(@"localVarRequestOptions\.Operation = ""(?<class>\w+)\.(?<method>\w+)"";");
var requestPattern   = new Regex(@"Client\.(?<verb>Get|Post|Put|Delete|Patch|Head|Options)(?:Async)?<[^(]*>\(""(?<path>[^""]+)""");

var methodsByEndpoint = new Dictionary<(string Verb, string Path), SortedSet<string>>();

foreach (var file in Directory.GetFiles(Path.Combine(sourceDirectory, "Api"), "*.cs").Concat(Directory.GetFiles(partialsDirectory, "*.cs")))
{
    var text = File.ReadAllText(file);

    foreach (Match operation in operationPattern.Matches(text))
    {
        if (requestPattern.Match(text, operation.Index) is not { Success: true } request)
            continue;

        var key = (request.Groups["verb"].Value.ToUpperInvariant(), request.Groups["path"].Value);

        if (!methodsByEndpoint.TryGetValue(key, out var methods))
            methodsByEndpoint[key] = methods = new SortedSet<string>(StringComparer.Ordinal);

        methods.Add($"{operation.Groups["class"].Value}.{operation.Groups["method"].Value}");
    }
}

// ---- The hand-written implementation: the names each member calls ----

var declarationPattern = new Regex(
    @"^\s*(?:(?:public|internal|private|protected|static|async|override|virtual|sealed|new|partial|readonly|extern)\s+)+[\w<>\[\],.?\s()]*?\b(?<name>\w+)\s*(?:<[^>]*>)?\s*(?:\(|\{|=>)",
    RegexOptions.Multiline);

var implementationFiles = Directory.GetFiles(sourceDirectory, "*.cs")
    .Concat(Directory.GetFiles(Path.Combine(sourceDirectory, "Extensions"), "*.cs"))
    .Concat(Directory.GetFiles(partialsDirectory, "*.cs"))
    .ToList();

var callsByMember    = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);
var apiCallsByMember = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);
var calledAnywhere   = new HashSet<string>(StringComparer.Ordinal);

foreach (var file in implementationFiles)
{
    var text         = File.ReadAllText(file);
    var declarations = declarationPattern.Matches(text).Cast<Match>().ToList();
    var isPartial    = Path.GetFullPath(file).StartsWith(Path.GetFullPath(partialsDirectory), StringComparison.OrdinalIgnoreCase);

    for (var i = 0; i < declarations.Count; i++)
    {
        var name  = BaseName(declarations[i].Groups["name"].Value);
        var start = declarations[i].Index + declarations[i].Length;
        var end   = i + 1 < declarations.Count ? declarations[i + 1].Index : text.Length;
        var body  = text[start..end];

        if (!callsByMember.TryGetValue(name, out var calls))
            callsByMember[name] = calls = new HashSet<string>(StringComparer.Ordinal);

        if (!apiCallsByMember.TryGetValue(name, out var apiCalls))
            apiCallsByMember[name] = apiCalls = new HashSet<string>(StringComparer.Ordinal);

        calls.UnionWith(CalledNames(body));

        // A partial of a generated API class calls that class's methods directly; elsewhere they are called on an API object.
        apiCalls.UnionWith(isPartial ? CalledNames(body) : ApiCalledNames(body));
    }

    // The partials only count through the members that call them.
    if (!isPartial)
        calledAnywhere.UnionWith(CalledNames(text));
}

// ---- The contracts: every member declared in EssSharp.Abstractions ----

var contractMembers = Directory.GetFiles(contractDirectory, "*.cs", SearchOption.AllDirectories)
    .Where(file => !IsBuildOutput(file))
    .Select(File.ReadAllText)
    .SelectMany(text => CalledNames(text).Concat(Regex.Matches(text, @"\b(?<name>\w+)\s*\{\s*get").Select(match => match.Groups["name"].Value)))
    .ToHashSet(StringComparer.Ordinal);

// ---- Generated methods reached from the contracts, and from anywhere in the implementation ----

var reached         = ApiCalls(Reach(contractMembers, callsByMember), apiCallsByMember);
var reachedAnywhere = ApiCalls(Reach(calledAnywhere, callsByMember), apiCallsByMember);

// ---- Classify each endpoint ----

var rows = endpoints
    .Select(endpoint =>
    {
        var methods = methodsByEndpoint.TryGetValue((endpoint.Verb, endpoint.Path), out var found) ? found.ToList() : new List<string>();
        var names   = methods.Select(method => method.Split('.')[1]).ToList();
        var state   = methods.Count == 0 ? State.NotGenerated
                    : names.Any(reached.Contains) ? State.Exposed
                    : names.Any(reachedAnywhere.Contains) ? State.Implemented
                    : State.Generated;

        return (Endpoint: endpoint, Methods: methods, State: state);
    })
    .ToList();

int Count( State state ) => rows.Count(row => row.State == state);

// ---- Write the report ----

var report = new StringBuilder();

report.AppendLine($"# Coverage of the Essbase {version} REST API");
report.AppendLine();
report.AppendLine($"Every endpoint in the Essbase {version} specification, and whether the EssSharp contracts reach it. This page is");
report.AppendLine("generated from the code: each endpoint is matched to the generated `EssSharp.Api` method that calls it, and an");
report.AppendLine("endpoint is exposed when a member of an `EssSharp.Abstractions` interface reaches that method.");
report.AppendLine();
report.AppendLine("| State | Count | Meaning |");
report.AppendLine("| --- | ---: | --- |");
report.AppendLine($"| Exposed | {Count(State.Exposed)} | An `IEss*` member reaches this endpoint. |");
report.AppendLine($"| Implemented, not in the contracts | {Count(State.Implemented)} | An `Ess*` class calls this endpoint, but no `IEss*` member reaches it. |");
report.AppendLine($"| Generated, not exposed | {Count(State.Generated)} | A generated method exists, but nothing calls it yet. |");
report.AppendLine($"| Not generated | {Count(State.NotGenerated)} | No generated method calls this endpoint. |");
report.AppendLine($"| **Total** | **{rows.Count}** | |");

var sections = new[]
{
    (State.NotGenerated, "Not generated"),
    (State.Implemented,  "Implemented, not in the contracts"),
    (State.Generated,    "Generated, not exposed"),
    (State.Exposed,      "Exposed")
};

foreach (var (state, heading) in sections)
{
    var stateRows = rows.Where(row => row.State == state).ToList();

    if (stateRows.Count == 0)
        continue;

    report.AppendLine();
    report.AppendLine($"## {heading}");

    foreach (var group in stateRows.GroupBy(row => row.Endpoint.Tag).OrderBy(group => group.Key, StringComparer.OrdinalIgnoreCase))
    {
        report.AppendLine();
        report.AppendLine($"<details><summary><strong>{group.Key}</strong> ({group.Count()})</summary>");
        report.AppendLine();

        foreach (var row in group.OrderBy(row => row.Endpoint.Path, StringComparer.Ordinal).ThenBy(row => row.Endpoint.Verb, StringComparer.Ordinal))
        {
            var methods = row.Methods.Count > 0 ? " - " + string.Join(", ", row.Methods.Select(method => $"`{method}`")) : "";
            report.AppendLine($"- `{row.Endpoint.Verb}` `{row.Endpoint.Path}`{methods}");
        }

        report.AppendLine();
        report.AppendLine("</details>");
    }
}

Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(outputFile))!);
File.WriteAllText(outputFile, report.ToString());

Console.WriteLine($"Essbase {version}: {Count(State.Exposed)} exposed, {Count(State.Implemented)} implemented but not in the contracts, " +
                  $"{Count(State.Generated)} generated but not exposed, {Count(State.NotGenerated)} not generated, {rows.Count} total.");
Console.WriteLine($"Wrote {outputFile}");

return 0;

// Gets the first tag Oracle gives an operation, which names the generated API class it lands in.
static string GetTag( JsonElement operation ) =>
    operation.TryGetProperty("tags", out var tags) && tags.GetArrayLength() > 0 && tags[0].GetString() is { } tag ? tag : "(untagged)";

// Strips the suffixes of a generated method's variants, so every variant counts as the same operation.
static string BaseName( string name ) =>
    Regex.Replace(name, "(WithHttpInfoAsync|WithHttpInfo|Async)$", "");

// Finds the base names of the methods the given code calls (or, in a contract, declares).
static IEnumerable<string> CalledNames( string code ) =>
    Regex.Matches(code, @"\b(?<name>\w+)\s*(?:<[^>()]*>)?\s*\(")
        .Select(match => BaseName(match.Groups["name"].Value));

// Finds the base names of the methods the given code calls on an API object, such as api.Method(...).
static IEnumerable<string> ApiCalledNames( string code ) =>
    Regex.Matches(code, @"(?:\b\w*[Aa]pi|GetApi<\w+>\(\))\s*\.\s*(?<name>\w+)\s*\(")
        .Select(match => BaseName(match.Groups["name"].Value));

// Collects the generated methods the given hand-written members call.
static HashSet<string> ApiCalls( HashSet<string> members, Dictionary<string, HashSet<string>> apiCallsByMember ) =>
    members.Where(apiCallsByMember.ContainsKey).SelectMany(member => apiCallsByMember[member]).ToHashSet(StringComparer.Ordinal);

// Follows calls, by member name, from the given members.
static HashSet<string> Reach( IEnumerable<string> start, Dictionary<string, HashSet<string>> callsByMember )
{
    var reached = new HashSet<string>(StringComparer.Ordinal);
    var pending = new Stack<string>(start);

    while (pending.TryPop(out var name))
    {
        if (reached.Add(name) && callsByMember.TryGetValue(name, out var calls))
        {
            foreach (var call in calls)
                pending.Push(call);
        }
    }

    return reached;
}

// Whether a file is build output rather than source.
static bool IsBuildOutput( string file ) =>
    file.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).Any(part => part is "bin" or "obj");

record Endpoint( string Verb, string Path, string Tag );

enum State { Exposed, Implemented, Generated, NotGenerated }
