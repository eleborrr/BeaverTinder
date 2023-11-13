using Contracts.Dto.Authentication.Register;
using Contracts.Dto.Vk;
using Services.Abstraction.Cqrs.Commands;

namespace Application.OAth.Register;

public record RegisterOAuthVkCommand(VkAuthDto UserDto) : ICommand<RegisterResponseDto>;