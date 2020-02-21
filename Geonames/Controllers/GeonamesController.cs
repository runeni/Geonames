using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        public IActionResult Index(string searchString)
        {
            var geonameList = new List<Geoname>();
            if (!String.IsNullOrEmpty(searchString))
            {
                var parameters= new { SearchString = searchString };
                var sql = "Select * From Geonames where Name = @SearchString";
                geonameList= _connection.Query<Geoname>(sql, parameters).ToList();
            }
            return View(geonameList);
        }
    }
}