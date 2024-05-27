using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Clickhouse.Services.Abstraction;
using MassTransit;

namespace BeaverTinder.Clickhouse.Services;

public class LikesConsumer : IConsumer<NewLikeToday>
{
    private readonly IClickhouseService _clickhouseService;

    public LikesConsumer(IClickhouseService clickhouseService)
    {
        _clickhouseService = clickhouseService;
    }

    public async Task Consume(ConsumeContext<NewLikeToday> context)
    {
        await _clickhouseService.UpsertCurrentDayLikes(DateTime.Now);
    }
}