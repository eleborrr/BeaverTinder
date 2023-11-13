using Contracts.Dto.Authentication.Login;
using Domain.Entities;
using Services.Abstraction.Cqrs.Commands;

namespace Application.OAuth.Login;

public record LoginOAuthVkCommand(User SignedUser) : ICommand<LoginResponseDto>;