using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Shared.Dto.Subscription;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;

public class GetAllSubscriptionsHandler: IQueryHandler<GetAllSubscriptionsQuery, IEnumerable<SubscriptionInfoDto>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly BeaverTinder.Shared.Subscription.SubscriptionClient _subscriptionClient;

    public GetAllSubscriptionsHandler(IRepositoryManager repositoryManager, BeaverTinder.Shared.Subscription.SubscriptionClient subscriptionClient)
    {
        _repositoryManager = repositoryManager;
        _subscriptionClient = subscriptionClient;
    }
    
    public async Task<Result<IEnumerable<SubscriptionInfoDto>>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await _subscriptionClient.GetAllAsync(new Empty(), cancellationToken: cancellationToken);
        if (subscriptions is null)
            return new Result<IEnumerable<SubscriptionInfoDto>>(null, false, "can't reach subscription service");

        if (subscriptions.Subscriptions is null || subscriptions.Subscriptions.Count == 0)
            return new Result<IEnumerable<SubscriptionInfoDto>>(null, true, "no subscriptions found");
        
        var result = subscriptions.Subscriptions.Select(sub => new SubscriptionInfoDto()
        {
            Id = sub.SubscriptionId,
            Name = sub.Name,
            PricePerMonth = sub.PricePerMonth,
            Description = sub.Description
        });
        
        return new Result<IEnumerable<SubscriptionInfoDto>>(
            result, true, null);
    }
}