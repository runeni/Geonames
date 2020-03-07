using FluentMigrator;
using Geonames.Extensions;

namespace Geonames.Migrations
{
    [Migration(20200302134800)]
    public class AddGeonamesTable : BaseMigration
    {
        public override void Up()
        {
            Create.Table("geonames")
                .WithColumn("id").AsInt64().PrimaryKey().Identity()
                .WithColumn("geoname_id").AsString(200).NotNullable()
                .WithColumn("name").AsString(200).NotNullable()
                .WithColumn("asciiname").AsString(200).Nullable()
                .WithColumn("alternatenames").AsString(10000).Nullable()
                .WithColumn("latitude").AsString(64).Nullable()
                .WithColumn("longitude").AsString(64).Nullable()
                .WithColumn("featureclass").AsString(1).Nullable()
                .WithColumn("featurecode").AsString(10).Nullable()
                .WithColumn("countrycode").AsString(2).Nullable()
                .WithColumn("cc2").AsString(200).Nullable()
                .WithColumn("admin1code").AsString(20).Nullable()
                .WithColumn("admin2code").AsString(80).Nullable()
                .WithColumn("admin3code").AsString(20).Nullable()
                .WithColumn("admin4code").AsString(20).Nullable()
                .WithColumn("population").AsInt64().Nullable()
                .WithColumn("elevation").AsInt32().Nullable()
                .WithColumn("dem").AsInt32().Nullable()
                .WithColumn("timezone").AsString(200).Nullable()
                .WithColumn("last_updated").AsString(255).NotNullable()
                .WithColumn("name_tsv").AsCustom("tsvector").Nullable()
                .WithTimeStamps();
        }

        public override void Down()
        {
            Execute.WithConnection(async (conn, tran) =>
            {
                var cmd = "DROP TABLE geonames;";
                var rowsAffected = ExecuteSql(conn, tran, cmd);
            });
        }
    }
}
