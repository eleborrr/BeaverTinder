using BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;
using BeaverTinder.Shared.Dto.Subscription;
using MediatR;

namespace BeaverTinder.Mobile.Graphql.Shared;

public partial class Queries
{
    public async Task<List<SubscriptionInfoDto>> GetAllSubscriptions()
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var query = new GetAllSubscriptionsQuery();
        var subscriptions = (await mediator.Send(query)).Value;
        if (subscriptions is null)
            return new List<SubscriptionInfoDto>();
        return subscriptions.ToList();
    }
}