using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using StackExchange.Redis;

namespace BeaverTinder.Application.Features.Redis.SaveCache;

public class SaveMetadataRedisCommandHandler: ICommandHandler<SaveMetadataRedisCommand>
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public SaveMetadataRedisCommandHandler(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public Task<Result> Handle(SaveMetadataRedisCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var db = _connectionMultiplexer.GetDatabase();

            var metadata = request.Data
                .Select(data => new HashEntry(data.Key, data.Value))
                .ToArray();
                        
            db.HashSet(request.Key, metadata);
            
            return Task.FromResult(new Result(true));
        }
        catch (Exception e)
        {
            return Task.FromResult(new Result(false, e.Message));
        }
    }
}