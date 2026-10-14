using System;

using EssSharp.Integration.Setup;

using Xunit;

namespace EssSharp.Integration
{
    /// <summary>
    /// Tests for how the harness reads the Essbase version from an image reference. They need no server.
    /// </summary>
    public class EssbaseContainerProfileTests
    {
        [Theory]
        [InlineData("appliedolap/essbase:21.7-latest", "21.7")]
        [InlineData("appliedolap/essbase:21.8.2-dev", "21.8.2")]
        [InlineData("appliedolap/essbase:26", "26.0")]
        [InlineData("localhost:5000/essbase:26.1-latest", "26.1")]
        [InlineData("appliedolap/essbase:26.1-latest@sha256:ab12cd34ef56", "26.1")]
        public void ParseVersion_ReadsTheVersionFromTheTag( string image, string expected )
        {
            Assert.Equal(Version.Parse(expected), EssbaseContainerProfile.ParseVersion(image));
        }

        [Theory]
        [InlineData("appliedolap/essbase")]
        [InlineData("appliedolap/essbase:latest")]
        [InlineData("appliedolap/essbase@sha256:12ab34cd56ef")]
        [InlineData("localhost:5000/essbase")]
        [InlineData("")]
        [InlineData(null)]
        public void ParseVersion_ReturnsNullWhenThereIsNoVersionTag( string image )
        {
            Assert.Null(EssbaseContainerProfile.ParseVersion(image));
        }
    }
}
