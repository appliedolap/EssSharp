using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;

using Newtonsoft.Json.Linq;

namespace EssSharp
{
    /// <summary>
    /// A smart list (text list) of a cube's outline.
    /// </summary>
    public class EssSmartList : EssObject, IEssSmartList
    {
        #region Private Data

        // The keys under which the server lists the two labels beside a list's entries.
        private const string MissingKey    = @"#Missing";
        private const string OutOfRangeKey = @"#OutOfRange";

        private readonly EssCube                 _cube;
        private readonly string                  _name;
        private readonly List<EssSmartListEntry> _entries;

        #endregion

        #region Constructors

        /// <summary />
        internal EssSmartList( string name, string missingName, string outOfRangeName, IEnumerable<EssSmartListEntry> entries, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssSmartList)}.");

            _name          = name;
            _entries       = entries?.ToList() ?? new List<EssSmartListEntry>();
            MissingName    = missingName;
            OutOfRangeName = outOfRangeName;
        }

        #endregion

        #region IEssObject Members

        /// <inheritdoc />
        public override string Name => _name;

        /// <inheritdoc />
        public override EssType Type => EssType.SmartList;

        #endregion

        #region IEssSmartList Members

        /// <inheritdoc />
        public IEssCube Cube => _cube;

        /// <inheritdoc />
        public IReadOnlyList<EssSmartListEntry> Entries => _entries;

        /// <inheritdoc />
        public string MissingName { get; }

        /// <inheritdoc />
        public string OutOfRangeName { get; }

        /// <inheritdoc />
        public void Delete() => DeleteAsync().GetAwaiter().GetResult();

        /// <inheritdoc />
        public async Task DeleteAsync( CancellationToken cancellationToken = default )
        {
            try
            {
                var action = new JObject { [@"smartListDelete"] = new JObject { [@"smartListName"] = Name } };

                await _cube.EditSmartListsAsync(action, warningsAreFailures: false, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception e)
            {
                throw new Exception($@"Unable to delete smart list ""{Name}"". {e.Message}", e);
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Reads the smart lists of a cube from the server's listing, which names each list and gives its labels and
        /// entries as pairs in the order of its keys.
        /// </summary>
        /// <param name="listing">The server's listing: an object with a property for each smart list.</param>
        /// <param name="cube">The cube whose outline holds the lists.</param>
        internal static List<IEssSmartList> FromListing( JObject listing, EssCube cube )
        {
            var smartLists = new List<IEssSmartList>();

            foreach (var list in listing?.Properties() ?? Enumerable.Empty<JProperty>())
            {
                var pairs = list.Value[@"pairs"] as JObject;
                var keys  = (list.Value[@"keys"] as JArray)?.Select(key => key.Value<string>()) ?? pairs?.Properties().Select(pair => pair.Name) ?? Enumerable.Empty<string>();

                var entries = new List<EssSmartListEntry>();

                foreach (var key in keys)
                {
                    if (int.TryParse(key, out var id))
                        entries.Add(new EssSmartListEntry(id, pairs?.Value<string>(key)));
                }

                smartLists.Add(new EssSmartList(list.Name, pairs?.Value<string>(MissingKey), pairs?.Value<string>(OutOfRangeKey), entries, cube));
            }

            return smartLists;
        }

        #endregion

        /// <inheritdoc />
        public override string ToString() =>
            $"{nameof(EssSmartList)} {{ {nameof(Cube)} = {Cube.Name}, {nameof(Name)} = {Name}, {nameof(Entries)} = {Entries.Count} }}";
    }
}
