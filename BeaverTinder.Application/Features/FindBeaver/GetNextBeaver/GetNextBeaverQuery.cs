using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.FindBeaver.GetNextBeaver;

public record GetNextBeaverQuery(
    User? CurrentUser,
    Role? UserRole) : IQuery<SearchUserResultDto>;