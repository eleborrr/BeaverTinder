using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Repositories.Abstractions;
using BeaverTinder.Shared.Dto.Subscription;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using grpcServices;

namespace BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;

public class GetAllSubscriptionsHandler: IQueryHandler<GetAllSubscriptionsQuery, IEnumerable<SubscriptionInfoDto>>
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly grpcServices.Subscription.SubscriptionClient _subscriptionClient;

    public GetAllSubscriptionsHandler(IRepositoryManager repositoryManager, grpcServices.Subscription.SubscriptionClient subscriptionClient)
    {
        _repositoryManager = repositoryManager;
        _subscriptionClient = subscriptionClient;
    }
    
    public async Task<Result<IEnumerable<SubscriptionInfoDto>>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await _subscriptionClient.GetAllAsync(new Empty(), cancellationToken: cancellationToken);
        if (subscriptions is null)
            return new Result<IEnumerable<SubscriptionInfoDto>>(null, false, "");

        var result = subscriptions.Subscriptions.Select(sub => new SubscriptionInfoDto()
        {
            Name = sub.Name,
            Expires = sub.Expires.ToDateTime()
        });
        
        return new Result<IEnumerable<SubscriptionInfoDto>>(
            result, true, null);
    }
}