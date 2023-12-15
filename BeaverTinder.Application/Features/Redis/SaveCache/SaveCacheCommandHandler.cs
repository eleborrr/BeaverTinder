using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using Microsoft.Extensions.Caching.Distributed;

namespace BeaverTinder.Application.Features.Redis.SaveCache;

public class SaveCacheCommandHandler: ICommandHandler<SaveCacheCommand>
{
    private readonly IDistributedCache _cache;

    public SaveCacheCommandHandler(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<Result> Handle(SaveCacheCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _cache.SetStringAsync(request.Key, request.Data, token: cancellationToken);
            return new Result(true);
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
    }
}