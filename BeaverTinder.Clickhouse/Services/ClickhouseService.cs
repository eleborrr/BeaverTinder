using BeaverTinder.Clickhouse.Repositories.Abstraction;
using BeaverTinder.Clickhouse.Services.Abstraction;
using BeaverTinder.Clickhouse.Helpers;

namespace BeaverTinder.Clickhouse.Services;

public class ClickhouseService : IClickhouseService
{
    private readonly IClickhouseRepository _clickhouseRepository;
    public ClickhouseService(IClickhouseRepository clickhouseRepository)
    {
        _clickhouseRepository = clickhouseRepository;
    }
    
    public async Task UpsertCurrentDayLikes(DateTime date)
    {
        await _clickhouseRepository.UpsertCurrentDayLikes(date);
    }

    public async Task<long> GetCurrentDayLikes(DateTime date)
    {
        var result = await _clickhouseRepository.GetCurrentDayLikes(date);
        return result?.CountLikes ?? 0;
    }

    public async Task<long> GetAllDaysLikes()
    {
        var todayViews = await _clickhouseRepository.GetCurrentDayLikes(DateTime.Now);
        if (AllDaysViewsHelper.AllDaysViewsWithoutToday is not null &&
            (DateTime.Now - AllDaysViewsHelper.LastDay!).Value.Hours < 24)
        {
            return todayViews is not null
                ? AllDaysViewsHelper.AllDaysViewsWithoutToday.Value + todayViews.CountLikes
                : AllDaysViewsHelper.AllDaysViewsWithoutToday.Value;
        }

        var allDaysViews = await _clickhouseRepository.GetAllDaysLikes();
        AllDaysViewsHelper.AllDaysViewsWithoutToday = allDaysViews - todayViews?.CountLikes;
        AllDaysViewsHelper.LastDay = DateTime.Now;
        return allDaysViews;
    }
}