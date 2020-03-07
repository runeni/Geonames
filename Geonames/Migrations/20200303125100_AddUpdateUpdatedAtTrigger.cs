using Dapper;
using FluentMigrator;
using Npgsql;

namespace Geonames.Migrations
{
    [Migration(20200303125100)]
    public class AddUpdateUpdatedAtTrigger : BaseMigration
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
            // Use this approach to get around timeout issues.
            Execute.WithConnection((conn, tran) =>
            {
                var command = "DROP TRIGGER update_geonames_updated_at on geonames";
                var rowsAffected = ExecuteSql(conn, tran, command);
            });
            Execute.WithConnection((conn, tran) =>
            {
                var command = "DROP FUNCTION update_updated_at_column";
                var rowsAffected = ExecuteSql(conn, tran, command);
            });
        }
    }
}