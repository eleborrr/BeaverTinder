using Application.Geolocation.GetGeolocations;
using Contracts.Dto.MediatR;
using Domain.Entities;
using Domain.Repositories;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Subscription.GetAllSubscriptions;

public class GetAllSubscriptionsHandler: IQueryHandler<GetAllSubscriptionsQuery, IEnumerable<Domain.Entities.Subscription>>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetAllSubscriptionsHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    
    public async Task<Result<IEnumerable<Domain.Entities.Subscription>>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await _repositoryManager.SubscriptionRepository.GetAllASync(cancellationToken);

        return new Result<IEnumerable<Domain.Entities.Subscription>>(
            subscriptions, true, null);
    }
}