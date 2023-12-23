using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;

namespace BeaverTinder.Application.Features.Redis.SaveCache;

public record SaveMetadataRedisCommand(string Key, Dictionary<string, string> Data) : ICommand;