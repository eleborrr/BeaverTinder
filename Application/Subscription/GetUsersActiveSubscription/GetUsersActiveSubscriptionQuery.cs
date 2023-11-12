using Contracts.Dto.Subscription;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Subscription.GetUsersActiveSubscription;

public class GetUsersActiveSubscriptionQuery: IQuery<SubscriptionInfoDto>
{
    public string UserId { get; set; } = default!;
}