using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;

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