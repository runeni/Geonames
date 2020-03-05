using System.IO;
using FluentMigrator;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Geonames.Migrations
{
    [Migration(20200305151500)]
    public class InsertFeatureClassificationsData : BaseMigration
    {
        private readonly IConfiguration _configuration;

        public InsertFeatureClassificationsData(IConfiguration configuration)
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
                        @"copy public.featureclassifications (class_code, content, lang, description)
                                    from stdin CSV DELIMITER E'\t' QUOTE E'\b' ESCAPE '\' NULL AS '' encoding 'UTF8'";
                    using (var writer = pgconn.BeginTextImport(command))
                    {
                        string line;
                        var file = new StreamReader(_configuration.GetValue<string>("PathToFeatureCodesFile"));
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
            Execute.WithConnection(async (conn, tran) =>
            {
                var rowsAffected = await ExecuteSqlAsync(conn, tran, "TRUNCATE TABLE featureclassifications;");
            });
        }

    }
}