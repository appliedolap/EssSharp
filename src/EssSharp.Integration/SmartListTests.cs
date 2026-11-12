using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests collection definition with <see cref="CollectionPriorityAttribute" /> for <see cref="TestCollectionOrderer" />
    /// </summary>
    [CollectionDefinition(nameof(SmartListTests), DisableParallelization = true), CollectionPriority(12)]
    public class SmartListTestsCollection : ICollectionFixture<CollectionFixture> { }

    /// <summary>
    /// Smart lists (text lists) against Essbase: a smart list created in Demo.Basic's outline is listed with its labels
    /// and entries, is written into the outline XML where a reader of that XML expects it, is refused under a name the
    /// outline already uses, and can be deleted. The tests create the smart list themselves and delete it at the end, so
    /// the outline is left as it was found.
    /// </summary>
    [Collection(nameof(SmartListTests)), Trait("type", "execute")]
    public class SmartListTests : IntegrationTestBase
    {
        /// <summary />
        /// <param name="output" />
        public SmartListTests( ITestOutputHelper output ) : base(output) { }

        private const string ApplicationName = @"Demo";
        private const string CubeName        = @"Basic";
        private const string SmartListName   = @"esssharp_confidence_level";
        private const string MissingName     = @"No Confidence";
        private const string OutOfRangeName  = @"Unknown Confidence";

        private static readonly (int Id, string Text)[] Entries = { (1, @"High"), (2, @"Moderate"), (3, @"Low") };

        [Fact(DisplayName = @"SmartListTests - 01 - Essbase_SmartList_IsListedWithItsLabelsAndEntries"), Priority(01)]
        public async Task Essbase_SmartList_IsListedWithItsLabelsAndEntries()
        {
            var cube = await GetCubeAsync();

            // Start from an outline without the list, so that what is listed is what this test created.
            await DeleteSmartListIfPresentAsync(cube);

            var created = await cube.CreateSmartListAsync(SmartListName, Entries.Select(entry => new EssSmartListEntry(entry.Id, entry.Text)), MissingName, OutOfRangeName, TestContext.Current.CancellationToken);

            Assert.Equal(SmartListName, created.Name);
            Assert.Equal(EssType.SmartList, created.Type);
            Assert.Same(cube, created.Cube);

            var listed = Assert.Single(await cube.GetSmartListsAsync(TestContext.Current.CancellationToken), candidate => candidate.Name == SmartListName);

            Assert.Equal(MissingName, listed.MissingName);
            Assert.Equal(OutOfRangeName, listed.OutOfRangeName);
            Assert.Equal(Entries, listed.Entries.Select(entry => (entry.Id, entry.Text)));

            // The list is found by name, whatever the case of the name asked for.
            var found = await cube.GetSmartListAsync(SmartListName.ToUpperInvariant(), TestContext.Current.CancellationToken);

            Assert.Equal(SmartListName, found.Name);
            Assert.Equal(Entries, found.Entries.Select(entry => (entry.Id, entry.Text)));
        }

        [Fact(DisplayName = @"SmartListTests - 02 - Essbase_SmartList_IsWrittenAfterTheDimensionsOfTheOutlineXml"), Priority(02)]
        public async Task Essbase_SmartList_IsWrittenAfterTheDimensionsOfTheOutlineXml()
        {
            var cube = await GetCubeAsync();

            await EnsureSmartListAsync(cube);

            using var outlineXml = await cube.ExportOutlineToXmlAsync(cancellationToken: TestContext.Current.CancellationToken);

            var application = XDocument.Load(outlineXml).Root;

            // A reader of the outline XML, such as an outline viewer, relies on this shape: the smart lists are not inside
            // the cube element, but are the last child of the application, after its dimensions.
            var smartLists = Assert.Single(application.Elements(@"smartLists"));

            Assert.Same(smartLists, application.Elements().Last());
            Assert.Empty(application.Element(@"cube").Descendants(@"smartLists"));
            Assert.NotEmpty(smartLists.ElementsBeforeSelf(@"Dimension"));

            // Each list is an enumeration: its name and missing label are attributes, its out-of-range name is an element,
            // and each entry is an element that gives the text as its name and the number as its value.
            var enumeration = Assert.Single(smartLists.Elements(@"enumeration"), candidate => (string)candidate.Attribute(@"name") == SmartListName);

            Assert.Equal(MissingName, (string)enumeration.Attribute(@"missingLabel"));
            Assert.Equal(OutOfRangeName, (string)enumeration.Element(@"outOfRangeName"));
            Assert.Equal(
                Entries.Select(entry => (entry.Text, entry.Id.ToString())),
                enumeration.Elements(@"enumEntry").Select(entry => ((string)entry.Attribute(@"name"), (string)entry.Attribute(@"value"))));
        }

        [Fact(DisplayName = @"SmartListTests - 03 - Essbase_SmartList_IsRefusedUnderANameTheOutlineAlreadyUses"), Priority(03)]
        public async Task Essbase_SmartList_IsRefusedUnderANameTheOutlineAlreadyUses()
        {
            var cube = await GetCubeAsync();

            await EnsureSmartListAsync(cube);

            var refused = await Assert.ThrowsAnyAsync<Exception>(() => cube.CreateSmartListAsync(SmartListName, new[] { new EssSmartListEntry(9, @"Other") }, @"Other Missing", @"Other Out Of Range", TestContext.Current.CancellationToken));

            TestContext.Current.TestOutputHelper?.WriteLine($@"Refused: {refused.Message}");

            Assert.Contains(@"already exists", refused.Message);

            // The list the outline already had is left as it was.
            var kept = await cube.GetSmartListAsync(SmartListName, TestContext.Current.CancellationToken);

            Assert.Equal(MissingName, kept.MissingName);
            Assert.Equal(Entries, kept.Entries.Select(entry => (entry.Id, entry.Text)));
        }

        [Fact(DisplayName = @"SmartListTests - 04 - Essbase_SmartList_CanBeDeleted"), Priority(04)]
        public async Task Essbase_SmartList_CanBeDeleted()
        {
            var cube = await GetCubeAsync();

            var smartList = await EnsureSmartListAsync(cube);

            await smartList.DeleteAsync(TestContext.Current.CancellationToken);

            Assert.DoesNotContain(await cube.GetSmartListsAsync(TestContext.Current.CancellationToken), candidate => candidate.Name == SmartListName);

            // A list the outline no longer has cannot be found, and deleting it again is refused.
            await Assert.ThrowsAnyAsync<Exception>(() => cube.GetSmartListAsync(SmartListName, TestContext.Current.CancellationToken));

            var refused = await Assert.ThrowsAnyAsync<Exception>(() => smartList.DeleteAsync(TestContext.Current.CancellationToken));

            TestContext.Current.TestOutputHelper?.WriteLine($@"Refused: {refused.Message}");

            Assert.Contains(@"does not exist", refused.Message);
        }

        #region Private Methods

        private async Task<IEssCube> GetCubeAsync() =>
            await GetEssServer().GetApplicationAsync(ApplicationName, TestContext.Current.CancellationToken).GetCubeAsync(CubeName, TestContext.Current.CancellationToken);

        /// <summary>
        /// Returns the test's smart list, creating it when the outline does not have it, so that each test can run alone.
        /// </summary>
        private static async Task<IEssSmartList> EnsureSmartListAsync( IEssCube cube )
        {
            var existing = (await cube.GetSmartListsAsync(TestContext.Current.CancellationToken)).FirstOrDefault(candidate => candidate.Name == SmartListName);

            return existing ?? await cube.CreateSmartListAsync(SmartListName, Entries.Select(entry => new EssSmartListEntry(entry.Id, entry.Text)), MissingName, OutOfRangeName, TestContext.Current.CancellationToken);
        }

        private static async Task DeleteSmartListIfPresentAsync( IEssCube cube )
        {
            var existing = (await cube.GetSmartListsAsync(TestContext.Current.CancellationToken)).FirstOrDefault(candidate => candidate.Name == SmartListName);

            if (existing != null)
                await existing.DeleteAsync(TestContext.Current.CancellationToken);
        }

        #endregion
    }
}
