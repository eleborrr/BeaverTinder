using Contracts.Dto.Authentication.Login;
using Domain.Entities;
using Services.Abstraction.Cqrs.Commands;

namespace Application.OAth.Login;

public record LoginOAuthVkCommand(User SignedUser) : ICommand<LoginResponseDto>;