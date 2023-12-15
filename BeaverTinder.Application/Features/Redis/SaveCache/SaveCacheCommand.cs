using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;

namespace BeaverTinder.Application.Features.Redis.SaveCache;

public record SaveCacheCommand(string Key, string Data) : ICommand;