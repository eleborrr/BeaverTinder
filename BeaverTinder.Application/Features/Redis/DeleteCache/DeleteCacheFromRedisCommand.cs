using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;

namespace BeaverTinder.Application.Features.Redis.DeleteCache;

public record DeleteCacheFromRedisCommand(string DataKey): ICommand;