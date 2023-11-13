using Contracts.Dto.Subscription;
using Services.Abstraction.Cqrs.Queries;

namespace Application.Subscription.GetUsersActiveSubscription;

public record GetUsersActiveSubscriptionQuery(string UserId): IQuery<SubscriptionInfoDto>;