using Geonames.Domain;
using Xunit;
using FluentAssertions;

namespace Geonames.Tests
{
    public class GeonameProviderTests
    {
        [Fact]
        public void TestGetTsQueryFormatted()
        {
            var searchString = "Uno Dos Tres";
            var formatted = GeonamesProvider.GetTsQueryFormatted(searchString);
            formatted.Should().Be("Uno:* <-> Dos:* <-> Tres:*");
        }
    }
}