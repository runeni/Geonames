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
                var parameters = new {SearchString = searchString};
                var commandDefinition = new CommandDefinition(
                    @"select geo.*, parent.*, feature.*
                    from geonames geo
                      join geonames parent on parent.featurecode = 'ADM1' and parent.admin1code = geo.admin1code and parent.countrycode = geo.countrycode
                      join featureclassifications feature on feature.classcode = geo.featureclass || '.' || geo.featurecode
                    where geo.name = @SearchString",
                    parameters
                );
                geonames = await _connection.QueryAsync<Geoname, Geoname, FeatureClassification, Geoname>(commandDefinition,(geoname, parent, feature) =>
                    {
                        geoname.Parent = parent;
                        geoname.FeatureClassification = feature;
                        return geoname;
                    });
            }

            return geonames;
        }
    }
}