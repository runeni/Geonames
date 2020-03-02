using FluentMigrator;
using FluentMigrator.Example.Migrations;

namespace Geonames.Migrations
{
    [Migration(20200302134800)]
    public class AddGeonamesTable : Migration
    {
        public override void Up()
        {
            Create.Table("Notes")
                .WithIdColumn()
                .WithColumn("Body").AsString(4000).NotNullable()
                .WithTimeStamps()
                .WithColumn("User_id").AsInt32();
        }

        public override void Down()
        {
            Delete.Table("Notes");
        }
    }
}
