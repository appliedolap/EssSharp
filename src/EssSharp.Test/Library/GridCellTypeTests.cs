using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of the types a grid's cells are sent to Essbase with, against a loopback stub that records the grid
    /// each operation posts. Essbase reads each cell as the type it is sent, so the client types a cell by its value: text is
    /// a member, a number or a missing or no-access label is data, and an empty cell is blank. A caller's type settles a
    /// numeric member name and a blank data cell. A data cell has a member to its left and a member above it (#72).
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class GridCellTypeTests
    {
        /// <summary>
        /// A grid Essbase answers with, for the operations whose answer the tests do not read.
        /// </summary>
        private const string EmptyGrid =
            @"{""slice"":{""columns"":1,""rows"":1,""data"":{""ranges"":[{""start"":0,""end"":0,""values"":[""""],""types"":[""7""]}]}}}";

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_WithARowOfNumbersBetweenTheHeadersAndTheMembers_SendsTheNumbersBlank")]
        public async Task RefreshAsync_WithARowOfNumbersBetweenTheHeadersAndTheMembers_SendsTheNumbersBlank()
        {
            // A template row of zeros under the month headers, above the first product, as Dodeca's Planning by Attribute has.
            using var stub = Stub();

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",          "Jan", "Feb" },
                new[] { "",          "0",   "0"   },
                new[] { "Cola",      "",    ""    },
                new[] { "Diet Cola", "",    ""    }
            });

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (values, types, _) = Posted(stub);

            Assert.Equal(new[] { "7", "0", "0", "7", "7", "7", "0", "7", "7", "0", "7", "7" }, types);
            Assert.Equal(new[] { "", "Jan", "Feb", "", "", "", "Cola", "", "", "Diet Cola", "", "" }, values);
        }

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_WithData_SendsEveryDataCellBlank")]
        public async Task RefreshAsync_WithData_SendsEveryDataCellBlank()
        {
            using var stub = Stub();

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",     "Jan",      "Feb" },
                new[] { "Cola", "1,710.5",  "1666" },
                new[] { "Root", "#Missing", "#No Access" }
            });

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (values, types, _) = Posted(stub);

            // "1,710.5" is not a number in the invariant culture, so it is sent as a member, as Essbase would read it.
            Assert.Equal(new[] { "7", "0", "0", "0", "0", "7", "0", "7", "7" }, types);
            Assert.Equal(new[] { "", "Jan", "Feb", "Cola", "1,710.5", "", "Root", "", "" }, values);
        }

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_WithAQuotedNumericMemberName_SendsItAsAMember")]
        public async Task RefreshAsync_WithAQuotedNumericMemberName_SendsItAsAMember()
        {
            using var stub = Stub();

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",     "Jan" },
                new[] { "'100", ""    }
            });

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (values, types, _) = Posted(stub);

            Assert.Equal(new[] { "7", "0", "0", "7" }, types);
            Assert.Equal("'100", values[2]);
        }

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_WithANumericMemberNameTheCallerTypesAsAMember_SendsItAsAMember")]
        public async Task RefreshAsync_WithANumericMemberNameTheCallerTypesAsAMember_SendsItAsAMember()
        {
            using var stub = Stub();

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",    "Jan" },
                new[] { "100", ""    }
            }, new[]
            {
                new[] { "7", "0" },
                new[] { "0", "7" }
            });

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (values, types, _) = Posted(stub);

            Assert.Equal(new[] { "7", "0", "0", "7" }, types);
            Assert.Equal("100", values[2]);
        }

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_WithANumberAmongTheColumnHeaderMembers_SendsItAsAMember")]
        public async Task RefreshAsync_WithANumberAmongTheColumnHeaderMembers_SendsItAsAMember()
        {
            // Product 100 entered as a number beside the members of its header row, as Dodeca's Income Statement Variance by Product has.
            using var stub = Stub();

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",      "100", "'100-10" },
                new[] { "",      "Jan", "Jan"     },
                new[] { "Sales", "",    ""        }
            });

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (values, types, _) = Posted(stub);

            Assert.Equal(new[] { "7", "0", "0", "7", "0", "0", "0", "7", "7" }, types);
            Assert.Equal("100", values[1]);
        }

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_WithANumberAmongTheRowHeaderMembers_SendsItAsAMember")]
        public async Task RefreshAsync_WithANumberAmongTheRowHeaderMembers_SendsItAsAMember()
        {
            using var stub = Stub();

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",        "Jan" },
                new[] { "'100-10", ""    },
                new[] { "100",     ""    }
            });

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (_, types, _) = Posted(stub);

            Assert.Equal(new[] { "7", "0", "0", "7", "0", "7" }, types);
        }

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_WithAnUnquotedNumericMemberNameAndNoType_SendsItAsData")]
        public async Task RefreshAsync_WithAnUnquotedNumericMemberNameAndNoType_SendsItAsData()
        {
            // A string alone cannot tell the member 100 from the number 100; the caller quotes the name or types the cell.
            using var stub = Stub();

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",    "Jan" },
                new[] { "100", ""    }
            });

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (_, types, _) = Posted(stub);

            Assert.Equal(new[] { "7", "0", "7", "7" }, types);
        }

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_WithALabelThatIsNotAMember_SendsItAsAMember")]
        public async Task RefreshAsync_WithALabelThatIsNotAMember_SendsItAsAMember()
        {
            // Text is a member, as the Essbase Java API reads it; Essbase reports an unknown one.
            using var stub = Stub();

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",      "Jan" },
                new[] { "Notes", ""    },
                new[] { "Cola",  ""    }
            });

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (_, types, _) = Posted(stub);

            Assert.Equal(new[] { "7", "0", "0", "7", "0", "7" }, types);
        }

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_WithTextBesideTheData_SendsItBlankAsAComment")]
        public async Task RefreshAsync_WithTextBesideTheData_SendsItBlankAsAComment()
        {
            // A key string to the right of a template's still-empty data, beyond the column members; Essbase REST keeps no comments.
            using var stub = Stub();

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",          "",         "Jan", "Feb", ""          },
                new[] { "Cola",      "New York", "",    "",    "Key=Cola;" },
                new[] { "Diet Cola", "New York", "",    "",    "Key=Diet;" }
            });

            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (values, types, _) = Posted(stub);

            Assert.Equal(new[] { "7", "7", "0", "0", "7", "0", "0", "7", "7", "7", "0", "0", "7", "7", "7" }, types);
            Assert.Equal(new[] { "", "", "Jan", "Feb", "", "Cola", "New York", "", "", "", "Diet Cola", "New York", "", "", "" }, values);
        }

        [Fact(DisplayName = "GridCellTypeTests - SubmitAsync_SendsTheDataBlockAndNeverWritesANoAccessCellOrANumberOutsideIt")]
        public async Task SubmitAsync_SendsTheDataBlockAndNeverWritesANoAccessCellOrANumberOutsideIt()
        {
            using var stub = Stub();

            var grid = await GridAsync(stub, Submitted);

            await grid.SubmitAsync(TestContext.Current.CancellationToken);

            var (values, types, dirtyCells) = Posted(stub);

            // The blank Feb cell of Cola sits in the data block, so it is sent as missing; the zeros have no member to their left.
            Assert.Equal(new[] { "7", "0", "0", "0", "2", "2", "0", "2", "7", "7", "7", "7" }, types);
            Assert.Equal(new[] { "", "Jan", "Feb", "Cola", "5", "", "Diet Cola", "", "", "", "", "" }, values);
            Assert.Equal(new[] { 4, 5, 7 }, dirtyCells);
        }

        [Fact(DisplayName = "GridCellTypeTests - SubmitAsync_WithoutSendingBlanksAsMissing_SendsTheMissingLabelAsMissing")]
        public async Task SubmitAsync_WithoutSendingBlanksAsMissing_SendsTheMissingLabelAsMissing()
        {
            using var stub = Stub();

            var grid = await GridAsync(stub, Submitted, sendBlanksAsMissing: false);

            await grid.SubmitAsync(TestContext.Current.CancellationToken);

            var (_, types, dirtyCells) = Posted(stub);

            // A blank cell is left alone, but a cell that says #Missing is written as missing.
            Assert.Equal(new[] { "7", "0", "0", "0", "2", "7", "0", "2", "7", "7", "7", "7" }, types);
            Assert.Equal(new[] { 4, 7 }, dirtyCells);
        }

        [Fact(DisplayName = "GridCellTypeTests - SubmitAsync_WithABlankCellTheCallerTypesAsData_SendsItAsMissing")]
        public async Task SubmitAsync_WithABlankCellTheCallerTypesAsData_SendsItAsMissing()
        {
            // Essbase types a missing value it returns as data; a blank cell typed so is a data cell, wherever the numbers are.
            using var stub = Stub();

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",     "Jan" },
                new[] { "Cola", ""    }
            }, new[]
            {
                new[] { "7", "0" },
                new[] { "0", "2" }
            });

            await grid.SubmitAsync(TestContext.Current.CancellationToken);

            var (_, types, dirtyCells) = Posted(stub);

            Assert.Equal(new[] { "7", "0", "0", "2" }, types);
            Assert.Equal(new[] { 3 }, dirtyCells);
        }

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_OnAGridEssbaseReturned_SendsItsCellsTyped")]
        public async Task RefreshAsync_OnAGridEssbaseReturned_SendsItsCellsTyped()
        {
            // The first refresh returns Product 100 by name, typed as a member, beside its Jan sales.
            var answers = new Queue<string>(new[]
            {
                @"{""slice"":{""columns"":2,""rows"":2,""data"":{""ranges"":[{""start"":0,""end"":3," +
                @"""values"":["""",""Jan"",""100"",""1710.0""],""types"":[""7"",""0"",""0"",""2""],""statuses"":[""0"",""0"",""0"",""0""]}]}}}",
                EmptyGrid
            });

            using var stub = Stub(() => answers.Dequeue());

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",     "Jan" },
                new[] { "'100", ""    }
            });

            await grid.RefreshAsync(TestContext.Current.CancellationToken);
            await grid.RefreshAsync(TestContext.Current.CancellationToken);

            var (values, types, _) = Posted(stub, 1);

            Assert.Equal(new[] { "7", "0", "0", "7" }, types);
            Assert.Equal(new[] { "", "Jan", "100", "" }, values);
        }

        [Fact(DisplayName = "GridCellTypeTests - RefreshAsync_WhenEssbaseRejectsTheGrid_LeavesTheGridsSliceAsItWas")]
        public async Task RefreshAsync_WhenEssbaseRejectsTheGrid_LeavesTheGridsSliceAsItWas()
        {
            using var stub = Stub(() => null);

            var grid = await GridAsync(stub, new[]
            {
                new[] { "",     "Jan" },
                new[] { "Cola", "5"   }
            });

            await Assert.ThrowsAnyAsync<System.Exception>(() => grid.RefreshAsync(TestContext.Current.CancellationToken));

            Assert.Equal(new[] { "", "Jan", "Cola", "5" }, grid.Slice.Data.Ranges[0].Values);
        }

        /// <summary>
        /// A grid to submit: Cola's Jan value, a blank in its Feb cell, Diet Cola's #Missing and #No Access, and a row of zeros
        /// with no member.
        /// </summary>
        private static string[][] Submitted => new[]
        {
            new[] { "",          "Jan",      "Feb"        },
            new[] { "Cola",      "5",        ""           },
            new[] { "Diet Cola", "#Missing", "#No Access" },
            new[] { "",          "0",        "0"          }
        };

        /// <summary>
        /// Returns a stub that answers the application and cube requests, and each grid operation with the given answer, or
        /// with a rejection when the answer is <see langword="null"/>.
        /// </summary>
        /// <param name="answer">The grid each operation is answered with; by default, a one-cell grid.</param>
        private static LoopbackEssbaseStub Stub( System.Func<string> answer = null ) => new LoopbackEssbaseStub()
        {
            ResponseProvider = requestLine => requestLine.Split(' ') switch
            {
                [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample")                      => (HttpStatusCode.OK, @"{""name"":""Sample""}"),
                [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample/databases/Basic")      => (HttpStatusCode.OK, @"{""name"":""Basic""}"),
                [ "POST", var path, .. ] when path.Split('?')[0].EndsWith("/applications/Sample/databases/Basic/grid") =>
                    (answer ?? (() => EmptyGrid))() is { } grid
                        ? (HttpStatusCode.OK, grid)
                        : (HttpStatusCode.BadRequest, @"{""errorMessage"":""Input Grid contains unknown Members : Cola""}"),
                _ => null
            }
        };

        /// <summary>
        /// Returns a grid of Sample.Basic on the given stub, holding the given values and, if given, types, row by row.
        /// </summary>
        /// <param name="stub">The stub.</param>
        /// <param name="values">The grid's values.</param>
        /// <param name="types">The caller's types, if any.</param>
        /// <param name="sendBlanksAsMissing">Whether a submit sends a blank data cell as missing.</param>
        private static async Task<IEssGrid> GridAsync( LoopbackEssbaseStub stub, string[][] values, string[][] types = null, bool sendBlanksAsMissing = true )
        {
            var cube = await new EssServer(stub.BasePath, "admin", "password").GetApplicationAsync("Sample", TestContext.Current.CancellationToken).GetCubeAsync("Basic", TestContext.Current.CancellationToken);
            var grid = cube.GetGrid();

            grid.Preferences = new EssGridPreferences() { SendBlanksAsMissing = sendBlanksAsMissing };

            var range = new EssGridRange()
            {
                Start  = 0,
                End    = values.Length * values[0].Length - 1,
                Values = values.SelectMany(row => row).ToList(),
                Types  = types?.SelectMany(row => row).ToList()
            };

            grid.Slice = new EssGridSlice() { Columns = values[0].Length, Rows = values.Length };
            grid.Slice.Data.Ranges.Add(range);

            return grid;
        }

        /// <summary>
        /// Returns the values, types, and dirty cells of the grid a grid operation posted.
        /// </summary>
        /// <param name="stub">The stub.</param>
        /// <param name="operation">Which grid operation, counting from zero.</param>
        private static (string[] Values, string[] Types, int[] DirtyCells) Posted( LoopbackEssbaseStub stub, int operation = 0 )
        {
            var request = stub.Requests.Where(request => request.StartsWith("POST ") && request.Contains("/databases/Basic/grid")).ElementAt(operation);

            using var body = JsonDocument.Parse(request.Substring(request.IndexOf("\r\n\r\n") + 4));

            var slice = body.RootElement.GetProperty("grid").GetProperty("slice");
            var range = slice.GetProperty("data").GetProperty("ranges")[0];

            var dirtyCells = slice.TryGetProperty("dirtyCells", out var cells) ? cells.EnumerateArray().Select(cell => cell.GetInt32()).ToArray() : new int[0];

            return (range.GetProperty("values").EnumerateArray().Select(value => value.GetString()).ToArray(),
                    range.GetProperty("types").EnumerateArray().Select(type => type.GetString()).ToArray(),
                    dirtyCells);
        }
    }
}
