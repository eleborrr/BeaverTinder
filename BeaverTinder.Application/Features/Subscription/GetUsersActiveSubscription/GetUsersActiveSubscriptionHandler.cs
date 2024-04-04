using Application.Subscription.GetUsersActiveSubscription;
using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Shared.Dto.Subscription;
using grpcServices;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;

public class GetUsersActiveSubscriptionHandler: 
    IQueryHandler<GetUsersActiveSubscriptionQuery, SubscriptionInfoDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly grpcServices.Subscription.SubscriptionClient _subscriptionClient;
    private readonly DateTime _defaultDateTime =
        new(10, 10, 10, 0, 0, 0, DateTimeKind.Utc);

    public GetUsersActiveSubscriptionHandler(
        UserManager<User> userManager,
        IRepositoryManager repositoryManager, grpcServices.Subscription.SubscriptionClient subscriptionClient)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
        _subscriptionClient = subscriptionClient;
    }

    public async Task<Result<SubscriptionInfoDto>> Handle(
        GetUsersActiveSubscriptionQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return new Result<SubscriptionInfoDto>(null, false, "User not found");
        
        var userSubs = await _subscriptionClient.GetActiveSubscriptionsByUserIdAsync(new GetActiveUserSubscriptionRequest()
        {
            UserId = request.UserId
        }, cancellationToken: cancellationToken);
        
        if (userSubs.Subscriptions.Any(c => c.Name == "Admin"))
        {
            return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto
            {
                Name = "Admin",
                Expires = _defaultDateTime
            }, true);
        }
        if (userSubs.Subscriptions.Any(c => c.Name == "Moderator"))
        {
            return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto
            {
                Name = "Moderator",
                Expires = _defaultDateTime
            }, true);
        }

        var userSub = await _subscriptionClient.GetActiveSubscriptionsByUserIdAsync(new GetActiveUserSubscriptionRequest
            {
                UserId = request.UserId
            }, cancellationToken: cancellationToken);
        if (userSub == null || userSubs.Subscriptions.Count == 0)
        {
            return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto
            {
                Name = "User",
                Expires = _defaultDateTime
            }, true);
        }

        var sub = userSubs.Subscriptions.First();
        return new Result<SubscriptionInfoDto>(new SubscriptionInfoDto
        {
            Name = sub.Name,
            Expires = sub.Expires.ToDateTime()
        }, true);
    }
}