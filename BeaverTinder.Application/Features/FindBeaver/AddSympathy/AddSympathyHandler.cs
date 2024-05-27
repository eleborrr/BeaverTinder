using BeaverTinder.Application.Dto.BeaverMatchSearch;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Features.LikesMade;
using BeaverTinder.Application.Services.Abstractions;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Commands;
using BeaverTinder.Application.Services.Abstractions.Likes;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace BeaverTinder.Application.Features.FindBeaver.AddSympathy;

public class AddSympathyHandler : ICommandHandler<AddSympathyCommand, LikeResponseDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMemoryCache _memoryCache;
    private readonly ILikeService _likeService;
    private readonly IMediator _mediator;

    public AddSympathyHandler(
        UserManager<User> userManager,
        IRepositoryManager repositoryManager,
        IMemoryCache memoryCache,
        IServiceManager _serviceManager,
        IMediator mediator)
    {
        _likeService = _serviceManager.LikeService;
        _repositoryManager = repositoryManager;
        _userManager = userManager;
        _memoryCache = memoryCache;
        _mediator = mediator;
    }

    public async Task<Result<LikeResponseDto>> Handle(AddSympathyCommand request, CancellationToken cancellationToken)
    {
        if (request.User1 is null)
            return new Result<LikeResponseDto>(new LikeResponseDto(
                LikeResponseStatus.Fail,
                "Your account not found"), false);

        var user2 = await _userManager.FindByIdAsync(request.UserId2);
        if (user2 is null)
            return new Result<LikeResponseDto>(new LikeResponseDto(
                LikeResponseStatus.Fail,
                "Can't find user with that id"), false);

        if (request.UserRole is null)
            return new Result<LikeResponseDto>(new LikeResponseDto(
                LikeResponseStatus.Fail,
                "Can't get user's role"), false);

        if (!await CheckSubscriptionLikePermission(request.User1, request.UserRole))
            return new Result<LikeResponseDto>(new LikeResponseDto(
                LikeResponseStatus.Fail,
                "Like limit!"), false);
        MemoryCacheUpdate(request.User1.Id);

        var newLike = new Domain.Entities.Like { UserId = request.User1.Id, LikedUserId = request.UserId2, LikeDate = DateTime.Now, Sympathy = request.Sympathy};
        await _repositoryManager.LikeRepository.AddAsync(newLike);
        await _mediator.Send(new AddLikeMadeQuery(), cancellationToken);
        return new Result<LikeResponseDto>(new LikeResponseDto(LikeResponseStatus.Ok), true);
    }
    
    private async Task<bool> CheckSubscriptionLikePermission(User? user, Role role)
    {
        if (user is null)
            return false;
        return (await _likeService.GetAllAsync())
            .Count(l => l.LikeDate.Date.Day == DateTime.Today.Day && l.UserId == user.Id) <= role.LikesCountAllowed;
    }
    
    private void MemoryCacheUpdate(string userId)
    {
        if (_memoryCache.TryGetValue(userId, out List<User>? likesCache))
        {
            _memoryCache.Set(userId, likesCache is null? new List<User>() :likesCache.Skip(1),
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
        }
    }
    
}