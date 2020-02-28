using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private readonly IDatabaseWrapper _connection;

        public GeonamesProvider(IDatabaseWrapper connection)
        {
            _connection = connection;
        }
        
        public async Task<IEnumerable<Geoname>> GetGeonames(string searchString)
        {
            IEnumerable<Geoname> geonames = new List<Geoname>();
            if (!String.IsNullOrEmpty(searchString))
            {
                var parameters = new {SearchString = GetTsQueryFormatted(searchString)};
                var commandDefinition = new CommandDefinition(
                    @"select geo.*, feature.*, co.*, admin.*, admin2.*
                    from geonames geo
                      join featureclassifications feature on feature.classcode = geo.featureclass || '.' || geo.featurecode
                      join countries co on co.iso = geo.countrycode
                      join admin1codesascii admin on admin.identifier = geo.countrycode || '.' || geo.admin1code
                      left join admin2codes admin2 on admin2.identifier = geo.countrycode || '.' || geo.admin1code || '.' || geo.admin2code and geo.countrycode = 'NO'
                    where geo.name_tsv @@ to_tsquery(@SearchString)",
                    parameters
                );
                geonames =
                    await _connection.QueryAsync<Geoname, FeatureClassification, Country, Admin1CodesAscii, Admin2Codes, Geoname>(commandDefinition,(geoname, feature, country, admincode, admin2code) =>
                    {
                        geoname.FeatureClassification = feature;
                        geoname.Country = country;
                        geoname.Admin1CodesAscii = admincode;
                        geoname.Admin2Codes = admin2code;
                        return geoname;
                    });
            }

            return geonames;
        }

        public static string GetTsQueryFormatted(string searchString)
        {
            var list = searchString.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(x => x + ":*");
            var str = string.Join(" <-> ", list);
            return str;
        }
    }
}