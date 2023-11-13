using MediatR;
using Services.Abstraction.Cqrs.Commands;

namespace Application.OAuth.AddUserToVk;

public record AddUserToVkCommand(string PlatformUserId, string VkUserId) : ICommand<Unit>;