using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />
    /// </summary>
    [CollectionDefinition(nameof(GridUnknownMemberTests), DisableParallelization = true), CollectionPriority(7)]
    public class GridUnknownMemberTestsCollection : ICollectionFixture<CollectionFixture> { }

    /// <summary>
    /// <see cref="IEssGrid.DisplayUnknownMembers"/> against Essbase: a grid that holds text that is not a member of the cube,
    /// which Essbase REST refuses unless the grid preferences keep comments, is retrieved with comments kept, keeps that text
    /// in its cells, and lists it, as the Essbase Java API's DisplayUnknownMembers option has Essbase do. A sheet of nothing
    /// but such text is left as it is rather than replaced by Essbase's default grid. The tests only read Sample.Basic.
    /// </summary>
    [Collection(nameof(GridUnknownMemberTests)), Trait("type", "execute")]
    public class GridUnknownMemberTests : IntegrationTestBase
    {
        /// <summary />
        /// <param name="output" />
        public GridUnknownMemberTests( ITestOutputHelper output ) : base(output) { }

        /// <summary>
        /// A complete Sample.Basic grid, as Essbase lays one out, with notes that are not members in the empty corner, at the
        /// end of its column header row, and below it, on three rows. One note holds a comma, as Essbase's refusal joins its
        /// names with one.
        /// </summary>
        private static readonly string[][] GridWithNotes =
        {
            new[] { "a corner note", "Measures", "Scenario", ""                    },
            new[] { "",              "",         "Year",     "a note, with comma"  },
            new[] { "Market",        "Product",  "",         ""                    },
            new[] { "a note below",  "",         "",         ""                    }
        };

        /// <summary>
        /// The notes in <see cref="GridWithNotes"/>, in the order the grid holds them.
        /// </summary>
        private static readonly string[] Notes = { "a corner note", "a note, with comma", "a note below" };

        [Fact(DisplayName = @"GridUnknownMemberTests - 01 - Essbase_RefreshWithNotes_RetrievesTheGridAndKeepsTheNotes"), Priority(01)]
        public async Task Essbase_RefreshWithNotes_RetrievesTheGridAndKeepsTheNotes()
        {
            var grid = await GridAsync(GridWithNotes, displayUnknownMembers: true);

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var values = grid.Slice.Data.Ranges[0].Values;

            Assert.Equal(4, grid.Slice.Rows);
            Assert.Equal(4, grid.Slice.Columns);
            Assert.True(double.TryParse(values[2 * 4 + 2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out _),
                        $@"The data cell holds ""{values[2 * 4 + 2]}"", not a number.");

            Assert.Equal("a corner note", values[0]);
            Assert.Equal("a note, with comma", values[1 * 4 + 3]);
            Assert.Equal("a note below", values[3 * 4]);
            Assert.Equal(Notes, grid.UnknownMembers);
        }

        [Fact(DisplayName = @"GridUnknownMemberTests - 02 - Essbase_RefreshOfOnlyUnknownText_LeavesTheGridAsItIs"), Priority(02)]
        public async Task Essbase_RefreshOfOnlyUnknownText_LeavesTheGridAsItIs()
        {
            var sheet = new[]
            {
                new[] { "Function",                   "Result"                                  },
                new[] { "@AddrOfRng(Titles.RowZ, 1)", "column F is hidden for testing purposes" }
            };

            var grid = await GridAsync(sheet, displayUnknownMembers: true);

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            Assert.Equal(sheet.SelectMany(row => row), grid.Slice.Data.Ranges[0].Values);
            Assert.Equal(sheet.SelectMany(row => row), grid.UnknownMembers);
        }

        [Fact(DisplayName = @"GridUnknownMemberTests - 03 - Essbase_RefreshWithACommentBesideTheData_KeepsTheCommentWithoutListingIt"), Priority(03)]
        public async Task Essbase_RefreshWithACommentBesideTheData_KeepsTheCommentWithoutListingIt()
        {
            // The comment lies in the data block, to the right of the data, where text is a comment rather than a member.
            var values = new[]
            {
                new[] { "",       "Measures", "Scenario", ""                          },
                new[] { "",       "",         "Year",     ""                          },
                new[] { "Market", "Product",  "",         "a comment beside the data" }
            };

            var grid = await GridAsync(values, displayUnknownMembers: true);

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            Assert.Equal("a comment beside the data", grid.Slice.Data.Ranges[0].Values[2 * 4 + 3]);
            Assert.Empty(grid.UnknownMembers);
        }

        [Fact(DisplayName = @"GridUnknownMemberTests - 04 - Essbase_ZoomInWithNotes_KeepsAndListsTheNotesAboveTheZoomedRows"), Priority(04)]
        public async Task Essbase_ZoomInWithNotes_KeepsAndListsTheNotesAboveTheZoomedRows()
        {
            // Essbase lays out the rows of the zoomed member again and drops the note on its own row below them, so only the
            // notes it keeps are listed.
            var grid = await GridAsync(GridWithNotes, displayUnknownMembers: true);

            await grid.ZoomAsync(EssGridZoomType.ZOOMIN, new List<EssGridSelection>() { new EssGridSelection(2, 1) }, TestContext.Current.CancellationToken);

            var values = grid.Slice.Data.Ranges[0].Values;

            Assert.True(grid.Slice.Rows > 4, $"The zoom returned {grid.Slice.Rows} rows.");
            Assert.Contains("a corner note", values);
            Assert.Contains("a note, with comma", values);
            Assert.DoesNotContain("a note below", values);
            Assert.Equal(new[] { "a corner note", "a note, with comma" }, grid.UnknownMembers);
        }

        [Fact(DisplayName = @"GridUnknownMemberTests - 05 - Essbase_RefreshWithNotesWithoutDisplayUnknownMembers_Fails"), Priority(05)]
        public async Task Essbase_RefreshWithNotesWithoutDisplayUnknownMembers_Fails()
        {
            var grid = await GridAsync(GridWithNotes, displayUnknownMembers: false);

            var exception = await Assert.ThrowsAnyAsync<Exception>(() => grid.RefreshAsync(TestContext.Current.CancellationToken));

            Assert.Contains("unknown Members", exception.Message);
            Assert.Empty(grid.UnknownMembers);
        }

        [Fact(DisplayName = @"GridUnknownMemberTests - 06 - Essbase_RefreshUnderAnotherAliasTable_TakesTheGridItReturned"), Priority(06)]
        public async Task Essbase_RefreshUnderAnotherAliasTable_TakesTheGridItReturned()
        {
            // Under GermanNames, Essbase returns every member of this grid under another name than the one it was sent.
            // A numeric member name is sent with a leading apostrophe, as a client writes one that a sheet holds as text.
            var values = new[]
            {
                new[] { "",    "Jan" },
                new[] { "'100", ""    }
            };

            var grid = await GridAsync(values, displayUnknownMembers: true);

            grid.Alias = "GermanNames";

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            Assert.Contains("Cola Getränke", grid.Slice.Data.Ranges[0].Values);
            Assert.Contains("Januar", grid.Slice.Data.Ranges[0].Values);
            Assert.Empty(grid.UnknownMembers);
        }

        [Fact(DisplayName = @"GridUnknownMemberTests - 07 - Essbase_RefreshOfTheDefaultGridUnderAnotherAliasTable_TakesTheGridItReturned"), Priority(07)]
        public async Task Essbase_RefreshOfTheDefaultGridUnderAnotherAliasTable_TakesTheGridItReturned()
        {
            // Essbase's default grid under GermanNames, retrieved under the default table, comes back as the default grid under
            // the default table's names.
            var values = new[]
            {
                new[] { "",     "Umsätze", "Produkt", "Markt", "Szenario" },
                new[] { "Jahr", "",        "",        "",      ""         }
            };

            var grid = await GridAsync(values, displayUnknownMembers: true);

            grid.Alias = "Default";

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            Assert.Contains("Product", grid.Slice.Data.Ranges[0].Values);
            Assert.DoesNotContain("Produkt", grid.Slice.Data.Ranges[0].Values);
            Assert.Empty(grid.UnknownMembers);
        }

        /// <summary>
        /// Returns a grid of Sample.Basic holding the given values, row by row.
        /// </summary>
        /// <param name="values">The grid's values.</param>
        /// <param name="displayUnknownMembers">Whether the grid tolerates text that is not a member.</param>
        private async Task<IEssGrid> GridAsync( string[][] values, bool displayUnknownMembers )
        {
            var cube = await GetEssServer()
                .GetApplicationAsync("Sample", TestContext.Current.CancellationToken)
                .GetCubeAsync("Basic", TestContext.Current.CancellationToken);

            var grid = cube.GetGrid();

            grid.DisplayUnknownMembers = displayUnknownMembers;
            grid.Slice = new EssGridSlice() { Columns = values[0].Length, Rows = values.Length };
            grid.Slice.Data.Ranges.Add(new EssGridRange()
            {
                Start  = 0,
                End    = values.Length * values[0].Length - 1,
                Values = values.SelectMany(row => row).ToList()
            });

            return grid;
        }
    }
}
