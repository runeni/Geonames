using FluentMigrator;

namespace Geonames.Migrations
{
    [Migration(20200304134000)]
    public class CreateBatchUpdater : BaseMigration
    {
        public override void Up()
        {
            Execute.WithConnection(async (conn, tran) =>
            {
                var cmd = @"CREATE OR REPLACE FUNCTION batch_update_name_tsv()
                          RETURNS void AS
                        $BODY$
                        DECLARE

                        id_val integer;

                        BEGIN

                          FOR id_val IN 0..120
                          LOOP

                          UPDATE geonames SET name_tsv = to_tsvector(name)
                            WHERE id > (100000 * id_val) AND id <= 100000 * (id_val + 1);

                          END LOOP;

                        END
                        $BODY$
                        LANGUAGE plpgsql;";
                var rowsAffected = ExecuteSql(conn, tran, cmd);
            });

        }

        public override void Down()
        {
            Execute.WithConnection((conn, tran) =>
            {
                var cmd = "DROP FUNCTION batch_update_name_tsv()";
                var rowsAffected = ExecuteSql(conn, tran, cmd);
            });
        }
        
    }
}