using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;

namespace BeaverTinder.Application.Features.Redis.UpdateCache;

public record UpdateCacheCommand(string DataKey, string Value): ICommand;