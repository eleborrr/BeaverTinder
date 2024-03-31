using BeaverTinder.Application.Dto.MediatR;
using BeaverTinder.Application.Dto.Subscription;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Domain.Repositories.Abstractions;

namespace BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;

public class GetAllSubscriptionsHandler: IQueryHandler<GetAllSubscriptionsQuery, IEnumerable<SubscriptionInfoDto>>
{
    private readonly IRepositoryManager _repositoryManager;

    public GetAllSubscriptionsHandler(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }
    
    public async Task<Result<IEnumerable<SubscriptionInfoDto>>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await _repositoryManager.SubscriptionRepository.GetAllASync(cancellationToken);

        return new Result<IEnumerable<SubscriptionInfoDto>>(
            subscriptions, true, null);
    }
}