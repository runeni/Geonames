using FluentMigrator;

namespace Geonames.Migrations
{
    [Migration(20200403144000)]
    public class _AddTriggerToUpdateTsVectors : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE OR REPLACE FUNCTION update_name_tsv_column()
            RETURNS TRIGGER AS $$
            BEGIN
              NEW.name_tsv = to_tsvector(name);
              RETURN NEW;
            END;
            $$ language 'plpgsql';");

            Execute.Sql(
                @"CREATE TRIGGER update_geonames_name_tsv BEFORE UPDATE ON geonames FOR EACH ROW EXECUTE PROCEDURE update_name_tsv_column();");
        }

        public override void Down()
        {
            Execute.Sql("DROP TRIGGER update_geonames_name_tsv on geonames");
            Execute.Sql("DROP FUNCTION update_name_tsv_column");
        }
        
    }
}