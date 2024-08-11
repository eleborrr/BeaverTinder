using BeaverTinder.Subscription.Core.Abstractions.Repositories;
using Google.Protobuf.WellKnownTypes;
using BeaverTinder.Shared;
using Grpc.Core;

namespace BeaverTinder.Subscription.Services;

public class SubscriptionRpcService : BeaverTinder.Shared.Subscription.SubscriptionBase
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUserSubscriptionRepository _userSubscriptionRepository;
    
    public SubscriptionRpcService(ISubscriptionRepository repository, IUserSubscriptionRepository userSubscriptionRepository)
    {
        _subscriptionRepository = repository;
        _userSubscriptionRepository = userSubscriptionRepository;
    }

    public override async Task<SubscriptionsList> GetAll(Empty request, ServerCallContext context)
    {
        var result = new SubscriptionsList();
        var subscriptions = (await _subscriptionRepository.GetAllASync(default))
            .Select(sub => new SubscriptionMsg()
            {
                SubscriptionId = sub.Id,
                Description = sub.Description,
                Name = sub.Name,
                PricePerMonth = sub.PricePerMonth,
                RoleId = sub.RoleId,
                RoleName = sub.RoleName
            });
        foreach (var subscription in subscriptions)
        {
            result.Subscriptions.Add(subscription);
        }

        Console.WriteLine(result.Subscriptions.Count);

        return result;
    }

    public override async Task<UpdateSubscriptionResponse> AddUserSubscription(UpdateSubscriptionMsg request, ServerCallContext context)
    {
        var userSub =
            await _userSubscriptionRepository
                .GetUserSubscriptionByUserIdAndSubsIdAsync(request.SubscriptionId, request.UserId);
        if (userSub == null)
        {
            await _userSubscriptionRepository
                .AddUserSubscriptionAsync(request.SubscriptionId, request.UserId);
           
            return new UpdateSubscriptionResponse
            {
                Result = true,
                Message = "Subscription successfully added",
                RoleName = userSub.RoleName
            };
        }
        if (userSub.Active)
        {
            var exp = userSub.Expires;
            userSub.Expires = exp + TimeSpan.FromDays(30);
            await _userSubscriptionRepository.SaveAsync();
            return new UpdateSubscriptionResponse
            {
                Result = true,
                Message = "Subscription successfully updated",
                RoleName = userSub.RoleName
            };
        }
        await _userSubscriptionRepository.UpdateUserSubAsync(request.SubscriptionId, request.UserId);
        return new UpdateSubscriptionResponse()
        {
            Result = true,
            Message = "Subscription successfully updated",
            RoleName = userSub.RoleName
        };
    }
    
    public override async Task<GetActiveUserSubscriptionsListResponse> GetActiveSubscriptionsByUserId(GetActiveUserSubscriptionRequest request, ServerCallContext context)
    {
        var result = new GetActiveUserSubscriptionsListResponse();
        
        var userSubs = (await _userSubscriptionRepository.GetActiveSubscriptionsByUserIdAsync(request.UserId))
            .Where(sub => sub.Active).ToList();
        if (userSubs is null || userSubs.Count == 0)
        {
            return result;
        }

        foreach (var sub in userSubs)
        {
            result.Subscriptions.Add(
                new SubscriptionInfoMsg
                {
                    Id = sub.SubId,
                    Expires = Timestamp.FromDateTimeOffset(sub.Expires),
                    Name = sub.RoleName,
                    Active = sub.Active,
                });
        }

        return result;
    }

    public override async Task<RefundMsg> RefundUserSubscription(UpdateSubscriptionMsg request, ServerCallContext context)
    {
        await _userSubscriptionRepository.DeleteUserSub(request.SubscriptionId, request.UserId);
        return new RefundMsg { Result = true };
    }
}