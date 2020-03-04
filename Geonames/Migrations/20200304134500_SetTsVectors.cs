using System;
using FluentMigrator;

namespace Geonames.Migrations
{
    [Migration(20200304134500)]
    public class SetTsVectors : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE OR REPLACE FUNCTION batch_update_name_tsv()
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
                        LANGUAGE plpgsql;");
            Execute.Sql("select batch_update_name_tsv()");
        }

        public override void Down()
        {
            Execute.Sql("UPDATE geonames SET name_tsv = null");
            Execute.Sql("DROP FUNCTION batch_update_name_tsv()");
        }
    }
}