using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of <see cref="IEssGrid.DisplayUnknownMembers"/> and of comments in the data block, against a loopback
    /// stub that records each request. Essbase REST keeps text it does not know, and comments, only with the grid preferences'
    /// comment retention on: it returns such text typed as a comment, and refuses it otherwise. To display unknown members, an
    /// operation runs with comment retention on, and lists the members it sent that Essbase returned as comments.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class GridUnknownMemberTests
    {
        /// <summary>
        /// A grid with a member on each axis and a note at the end of its column header row.
        /// </summary>
        private static readonly string[][] GridWithANote =
        {
            new[] { "",     "Jan", "Note" },
            new[] { "Cola", "",    ""     }
        };

        /// <summary>
        /// The answer to <see cref="GridWithANote"/> with the note kept as a comment.
        /// </summary>
        private static readonly (HttpStatusCode, string) NoteKeptAsAComment =
            Answer(new[] { new[] { "", "Jan", "Note" }, new[] { "Cola", "100.0", "" } }, new[] { new[] { "7", "0", "3" }, new[] { "0", "2", "7" } });

        [Fact(DisplayName = "GridUnknownMemberTests - RefreshAsync_WithoutDisplayUnknownMembers_FailsOnTheRefusalWithoutKeepingComments")]
        public async Task RefreshAsync_WithoutDisplayUnknownMembers_FailsOnTheRefusalWithoutKeepingComments()
        {
            using var stub = Stub((HttpStatusCode.BadRequest, @"{""errorMessage"":""Input Grid contains unknown Members : Note""}"));

            var grid = await GridAsync(stub, GridWithANote, displayUnknownMembers: false);

            await Assert.ThrowsAnyAsync<System.Exception>(() => grid.RefreshAsync(TestContext.Current.CancellationToken));

            Assert.All(SessionPreferences(stub), preferences => Assert.False(KeepsComments(preferences)));
            Assert.Empty(grid.UnknownMembers);
        }

        [Fact(DisplayName = "GridUnknownMemberTests - RefreshAsync_WithDisplayUnknownMembers_KeepsCommentsForTheOperationOnly")]
        public async Task RefreshAsync_WithDisplayUnknownMembers_KeepsCommentsForTheOperationOnly()
        {
            using var stub = Stub(NoteKeptAsAComment);

            var grid = await GridAsync(stub, GridWithANote);

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            Assert.True(KeepsComments(Assert.Single(SessionPreferences(stub))));
            Assert.False(grid.Preferences.FormulaRetention.Comments);
        }

        [Fact(DisplayName = "GridUnknownMemberTests - RefreshAsync_WithDisplayUnknownMembers_SendsTheNoteAsAMemberAndListsItWhenItComesBackAComment")]
        public async Task RefreshAsync_WithDisplayUnknownMembers_SendsTheNoteAsAMemberAndListsItWhenItComesBackAComment()
        {
            using var stub = Stub(NoteKeptAsAComment);

            var grid = await GridAsync(stub, GridWithANote);

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (values, types) = Posts(stub).Single();

            Assert.Equal(new[] { "", "Jan", "Note", "Cola", "", "" }, values);
            Assert.Equal(new[] { "7", "0", "0", "0", "7", "7" }, types);

            Assert.Equal(new[] { "", "Jan", "Note", "Cola", "100.0", "" }, grid.Slice.Data.Ranges[0].Values);
            Assert.Equal(new[] { "Note" }, grid.UnknownMembers);
        }

        [Fact(DisplayName = "GridUnknownMemberTests - RefreshAsync_WithCommentsKept_SendsTextInTheDataBlockAsAComment")]
        public async Task RefreshAsync_WithCommentsKept_SendsTextInTheDataBlockAsAComment()
        {
            // A comment to the right of the data, in the data block.
            var values = new[]
            {
                new[] { "",     "Jan", ""          },
                new[] { "Cola", "",    "a comment" }
            };

            using var stub = Stub(Answer(new[] { new[] { "", "Jan", "" }, new[] { "Cola", "100.0", "a comment" } }, new[] { new[] { "7", "0", "7" }, new[] { "0", "2", "3" } }));

            var grid = await GridAsync(stub, values, displayUnknownMembers: false, keepComments: true);

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (sentValues, sentTypes) = Posts(stub).Single();

            Assert.Equal("a comment", sentValues[5]);
            Assert.Equal("3", sentTypes[5]);
            Assert.Equal("a comment", grid.Slice.Data.Ranges[0].Values[5]);
        }

        [Fact(DisplayName = "GridUnknownMemberTests - RefreshAsync_WithoutCommentsKept_SendsTextInTheDataBlockBlank")]
        public async Task RefreshAsync_WithoutCommentsKept_SendsTextInTheDataBlockBlank()
        {
            // Essbase refuses text in the data block when comments are not kept.
            var values = new[]
            {
                new[] { "",     "Jan", ""          },
                new[] { "Cola", "",    "a comment" }
            };

            using var stub = Stub(Answer(new[] { new[] { "", "Jan" }, new[] { "Cola", "100.0" } }, new[] { new[] { "7", "0" }, new[] { "0", "2" } }));

            var grid = await GridAsync(stub, values, displayUnknownMembers: false);

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (sentValues, sentTypes) = Posts(stub).Single();

            Assert.Equal("", sentValues[5]);
            Assert.Equal("7", sentTypes[5]);
        }

        [Fact(DisplayName = "GridUnknownMemberTests - RefreshAsync_WithDisplayUnknownMembers_DoesNotListAComment")]
        public async Task RefreshAsync_WithDisplayUnknownMembers_DoesNotListAComment()
        {
            // The comment in the data block is sent as a comment, not as a member, so it is not an unknown member.
            var values = new[]
            {
                new[] { "",     "Jan", "Note"      },
                new[] { "Cola", "",    "a comment" }
            };

            using var stub = Stub(Answer(new[] { new[] { "", "Jan", "Note" }, new[] { "Cola", "100.0", "a comment" } }, new[] { new[] { "7", "0", "3" }, new[] { "0", "2", "3" } }));

            var grid = await GridAsync(stub, values);

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            Assert.Equal("3", Posts(stub).Single().Types[5]);
            Assert.Equal(new[] { "Note" }, grid.UnknownMembers);
        }

        [Fact(DisplayName = "GridUnknownMemberTests - RefreshAsync_WhenEssbaseAnswersWithItsDefaultGrid_LeavesTheGridAsItIs")]
        public async Task RefreshAsync_WhenEssbaseAnswersWithItsDefaultGrid_LeavesTheGridAsItIs()
        {
            // A sheet of text holds no member Essbase knows, so Essbase answers with its default grid, which keeps none of it.
            var values = new[]
            {
                new[] { "Function",                   "Result"                                  },
                new[] { "@AddrOfRng(Titles.RowZ, 1)", "column F is hidden for testing purposes" }
            };

            using var stub = Stub(Answer(new[] { new[] { "Function", "Measures", "Product", "Market", "Scenario" }, new[] { "Year", "105522.0", "", "", "" } },
                                         new[] { new[] { "3", "0", "0", "0", "0" }, new[] { "0", "2", "7", "7", "7" } }));

            var grid = await GridAsync(stub, values);

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            Assert.Equal(values.SelectMany(row => row), grid.Slice.Data.Ranges[0].Values);
            Assert.Equal(values.SelectMany(row => row), grid.UnknownMembers);
        }

        [Fact(DisplayName = "GridUnknownMemberTests - ZoomAsync_WithDisplayUnknownMembers_ListsTheNoteWhereverTheZoomPutsIt")]
        public async Task ZoomAsync_WithDisplayUnknownMembers_ListsTheNoteWhereverTheZoomPutsIt()
        {
            using var stub = Stub(Answer(new[] { new[] { "", "Jan", "Feb", "Note" }, new[] { "Cola", "100.0", "110.0", "" } },
                                         new[] { new[] { "7", "0", "0", "3" }, new[] { "0", "2", "2", "7" } }));

            var grid = await GridAsync(stub, GridWithANote);

            await grid.ZoomAsync(EssGridZoomType.ZOOMIN, new List<EssGridSelection>() { new EssGridSelection(0, 1) }, TestContext.Current.CancellationToken);

            Assert.Equal("Note", grid.Slice.Data.Ranges[0].Values[3]);
            Assert.Equal(new[] { "Note" }, grid.UnknownMembers);
        }

        /// <summary>
        /// Returns a grid Essbase answers with, holding the given values and types, row by row.
        /// </summary>
        /// <param name="values">The grid's values.</param>
        /// <param name="types">The grid's types.</param>
        private static (HttpStatusCode, string) Answer( string[][] values, string[][] types )
        {
            var count = values.Length * values[0].Length;

            var grid = new
            {
                slice = new
                {
                    columns = values[0].Length,
                    rows    = values.Length,
                    data    = new
                    {
                        ranges = new[]
                        {
                            new
                            {
                                start    = 0,
                                end      = count - 1,
                                values   = values.SelectMany(row => row).ToArray(),
                                types    = types.SelectMany(row => row).ToArray(),
                                statuses = Enumerable.Repeat("0", count).ToArray()
                            }
                        }
                    }
                }
            };

            return (HttpStatusCode.OK, JsonSerializer.Serialize(grid));
        }

        /// <summary>
        /// Returns a stub that answers the application and cube requests, and the grid operations with the given answers, one
        /// for each, in order.
        /// </summary>
        /// <param name="answers">The answers to the grid operations.</param>
        private static LoopbackEssbaseStub Stub( params (HttpStatusCode, string)[] answers )
        {
            var next = new Queue<(HttpStatusCode, string)>(answers);

            return new LoopbackEssbaseStub()
            {
                ResponseProvider = requestLine => requestLine.Split(' ') switch
                {
                    [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample")                 => (HttpStatusCode.OK, @"{""name"":""Sample""}"),
                    [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample/databases/Basic") => (HttpStatusCode.OK, @"{""name"":""Basic""}"),
                    [ "POST", var path, .. ] when path.Split('?')[0].EndsWith("/applications/Sample/databases/Basic/grid") =>
                        next.Count > 0 ? next.Dequeue() : (HttpStatusCode.InternalServerError, @"{""errorMessage"":""The test sent more grids than it answers.""}"),
                    _ => null
                }
            };
        }

        /// <summary>
        /// Returns a grid of Sample.Basic on the given stub, holding the given values, row by row.
        /// </summary>
        /// <param name="stub">The stub.</param>
        /// <param name="values">The grid's values.</param>
        /// <param name="displayUnknownMembers">Whether the grid tolerates text that is not a member.</param>
        /// <param name="keepComments">Whether the grid's preferences keep comments.</param>
        private static async Task<IEssGrid> GridAsync( LoopbackEssbaseStub stub, string[][] values, bool displayUnknownMembers = true, bool keepComments = false )
        {
            var cube = await new EssServer(stub.BasePath, "admin", "password").GetApplicationAsync("Sample", TestContext.Current.CancellationToken).GetCubeAsync("Basic", TestContext.Current.CancellationToken);
            var grid = cube.GetGrid();

            grid.Preferences                           = new EssGridPreferences();
            grid.Preferences.FormulaRetention.Comments = keepComments;
            grid.DisplayUnknownMembers                 = displayUnknownMembers;

            grid.Slice = new EssGridSlice() { Columns = values[0].Length, Rows = values.Length };
            grid.Slice.Data.Ranges.Add(new EssGridRange()
            {
                Start  = 0,
                End    = values.Length * values[0].Length - 1,
                Values = values.SelectMany(row => row).ToList()
            });

            return grid;
        }

        /// <summary>
        /// Returns the values and types of each grid the grid operations posted, in order.
        /// </summary>
        /// <param name="stub">The stub.</param>
        private static List<(string[] Values, string[] Types)> Posts( LoopbackEssbaseStub stub ) =>
            stub.Requests.Where(request => request.StartsWith("POST ") && request.Contains("/databases/Basic/grid")).Select(request =>
            {
                using var body = JsonDocument.Parse(Body(request));

                var range = body.RootElement.GetProperty("grid").GetProperty("slice").GetProperty("data").GetProperty("ranges")[0];

                return (range.GetProperty("values").EnumerateArray().Select(value => value.GetString()).ToArray(),
                        range.GetProperty("types").EnumerateArray().Select(type => type.GetString()).ToArray());
            }).ToList();

        /// <summary>
        /// Returns the body of each request that set the session's grid preferences, in order.
        /// </summary>
        /// <param name="stub">The stub.</param>
        private static List<string> SessionPreferences( LoopbackEssbaseStub stub ) =>
            stub.Requests.Where(request => request.StartsWith("PUT ") && request.Contains("/preferences/grid")).Select(Body).ToList();

        /// <summary>
        /// Returns whether the given grid preferences keep comments.
        /// </summary>
        /// <param name="preferences">The body of a request that set the session's grid preferences.</param>
        private static bool KeepsComments( string preferences )
        {
            using var body = JsonDocument.Parse(preferences);

            return body.RootElement.TryGetProperty("formulaRetention", out var retention) &&
                   retention.TryGetProperty("comments", out var comments) &&
                   comments.GetBoolean();
        }

        /// <summary>
        /// Returns the body of the given recorded request.
        /// </summary>
        /// <param name="request">The request, as the stub recorded it.</param>
        private static string Body( string request ) => request.Substring(request.IndexOf("\r\n\r\n") + 4);
    }
}
