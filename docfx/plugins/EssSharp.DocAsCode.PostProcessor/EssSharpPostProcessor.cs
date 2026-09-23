using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

using Docfx.Plugins;

namespace EssSharp.DocAsCode.PostProcessor
{
    /// <summary>
    /// Points each package's EssSharp namespace entry in the generated tables of contents at that package's own namespace page.
    /// </summary>
    /// <remarks>
    /// EssSharp and EssSharp.Abstractions both declare the EssSharp namespace, and the documentation keeps the two packages in
    /// separate API sections. docfx resolves the shared namespace uid to a single page, so the namespace entry in both sections
    /// links to the same package's page. A namespace entry is repointed at the namespace page in the same folder as the types it
    /// lists, which is always its own package's page. Set the ESSSHARP_DOCFX_DEBUG environment variable to 1 to attach a debugger.
    /// </remarks>
    [Export(nameof(EssSharpPostProcessor), typeof(IPostProcessor))]
    public class EssSharpPostProcessor : IPostProcessor
    {
        private const string DebugVariable = @"ESSSHARP_DOCFX_DEBUG";
        private const string NamespaceUid  = @"EssSharp";
        private const string NamespacePage = NamespaceUid + @".html";

        private static readonly UTF8Encoding Encoding = new(false);

        private static readonly JsonSerializerOptions JsonOptions = new() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };

        // A namespace anchor in toc.html, followed by the anchor of the first type it lists.
        private static readonly Regex TocHtmlNamespace = new(
            $@"(<a href="")(?<href>[^""]*)(""[^>]*\btitle=""{Regex.Escape(NamespaceUid)}"">{Regex.Escape(NamespaceUid)}</a>\s*<ul[^>]*>\s*<li>\s*(?:<span[^>]*></span>\s*)?<a href="")(?<child>[^""]*)""",
            RegexOptions.CultureInvariant);

        /// <inheritdoc />
        public ImmutableDictionary<string, object> PrepareMetadata( ImmutableDictionary<string, object> metadata )
        {
            LaunchDebuggerIfRequested();

            return metadata;
        }

        /// <inheritdoc />
        public Manifest Process( Manifest manifest, string outputFolder, CancellationToken cancellationToken = default )
        {
            LaunchDebuggerIfRequested();

            if ( string.IsNullOrEmpty(outputFolder) )
                throw new ArgumentException("The given output folder is null or empty.", nameof(outputFolder));

            if (!Directory.Exists(outputFolder))
                return manifest;

            var repointed = 0;

            foreach (var tocJsonFile in Directory.EnumerateFiles(outputFolder, "toc.json", SearchOption.AllDirectories))
            {
                cancellationToken.ThrowIfCancellationRequested();
                repointed += RepointTocJson(tocJsonFile);
            }

            foreach (var tocHtmlFile in Directory.EnumerateFiles(outputFolder, "toc.html", SearchOption.AllDirectories))
            {
                cancellationToken.ThrowIfCancellationRequested();
                repointed += RepointTocHtml(tocHtmlFile);
            }

            Console.WriteLine($@"{nameof(EssSharpPostProcessor)}: pointed {repointed} {NamespaceUid} namespace entries at their own package's page.");

            return manifest;
        }

        /// <summary>
        /// Repoints the namespace entries in a toc.json file, and returns the number of entries changed.
        /// </summary>
        /// <param name="path">The toc.json file.</param>
        private static int RepointTocJson( string path )
        {
            if (JsonNode.Parse(File.ReadAllText(path, Encoding)) is not JsonObject toc)
                return 0;

            var repointed = RepointTocItems(toc["items"] as JsonArray);

            if (repointed > 0)
                File.WriteAllText(path, toc.ToJsonString(JsonOptions), Encoding);

            return repointed;
        }

        /// <summary>
        /// Repoints the namespace entries in a table of contents item list and its descendants, and returns the number of entries changed.
        /// </summary>
        /// <param name="items">The item list.</param>
        private static int RepointTocItems( JsonArray? items )
        {
            if (items is null)
                return 0;

            var repointed = 0;

            foreach (var item in items.OfType<JsonObject>())
            {
                if (IsSharedNamespace(item) && GetNamespaceHref((item["items"] as JsonArray)?.OfType<JsonObject>().FirstOrDefault()?["href"]?.GetValue<string>()) is { } href && (string?)item["href"] != href)
                {
                    item["href"] = href;

                    if (item.ContainsKey("topicHref"))
                        item["topicHref"] = href;

                    repointed++;
                }

                repointed += RepointTocItems(item["items"] as JsonArray);
            }

            return repointed;
        }

        /// <summary>
        /// Repoints the namespace anchors in a toc.html file, and returns the number of anchors changed.
        /// </summary>
        /// <param name="path">The toc.html file.</param>
        private static int RepointTocHtml( string path )
        {
            var repointed = 0;

            var html = File.ReadAllText(path, Encoding);
            var updatedHtml = TocHtmlNamespace.Replace(html, match =>
            {
                if (GetNamespaceHref(match.Groups["child"].Value) is not { } href || href == match.Groups["href"].Value)
                    return match.Value;

                repointed++;

                return match.Value.Substring(0, match.Groups["href"].Index - match.Index) + href + match.Value.Substring(match.Groups["href"].Index - match.Index + match.Groups["href"].Length);
            });

            if (repointed > 0)
                File.WriteAllText(path, updatedHtml, Encoding);

            return repointed;
        }

        /// <summary>
        /// Returns whether a table of contents item is an entry for the shared EssSharp namespace.
        /// </summary>
        /// <param name="item">The table of contents item.</param>
        private static bool IsSharedNamespace( JsonObject item ) =>
            (string?)item["type"] == "Namespace" && (string?)item["topicUid"] == NamespaceUid;

        /// <summary>
        /// Returns the namespace page in the same folder as the given type page, or null if there is no type page.
        /// </summary>
        /// <param name="typeHref">The href of a type the namespace entry lists.</param>
        private static string? GetNamespaceHref( string? typeHref )
        {
            if (string.IsNullOrEmpty(typeHref))
                return null;

            return typeHref.Substring(0, typeHref.LastIndexOf('/') + 1) + NamespacePage;
        }

        /// <summary>
        /// Launches a debugger when the ESSSHARP_DOCFX_DEBUG environment variable is 1.
        /// </summary>
        private static void LaunchDebuggerIfRequested()
        {
            if (Environment.GetEnvironmentVariable(DebugVariable) == "1" && !Debugger.IsAttached)
                Debugger.Launch();
        }
    }
}
