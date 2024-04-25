using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Shared.Dto.Subscription;

namespace BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;

public class GetAllSubscriptionsQuery: IQuery<IEnumerable<SubscriptionInfoDto>>
{
    
}