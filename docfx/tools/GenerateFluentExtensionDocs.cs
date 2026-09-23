#:package YamlDotNet@18.1.0

// Documents the FluentExtensions chaining methods on the interfaces they apply to.
//
// FluentExtensions is hidden from the docs: its methods extend Task<IEssCube>, Task<IEssFolder>, and so on, so docfx
// would only list them on a FluentExtensions page, which is not where a developer looks for them. docfx writes the
// class's metadata to obj/fluent-metadata (see filter.fluent.yml); this tool groups each method under the interface
// its Task<T> receiver produces and writes a docfx overwrite file per interface, which adds a "Chaining" section to
// that interface's remarks.
//
// Any method that cannot be placed is reported as a warning (a GitHub annotation when running in Actions) and
// skipped; the tool never fails the docs build.
//
// Usage, from the repository root, between "docfx metadata" and "docfx build":
//   dotnet run docfx/tools/GenerateFluentExtensionDocs.cs -- [metadataDir] [interfaceMetadataDir] [outputDir]

using System.Text;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

var metadataDirectory  = args.ElementAtOrDefault(0) ?? "docfx/obj/fluent-metadata";
var interfaceDirectory = args.ElementAtOrDefault(1) ?? "docfx/api/EssSharp.Abstractions";
var outputDirectory    = args.ElementAtOrDefault(2) ?? "docfx/obj/fluent-overwrite";

var warnings = 0;

try
{
    // Start from an empty output folder, so a removed method never leaves a stale section behind.
    if (Directory.Exists(outputDirectory))
        Directory.Delete(outputDirectory, recursive: true);

    Directory.CreateDirectory(outputDirectory);

    var metadataFile = Path.Combine(metadataDirectory, "EssSharp.FluentExtensions.yml");

    if (!File.Exists(metadataFile))
    {
        Warn($"No FluentExtensions metadata at {metadataFile}; run \"docfx metadata\" first. No chaining sections were generated.");
        return 0;
    }

    var methods = LoadItems(metadataFile)
        .Where(item => Scalar(item, "isExtensionMethod") == "true")
        .ToList();

    if (methods.Count == 0)
    {
        Warn($"{metadataFile} contains no extension methods. No chaining sections were generated.");
        return 0;
    }

    var sections = new Dictionary<string, List<(string Signature, string Summary)>>();

    foreach (var method in methods)
    {
        var name = Scalar(method, "name") ?? Scalar(method, "uid") ?? "(unnamed)";

        if (GetTargetInterface(method) is not { } interfaceUid)
        {
            Warn($"FluentExtensions.{name}: cannot tell which interface its receiver produces; it is not documented.");
            continue;
        }

        if (!File.Exists(Path.Combine(interfaceDirectory, interfaceUid + ".yml")))
        {
            Warn($"FluentExtensions.{name}: its receiver produces {interfaceUid}, which has no API page; it is not documented.");
            continue;
        }

        if (!sections.TryGetValue(interfaceUid, out var entries))
            sections[interfaceUid] = entries = new List<(string, string)>();

        entries.Add((GetCallSignature(method), Scalar(method, "summary") ?? string.Empty));
    }

    foreach (var (interfaceUid, entries) in sections)
    {
        var interfaceName = interfaceUid.Substring(interfaceUid.LastIndexOf('.') + 1);
        var existingRemarks = LoadItems(Path.Combine(interfaceDirectory, interfaceUid + ".yml"))
            .Where(item => Scalar(item, "uid") == interfaceUid)
            .Select(item => Scalar(item, "remarks"))
            .FirstOrDefault();

        var markdown = new StringBuilder();
        markdown.AppendLine("---");
        markdown.AppendLine($"uid: {interfaceUid}");
        markdown.AppendLine("remarks: *content");
        markdown.AppendLine("---");

        // An overwrite replaces the remarks, so keep any the interface already has.
        if (!string.IsNullOrWhiteSpace(existingRemarks))
        {
            markdown.AppendLine(existingRemarks.Trim());
            markdown.AppendLine();
        }

        markdown.AppendLine($"### Chaining from a `Task<{interfaceName}>`");
        markdown.AppendLine();
        markdown.AppendLine($"These methods can be called directly on a `Task<{interfaceName}>`, such as the one an asynchronous `Get` call returns, without awaiting it first.");
        markdown.AppendLine();
        markdown.AppendLine("| Method | Description |");
        markdown.AppendLine("|---|---|");

        foreach (var (signature, summary) in entries)
            markdown.AppendLine($"| `{signature}` | {ToTableCell(summary)} |");

        File.WriteAllText(Path.Combine(outputDirectory, interfaceUid + ".md"), markdown.ToString(), new UTF8Encoding(false));
    }

    Console.WriteLine($"Documented {sections.Values.Sum(entries => entries.Count)} of {methods.Count} chaining methods on {sections.Count} interfaces in {outputDirectory}.");
}
catch (Exception exception)
{
    Warn($"Generating the chaining sections failed, so none were generated: {exception.Message}");
}

return 0;

// Returns the interface uid a fluent method's Task<T> receiver produces: the T itself, or the IEss* interface a generic T is
// constrained to. Returns null when that is not exactly one interface.
static string? GetTargetInterface(YamlMappingNode method)
{
    var syntax = method.Children.TryGetValue(new YamlScalarNode("syntax"), out var node) ? node as YamlMappingNode : null;
    var receiverType = (syntax?.Children.TryGetValue(new YamlScalarNode("parameters"), out var parameters) is true ? parameters as YamlSequenceNode : null)?
        .Children.OfType<YamlMappingNode>().FirstOrDefault() is { } receiver ? Scalar(receiver, "type") : null;

    if (receiverType is null || Regex.Match(receiverType, @"^System\.Threading\.Tasks\.Task\{(?<t>.+)\}$") is not { Success: true } match)
        return null;

    var produced = match.Groups["t"].Value;

    // A concrete receiver, e.g. Task<IEssCube>.
    if (!produced.StartsWith('{'))
        return produced.StartsWith("EssSharp.IEss", StringComparison.Ordinal) ? produced : null;

    // A generic receiver, e.g. Task<T> where T : class, IEssScript.
    var typeParameter = produced.Trim('{', '}');
    var content = syntax is null ? null : Scalar(syntax, "content");
    var constraint = content is null ? null : Regex.Match(content, $@"\bwhere\s+{Regex.Escape(typeParameter)}\s*:\s*(?<c>[^\r\n]+?)(?:\s+where\s|$)");
    var interfaces = constraint is { Success: true }
        ? Regex.Matches(constraint.Groups["c"].Value, @"\bIEss\w+\b").Select(m => "EssSharp." + m.Value).Distinct().ToList()
        : new List<string>();

    return interfaces.Count == 1 ? interfaces[0] : null;
}

// Returns the method as a caller writes it after the dot: the declaration without "public static" and the "this" receiver.
static string GetCallSignature(YamlMappingNode method)
{
    var syntax = method.Children.TryGetValue(new YamlScalarNode("syntax"), out var node) ? node as YamlMappingNode : null;
    var content = (syntax is null ? null : Scalar(syntax, "content")) ?? Scalar(method, "name") ?? string.Empty;

    content = Regex.Replace(content, @"^public\s+static\s+", string.Empty);
    content = Regex.Replace(content, @"\(\s*this\s+[^,)]+\s+\w+\s*(,\s*)?", "(");

    return content.Replace("`", "'");
}

static string ToTableCell(string text) =>
    Regex.Replace(text, @"\s+", " ").Trim().Replace("|", "\\|");

static List<YamlMappingNode> LoadItems(string path)
{
    var stream = new YamlStream();

    using (var reader = new StreamReader(path))
        stream.Load(reader);

    var root = stream.Documents.FirstOrDefault()?.RootNode as YamlMappingNode;

    return (root?.Children.TryGetValue(new YamlScalarNode("items"), out var items) is true ? items as YamlSequenceNode : null)?
        .Children.OfType<YamlMappingNode>().ToList() ?? new List<YamlMappingNode>();
}

static string? Scalar(YamlMappingNode node, string key) =>
    node.Children.TryGetValue(new YamlScalarNode(key), out var value) ? (value as YamlScalarNode)?.Value : null;

void Warn(string message)
{
    warnings++;

    if (Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true")
        Console.WriteLine($"::warning title=Fluent extension docs::{message}");
    else
        Console.Error.WriteLine($"warning: {message}");
}
