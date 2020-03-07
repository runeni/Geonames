using System.Data;
using System.Threading.Tasks;
using Dapper;
using FluentMigrator;
using Npgsql;

namespace Geonames.Migrations
{
    public abstract class BaseMigration : Migration
    {
        protected async Task<int> ExecuteSqlAsync(IDbConnection connection, IDbTransaction transaction, string command)
        {
            int rowsAffected;
            using (var pgconn = new NpgsqlConnection(connection.ConnectionString))
            {
                pgconn.Open();
                rowsAffected = await pgconn.ExecuteAsync(command);
            }
            return rowsAffected;
        }
 
        protected int ExecuteSql(IDbConnection connection, IDbTransaction transaction, string command)
        {
            int rowsAffected;
            using (var pgconn = new NpgsqlConnection(connection.ConnectionString))
            {
                pgconn.Open();
                rowsAffected = pgconn.Execute(command);
            }
            return rowsAffected;
        }
    }
}

