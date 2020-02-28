using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using FluentAssertions;
using Geonames.Domain;
using Geonames.Models;
using Moq;
using Xunit;

namespace Geonames.Tests
{
    public class GeonameProviderTests
    {
        private static GeonamesProvider GetObject(IDatabaseWrapper connection)
        {
            return new GeonamesProvider(connection);
        }

        [Fact]
        public async Task TestGetGeonames()
        {
            var expected = new List<Geoname>
            {
                new Geoname
                {
                    Id = 0,
                    GeonameId = 1,
                    Name = "My fav place"
                }
            };
            var dbConnectionMock = new Mock<IDatabaseWrapper>();
            dbConnectionMock
                .Setup(g => g.QueryAsync(It.IsAny<CommandDefinition>(),
                    It.IsAny<Func<Geoname, FeatureClassification, Country, Admin1CodesAscii, Admin2Codes, Geoname>>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(expected);

            var geonamesProvider = GetObject(dbConnectionMock.Object);

            var actual = await geonamesProvider.GetGeonames("my location");
            dbConnectionMock
                .Verify(d =>
                    d.QueryAsync(
                        It.Is<CommandDefinition>(command =>
                            command.CommandText.StartsWith("select geo.*") &&
                            command.Parameters.ToString().Equals("{ SearchString = my:* <-> location:* }")
                        ),
                        It.IsAny<Func<Geoname, FeatureClassification, Country, Admin1CodesAscii, Admin2Codes, Geoname
                        >>(),
                        It.Is<string>(s => s.Contains("Id"))));
            actual.Should().Equal(expected);
        }

        [Theory]
        [InlineData("Uno", "Uno:*")]
        [InlineData("Uno Dos", "Uno:* <-> Dos:*")]
        [InlineData("Uno Dos Tres", "Uno:* <-> Dos:* <-> Tres:*")]
        public void TestGetTsQueryFormatted(string searchString, string expected)
        {
            var formatted = GeonamesProvider.GetTsQueryFormatted(searchString);
            formatted.Should().Be(expected);
        }
    }
}