using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using MediatR;

namespace Application.OAuth.AddUserToVk;

public record AddUserToVkCommand(string PlatformUserId, string VkUserId) : ICommand<Unit>;