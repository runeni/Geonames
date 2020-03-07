using System.IO;
using FluentMigrator;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Geonames.Migrations
{
    [Migration(20200305143000)]
    public class InsertCountriesData : BaseMigration
    {
        private readonly IConfiguration _configuration;

        public InsertCountriesData(IConfiguration configuration)
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
                        @"copy public.countries (iso, iso3, iso_numeric, fips, name, capital, area, population, continent, tld, currency_code, currency_name, phone, postal_code_format,
                                                 postal_code_regex, languages, geoname_id, neighbours, equivalent_fips_code)
                                    from stdin CSV DELIMITER E'\t' QUOTE E'\b' ESCAPE '\' NULL AS '' encoding 'UTF8'";
                    using (var writer = pgconn.BeginTextImport(command))
                    {
                        string line;
                        var file = new StreamReader(_configuration.GetValue<string>("PathToCountryInfoFile"));
                        while ((line = file.ReadLine()) != null)
                        {
                            if (line.StartsWith("#")) continue;
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
            Execute.WithConnection((conn, tran) =>
            {
                var rowsAffected = ExecuteSql(conn, tran, "TRUNCATE TABLE countries;");
            });
        }
        
    }
}