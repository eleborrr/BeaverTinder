using BeaverTinder.Application.Dto.Authentication.Register;
using BeaverTinder.Application.Dto.Vk;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;

namespace Application.OAuth.Register;

public record RegisterOAuthVkCommand(VkAuthDto UserDto) : ICommand<RegisterResponseDto>;