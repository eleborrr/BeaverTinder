using BeaverTinder.Clickhouse.Repositories.Abstraction;
using BeaverTinder.Clickhouse.Services.Abstraction;
using BeaverTinder.Clickhouse.Helpers;
using RabbitMQ.Client;

namespace BeaverTinder.Clickhouse.Services;

public class ClickhouseService : IClickhouseService
{
    private readonly IClickhouseRepository _clickhouseRepository;
    private readonly IModel _channel;
    public ClickhouseService(
        IClickhouseRepository clickhouseRepository,
        IModel channel)
    {
        _clickhouseRepository = clickhouseRepository;
        _channel = channel;
    }
    
    public async Task UpsertCurrentDayLikes(DateTime date)
    {
        await _clickhouseRepository.UpsertCurrentDayLikes(date);
    }

    public async Task<long> GetCurrentDayLikes(DateTime date)
    {
        var result = await _clickhouseRepository.GetCurrentDayLikes(date);
        return result.CountLikes;
    }

    public async Task<long> GetAllDaysLikes()
    {
        var todayViews = await _clickhouseRepository.GetCurrentDayLikes(DateTime.Now);
        if (AllDaysViewsHelper.AllDaysViewsWithoutToday is not null &&
            (DateTime.Now - AllDaysViewsHelper.LastDay!).Value.Hours < 24)
        {
            return AllDaysViewsHelper.AllDaysViewsWithoutToday.Value + todayViews.CountLikes;
        }

        var allDaysViews = await _clickhouseRepository.GetAllDaysLikes();
        AllDaysViewsHelper.AllDaysViewsWithoutToday = allDaysViews - todayViews?.CountLikes;
        AllDaysViewsHelper.LastDay = DateTime.Now;
        return allDaysViews;
    }

    public string ConnectUserToQueue()
    {
        var queueName = _channel.QueueDeclare(Guid.NewGuid().ToString(), false, false, true).QueueName;
        _channel.QueueBind(queueName, Shared.StaticValues.Clickhouse.ExchangeName, "");

        return queueName;
    }
}