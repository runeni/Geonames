using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Geonames.Models;

namespace Geonames.Domain
{
    public interface IDatabaseWrapper
    {
        Task<IEnumerable<TReturn>>
            QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(CommandDefinition command,
                Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id");
    }

    public class DatabaseWrapper : IDatabaseWrapper
    {
        private readonly IDbConnection _connection;

        public DatabaseWrapper(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TReturn>>
            QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(CommandDefinition command,
                Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id") =>
            await _connection.QueryAsync(command, map, splitOn);
    }
}