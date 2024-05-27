using BeaverTinder.Clickhouse.Contracts;
using BeaverTinder.Clickhouse.Repositories.Abstraction;
using ClickHouse.Client.ADO;
using ClickHouse.Client.Utility;
using BeaverTinder.Clickhouse.Helpers;

namespace BeaverTinder.Clickhouse.Repositories;

public class ClickhouseRepository : IClickhouseRepository
{
    private string _table = $"{ConnectionStrings.Clickhouse.DbName}.{ConnectionStrings.Clickhouse.TableName}";

    public async Task<LikeMadeDto> UpsertCurrentDayLikes(DateTime date)
    {
        var roundDate = date.Date;
        var currentDay = await GetCurrentDayLikes(roundDate);
        using var connection = new ClickHouseConnection(ConnectionStrings.Clickhouse.ConnectionString);
        
        await connection.ExecuteScalarAsync(
            $"ALTER TABLE {_table} UPDATE count_likes = count_likes + 1 WHERE id = {currentDay.Id}");
        return await GetCurrentDayLikes(roundDate);
    }

    public async Task<LikeMadeDto> GetCurrentDayLikes(DateTime date)
    {
        using var connection = new ClickHouseConnection(ConnectionStrings.Clickhouse.ConnectionString);
        // ExecuteScalarAsync is an async extension which creates command and executes it
        var roundDate = date.Date;
        var formattedDate = roundDate.ToString("yyyy-MM-dd HH:mm:ss");
        var curDay = await GetCurrDay(connection, roundDate);
        if (curDay is null)
        {
            await connection.ExecuteScalarAsync(
                $"INSERT INTO {_table} (date_likes, count_likes) VALUES\n ('{formattedDate}', 0)");
            return (await GetCurrDay(connection, roundDate))!;
        }
        
        return curDay;
    }

    private async Task<LikeMadeDto?> GetCurrDay(ClickHouseConnection connection, DateTime date)
    {
        var formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
        var sql = $"SELECT * FROM {_table} where date_likes = '{formattedDate}';";
        var reader = 
            await connection.ExecuteReaderAsync(sql);
        var item = new LikeMadeDto();
        if (reader.Read())
        {
            item.Id = uint.Parse(reader[0].ToString()!);
            item.Date = DateTime.Parse(reader[1].ToString()!);
            item.CountLikes = long.Parse(reader[2].ToString()!);
        }
        else
        {
            return null;
        }

        return item;
    }

    public async Task<long> GetAllDaysLikes()
    {
        using var connection = new ClickHouseConnection(ConnectionStrings.Clickhouse.ConnectionString);
        var reader = 
            await connection.ExecuteReaderAsync(
                $"SELECT sum(count_likes) FROM {_table};");
        var count = 0L;
        if (reader.Read())
        {
            count = long.Parse(reader[0].ToString()!);
        }

        return count;
    }
}