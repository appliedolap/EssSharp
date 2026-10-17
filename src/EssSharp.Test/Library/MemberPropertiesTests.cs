using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// Client-side tests of a member's calculation properties against a loopback stub that scripts the server's responses.
    /// Essbase leaves a property out of a member's JSON when it has its default value.
    /// </summary>
    [Collection(nameof(LibraryCollection))]
    public class MemberPropertiesTests
    {
        [Fact(DisplayName = "MemberPropertiesTests - GetMemberAsync_GivesTheCalculationPropertiesEssbaseReturns")]
        public async Task GetMemberAsync_GivesTheCalculationPropertiesEssbaseReturns()
        {
            using var stub = Stub(@"{""name"":""Ending Inventory"",""consolidation"":""~"",""formula"":""\""Opening Inventory\"" + \""Additions\"";"",""twoPassCalc"":true,""timeBalance"":""LAST"",""timeBalanceSkipOption"":""MISSING"",""expense"":true}");

            var member = await (await Cube(stub)).GetMemberAsync("Ending Inventory", cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(("~", @"""Opening Inventory"" + ""Additions"";", true, "LAST", "MISSING", true),
                         (member.Consolidation, member.Formula, member.TwoPassCalc, member.TimeBalance, member.TimeBalanceSkipOption, member.Expense));
        }

        [Fact(DisplayName = "MemberPropertiesTests - GetMemberAsync_GivesTheDefaultsEssbaseLeavesOut")]
        public async Task GetMemberAsync_GivesTheDefaultsEssbaseLeavesOut()
        {
            using var stub = Stub(@"{""name"":""100""}");

            var member = await (await Cube(stub)).GetMemberAsync("100", cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(("+", (string)null, false, "NONE", "NONE", false),
                         (member.Consolidation, member.Formula, member.TwoPassCalc, member.TimeBalance, member.TimeBalanceSkipOption, member.Expense));
        }

        [Fact(DisplayName = "MemberPropertiesTests - GetMemberAsync_AsksForTheCalculationFieldsRequested")]
        public async Task GetMemberAsync_AsksForTheCalculationFieldsRequested()
        {
            using var stub = Stub(@"{""name"":""Margin %""}");

            await (await Cube(stub)).GetMemberAsync("Margin %", EssMemberFields.formula | EssMemberFields.twoPassCalc, TestContext.Current.CancellationToken);

            string request = stub.Requests.Single(candidate => candidate.StartsWith("GET ") && candidate.Contains("/outline/"));

            string[] fields = System.Uri.UnescapeDataString(request.Split(' ')[1]).Split('?')[1].Split('&')
                .Single(parameter => parameter.StartsWith("fields=")).Substring("fields=".Length).Split(',');

            Assert.Contains("formula", fields);
            Assert.Contains("twoPassCalc", fields);
        }

        [Theory(DisplayName = "MemberPropertiesTests - GetMemberAsync_GivesADimensionsTag")]
        [InlineData(@"{""name"":""Product"",""dimension"":true}", EssDimensionType.REGULAR)]
        [InlineData(@"{""name"":""Year"",""dimension"":true,""dimensionType"":""TIME""}", EssDimensionType.TIME)]
        [InlineData(@"{""name"":""100""}", EssDimensionType.Unknown)]
        public async Task GetMemberAsync_GivesADimensionsTag( string memberJson, EssDimensionType dimensionType )
        {
            using var stub = Stub(memberJson);

            var member = await (await Cube(stub)).GetMemberAsync("member", cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(dimensionType, member.DimensionType);
        }

        [Fact(DisplayName = "MemberPropertiesTests - GetMemberAsync_AsksForEachFieldByTheNameEssbaseKnows")]
        public async Task GetMemberAsync_AsksForEachFieldByTheNameEssbaseKnows()
        {
            using var stub = Stub(@"{""name"":""Qtr2""}");

            await (await Cube(stub)).GetMemberAsync("Qtr2", EssMemberFields.perviousSiblingCount | EssMemberFields.descentantsCount | EssMemberFields.dimensionSolveOrder |
                                                            EssMemberFields.dimensionStorageType | EssMemberFields.hasUniqueName, TestContext.Current.CancellationToken);

            string request = stub.Requests.Single(candidate => candidate.StartsWith("GET ") && candidate.Contains("/outline/"));
            string[] fields = System.Uri.UnescapeDataString(request.Split(' ')[1]).Split('?')[1].Split('&')
                .Single(parameter => parameter.StartsWith("fields=")).Substring("fields=".Length).Split(',');

            Assert.Equal(new[] { "dataStorageType", "descendantsCount", "dimSolveOrder", "dimStorageType", "memberHasUniqueName", "previousSiblingsCount" }, fields.OrderBy(field => field, System.StringComparer.Ordinal));
        }

        /// <summary>
        /// Returns a stub that answers the application and cube requests, and every outline member request with the given JSON.
        /// </summary>
        /// <param name="memberJson">The member's JSON.</param>
        private static LoopbackEssbaseStub Stub( string memberJson ) => new LoopbackEssbaseStub()
        {
            ResponseProvider = requestLine => requestLine.Split(' ') switch
            {
                [ "GET", var path, .. ] when path.EndsWith("/applications/Sample")                 => (HttpStatusCode.OK, @"{""name"":""Sample""}"),
                [ "GET", var path, .. ] when path.EndsWith("/applications/Sample/databases/Basic") => (HttpStatusCode.OK, @"{""name"":""Basic""}"),
                [ "GET", var path, .. ] when path.Contains("/outline/Sample/Basic/")               => (HttpStatusCode.OK, memberJson),
                _                                                                                  => null
            }
        };

        /// <summary>
        /// Returns Sample.Basic on the given stub.
        /// </summary>
        /// <param name="stub">The stub.</param>
        private static Task<IEssCube> Cube( LoopbackEssbaseStub stub ) =>
            new EssServer(stub.BasePath, "admin", "password").GetApplicationAsync("Sample", TestContext.Current.CancellationToken).GetCubeAsync("Basic", TestContext.Current.CancellationToken);
    }
}
