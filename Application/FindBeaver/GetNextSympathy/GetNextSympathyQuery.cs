using Contracts.Dto.BeaverMatchSearch;
using Domain.Entities;
using Services.Abstraction.Cqrs.Queries;

namespace Application.FindBeaver.GetNextSympathy;

public record GetNextSympathyQuery(User? CurrentUser) : IQuery<SearchUserResultDto>;