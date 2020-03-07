using FluentMigrator;

namespace Geonames.Migrations
{
    [Migration(20200304144000)]
    public class AddTriggerToUpdateTsVectors : BaseMigration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE OR REPLACE FUNCTION update_name_tsv_column()
            RETURNS TRIGGER AS $$
            BEGIN
              NEW.name_tsv = to_tsvector(NEW.name);
              RETURN NEW;
            END;
            $$ language 'plpgsql';");

            
            Execute.WithConnection( async (conn, tran) =>
            {
                var cmd = @"CREATE TRIGGER update_geonames_name_tsv BEFORE UPDATE ON geonames
                            FOR EACH ROW EXECUTE PROCEDURE update_name_tsv_column();";
                var rowsAffected = await ExecuteSqlAsync(conn, tran, cmd);
            });
        }

        public override void Down()
        {
            Execute.Sql("DROP TRIGGER update_geonames_name_tsv on geonames");
            Execute.Sql("DROP FUNCTION update_name_tsv_column");
        }
    }
}