using System.IO;
using Dapper;
using FluentMigrator;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Geonames.Migrations
{
    [Migration(20200303120500)]
    public class InsertGeonamesData : BaseMigration
    {
        private readonly IConfiguration _configuration;

        public InsertGeonamesData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void Up()
        {
            Execute.WithConnection((conn, tran) =>
            {
                var cmd = new NpgsqlCommand();
                using (var pgconn = new NpgsqlConnection(conn.ConnectionString))
                {
                    pgconn.Open();
                    var command =
                        @"copy public.geonames (geoname_id, name, asciiname, alternatenames, latitude, longitude, featureclass, featurecode, countrycode, cc2, admin1code,
                                                          admin2code, admin3code, admin4code, population, elevation, dem, timezone, last_updated)
                                    from stdin CSV DELIMITER E'\t' QUOTE E'\b' ESCAPE '\' NULL AS '' encoding 'UTF8'";
                    using (var writer = pgconn.BeginTextImport(command))
                    {
                        string line;
                        var file = new StreamReader(_configuration.GetValue<string>("PathToAllCountriesFile"));
                        while ((line = file.ReadLine()) != null)
                        {
                            writer.Write(line);
                            writer.Write("\n");
                        }
                    }
                }
            });
        }

        public override void Down()
        {
            // Use this approach to get around timeout issues.
            Execute.WithConnection(async (conn, tran) =>
            {
                var rowsAffected = await ExecuteSqlAsync(conn, tran, "TRUNCATE TABLE geonames;");
            });
        }
    }
}