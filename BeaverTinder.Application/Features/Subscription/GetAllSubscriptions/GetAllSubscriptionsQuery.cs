using BeaverTinder.Application.Dto.Subscription;
using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;

namespace BeaverTinder.Application.Features.Subscription.GetAllSubscriptions;

public class GetAllSubscriptionsQuery: IQuery<IEnumerable<SubscriptionInfoDto>>
{
    
}