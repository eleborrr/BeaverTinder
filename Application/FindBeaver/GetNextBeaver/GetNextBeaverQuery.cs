using Contracts.Dto.BeaverMatchSearch;
using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.FindBeaver.GetNextBeaver;

public record GetNextBeaverQuery(
    User? CurrentUser,
    Role? UserRole) : IQuery<SearchUserResultDto>;