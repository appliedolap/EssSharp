using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of the data changes a submit tracks, against a loopback stub that answers each grid operation in
    /// turn. With <see cref="EssGridPreferences.TrackDataChanges"/> on, a submit refreshes the grid first, and lists each data
    /// cell whose value the submit changed with the member of every one of the grid's dimensions. Essbase places each
    /// dimension in the grid it answers with: a row dimension in a column, a column dimension in a row, and a dimension on
    /// neither axis in the point of view, which a grid may show in a row of its own above the column headers. A change lists
    /// the row dimensions from left to right, then the column dimensions from top to bottom, and then the point of view.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class GridDataChangeTests
    {
        /// <summary>
        /// The types of the cells of <see cref="PointOfViewRowGrid"/>.
        /// </summary>
        private static readonly string[][] PointOfViewRowTypes =
        {
            new[] { "7", "0", "0", "0" },
            new[] { "7", "0", "0", "7" },
            new[] { "0", "2", "2", "7" },
            new[] { "0", "2", "2", "7" }
        };

        /// <summary>
        /// The dimensions of <see cref="PointOfViewRowGrid"/>, as Essbase places them: Year in the second row, Product in the
        /// first column, and the rest in the point of view.
        /// </summary>
        private static readonly object[] PointOfViewRowDimensions =
        {
            Dimension("Year",     row: 1),
            Dimension("Measures", pov: "Sales"),
            Dimension("Product",  column: 0),
            Dimension("Market",   pov: "New York"),
            Dimension("Scenario", pov: "Budget")
        };

        [Fact(DisplayName = "GridDataChangeTests - SubmitAsync_WithThePointOfViewInARowAboveTheColumnHeaders_TracksTheChangedCellsMembers")]
        public async Task SubmitAsync_WithThePointOfViewInARowAboveTheColumnHeaders_TracksTheChangedCellsMembers()
        {
            using var stub = Stub(Answer(PointOfViewRowGrid("640.0"), PointOfViewRowTypes, PointOfViewRowDimensions),
                                  Answer(PointOfViewRowGrid("641.0"), PointOfViewRowTypes, PointOfViewRowDimensions));

            var grid = await GridAsync(stub, PointOfViewRowGrid("641"));

            await grid.SubmitAsync(TestContext.Current.CancellationToken);

            var change = Assert.Single(grid.DataChanges.DataChanges);

            Assert.Equal(640.0, change.OldValue);
            Assert.Equal(641.0, change.NewValue);
            Assert.Equal(new[] { "Product=Cola#3", "Year=Jan#1", "Measures=Sales#2", "Market=New York#4", "Scenario=Budget#5" }, DataPoints(change));
            Assert.All(change.DataPoints, dataPoint => Assert.Equal(string.Empty, dataPoint.Alias));
        }

        [Fact(DisplayName = "GridDataChangeTests - SubmitAsync_WithMembersNotRepeated_TracksTheMembersAboveAndToTheLeft")]
        public async Task SubmitAsync_WithMembersNotRepeated_TracksTheMembersAboveAndToTheLeft()
        {
            // Two row dimensions and two column dimensions, with East and Actual shown once for the cells they span.
            string[][] Cells( string dietColaFebruary ) => new[]
            {
                new[] { "",     "",          "Actual", ""               },
                new[] { "",     "",          "Jan",    "Feb"            },
                new[] { "East", "Cola",      "1.0",    "2.0"            },
                new[] { "",     "Diet Cola", "3.0",    dietColaFebruary }
            };

            string[][] types =
            {
                new[] { "7", "7", "0", "7" },
                new[] { "7", "7", "0", "0" },
                new[] { "0", "0", "2", "2" },
                new[] { "7", "0", "2", "2" }
            };

            object[] dimensions =
            {
                Dimension("Year",     row: 1),
                Dimension("Measures", pov: "Sales"),
                Dimension("Product",  column: 1),
                Dimension("Market",   column: 0),
                Dimension("Scenario", row: 0)
            };

            using var stub = Stub(Answer(Cells("4.0"), types, dimensions), Answer(Cells("5.0"), types, dimensions));

            var grid = await GridAsync(stub, Cells("5"));

            await grid.SubmitAsync(TestContext.Current.CancellationToken);

            var change = Assert.Single(grid.DataChanges.DataChanges);

            Assert.Equal(new[] { "Market=East#4", "Product=Diet Cola#3", "Scenario=Actual#5", "Year=Feb#1", "Measures=Sales#2" }, DataPoints(change));
        }

        [Fact(DisplayName = "GridDataChangeTests - SubmitAsync_WithoutAliases_TracksTheMembersByName")]
        public async Task SubmitAsync_WithoutAliases_TracksTheMembersByName()
        {
            using var stub = Stub(Answer(PointOfViewRowGrid("640.0"), PointOfViewRowTypes, PointOfViewRowDimensions),
                                  Answer(PointOfViewRowGrid("641.0"), PointOfViewRowTypes, PointOfViewRowDimensions));

            var grid = await GridAsync(stub, PointOfViewRowGrid("641"));

            grid.UseAliases = false;

            await grid.SubmitAsync(TestContext.Current.CancellationToken);

            var change = Assert.Single(grid.DataChanges.DataChanges);

            Assert.Equal(new[] { "Product=Cola#3", "Year=Jan#1", "Measures=Sales#2", "Market=New York#4", "Scenario=Budget#5" }, DataPoints(change));
            Assert.All(change.DataPoints, dataPoint => Assert.Null(dataPoint.Alias));
        }

        /// <summary>
        /// Returns the cells of a grid with its point of view in a row above the column headers, as Essbase answers it: Budget,
        /// New York, and Sales above January and February, over Cola and Diet Cola.
        /// </summary>
        /// <param name="colaJanuary">Cola's January value.</param>
        private static string[][] PointOfViewRowGrid( string colaJanuary ) => new[]
        {
            new[] { "",          "Budget",    "New York", "Sales" },
            new[] { "",          "Jan",       "Feb",      ""      },
            new[] { "Cola",      colaJanuary, "610.0",    ""      },
            new[] { "Diet Cola", "",          "",         ""      }
        };

        /// <summary>
        /// Returns each of a change's data points as its dimension, member, and dimension number.
        /// </summary>
        /// <param name="change">The change.</param>
        private static string[] DataPoints( EssDataChange change ) =>
            change.DataPoints.Select(dataPoint => $"{dataPoint.DimensionName}={dataPoint.Member}#{dataPoint.DimensionNumber}").ToArray();

        /// <summary>
        /// Returns a grid dimension as Essbase answers it: in the given row or column, or in the point of view.
        /// </summary>
        /// <param name="name">The dimension's name.</param>
        /// <param name="row">The row of a column dimension.</param>
        /// <param name="column">The column of a row dimension.</param>
        /// <param name="pov">The point of view member of a dimension on neither axis.</param>
        private static object Dimension( string name, int row = -1, int column = -1, string pov = "" ) =>
            new { name, row, column, pov, hidden = false, expanded = false };

        /// <summary>
        /// Returns a grid Essbase answers with: the given cells and their types, row by row, and its dimensions. An empty data
        /// cell has the missing status, as Essbase gives it.
        /// </summary>
        /// <param name="values">The grid's values.</param>
        /// <param name="types">The grid's types.</param>
        /// <param name="dimensions">The grid's dimensions.</param>
        private static string Answer( string[][] values, string[][] types, object[] dimensions ) => JsonSerializer.Serialize(new
        {
            alias = "Default",
            dimensions,
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
                            start  = 0,
                            end    = values.Length * values[0].Length - 1,
                            values   = values.SelectMany(row => row).ToArray(),
                            types    = types.SelectMany(row => row).ToArray(),
                            statuses = values.SelectMany(row => row).Zip(types.SelectMany(row => row), (value, type) => type == "2" && value == "" ? "8194" : "0").ToArray()
                        }
                    }
                }
            }
        });

        /// <summary>
        /// Returns a stub that answers the application, cube, and dimension requests, and the grid operations with the given
        /// grids in turn.
        /// </summary>
        /// <param name="answers">The grid each operation is answered with, in order.</param>
        private static LoopbackEssbaseStub Stub( params string[] answers )
        {
            var queue = new Queue<string>(answers);

            return new LoopbackEssbaseStub()
            {
                ResponseProvider = requestLine => requestLine.Split(' ') switch
                {
                    [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample")                 => (HttpStatusCode.OK, @"{""name"":""Sample""}"),
                    [ "GET",  var path, .. ] when path.EndsWith("/applications/Sample/databases/Basic") => (HttpStatusCode.OK, @"{""name"":""Basic""}"),
                    [ "GET",  var path, .. ] when path.Split('?')[0].EndsWith("/applications/Sample/databases/Basic/dimensions") =>
                        (HttpStatusCode.OK, @"{""items"":[{""name"":""Year""},{""name"":""Measures""},{""name"":""Product""},{""name"":""Market""},{""name"":""Scenario""}]}"),
                    [ "POST", var path, .. ] when path.Split('?')[0].EndsWith("/applications/Sample/databases/Basic/grid") =>
                        (HttpStatusCode.OK, queue.Dequeue()),
                    _ => null
                }
            };
        }

        /// <summary>
        /// Returns a grid of Sample.Basic on the given stub, tracking data changes, holding the given values row by row.
        /// </summary>
        /// <param name="stub">The stub.</param>
        /// <param name="values">The grid's values.</param>
        private static async Task<IEssGrid> GridAsync( LoopbackEssbaseStub stub, string[][] values )
        {
            var cube = await new EssServer(stub.BasePath, "admin", "password").GetApplicationAsync("Sample", TestContext.Current.CancellationToken).GetCubeAsync("Basic", TestContext.Current.CancellationToken);
            var grid = cube.GetGrid();

            grid.Preferences = new EssGridPreferences() { TrackDataChanges = true };

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
