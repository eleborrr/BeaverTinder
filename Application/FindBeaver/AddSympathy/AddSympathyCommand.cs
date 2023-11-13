using Contracts.Dto.BeaverMatchSearch;
using Domain.Entities;
using Services.Abstraction.Cqrs.Commands;

namespace Application.FindBeaver.AddSympathy;

public record AddSympathyCommand(
    User? User1,
    string UserId2,
    bool Sympathy,
    Role? UserRole) : ICommand<LikeResponseDto>;