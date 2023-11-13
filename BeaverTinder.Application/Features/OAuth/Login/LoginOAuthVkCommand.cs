using BeaverTinder.Application.Dto.Authentication.Login;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.OAuth.Login;

public record LoginOAuthVkCommand(User SignedUser) : ICommand<LoginResponseDto>;