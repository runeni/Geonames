using Dapper;
using FluentMigrator;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Npgsql;

namespace Geonames.Migrations
{
    [Migration(20200304134500)]
    public class SetTsVectors : BaseMigration
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
                var rowsAffected = await ExecuteSqlAsync(conn, tran, cmd);
            });

            // Use this approach to get around timeout issues.
            Execute.WithConnection(async (conn, tran) =>
            {
                var cmd = "select batch_update_name_tsv()";
                var rowsAffected = await ExecuteSqlAsync(conn, tran, cmd);
            });
        }

        public override void Down()
        {
            // Use this approach to get around timeout issues.
            Execute.WithConnection(async (conn, tran) =>
            {
                var cmd = "UPDATE geonames SET name_tsv = null";
                var rowsAffected = await ExecuteSqlAsync(conn, tran, cmd);
            });

            Execute.WithConnection(async (conn, tran) =>
            {
                var cmd = "DROP FUNCTION batch_update_name_tsv()";
                var rowsAffected = await ExecuteSqlAsync(conn, tran, cmd);
            });
        }
    }
}