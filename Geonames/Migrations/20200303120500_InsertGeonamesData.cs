using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Dapper;
using FluentMigrator;
using Npgsql;
using Npgsql;

namespace Geonames.Migrations
{
    [Migration(20200303120500)]
    public class InsertGeonamesData : Migration
    {
        public override void Up()
        {
            Execute.WithConnection((conn, tran) =>
            {
                using (var pgconn = new NpgsqlConnection(conn.ConnectionString))
                {
                    pgconn.Open();
                    var command =
                        @"copy public.geonames (geoname_id, name, asciiname, alternatenames, latitude, longitude, featureclass, featurecode, countrycode, cc2, admin1code,
                                                          admin2code, admin3code, admin4code, population, elevation, dem, timezone, last_updated)
                                    from stdin CSV DELIMITER E'\t' QUOTE E'\b' ESCAPE '\' NULL AS '' encoding 'UTF8'";
                    using (var writer = pgconn.BeginTextImport(command))
                    {
                        var blacklist = Enumerable.Range(573484, 577069).ToList();
                        string line;
                        int counter = 0;
                        var file = new StreamReader("Migrations/allCountries.txt");
                        while ((line = file.ReadLine()) != null)
                        {
                            writer.Write(line);
                            writer.Write("\n");

                            counter += 1;
                        }

                        Console.WriteLine(String.Format("Ending in {0}", counter));
                    }
                }
            });
        }

        public override void Down()
        {
            Execute.Sql("truncate table geonames;");
        }

    }
}