using BeaverTinder.Clickhouse.Contracts;

namespace BeaverTinder.Clickhouse.Services.Abstraction;

public interface IClickhouseService
{
    public Task UpsertCurrentDayLikes(DateTime date);
    public Task<long> GetCurrentDayLikes(DateTime date);
    public Task<long> GetAllDaysLikes();
}