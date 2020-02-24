using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Geonames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Geonames.Controllers
{
    public class GeonamesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public GeonamesController(IConfiguration configuration, IDbConnection connection)
        {
            _configuration = configuration;
            _connection = connection;
        }

        // GET
        public async Task<IActionResult> Index(string searchString)
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
                    },
                    splitOn: "geonameId");
            }
            return View(geonames);
        }
    }
}