using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics;
using System.Text;

using Docfx.Plugins;
using Newtonsoft.Json;

namespace EssSharp.DocAsCode.PostProcessor
{
    [Export(nameof(EssSharpPostProcessor), typeof(IPostProcessor))]
    public class EssSharpPostProcessor : IPostProcessor
    {
        public ImmutableDictionary<string, object> PrepareMetadata( ImmutableDictionary<string, object> metadata )
        {
            #if DEBUG
            {
                Debugger.Launch();
            }
            #endif

            if ( Debugger.IsAttached )
                Debugger.Break();

            return metadata;
        }

        public Manifest Process( Manifest manifest, string outputFolder )
        {
            #if DEBUG
            {
                Debugger.Launch();
            }
            #endif

            if ( Debugger.IsAttached )
                Debugger.Break();

            if ( string.IsNullOrEmpty(outputFolder) )
                throw new ArgumentException("The given output folder is null or empty.", nameof(outputFolder));

            var siteDirectory = new DirectoryInfo(Path.Combine(new DirectoryInfo(outputFolder).Parent?.FullName ?? string.Empty, "_site"));
            var generatedTocHtmlFile = new FileInfo(Path.Combine(siteDirectory.FullName, "toc.html"));

            var encoding = new UTF8Encoding(false);

            if ( generatedTocHtmlFile.Exists )
            {
                var hasReplacedLine = false;

                var updatedTocHtmlLines = File.ReadAllLines(generatedTocHtmlFile.FullName, encoding)
                    .Select(line =>
                    {
                        if ( !hasReplacedLine && line.Contains(@"api/EssSharp.Abstractions/EssSharp.html") )
                        {
                            hasReplacedLine = true;
                            return line.Replace(@"api/EssSharp.Abstractions/EssSharp.html", @"api/EssSharp/EssSharp.html");
                        }
                        return line;
                    });

                File.WriteAllLines(generatedTocHtmlFile.FullName, updatedTocHtmlLines, encoding);
            }

            var generatedTocJsonFile = new FileInfo(Path.Combine(siteDirectory.FullName, "toc.json"));

            if ( generatedTocJsonFile.Exists )
            {
                var generatedTocJsonString = File.ReadAllText(generatedTocJsonFile.FullName, encoding);

                dynamic generatedTocJson = JsonConvert.DeserializeObject(generatedTocJsonString) 
                    ?? throw new Exception("Unable to deserialize the toc.json");

                generatedTocJson["items"][0]["items"][0]["href"]      = @"api/EssSharp/EssSharp.html";
                generatedTocJson["items"][0]["items"][0]["topicHref"] = @"api/EssSharp/EssSharp.html";
                string updatedTocJsonString = JsonConvert.SerializeObject(generatedTocJson);
                File.WriteAllText(generatedTocJsonFile.FullName, updatedTocJsonString, encoding);
            }

            return manifest;
        }
    }
}