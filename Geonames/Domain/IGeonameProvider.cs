using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Geonames.Models;

namespace Geonames.Domain
{
    public interface IGeonamesProvider
    {
        Task<IEnumerable<Geoname>> GetGeonames(string searchString);
    }

    public class GeonamesProvider : IGeonamesProvider
    {
        private readonly IDbConnection _connection;

        public GeonamesProvider(IDbConnection connection)
        {
            _connection = connection;
        }
        
        public async Task<IEnumerable<Geoname>> GetGeonames(string searchString)
        {
            IEnumerable<Geoname> geonames = new List<Geoname>();
            if (!String.IsNullOrEmpty(searchString))
            {
                var parameters = new {SearchString = $"%{searchString}%"};
                var commandDefinition = new CommandDefinition(
                    @"select geo.*, feature.*, co.*, admin.*
                    from geonames geo
                      join featureclassifications feature on feature.classcode = geo.featureclass || '.' || geo.featurecode
                      join countries co on co.iso = geo.countrycode
                      join admin1codesascii admin on admin.identifier = geo.countrycode || '.' || geo.admin1code
                    where geo.name like @SearchString",
                    parameters
                );
                geonames = await _connection.QueryAsync<Geoname, FeatureClassification, Country, Admin1CodesAscii, Geoname>(commandDefinition,(geoname, feature, country, admincode) =>
                    {
                        geoname.FeatureClassification = feature;
                        geoname.Country = country;
                        geoname.Admin1CodesAscii = admincode;
                        return geoname;
                    });
            }

            return geonames;
        }
    }
}