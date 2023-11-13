using MediatR;
using Services.Abstraction.Cqrs.Commands;

namespace Application.OAth.AddUserToVk;

public record AddUserToVkCommand(string PlatformUserId, string VkUserId) : ICommand<Unit>;