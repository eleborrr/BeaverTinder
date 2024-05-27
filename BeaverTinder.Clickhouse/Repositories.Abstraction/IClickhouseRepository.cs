using BeaverTinder.Clickhouse.Contracts;

namespace BeaverTinder.Clickhouse.Repositories.Abstraction;

public interface IClickhouseRepository
{
    public Task<LikeMadeDto> UpsertCurrentDayLikes(DateTime date);
    public Task<LikeMadeDto> GetCurrentDayLikes(DateTime date);
    public Task<long> GetAllDaysLikes();
}
