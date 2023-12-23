using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Application.Features.Like.GetIsMutualSympathy;

public class GetIsMutualSympathyHandler : IQueryHandler<GetIsMutualSympathyQuery, bool>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetIsMutualSympathyHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public async Task<Result<bool>> Handle(GetIsMutualSympathyQuery request, CancellationToken cancellationToken)
    {
        var likes = (await _repositoryManager.LikeRepository.GetAll()).ToList();
        var isMutualSympathy = (likes
                               .Any(l => l.UserId == request.user1.Id && l.LikedUserId == request.user2.Id && l.Sympathy)
                               &&
                               likes
                               .Any(l => l.UserId == request.user2.Id && l.LikedUserId == request.user1.Id && l.Sympathy));
        return new Result<bool>(isMutualSympathy, true, null);
    }
}