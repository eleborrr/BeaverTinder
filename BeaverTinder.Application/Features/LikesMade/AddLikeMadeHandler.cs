using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Shared.StaticValues;
using RabbitMQ.Client;
using MassTransit;

namespace BeaverTinder.Application.Features.LikesMade;

public class AddLikeMadeHandler : IQueryHandler<AddLikeMadeQuery, bool>
{
    private readonly IBus _bus;
    private readonly IModel _channel;

    public AddLikeMadeHandler(IBus bus, IModel channel)
    {
        _bus = bus;
        _channel = channel;
    }

    public async Task<Result<bool>> Handle(AddLikeMadeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            await _bus.Publish(new NewLikeToday(),cancellationToken);
        
            _channel.BasicPublish(
                exchange: Clickhouse.ExchangeName,
                routingKey:Clickhouse.RoutingKey,
                basicProperties:null
            );
        }
        catch (Exception)
        {
            return new Result<bool>(false, false);
        }

        return new Result<bool>(true, true);
    }
}