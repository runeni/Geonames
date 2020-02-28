using System;
using Xunit;
using DynamixPostgreSQLHandler;

namespace Geonames.IntegrationsTests
{
    public class DatabaseFixture : IDisposable
    {
        private SQLHandler _connection;
        public DatabaseFixture()
        {
            var connectionString =
                "Server=localhost;Database=geonamesdb_test;User ID=geotest;Password=geotestaren;Port=5432;Pooling=true;";
            _connection = new SQLHandler(connectionString);
        }

        public SQLHandler GetConnection()
        {
            // _connection.ExecuteSQL(CreateTablesBuilder.GetSql());
            return _connection;
        }

        public void Dispose()
        {
            _connection.ExecuteSQL("");
        }
    }

    [CollectionDefinition("Database")]
    public class DatabaseCollectionFixture : ICollectionFixture<DatabaseFixture>
    {
    }
}