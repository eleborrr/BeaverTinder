using BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;
using BeaverTinder.Shared.Dto.Subscription;
using MediatR;

namespace BeaverTinder.Mobile.Graphql.Subscription.Queries;

public class SubscriptionQuery
{
    private readonly IMediator _mediator;

    public SubscriptionQuery(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<SubscriptionInfoDto>> GetAllSubscriptions()
    {
        var query = new GetAllSubscriptionsQuery();
        var subscriptions = (await _mediator.Send(query)).Value;
        if (subscriptions is null)
            return new List<SubscriptionInfoDto>();
        return subscriptions.ToList();
    }
}
