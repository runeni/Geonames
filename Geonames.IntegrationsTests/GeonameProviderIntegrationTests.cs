using System.Data;
using Xunit;

namespace Geonames.IntegrationsTests
{
    public class GeonameProviderIntegrationTests
    {
        [Fact]
        public void TestHandler()
        {
            var handler = new DatabaseFixture();
            var connection = handler.GetConnection();
            DataTable dt = connection.ExecuteSQL(
                @"insert into geonames(geoname_id, name, asciiname, alternatenames, latitude, longitude, featureclass, featurecode, countrycode, cc2, admin1code, admin2code, admin3code, admin4code, population, elevation, dem, timezone)
                 values (1, 'dreamplace', 'dreamplace', 'dreamplace', '0', '0', 'P', 'PPL', 'NO', null, 28, null, null, null, null, null, null, 'Europe/Oslo'); select * from countries;");
        }
    }
}