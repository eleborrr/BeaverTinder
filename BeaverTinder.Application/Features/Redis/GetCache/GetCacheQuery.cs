using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

namespace BeaverTinder.Application.Features.Redis.GetCache;

public record GetCacheQuery(string DataKey): IQuery<Dictionary<string, string>>;