using System.Collections.Generic;
using System.Linq;

using EssSharp.Model;

using Newtonsoft.Json.Linq;

using Xunit;

namespace EssSharp.Test.Contract
{
    public class SliceSerializationTests
    {
        [Fact]
        public void SliceSerializesRowsBeforeRangeData()
        {
            var slice = new Slice(
                rows: 2,
                dirtyCells: new List<int> { 1 },
                dirtyTexts: new List<int> { 2 },
                columns: 3,
                data: new Data());

            var propertyNames = JObject.Parse(slice.ToJson())
                .Properties()
                .Select(property => property.Name);

            Assert.Equal(
                new[] { "rows", "dirtyCells", "dirtyTexts", "columns", "data" },
                propertyNames);
        }
    }
}
