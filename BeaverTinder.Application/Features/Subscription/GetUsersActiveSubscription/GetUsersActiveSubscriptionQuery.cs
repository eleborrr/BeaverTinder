using BeaverTinder.Application.Services.Abstractions.Cqrs.Queries;
using BeaverTinder.Shared.Dto.Subscription;

namespace Application.Subscription.GetUsersActiveSubscription;

public record GetUsersActiveSubscriptionQuery(string UserId): IQuery<SubscriptionInfoDto>;