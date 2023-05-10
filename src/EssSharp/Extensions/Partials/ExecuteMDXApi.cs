using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using EssSharp.Model;

namespace EssSharp.Api
{
    /// <summary />
    public partial class ExecuteMDXApi
    {
        /// <summary>
        /// Gets the mdx response as a list of strings.
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="application">Application Name</param>
        /// <param name="database">Cube Name</param>
        /// <param name="body">Query and Preferences (optional)</param>
        public List<string> MDXGetDataMemberList( string application, string database, MDXInput body = null ) => MDXGetDataMemberListAsync(application, database, body)?.GetAwaiter().GetResult();

        /// <summary>
        /// Asynchronously gets the mdx response as a list of strings.
        /// </summary>
        /// <exception cref="EssSharp.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="application">Application Name</param>
        /// <param name="database">Cube Name</param>
        /// <param name="body">Query and Preferences (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        public async Task<List<string>> MDXGetDataMemberListAsync( string application, string database, MDXInput body = null, System.Threading.CancellationToken cancellationToken = default )
        {
            var dataList = new List<string>();

            if ( (await MDXExecuteMDXWithHttpInfoAsync(application, database, default, body, 0, cancellationToken).ConfigureAwait(false))?.RawContent is { } rawContent )
            {
                try
                {
                    if ( JObject.Parse(rawContent)?["data"] is { } data )
                        dataList = data.Children().Values<string>().ToList();
                }
                catch ( Exception e )
                {
                    throw new Exception($@"Unable to parse MDX data. {e.Message}".Trim(), e);
                }
            }

            return dataList;
        }
    }
}
