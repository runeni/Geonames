using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Geonames.Domain;
using Xunit;
using FluentAssertions;
using FluentAssertions.Common;
using Geonames.Models;
using Moq;
using Moq.Dapper;
using Npgsql;

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

        [Fact]
        public async Task TestGetGeonames()
        {
            var expected = new List<Geoname>()
            {
                new Geoname()
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
                    d.QueryAsync<Geoname, FeatureClassification, Country, Admin1CodesAscii, Admin2Codes, Geoname>(
                        It.Is<CommandDefinition>(command =>
                            command.CommandText.StartsWith("select geo.*") &&
                            command.Parameters.ToString().Equals("{ SearchString = my:* <-> location:* }")
                        ),
                        It.IsAny<Func<Geoname, FeatureClassification, Country, Admin1CodesAscii, Admin2Codes, Geoname
                        >>(),
                        It.Is<string>(s => s.Contains("Id"))));
            actual.Should().Equal(expected);
        }


        private static GeonamesProvider GetObject(IDatabaseWrapper connection)
        {
            return new GeonamesProvider(connection);
        }
    }
}