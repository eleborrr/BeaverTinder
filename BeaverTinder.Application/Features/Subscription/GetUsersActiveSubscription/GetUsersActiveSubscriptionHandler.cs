using Application.Subscription.GetUsersActiveSubscription;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Dto.Subscription;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;

public class GetUsersActiveSubscriptionHandler: 
    IQueryHandler<GetUsersActiveSubscriptionQuery, SubscriptionInfoDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly DateTime _defaultDateTime =
        new(10, 10, 10, 0, 0, 0, DateTimeKind.Utc);

    public GetUsersActiveSubscriptionHandler(
        UserManager<User> userManager,
        IRepositoryManager repositoryManager)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
    }

    public async Task<Result<SubscriptionInfoDto>> Handle(
        GetUsersActiveSubscriptionQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        var roles = await _userManager.GetRolesAsync(user!);
        if (roles.Any(c => c == "Admin"))
        {
            return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto
            {
                Name = "Admin",
                Expires = _defaultDateTime
            }, true);
        }
        if (roles.Any(c => c == "Moderator"))
        {
            return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto
            {
                Name = "Moderator",
                Expires = _defaultDateTime
            }, true);
        }
        var userSub = (await _repositoryManager
                .UserSubscriptionRepository.GetActiveSubscriptionsByUserIdAsync(request.UserId))
            .FirstOrDefault();
        if (userSub == null)
        {
            return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto
            {
                Name = "User",
                Expires = _defaultDateTime
            }, true);
        }
        var sub = await _repositoryManager
            .SubscriptionRepository.GetBySubscriptionIdAsync(userSub.SubsId);
        return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto
        {
            Name = sub!.Name,
            Expires = userSub.Expires
        }, true);
    }
}