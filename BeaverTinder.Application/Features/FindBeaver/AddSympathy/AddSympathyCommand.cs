using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.FindBeaver.AddSympathy;

public record AddSympathyCommand(
    User? User1,
    string UserId2,
    bool Sympathy,
    Role? UserRole) : ICommand<LikeResponseDto>;