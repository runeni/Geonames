using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Geonames.Domain;
using Xunit;
using FluentAssertions;
using Geonames.Models;
using Moq;
using Moq.Dapper;

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
            var dbConnectionMock = new Mock<IDbConnection>();
            dbConnectionMock
                .SetupDapperAsync(g => g.QueryAsync(It.IsAny<CommandDefinition>(),
                    It.IsAny<Func<Geoname, FeatureClassification, Country, Admin1CodesAscii, Admin2Codes, Geoname>>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(new List<Geoname>());
            var geonamesProvider = GetObject(dbConnectionMock.Object);
            
            var actual = await geonamesProvider.GetGeonames("my location");
                dbConnectionMock
                    .Verify(d => d.QueryAsync<Geoname, FeatureClassification, Country, Admin1CodesAscii, Admin2Codes, Geoname>(
                        It.Is<CommandDefinition>(command => command.CommandText.StartsWith("select")),
                        It.IsAny<Func<Geoname, FeatureClassification, Country, Admin1CodesAscii, Admin2Codes, Geoname>>(),
                        It.Is<string>(s => s.Contains("Id"))));
        }


        private static GeonamesProvider GetObject(IDbConnection connection)
        {
            return new GeonamesProvider(connection);
        }
    }
}