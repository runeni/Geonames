using FluentMigrator;

namespace Geonames.Migrations
{
    [Migration(20200303125100)]
    public class AddUpdateUpdatedAtTrigger : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE OR REPLACE FUNCTION update_updated_at_column()
            RETURNS TRIGGER AS $$
            BEGIN
              NEW.updated_at = now();
              RETURN NEW;
            END;
            $$ language 'plpgsql';");

            Execute.Sql(
                @"CREATE TRIGGER update_geonames_updated_at BEFORE UPDATE ON geonames FOR EACH ROW EXECUTE PROCEDURE update_updated_at_column();");
        }

        public override void Down()
        {
            Execute.Sql("DROP TRIGGER update_geonames_updated_at on geonames");
            Execute.Sql("DROP FUNCTION update_updated_at_column");
        }
    }
}