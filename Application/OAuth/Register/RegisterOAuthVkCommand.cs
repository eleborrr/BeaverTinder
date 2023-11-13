using Contracts.Dto.Authentication.Register;
using Contracts.Dto.Vk;
using Services.Abstraction.Cqrs.Commands;

namespace Application.OAuth.Register;

public record RegisterOAuthVkCommand(VkAuthDto UserDto) : ICommand<RegisterResponseDto>;