using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;

namespace BeaverTinder.Application.Features.FindBeaver.GetNextSympathy;

public record GetNextSympathyQuery(User? CurrentUser) : IQuery<SearchUserResultDto>;