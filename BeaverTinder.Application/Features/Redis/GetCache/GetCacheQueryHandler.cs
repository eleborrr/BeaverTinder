using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using StackExchange.Redis;

namespace BeaverTinder.Application.Features.Redis.GetCache;

public class GetCacheQueryHandler: IQueryHandler<GetCacheQuery, Dictionary<string, string>>
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    public GetCacheQueryHandler(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
    }

    public Task<Result<Dictionary<string, string>>> Handle(GetCacheQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var db = _connectionMultiplexer.GetDatabase();
            var hashFields = db.HashGetAll(request.DataKey);

            var result = new Dictionary<string, string>();

            foreach (var hashField in hashFields)
                result[hashField.Name] = hashField.Value;
            
            return Task.FromResult(new Result<Dictionary<string,string>>(result,true));
        }
        catch (Exception e)
        {
            return Task.FromResult(new Result<Dictionary<string,string>>(null, false, e.Message));
        }
    }
}