using FluentMigrator;
using FluentMigrator.Example.Migrations;

namespace Geonames.Migrations
{
    [Migration(20200302140500)]
    public class AddExamplesTables : Migration
    {
        public override void Up()
        {
            Create.Table("Examples")
                .WithIdColumn()
                .WithColumn("Body").AsString(4000).NotNullable()
                .WithTimeStamps()
                .WithColumn("User_id").AsInt32();
        }

        public override void Down()
        {
            Delete.Table("Examples");
        }
        
    }
}