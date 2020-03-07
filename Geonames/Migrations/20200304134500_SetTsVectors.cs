using System;
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
            Execute.WithConnection( async (conn, tran) =>
            {
                var cmd = "select batch_update_name_tsv();";
                var rowsAffected = await ExecuteSqlAsync(conn, tran, cmd);
            });

        }

        public override void Down()
        {
            // Use this approach to get around timeout issues.
            Execute.WithConnection((conn, tran) =>
            {
                var cmd = "UPDATE geonames SET name_tsv = null";
                var rowsAffected = ExecuteSql(conn, tran, cmd);
            });
        }
    }
}