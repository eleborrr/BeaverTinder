using System.Collections;
using Contracts.Dto.MediatR;
using Domain.Repositories;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Like.GetLikes;

public class GetAllLikesHandler: IQueryHandler<GetAllLikesQuery, IEnumerable<Domain.Entities.Like>>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetAllLikesHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<IEnumerable<Domain.Entities.Like>>> Handle(GetAllLikesQuery request, CancellationToken cancellationToken)
    {
        var likes = await _repositoryManager.LikeRepository.GetAllAsync(default);
        return new Result<IEnumerable<Domain.Entities.Like>>(
            likes, true, null);
    }
}