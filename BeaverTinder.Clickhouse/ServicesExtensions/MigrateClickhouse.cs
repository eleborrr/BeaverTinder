using BeaverTinder.Clickhouse.Helpers;
using ClickHouse.Client.ADO;
using ClickHouse.Client.Utility;

namespace BeaverTinder.Clickhouse.ServicesExtensions;

public static class MigrateClickhouse
{
    public static async Task UpsertDbAndTable()
    {
        using var connection = new ClickHouseConnection(ConnectionStrings.Clickhouse.ConnectionString);
        // ExecuteScalarAsync is an async extension which creates command and executes it
        await connection.ExecuteScalarAsync($"CREATE DATABASE IF NOT EXISTS {ConnectionStrings.Clickhouse.DbName}");
        // ExecuteScalarAsync is an async extension which creates command and executes it
        await connection.ExecuteScalarAsync(
            $@"CREATE TABLE IF NOT EXISTS {ConnectionStrings.Clickhouse.DbName}.{ConnectionStrings.Clickhouse.TableName}
                    (
                     id UInt32  DEFAULT UUIDStringToNum(randomString(16)),
                     date_likes DateTime,
                     count_likes UInt64)
                    ENGINE = MergeTree()
                     PRIMARY KEY id 
                     ORDER BY id;");
    }
}