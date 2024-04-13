using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Entities;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Shared;
using BeaverTinder.Shared.Dto.Subscription;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Identity;

namespace BeaverTinder.Application.Features.Subscription.GetUsersActiveSubscription;

public class GetUsersActiveSubscriptionHandler: 
    IQueryHandler<GetUsersActiveSubscriptionQuery, UserSubscriptionDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly BeaverTinder.Shared.Subscription.SubscriptionClient _subscriptionClient;
    private readonly DateTime _defaultDateTime =
        new(10, 10, 10, 0, 0, 0, DateTimeKind.Utc);

    public GetUsersActiveSubscriptionHandler(
        UserManager<User> userManager,
        IRepositoryManager repositoryManager, BeaverTinder.Shared.Subscription.SubscriptionClient subscriptionClient)
    {
        _userManager = userManager;
        _repositoryManager = repositoryManager;
        _subscriptionClient = subscriptionClient;
    }

    public async Task<Result<UserSubscriptionDto>> Handle(
        GetUsersActiveSubscriptionQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        Console.WriteLine(user);
        if (user is null)
            return new Result<UserSubscriptionDto>(null, false, "User not found");
        var userSubs = await _subscriptionClient.GetActiveSubscriptionsByUserIdAsync(new GetActiveUserSubscriptionRequest()
        {
            UserId = request.UserId
        }, cancellationToken: cancellationToken);
        Console.WriteLine(userSubs);
        if (userSubs == null || userSubs.Subscriptions.Count == 0)
        {
            return new Result<UserSubscriptionDto>(new UserSubscriptionDto
            {
                Name = "User",
                Expires = _defaultDateTime
            }, true);
        }
        
        
        if (userSubs.Subscriptions.Any(c => c.Name == "Admin"))
        {
            return new Result<UserSubscriptionDto>(new UserSubscriptionDto
            {
                Name = "Admin",
                Expires = _defaultDateTime
            }, true);
        }
        if (userSubs.Subscriptions.Any(c => c.Name == "Moderator"))
        {
            return new Result<UserSubscriptionDto>(new UserSubscriptionDto
            {
                Name = "Moderator",
                Expires = _defaultDateTime
            }, true);
        }

        var sub = userSubs.Subscriptions.First();
        return new Result<UserSubscriptionDto>(new UserSubscriptionDto
        {
            Name = sub.Name,
            Expires = sub.Expires.ToDateTime(),
        }, true);
    }
}