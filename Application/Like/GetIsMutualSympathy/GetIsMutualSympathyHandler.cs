﻿using Contracts.Dto.MediatR;
using Domain.Repositories;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Like.GetIsMutualSympathy;

public class GetIsMutualSympathyHandler : IQueryHandler<GetIsMutualSympathyQuery, bool>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetIsMutualSympathyHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    public async Task<Result<bool>> Handle(GetIsMutualSympathyQuery request, CancellationToken cancellationToken)
    {
        var isMutualSympathy = (await _repositoryManager.LikeRepository.GetAllAsync(default))
                               .Any(l => l.UserId == request.user1.Id && l.LikedUserId == request.user2.Id && l.Sympathy)
                               &&
                               (await _repositoryManager.LikeRepository.GetAllAsync(default))
                               .Any(l => l.UserId == request.user2.Id && l.LikedUserId == request.user1.Id && l.Sympathy);
        return new Result<bool>(isMutualSympathy, true, null);
    }
}