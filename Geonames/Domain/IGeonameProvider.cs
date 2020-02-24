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
                    "select g.*, p.* from geonames g left join geonames p on p.featurecode = 'ADM1' and p.admin1code = g.admin1code and p.countrycode = g.countrycode where g.name = @SearchString",
                    parameters
                );
                geonames = await _connection.QueryAsync<Geoname, Geoname, Geoname>(commandDefinition,(geoname, parent) =>
                    {
                        geoname.Parent = parent;
                        return geoname;
                    });
            }

            return geonames;
        }
    }
}